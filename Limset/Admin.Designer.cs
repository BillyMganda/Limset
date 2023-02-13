namespace Limset
{
    partial class Admin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDataMachine = new System.Windows.Forms.Button();
            this.btnDataOnline = new System.Windows.Forms.Button();
            this.btnDisableUser = new System.Windows.Forms.Button();
            this.btnAddNewUser = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDataMachine);
            this.groupBox1.Controls.Add(this.btnDataOnline);
            this.groupBox1.Controls.Add(this.btnDisableUser);
            this.groupBox1.Controls.Add(this.btnAddNewUser);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(487, 115);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Admin Tools";
            // 
            // btnDataMachine
            // 
            this.btnDataMachine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataMachine.Image = ((System.Drawing.Image)(resources.GetObject("btnDataMachine.Image")));
            this.btnDataMachine.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDataMachine.Location = new System.Drawing.Point(365, 20);
            this.btnDataMachine.Name = "btnDataMachine";
            this.btnDataMachine.Size = new System.Drawing.Size(113, 86);
            this.btnDataMachine.TabIndex = 3;
            this.btnDataMachine.Text = "Data to Machine";
            this.btnDataMachine.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDataMachine.UseVisualStyleBackColor = true;
            this.btnDataMachine.Click += new System.EventHandler(this.btnDataMachine_Click);
            // 
            // btnDataOnline
            // 
            this.btnDataOnline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataOnline.Image = ((System.Drawing.Image)(resources.GetObject("btnDataOnline.Image")));
            this.btnDataOnline.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDataOnline.Location = new System.Drawing.Point(246, 20);
            this.btnDataOnline.Name = "btnDataOnline";
            this.btnDataOnline.Size = new System.Drawing.Size(113, 86);
            this.btnDataOnline.TabIndex = 2;
            this.btnDataOnline.Text = "Data Online";
            this.btnDataOnline.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDataOnline.UseVisualStyleBackColor = true;
            this.btnDataOnline.Click += new System.EventHandler(this.btnDataOnline_Click);
            // 
            // btnDisableUser
            // 
            this.btnDisableUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisableUser.Image = ((System.Drawing.Image)(resources.GetObject("btnDisableUser.Image")));
            this.btnDisableUser.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDisableUser.Location = new System.Drawing.Point(127, 20);
            this.btnDisableUser.Name = "btnDisableUser";
            this.btnDisableUser.Size = new System.Drawing.Size(113, 86);
            this.btnDisableUser.TabIndex = 1;
            this.btnDisableUser.Text = "Disable User";
            this.btnDisableUser.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDisableUser.UseVisualStyleBackColor = true;
            this.btnDisableUser.Click += new System.EventHandler(this.btnDisableUser_Click);
            // 
            // btnAddNewUser
            // 
            this.btnAddNewUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewUser.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNewUser.Image")));
            this.btnAddNewUser.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAddNewUser.Location = new System.Drawing.Point(8, 20);
            this.btnAddNewUser.Name = "btnAddNewUser";
            this.btnAddNewUser.Size = new System.Drawing.Size(113, 86);
            this.btnAddNewUser.TabIndex = 0;
            this.btnAddNewUser.Text = "Add New User";
            this.btnAddNewUser.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddNewUser.UseVisualStyleBackColor = true;
            this.btnAddNewUser.Click += new System.EventHandler(this.btnAddNewUser_Click);
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 123);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Admin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private Button btnAddNewUser;
        private Button btnDataOnline;
        private Button btnDisableUser;
        private Button btnDataMachine;
    }
}