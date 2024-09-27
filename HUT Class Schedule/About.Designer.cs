namespace HUT_Class_Schedule
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label1.Location = new Point(91, 29);
            label1.Name = "label1";
            label1.Size = new Size(278, 36);
            label1.TabIndex = 0;
            label1.Text = "HUT Class Schedule";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(18, 196);
            label2.Name = "label2";
            label2.Size = new Size(424, 90);
            label2.TabIndex = 1;
            label2.Text = resources.GetString("label2.Text");
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 310);
            label3.Name = "label3";
            label3.Size = new Size(428, 60);
            label3.TabIndex = 2;
            label3.Text = "HUT Class Schedule 作为一个开源软件为HUT学生提供使用。\r\n对于任何情况下使用本程序造成的任何索赔、损害赔偿或其他\r\n情况作者不承担责任。";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label4.Location = new Point(26, 111);
            label4.Name = "label4";
            label4.Size = new Size(196, 45);
            label4.TabIndex = 3;
            label4.Text = "作者：GoodBoyboy\r\n\r\n主页：www.goodboyboy.top";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label5.Location = new Point(240, 111);
            label5.Name = "label5";
            label5.Size = new Size(198, 45);
            label5.TabIndex = 4;
            label5.Text = "GitHub：GoodBoyboy666\r\n\r\nEmail：me@goodboyboy.top";
            // 
            // label6
            // 
            label6.BorderStyle = BorderStyle.FixedSingle;
            label6.Location = new Point(18, 177);
            label6.Name = "label6";
            label6.Size = new Size(420, 2);
            label6.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(240, 427);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 6;
            button1.Text = "项目仓库";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(18, 427);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 7;
            button2.Text = "作者主页";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(128, 427);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 8;
            button3.Text = "作者博客";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(348, 427);
            button4.Name = "button4";
            button4.Size = new Size(94, 29);
            button4.TabIndex = 9;
            button4.Text = "开源协议";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(465, 481);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "About";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "关于";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}