using MYGIS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson_11
{
    public partial class Form2 : Form
    {
        GISLayer Layer;
        public Form2(GISLayer _layer)
        {
            InitializeComponent();
            Layer = _layer;
            //for (int i = 0; i < layer.Fields.Count; i++) //添加一系列的列
            //{
            //    dataGridView1.Columns.Add(layer.Fields[i].name, layer.Fields[i].name);
            //}
            //for (int i = 0; i < layer.FeatureCount(); i++)
            //{
            //    dataGridView1.Rows.Add();
            //    for (int j = 0; j < layer.Fields.Count; j++)
            //    {
            //        dataGridView1.Rows[i].Cells[j].Value = layer.GetFeature(i).getAttribute(j);
            //    }

            //}
            
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            FillValue();
        }

        private void FillValue()//初始化DataGridView的部分移动到了此函数中
        {
            //增加ID列
            dataGridView1.Columns.Add("ID", "ID");
            //增加其他列 用以记录所有字段
            for (int i=0;i< Layer.Fields.Count; i++)
            {
                dataGridView1.Columns.Add(Layer.Fields[i].name, Layer.Fields[i].name);
            }
            //增加行
            for (int i = 0; i < Layer.FeatureCount(); i++)
            {
                dataGridView1.Rows.Add();
                //增加ID值
                for (int j = 0; j < Layer.Fields.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j+1].Value = Layer.GetFeature(i).getAttribute(j);
                }
                //确定每行的选择状态
                dataGridView1.Rows[i].Selected = Layer.GetFeature(i).Selected;
            }
        }
    }
}
