using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace CopperCatServer
{
	public partial class frmServer : Form
	{
		TcpListener mTCPListener;

		private List<ClientNode> mlClientSocks;

		/////////////////////////////////////////////////////////////////
		Rectangle rect = new Rectangle();
		int gridSize = 64;
		int mapPositionX = 700;
		int mapPositionY = 10;
		SolidBrush brushGround;
		SolidBrush[] brushPlayer;
		int[] playerPosX;
		int[] playerPosY;
		string[] playerActive;
		/////////////////////////////////////////////////////////////////

		public frmServer()
		{
			InitializeComponent();
			mlClientSocks = new List<ClientNode>(2);
			CheckForIllegalCrossThreadCalls = false;
		}

		private void frmServer_Load(object sender, EventArgs e)
		{
			IPAddress ipa = findMyIPV4Address();
			if (ipa != null)
				txtServerIP.Text = ipa.ToString();


			////////////////////////////////////////////////////////////

			brushGround = new SolidBrush(Color.ForestGreen);

			brushPlayer = new SolidBrush[4];
			brushPlayer[0] = new SolidBrush(Color.Red);
			brushPlayer[1] = new SolidBrush(Color.Yellow);
			brushPlayer[2] = new SolidBrush(Color.Blue);
			brushPlayer[3] = new SolidBrush(Color.Orange);

			playerPosX = new int[4];
			playerPosY = new int[4];
			playerPosX[0] = 2;
			playerPosY[0] = 4;
			playerPosX[1] = 5;
			playerPosY[1] = 2;
			playerPosX[2] = 7;
			playerPosY[2] = 5;
			playerPosX[3] = 8;
			playerPosY[3] = 7;

			playerActive = new string[4];
			playerActive[0] = "-1";   // -1: Availible player slot
			playerActive[1] = "-1";   // -1: Availible player slot
			playerActive[2] = "-1";   // -1: Availible player slot
			playerActive[3] = "-1";   // -1: Availible player slot
		}

		private void btnStartServer_Click(object sender, EventArgs e)
		{
			IPAddress ipaddr;
			int nPort;

			if (!int.TryParse(txtPort.Text, out nPort))
			{
				nPort = 23000; // If it could not determine port from value. Setting default port.
			}
			if (!IPAddress.TryParse(txtServerIP.Text, out ipaddr))
			{
				printLog("Invalid IP address supplied.");
				return;
			}

			mTCPListener = new TcpListener(ipaddr, nPort);
			mTCPListener.Start();
			mTCPListener.BeginAcceptTcpClient(onCompleteAcceptTcpClient, mTCPListener);
		}

		void onCompleteAcceptTcpClient(IAsyncResult iar)
		{
			TcpListener tcpl = (TcpListener)iar.AsyncState;
			TcpClient tclient = null;
			ClientNode cNode = null;

			try
			{
				tclient = tcpl.EndAcceptTcpClient(iar);
				printLog("Client connected...");
				tcpl.BeginAcceptTcpClient(onCompleteAcceptTcpClient, tcpl);

				lock (mlClientSocks)
				{
					mlClientSocks.Add((cNode = new ClientNode(tclient, new byte[512], new byte[512], tclient.Client.RemoteEndPoint.ToString())));
					lbClients.Items.Add(cNode.ToString());

					// ADDED BY ME!
					PlayerJoined(cNode.strId);
				}

				tclient.GetStream().BeginRead(cNode.Rx, 0, cNode.Rx.Length, onCompleteReadFromTCPClientStream, tclient);
			}
			catch (Exception exc)
			{
				printLog("Exception: " + exc.Message);
			}
		}

		void onCompleteReadFromTCPClientStream(IAsyncResult iar)
		{
			TcpClient tcpc;
			int nCountReadBytes = 0;
			string strRecv;
			ClientNode cn = null;

			try
			{
				lock(mlClientSocks)
				{
					tcpc = (TcpClient)iar.AsyncState;
					cn = mlClientSocks.Find(x => x.strId == tcpc.Client.RemoteEndPoint.ToString());
					nCountReadBytes = tcpc.GetStream().EndRead(iar);
					if (nCountReadBytes == 0) // This happens when the client has disconnected
					{
						printLog("Client disconnected...");
						mlClientSocks.Remove(cn);
						lbClients.Items.Remove(cn.ToString());

						// ADDED BY ME!
						PlayerLeaved(cn.strId);
						return;
					}

					strRecv = Encoding.ASCII.GetString(cn.Rx, 0, nCountReadBytes).Trim();
					printLog(DateTime.Now + " - " + cn.ToString() + ": " + strRecv);
					NetworkReceive(cn.strId, strRecv);
					cn.Rx = new byte[512];
					tcpc.GetStream().BeginRead(cn.Rx, 0, cn.Rx.Length, onCompleteReadFromTCPClientStream, tcpc);
				}
			}
			catch (Exception ex)
			{
				lock(mlClientSocks)
				{
					printLog("Client disconnected: " + cn.ToString());
					mlClientSocks.Remove(cn);
					lbClients.Items.Remove(cn.ToString());

					// ADDED BY ME!
					PlayerLeaved(cn.strId);
					return;
				}
			}
		}

		public void printLog(string log)
		{
			txtServerLog.Invoke(new Action<string>(doInvokeServerLog), log);
		}

		public void doInvokeServerLog(string log)
		{
			txtServerLog.Text += log + Environment.NewLine;
		}

		private IPAddress findMyIPV4Address()
		{
			string strThisHostName = string.Empty;
			IPHostEntry thisHostDNSEntry = null;
			IPAddress[] allIPsOfThisHost = null;
			IPAddress ipv4Ret = null;

			try
			{
				strThisHostName = System.Net.Dns.GetHostName();                     // Gets the host name of the local computer
				thisHostDNSEntry = System.Net.Dns.GetHostEntry(strThisHostName);    // Resolves a host name or IP address to an IPHostEntry instance
				allIPsOfThisHost = thisHostDNSEntry.AddressList;					// Get all IP addresses of the local computer. Just like ipconfig /all

				// Find the first (preferred) IP address from the list
				for (int idx = allIPsOfThisHost.Length - 1; idx >= 0; idx--)
				{
					if(allIPsOfThisHost[idx].AddressFamily == AddressFamily.InterNetwork)
					{
						ipv4Ret = allIPsOfThisHost[idx];	// Found preferred IP address to use
						break;
					}
				}
			}
			catch(Exception exc)
			{
				printLog("Exception: " + exc);
			}

			return ipv4Ret;
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			NetworkSend(lbClients.SelectedItem.ToString(), txtPayload.Text);

			/*
			if (lbClients.Items.Count <= 0) return;
			if (string.IsNullOrEmpty(txtPayload.Text)) return;

			ClientNode cn = null;

			lock(mlClientSocks)
			{
				cn = mlClientSocks.Find(x => x.strId == lbClients.SelectedItem.ToString());
				cn.Tx = new byte[512];

				try
				{
					if (cn != null)
					{
						if (cn.tclient != null)
						{
							if (cn.tclient.Client.Connected)
							{
								cn.Tx = Encoding.ASCII.GetBytes(txtPayload.Text);
								cn.tclient.GetStream().BeginWrite(cn.Tx, 0, cn.Tx.Length, onCompleteWriteToClientStream, cn.tclient);
							}
						}
					}
				}
				catch (Exception exc)
				{
					printLog("Exception: " + exc.Message);
				}
			}
			*/
		}

		private void NetworkSend(string toAddress, string text)
		{
			if (lbClients.Items.Count <= 0) return;
			if (string.IsNullOrEmpty(text)) return;

			text += "#"; // New line

			ClientNode cn = null;

			lock (mlClientSocks)
			{
				cn = mlClientSocks.Find(x => x.strId == toAddress);
				cn.Tx = new byte[512];

				try
				{
					if (cn != null)
					{
						if (cn.tclient != null)
						{
							if (cn.tclient.Client.Connected)
							{
								cn.Tx = Encoding.ASCII.GetBytes(text);
								cn.tclient.GetStream().BeginWrite(cn.Tx, 0, cn.Tx.Length, onCompleteWriteToClientStream, cn.tclient);
							}
						}
					}
				}
				catch (Exception exc)
				{
					printLog("Exception: " + exc.Message);
				}
			}
		}

		private void NetworkReceive(string fromAddress, string text)
		{
			printLog("[" + fromAddress + "]" + "[" + text + "]"); // DEBUG!!!!!!!!!!!!!!

			/*
			int[] playerPosX;
			int[] playerPosY;
			string[] playerActive;			 
			 */

			/*
			// Player0MoveLeft
			if (text == "Player" + i.ToString() + "MoveLeft")
			{
				playerPosX[i]--;

				for (int j = 0; j < 4; i++)
				{
					if (i != j)
						NetworkSend(mlClientSocks[j], "Player" + i.ToString() + "MoveLeft"); // Player0MoveLeft
				}

				// Update map
				Invalidate();
			}
			*/


			/*
			int fromPlayerSlot = -1;

			if (text.Substring(0, 6) == "Player")
			{
				//int.TryParse(text.Substring(7,1), out fromPlayerSlot);

				string t = text[6].ToString();

				int.TryParse(t, out fromPlayerSlot);

				// Player0MoveLeft
				if (fromPlayerSlot != -1 && text == "Player" + fromPlayerSlot.ToString() + "MoveLeft")
				{
					playerPosX[fromPlayerSlot]--;

					for (int i = 0; i < 4; i++)
					{
						if(i != fromPlayerSlot && playerActive[i] != "-1")
							NetworkSend(mlClientSocks[i].strId, text); // Player0MoveLeft
					}

					// Update map
					Invalidate();
					return;
				}
				
			}
			*/


			int fromPlayerSlot = -1;

			if (text.Substring(0, 6) == "Player")
			{
				string t = text[6].ToString();
				int.TryParse(t, out fromPlayerSlot);

				if (text.Substring(7) == "MoveLeft")
					playerPosX[fromPlayerSlot]--; // Player0MoveLeft
				if (text.Substring(7) == "MoveUp")
					playerPosY[fromPlayerSlot]--; // Player0MoveUp
				if (text.Substring(7) == "MoveRight")
					playerPosX[fromPlayerSlot]++; // Player0MoveRight
				if (text.Substring(7) == "MoveDown")
					playerPosY[fromPlayerSlot]++; // Player0MoveDown

				for (int i = 0; i < 4; i++)
				{
					if (i != fromPlayerSlot && playerActive[i] != "-1")
						NetworkSend(mlClientSocks[i].strId, text);
				}

				// Update map
				Invalidate();
				return;
			}
		}

		private void onCompleteWriteToClientStream(IAsyncResult iar)
		{
			try
			{
				TcpClient tcpc = (TcpClient)iar.AsyncState;
				tcpc.GetStream().EndWrite(iar); ;
			}
			catch (Exception exc)
			{
				printLog("Exception: " + exc.Message);
			}
		}

		private void frmServer_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			// Draw map
			for (int y = 0; y < 10; y++)
			{
				for (int x = 0; x < 10; x++)
				{
					rect.X = mapPositionX + x * gridSize;
					rect.Y = mapPositionY + y * gridSize;
					rect.Width = gridSize;
					rect.Height = gridSize;
					g.FillRectangle(brushGround, rect);
				}
			}

			// Draw players
			for(int i = 0; i < 4; i++)
			{
				if (playerActive[i] != "-1")
				{
					rect.X = mapPositionX + playerPosX[i] * gridSize;
					rect.Y = mapPositionY + playerPosY[i] * gridSize;
					rect.Width = gridSize;
					rect.Height = gridSize;
					g.FillRectangle(brushPlayer[i], rect);
				}
			}
		}

		private void PlayerJoined(string toAddress)
		{
			// Get first availible player slot
			int newPlayerSlot = -1;
			for (int i = 0; i < 4; i++)
			{
				if (playerActive[i] == "-1")
				{
					// Found a player slot. I.e: Player 3 = 2
					newPlayerSlot = i;

					// Update player slot with network address
					playerActive[i] = toAddress;
					break;
				}
			}

			// Send player slot ID to new player
			NetworkSend(toAddress, "PlayerSlot=" + newPlayerSlot.ToString());

			// Inform new player about current players
			for (int i = 0; i < 4; i++)
			{
				if (playerActive[i] != "-1" && i != newPlayerSlot)
				{
					NetworkSend(toAddress, "NewPlayer=" + i.ToString());
					NetworkSend(toAddress, "SetXPlayer" + i.ToString() + "=" + playerPosX[i].ToString());
					NetworkSend(toAddress, "SetYPlayer" + i.ToString() + "=" + playerPosY[i].ToString());
				}
			}

			// Inform players that a new player has joined the game
			foreach (var cli in mlClientSocks)
			{
				NetworkSend(cli.strId, "NewPlayer=" + newPlayerSlot.ToString());
			}

			// Update map
			Invalidate();
		}

		private void PlayerLeaved(string toAddress)
		{
			// Update player slot back to -1. Signaling an availible player slot.
			int quitPlayerSlot = -1;
			for (int i = 0; i < 4; i++)
			{
				if (playerActive[i] == toAddress)
				{
					playerActive[i] = "-1";
					quitPlayerSlot = i;
					break;
				}
			}

			// Inform players that a player has left the game
			foreach (var cli in mlClientSocks)
			{
				NetworkSend(cli.strId, "QuitPlayer=" + quitPlayerSlot.ToString());
			}

			// Update map
			Invalidate();
		}
	}
}
