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

namespace Lesson_1
{
    public partial class Form1 : Form
    {
        List<GISPoint> points = new List<GISPoint>();
        public Form1()
        {
            InitializeComponent();
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
            string attribute = textBox3.Text;
            GISVertex onevertex = new GISVertex(x, y);
            GISPoint onepoint = new GISPoint(onevertex, attribute);
            Graphics graphics = this.CreateGraphics();
            onepoint.DrawPoint(graphics);
            onepoint.DrawAttribute(graphics); //绘制属性
            points.Add(onepoint);  //保存点到已建好的列表
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {//点击空间对象显示属性信息
            //根据鼠标的点击创建节点信息
            GISVertex onevertex = new GISVertex((double)e.X, (double)e.Y);
            double mindistance = Double.MaxValue;
            int findid = -1;
            for (int i = 0; i < points.Count; i++)//通过循环所有的点找出离鼠标点距离最短的点
            {
                double distance = points[i].Distance(onevertex);
                if (distance < mindistance)
                {
                    mindistance = distance;
                    findid = i;
                }
            }
            if (mindistance > 5 || findid == -1)
            {
                MessageBox.Show("没有点实体或鼠标点击位置不准确！");
            }
            else
                MessageBox.Show(points[findid].Attribute);
        }
    }
}
