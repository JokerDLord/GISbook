using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MYGIS
{    
    public partial class LayerDialog : Form
    {
        GISDocument Document;
        GISPanel Mapwindow;

        public LayerDialog(GISDocument document, GISPanel mapwindow)
        {
            InitializeComponent();
            Document = document;
            Mapwindow = mapwindow;
        }

        private void Form3_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i < Document.layers.Count; i++)
            {
                listBox1.Items.Insert(0, Document.layers[i].Name);
            }
            if (Document.layers.Count > 0)
            {
                listBox1.SelectedIndex = 0;//缺省选择项为第一项
            }
        }


        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //用以应对listbox选择改变的情况 更改相应的项
            if (listBox1.SelectedItem == null) return;
            GISLayer layer = Document.getLayer(listBox1.SelectedItem.ToString());//根据选中情况获取对应的layer
            checkBox1.Checked = layer.Selectable;
            checkBox2.Checked = layer.Visible;
            checkBox3.Checked = layer.DrawAttributeOrNot;
            comboBox1.Items.Clear();
            for (int i = 0; i < layer.Fields.Count; i++)
            {
                comboBox1.Items.Add(layer.Fields[i].name);
                comboBox3.Items.Add(layer.Fields[i].name);
            }
            comboBox1.SelectedIndex = (layer.Fields.Count > 0) ? layer.LabelIndex : -1;
            comboBox3.SelectedIndex = (layer.Fields.Count > 0) ? layer.ThematicFieldIndex : -1;
            fileaddr.Text = layer.Path;
            textBox1.Text = layer.Name;

            if (layer.ThematicType == THEMATICTYPE.UnifiedValue)
            {
                comboBox2.SelectedIndex = 0;
                GISThematic Thematic = layer.Thematics[layer.ThematicType];
                btfillcolor.BackColor = Thematic.InsideColor;
                tbdrawsize.Text = Thematic.Size.ToString();
                btboundcolor.BackColor = Thematic.OutsideColor;
            }
            else if (layer.ThematicType == THEMATICTYPE.UniqueValue)
            {
                comboBox2.SelectedIndex = 1;                
            }
            else if (layer.ThematicType == THEMATICTYPE.GradualColor)
            {
                comboBox2.SelectedIndex = 2;
                tblevelnumber.Text = layer.Thematics.Count.ToString();
            }

        }

        private void SettingColor_Click(object sender, EventArgs e)
        {
            ColorDialog colord = new ColorDialog();
            colord.Color = ((Button)sender).BackColor;
            if (colord.ShowDialog() == DialogResult.OK)
            {
                //在打开的颜色对话框中设置颜色，并用新的颜色更新按钮背景
                ((Button)sender).BackColor = colord.Color;
                Clicked(sender, e);
            }
        }

        private void Form3_Click(object sender, EventArgs e)
        {

        }

        private void Clicked(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            GISLayer layer = Document.getLayer(listBox1.SelectedItem.ToString());
            layer.Selectable = checkBox1.Checked;
            layer.Visible = checkBox2.Checked;
            layer.DrawAttributeOrNot = checkBox3.Checked;
            layer.LabelIndex = comboBox1.SelectedIndex;
            //if (layer.ThematicType == THEMATICTYPE.UnifiedValue)
            //{
            //    GISThematic Thematic = layer.Thematics[layer.ThematicType];
            //    Thematic.InsideColor = btfillcolor.BackColor;
            //    Thematic.OutsideColor = btboundcolor.BackColor;
            //    Thematic.Size = ((tbdrawsize.Text == "") ? Thematic.Size : Int32.Parse(tbdrawsize.Text));

            //}

        }

        private void Bteditname_Click(object sender, EventArgs e)
        {
            //修改图层名
            if (listBox1.SelectedItem == null) return;
            //确保新输入的图层名不会与选中之外的其他图层名相同
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (i != listBox1.SelectedIndex)
                {
                    if (listBox1.Items[i].ToString() == textBox1.Text)
                    {
                        MessageBox.Show("不能与已有图层名重复!!");
                        return;
                    }
                }
            }
            GISLayer layer = Document.getLayer(listBox1.SelectedItem.ToString());
            layer.Name = textBox1.Text;
            listBox1.SelectedItem = textBox1.Text;
        }

        private void Btaddlayer_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "GIS File (*." + GISConst.SHPFILE + ",*." + GISConst.MYFILE + ")|*."
                + GISConst.SHPFILE + ";*." + GISConst.MYFILE;
            openFileDialog.RestoreDirectory = false;
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            GISLayer layer = Document.Addlayer(openFileDialog.FileName);
            listBox1.Items.Insert(0, layer.Name);
            listBox1.SelectedIndex = 0;
        }

        private void Btdeletelayer_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            Document.RemoveLayer(listBox1.SelectedItem.ToString());
            listBox1.Items.Remove(listBox1.SelectedItem);
            if (listBox1.Items.Count > 0) listBox1.SelectedIndex = 0;
        }

        private void Btmoveup_Click(object sender, EventArgs e)
        {
            //无选择
            if (listBox1.SelectedItem == null) return;
            //当前无法上移
            if (listBox1.SelectedIndex == 0) return;
            //当前图层名
            string selectedname = listBox1.SelectedItem.ToString();
            //需要调换的图层名字
            string uppername = listBox1.Items[listBox1.SelectedIndex - 1].ToString();
            //在listBox1中完成调换
            listBox1.Items[listBox1.SelectedIndex - 1] = selectedname;
            listBox1.Items[listBox1.SelectedIndex] = uppername;
            //在document中完成调换
            Document.SwitchLayer(selectedname, uppername);
            listBox1.SelectedIndex--;
        }

        private void Btmovedown_Click(object sender, EventArgs e)
        {
            //无选择
            if (listBox1.SelectedItem == null) return;
            //当前无法下移
            if (listBox1.Items.Count == 1) return;
            if (listBox1.SelectedIndex == listBox1.Items.Count-1) return;
            //当前图层名
            string selectedname = listBox1.SelectedItem.ToString();
            //需要调换的图层名字
            string lowername = listBox1.Items[listBox1.SelectedIndex + 1].ToString();
            //在listBox1中完成调换
            listBox1.Items[listBox1.SelectedIndex + 1] = selectedname;
            listBox1.Items[listBox1.SelectedIndex] = lowername;
            //在document中完成调换
            Document.SwitchLayer(selectedname, lowername);
            listBox1.SelectedIndex++;
        }

        private void Btexportlayer_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            SaveFileDialog savefiledialog1 = new SaveFileDialog();
            savefiledialog1.Filter = "GIS file (*." + GISConst.MYFILE + ")|*." + GISConst.MYFILE;
            savefiledialog1.FilterIndex = 1;
            savefiledialog1.RestoreDirectory = false;

            if (savefiledialog1.ShowDialog() == DialogResult.OK)
            {
                GISLayer layer = Document.getLayer(listBox1.SelectedItem.ToString());
                GISMyFile.WriteFile(layer, savefiledialog1.FileName);
                MessageBox.Show("Done! " + savefiledialog1.FileName + " saved!");
            }
        }

        private void Btsavedocument_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefiledialog1 = new SaveFileDialog();
            savefiledialog1.Filter = "GIS Document (*." + GISConst.MYDOC + ")|*." + GISConst.MYDOC;
            savefiledialog1.FilterIndex = 1;
            savefiledialog1.RestoreDirectory = false;
            if (savefiledialog1.ShowDialog() == DialogResult.OK)
            {
                Document.Write(savefiledialog1.FileName);
                MessageBox.Show("Done!! jkd saved");
            }
        }

        private void Btopenattribute_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            GISLayer layer = Document.getLayer(listBox1.SelectedItem.ToString());
            Mapwindow.OpenAttributeWindow(layer);
        }

        private void Btapply_Click(object sender, EventArgs e)
        {
            Mapwindow.UpdateMap();
        }

        private void Btdispose_Click(object sender, EventArgs e)
        {
            Mapwindow.UpdateMap();
            Close();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //唯一值地图
            if (comboBox2.SelectedIndex == 0)
            {
                comboBox3.Visible = false;
                tblevelnumber.Visible = false;
                btfillcolor.Visible = true;
                tbdrawsize.Visible = true;
                btboundcolor.Visible = true;

            }
            //唯一值地图
            else if (comboBox2.SelectedIndex == 1)
            {
                comboBox3.Visible = true;
                tblevelnumber.Visible = false;
                btfillcolor.Visible = false;
                tbdrawsize.Visible = false;
                btboundcolor.Visible = false;
            }
            //分层设色地图
            else if (comboBox2.SelectedIndex == 2)
            {
                comboBox3.Visible = true;
                tblevelnumber.Visible = true;
                btfillcolor.Visible = false;
                tbdrawsize.Visible = false;
                btboundcolor.Visible = false;
            }
        }

        private void Btchangethematic_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            GISLayer layer = Document.getLayer(listBox1.SelectedItem.ToString());
            //唯一值地图
            if (comboBox2.SelectedIndex == 0)
            {
                layer.MakeUnifiedValueMap();
                GISThematic Thematic = layer.Thematics[layer.ThematicType];
                Thematic.InsideColor = btfillcolor.BackColor;
                Thematic.OutsideColor = btboundcolor.BackColor;
                Thematic.Size = (tbdrawsize.Text == "") ? Thematic.Size : Int32.Parse(tbdrawsize.Text);
            }
            //独立值地图
            else if (comboBox2.SelectedIndex == 1)
            {
                layer.MakeUniqueValueMap(comboBox3.SelectedIndex);
            }
            //分层设色地图
            else if (comboBox2.SelectedIndex == 2)
            {
                if (layer.MakeGradualColor(comboBox3.SelectedIndex, Int32.Parse(tblevelnumber.Text)) == false)
                {
                    MessageBox.Show("基于该属性无法绘制分层设色地图!!");
                    return;
                }
            }
            //更新地图绘制
            Mapwindow.UpdateMap();
        }

        private void LayerDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Mapwindow.UpdateMap();
        }
    }
}
