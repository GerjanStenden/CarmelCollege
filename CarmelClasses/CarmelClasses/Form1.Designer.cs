namespace CarmelClasses
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
            this.btn_Convert = new System.Windows.Forms.Button();
            this.comboBox_Templates = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_Schedules = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_Wings = new System.Windows.Forms.ComboBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.textBox_InputFile = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.dgClassHourColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgStartingHourColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgStartingMinutesColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgDurationColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btn_AddTemplate = new System.Windows.Forms.Button();
            this.btn_DeleteTemplate = new System.Windows.Forms.Button();
            this.btn_EditTemplate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Convert
            // 
            this.btn_Convert.Location = new System.Drawing.Point(197, 124);
            this.btn_Convert.Name = "btn_Convert";
            this.btn_Convert.Size = new System.Drawing.Size(75, 23);
            this.btn_Convert.TabIndex = 4;
            this.btn_Convert.Text = "Convert";
            this.btn_Convert.UseVisualStyleBackColor = true;
            this.btn_Convert.Click += new System.EventHandler(this.btn_Convert_Click);
            // 
            // comboBox_Templates
            // 
            this.comboBox_Templates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Templates.FormattingEnabled = true;
            this.comboBox_Templates.Location = new System.Drawing.Point(77, 8);
            this.comboBox_Templates.Name = "comboBox_Templates";
            this.comboBox_Templates.Size = new System.Drawing.Size(195, 21);
            this.comboBox_Templates.TabIndex = 5;
            this.comboBox_Templates.SelectedIndexChanged += new System.EventHandler(this.comboBox_Templates_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Templates:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Roosters:";
            // 
            // comboBox_Schedules
            // 
            this.comboBox_Schedules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Schedules.FormattingEnabled = true;
            this.comboBox_Schedules.Location = new System.Drawing.Point(77, 35);
            this.comboBox_Schedules.Name = "comboBox_Schedules";
            this.comboBox_Schedules.Size = new System.Drawing.Size(195, 21);
            this.comboBox_Schedules.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Vleugels:";
            // 
            // comboBox_Wings
            // 
            this.comboBox_Wings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Wings.FormattingEnabled = true;
            this.comboBox_Wings.Location = new System.Drawing.Point(77, 62);
            this.comboBox_Wings.Name = "comboBox_Wings";
            this.comboBox_Wings.Size = new System.Drawing.Size(195, 21);
            this.comboBox_Wings.TabIndex = 10;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Text files|*.txt|CSV files|*.csv";
            // 
            // textBox_InputFile
            // 
            this.textBox_InputFile.Location = new System.Drawing.Point(12, 98);
            this.textBox_InputFile.Name = "textBox_InputFile";
            this.textBox_InputFile.ReadOnly = true;
            this.textBox_InputFile.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBox_InputFile.Size = new System.Drawing.Size(260, 20);
            this.textBox_InputFile.TabIndex = 11;
            this.textBox_InputFile.Text = "Klik hier om een CSV bestand te laden.";
            this.textBox_InputFile.Click += new System.EventHandler(this.textBox_InputFile_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgClassHourColumn,
            this.dgStartingHourColumn,
            this.dgStartingMinutesColumn,
            this.dgDurationColumn});
            this.dataGridView.Location = new System.Drawing.Point(12, 159);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(460, 190);
            this.dataGridView.TabIndex = 12;
            // 
            // dgClassHourColumn
            // 
            this.dgClassHourColumn.HeaderText = "Lesuur";
            this.dgClassHourColumn.Items.AddRange(new object[] {
            "PAUZE",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.dgClassHourColumn.Name = "dgClassHourColumn";
            // 
            // dgStartingHourColumn
            // 
            this.dgStartingHourColumn.HeaderText = "Uur";
            this.dgStartingHourColumn.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.dgStartingHourColumn.Name = "dgStartingHourColumn";
            // 
            // dgStartingMinutesColumn
            // 
            this.dgStartingMinutesColumn.HeaderText = "Minuten";
            this.dgStartingMinutesColumn.Items.AddRange(new object[] {
            "00",
            "05",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60"});
            this.dgStartingMinutesColumn.Name = "dgStartingMinutesColumn";
            // 
            // dgDurationColumn
            // 
            this.dgDurationColumn.HeaderText = "Duur";
            this.dgDurationColumn.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "60"});
            this.dgDurationColumn.Name = "dgDurationColumn";
            // 
            // btn_AddTemplate
            // 
            this.btn_AddTemplate.Location = new System.Drawing.Point(278, 8);
            this.btn_AddTemplate.Name = "btn_AddTemplate";
            this.btn_AddTemplate.Size = new System.Drawing.Size(21, 21);
            this.btn_AddTemplate.TabIndex = 13;
            this.btn_AddTemplate.Text = "+";
            this.btn_AddTemplate.UseVisualStyleBackColor = true;
            this.btn_AddTemplate.Click += new System.EventHandler(this.btn_AddTemplate_Click);
            // 
            // btn_DeleteTemplate
            // 
            this.btn_DeleteTemplate.Location = new System.Drawing.Point(305, 8);
            this.btn_DeleteTemplate.Name = "btn_DeleteTemplate";
            this.btn_DeleteTemplate.Size = new System.Drawing.Size(21, 21);
            this.btn_DeleteTemplate.TabIndex = 14;
            this.btn_DeleteTemplate.Text = "-";
            this.btn_DeleteTemplate.UseVisualStyleBackColor = true;
            this.btn_DeleteTemplate.Click += new System.EventHandler(this.btn_DeleteTemplate_Click);
            // 
            // btn_EditTemplate
            // 
            this.btn_EditTemplate.Location = new System.Drawing.Point(332, 8);
            this.btn_EditTemplate.Name = "btn_EditTemplate";
            this.btn_EditTemplate.Size = new System.Drawing.Size(21, 21);
            this.btn_EditTemplate.TabIndex = 15;
            this.btn_EditTemplate.Text = "*";
            this.btn_EditTemplate.UseVisualStyleBackColor = true;
            this.btn_EditTemplate.Click += new System.EventHandler(this.btn_EditTemplate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.btn_EditTemplate);
            this.Controls.Add(this.btn_DeleteTemplate);
            this.Controls.Add(this.btn_AddTemplate);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.textBox_InputFile);
            this.Controls.Add(this.comboBox_Wings);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_Schedules);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_Templates);
            this.Controls.Add(this.btn_Convert);
            this.Name = "Form1";
            this.Text = "RCT Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Convert;
        private System.Windows.Forms.ComboBox comboBox_Templates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Schedules;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_Wings;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox textBox_InputFile;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgClassHourColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgStartingHourColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgStartingMinutesColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgDurationColumn;
        private System.Windows.Forms.Button btn_AddTemplate;
        private System.Windows.Forms.Button btn_DeleteTemplate;
        private System.Windows.Forms.Button btn_EditTemplate;
    }
}

