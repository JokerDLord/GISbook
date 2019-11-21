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

namespace Lesson_14
{
    public partial class Form2 : Form
    {
        GISLayer Layer;
        Form1 MapWindow = null;
        bool FromMapWindow = true;//记录选择数据集的来源
        public Form2(GISLayer _layer,Form1 mapwindow)
        {
            InitializeComponent();
            Layer = _layer;
            MapWindow = mapwindow;
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
            FromMapWindow = true;
            FillValue();
            FromMapWindow = false;
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
                dataGridView1.Rows[i].Cells[0].Value = Layer.GetFeature(i).ID;
                //增加其他属性值
                for (int j = 0; j < Layer.Fields.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j+1].Value = Layer.GetFeature(i).getAttribute(j);
                }
                //确定每行的选择状态
                dataGridView1.Rows[i].Selected = Layer.GetFeature(i).Selected;
            }
        }

        public void UpdateData()
        {
            FromMapWindow = true;
            dataGridView1.ClearSelection();
            //根据layer选中的gisfeature的id确定其处于哪个row
            foreach (GISFeature feature in Layer.Selection)
                SelectRowByID(feature.ID).Selected = true;
            FromMapWindow = false;
        }

        public DataGridViewRow SelectRowByID(int id)//判断该row的第一个cell是不是所需的id
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
                if ((int)(row.Cells[0].Value) == id) return row;
            return null;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //如果是来自地图窗口的就不继续
            if (FromMapWindow) return;
            //如果两个窗口当前选集都是空的 也没必要继续
            if (Layer.Selection.Count == 0 && dataGridView1.SelectedRows.Count == 0) return;
            //更新当前窗口的选择集
            Layer.ClearSelection();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                //空值也可能会被选中
                if (row.Cells[0].Value != null)
                    Layer.AddSelectedFeatureByID((int)(row.Cells[0].Value));
            }
            //更新地图窗口的显示
            MapWindow.UpdateMap();
        }
    }
}
