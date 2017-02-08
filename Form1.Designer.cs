namespace Load_Effort_Optimisation
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.usernamelvl = new System.Windows.Forms.Label();
            this.passlvl = new System.Windows.Forms.Label();
            this.usernamechkbox = new System.Windows.Forms.TextBox();
            this.passchkbox = new System.Windows.Forms.TextBox();
            this.submitbtn = new System.Windows.Forms.Button();
            this.resetbtn = new System.Windows.Forms.Button();
            this.closebtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // usernamelvl
            // 
            this.usernamelvl.AutoSize = true;
            this.usernamelvl.Font = new System.Drawing.Font("MV Boli", 25.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernamelvl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.usernamelvl.Location = new System.Drawing.Point(226, 73);
            this.usernamelvl.Name = "usernamelvl";
            this.usernamelvl.Size = new System.Drawing.Size(170, 45);
            this.usernamelvl.TabIndex = 0;
            this.usernamelvl.Text = "Username";
            // 
            // passlvl
            // 
            this.passlvl.AutoSize = true;
            this.passlvl.Font = new System.Drawing.Font("MV Boli", 25.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passlvl.ForeColor = System.Drawing.Color.RoyalBlue;
            this.passlvl.Location = new System.Drawing.Point(227, 128);
            this.passlvl.Name = "passlvl";
            this.passlvl.Size = new System.Drawing.Size(162, 45);
            this.passlvl.TabIndex = 1;
            this.passlvl.Text = "Password";
            // 
            // usernamechkbox
            // 
            this.usernamechkbox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernamechkbox.Location = new System.Drawing.Point(448, 76);
            this.usernamechkbox.Multiline = true;
            this.usernamechkbox.Name = "usernamechkbox";
            this.usernamechkbox.Size = new System.Drawing.Size(182, 36);
            this.usernamechkbox.TabIndex = 2;
            this.usernamechkbox.TextChanged += new System.EventHandler(this.usernamechkbox_TextChanged);
            // 
            // passchkbox
            // 
            this.passchkbox.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passchkbox.Location = new System.Drawing.Point(448, 144);
            this.passchkbox.Multiline = true;
            this.passchkbox.Name = "passchkbox";
            this.passchkbox.PasswordChar = '*';
            this.passchkbox.Size = new System.Drawing.Size(182, 32);
            this.passchkbox.TabIndex = 3;
            this.passchkbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passchkbox_KeyPress);
            // 
            // submitbtn
            // 
            this.submitbtn.Font = new System.Drawing.Font("Stencil", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitbtn.Location = new System.Drawing.Point(325, 231);
            this.submitbtn.Name = "submitbtn";
            this.submitbtn.Size = new System.Drawing.Size(99, 35);
            this.submitbtn.TabIndex = 4;
            this.submitbtn.Text = "&Submit";
            this.submitbtn.UseVisualStyleBackColor = true;
            this.submitbtn.Click += new System.EventHandler(this.submitbtn_Click);
            // 
            // resetbtn
            // 
            this.resetbtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.resetbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.resetbtn.Font = new System.Drawing.Font("Stencil", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.resetbtn.Location = new System.Drawing.Point(467, 231);
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.Size = new System.Drawing.Size(90, 35);
            this.resetbtn.TabIndex = 5;
            this.resetbtn.Text = "&Reset";
            this.resetbtn.UseVisualStyleBackColor = true;
            this.resetbtn.Click += new System.EventHandler(this.resetbtn_Click);
            // 
            // closebtn
            // 
            this.closebtn.Font = new System.Drawing.Font("Stencil", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.Location = new System.Drawing.Point(394, 290);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(90, 35);
            this.closebtn.TabIndex = 6;
            this.closebtn.Text = "&Close";
            this.closebtn.UseVisualStyleBackColor = true;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-6, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(227, 299);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.resetbtn;
            this.ClientSize = new System.Drawing.Size(667, 479);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.resetbtn);
            this.Controls.Add(this.submitbtn);
            this.Controls.Add(this.passchkbox);
            this.Controls.Add(this.usernamechkbox);
            this.Controls.Add(this.passlvl);
            this.Controls.Add(this.usernamelvl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usernamelvl;
        private System.Windows.Forms.Label passlvl;
        private System.Windows.Forms.TextBox passchkbox;
        private System.Windows.Forms.Button resetbtn;
        private System.Windows.Forms.Button submitbtn;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TextBox usernamechkbox;
    }
}

