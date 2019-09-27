namespace CopperCatClient
{
	partial class frmClient
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
			this.txtClientLog = new System.Windows.Forms.TextBox();
			this.lblServerIP = new System.Windows.Forms.Label();
			this.lblServerPort = new System.Windows.Forms.Label();
			this.txtServerIP = new System.Windows.Forms.TextBox();
			this.txtServerPort = new System.Windows.Forms.TextBox();
			this.lblPayload = new System.Windows.Forms.Label();
			this.txtPayload = new System.Windows.Forms.TextBox();
			this.btnConnect = new System.Windows.Forms.Button();
			this.btnSend = new System.Windows.Forms.Button();
			this.btnMoveLeft = new System.Windows.Forms.Button();
			this.btnMoveUp = new System.Windows.Forms.Button();
			this.btnMoveRight = new System.Windows.Forms.Button();
			this.btnMoveDown = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtClientLog
			// 
			this.txtClientLog.Enabled = false;
			this.txtClientLog.Location = new System.Drawing.Point(12, 12);
			this.txtClientLog.Multiline = true;
			this.txtClientLog.Name = "txtClientLog";
			this.txtClientLog.Size = new System.Drawing.Size(428, 226);
			this.txtClientLog.TabIndex = 0;
			// 
			// lblServerIP
			// 
			this.lblServerIP.AutoSize = true;
			this.lblServerIP.Location = new System.Drawing.Point(12, 244);
			this.lblServerIP.Name = "lblServerIP";
			this.lblServerIP.Size = new System.Drawing.Size(54, 13);
			this.lblServerIP.TabIndex = 1;
			this.lblServerIP.Text = "Server IP:";
			// 
			// lblServerPort
			// 
			this.lblServerPort.AutoSize = true;
			this.lblServerPort.Location = new System.Drawing.Point(12, 276);
			this.lblServerPort.Name = "lblServerPort";
			this.lblServerPort.Size = new System.Drawing.Size(63, 13);
			this.lblServerPort.TabIndex = 2;
			this.lblServerPort.Text = "Server Port:";
			// 
			// txtServerIP
			// 
			this.txtServerIP.Location = new System.Drawing.Point(81, 241);
			this.txtServerIP.Name = "txtServerIP";
			this.txtServerIP.Size = new System.Drawing.Size(159, 20);
			this.txtServerIP.TabIndex = 3;
			this.txtServerIP.Text = "192.168.1.142";
			// 
			// txtServerPort
			// 
			this.txtServerPort.Enabled = false;
			this.txtServerPort.Location = new System.Drawing.Point(81, 273);
			this.txtServerPort.Name = "txtServerPort";
			this.txtServerPort.Size = new System.Drawing.Size(78, 20);
			this.txtServerPort.TabIndex = 4;
			this.txtServerPort.Text = "23000";
			// 
			// lblPayload
			// 
			this.lblPayload.AutoSize = true;
			this.lblPayload.Location = new System.Drawing.Point(273, 244);
			this.lblPayload.Name = "lblPayload";
			this.lblPayload.Size = new System.Drawing.Size(48, 13);
			this.lblPayload.TabIndex = 5;
			this.lblPayload.Text = "Payload:";
			// 
			// txtPayload
			// 
			this.txtPayload.Location = new System.Drawing.Point(327, 241);
			this.txtPayload.Name = "txtPayload";
			this.txtPayload.Size = new System.Drawing.Size(113, 20);
			this.txtPayload.TabIndex = 6;
			// 
			// btnConnect
			// 
			this.btnConnect.Location = new System.Drawing.Point(165, 271);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(75, 23);
			this.btnConnect.TabIndex = 7;
			this.btnConnect.Text = "Connect";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(365, 270);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(75, 23);
			this.btnSend.TabIndex = 8;
			this.btnSend.Text = "Send";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// btnMoveLeft
			// 
			this.btnMoveLeft.Location = new System.Drawing.Point(167, 389);
			this.btnMoveLeft.Name = "btnMoveLeft";
			this.btnMoveLeft.Size = new System.Drawing.Size(32, 32);
			this.btnMoveLeft.TabIndex = 9;
			this.btnMoveLeft.Text = "<";
			this.btnMoveLeft.UseVisualStyleBackColor = true;
			this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
			// 
			// btnMoveUp
			// 
			this.btnMoveUp.Location = new System.Drawing.Point(205, 351);
			this.btnMoveUp.Name = "btnMoveUp";
			this.btnMoveUp.Size = new System.Drawing.Size(32, 32);
			this.btnMoveUp.TabIndex = 10;
			this.btnMoveUp.Text = "^";
			this.btnMoveUp.UseVisualStyleBackColor = true;
			this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
			// 
			// btnMoveRight
			// 
			this.btnMoveRight.Location = new System.Drawing.Point(243, 389);
			this.btnMoveRight.Name = "btnMoveRight";
			this.btnMoveRight.Size = new System.Drawing.Size(32, 32);
			this.btnMoveRight.TabIndex = 11;
			this.btnMoveRight.Text = ">";
			this.btnMoveRight.UseVisualStyleBackColor = true;
			this.btnMoveRight.Click += new System.EventHandler(this.btnMoveRight_Click);
			// 
			// btnMoveDown
			// 
			this.btnMoveDown.Location = new System.Drawing.Point(205, 427);
			this.btnMoveDown.Name = "btnMoveDown";
			this.btnMoveDown.Size = new System.Drawing.Size(32, 32);
			this.btnMoveDown.TabIndex = 12;
			this.btnMoveDown.Text = "v";
			this.btnMoveDown.UseVisualStyleBackColor = true;
			this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
			// 
			// frmClient
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1099, 659);
			this.Controls.Add(this.btnMoveDown);
			this.Controls.Add(this.btnMoveRight);
			this.Controls.Add(this.btnMoveUp);
			this.Controls.Add(this.btnMoveLeft);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.txtPayload);
			this.Controls.Add(this.lblPayload);
			this.Controls.Add(this.txtServerPort);
			this.Controls.Add(this.txtServerIP);
			this.Controls.Add(this.lblServerPort);
			this.Controls.Add(this.lblServerIP);
			this.Controls.Add(this.txtClientLog);
			this.Name = "frmClient";
			this.Text = "CopperCat - Client";
			this.Load += new System.EventHandler(this.frmClient_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmClient_Paint);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtClientLog;
		private System.Windows.Forms.Label lblServerIP;
		private System.Windows.Forms.Label lblServerPort;
		private System.Windows.Forms.TextBox txtServerIP;
		private System.Windows.Forms.TextBox txtServerPort;
		private System.Windows.Forms.Label lblPayload;
		private System.Windows.Forms.TextBox txtPayload;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.Button btnMoveLeft;
		private System.Windows.Forms.Button btnMoveUp;
		private System.Windows.Forms.Button btnMoveRight;
		private System.Windows.Forms.Button btnMoveDown;
	}
}

