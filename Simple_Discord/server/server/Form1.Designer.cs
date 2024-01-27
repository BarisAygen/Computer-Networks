namespace server
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
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_listen = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.logs_users = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.logs_if100_subscriptions = new System.Windows.Forms.RichTextBox();
            this.logs_sps101_subscriptions = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(132, 106);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(205, 26);
            this.textBox_port.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port:";
            // 
            // button_listen
            // 
            this.button_listen.Location = new System.Drawing.Point(351, 102);
            this.button_listen.Name = "button_listen";
            this.button_listen.Size = new System.Drawing.Size(84, 34);
            this.button_listen.TabIndex = 2;
            this.button_listen.Text = "Listen";
            this.button_listen.UseVisualStyleBackColor = true;
            this.button_listen.Click += new System.EventHandler(this.button_listen_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(63, 172);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(385, 261);
            this.logs.TabIndex = 3;
            this.logs.Text = "";
            // 
            // logs_users
            // 
            this.logs_users.Location = new System.Drawing.Point(500, 172);
            this.logs_users.Name = "logs_users";
            this.logs_users.Size = new System.Drawing.Size(372, 261);
            this.logs_users.TabIndex = 7;
            this.logs_users.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(614, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Connected Users";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1053, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "IF100 Subscriptions";
            // 
            // logs_if100_subscriptions
            // 
            this.logs_if100_subscriptions.Location = new System.Drawing.Point(924, 172);
            this.logs_if100_subscriptions.Name = "logs_if100_subscriptions";
            this.logs_if100_subscriptions.Size = new System.Drawing.Size(386, 261);
            this.logs_if100_subscriptions.TabIndex = 10;
            this.logs_if100_subscriptions.Text = "";
            // 
            // logs_sps101_subscriptions
            // 
            this.logs_sps101_subscriptions.Location = new System.Drawing.Point(1354, 172);
            this.logs_sps101_subscriptions.Name = "logs_sps101_subscriptions";
            this.logs_sps101_subscriptions.Size = new System.Drawing.Size(386, 261);
            this.logs_sps101_subscriptions.TabIndex = 11;
            this.logs_sps101_subscriptions.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1479, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "SPS101 Subscriptions";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1798, 614);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.logs_sps101_subscriptions);
            this.Controls.Add(this.logs_if100_subscriptions);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logs_users);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button_listen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_port);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_listen;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.RichTextBox logs_users;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox logs_if100_subscriptions;
        private System.Windows.Forms.RichTextBox logs_sps101_subscriptions;
        private System.Windows.Forms.Label label5;
    }
}

