using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatServer
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private IPAddress HostIP=IPAddress.Parse("127.0.0.1");
		private IPEndPoint ChatServer;
		private Socket ChatSocket;
		private bool flag=true;
		private Socket AcceptedSocket;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.StatusBar statusBar1;

		public Form1()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(88, 16);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(104, 24);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "";
			// 
			// textBox2
			// 
			this.textBox2.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.textBox2.Location = new System.Drawing.Point(8, 64);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox2.Size = new System.Drawing.Size(320, 80);
			this.textBox2.TabIndex = 2;
			this.textBox2.Text = "";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(208, 16);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(64, 24);
			this.button1.TabIndex = 3;
			this.button1.Text = "Login";
			this.button1.Click += new System.EventHandler(this.LoginBtn_click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(48, 280);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(72, 24);
			this.button2.TabIndex = 4;
			this.button2.Text = "Send";
			this.button2.Click += new System.EventHandler(this.SendBtn_click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(200, 280);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(80, 24);
			this.button3.TabIndex = 5;
			this.button3.Text = "Disconnect";
			this.button3.Click += new System.EventHandler(this.DiscBtn_click);
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(8, 176);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(320, 88);
			this.textBox3.TabIndex = 6;
			this.textBox3.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(48, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 24);
			this.label1.TabIndex = 7;
			this.label1.Text = "Name:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Message Received:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = "Message to be Sent:";
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 318);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(336, 16);
			this.statusBar1.TabIndex = 11;
			this.statusBar1.Text = "Status";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(336, 334);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.statusBar1,
																		  this.label3,
																		  this.label2,
																		  this.label1,
																		  this.textBox3,
																		  this.button3,
																		  this.button2,
																		  this.button1,
																		  this.textBox2,
																		  this.textBox1});
			this.Name = "Form1";
			this.Text = "ChatServer";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		

		private void LoginBtn_click(object sender, System.EventArgs e)
		{
			
			HostIP=IPAddress.Parse("127.0.0.1");
			try
			{
				ChatServer=new IPEndPoint(HostIP,Int32.Parse("3000"));
				ChatSocket=new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
				ChatSocket.Bind(ChatServer);
				ChatSocket.Listen(50);
				statusBar1.Text=textBox1.Text+" has logoned on.Now begin to listen...";
				AcceptedSocket=ChatSocket.Accept();
				Thread thread=new Thread(new ThreadStart(ChatProcess));
				thread.Start();

			}
			catch(Exception ee)
			{statusBar1.Text=ee.Message;}

		}

		private void ChatProcess()
		{
			if(AcceptedSocket.Connected)
			{
				statusBar1.Text="Ready for the chatting!";
				while(flag)
				{
					Byte[] ReceivedByte=new Byte[64];
					AcceptedSocket.Receive(ReceivedByte,ReceivedByte.Length,0);
                    string ReceivedStr = System.Text.Encoding.ASCII.GetString(ReceivedByte);
					textBox2.AppendText(ReceivedStr+"\r\n");

				}
			}
		}
		private void SendBtn_click(object sender, System.EventArgs e)
		{
			try
			{
				Byte[] SentByte=new Byte[64];
				string SentStr=textBox3.Text+"\r\n";
				SentByte=System.Text.Encoding.ASCII.GetBytes(SentStr.ToCharArray());
				AcceptedSocket.Send(SentByte,SentByte.Length,0);
				textBox3.Clear();
			}
			catch
			{statusBar1.Text="The connection has not existed!";}
		}

		private void DiscBtn_click(object sender, System.EventArgs e)
		{
			try
			{
				ChatSocket.Close();
				AcceptedSocket.Close();
				statusBar1.Text="The connection is disconnected!";
			}
			catch{statusBar1.Text="The connection has not existed!";}
		}
	}
}
