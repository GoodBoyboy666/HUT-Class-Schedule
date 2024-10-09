namespace HUT_Class_Schedule
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Schedule = new DataGridView();
            head = new DataGridViewTextBoxColumn();
            Monday = new DataGridViewTextBoxColumn();
            Tuesday = new DataGridViewTextBoxColumn();
            Wednesday = new DataGridViewTextBoxColumn();
            Thursday = new DataGridViewTextBoxColumn();
            Friday = new DataGridViewTextBoxColumn();
            Saturday = new DataGridViewTextBoxColumn();
            Sunday = new DataGridViewTextBoxColumn();
            More_Info = new RichTextBox();
            textBox_Account = new TextBox();
            textBox_Password = new TextBox();
            label1 = new Label();
            label2 = new Label();
            Get_Schedule = new Button();
            label3 = new Label();
            Show_Passwd = new Button();
            menuStrip1 = new MenuStrip();
            主页ToolStripMenuItem = new ToolStripMenuItem();
            关于ToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            statusBar = new ToolStripProgressBar();
            statusLabel = new ToolStripStatusLabel();
            label4 = new Label();
            ZhouCicomboBox = new ComboBox();
            logout = new Button();
            ((System.ComponentModel.ISupportInitialize)Schedule).BeginInit();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // Schedule
            // 
            Schedule.AllowUserToAddRows = false;
            Schedule.AllowUserToDeleteRows = false;
            Schedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Schedule.Columns.AddRange(new DataGridViewColumn[] { head, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday });
            Schedule.Location = new Point(2, 92);
            Schedule.MultiSelect = false;
            Schedule.Name = "Schedule";
            Schedule.ReadOnly = true;
            Schedule.RowHeadersVisible = false;
            Schedule.RowHeadersWidth = 51;
            Schedule.RowTemplate.Height = 60;
            Schedule.RowTemplate.Resizable = DataGridViewTriState.False;
            Schedule.Size = new Size(1011, 469);
            Schedule.TabIndex = 0;
            Schedule.SelectionChanged += Schedule_SelectionChanged;
            // 
            // head
            // 
            head.HeaderText = "周/节次";
            head.MinimumWidth = 6;
            head.Name = "head";
            head.ReadOnly = true;
            head.Resizable = DataGridViewTriState.False;
            head.SortMode = DataGridViewColumnSortMode.NotSortable;
            head.Width = 125;
            // 
            // Monday
            // 
            Monday.HeaderText = "星期一";
            Monday.MinimumWidth = 6;
            Monday.Name = "Monday";
            Monday.ReadOnly = true;
            Monday.Resizable = DataGridViewTriState.True;
            Monday.SortMode = DataGridViewColumnSortMode.NotSortable;
            Monday.Width = 125;
            // 
            // Tuesday
            // 
            Tuesday.HeaderText = "星期二";
            Tuesday.MinimumWidth = 6;
            Tuesday.Name = "Tuesday";
            Tuesday.ReadOnly = true;
            Tuesday.Resizable = DataGridViewTriState.True;
            Tuesday.SortMode = DataGridViewColumnSortMode.NotSortable;
            Tuesday.Width = 125;
            // 
            // Wednesday
            // 
            Wednesday.HeaderText = "星期三";
            Wednesday.MinimumWidth = 6;
            Wednesday.Name = "Wednesday";
            Wednesday.ReadOnly = true;
            Wednesday.Resizable = DataGridViewTriState.True;
            Wednesday.SortMode = DataGridViewColumnSortMode.NotSortable;
            Wednesday.Width = 125;
            // 
            // Thursday
            // 
            Thursday.HeaderText = "星期四";
            Thursday.MinimumWidth = 6;
            Thursday.Name = "Thursday";
            Thursday.ReadOnly = true;
            Thursday.Resizable = DataGridViewTriState.True;
            Thursday.SortMode = DataGridViewColumnSortMode.NotSortable;
            Thursday.Width = 125;
            // 
            // Friday
            // 
            Friday.HeaderText = "星期五";
            Friday.MinimumWidth = 6;
            Friday.Name = "Friday";
            Friday.ReadOnly = true;
            Friday.Resizable = DataGridViewTriState.True;
            Friday.SortMode = DataGridViewColumnSortMode.NotSortable;
            Friday.Width = 125;
            // 
            // Saturday
            // 
            Saturday.HeaderText = "星期六";
            Saturday.MinimumWidth = 6;
            Saturday.Name = "Saturday";
            Saturday.ReadOnly = true;
            Saturday.Resizable = DataGridViewTriState.True;
            Saturday.SortMode = DataGridViewColumnSortMode.NotSortable;
            Saturday.Width = 125;
            // 
            // Sunday
            // 
            Sunday.HeaderText = "星期天";
            Sunday.MinimumWidth = 6;
            Sunday.Name = "Sunday";
            Sunday.ReadOnly = true;
            Sunday.Resizable = DataGridViewTriState.True;
            Sunday.SortMode = DataGridViewColumnSortMode.NotSortable;
            Sunday.Width = 125;
            // 
            // More_Info
            // 
            More_Info.Location = new Point(1019, 135);
            More_Info.Name = "More_Info";
            More_Info.ReadOnly = true;
            More_Info.Size = new Size(192, 426);
            More_Info.TabIndex = 1;
            More_Info.Text = "";
            // 
            // textBox_Account
            // 
            textBox_Account.Location = new Point(89, 44);
            textBox_Account.Name = "textBox_Account";
            textBox_Account.Size = new Size(164, 27);
            textBox_Account.TabIndex = 2;
            // 
            // textBox_Password
            // 
            textBox_Password.Location = new Point(350, 44);
            textBox_Password.Name = "textBox_Password";
            textBox_Password.PasswordChar = '*';
            textBox_Password.Size = new Size(164, 27);
            textBox_Password.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 47);
            label1.Name = "label1";
            label1.Size = new Size(54, 20);
            label1.TabIndex = 4;
            label1.Text = "学号：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(277, 47);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 5;
            label2.Text = "密码：";
            // 
            // Get_Schedule
            // 
            Get_Schedule.Location = new Point(852, 43);
            Get_Schedule.Name = "Get_Schedule";
            Get_Schedule.Size = new Size(161, 29);
            Get_Schedule.TabIndex = 6;
            Get_Schedule.Text = "获取课表";
            Get_Schedule.UseVisualStyleBackColor = true;
            Get_Schedule.Click += Get_Schedule_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1019, 92);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 7;
            label3.Text = "课程详情：";
            // 
            // Show_Passwd
            // 
            Show_Passwd.Location = new Point(530, 44);
            Show_Passwd.Name = "Show_Passwd";
            Show_Passwd.Size = new Size(94, 29);
            Show_Passwd.TabIndex = 8;
            Show_Passwd.Text = "显示密码";
            Show_Passwd.UseVisualStyleBackColor = true;
            Show_Passwd.MouseDown += Show_Passwd_MouseDown;
            Show_Passwd.MouseUp += Show_Passwd_MouseUp;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 主页ToolStripMenuItem, 关于ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1223, 28);
            menuStrip1.TabIndex = 9;
            menuStrip1.Text = "menuStrip1";
            // 
            // 主页ToolStripMenuItem
            // 
            主页ToolStripMenuItem.Name = "主页ToolStripMenuItem";
            主页ToolStripMenuItem.Size = new Size(53, 24);
            主页ToolStripMenuItem.Text = "主页";
            主页ToolStripMenuItem.Click += 主页ToolStripMenuItem_Click;
            // 
            // 关于ToolStripMenuItem
            // 
            关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            关于ToolStripMenuItem.Size = new Size(53, 24);
            关于ToolStripMenuItem.Text = "关于";
            关于ToolStripMenuItem.Click += 关于ToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusBar, statusLabel });
            statusStrip1.Location = new Point(0, 560);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1223, 26);
            statusStrip1.TabIndex = 10;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusBar
            // 
            statusBar.Name = "statusBar";
            statusBar.Size = new Size(100, 18);
            statusBar.Visible = false;
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(201, 20);
            statusLabel.Text = "点击“获取课表”获取当前课表";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(688, 48);
            label4.Name = "label4";
            label4.Size = new Size(54, 20);
            label4.TabIndex = 11;
            label4.Text = "周次：";
            // 
            // ZhouCicomboBox
            // 
            ZhouCicomboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ZhouCicomboBox.FormattingEnabled = true;
            ZhouCicomboBox.Location = new Point(739, 44);
            ZhouCicomboBox.Name = "ZhouCicomboBox";
            ZhouCicomboBox.Size = new Size(68, 28);
            ZhouCicomboBox.TabIndex = 12;
            // 
            // logout
            // 
            logout.Location = new Point(1037, 44);
            logout.Name = "logout";
            logout.Size = new Size(90, 29);
            logout.TabIndex = 13;
            logout.Text = "退出登录";
            logout.UseVisualStyleBackColor = true;
            logout.Click += logout_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1223, 586);
            Controls.Add(logout);
            Controls.Add(ZhouCicomboBox);
            Controls.Add(label4);
            Controls.Add(statusStrip1);
            Controls.Add(Show_Passwd);
            Controls.Add(label3);
            Controls.Add(Get_Schedule);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox_Password);
            Controls.Add(textBox_Account);
            Controls.Add(More_Info);
            Controls.Add(Schedule);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "MainWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "湖工大课表 v2.0.2";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)Schedule).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView Schedule;
        private RichTextBox More_Info;
        private TextBox textBox_Account;
        private TextBox textBox_Password;
        private Label label1;
        private Label label2;
        private Button Get_Schedule;
        private Label label3;
        private Button Show_Passwd;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 主页ToolStripMenuItem;
        private ToolStripMenuItem 关于ToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar statusBar;
        private ToolStripStatusLabel statusLabel;
        private DataGridViewTextBoxColumn head;
        private DataGridViewTextBoxColumn Monday;
        private DataGridViewTextBoxColumn Tuesday;
        private DataGridViewTextBoxColumn Wednesday;
        private DataGridViewTextBoxColumn Thursday;
        private DataGridViewTextBoxColumn Friday;
        private DataGridViewTextBoxColumn Saturday;
        private DataGridViewTextBoxColumn Sunday;
        private Label label4;
        private ComboBox ZhouCicomboBox;
        private Button logout;
    }
}
