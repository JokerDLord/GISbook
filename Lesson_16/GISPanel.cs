using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MYGIS
{
    public partial class GISPanel : UserControl
    {

        GISView view = null;
        //AttributeForm AttributeWindow = null;
        Dictionary<GISLayer, AttributeForm> AllAttWnds = new Dictionary<GISLayer, AttributeForm>();
        //BufferedGraphics backMap;
        //BufferedGraphicsContext backWindow;

        Bitmap bitbackwindow;

        //记录鼠标按钮被按下后的位置和鼠标移动中的位置
        MOUSECOMMAND MouseCommand = MOUSECOMMAND.Unused;
        int MouseStartX = 0;
        int MouseStartY = 0;
        int MouseMovingX = 0;
        int MouseMovingY = 0;

        bool MouseOnMap = false;//用于确定up是接续前面down且都是在地图窗口中被激活的
        GISDocument document = new GISDocument();

        public GISPanel()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        public void UpdateMap()
        {
            ////清空之前占用的绘图资源
            //if (backMap != null)
            //{
            //    if (backMap.Graphics != null)
            //    {
            //        backMap.Graphics.Dispose();
            //    }
            //    backMap.Dispose();//释放backmap的所有资源
            //}
            ////初始化绘图资源
            //Graphics frontgraphics = CreateGraphics();
            //backMap = backWindow.Allocate(frontgraphics, ClientRectangle);//创建使用指定的像素格式的指定大小的图像缓冲区
            //frontgraphics.Dispose();
            ////在背景窗口中绘图
            //Graphics graphics = backMap.Graphics;
            //graphics.FillRectangle(new SolidBrush(Color.Black), ClientRectangle);
            //layer.draw(graphics,view);
            ////把绘图内容搬到前端
            //Invalidate();//使整个控件画面无效并重绘控件

            if (view == null)
            {
                if (document.IsEmpty()) return;
                view = new GISView(new GISExtent(document.Extent), ClientRectangle);
            }

            //原始方法 不同上
            if (ClientRectangle.Width * ClientRectangle.Height == 0) return;
            //确保view窗口尺寸
            view.UpdateRectangle(ClientRectangle);
            //建立背景窗口
            if (bitbackwindow != null) bitbackwindow.Dispose();
            bitbackwindow = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            //在背景窗口绘图
            Graphics g = Graphics.FromImage(bitbackwindow);
            g.FillRectangle(new SolidBrush(Color.LightBlue), ClientRectangle);
            document.draw(g, view);
            //layer.draw(g, view);

            //把背景窗口绘制到前景
            Graphics frontgraphics = CreateGraphics();
            frontgraphics.DrawImage(bitbackwindow, 0, 0);


            //UpdateStatusBar();
        }

        public void OpenAttributeWindow(GISLayer layer)
        {
            AttributeForm AttributeWindow = null;
            //如果属性窗口之前已经存在了，就找到它，然后移除记录，稍后统一添加
            if (AllAttWnds.ContainsKey(layer))
            {
                AttributeWindow = AllAttWnds[layer];
                AllAttWnds.Remove(layer);
            }
            //如果属性窗口没有初始化 则初始化
            if (AttributeWindow == null)
                AttributeWindow = new AttributeForm(layer, this);
            //如果窗口资源被释放了 则初始化
            if (AttributeWindow.IsDisposed)
                AttributeWindow = new AttributeForm(layer, this);
            //添加属性窗口与图层的关联记录
            AllAttWnds.Add(layer, AttributeWindow);

            //显示窗口属性
            AttributeWindow.Show();
            //如果属性窗口最小化了 令他正常显示
            if (AttributeWindow.WindowState == FormWindowState.Minimized)
                AttributeWindow.WindowState = FormWindowState.Normal;
            //吧属性窗口放在最前端显示
            AttributeWindow.BringToFront();
        }

        private void UpdateAttributeWindow()
        {
            //如果文档为空，则返回
            if (document.IsEmpty()) return;
            foreach (AttributeForm AttributeWindow in AllAttWnds.Values)
            {
                //如果属性窗口为空，则继续
                if (AttributeWindow == null) continue;
                //如果属性窗口已经释放 则继续
                if (AttributeWindow.IsDisposed) continue;
                //更新数据
                AttributeWindow.UpdateData();
            }
            ////如果属性窗口为空，则返回
            //if (AttributeWindow == null) return;
            ////如果属性窗口已经释放 则返回
            //if (AttributeWindow.IsDisposed) return;
            //调用属性窗口更新函数
            //AttributeWindow.UpdateData();
        }


        private void GISPanel_Paint(object sender, PaintEventArgs e)
        {
            if (bitbackwindow != null)
            {
                //是鼠标操作引起的窗口重绘
                if (MouseOnMap)
                {
                    //是地图移动
                    if (MouseCommand == MOUSECOMMAND.Pan)
                    {
                        e.Graphics.DrawImage(bitbackwindow, MouseMovingX - MouseStartX,
                            MouseMovingY - MouseStartY);

                    }
                    //是由于选择或缩放操作造成，就画一个框
                    else if (MouseCommand != MOUSECOMMAND.Unused)
                    {
                        e.Graphics.DrawImage(bitbackwindow, 0, 0);
                        e.Graphics.FillRectangle(new SolidBrush(GISConst.ZoomSelectBoxColor),
                            new Rectangle(
                                Math.Min(MouseStartX, MouseMovingX),
                                Math.Min(MouseStartY, MouseMovingY),
                                Math.Abs(MouseStartX - MouseMovingX),
                                Math.Abs(MouseStartY - MouseMovingY)));
                    }
                }
                //如果不是鼠标引起的，直接复制背景窗口
                else
                {
                    //旧方法
                    e.Graphics.DrawImage(bitbackwindow, 0, 0);
                }
            }
            //if (backMap != null)
            //    backMap.Render(e.Graphics);//将图形缓冲区内容写入e绘图对象
            //if (bitbackwindow != null)
            //    e.Graphics.DrawImage(bitbackwindow, 0, 0);
        }


        private void GISPanel_SizeChanged(object sender, EventArgs e)
        {
            //if (layer == null) return;
            //if (ClientRectangle.Width == 0 || ClientRectangle.Height == 0) return;
            //view.UpdateWindowSize(ClientRectangle);//先根据窗口大小更改extent 
            //UpdateMap();//再更新图中内容
            UpdateMap();//再更新图中内容
        }


        private void GISPanel_MouseDown(object sender, MouseEventArgs e)
        {
            MouseStartX = e.X;
            MouseStartY = e.Y;
            //如果按的是左键且当前有命令
            MouseOnMap = ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
                && MouseCommand != MOUSECOMMAND.Unused);
            if (e.Button == MouseButtons.Middle)
            {
                MouseCommand = MOUSECOMMAND.Pan;
            }
        }


        private void GISPanel_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMovingX = e.X;
            MouseMovingY = e.Y;
            //重绘 根据不同的鼠标操作（选择、缩放、平移），会有不同的事件处理
            if (MouseOnMap) Invalidate();

            //GISVertex wv = view.ToMapVertex(e.Location);
            //toolStripStatusLabel2.Text = "mouse pointer@" + wv.x.ToString() + "|" + wv.y.ToString();
        }


        private void GISPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (document.IsEmpty()) return;
            if (MouseOnMap == false) return;
            MouseOnMap = false;
            switch (MouseCommand)
            {
                case MOUSECOMMAND.Select:
                    //如果CTRL没被按住，就清空选择集 按住CTRL键表示选择多个 即向选择集中新增空间对象
                    if (Control.ModifierKeys != Keys.Control) document.ClearSelection();
                    //初始化选择结果
                    SelectResult sr = SelectResult.UnknownType;
                    if (e.X == MouseStartX && e.Y == MouseStartY)
                    {
                        //点选
                        GISVertex v = view.ToMapVertex(e.Location);
                        sr = document.Select(v, view);
                    }
                    else
                    {
                        //框选
                        GISExtent extent = view.Rect2Extent(e.X, MouseStartX, e.Y, MouseStartY);
                        sr = document.Select(extent);
                    }
                    if (sr == SelectResult.OK || Control.ModifierKeys != Keys.Control)
                    {
                        UpdateMap();
                        UpdateAttributeWindow();
                    }
                    break;
                case MOUSECOMMAND.Zoomin:
                    if (e.X == MouseStartX && e.Y == MouseStartY)
                    {
                        //单点放大
                        GISVertex MouseLocation = view.ToMapVertex(e.Location);
                        GISExtent E1 = view.getRealExtent();
                        double newwidth = E1.getWidth() * GISConst.ZoominFactor;
                        double newheight = E1.getHeight() * GISConst.ZoominFactor;
                        double newminx = MouseLocation.x - (MouseLocation.x - E1.getMinX()) * GISConst.ZoominFactor;
                        double newminy = MouseLocation.y - (MouseLocation.y - E1.getMinY()) * GISConst.ZoominFactor;
                        view.UpdateExtent(new GISExtent(newminx, newminx + newwidth, newminy, newminy + newheight));
                    }
                    else
                    {
                        //拉框放大
                        view.UpdateExtent(view.Rect2Extent(e.X, MouseStartX, e.Y, MouseStartY));
                    }
                    UpdateMap();
                    break;
                case MOUSECOMMAND.Zoomout:
                    if (e.X == MouseStartX && e.Y == MouseStartY)
                    {
                        //单点缩小
                        GISExtent e1 = view.getRealExtent();
                        GISVertex mouselocation = view.ToMapVertex(e.Location);
                        double newwidth = e1.getWidth() / GISConst.ZoomoutFactor;
                        double newheight = e1.getHeight() / GISConst.ZoomoutFactor;
                        double newminx = mouselocation.x - (mouselocation.x - e1.getMinX()) / GISConst.ZoomoutFactor;
                        double newminy = mouselocation.y - (mouselocation.y - e1.getMinY()) / GISConst.ZoomoutFactor;
                        view.UpdateExtent(new GISExtent(newminx, newminx + newwidth, newminy, newminy + newheight));
                    }
                    else
                    {
                        //拉框缩小
                        GISExtent e3 = view.Rect2Extent(e.X, MouseStartX, e.Y, MouseStartY);
                        GISExtent e1 = view.getRealExtent();
                        double newwidth = e1.getWidth() * e1.getWidth() / e3.getWidth();
                        double newheight = e1.getHeight() * e1.getHeight() / e3.getHeight();
                        double newminx = e3.getMinX() - (e3.getMinX() - e1.getMinX()) * newwidth / e1.getWidth();
                        double newminy = e3.getMinY() - (e3.getMinY() - e1.getMinY()) * newheight / e1.getHeight();
                        view.UpdateExtent(new GISExtent(newminx, newminx + newwidth, newminy, newminy + newheight));
                    }
                    UpdateMap();
                    break;
                case MOUSECOMMAND.Pan:
                    if (e.X != MouseStartX && e.Y != MouseStartY)
                    {
                        GISExtent e1 = view.getRealExtent();
                        GISVertex m1 = view.ToMapVertex(new Point(MouseStartX, MouseStartY));
                        GISVertex m2 = view.ToMapVertex(e.Location);
                        double newwidth = e1.getWidth();
                        double newheight = e1.getHeight();
                        double newminx = e1.getMinX() - (m2.x - m1.x);
                        double newminy = e1.getMinY() - (m2.y - m1.y);
                        view.UpdateExtent(new GISExtent(newminx, newminx + newwidth, newminy, newminy + newheight));
                        UpdateMap();
                    }
                    break;
            }
        }


        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.Equals(openDocumentToolStripMenuItem))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "GIS Document (*." + GISConst.MYDOC + ")|*." + GISConst.MYDOC;
                openFileDialog.RestoreDirectory = false;
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                document.Read(openFileDialog.FileName);
                if (document.IsEmpty() == false)
                    UpdateMap();
            }
            else if (sender.Equals(layerControlToolStripMenuItem))
            {
                LayerDialog layercontrol = new LayerDialog(document, this);
                layercontrol.ShowDialog();
            }

            //if (document.IsEmpty()) return;
            else if (sender.Equals(fullExtentToolStripMenuItem))
            {
                if (document.IsEmpty() || view == null) return;
                view.UpdateExtent(document.Extent);
                UpdateMap();
            }
            else
            {
                if (document.IsEmpty() || view == null) return;
                selectToolStripMenuItem.Checked = false;
                zoomInToolStripMenuItem.Checked = false;
                zoomOutToolStripMenuItem.Checked = false;
                panToolStripMenuItem.Checked = false;
                ((ToolStripMenuItem)sender).Checked = true;
                if (sender.Equals(selectToolStripMenuItem))
                    MouseCommand = MOUSECOMMAND.Select;
                else if (sender.Equals(zoomInToolStripMenuItem))
                    MouseCommand = MOUSECOMMAND.Zoomin;
                else if (sender.Equals(zoomOutToolStripMenuItem))
                    MouseCommand = MOUSECOMMAND.Zoomout;
                else if (sender.Equals(panToolStripMenuItem))
                    MouseCommand = MOUSECOMMAND.Pan;
            }
        }

        private void GISPanel_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(this.PointToScreen(e.Location));

            //if (layer == null) return;
            //if (sender.Equals(fullExtentToolStripMenuItem))
            //{
            //    view.UpdateExtent(layer.Extent);
            //    UpdateMap();
            //}
            //else
            //{
            //    selectToolStripMenuItem.Checked = false;
            //    zoomInToolStripMenuItem.Checked = false;
            //    zoomOutToolStripMenuItem.Checked = false;
            //    panToolStripMenuItem.Checked = false;
            //    ((ToolStripMenuItem)sender).Checked = true;
            //    if (sender.Equals(selectToolStripMenuItem))
            //        MouseCommand = MOUSECOMMAND.Select;
            //    else if (sender.Equals(zoomInToolStripMenuItem))
            //        MouseCommand = MOUSECOMMAND.Zoomin;
            //    else if (sender.Equals(zoomOutToolStripMenuItem))
            //        MouseCommand = MOUSECOMMAND.Zoomout;
            //    else if (sender.Equals(panToolStripMenuItem))
            //        MouseCommand = MOUSECOMMAND.Pan;
            //}
        }

    }
}
