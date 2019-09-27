namespace CopperCatServer
{
	partial class frmServer
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
			this.txtServerLog = new System.Windows.Forms.TextBox();
			this.btnStartServer = new System.Windows.Forms.Button();
			this.lblServerIP = new System.Windows.Forms.Label();
			this.txtServerIP = new System.Windows.Forms.TextBox();
			this.lbClients = new System.Windows.Forms.ListBox();
			this.lblClients = new System.Windows.Forms.Label();
			this.lblPort = new System.Windows.Forms.Label();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.lblPayload = new System.Windows.Forms.Label();
			this.txtPayload = new System.Windows.Forms.TextBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.lblServerLog = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtServerLog
			// 
			this.txtServerLog.Enabled = false;
			this.txtServerLog.Location = new System.Drawing.Point(12, 28);
			this.txtServerLog.Multiline = true;
			this.txtServerLog.Name = "txtServerLog";
			this.txtServerLog.Size = new System.Drawing.Size(367, 201);
			this.txtServerLog.TabIndex = 0;
			// 
			// btnStartServer
			// 
			this.btnStartServer.Location = new System.Drawing.Point(176, 233);
			this.btnStartServer.Name = "btnStartServer";
			this.btnStartServer.Size = new System.Drawing.Size(75, 23);
			this.btnStartServer.TabIndex = 1;
			this.btnStartServer.Text = "Start server";
			this.btnStartServer.UseVisualStyleBackColor = true;
			this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
			// 
			// lblServerIP
			// 
			this.lblServerIP.AutoSize = true;
			this.lblServerIP.Location = new System.Drawing.Point(13, 238);
			this.lblServerIP.Name = "lblServerIP";
			this.lblServerIP.Size = new System.Drawing.Size(54, 13);
			this.lblServerIP.TabIndex = 2;
			this.lblServerIP.Text = "Server IP:";
			// 
			// txtServerIP
			// 
			this.txtServerIP.Enabled = false;
			this.txtServerIP.Location = new System.Drawing.Point(70, 235);
			this.txtServerIP.Name = "txtServerIP";
			this.txtServerIP.Size = new System.Drawing.Size(100, 20);
			this.txtServerIP.TabIndex = 3;
			// 
			// lbClients
			// 
			this.lbClients.FormattingEnabled = true;
			this.lbClients.Location = new System.Drawing.Point(385, 28);
			this.lbClients.Name = "lbClients";
			this.lbClients.Size = new System.Drawing.Size(262, 199);
			this.lbClients.TabIndex = 4;
			// 
			// lblClients
			// 
			this.lblClients.AutoSize = true;
			this.lblClients.Location = new System.Drawing.Point(385, 12);
			this.lblClients.Name = "lblClients";
			this.lblClients.Size = new System.Drawing.Size(41, 13);
			this.lblClients.TabIndex = 5;
			this.lblClients.Text = "Clients:";
			// 
			// lblPort
			// 
			this.lblPort.AutoSize = true;
			this.lblPort.Location = new System.Drawing.Point(13, 264);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(29, 13);
			this.lblPort.TabIndex = 6;
			this.lblPort.Text = "Port:";
			// 
			// txtPort
			// 
			this.txtPort.Enabled = false;
			this.txtPort.Location = new System.Drawing.Point(70, 261);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(100, 20);
			this.txtPort.TabIndex = 7;
			this.txtPort.Text = "23000";
			// 
			// lblPayload
			// 
			this.lblPayload.AutoSize = true;
			this.lblPayload.Location = new System.Drawing.Point(385, 238);
			this.lblPayload.Name = "lblPayload";
			this.lblPayload.Size = new System.Drawing.Size(48, 13);
			this.lblPayload.TabIndex = 8;
			this.lblPayload.Text = "Payload:";
			// 
			// txtPayload
			// 
			this.txtPayload.Location = new System.Drawing.Point(439, 235);
			this.txtPayload.Name = "txtPayload";
			this.txtPayload.Size = new System.Drawing.Size(127, 20);
			this.txtPayload.TabIndex = 9;
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(572, 233);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(75, 23);
			this.btnSend.TabIndex = 10;
			this.btnSend.Text = "Send";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// lblServerLog
			// 
			this.lblServerLog.AutoSize = true;
			this.lblServerLog.Location = new System.Drawing.Point(13, 12);
			this.lblServerLog.Name = "lblServerLog";
			this.lblServerLog.Size = new System.Drawing.Size(58, 13);
			this.lblServerLog.TabIndex = 11;
			this.lblServerLog.Text = "Server log:";
			// 
			// frmServer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1347, 658);
			this.Controls.Add(this.lblServerLog);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.txtPayload);
			this.Controls.Add(this.lblPayload);
			this.Controls.Add(this.txtPort);
			this.Controls.Add(this.lblPort);
			this.Controls.Add(this.lblClients);
			this.Controls.Add(this.lbClients);
			this.Controls.Add(this.txtServerIP);
			this.Controls.Add(this.lblServerIP);
			this.Controls.Add(this.btnStartServer);
			this.Controls.Add(this.txtServerLog);
			this.Name = "frmServer";
			this.Text = "CopperCat - Server";
			this.Load += new System.EventHandler(this.frmServer_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmServer_Paint);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtServerLog;
		private System.Windows.Forms.Button btnStartServer;
		private System.Windows.Forms.Label lblServerIP;
		private System.Windows.Forms.TextBox txtServerIP;
		private System.Windows.Forms.ListBox lbClients;
		private System.Windows.Forms.Label lblClients;
		private System.Windows.Forms.Label lblPort;
		private System.Windows.Forms.TextBox txtPort;
		private System.Windows.Forms.Label lblPayload;
		private System.Windows.Forms.TextBox txtPayload;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.Label lblServerLog;
	}
}

