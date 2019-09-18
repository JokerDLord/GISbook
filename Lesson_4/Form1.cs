using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MYGIS;

namespace Lesson_4
{
    public partial class Form1 : Form
    {
        List<GISFeature> features = new List<GISFeature>();
        GISView view = null;
        public Form1()
        {
            InitializeComponent();
            view = new GISView(new GISExtent
                (new GISVertex(0, 0), new GISVertex(100, 100)), ClientRectangle);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            double x = Convert.ToDouble(textBox1.Text);
            double y = Convert.ToDouble(textBox2.Text);
            GISVertex onevertex = new GISVertex(x, y);
            GISPoint onepoint = new GISPoint(onevertex);
            //获取属性信息
            string attribute = textBox3.Text;
            GISAttribute oneattribute = new GISAttribute();
            oneattribute.AddValue(attribute);
            //新建一个GISFeature 并添加到features数组中
            GISFeature onefeature = new GISFeature(onepoint, oneattribute);
            features.Add(onefeature);
            //画出这个GISFeature
            Graphics graphics = this.CreateGraphics();
            onefeature.draw(graphics, view, true, 0); 
            //参数分别是画笔 是否绘制属性 属性列表values的索引
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {//点击空间对象显示属性信息
            //根据鼠标的点击创建节点信息
            GISVertex mouselocation = view.ToMapVertex(new Point(e.X, e.Y));
            double mindistance = Double.MaxValue;
            int id = -1;
            //通过遍历找出features数组中元素的中心点与点击位置最近的点
            for (int i = 0; i < features.Count; i++)
            {
                double onedistance = features[i].spatialpart.centroid.Distance(mouselocation);
                if (onedistance < mindistance)
                {
                    mindistance = onedistance;
                    id = i;
                }
            }
            //判断是否存在空间对象
            if (id == -1)
            {
                MessageBox.Show("没有任何空间对象！！");
                return;//结束了
            }

            Point nearestpoint = view.ToScreenPoint(features[id].spatialpart.centroid);
            int screendistance = Math.Abs(nearestpoint.X - e.X) + 
                Math.Abs(nearestpoint.Y - e.Y);
            if (screendistance > 5)
            {
                MessageBox.Show("请靠近空间对象点击");
                return;
            }
            MessageBox.Show("该空间对象的属性是 " + features[id].getAttribute(0));            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //从文本框获取更新的地图范围
            double minx = Double.Parse(textBox4.Text);
            double miny = Double.Parse(textBox5.Text);
            double maxx = Double.Parse(textBox6.Text);
            double maxy = Double.Parse(textBox7.Text);
            //更新view
            view.Update(new GISExtent(minx, maxx, miny, maxy), ClientRectangle);
            UpdateMap();
            
        }
        private void UpdateMap()
        {
            Graphics graphics = CreateGraphics();
            //用黑色填充整个窗口
            graphics.FillRectangle(new SolidBrush(Color.Black), ClientRectangle);
            //根据新的view在窗口上绘制数组中的每个空间对象
            for (int i = 0; i < features.Count; i++)
            {
                features[i].draw(graphics, view, true, 0);
            }
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void MapButtonClick(object sender, EventArgs e)
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
    }
}
