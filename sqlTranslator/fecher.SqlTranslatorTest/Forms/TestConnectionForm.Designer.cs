namespace SQLTranslatorTest
{
    partial class TestConnectionForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.databasetbx = new System.Windows.Forms.TextBox();
            this.usertbx = new System.Windows.Forms.TextBox();
            this.passwordtbx = new System.Windows.Forms.TextBox();
            this.TestConnectionbtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.serverinstancetbx = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Database";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "User";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // databasetbx
            // 
            this.databasetbx.Location = new System.Drawing.Point(107, 63);
            this.databasetbx.Name = "databasetbx";
            this.databasetbx.Size = new System.Drawing.Size(252, 20);
            this.databasetbx.TabIndex = 1;
            this.databasetbx.Text = "test";
            // 
            // usertbx
            // 
            this.usertbx.Location = new System.Drawing.Point(107, 99);
            this.usertbx.Name = "usertbx";
            this.usertbx.Size = new System.Drawing.Size(252, 20);
            this.usertbx.TabIndex = 2;
            // 
            // passwordtbx
            // 
            this.passwordtbx.Location = new System.Drawing.Point(107, 136);
            this.passwordtbx.Name = "passwordtbx";
            this.passwordtbx.PasswordChar = '*';
            this.passwordtbx.Size = new System.Drawing.Size(252, 20);
            this.passwordtbx.TabIndex = 3;
            this.passwordtbx.UseSystemPasswordChar = true;
            // 
            // TestConnectionbtn
            // 
            this.TestConnectionbtn.Location = new System.Drawing.Point(136, 192);
            this.TestConnectionbtn.Name = "TestConnectionbtn";
            this.TestConnectionbtn.Size = new System.Drawing.Size(139, 23);
            this.TestConnectionbtn.TabIndex = 4;
            this.TestConnectionbtn.Text = "Test Connection";
            this.TestConnectionbtn.UseVisualStyleBackColor = true;
            this.TestConnectionbtn.Click += new System.EventHandler(this.TestConnectionbtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "label4";
            // 
            // serverinstancetbx
            // 
            this.serverinstancetbx.Location = new System.Drawing.Point(107, 27);
            this.serverinstancetbx.Name = "serverinstancetbx";
            this.serverinstancetbx.Size = new System.Drawing.Size(252, 20);
            this.serverinstancetbx.TabIndex = 0;
            this.serverinstancetbx.Text = "nb-ora-trv01\\sqlexpress";
            // 
            // TestConnectionForm
            // 
            this.AcceptButton = this.TestConnectionbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 235);
            this.Controls.Add(this.serverinstancetbx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TestConnectionbtn);
            this.Controls.Add(this.passwordtbx);
            this.Controls.Add(this.usertbx);
            this.Controls.Add(this.databasetbx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TestConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database connection";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox databasetbx;
        private System.Windows.Forms.TextBox usertbx;
        private System.Windows.Forms.TextBox passwordtbx;
        private System.Windows.Forms.Button TestConnectionbtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox serverinstancetbx;
    }
}