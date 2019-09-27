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

namespace CopperCatClient
{
	public partial class frmClient : Form
	{
		TcpClient mTcpClient;
		byte[] mRx;

		/////////////////////////////////////////////////////////////////
		Rectangle rect = new Rectangle();
		int gridSize = 64;
		int mapPositionX = 450;
		int mapPositionY = 10;
		int mapSizeWidth = 10;
		int mapSizeHeight = 10;
		SolidBrush brushUnknown;
		SolidBrush brushGround;
		SolidBrush[] brushPlayer;
		int[] playerPosX;
		int[] playerPosY;
		bool[] playerActive;
		int playerSlot = -1;		
		/////////////////////////////////////////////////////////////////


		public frmClient()
		{
			InitializeComponent();
		}

		private void btnConnect_Click(object sender, EventArgs e)
		{
			IPAddress ipa;
			int nPort;

			try
			{
				if (string.IsNullOrEmpty(txtServerIP.Text) || string.IsNullOrEmpty(txtServerPort.Text))
					return;
				if (!IPAddress.TryParse(txtServerIP.Text, out ipa))
				{
					printLog("Please supply an IP Address.");
					return;
				}
				if (!int.TryParse(txtServerPort.Text, out nPort))
				{
					nPort = 23000; // Could not figure out port so setting to default port
				}

				mTcpClient = new TcpClient();
				mTcpClient.BeginConnect(ipa, nPort, onCompleteConnect, mTcpClient);
			}
			catch(Exception exc)
			{
				printLog("Exception: " + exc.Message);
			}
		}

		void onCompleteConnect(IAsyncResult iar)
		{
			TcpClient tcpc;

			try
			{
				tcpc = (TcpClient)iar.AsyncState;
				tcpc.EndConnect(iar);
				printLog("Connected!");
				mRx = new byte[512];
				tcpc.GetStream().BeginRead(mRx, 0, mRx.Length, onCompleteReadFromServerStream, tcpc);
			}
			catch (Exception ex)
			{
				printLog("Exception: " + ex.Message);
			}
		}

		void onCompleteReadFromServerStream(IAsyncResult iar)
		{
			TcpClient tcpc;
			int nCountBytesReceivedFromServer;
			string strReceived;

			try
			{
				tcpc = (TcpClient)iar.AsyncState;
				nCountBytesReceivedFromServer = tcpc.GetStream().EndRead(iar);

				if (nCountBytesReceivedFromServer == 0)
				{
					printLog("Connection lost from server...");

					PlayerDisconnected();
					return;
				}
				strReceived = Encoding.ASCII.GetString(mRx, 0, nCountBytesReceivedFromServer);

				printLog(strReceived);
				NetworkReceived(strReceived);

				mRx = new byte[512];
				tcpc.GetStream().BeginRead(mRx, 0, mRx.Length, onCompleteReadFromServerStream, tcpc);
			}
			catch (System.IO.IOException ex)
			{
				printLog("Server shutdown!");

				PlayerDisconnected();
			}
			catch (Exception exc)
			{
				printLog("Exception: " + exc.Message);
			}
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			NetworkSend(txtPayload.Text);
			/*
			byte[] tx;

			if (string.IsNullOrEmpty(txtPayload.Text)) return;

			try
			{
				tx = Encoding.ASCII.GetBytes(txtPayload.Text);

				if (mTcpClient != null)
				{
					if (mTcpClient.Client.Connected)
					{
						mTcpClient.GetStream().BeginWrite(tx, 0, tx.Length, onCompleteWriteToServer, mTcpClient);
					}
				}
			}
			catch (Exception exc)
			{
				printLog("Exception: " + exc.Message);
			}
			*/
		}

		void NetworkSend(string text)
		{
			byte[] tx;

			if (string.IsNullOrEmpty(text)) return;

			try
			{
				tx = Encoding.ASCII.GetBytes(text);

				if (mTcpClient != null)
				{
					if (mTcpClient.Client.Connected)
					{
						mTcpClient.GetStream().BeginWrite(tx, 0, tx.Length, onCompleteWriteToServer, mTcpClient);
					}
				}
			}
			catch (Exception exc)
			{
				printLog("Exception: " + exc.Message);
			}
		}

		void onCompleteWriteToServer(IAsyncResult iar)
		{
			TcpClient tcpc;

			try
			{
				tcpc = (TcpClient)iar.AsyncState;
				tcpc.GetStream().EndWrite(iar);
			}
			catch (Exception exc)
			{
				printLog("Exception: " + exc.Message);
			}
		}

		public void printLog(string log)
		{
			txtClientLog.Invoke(new Action<string>(doInvokeClientLog), log);
		}

		public void doInvokeClientLog(string log)
		{
			string[] splitLog;

			// Convert from one line seperator to multiple lines. Needed due to network may not flush very small packets
			splitLog = log.Split('#');

			for (int i = 0; i < splitLog.Count(); i++)
			{
				if (splitLog[i] != "")
					txtClientLog.Text += splitLog[i] + Environment.NewLine;
			}
		}

		private void frmClient_Load(object sender, EventArgs e)
		{
			////////////////////////////////////////////////////////////

			brushUnknown = new SolidBrush(Color.DarkGray);
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

			playerActive = new bool[4];
			playerActive[0] = false;   // -1: Availible player slot
			playerActive[1] = false;   // -1: Availible player slot
			playerActive[2] = false;   // -1: Availible player slot
			playerActive[3] = false;   // -1: Availible player slot
		}

		private void frmClient_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			// Draw map
			for (int y = 0; y < mapSizeHeight; y++)
			{
				for (int x = 0; x < mapSizeWidth; x++)
				{
					rect.X = mapPositionX + x * gridSize;
					rect.Y = mapPositionY + y * gridSize;
					rect.Width = gridSize;
					rect.Height = gridSize;
					if (playerSlot == -1)
						g.FillRectangle(brushUnknown, rect);
					else
						g.FillRectangle(brushGround, rect);
				}
			}

			// Draw players
			for (int i = 0; i < 4; i++)
			{
				if (playerActive[i])
				{
					rect.X = mapPositionX + playerPosX[i] * gridSize;
					rect.Y = mapPositionY + playerPosY[i] * gridSize;
					rect.Width = gridSize;
					rect.Height = gridSize;
					g.FillRectangle(brushPlayer[i], rect);
				}
			}
		}

		// This player disconnected, reset map data
		private void PlayerDisconnected()
		{
			playerPosX[0] = 2;
			playerPosY[0] = 4;
			playerPosX[1] = 5;
			playerPosY[1] = 2;
			playerPosX[2] = 7;
			playerPosY[2] = 5;
			playerPosX[3] = 8;
			playerPosY[3] = 7;

			playerActive[0] = false;
			playerActive[1] = false;
			playerActive[2] = false;
			playerActive[3] = false;

			playerSlot = -1;

			// Update map
			Invalidate();
		}

		private void NetworkReceived(string text)
		{
			string[] receivedLines;

			// Convert from one line seperator to multiple lines. Needed due to network may not flush very small packets
			receivedLines = text.Split('#');

			for (int i = 0; i < receivedLines.Count(); i++)
			{
				// TODO: Rewrite this crude method
				if (receivedLines[i] == "PlayerSlot=0")
					playerSlot = 0;
				if (receivedLines[i] == "PlayerSlot=1")
					playerSlot = 1;
				if (receivedLines[i] == "PlayerSlot=2")
					playerSlot = 2;
				if (receivedLines[i] == "PlayerSlot=3")
					playerSlot = 3;

				// TODO: Rewrite this crude method
				if (receivedLines[i] == "NewPlayer=0")
					playerActive[0] = true;
				if (receivedLines[i] == "NewPlayer=1")
					playerActive[1] = true;
				if (receivedLines[i] == "NewPlayer=2")
					playerActive[2] = true;
				if (receivedLines[i] == "NewPlayer=3")
					playerActive[3] = true;

				// TODO: Rewrite this crude method
				if (receivedLines[i] == "QuitPlayer=0")
					playerActive[0] = false;
				if (receivedLines[i] == "QuitPlayer=1")
					playerActive[1] = false;
				if (receivedLines[i] == "QuitPlayer=2")
					playerActive[2] = false;
				if (receivedLines[i] == "QuitPlayer=3")
					playerActive[3] = false;


				// TODO: Rewrite this crude method
				//0
				if (receivedLines[i] == "Player0MoveLeft")
					playerPosX[0]--;
				if (receivedLines[i] == "Player0MoveUp")
					playerPosY[0]--;
				if (receivedLines[i] == "Player0MoveRight")
					playerPosX[0]++;
				if (receivedLines[i] == "Player0MoveDown")
					playerPosY[0]++;
				//1
				if (receivedLines[i] == "Player1MoveLeft")
					playerPosX[1]--;
				if (receivedLines[i] == "Player1MoveUp")
					playerPosY[1]--;
				if (receivedLines[i] == "Player1MoveRight")
					playerPosX[1]++;
				if (receivedLines[i] == "Player1MoveDown")
					playerPosY[1]++;
				//2
				if (receivedLines[i] == "Player2MoveLeft")
					playerPosX[2]--;
				if (receivedLines[i] == "Player2MoveUp")
					playerPosY[2]--;
				if (receivedLines[i] == "Player2MoveRight")
					playerPosX[2]++;
				if (receivedLines[i] == "Player2MoveDown")
					playerPosY[2]++;
				//3
				if (receivedLines[i] == "Player3MoveLeft")
					playerPosX[3]--;
				if (receivedLines[i] == "Player3MoveUp")
					playerPosY[3]--;
				if (receivedLines[i] == "Player3MoveRight")
					playerPosX[3]++;
				if (receivedLines[i] == "Player3MoveDown")
					playerPosY[3]++;


				// TODO: Rewrite this crude method
				if (receivedLines[i].Length > 12)
				{
					int tint = 0;
					//0
					if (receivedLines[i].Substring(0, 12) == "SetXPlayer0=")
					{
						if(int.TryParse(receivedLines[i].Substring(12), out tint))
							playerPosX[0] = tint;
					}
					if (receivedLines[i].Substring(0, 12) == "SetYPlayer0=")
					{
						if (int.TryParse(receivedLines[i].Substring(12), out tint))
							playerPosY[0] = tint;
					}
					//1
					if (receivedLines[i].Substring(0, 12) == "SetXPlayer1=")
					{
						if (int.TryParse(receivedLines[i].Substring(12), out tint))
							playerPosX[1] = tint;
					}
					if (receivedLines[i].Substring(0, 12) == "SetYPlayer1=")
					{
						if (int.TryParse(receivedLines[i].Substring(12), out tint))
							playerPosY[1] = tint;
					}
					//2
					if (receivedLines[i].Substring(0, 12) == "SetXPlayer2=")
					{
						if (int.TryParse(receivedLines[i].Substring(12), out tint))
							playerPosX[2] = tint;
					}
					if (receivedLines[i].Substring(0, 12) == "SetYPlayer2=")
					{
						if (int.TryParse(receivedLines[i].Substring(12), out tint))
							playerPosY[2] = tint;
					}
					//3
					if (receivedLines[i].Substring(0, 12) == "SetXPlayer3=")
					{
						if (int.TryParse(receivedLines[i].Substring(12), out tint))
							playerPosX[3] = tint;
					}
					if (receivedLines[i].Substring(0, 12) == "SetYPlayer3=")
					{
						if (int.TryParse(receivedLines[i].Substring(12), out tint))
							playerPosY[3] = tint;
					}

				}


			}

			// Update map - VERY CRUDE SOLUTION. TODO: Need a better way to refresh screen/window
			Invalidate();
		}

		/*
		int[] playerPosX;
		int[] playerPosY;
		bool[] playerActive;
		int playerSlot = -1;			 
		*/

		private void btnMoveLeft_Click(object sender, EventArgs e)
		{
			if (playerPosX[playerSlot] == 0) return;

			playerPosX[playerSlot]--;
			NetworkSend("Player" + playerSlot + "MoveLeft"); // Player0MoveLeft
						
			// Update map
			Invalidate();
		}

		private void btnMoveUp_Click(object sender, EventArgs e)
		{
			if (playerPosY[playerSlot] == 0) return;

			playerPosY[playerSlot]--;
			NetworkSend("Player" + playerSlot + "MoveUp"); // Player0MoveUp

			// Update map
			Invalidate();
		}

		private void btnMoveRight_Click(object sender, EventArgs e)
		{
			if (playerPosX[playerSlot] == mapSizeWidth - 1) return;

			playerPosX[playerSlot]++;
			NetworkSend("Player" + playerSlot + "MoveRight"); // Player0MoveRight

			// Update map
			Invalidate();
		}

		private void btnMoveDown_Click(object sender, EventArgs e)
		{
			if (playerPosY[playerSlot] == mapSizeHeight - 1) return;

			playerPosY[playerSlot]++;
			NetworkSend("Player" + playerSlot + "MoveDown"); // Player0MoveDown

			// Update map
			Invalidate();
		}
	}
}
