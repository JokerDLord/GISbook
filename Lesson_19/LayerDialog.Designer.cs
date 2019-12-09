namespace MYGIS
{
    partial class LayerDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.btmoveup = new System.Windows.Forms.Button();
            this.btmovedown = new System.Windows.Forms.Button();
            this.btopenattribute = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btaddlayer = new System.Windows.Forms.Button();
            this.btdeletelayer = new System.Windows.Forms.Button();
            this.btexportlayer = new System.Windows.Forms.Button();
            this.btsavedocument = new System.Windows.Forms.Button();
            this.fileaddr = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bteditname = new System.Windows.Forms.Button();
            this.btapply = new System.Windows.Forms.Button();
            this.btdispose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btchangethematic = new System.Windows.Forms.Button();
            this.tblevelnumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbdrawsize = new System.Windows.Forms.TextBox();
            this.btboundcolor = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btfillcolor = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(32, 18);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(484, 220);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(58, 285);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(60, 16);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "可选择";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.Clicked);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(58, 326);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "可视";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Click += new System.EventHandler(this.Clicked);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(58, 367);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 16);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "自动标注";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.Click += new System.EventHandler(this.Clicked);
            // 
            // btmoveup
            // 
            this.btmoveup.Location = new System.Drawing.Point(574, 82);
            this.btmoveup.Name = "btmoveup";
            this.btmoveup.Size = new System.Drawing.Size(75, 23);
            this.btmoveup.TabIndex = 4;
            this.btmoveup.Text = "上移";
            this.btmoveup.UseVisualStyleBackColor = true;
            this.btmoveup.Click += new System.EventHandler(this.Btmoveup_Click);
            // 
            // btmovedown
            // 
            this.btmovedown.Location = new System.Drawing.Point(574, 141);
            this.btmovedown.Name = "btmovedown";
            this.btmovedown.Size = new System.Drawing.Size(75, 23);
            this.btmovedown.TabIndex = 5;
            this.btmovedown.Text = "下移";
            this.btmovedown.UseVisualStyleBackColor = true;
            this.btmovedown.Click += new System.EventHandler(this.Btmovedown_Click);
            // 
            // btopenattribute
            // 
            this.btopenattribute.Location = new System.Drawing.Point(574, 215);
            this.btopenattribute.Name = "btopenattribute";
            this.btopenattribute.Size = new System.Drawing.Size(75, 23);
            this.btopenattribute.TabIndex = 6;
            this.btopenattribute.Text = "打开属性表";
            this.btopenattribute.UseVisualStyleBackColor = true;
            this.btopenattribute.Click += new System.EventHandler(this.Btopenattribute_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(136, 365);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(153, 20);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.Click += new System.EventHandler(this.Clicked);
            // 
            // btaddlayer
            // 
            this.btaddlayer.Location = new System.Drawing.Point(198, 269);
            this.btaddlayer.Name = "btaddlayer";
            this.btaddlayer.Size = new System.Drawing.Size(75, 23);
            this.btaddlayer.TabIndex = 8;
            this.btaddlayer.Text = "添加图层";
            this.btaddlayer.UseVisualStyleBackColor = true;
            this.btaddlayer.Click += new System.EventHandler(this.Btaddlayer_Click);
            // 
            // btdeletelayer
            // 
            this.btdeletelayer.Location = new System.Drawing.Point(279, 269);
            this.btdeletelayer.Name = "btdeletelayer";
            this.btdeletelayer.Size = new System.Drawing.Size(75, 23);
            this.btdeletelayer.TabIndex = 9;
            this.btdeletelayer.Text = "删除图层";
            this.btdeletelayer.UseVisualStyleBackColor = true;
            this.btdeletelayer.Click += new System.EventHandler(this.Btdeletelayer_Click);
            // 
            // btexportlayer
            // 
            this.btexportlayer.Location = new System.Drawing.Point(360, 269);
            this.btexportlayer.Name = "btexportlayer";
            this.btexportlayer.Size = new System.Drawing.Size(75, 23);
            this.btexportlayer.TabIndex = 10;
            this.btexportlayer.Text = "导出图层";
            this.btexportlayer.UseVisualStyleBackColor = true;
            this.btexportlayer.Click += new System.EventHandler(this.Btexportlayer_Click);
            // 
            // btsavedocument
            // 
            this.btsavedocument.Location = new System.Drawing.Point(441, 269);
            this.btsavedocument.Name = "btsavedocument";
            this.btsavedocument.Size = new System.Drawing.Size(75, 23);
            this.btsavedocument.TabIndex = 11;
            this.btsavedocument.Text = "储存文档";
            this.btsavedocument.UseVisualStyleBackColor = true;
            this.btsavedocument.Click += new System.EventHandler(this.Btsavedocument_Click);
            // 
            // fileaddr
            // 
            this.fileaddr.AutoSize = true;
            this.fileaddr.Location = new System.Drawing.Point(346, 316);
            this.fileaddr.Name = "fileaddr";
            this.fileaddr.Size = new System.Drawing.Size(65, 12);
            this.fileaddr.TabIndex = 12;
            this.fileaddr.Text = "文件地址：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(346, 347);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "图层名称：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(416, 343);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(146, 21);
            this.textBox1.TabIndex = 14;
            // 
            // bteditname
            // 
            this.bteditname.Location = new System.Drawing.Point(603, 342);
            this.bteditname.Name = "bteditname";
            this.bteditname.Size = new System.Drawing.Size(75, 23);
            this.bteditname.TabIndex = 15;
            this.bteditname.Text = "修改";
            this.bteditname.UseVisualStyleBackColor = true;
            this.bteditname.Click += new System.EventHandler(this.Bteditname_Click);
            // 
            // btapply
            // 
            this.btapply.Location = new System.Drawing.Point(487, 398);
            this.btapply.Name = "btapply";
            this.btapply.Size = new System.Drawing.Size(75, 23);
            this.btapply.TabIndex = 16;
            this.btapply.Text = "应用";
            this.btapply.UseVisualStyleBackColor = true;
            this.btapply.Click += new System.EventHandler(this.Btapply_Click);
            // 
            // btdispose
            // 
            this.btdispose.Location = new System.Drawing.Point(603, 398);
            this.btdispose.Name = "btdispose";
            this.btdispose.Size = new System.Drawing.Size(75, 23);
            this.btdispose.TabIndex = 17;
            this.btdispose.Text = "关闭";
            this.btdispose.UseVisualStyleBackColor = true;
            this.btdispose.Click += new System.EventHandler(this.Btdispose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btchangethematic);
            this.groupBox1.Controls.Add(this.tblevelnumber);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.comboBox3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbdrawsize);
            this.groupBox1.Controls.Add(this.btboundcolor);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btfillcolor);
            this.groupBox1.Location = new System.Drawing.Point(698, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 403);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "显示设置";
            // 
            // btchangethematic
            // 
            this.btchangethematic.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btchangethematic.Location = new System.Drawing.Point(99, 323);
            this.btchangethematic.Name = "btchangethematic";
            this.btchangethematic.Size = new System.Drawing.Size(124, 31);
            this.btchangethematic.TabIndex = 24;
            this.btchangethematic.Text = "修改专题地图类型";
            this.btchangethematic.UseVisualStyleBackColor = true;
            this.btchangethematic.Click += new System.EventHandler(this.Btchangethematic_Click);
            // 
            // tblevelnumber
            // 
            this.tblevelnumber.Location = new System.Drawing.Point(99, 251);
            this.tblevelnumber.Name = "tblevelnumber";
            this.tblevelnumber.Size = new System.Drawing.Size(124, 21);
            this.tblevelnumber.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 256);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "分级数量";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(99, 205);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(124, 20);
            this.comboBox3.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "专题属性";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "唯一值地图",
            "独立值地图",
            "分级设色地图"});
            this.comboBox2.Location = new System.Drawing.Point(111, 157);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(112, 20);
            this.comboBox2.TabIndex = 19;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.ComboBox2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "专题地图类型";
            // 
            // tbdrawsize
            // 
            this.tbdrawsize.Location = new System.Drawing.Point(99, 61);
            this.tbdrawsize.Name = "tbdrawsize";
            this.tbdrawsize.Size = new System.Drawing.Size(124, 21);
            this.tbdrawsize.TabIndex = 5;
            this.tbdrawsize.Text = "2";
            this.tbdrawsize.TextChanged += new System.EventHandler(this.Clicked);
            // 
            // btboundcolor
            // 
            this.btboundcolor.Location = new System.Drawing.Point(99, 102);
            this.btboundcolor.Name = "btboundcolor";
            this.btboundcolor.Size = new System.Drawing.Size(124, 21);
            this.btboundcolor.TabIndex = 4;
            this.btboundcolor.UseVisualStyleBackColor = true;
            this.btboundcolor.Click += new System.EventHandler(this.SettingColor_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "边界颜色";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "控制尺寸";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "填充颜色";
            // 
            // btfillcolor
            // 
            this.btfillcolor.Location = new System.Drawing.Point(99, 20);
            this.btfillcolor.Name = "btfillcolor";
            this.btfillcolor.Size = new System.Drawing.Size(124, 21);
            this.btfillcolor.TabIndex = 0;
            this.btfillcolor.UseVisualStyleBackColor = true;
            this.btfillcolor.Click += new System.EventHandler(this.SettingColor_Click);
            // 
            // LayerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btdispose);
            this.Controls.Add(this.btapply);
            this.Controls.Add(this.bteditname);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileaddr);
            this.Controls.Add(this.btsavedocument);
            this.Controls.Add(this.btexportlayer);
            this.Controls.Add(this.btdeletelayer);
            this.Controls.Add(this.btaddlayer);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btopenattribute);
            this.Controls.Add(this.btmovedown);
            this.Controls.Add(this.btmoveup);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.listBox1);
            this.Name = "LayerDialog";
            this.Text = "LayerDialog";
            this.Shown += new System.EventHandler(this.Form3_Shown);
            this.Click += new System.EventHandler(this.Form3_Click);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button btmoveup;
        private System.Windows.Forms.Button btmovedown;
        private System.Windows.Forms.Button btopenattribute;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btaddlayer;
        private System.Windows.Forms.Button btdeletelayer;
        private System.Windows.Forms.Button btexportlayer;
        private System.Windows.Forms.Button btsavedocument;
        private System.Windows.Forms.Label fileaddr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button bteditname;
        private System.Windows.Forms.Button btapply;
        private System.Windows.Forms.Button btdispose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbdrawsize;
        private System.Windows.Forms.Button btboundcolor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btfillcolor;
        private System.Windows.Forms.TextBox tblevelnumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btchangethematic;
    }
}