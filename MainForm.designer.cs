namespace LogReader
{
    partial class MainForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Failed Cases");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Passed Cases");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Case", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Script Errors");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Log", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            this.testcaseTreeView = new System.Windows.Forms.TreeView();
            this.createSuiteButton = new System.Windows.Forms.Button();
            this.testcaseRichTextBox = new System.Windows.Forms.RichTextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.testcaseFailTextBox = new System.Windows.Forms.TextBox();
            this.testcasePassTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // testcaseTreeView
            // 
            this.testcaseTreeView.AllowDrop = true;
            this.testcaseTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.testcaseTreeView.CheckBoxes = true;
            this.testcaseTreeView.HideSelection = false;
            this.testcaseTreeView.Location = new System.Drawing.Point(12, 72);
            this.testcaseTreeView.Name = "testcaseTreeView";
            treeNode1.Name = "failed_case";
            treeNode1.Text = "Failed Cases";
            treeNode1.ToolTipText = "Failed Cases";
            treeNode2.Name = "passed_case";
            treeNode2.Text = "Passed Cases";
            treeNode2.ToolTipText = "Passed Cases";
            treeNode3.Name = "case";
            treeNode3.Text = "Case";
            treeNode3.ToolTipText = "Case";
            treeNode4.Name = "script_error";
            treeNode4.Text = "Script Errors";
            treeNode4.ToolTipText = "Script Errors";
            treeNode5.Name = "Log";
            treeNode5.Text = "Log";
            this.testcaseTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this.testcaseTreeView.Size = new System.Drawing.Size(160, 407);
            this.testcaseTreeView.TabIndex = 0;
            this.testcaseTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.testcaseTreeView_AfterCheck);
            this.testcaseTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.testcaseTreeView_AfterSelect);
            this.testcaseTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.testcaseTreeView_DragDrop);
            this.testcaseTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.testcaseTreeView_DragEnter);
            this.testcaseTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.testcaseTreeView_KeyDown);
            // 
            // createSuiteButton
            // 
            this.createSuiteButton.AllowDrop = true;
            this.createSuiteButton.BackColor = System.Drawing.Color.LightGray;
            this.createSuiteButton.Enabled = false;
            this.createSuiteButton.FlatAppearance.BorderSize = 0;
            this.createSuiteButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.createSuiteButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.createSuiteButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.createSuiteButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createSuiteButton.ForeColor = System.Drawing.Color.Black;
            this.createSuiteButton.Location = new System.Drawing.Point(12, 12);
            this.createSuiteButton.Name = "createSuiteButton";
            this.createSuiteButton.Size = new System.Drawing.Size(160, 28);
            this.createSuiteButton.TabIndex = 12;
            this.createSuiteButton.Text = "Drop Suite file";
            this.createSuiteButton.UseVisualStyleBackColor = false;
            this.createSuiteButton.Click += new System.EventHandler(this.createSuiteButton_Click);
            this.createSuiteButton.DragDrop += new System.Windows.Forms.DragEventHandler(this.createSuiteButton_DragDrop);
            this.createSuiteButton.DragEnter += new System.Windows.Forms.DragEventHandler(this.createSuiteButton_DragEnter);
            // 
            // testcaseRichTextBox
            // 
            this.testcaseRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.testcaseRichTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.testcaseRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.testcaseRichTextBox.Location = new System.Drawing.Point(178, 12);
            this.testcaseRichTextBox.Name = "testcaseRichTextBox";
            this.testcaseRichTextBox.ReadOnly = true;
            this.testcaseRichTextBox.Size = new System.Drawing.Size(337, 467);
            this.testcaseRichTextBox.TabIndex = 0;
            this.testcaseRichTextBox.Text = "";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.Control;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Location = new System.Drawing.Point(92, 49);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(24, 13);
            this.textBox6.TabIndex = 18;
            this.textBox6.Text = "  Fail";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.Control;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Location = new System.Drawing.Point(12, 49);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(27, 13);
            this.textBox5.TabIndex = 17;
            this.textBox5.Text = "Pass";
            // 
            // testcaseFailTextBox
            // 
            this.testcaseFailTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.testcaseFailTextBox.Location = new System.Drawing.Point(122, 46);
            this.testcaseFailTextBox.Name = "testcaseFailTextBox";
            this.testcaseFailTextBox.ReadOnly = true;
            this.testcaseFailTextBox.Size = new System.Drawing.Size(41, 20);
            this.testcaseFailTextBox.TabIndex = 0;
            // 
            // testcasePassTextBox
            // 
            this.testcasePassTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.testcasePassTextBox.Location = new System.Drawing.Point(45, 46);
            this.testcasePassTextBox.Name = "testcasePassTextBox";
            this.testcasePassTextBox.ReadOnly = true;
            this.testcasePassTextBox.Size = new System.Drawing.Size(41, 20);
            this.testcasePassTextBox.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(527, 491);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.testcaseFailTextBox);
            this.Controls.Add(this.testcasePassTextBox);
            this.Controls.Add(this.testcaseRichTextBox);
            this.Controls.Add(this.createSuiteButton);
            this.Controls.Add(this.testcaseTreeView);
            this.Name = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView testcaseTreeView;
        protected System.Windows.Forms.Button createSuiteButton;
        private System.Windows.Forms.RichTextBox testcaseRichTextBox;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox testcaseFailTextBox;
        private System.Windows.Forms.TextBox testcasePassTextBox;
    }
}

