﻿namespace Kategoryzacja_Eksper
{
    partial class Ass_Video_Form
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
            this.components = new System.ComponentModel.Container();
            this.SetupPanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbScreenOrder = new System.Windows.Forms.CheckBox();
            this.numStartSample = new System.Windows.Forms.NumericUpDown();
            this.appTerm = new System.Windows.Forms.Button();
            this.bExperStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bLoadInputFile = new System.Windows.Forms.Button();
            this.tAge = new System.Windows.Forms.TextBox();
            this.tSurname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tInputFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.bFolderAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tBCD = new System.Windows.Forms.TextBox();
            this.PresentPanel = new System.Windows.Forms.Panel();
            this.ResponsePanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.lInstructions = new System.Windows.Forms.Label();
            this.tAnswer = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SetupPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStartSample)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.ResponsePanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SetupPanel
            // 
            this.SetupPanel.BackColor = System.Drawing.SystemColors.Control;
            this.SetupPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SetupPanel.Controls.Add(this.label6);
            this.SetupPanel.Controls.Add(this.label5);
            this.SetupPanel.Controls.Add(this.cbScreenOrder);
            this.SetupPanel.Controls.Add(this.numStartSample);
            this.SetupPanel.Controls.Add(this.appTerm);
            this.SetupPanel.Controls.Add(this.bExperStart);
            this.SetupPanel.Controls.Add(this.groupBox2);
            this.SetupPanel.Controls.Add(this.groupBox1);
            this.SetupPanel.Location = new System.Drawing.Point(0, 0);
            this.SetupPanel.Name = "SetupPanel";
            this.SetupPanel.Size = new System.Drawing.Size(516, 400);
            this.SetupPanel.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(289, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "próbki";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(142, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "od";
            // 
            // cbScreenOrder
            // 
            this.cbScreenOrder.AutoSize = true;
            this.cbScreenOrder.Checked = true;
            this.cbScreenOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbScreenOrder.Location = new System.Drawing.Point(3, 178);
            this.cbScreenOrder.Name = "cbScreenOrder";
            this.cbScreenOrder.Size = new System.Drawing.Size(116, 17);
            this.cbScreenOrder.TabIndex = 6;
            this.cbScreenOrder.Text = "Odwrócone ekrany";
            this.cbScreenOrder.UseVisualStyleBackColor = true;
            // 
            // numStartSample
            // 
            this.numStartSample.Location = new System.Drawing.Point(163, 118);
            this.numStartSample.Name = "numStartSample";
            this.numStartSample.Size = new System.Drawing.Size(120, 20);
            this.numStartSample.TabIndex = 12;
            // 
            // appTerm
            // 
            this.appTerm.Location = new System.Drawing.Point(145, 174);
            this.appTerm.Name = "appTerm";
            this.appTerm.Size = new System.Drawing.Size(174, 23);
            this.appTerm.TabIndex = 8;
            this.appTerm.Text = "Wróć do poprzedniego ekranu";
            this.appTerm.UseVisualStyleBackColor = true;
            this.appTerm.Click += new System.EventHandler(this.appTerm_Click);
            // 
            // bExperStart
            // 
            this.bExperStart.Location = new System.Drawing.Point(3, 115);
            this.bExperStart.Name = "bExperStart";
            this.bExperStart.Size = new System.Drawing.Size(125, 23);
            this.bExperStart.TabIndex = 7;
            this.bExperStart.Text = "uruchom eksperyment";
            this.bExperStart.UseVisualStyleBackColor = true;
            this.bExperStart.Click += new System.EventHandler(this.bExperStart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bLoadInputFile);
            this.groupBox2.Controls.Add(this.tAge);
            this.groupBox2.Controls.Add(this.tSurname);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tInputFile);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tName);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(508, 107);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Input Parameters";
            // 
            // bLoadInputFile
            // 
            this.bLoadInputFile.Location = new System.Drawing.Point(245, 73);
            this.bLoadInputFile.Name = "bLoadInputFile";
            this.bLoadInputFile.Size = new System.Drawing.Size(75, 23);
            this.bLoadInputFile.TabIndex = 3;
            this.bLoadInputFile.Text = "Wczytaj plik";
            this.bLoadInputFile.UseVisualStyleBackColor = true;
            this.bLoadInputFile.Click += new System.EventHandler(this.bLoadInputFile_Click);
            // 
            // tAge
            // 
            this.tAge.Location = new System.Drawing.Point(203, 31);
            this.tAge.MaxLength = 40;
            this.tAge.Name = "tAge";
            this.tAge.Size = new System.Drawing.Size(89, 20);
            this.tAge.TabIndex = 2;
            // 
            // tSurname
            // 
            this.tSurname.Location = new System.Drawing.Point(108, 31);
            this.tSurname.MaxLength = 40;
            this.tSurname.Name = "tSurname";
            this.tSurname.Size = new System.Drawing.Size(89, 20);
            this.tSurname.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Plik wejściowy";
            // 
            // tInputFile
            // 
            this.tInputFile.Location = new System.Drawing.Point(13, 76);
            this.tInputFile.Name = "tInputFile";
            this.tInputFile.ReadOnly = true;
            this.tInputFile.Size = new System.Drawing.Size(226, 20);
            this.tInputFile.TabIndex = 5;
            this.tInputFile.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Badany: Imię, Nazwisko,Wiek";
            // 
            // tName
            // 
            this.tName.Location = new System.Drawing.Point(13, 31);
            this.tName.MaxLength = 40;
            this.tName.Name = "tName";
            this.tName.Size = new System.Drawing.Size(89, 20);
            this.tName.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.bFolderAdd);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tBCD);
            this.groupBox1.Location = new System.Drawing.Point(3, 203);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(508, 192);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(338, 41);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(164, 20);
            this.textBox2.TabIndex = 4;
            // 
            // bFolderAdd
            // 
            this.bFolderAdd.Location = new System.Drawing.Point(6, 43);
            this.bFolderAdd.Name = "bFolderAdd";
            this.bFolderAdd.Size = new System.Drawing.Size(75, 23);
            this.bFolderAdd.TabIndex = 3;
            this.bFolderAdd.Text = "Wybierz Folder";
            this.bFolderAdd.UseVisualStyleBackColor = true;
            this.bFolderAdd.Click += new System.EventHandler(this.bFolderAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "CD: ";
            // 
            // tBCD
            // 
            this.tBCD.Location = new System.Drawing.Point(35, 17);
            this.tBCD.Name = "tBCD";
            this.tBCD.ReadOnly = true;
            this.tBCD.Size = new System.Drawing.Size(467, 20);
            this.tBCD.TabIndex = 1;
            this.tBCD.TextChanged += new System.EventHandler(this.tBCD_TextChanged);
            // 
            // PresentPanel
            // 
            this.PresentPanel.BackColor = System.Drawing.Color.Black;
            this.PresentPanel.Location = new System.Drawing.Point(675, 335);
            this.PresentPanel.Name = "PresentPanel";
            this.PresentPanel.Size = new System.Drawing.Size(200, 100);
            this.PresentPanel.TabIndex = 1;
            // 
            // ResponsePanel
            // 
            this.ResponsePanel.Controls.Add(this.tableLayoutPanel1);
            this.ResponsePanel.Location = new System.Drawing.Point(447, 482);
            this.ResponsePanel.Name = "ResponsePanel";
            this.ResponsePanel.Size = new System.Drawing.Size(444, 58);
            this.ResponsePanel.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 11;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.09F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.1F));
            this.tableLayoutPanel1.Controls.Add(this.button1, 10, 1);
            this.tableLayoutPanel1.Controls.Add(this.lInstructions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tAnswer, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.MaximumSize = new System.Drawing.Size(0, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(444, 58);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(401, 31);
            this.button1.Margin = new System.Windows.Forms.Padding(1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.zapisz_odpowiedz);
            // 
            // lInstructions
            // 
            this.lInstructions.AutoEllipsis = true;
            this.lInstructions.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lInstructions, 11);
            this.lInstructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lInstructions.ForeColor = System.Drawing.Color.White;
            this.lInstructions.Location = new System.Drawing.Point(1, 1);
            this.lInstructions.Margin = new System.Windows.Forms.Padding(1);
            this.lInstructions.Name = "lInstructions";
            this.lInstructions.Size = new System.Drawing.Size(442, 28);
            this.lInstructions.TabIndex = 11;
            this.lInstructions.Text = "Przykładowe pytanie dla słuchacza asdfasdfasddddddddddddddddd";
            this.lInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tAnswer
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tAnswer, 10);
            this.tAnswer.Location = new System.Drawing.Point(3, 33);
            this.tAnswer.Name = "tAnswer";
            this.tAnswer.Size = new System.Drawing.Size(394, 20);
            this.tAnswer.TabIndex = 12;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Ass_Video_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.ResponsePanel);
            this.Controls.Add(this.PresentPanel);
            this.Controls.Add(this.SetupPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Ass_Video_Form";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Aso_Video_Form_KeyDown);
            this.SetupPanel.ResumeLayout(false);
            this.SetupPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStartSample)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResponsePanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel SetupPanel;
        private System.Windows.Forms.Panel PresentPanel;
        private System.Windows.Forms.Panel ResponsePanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tInputFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bLoadInputFile;
        private System.Windows.Forms.TextBox tAge;
        private System.Windows.Forms.TextBox tSurname;
        private System.Windows.Forms.Button bExperStart;
        private System.Windows.Forms.Button appTerm;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cbScreenOrder;
        private System.Windows.Forms.TextBox tBCD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button bFolderAdd;
        private System.Windows.Forms.NumericUpDown numStartSample;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lInstructions;
        private System.Windows.Forms.TextBox tAnswer;
    }
}

