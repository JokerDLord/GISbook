﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MYGIS;

namespace Lesson_13
{
    public partial class Form1 : Form
    {
        GISLayer layer = null;
        GISView view = null;
        Form2 AttributeWindow = null;
        BufferedGraphics backMap;
        BufferedGraphicsContext backWindow;

        Bitmap bitbackwindow;

        //记录鼠标按钮被按下后的位置和鼠标移动中的位置
        MOUSECOMMAND MouseCommand = MOUSECOMMAND.Select;
        int MouseStartX = 0;
        int MouseStartY = 0;
        int MouseMovingX = 0;
        int MouseMovingY = 0;

        bool MouseOnMap = false;//用于确定up是接续前面down且都是在地图窗口中被激活的

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint |
                System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                true);
            backWindow = BufferedGraphicsManager.Current;
            view = new GISView(new GISExtent(new GISVertex(0, 0), new GISVertex(1, 1)), ClientRectangle);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Shapefile文件|*.shp";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            layer = GISShapefile.ReadShapefile(openFileDialog.FileName);
            layer.DrawAttributeOrNot = false;
            MessageBox.Show("read " + layer.FeatureCount() + "objects");
            view.UpdateExtent(layer.Extent);
            UpdateMap();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            view.UpdateExtent(layer.Extent);
            UpdateMap();
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


            //原始方法 不同上
            if (ClientRectangle.Width * ClientRectangle.Height == 0) return;
            //确保view窗口尺寸
            view.UpdateRectangle(ClientRectangle);
            //建立背景窗口
            if (bitbackwindow != null) bitbackwindow.Dispose();
            bitbackwindow = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            //在背景窗口绘图
            Graphics g = Graphics.FromImage(bitbackwindow);
            g.FillRectangle(new SolidBrush(Color.Black), ClientRectangle);
            layer.draw(g, view);
            //把背景窗口绘制到前景
            Graphics frontgraphics = CreateGraphics();
            frontgraphics.DrawImage(bitbackwindow, 0, 0);


            UpdateStatusBar();
        }
        public void MapButtonClick(object sender, EventArgs e)
        {
            GISMapActions action = GISMapActions.zoomin;
            if ((Button)sender == button3) action = GISMapActions.zoomin;
            else if ((Button)sender == button4) action = GISMapActions.zoomout;
            else if ((Button)sender == button5) action = GISMapActions.moveup;
            else if ((Button)sender == button6) action = GISMapActions.movedown;
            else if ((Button)sender == button7) action = GISMapActions.moveleft;
            else if ((Button)sender == button8) action = GISMapActions.moveright;
            view.ChangeView(action);
            UpdateMap();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenAttributeWindow();
        }

        private void OpenAttributeWindow()
        {
            //如果图层为空就返回
            if (layer == null) return;
            //如果属性窗口没有初始化 则初始化
            if (AttributeWindow == null)
                AttributeWindow = new Form2(layer, this);
            //如果窗口资源被释放了 则初始化
            if(AttributeWindow.IsDisposed)
                AttributeWindow = new Form2(layer, this);
            //显示窗口属性
            AttributeWindow.Show();
            //如果属性窗口最小化了 令他正常显示
            if (AttributeWindow.WindowState == FormWindowState.Minimized)
                AttributeWindow.WindowState = FormWindowState.Normal;
            //吧属性窗口放在最前端显示
            AttributeWindow.BringToFront();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GISMyFile.WriteFile(layer, @"D:\mygisfile\mygisfile.jkgeo");
            MessageBox.Show("done.");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            layer = GISMyFile.ReadFile(@"D:\mygisfile\mygisfile.jkgeo");
            MessageBox.Show("read " + layer.FeatureCount() + " objects.");
            view.UpdateExtent(layer.Extent);
            UpdateMap();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (layer == null) return;
            GISVertex v = view.ToMapVertex(e.Location);
            Console.WriteLine("mapvertex @" + v.x.ToString() +"|"+ v.y.ToString()); //此处鼠标点到地图点的转换???
            SelectResult sr = layer.Select(v, view);
            if (sr == SelectResult.OK)
            {
                UpdateMap();
                //toolStripStatusLabel1.Text = layer.Selection.Count.ToString();
                //toolStripStatusLabel2.Text = "click @" + v.x.ToString() + "|" + v.y.ToString();
                UpdateAttributeWindow();
                //statusStrip1.Text = layer.Selection.Count.ToString();
            }
        }

        private void btClearSelect_Click(object sender, EventArgs e)
        {
            if (layer == null) return;
            layer.ClearSelection();
            UpdateMap();
            //toolStripStatusLabel1.Text = "0";
            toolStripStatusLabel2.Text = "click @";
            //statusStrip1.Text = "0";
            UpdateAttributeWindow();
        }

        private void UpdateAttributeWindow()
        {
            //如果图层为空，则返回
            if (layer == null) return;
            //如果属性窗口为空，则返回
            if (AttributeWindow==null) return;
            //如果属性窗口已经释放 则返回
            if (AttributeWindow.IsDisposed) return;
            //调用属性窗口更新函数
            AttributeWindow.UpdateData();
        }

        public void UpdateStatusBar()
        {
            toolStripStatusLabel1.Text = layer.Selection.Count.ToString();
        }
        

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
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

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //if (layer == null) return;
            //if (ClientRectangle.Width == 0 || ClientRectangle.Height == 0) return;
            //view.UpdateWindowSize(ClientRectangle);//先根据窗口大小更改extent 
            //UpdateMap();//再更新图中内容
            UpdateMap();//再更新图中内容
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMovingX = e.X;
            MouseMovingY = e.Y;
            //重绘 根据不同的鼠标操作（选择、缩放、平移），会有不同的事件处理
            if (MouseOnMap) Invalidate();

            GISVertex wv = view.ToMapVertex(e.Location);
            toolStripStatusLabel2.Text = "mouse pointer@" + wv.x.ToString() + "|" + wv.y.ToString();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseStartX = e.X;
            MouseStartY = e.Y;
            //如果按的是左键且当前有命令
            MouseOnMap = (e.Button == MouseButtons.Left && MouseCommand != MOUSECOMMAND.Unused);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (layer == null) return;
            if (MouseOnMap == false) return;
            MouseOnMap = false;
            switch (MouseCommand)
            {
                case MOUSECOMMAND.Select:
                    //如果CTRL没被按住，就清空选择集 按住CTRL键表示选择多个 即向选择集中新增空间对象
                    if (Control.ModifierKeys != Keys.Control) layer.ClearSelection();
                    //初始化选择结果
                    SelectResult sr = SelectResult.UnknownType;
                    if (e.X == MouseStartX && e.Y == MouseStartY)
                    {
                        //点选
                        GISVertex v = view.ToMapVertex(e.Location);
                        sr = layer.Select(v, view);
                    }
                    else
                    {
                        //框选
                        GISExtent extent = view.Rect2Extent(e.X, MouseStartX, e.Y, MouseStartY);
                        sr = layer.Select(extent);
                    }
                    if (sr == SelectResult.OK || Control.ModifierKeys != Keys.Control)
                    {
                        UpdateMap();
                        UpdateAttributeWindow();
                    }
                    break;
                case MOUSECOMMAND.Zoomin:
                    break;
                case MOUSECOMMAND.Zoomout:
                    break;
                case MOUSECOMMAND.Pan:
                    break;
            }
        }
    }
}
