namespace client
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
            this.label_IP = new System.Windows.Forms.Label();
            this.label_port = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.logs_if100 = new System.Windows.Forms.RichTextBox();
            this.textBox_message_if100 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label_username = new System.Windows.Forms.Label();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.button_subscribe_if100 = new System.Windows.Forms.Button();
            this.button_unsubscribe_if100 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.logs_connect = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_subscribe_sps101 = new System.Windows.Forms.Button();
            this.button_unsubscribe_sps101 = new System.Windows.Forms.Button();
            this.logs_sps101 = new System.Windows.Forms.RichTextBox();
            this.textBox_message_sps101 = new System.Windows.Forms.TextBox();
            this.button_send_sps101 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.button_send_if100 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_IP
            // 
            this.label_IP.AutoSize = true;
            this.label_IP.Location = new System.Drawing.Point(59, 64);
            this.label_IP.Name = "label_IP";
            this.label_IP.Size = new System.Drawing.Size(22, 16);
            this.label_IP.TabIndex = 0;
            this.label_IP.Text = "IP:";
            // 
            // label_port
            // 
            this.label_port.AutoSize = true;
            this.label_port.Location = new System.Drawing.Point(45, 97);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(34, 16);
            this.label_port.TabIndex = 1;
            this.label_port.Text = "Port:";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(89, 62);
            this.textBox_ip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(116, 22);
            this.textBox_ip.TabIndex = 2;
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(89, 97);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(116, 22);
            this.textBox_port.TabIndex = 3;
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(89, 191);
            this.button_connect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(117, 27);
            this.button_connect.TabIndex = 4;
            this.button_connect.Text = "connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // logs_if100
            // 
            this.logs_if100.Enabled = false;
            this.logs_if100.Location = new System.Drawing.Point(332, 180);
            this.logs_if100.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logs_if100.Name = "logs_if100";
            this.logs_if100.Size = new System.Drawing.Size(316, 182);
            this.logs_if100.TabIndex = 5;
            this.logs_if100.Text = "";
            // 
            // textBox_message_if100
            // 
            this.textBox_message_if100.Enabled = false;
            this.textBox_message_if100.Location = new System.Drawing.Point(404, 385);
            this.textBox_message_if100.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_message_if100.Name = "textBox_message_if100";
            this.textBox_message_if100.Size = new System.Drawing.Size(148, 22);
            this.textBox_message_if100.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(328, 389);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Message:";
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.Location = new System.Drawing.Point(7, 143);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(73, 16);
            this.label_username.TabIndex = 9;
            this.label_username.Text = "Username:";
            this.label_username.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(89, 139);
            this.textBox_username.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(116, 22);
            this.textBox_username.TabIndex = 10;
            // 
            // button_subscribe_if100
            // 
            this.button_subscribe_if100.Enabled = false;
            this.button_subscribe_if100.Location = new System.Drawing.Point(433, 95);
            this.button_subscribe_if100.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_subscribe_if100.Name = "button_subscribe_if100";
            this.button_subscribe_if100.Size = new System.Drawing.Size(120, 27);
            this.button_subscribe_if100.TabIndex = 11;
            this.button_subscribe_if100.Text = "subscribe";
            this.button_subscribe_if100.UseVisualStyleBackColor = true;
            this.button_subscribe_if100.Click += new System.EventHandler(this.button_subscribe_if100_Click);
            // 
            // button_unsubscribe_if100
            // 
            this.button_unsubscribe_if100.Enabled = false;
            this.button_unsubscribe_if100.Location = new System.Drawing.Point(433, 137);
            this.button_unsubscribe_if100.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_unsubscribe_if100.Name = "button_unsubscribe_if100";
            this.button_unsubscribe_if100.Size = new System.Drawing.Size(120, 27);
            this.button_unsubscribe_if100.TabIndex = 13;
            this.button_unsubscribe_if100.Text = "unsubscribe";
            this.button_unsubscribe_if100.UseVisualStyleBackColor = true;
            this.button_unsubscribe_if100.Click += new System.EventHandler(this.button_unsubscribe_if100_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(469, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "IF100";
            // 
            // logs_connect
            // 
            this.logs_connect.Enabled = false;
            this.logs_connect.Location = new System.Drawing.Point(35, 276);
            this.logs_connect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logs_connect.Name = "logs_connect";
            this.logs_connect.Size = new System.Drawing.Size(237, 133);
            this.logs_connect.TabIndex = 15;
            this.logs_connect.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(837, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "SPS101";
            // 
            // button_subscribe_sps101
            // 
            this.button_subscribe_sps101.Enabled = false;
            this.button_subscribe_sps101.Location = new System.Drawing.Point(799, 95);
            this.button_subscribe_sps101.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_subscribe_sps101.Name = "button_subscribe_sps101";
            this.button_subscribe_sps101.Size = new System.Drawing.Size(120, 27);
            this.button_subscribe_sps101.TabIndex = 17;
            this.button_subscribe_sps101.Text = "subscribe";
            this.button_subscribe_sps101.UseVisualStyleBackColor = true;
            this.button_subscribe_sps101.Click += new System.EventHandler(this.button_subscribe_sps101_Click);
            // 
            // button_unsubscribe_sps101
            // 
            this.button_unsubscribe_sps101.Enabled = false;
            this.button_unsubscribe_sps101.Location = new System.Drawing.Point(799, 137);
            this.button_unsubscribe_sps101.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_unsubscribe_sps101.Name = "button_unsubscribe_sps101";
            this.button_unsubscribe_sps101.Size = new System.Drawing.Size(120, 27);
            this.button_unsubscribe_sps101.TabIndex = 18;
            this.button_unsubscribe_sps101.Text = "unsubscribe";
            this.button_unsubscribe_sps101.UseVisualStyleBackColor = true;
            this.button_unsubscribe_sps101.Click += new System.EventHandler(this.button_unsubscribe_sps101_Click);
            // 
            // logs_sps101
            // 
            this.logs_sps101.Enabled = false;
            this.logs_sps101.Location = new System.Drawing.Point(704, 180);
            this.logs_sps101.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logs_sps101.Name = "logs_sps101";
            this.logs_sps101.Size = new System.Drawing.Size(316, 182);
            this.logs_sps101.TabIndex = 19;
            this.logs_sps101.Text = "";
            // 
            // textBox_message_sps101
            // 
            this.textBox_message_sps101.Enabled = false;
            this.textBox_message_sps101.Location = new System.Drawing.Point(776, 385);
            this.textBox_message_sps101.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_message_sps101.Name = "textBox_message_sps101";
            this.textBox_message_sps101.Size = new System.Drawing.Size(148, 22);
            this.textBox_message_sps101.TabIndex = 20;
            // 
            // button_send_sps101
            // 
            this.button_send_sps101.Enabled = false;
            this.button_send_sps101.Location = new System.Drawing.Point(935, 385);
            this.button_send_sps101.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_send_sps101.Name = "button_send_sps101";
            this.button_send_sps101.Size = new System.Drawing.Size(87, 32);
            this.button_send_sps101.TabIndex = 21;
            this.button_send_sps101.Text = "send";
            this.button_send_sps101.UseVisualStyleBackColor = true;
            this.button_send_sps101.Click += new System.EventHandler(this.button_send_sps101_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(700, 389);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 16);
            this.label4.TabIndex = 22;
            this.label4.Text = "Message:";
            // 
            // button_disconnect
            // 
            this.button_disconnect.Enabled = false;
            this.button_disconnect.Location = new System.Drawing.Point(89, 223);
            this.button_disconnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(117, 27);
            this.button_disconnect.TabIndex = 23;
            this.button_disconnect.Text = "disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // button_send_if100
            // 
            this.button_send_if100.Enabled = false;
            this.button_send_if100.Location = new System.Drawing.Point(563, 380);
            this.button_send_if100.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_send_if100.Name = "button_send_if100";
            this.button_send_if100.Size = new System.Drawing.Size(87, 32);
            this.button_send_if100.TabIndex = 24;
            this.button_send_if100.Text = "send";
            this.button_send_if100.UseVisualStyleBackColor = true;
            this.button_send_if100.Click += new System.EventHandler(this.button_send_if100_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 492);
            this.Controls.Add(this.button_send_if100);
            this.Controls.Add(this.button_disconnect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_send_sps101);
            this.Controls.Add(this.textBox_message_sps101);
            this.Controls.Add(this.logs_sps101);
            this.Controls.Add(this.button_unsubscribe_sps101);
            this.Controls.Add(this.button_subscribe_sps101);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.logs_connect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_unsubscribe_if100);
            this.Controls.Add(this.button_subscribe_if100);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.label_username);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_message_if100);
            this.Controls.Add(this.logs_if100);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.label_port);
            this.Controls.Add(this.label_IP);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.Label label_port;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.RichTextBox logs_if100;
        private System.Windows.Forms.TextBox textBox_message_if100;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_username;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.Button button_subscribe_if100;
        private System.Windows.Forms.Button button_unsubscribe_if100;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox logs_connect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_subscribe_sps101;
        private System.Windows.Forms.Button button_unsubscribe_sps101;
        private System.Windows.Forms.RichTextBox logs_sps101;
        private System.Windows.Forms.TextBox textBox_message_sps101;
        private System.Windows.Forms.Button button_send_sps101;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.Button button_send_if100;
    }
}

