namespace LogReader
{
    partial class Form1
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
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Failed Cases");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Passed Cases");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Case", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Script Errors");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Log", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView = new System.Windows.Forms.TreeView();
            this.test_performance = new System.Windows.Forms.GroupBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.failed = new System.Windows.Forms.TextBox();
            this.passed = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.test_id = new System.Windows.Forms.TextBox();
            this.test_owner = new System.Windows.Forms.TextBox();
            this.case_summary = new System.Windows.Forms.GroupBox();
            this.Description = new System.Windows.Forms.RichTextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Create_Suite = new System.Windows.Forms.Button();
            this.test_performance.SuspendLayout();
            this.case_summary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView.Location = new System.Drawing.Point(6, 208);
            this.treeView.Name = "treeView";
            treeNode6.Name = "failed_case";
            treeNode6.Text = "Failed Cases";
            treeNode6.ToolTipText = "Failed Cases";
            treeNode7.Name = "passed_case";
            treeNode7.Text = "Passed Cases";
            treeNode7.ToolTipText = "Passed Cases";
            treeNode8.Name = "case";
            treeNode8.Text = "Case";
            treeNode8.ToolTipText = "Case";
            treeNode9.Name = "script_error";
            treeNode9.Text = "Script Errors";
            treeNode9.ToolTipText = "Script Errors";
            treeNode10.Name = "Log";
            treeNode10.Text = "Log";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode10});
            this.treeView.Size = new System.Drawing.Size(160, 443);
            this.treeView.TabIndex = 1;
            this.treeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.node_AfterCheck);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // test_performance
            // 
            this.test_performance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.test_performance.Controls.Add(this.textBox6);
            this.test_performance.Controls.Add(this.textBox5);
            this.test_performance.Controls.Add(this.failed);
            this.test_performance.Controls.Add(this.passed);
            this.test_performance.Location = new System.Drawing.Point(19, 594);
            this.test_performance.Name = "test_performance";
            this.test_performance.Size = new System.Drawing.Size(288, 46);
            this.test_performance.TabIndex = 3;
            this.test_performance.TabStop = false;
            this.test_performance.Text = "Test Performance";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.Control;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Location = new System.Drawing.Point(153, 22);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(33, 13);
            this.textBox6.TabIndex = 12;
            this.textBox6.Text = "  Failed";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.Control;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Location = new System.Drawing.Point(2, 22);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(47, 13);
            this.textBox5.TabIndex = 11;
            this.textBox5.Text = "  Passed";
            // 
            // failed
            // 
            this.failed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.failed.BackColor = System.Drawing.SystemColors.Window;
            this.failed.Location = new System.Drawing.Point(192, 16);
            this.failed.Name = "failed";
            this.failed.ReadOnly = true;
            this.failed.Size = new System.Drawing.Size(86, 20);
            this.failed.TabIndex = 1;
            // 
            // passed
            // 
            this.passed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.passed.BackColor = System.Drawing.SystemColors.Window;
            this.passed.Location = new System.Drawing.Point(55, 16);
            this.passed.Name = "passed";
            this.passed.ReadOnly = true;
            this.passed.Size = new System.Drawing.Size(86, 20);
            this.passed.TabIndex = 0;
            // 
            // name
            // 
            this.name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.name.BackColor = System.Drawing.SystemColors.Window;
            this.name.Location = new System.Drawing.Point(46, 19);
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Size = new System.Drawing.Size(251, 20);
            this.name.TabIndex = 4;
            // 
            // test_id
            // 
            this.test_id.BackColor = System.Drawing.SystemColors.Window;
            this.test_id.Location = new System.Drawing.Point(29, 51);
            this.test_id.Multiline = true;
            this.test_id.Name = "test_id";
            this.test_id.ReadOnly = true;
            this.test_id.Size = new System.Drawing.Size(133, 20);
            this.test_id.TabIndex = 5;
            this.test_id.Text = "\r\n\r\n";
            // 
            // test_owner
            // 
            this.test_owner.BackColor = System.Drawing.SystemColors.Window;
            this.test_owner.Location = new System.Drawing.Point(211, 51);
            this.test_owner.Name = "test_owner";
            this.test_owner.ReadOnly = true;
            this.test_owner.Size = new System.Drawing.Size(86, 20);
            this.test_owner.TabIndex = 6;
            // 
            // case_summary
            // 
            this.case_summary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.case_summary.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.case_summary.Controls.Add(this.Description);
            this.case_summary.Controls.Add(this.textBox3);
            this.case_summary.Controls.Add(this.textBox2);
            this.case_summary.Controls.Add(this.test_performance);
            this.case_summary.Controls.Add(this.textBox1);
            this.case_summary.Controls.Add(this.test_id);
            this.case_summary.Controls.Add(this.name);
            this.case_summary.Controls.Add(this.test_owner);
            this.case_summary.Location = new System.Drawing.Point(172, 12);
            this.case_summary.Name = "case_summary";
            this.case_summary.Size = new System.Drawing.Size(315, 646);
            this.case_summary.TabIndex = 7;
            this.case_summary.TabStop = false;
            this.case_summary.Text = "Case Summary";
            // 
            // Description
            // 
            this.Description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Description.AutoWordSelection = true;
            this.Description.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Description.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Description.EnableAutoDragDrop = true;
            this.Description.Location = new System.Drawing.Point(17, 77);
            this.Description.MaximumSize = new System.Drawing.Size(1400, 1400);
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Size = new System.Drawing.Size(280, 511);
            this.Description.TabIndex = 11;
            this.Description.Text = "";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Control;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(168, 54);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(37, 13);
            this.textBox3.TabIndex = 9;
            this.textBox3.Text = "  Owner";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Control;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(6, 22);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(34, 13);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "Name";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(6, 54);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(17, 13);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "ID";
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 66);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 126);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // Create_Suite
            // 
            this.Create_Suite.AllowDrop = true;
            this.Create_Suite.BackColor = System.Drawing.Color.LightGray;
            this.Create_Suite.Enabled = false;
            this.Create_Suite.FlatAppearance.BorderSize = 0;
            this.Create_Suite.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Create_Suite.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.Create_Suite.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Create_Suite.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Create_Suite.ForeColor = System.Drawing.Color.Black;
            this.Create_Suite.Location = new System.Drawing.Point(6, 25);
            this.Create_Suite.Name = "Create_Suite";
            this.Create_Suite.Size = new System.Drawing.Size(160, 28);
            this.Create_Suite.TabIndex = 12;
            this.Create_Suite.Text = "Drop Orignal SUITE";
            this.Create_Suite.UseVisualStyleBackColor = false;
            this.Create_Suite.DragDrop += new System.Windows.Forms.DragEventHandler(this.createSuite_DragDrop);
            this.Create_Suite.DragEnter += new System.Windows.Forms.DragEventHandler(this.createSuite_DragEnter);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(507, 681);
            this.Controls.Add(this.Create_Suite);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.case_summary);
            this.Controls.Add(this.treeView);
            this.MinimumSize = new System.Drawing.Size(515, 708);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.form_DragEnter);
            this.test_performance.ResumeLayout(false);
            this.test_performance.PerformLayout();
            this.case_summary.ResumeLayout(false);
            this.case_summary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.GroupBox test_performance;
        private System.Windows.Forms.TextBox failed;
        private System.Windows.Forms.TextBox passed;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox test_id;
        private System.Windows.Forms.TextBox test_owner;
        private System.Windows.Forms.GroupBox case_summary;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox Description;
        protected System.Windows.Forms.Button Create_Suite;
    }
}

