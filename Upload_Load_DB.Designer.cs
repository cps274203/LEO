namespace Load_Effort_Optimisation
{
    partial class Upload_Load_DB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Upload_Load_DB));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.projectlistBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.load_DBDataSet = new Load_Effort_Optimisation.Load_DBDataSet();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.platformIPmappingBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.project_listTableAdapter = new Load_Effort_Optimisation.Load_DBDataSetTableAdapters.Project_listTableAdapter();
            this.platform_IP_mappingTableAdapter = new Load_Effort_Optimisation.Load_DBDataSetTableAdapters.Platform_IP_mappingTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.projectlistBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.load_DBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.platformIPmappingBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(105, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 5;
            this.label1.Text = "User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(10, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "Project Name";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(511, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 44);
            this.button1.TabIndex = 4;
            this.button1.Text = "Submit to Load DB";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(12, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "Human Verdict";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "PASS",
            "FAIL",
            "INVALID",
            "TBD",
            "COND_PASS"});
            this.comboBox1.Location = new System.Drawing.Point(105, 101);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Arrowhead",
            "Athens",
            "Uluru",
            "Paris",
            "Pluto",
            "Pluto2",
            "Moon",
            "Malibu",
            "Nalanda",
            "Nifty",
            "Niles",
            "Neo",
            "Oasis",
            "Rabat2",
            "Rabat2_MR",
            "Tofino",
            "Udupi",
            "Unity",
            "Vancouver1",
            "Vancouver2",
            "Vancouver3",
            "Wayanad",
            "Xanadu",
            "Yukon",
            "Yukon1_5",
            "Yukon2"});
            this.comboBox2.Location = new System.Drawing.Point(105, 51);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 2;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 652);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(867, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(12, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "Platform Name";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Dell R610",
            "Dell R620",
            "Dell R710",
            "Kontron TIGH2U",
            "Kontron SB",
            "ComE",
            "MPX50",
            "RMS220",
            "MPX12000",
            "RHKVM-HP G8",
            "RHKVM-Dell 620",
            "RHKVM-CISCO UCS",
            "RHKVM-Kontron",
            "KVM",
            "VMware-CISCO UCS",
            "VMware-HP G8",
            "VMware-Dell 620",
            "CentOS",
            "CISCO UCS",
            "HP DL360 G7",
            "DellR630",
            "VMware-Dell 630",
            "RHKVM-Dell 630",
            "HP DL460 G8",
            "VMware-HP DL460 G8",
            "DellR720",
            "VMware-Dell 720",
            "HP DL360 G8",
            "DELL620-OEL5.7",
            "Amazon c3.2x large"});
            this.comboBox3.Location = new System.Drawing.Point(105, 151);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 21);
            this.comboBox3.TabIndex = 9;
            // 
            // comboBox4
            // 
            this.comboBox4.DataSource = this.projectlistBindingSource;
            this.comboBox4.DisplayMember = "Project_name";
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(106, 51);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(121, 21);
            this.comboBox4.TabIndex = 11;
            // 
            // projectlistBindingSource
            // 
            this.projectlistBindingSource.DataMember = "Project_list";
            this.projectlistBindingSource.DataSource = this.load_DBDataSet;
            // 
            // load_DBDataSet
            // 
            this.load_DBDataSet.DataSetName = "Load_DBDataSet";
            this.load_DBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // comboBox6
            // 
            this.comboBox6.DataSource = this.platformIPmappingBindingSource;
            this.comboBox6.DisplayMember = "Platform_name";
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(106, 151);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(121, 21);
            this.comboBox6.TabIndex = 13;
            // 
            // platformIPmappingBindingSource
            // 
            this.platformIPmappingBindingSource.DataMember = "Platform_IP_mapping";
            this.platformIPmappingBindingSource.DataSource = this.load_DBDataSet;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(511, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 44);
            this.button2.TabIndex = 14;
            this.button2.Text = "Load Project name from DB";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(511, 143);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(145, 44);
            this.button3.TabIndex = 15;
            this.button3.Text = "Load Platform name from DB";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label5.Location = new System.Drawing.Point(-3, 661);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(454, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Note:-Database is located at \\\\bcnas02\\RAD\\RAD\\DraftDocs\\DV\\Users\\Chandra\\LEO_DB\\" +
    "";
            // 
            // project_listTableAdapter
            // 
            this.project_listTableAdapter.ClearBeforeFill = true;
            // 
            // platform_IP_mappingTableAdapter
            // 
            this.platform_IP_mappingTableAdapter.ClearBeforeFill = true;
            // 
            // Upload_Load_DB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 674);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBox6);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "Upload_Load_DB";
            this.Text = "Upload_to_Load_DB";
            this.Load += new System.EventHandler(this.Upload_Load_DB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.projectlistBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.load_DBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.platformIPmappingBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private Load_DBDataSet load_DBDataSet;
        private System.Windows.Forms.BindingSource projectlistBindingSource;
        private Load_DBDataSetTableAdapters.Project_listTableAdapter project_listTableAdapter;
        private System.Windows.Forms.BindingSource platformIPmappingBindingSource;
        private Load_DBDataSetTableAdapters.Platform_IP_mappingTableAdapter platform_IP_mappingTableAdapter;
    }
}