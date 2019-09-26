using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MYGIS
{
    public class GISVertex //节点类拥有算距离的方法 
    {
        public double x;
        public double y;

        public GISVertex(double _x, double _y)
        {
            x = _x;
            y = _y;
        }

        public double Distance(GISVertex anothervertex)
        {
            return Math.Sqrt((x - anothervertex.x) * (x - anothervertex.x)
                - (y - anothervertex.y) * (y - anothervertex.y));
        }
        public void CopyFrom(GISVertex v)
        {
            x = v.x;
            y = v.y;
        }
    }

    public class GISPoint:GISSpatial
    {
        //public GISVertex Location; **previous code
        //public string Attribute;

        public GISPoint(GISVertex onevertex)
        {
            centroid = onevertex;
            extent = new GISExtent(onevertex, onevertex);
        }
        public override void draw(Graphics graphics, GISView view)
        {
            Point screenpoint = view.ToScreenPoint(centroid);
            graphics.FillEllipse(new SolidBrush(Color.Red),
                new Rectangle(screenpoint.X - 3, screenpoint.Y - 3, 6, 6));
        }
        public double Distance(GISVertex anothervertex)
        {
            return centroid.Distance(anothervertex);
        }

    }
    public class GISLine : GISSpatial
    {
        List<GISVertex> Vertexes;
        public double length;
        public GISLine(List<GISVertex> _vertexes)
        {
            Vertexes = _vertexes;
            centroid = GISTools.CalculateCentroid(_vertexes);
            extent = GISTools.CalculateExtent(_vertexes);
            length = GISTools.CalculateLength(_vertexes);
        }
        public override void draw(Graphics graphics, GISView view)
        {
            Point[] points = GISTools.GetScreenPoints(Vertexes, view);
            graphics.DrawLines(new Pen(Color.Red, 2), points);
        }
        public GISVertex FromNode()
        {
            return Vertexes[0];
        }
        public GISVertex ToNode()
        {
            return Vertexes[Vertexes.Count - 1];
        }
    }
    public class GISPolygon:GISSpatial
    {
        List<GISVertex> Vertexes;
        public double Area;
        public GISPolygon(List<GISVertex> _vertexes)
        {
            Vertexes = _vertexes;
            centroid = GISTools.CalculateCentroid(_vertexes);
            extent = GISTools.CalculateExtent(_vertexes);
            Area = GISTools.CalculateArea(_vertexes);
        }
        public override void draw(Graphics graphics, GISView view)
        {
            Point[] points = GISTools.GetScreenPoints(Vertexes, view);
            graphics.FillPolygon(new SolidBrush(Color.Yellow), points);
            graphics.DrawPolygon(new Pen(Color.White, 2), points);
        }
    }


    public class GISFeature
    {
        public GISSpatial spatialpart;
        public GISAttribute attributepart; //空间与属性信息

        public GISFeature(GISSpatial spatial, GISAttribute attribute) //构造函数传入空间与属性信息
        {
            spatialpart = spatial;
            attributepart = attribute;
        }
        public void draw(Graphics graphics,GISView view, bool DrawAttributeOrNot, int index)
            //画空间与属性信息
        {
            spatialpart.draw(graphics,view);
            if (DrawAttributeOrNot)
                attributepart.draw(graphics,view, spatialpart.centroid, index);
            //此处引用了attributed的方法
        }
        public object getAttribute(int index)//获取属性值
        {
            return attributepart.GetValue(index);
        }
    }

    public class GISAttribute
    {
        public ArrayList values = new ArrayList();

        public void AddValue(object o)
        {
            values.Add(o);
        }
        public object GetValue(int index)
        {
            return values[index];
        }
        public void draw(Graphics graphics,GISView view, GISVertex location, int index)
        {
            Point screenpoint = view.ToScreenPoint(location);//转换坐标到屏幕点
            graphics.DrawString(values[index].ToString(), 
                new Font("宋体", 20),
                new SolidBrush(Color.Green), 
                new PointF(screenpoint.X, screenpoint.Y));
        }
    }

    public abstract class GISSpatial //抽象的空间信息类
    {
        public GISVertex centroid; //中心点 属于节点类的对象
        public GISExtent extent;   //空间范围 最小外接矩形
        public abstract void draw(Graphics graphics, GISView view);
    }//有了抽象的空间spatial类之后 就可以重新定义三个对象实体类

    public class GISExtent //这是一个重要的类 空间范围
    {
        public GISVertex bottomleft;
        public GISVertex upright; //外接矩形的左下和右上角的节点
        public GISExtent(GISVertex _bottomleft, GISVertex _upright)
        {
            bottomleft = _bottomleft;
            upright = _upright;
        }
        public GISExtent(double x1, double x2, double y1, double y2)
        {
            upright = new GISVertex(Math.Max(x1, x2), Math.Max(y1, y2));
            bottomleft = new GISVertex(Math.Min(x1, x2), Math.Min(y1, y2));
        }
        public double getMinX()
        {
            return bottomleft.x;
        }
        public double getMaxX()
        {
            return upright.x;
        }
        public double getMinY()
        {
            return bottomleft.y;
        }
        public double getMaxY()
        {
            return upright.y;
        }
        public double getWidth()
        {
            return upright.x - bottomleft.x;
        }
        public double getHeight()
        {
            return upright.y - bottomleft.y;
        }

        double ZoomingFactor = 1.2;//定义缩放因子
        double MovingFactor = 0.25;//移动因子

        public void ChangeExtent(GISMapActions action)
        {
            double newminx = bottomleft.x, newminy = bottomleft.y,
                newmaxx = upright.x, newmaxy = upright.y;
            switch (action)
            {
                case GISMapActions.zoomin:
                    newminx = ((getMinX() + getMaxX()) - getWidth() / ZoomingFactor) / 2;
                    newminy = ((getMinY() + getMaxY()) - getHeight() / ZoomingFactor) / 2;
                    newmaxx = ((getMinX() + getMaxX()) + getWidth() / ZoomingFactor) / 2;
                    newmaxy = ((getMinY() + getMaxY()) + getHeight() / ZoomingFactor) / 2;
                    break;
                case GISMapActions.zoomout:
                    newminx = ((getMinX() + getMaxX()) - getWidth() * ZoomingFactor) / 2;
                    newminy = ((getMinY() + getMaxY()) - getHeight() * ZoomingFactor) / 2;
                    newmaxx = ((getMinX() + getMaxX()) + getWidth() * ZoomingFactor) / 2;
                    newmaxy = ((getMinY() + getMaxY()) + getHeight() * ZoomingFactor) / 2;
                    break;
                case GISMapActions.moveup:
                    newminy = getMinY() - getHeight() * MovingFactor;
                    newmaxy = getMaxY() - getHeight() * MovingFactor;
                    break;
                case GISMapActions.movedown:
                    newminy = getMinY() + getHeight() * MovingFactor;
                    newmaxy = getMaxY() + getHeight() * MovingFactor;
                    break;
                case GISMapActions.moveleft:
                    newminx = getMinX() + getWidth() * MovingFactor;
                    newmaxx = getMaxX() + getWidth() * MovingFactor;
                    break;
                case GISMapActions.moveright:
                    newminx = getMinX() - getWidth() * MovingFactor;
                    newmaxx = getMaxX() - getWidth() * MovingFactor;
                    break;
            }
            upright.x = newmaxx;
            upright.y = newmaxy;
            bottomleft.x = newminx;
            bottomleft.y = newminy;
        }
        public void CopyFrom(GISExtent extent)
        {
            upright.CopyFrom(extent.upright);
            bottomleft.CopyFrom(extent.bottomleft);
        }
    }

    public class GISView
    {
        GISExtent CurrentMapExtent;
        Rectangle MapWindowsSize;
        double MapMinX, MapMinY;
        int WinW, WinH;
        double MapW, MapH;
        double ScaleX, ScaleY;
        /*显然有公式
         ScreenX = (MapX-MapMinX)/ScaleX
         ScreenY = WinH-(MapY-MapMinY)/ScaleY   */


        public GISView(GISExtent _extent, Rectangle _rectangle)
        {
            //CurrentMapExtent = _extent;//需要更新extent类使其提供高、宽信息
            //MapWindowsSize = _rectangle;
            Update(_extent, _rectangle);
        }
        public void Update(GISExtent _extent, Rectangle _rectangle)
        {
            CurrentMapExtent = _extent;
            MapWindowsSize = _rectangle;
            MapMinX = CurrentMapExtent.getMinX();
            MapMinY = CurrentMapExtent.getMinY();
            WinW = MapWindowsSize.Width;
            WinH = MapWindowsSize.Height;
            MapW = CurrentMapExtent.getWidth();
            MapH = CurrentMapExtent.getHeight();
            ScaleX = MapW / WinW;
            ScaleY = MapH / WinH;
        }
        public Point ToScreenPoint(GISVertex onevertex)//地图点到屏幕点转换
        {
            double ScreenX = (onevertex.x - MapMinX) / ScaleX;
            double ScreenY = WinH - (onevertex.y - MapMinY) / ScaleY;
            return new Point((int)ScreenX, (int)ScreenY);
        }
        public GISVertex ToMapVertex(Point point)//屏幕点到地图点转换
        {
            double MapX = ScaleX * point.X + MapMinX;
            double MapY = ScaleX * (WinH - point.Y) + MapMinY;
            return new GISVertex(MapX, MapY);
        }
        public void ChangeView(GISMapActions action)
        {
            CurrentMapExtent.ChangeExtent(action);
            Update(CurrentMapExtent, MapWindowsSize);
        }
        public void UpdateExtent(GISExtent extent)
        {
            CurrentMapExtent.CopyFrom(extent);
            Update(CurrentMapExtent, MapWindowsSize);
        }
    }

    //定义一个枚举类型用于记录各种地图浏览操作
    public enum GISMapActions
    {
        zoomin,zoomout,
        moveup,movedown,moveleft,moveright
    };

    public class GISShapefile //用于shapefile文件读取的类
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)] //头文件结构体
        struct ShapefileHeader
        {
            public int Unused1, Unused2, Unused3, Unused4;
            public int Unused5, Unused6, Unused7, Unused8;
            public int ShapeType;
            public double Xmin;
            public double Ymin;
            public double Xmax;
            public double Ymax;
            public double Unused9, Unused10, Unused11, Unused12;
        }
        static ShapefileHeader ReadFileHeader(BinaryReader br) //用于读取文件头的函数
        {//*************************************
            byte[] buff = br.ReadBytes(Marshal.SizeOf(typeof(ShapefileHeader)));
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);//handle读取buff数组在内存中的指针
            //指针指向的内存被映射给一个结构体实例header
            ShapefileHeader header = (ShapefileHeader)Marshal.PtrToStructure
                (handle.AddrOfPinnedObject(), typeof(ShapefileHeader));
            handle.Free(); //释放内存 将其还给C#管理
            return header;
        }

        public static GISLayer ReadShapefile(string shpfilename)
        {
            FileStream fsr = new FileStream(shpfilename, FileMode.Open);//打开shp文件
            BinaryReader br = new BinaryReader(fsr);//获取文件流后用二进制读取工具
            ShapefileHeader sfh = ReadFileHeader(br);//调用之前的函数 获取头文件
            SHAPETYPE ShapeType = (SHAPETYPE)Enum.Parse( //类型整数变对应的枚举值
                typeof(SHAPETYPE), sfh.ShapeType.ToString());
            GISExtent extent = new GISExtent(sfh.Xmax, sfh.Xmin, sfh.Ymax, sfh.Ymin);
            string dbffilename = shpfilename.Replace(".shp", ".dbf");//更改后缀
            DataTable table = ReadDBF(dbffilename);
            GISLayer layer = new GISLayer(shpfilename, ShapeType, extent, ReadFields(table)); //gislayer的构造参数分别是名字 图层类型 范围 *GISField的泛型
            int rowindex = 0; //当前读取的记录位置
            while (br.PeekChar() != -1)
            {
                RecordHeader rh = ReadRecordHeader(br);
                int RecordLength = FromBigToLittle(rh.RecordLength) * 2 - 4;
                byte[] RecordContent = br.ReadBytes(RecordLength);//将记录内容读入字节数组

                if (ShapeType == SHAPETYPE.point)
                {
                    GISPoint onepoint = ReadPoint(RecordContent);
                    GISFeature onefeature = new GISFeature(onepoint, ReadAttribute(table,rowindex));
                    layer.AddFeature(onefeature);
                }
                if (ShapeType == SHAPETYPE.line)
                {
                    List<GISLine> lines = ReadLines(RecordContent);
                    for (int i = 0; i < lines.Count; i++)
                    {
                        GISFeature onefeature = new GISFeature(lines[i], ReadAttribute(table, rowindex));
                        layer.AddFeature(onefeature);
                    }
                }
                if (ShapeType == SHAPETYPE.polygon)
                {
                    List<GISPolygon> polygons = ReadPolygons(RecordContent);
                    for (int i = 0; i < polygons.Count; i++)
                    {
                        GISFeature onefeature = new GISFeature(polygons[i], ReadAttribute(table, rowindex));
                        layer.AddFeature(onefeature);
                    }
                }
                rowindex++;

            }

            br.Close();
            fsr.Close();//归还文件权限于操作系统
            return layer;//最后返回一个图层文件
        }
        static GISPoint ReadPoint(byte[] RecordContent)
        {//从字节数组指定位置的八个字节转换为双精度浮点数
            double x = BitConverter.ToDouble(RecordContent, 0);
            double y = BitConverter.ToDouble(RecordContent, 8);
            return new GISPoint(new GISVertex(x, y));
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)] //逐条记录的记录头的结构体
        struct RecordHeader
        {
            public int RecordNumber;
            public int RecordLength;
            public int ShapeType;
        }
        static RecordHeader ReadRecordHeader(BinaryReader br) 
            //用于读取记录头的函数 几乎与读文件头的函数相同
        {//*************************************
            byte[] buff = br.ReadBytes(Marshal.SizeOf(typeof(RecordHeader)));
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);//handle读取buff数组在内存中的指针
            //指针指向的内存被映射给一个结构体实例header
            RecordHeader header = (RecordHeader)Marshal.PtrToStructure
                (handle.AddrOfPinnedObject(), typeof(RecordHeader));
            handle.Free(); //释放内存 将其还给C#管理
            return header;
        }
        //通用转换函数 可用于BigInteger颠倒字节顺序重新构造正确数值
        static int FromBigToLittle(int bigvalue)
        {
            byte[] bigbytes = new byte[4];
            GCHandle handle = GCHandle.Alloc(bigbytes, GCHandleType.Pinned);
            Marshal.StructureToPtr(bigvalue, handle.AddrOfPinnedObject(), false);
            handle.Free();
            byte b2 = bigbytes[2];
            byte b3 = bigbytes[3];
            bigbytes[3] = bigbytes[0];
            bigbytes[1] = b2;
            bigbytes[0] = b3;
            return BitConverter.ToInt32(bigbytes, 0);
        }

        static List<GISLine> ReadLines(byte[] RecordContent)//读取线文件
        {
            int N = BitConverter.ToInt32(RecordContent, 32);//面线数量
            int M = BitConverter.ToInt32(RecordContent, 36);
            int[] parts = new int[N + 1];//前n个节点记录每个独立实体的起始点位置，最后一个元素值为M
            for (int i = 0; i < N; i++) //相当于从40开始 每四个字节都记录的一个对象的起始点位置
            {
                parts[i] = BitConverter.ToInt32(RecordContent, 40 + i * 4);
            }
            parts[N] = M;
            List<GISLine> lines = new List<GISLine>();
            for (int i = 0; i < N; i++)
            {
                List<GISVertex> vertexs = new List<GISVertex>();
                for (int j = parts[i]; j < parts[i + 1]; j++) //之后都是顺序记录着的所有节点的xy坐标
                {
                    double x = BitConverter.ToDouble(RecordContent, 40 + N * 4 + j * 16);
                    double y = BitConverter.ToDouble(RecordContent, 40 + N * 4 + j * 16 + 8);
                    vertexs.Add(new GISVertex(x, y));
                }
                lines.Add(new GISLine(vertexs));
            }
            return lines;

        }

        static List<GISPolygon> ReadPolygons(byte[] RecordContent)//读取线文件
        {
            int N = BitConverter.ToInt32(RecordContent, 32);//面线数量
            int M = BitConverter.ToInt32(RecordContent, 36);
            int[] parts = new int[N + 1];//前n个节点记录每个独立实体的起始点位置，最后一个元素值为M
            for (int i = 0; i < N; i++) //相当于从40开始 每四个字节都记录的一个对象的起始点位置
            {
                parts[i] = BitConverter.ToInt32(RecordContent, 40 + i * 4);
            }
            parts[N] = M;
            List<GISPolygon> polygons = new List<GISPolygon>();
            for (int i = 0; i < N; i++)
            {
                List<GISVertex> vertexs = new List<GISVertex>();
                for (int j = parts[i]; j < parts[i + 1]; j++) //之后都是顺序记录着的所有节点的xy坐标
                {
                    double x = BitConverter.ToDouble(RecordContent, 40 + N * 4 + j * 16);
                    double y = BitConverter.ToDouble(RecordContent, 40 + N * 4 + j * 16 + 8);
                    vertexs.Add(new GISVertex(x, y));
                }
                polygons.Add(new GISPolygon(vertexs));
            }
            return polygons;

        }

        static DataTable ReadDBF(string dbffilename)
        {
            System.IO.FileInfo f = new FileInfo(dbffilename);//通过系统自带的fileinfo对象获取文件所在的路径及文件名
            DataSet ds = null;
            string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                f.DirectoryName + ";Extended Properties=DBASE III";
            using (OleDbConnection con = new OleDbConnection(constr))
            {
                var sql = "select * from " + f.Name; //通过一条sql语句把所有的数据选择出来并加载到dataset实例ds中
                OleDbCommand cmd = new OleDbCommand(sql, con);
                con.Open();
                ds = new DataSet(); ;
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
            }
            return ds.Tables[0];
        }

        static List<GISField> ReadFields(DataTable table)//参数是上一个函数读出的datatable对象
        {
            List<GISField> fields = new List<GISField>();
            foreach (DataColumn column in table.Columns) //遍历column逐个获得字段类型和字段名称
            {
                fields.Add(new GISField(column.DataType, column.ColumnName));
            }
            return fields; //返回值就是gislayer需要的字段结构
        }

        static GISAttribute ReadAttribute(DataTable table, int RowIndex)
        {
            GISAttribute attribute = new GISAttribute();
            DataRow row = table.Rows[RowIndex]; //该函数读取给定序号的rowindex的一行数据
            for (int i = 0; i < table.Columns.Count; i++)
            {
                attribute.AddValue(row[i]);
            }
            return attribute;
        }

        public static void test()
        {
            Console.WriteLine("this is used to test the GitHub");
        }
    }
    public enum SHAPETYPE
    {
        point = 1,
        line = 3,
        polygon = 5
    };

    public class GISLayer
    {
        public string Name;
        public GISExtent Extent;
        public bool DrawAttributeOrNot;
        public int LabelIndex;
        public SHAPETYPE ShapeType;
        List<GISFeature> Features = new List<GISFeature>(); //私有的 不宜改动
        public List<GISField> Fields;
        public GISLayer(string _name, SHAPETYPE _shapetype, GISExtent _extent, List<GISField> _fields)
        {
            Name = _name;
            ShapeType = _shapetype;
            Extent = _extent;
            Fields = _fields;
        }
        public GISLayer(string _name, SHAPETYPE _shapetype, GISExtent _extent)
        {
            Name = _name;
            ShapeType = _shapetype;
            Extent = _extent;
            Fields = new List<GISField>();
        }
        public void draw(Graphics graphics, GISView view)
        {
            for (int i = 0; i < Features.Count; i++)
            {
                Features[i].draw(graphics,view,DrawAttributeOrNot,LabelIndex);
            }
        }
        public void AddFeature(GISFeature feature)
        {
            Features.Add(feature);
        }
        public int FeatureCount()
        {
            return Features.Count;
        }
        public GISFeature GetFeature(int i)
        {
            return Features[i];
        }
        
    }
    public class GISTools
    {
        public static GISVertex CalculateCentroid(List<GISVertex> _vertexes)
        {
            if (_vertexes.Count == 0) return null;
            double x = 0;
            double y = 0;
            for (int i = 0; i < _vertexes.Count; i++)
            {
                x += _vertexes[i].x;
                y += _vertexes[i].y;
            }
            return new GISVertex(x / _vertexes.Count, y / _vertexes.Count);
        }

        public static GISExtent CalculateExtent(List<GISVertex> _vertexes)
        {
            if (_vertexes.Count == 0) return null;
            double minx = Double.MaxValue;
            double miny = Double.MaxValue;
            double maxx = Double.MinValue;
            double maxy = Double.MinValue;
            for (int i = 0; i < _vertexes.Count; i++)
            {
                if (_vertexes[i].x < minx) minx = _vertexes[i].x;
                if (_vertexes[i].y < miny) miny = _vertexes[i].y;
                if (_vertexes[i].x > maxx) maxx = _vertexes[i].x;
                if (_vertexes[i].y < maxy) maxy = _vertexes[i].y;
            }
            return new GISExtent(minx, miny, maxx, maxy);
        }

        public static double CalculateLength(List<GISVertex> _vertexes)
        {
            double length = 0;
            for (int i = 0; i < _vertexes.Count - 1; i++)
            {
                length += _vertexes[i].Distance(_vertexes[i + 1]);
            }
            return length;
        }

        public static double CalculateArea(List<GISVertex> _vertexes)
        {
            double area = 0;
            for (int i = 0; i < _vertexes.Count - 1; i++)
            {
                area += VectorProduct(_vertexes[i], _vertexes[i + 1]);
            }
            area += VectorProduct(_vertexes[_vertexes.Count - 1], _vertexes[0]);//注意此处最后一个点到起始点的矢量也要算
            return area / 2;
        }
        public static double VectorProduct(GISVertex v1, GISVertex v2)
        {
            return v1.x * v2.y - v1.y * v2.x;
        }
        public static Point[] GetScreenPoints(List<GISVertex> _vertexes, GISView view)
        {
            Point[] points = new Point[_vertexes.Count];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = view.ToScreenPoint(_vertexes[i]);
            }
            return points;
        }

        public static byte[] ToBytes(object c) //结构体实例转字节数组的方法
        {
            byte [] bytes = new byte[Marshal.SizeOf(c.GetType())]; //定义一个与结构体字节数等长的字节数组
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);//为指定对象c分配句柄 用于内存操作
            Marshal.StructureToPtr(c, handle.AddrOfPinnedObject(), false);
            handle.Free();
            return bytes;
        }

        //string转bw写入函数
        public static void WriteString(string s, BinaryWriter bw)
        {//先写一个整数记录字符串长度 再将string变字节数组写入
            bw.Write(StringLength(s)); //一般情况下字符数都是字节数 但是中文会占用两个字节
            byte[] sbytes = Encoding.Default.GetBytes(s);
            bw.Write(sbytes);
        }

        public static int StringLength(string s)
        {
            int ChineseCount = 0;
            //将字符串转换为ASCII码来编码的字节数组
            byte[] bs = new ASCIIEncoding().GetBytes(s);
            foreach (byte b in bs) {
                //转bs时所有双字节中文会被转换成单字节的0X3F
                if (b == 0X3F) ChineseCount++;
            }
            return ChineseCount + bs.Length;
        }

        //给定数据类型转换为整数
        public static int TypeToInt(Type type)
        {
            ALLTYPES onetype = (ALLTYPES)Enum.Parse(typeof(ALLTYPES), type.ToString().Replace(".", "_"));
            return (int)onetype;
        }


    }
    public class GISField
    {
        public Type datatype;
        public string name;
        public GISField(Type _dt, string _name)
        {
            datatype = _dt;
            name = _name;
        }
    }

    public class GISMyFile
    {
        [StructLayout(LayoutKind.Sequential, Pack = 4)]//定义结构体
        struct MyFileHeader
        {
            public double MinX, MinY, MaxX, MaxY;
            public int FeatureCount, Shapetype, FieldCount;
        };

        static void WriteFileHeader(GISLayer layer, BinaryWriter bw)//bw是与文件相连的文件写入工具
        {
            MyFileHeader mfh = new MyFileHeader();
            mfh.MinX = layer.Extent.getMinX();
            mfh.MinY = layer.Extent.getMinY();
            mfh.MaxX = layer.Extent.getMaxX();
            mfh.MaxY = layer.Extent.getMaxY();
            mfh.FeatureCount = layer.FeatureCount();
            mfh.Shapetype = (int)(layer.ShapeType);
            mfh.FieldCount = layer.Fields.Count;
            bw.Write(GISTools.ToBytes(mfh));
        }

        //写文件函数主框架
        public static void WriteFile(GISLayer layer, string filename)
        {
            FileStream fsr = new FileStream(filename, FileMode.Create); //根据文件名创建文件流
            BinaryWriter bw = new BinaryWriter(fsr);
            WriteFileHeader(layer, bw); //写入头文件
            GISTools.WriteString(layer.Name, bw); //写入图层名字
            WriteFields(layer.Fields, bw); //写入字段
            //其他内容
            bw.Close();
            fsr.Close();
        }

        static void WriteFields(List<GISField> fields, BinaryWriter bw)
        {
            for(int fieldindex = 0;fieldindex<fields.Count;fieldindex++)
            {
                GISField field = fields[fieldindex];
                bw.Write(GISTools.TypeToInt(field.datatype)); //字段类型
                GISTools.WriteString(field.name, bw); //字段名
            }
        }

    }

    public enum ALLTYPES //枚举中不允许有. 只有_
    {
        System_Boolean,
        System_Byte,
        System_Char,
        System_Decimal,
        System_Double,
        System_Single,
        System_Int32,
        System_Int64,
        System_SByte,
        System_Int16,
        System_String,
        System_UInt32,
        System_UInt64,
        System_UInt16
    };


}