using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace MYGIS
{
    class GISVertex //节点类拥有算距离的方法 
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
    }
    class GISPoint:GISSpatial
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
    class GISLine:GISSpatial
    {
        List<GISVertex> AllVertexs;
        public override void draw(Graphics graphics, GISView view)
        {
            
        }
    }
    class GISPolygon:GISSpatial
    {
        List<GISVertex> AllVertexs;
        public override void draw(Graphics graphics, GISView view)
        {

        }
    }


    class GISFeature
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

    class GISAttribute
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

    abstract class GISSpatial //抽象的空间信息类
    {
        public GISVertex centroid; //中心点 属于节点类的对象
        public GISExtent extent;   //空间范围 最小外接矩形
        public abstract void draw(Graphics graphics, GISView view);
    }//有了抽象的空间spatial类之后 就可以重新定义三个对象实体类

    class GISExtent //这是一个重要的类 空间范围
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
    }

    class GISView
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
    }

}