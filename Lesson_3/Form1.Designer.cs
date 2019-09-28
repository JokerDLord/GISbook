namespace Lesson_3
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbX = new System.Windows.Forms.TextBox();
            this.tbY = new System.Windows.Forms.TextBox();
            this.tbAttribute = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbMinX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbMinY = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbMaxX = new System.Windows.Forms.TextBox();
            this.tbMaxY = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 0;
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(239, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Y";
            this.label3.Click += new System.EventHandler(this.Label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(395, 41);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "属性";
            // 
            // tbX
            // 
            this.tbX.Location = new System.Drawing.Point(100, 38);
            this.tbX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbX.Name = "tbX";
            this.tbX.Size = new System.Drawing.Size(135, 21);
            this.tbX.TabIndex = 4;
            this.tbX.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // tbY
            // 
            this.tbY.Location = new System.Drawing.Point(255, 38);
            this.tbY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbY.Name = "tbY";
            this.tbY.Size = new System.Drawing.Size(137, 21);
            this.tbY.TabIndex = 5;
            // 
            // tbAttribute
            // 
            this.tbAttribute.Location = new System.Drawing.Point(428, 38);
            this.tbAttribute.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbAttribute.Name = "tbAttribute";
            this.tbAttribute.Size = new System.Drawing.Size(224, 21);
            this.tbAttribute.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(655, 38);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button1.Size = new System.Drawing.Size(88, 20);
            this.button1.TabIndex = 7;
            this.button1.Text = "添加点实体";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 78);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "MinX";
            // 
            // tbMinX
            // 
            this.tbMinX.Location = new System.Drawing.Point(66, 72);
            this.tbMinX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbMinX.Name = "tbMinX";
            this.tbMinX.Size = new System.Drawing.Size(135, 21);
            this.tbMinX.TabIndex = 9;
            this.tbMinX.TextChanged += new System.EventHandler(this.TextBox4_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(205, 78);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "MinY";
            // 
            // tbMinY
            // 
            this.tbMinY.Location = new System.Drawing.Point(238, 72);
            this.tbMinY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbMinY.Name = "tbMinY";
            this.tbMinY.Size = new System.Drawing.Size(134, 21);
            this.tbMinY.TabIndex = 11;
            this.tbMinY.TextChanged += new System.EventHandler(this.TextBox5_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(377, 77);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "MaxX";
            // 
            // tbMaxX
            // 
            this.tbMaxX.Location = new System.Drawing.Point(411, 72);
            this.tbMaxX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbMaxX.Name = "tbMaxX";
            this.tbMaxX.Size = new System.Drawing.Size(136, 21);
            this.tbMaxX.TabIndex = 13;
            this.tbMaxX.TextChanged += new System.EventHandler(this.TextBox6_TextChanged);
            // 
            // tbMaxY
            // 
            this.tbMaxY.Location = new System.Drawing.Point(586, 72);
            this.tbMaxY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbMaxY.Name = "tbMaxY";
            this.tbMaxY.Size = new System.Drawing.Size(136, 21);
            this.tbMaxY.TabIndex = 15;
            this.tbMaxY.TextChanged += new System.EventHandler(this.TextBox7_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(553, 77);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "MaxY";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(740, 72);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 20);
            this.button2.TabIndex = 16;
            this.button2.Text = "更新地图";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 509);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbMaxY);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbMaxX);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbMinY);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbMinX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbAttribute);
            this.Controls.Add(this.tbY);
            this.Controls.Add(this.tbX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbX;
        private System.Windows.Forms.TextBox tbY;
        private System.Windows.Forms.TextBox tbAttribute;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbMinX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbMinY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbMaxX;
        private System.Windows.Forms.TextBox tbMaxY;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button2;
    }
}

