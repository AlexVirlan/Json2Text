﻿namespace Secrets2GitlabVar
{
    partial class frmMain
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
            txtIn = new TextBox();
            txtOut = new TextBox();
            chkAutoConvert = new CheckBox();
            btnConvert = new Button();
            label1 = new Label();
            label2 = new Label();
            lblPaste = new Label();
            lblDemoVal = new Label();
            chkSpaceInES = new CheckBox();
            chkTrimProp = new CheckBox();
            chkTrimVal = new CheckBox();
            cmbES = new ComboBox();
            cmbChildBeh = new ComboBox();
            cmbChildSep = new ComboBox();
            cmbArrayBeh = new ComboBox();
            cmbArrayBrack = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            SuspendLayout();
            // 
            // txtIn
            // 
            txtIn.BackColor = Color.FromArgb(17, 17, 17);
            txtIn.BorderStyle = BorderStyle.FixedSingle;
            txtIn.ForeColor = Color.White;
            txtIn.Location = new Point(12, 25);
            txtIn.Multiline = true;
            txtIn.Name = "txtIn";
            txtIn.ScrollBars = ScrollBars.Both;
            txtIn.Size = new Size(470, 304);
            txtIn.TabIndex = 0;
            txtIn.TextChanged += txtIn_TextChanged;
            // 
            // txtOut
            // 
            txtOut.BackColor = Color.FromArgb(17, 17, 17);
            txtOut.BorderStyle = BorderStyle.FixedSingle;
            txtOut.ForeColor = Color.White;
            txtOut.Location = new Point(488, 25);
            txtOut.Multiline = true;
            txtOut.Name = "txtOut";
            txtOut.ScrollBars = ScrollBars.Both;
            txtOut.Size = new Size(470, 304);
            txtOut.TabIndex = 1;
            // 
            // chkAutoConvert
            // 
            chkAutoConvert.AutoSize = true;
            chkAutoConvert.Location = new Point(863, 357);
            chkAutoConvert.Name = "chkAutoConvert";
            chkAutoConvert.Size = new Size(95, 19);
            chkAutoConvert.TabIndex = 3;
            chkAutoConvert.Text = "Auto convert";
            chkAutoConvert.UseVisualStyleBackColor = true;
            chkAutoConvert.CheckedChanged += chkAutoConvert_CheckedChanged;
            // 
            // btnConvert
            // 
            btnConvert.BackColor = Color.FromArgb(17, 17, 17);
            btnConvert.Cursor = Cursors.Hand;
            btnConvert.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
            btnConvert.FlatAppearance.MouseDownBackColor = Color.FromArgb(117, 117, 117);
            btnConvert.FlatAppearance.MouseOverBackColor = Color.FromArgb(64, 64, 64);
            btnConvert.FlatStyle = FlatStyle.Flat;
            btnConvert.Location = new Point(712, 352);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(145, 29);
            btnConvert.TabIndex = 2;
            btnConvert.Text = "Convert";
            btnConvert.UseVisualStyleBackColor = false;
            btnConvert.Click += btnConvert_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 7);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 3;
            label1.Text = "Input:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(488, 7);
            label2.Name = "label2";
            label2.Size = new Size(48, 15);
            label2.TabIndex = 3;
            label2.Text = "Output:";
            // 
            // lblPaste
            // 
            lblPaste.AutoSize = true;
            lblPaste.Cursor = Cursors.Hand;
            lblPaste.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            lblPaste.Location = new Point(170, 7);
            lblPaste.Name = "lblPaste";
            lblPaste.Size = new Size(35, 15);
            lblPaste.TabIndex = 3;
            lblPaste.Text = "Paste";
            lblPaste.Click += lblPaste_Click;
            // 
            // lblDemoVal
            // 
            lblDemoVal.AutoSize = true;
            lblDemoVal.Cursor = Cursors.Hand;
            lblDemoVal.Font = new Font("Segoe UI", 9F, FontStyle.Underline);
            lblDemoVal.Location = new Point(255, 7);
            lblDemoVal.Name = "lblDemoVal";
            lblDemoVal.Size = new Size(75, 15);
            lblDemoVal.TabIndex = 3;
            lblDemoVal.Text = "Demo values";
            lblDemoVal.Click += lblDemoVal_Click;
            // 
            // chkSpaceInES
            // 
            chkSpaceInES.AutoSize = true;
            chkSpaceInES.Location = new Point(12, 390);
            chkSpaceInES.Name = "chkSpaceInES";
            chkSpaceInES.Size = new Size(167, 19);
            chkSpaceInES.TabIndex = 3;
            chkSpaceInES.Text = "Spaces for equality symbol";
            chkSpaceInES.UseVisualStyleBackColor = true;
            chkSpaceInES.CheckedChanged += TriggerConvert;
            // 
            // chkTrimProp
            // 
            chkTrimProp.AutoSize = true;
            chkTrimProp.Checked = true;
            chkTrimProp.CheckState = CheckState.Checked;
            chkTrimProp.Location = new Point(188, 390);
            chkTrimProp.Name = "chkTrimProp";
            chkTrimProp.Size = new Size(192, 19);
            chkTrimProp.TabIndex = 3;
            chkTrimProp.Text = "Remove spaces from properties";
            chkTrimProp.UseVisualStyleBackColor = true;
            chkTrimProp.CheckedChanged += TriggerConvert;
            // 
            // chkTrimVal
            // 
            chkTrimVal.AutoSize = true;
            chkTrimVal.Checked = true;
            chkTrimVal.CheckState = CheckState.Checked;
            chkTrimVal.Location = new Point(389, 390);
            chkTrimVal.Name = "chkTrimVal";
            chkTrimVal.Size = new Size(172, 19);
            chkTrimVal.TabIndex = 3;
            chkTrimVal.Text = "Remove spaces from values";
            chkTrimVal.UseVisualStyleBackColor = true;
            chkTrimVal.CheckedChanged += TriggerConvert;
            // 
            // cmbES
            // 
            cmbES.BackColor = Color.FromArgb(17, 17, 17);
            cmbES.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbES.FlatStyle = FlatStyle.Flat;
            cmbES.ForeColor = Color.White;
            cmbES.FormattingEnabled = true;
            cmbES.Items.AddRange(new object[] { "Equal", "Colon" });
            cmbES.Location = new Point(12, 355);
            cmbES.Name = "cmbES";
            cmbES.Size = new Size(134, 23);
            cmbES.TabIndex = 4;
            cmbES.SelectedIndexChanged += TriggerConvert;
            // 
            // cmbChildBeh
            // 
            cmbChildBeh.BackColor = Color.FromArgb(17, 17, 17);
            cmbChildBeh.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChildBeh.FlatStyle = FlatStyle.Flat;
            cmbChildBeh.ForeColor = Color.White;
            cmbChildBeh.FormattingEnabled = true;
            cmbChildBeh.Items.AddRange(new object[] { "Include", "Ignore" });
            cmbChildBeh.Location = new Point(152, 355);
            cmbChildBeh.Name = "cmbChildBeh";
            cmbChildBeh.Size = new Size(134, 23);
            cmbChildBeh.TabIndex = 4;
            cmbChildBeh.SelectedIndexChanged += TriggerConvert;
            // 
            // cmbChildSep
            // 
            cmbChildSep.BackColor = Color.FromArgb(17, 17, 17);
            cmbChildSep.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChildSep.FlatStyle = FlatStyle.Flat;
            cmbChildSep.ForeColor = Color.White;
            cmbChildSep.FormattingEnabled = true;
            cmbChildSep.Items.AddRange(new object[] { "Dot", "Underscore", "Dash", "None" });
            cmbChildSep.Location = new Point(292, 355);
            cmbChildSep.Name = "cmbChildSep";
            cmbChildSep.Size = new Size(134, 23);
            cmbChildSep.TabIndex = 4;
            cmbChildSep.SelectedIndexChanged += TriggerConvert;
            // 
            // cmbArrayBeh
            // 
            cmbArrayBeh.BackColor = Color.FromArgb(17, 17, 17);
            cmbArrayBeh.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbArrayBeh.FlatStyle = FlatStyle.Flat;
            cmbArrayBeh.ForeColor = Color.White;
            cmbArrayBeh.FormattingEnabled = true;
            cmbArrayBeh.Items.AddRange(new object[] { "Include", "Ignore" });
            cmbArrayBeh.Location = new Point(432, 355);
            cmbArrayBeh.Name = "cmbArrayBeh";
            cmbArrayBeh.Size = new Size(134, 23);
            cmbArrayBeh.TabIndex = 4;
            cmbArrayBeh.SelectedIndexChanged += TriggerConvert;
            // 
            // cmbArrayBrack
            // 
            cmbArrayBrack.BackColor = Color.FromArgb(17, 17, 17);
            cmbArrayBrack.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbArrayBrack.FlatStyle = FlatStyle.Flat;
            cmbArrayBrack.ForeColor = Color.White;
            cmbArrayBrack.FormattingEnabled = true;
            cmbArrayBrack.Items.AddRange(new object[] { "Round", "Square", "Curly", "None" });
            cmbArrayBrack.Location = new Point(572, 355);
            cmbArrayBrack.Name = "cmbArrayBrack";
            cmbArrayBrack.Size = new Size(134, 23);
            cmbArrayBrack.TabIndex = 4;
            cmbArrayBrack.SelectedIndexChanged += TriggerConvert;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 338);
            label3.Name = "label3";
            label3.Size = new Size(94, 15);
            label3.TabIndex = 3;
            label3.Text = "Equality symbol:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(152, 338);
            label4.Name = "label4";
            label4.Size = new Size(94, 15);
            label4.TabIndex = 3;
            label4.Text = "Child behaviour:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(292, 337);
            label5.Name = "label5";
            label5.Size = new Size(90, 15);
            label5.TabIndex = 3;
            label5.Text = "Child separator:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(432, 337);
            label6.Name = "label6";
            label6.Size = new Size(94, 15);
            label6.TabIndex = 3;
            label6.Text = "Array behaviour:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(572, 337);
            label7.Name = "label7";
            label7.Size = new Size(85, 15);
            label7.TabIndex = 3;
            label7.Text = "Array brackets:";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 26, 26);
            ClientSize = new Size(971, 419);
            Controls.Add(cmbArrayBrack);
            Controls.Add(cmbArrayBeh);
            Controls.Add(cmbChildBeh);
            Controls.Add(cmbChildSep);
            Controls.Add(cmbES);
            Controls.Add(label2);
            Controls.Add(lblDemoVal);
            Controls.Add(lblPaste);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(btnConvert);
            Controls.Add(chkTrimVal);
            Controls.Add(chkTrimProp);
            Controls.Add(chkSpaceInES);
            Controls.Add(chkAutoConvert);
            Controls.Add(txtOut);
            Controls.Add(txtIn);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Secrets2GitlabVar - AvA.Soft";
            Load += frmMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtIn;
        private TextBox txtOut;
        private CheckBox chkAutoConvert;
        private Button btnConvert;
        private Label label1;
        private Label label2;
        private Label lblPaste;
        private Label lblDemoVal;
        private CheckBox chkSpaceInES;
        private CheckBox chkTrimProp;
        private CheckBox chkTrimVal;
        private ComboBox cmbES;
        private ComboBox cmbChildBeh;
        private ComboBox cmbChildSep;
        private ComboBox cmbArrayBeh;
        private ComboBox cmbArrayBrack;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
    }
}
