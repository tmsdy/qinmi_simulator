using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ChatClient
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
public class Form1 : System.Windows.Forms.Form
{
    private IContainer components = null;
    static IPAddress HostIP = IPAddress.Parse("14.152.107.119"); //14.152.107.119
	private IPEndPoint ChatServer = new IPEndPoint(HostIP, Int32.Parse("3000"));
	private Socket ChatSocket;
	private bool flag=true;
	private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox textBox2;
	private System.Windows.Forms.Button button2;
	private System.Windows.Forms.Button button3;
    private System.Windows.Forms.TextBox textBox3;
	private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.StatusBar statusBar1;
    private System.Timers.Timer RcvPktTimer = new System.Timers.Timer();
    private System.Timers.Timer timer = new System.Timers.Timer(1000);
    private System.Timers.Timer SendPktTimer = new System.Timers.Timer();
    private int g_line = 0;
    private Button button1;
    private bool g_start = false;
    private bool keepalivethread_shouldstop = false;
    private RadioButton radioButton1;
    private RadioButton radioButton2;
    private RadioButton radioButton3;
    private RadioButton radioButton4;
    private RadioButton radioButton5;
    private RadioButton radioButton6;
    private CheckBox checkBox1;
    public System.Threading.Thread thread;
 
	public Form1()
	{
				//
				// Windows 窗体设计器支持所必需的
				//
		InitializeComponent();
        CheckForIllegalCrossThreadCalls = false;


        try
        {
            ChatSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ChatSocket.Connect(ChatServer);
            //statusBar1.Text=textBox1.Text+" has logoned on.Now begin to connect...";

            /*开一个线程去发心跳报文*/
            thread = new Thread(new ThreadStart(keepalivethread));
            //thread.Name= "keepalivethread";
            thread.Start();
            /*
            Thread thread = new Thread(new ThreadStart(ChatProcess));
            thread.Start();
            */
            RcvPktTimer.Elapsed += new System.Timers.ElapsedEventHandler(RcvPkt);
            RcvPktTimer.Interval = 100;
            RcvPktTimer.Enabled = true;
            statusBar1.Text = "连接成功";
   
        }
        catch (Exception ee)
        { statusBar1.Text = ee.Message; }
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
        this.button2 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.textBox3 = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.statusBar1 = new System.Windows.Forms.StatusBar();
        this.button1 = new System.Windows.Forms.Button();
        this.radioButton1 = new System.Windows.Forms.RadioButton();
        this.radioButton2 = new System.Windows.Forms.RadioButton();
        this.radioButton3 = new System.Windows.Forms.RadioButton();
        this.radioButton4 = new System.Windows.Forms.RadioButton();
        this.radioButton5 = new System.Windows.Forms.RadioButton();
        this.radioButton6 = new System.Windows.Forms.RadioButton();
        this.checkBox1 = new System.Windows.Forms.CheckBox();
        this.SuspendLayout();
        // 
        // textBox1
        // 
        this.textBox1.Location = new System.Drawing.Point(182, 371);
        this.textBox1.Multiline = true;
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(87, 24);
        this.textBox1.TabIndex = 1;
        this.textBox1.Text = "SWCP01075#";
        // 
        // textBox2
        // 
        this.textBox2.Location = new System.Drawing.Point(696, 28);
        this.textBox2.Multiline = true;
        this.textBox2.Name = "textBox2";
        this.textBox2.ReadOnly = true;
        this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.textBox2.Size = new System.Drawing.Size(329, 415);
        this.textBox2.TabIndex = 2;
        // 
        // button2
        // 
        this.button2.Location = new System.Drawing.Point(36, 358);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(117, 68);
        this.button2.TabIndex = 4;
        this.button2.Text = "Send";
        this.button2.Click += new System.EventHandler(this.SendBtn_click);
        // 
        // button3
        // 
        this.button3.Location = new System.Drawing.Point(449, 361);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(195, 65);
        this.button3.TabIndex = 5;
        this.button3.Text = "Disconnect";
        this.button3.Click += new System.EventHandler(this.DiscBtn_click);
        // 
        // textBox3
        // 
        this.textBox3.Location = new System.Drawing.Point(8, 67);
        this.textBox3.Multiline = true;
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new System.Drawing.Size(657, 285);
        this.textBox3.TabIndex = 6;
        this.textBox3.Text = "SWAP00111111111111112#";
        this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
        // 
        // label2
        // 
        this.label2.Location = new System.Drawing.Point(694, 9);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(112, 16);
        this.label2.TabIndex = 8;
        this.label2.Text = "Message Received:";
        // 
        // label3
        // 
        this.label3.Location = new System.Drawing.Point(10, 48);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(128, 16);
        this.label3.TabIndex = 9;
        this.label3.Text = "Message to be Sent:";
        // 
        // statusBar1
        // 
        this.statusBar1.Location = new System.Drawing.Point(0, 449);
        this.statusBar1.Name = "statusBar1";
        this.statusBar1.Size = new System.Drawing.Size(1037, 16);
        this.statusBar1.TabIndex = 11;
        this.statusBar1.Text = "Status";
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(300, 371);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(104, 43);
        this.button1.TabIndex = 12;
        this.button1.Text = "下线";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // radioButton1
        // 
        this.radioButton1.AutoSize = true;
        this.radioButton1.Location = new System.Drawing.Point(165, 9);
        this.radioButton1.Name = "radioButton1";
        this.radioButton1.Size = new System.Drawing.Size(41, 16);
        this.radioButton1.TabIndex = 13;
        this.radioButton1.TabStop = true;
        this.radioButton1.Text = "111";
        this.radioButton1.UseVisualStyleBackColor = true;
        this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
        // 
        // radioButton2
        // 
        this.radioButton2.AutoSize = true;
        this.radioButton2.Location = new System.Drawing.Point(266, 9);
        this.radioButton2.Name = "radioButton2";
        this.radioButton2.Size = new System.Drawing.Size(41, 16);
        this.radioButton2.TabIndex = 14;
        this.radioButton2.TabStop = true;
        this.radioButton2.Text = "112";
        this.radioButton2.UseVisualStyleBackColor = true;
        this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
        // 
        // radioButton3
        // 
        this.radioButton3.AutoSize = true;
        this.radioButton3.Location = new System.Drawing.Point(367, 9);
        this.radioButton3.Name = "radioButton3";
        this.radioButton3.Size = new System.Drawing.Size(41, 16);
        this.radioButton3.TabIndex = 15;
        this.radioButton3.TabStop = true;
        this.radioButton3.Text = "113";
        this.radioButton3.UseVisualStyleBackColor = true;
        this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
        // 
        // radioButton4
        // 
        this.radioButton4.AutoSize = true;
        this.radioButton4.Location = new System.Drawing.Point(165, 32);
        this.radioButton4.Name = "radioButton4";
        this.radioButton4.Size = new System.Drawing.Size(41, 16);
        this.radioButton4.TabIndex = 16;
        this.radioButton4.TabStop = true;
        this.radioButton4.Text = "333";
        this.radioButton4.UseVisualStyleBackColor = true;
        this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
        // 
        // radioButton5
        // 
        this.radioButton5.AutoSize = true;
        this.radioButton5.Location = new System.Drawing.Point(267, 32);
        this.radioButton5.Name = "radioButton5";
        this.radioButton5.Size = new System.Drawing.Size(41, 16);
        this.radioButton5.TabIndex = 17;
        this.radioButton5.TabStop = true;
        this.radioButton5.Text = "114";
        this.radioButton5.UseVisualStyleBackColor = true;
        this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
        // 
        // radioButton6
        // 
        this.radioButton6.AutoSize = true;
        this.radioButton6.Location = new System.Drawing.Point(369, 32);
        this.radioButton6.Name = "radioButton6";
        this.radioButton6.Size = new System.Drawing.Size(41, 16);
        this.radioButton6.TabIndex = 18;
        this.radioButton6.TabStop = true;
        this.radioButton6.Text = "115";
        this.radioButton6.UseVisualStyleBackColor = true;
        this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
        // 
        // checkBox1
        // 
        this.checkBox1.AutoSize = true;
        this.checkBox1.Checked = true;
        this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
        this.checkBox1.Location = new System.Drawing.Point(182, 410);
        this.checkBox1.Name = "checkBox1";
        this.checkBox1.Size = new System.Drawing.Size(60, 16);
        this.checkBox1.TabIndex = 19;
        this.checkBox1.Text = "发心跳";
        this.checkBox1.UseVisualStyleBackColor = true;
        this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
        // 
        // Form1
        // 
        this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
        this.ClientSize = new System.Drawing.Size(1037, 465);
        this.Controls.Add(this.checkBox1);
        this.Controls.Add(this.radioButton6);
        this.Controls.Add(this.radioButton5);
        this.Controls.Add(this.radioButton4);
        this.Controls.Add(this.radioButton3);
        this.Controls.Add(this.radioButton2);
        this.Controls.Add(this.radioButton1);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.statusBar1);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.button3);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.textBox2);
        this.Controls.Add(this.textBox1);
        this.Controls.Add(this.textBox3);
        this.Name = "Form1";
        this.Text = "力豪手表模拟登录";
        this.ResumeLayout(false);
        this.PerformLayout();

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

    void RcvPkt(object sender, System.Timers.ElapsedEventArgs e)
    {
        try
        {
            if (ChatSocket.Connected && (flag))
            {

                //statusBar1.Text="Ready for the chatting!";


                Byte[] ReceivedByte = new Byte[256];
                ChatSocket.Receive(ReceivedByte, ReceivedByte.Length, 0);
                //string ReceivedStr=System.Text.Encoding.BigEndianUnicode.GetString(ReceivedByte);
                string ReceivedStr = System.Text.Encoding.ASCII.GetString(ReceivedByte);
                string str = DateTime.Now.ToString("G");
                textBox2.AppendText(str + ": " + ReceivedStr);
                textBox2.AppendText("\r\n");
                // Thread.Sleep(500);

            }
            else 
            {
                /*如果socket断开 则重连*/
                if (!ChatSocket.Connected)
                {
                    textBox2.AppendText("接收报文：由于socket断开，重连" + "\r\n"); 
                    ChatSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    ChatSocket.Connect(ChatServer);
                    RcvPktTimer.Enabled = true;

                }
            }
        }
        catch (Exception ee)
        { textBox2.AppendText( "接收报文 " + ee.Message + "\r\n"); }
    }

    void SendPkt(object sender, System.EventArgs e)
    {
        try
        {
            Byte[] SentByte = new Byte[64];

            /*发心跳*/
            if (!(timer.Enabled))
            {
                timer.Elapsed += new System.Timers.ElapsedEventHandler(keepalive);
                timer.Interval = 1000;
                timer.Enabled = true;
            }

            string[] str = textBox3.Text.Split('\n');
            for (int i = 0; i < str.Length; i++)
            {
                str[i] = str[i].Replace("\r", string.Empty);
                SentByte = System.Text.Encoding.ASCII.GetBytes(str[i].ToCharArray());
                ChatSocket.Send(SentByte, SentByte.Length, 0);
                if (i < (str.Length - 1))
                    Thread.Sleep(3000);
            }


        }
        catch
        { statusBar1.Text = "The connection has not existed!"; }
    }

	private void ChatProcess()
	{
		if(ChatSocket.Connected)
		{
			//statusBar1.Text="Ready for the chatting!";
			while(flag)
			{
				Byte[] ReceivedByte=new Byte[64];
				ChatSocket.Receive(ReceivedByte,ReceivedByte.Length,0);
				//string ReceivedStr=System.Text.Encoding.BigEndianUnicode.GetString(ReceivedByte);
                string ReceivedStr = System.Text.Encoding.ASCII.GetString(ReceivedByte);
				textBox2.AppendText(ReceivedStr+"\r\n");
                Thread.Sleep(500);
			}
		}
	}


	private void SendBtn_click(object sender, System.EventArgs e)
	{
		try
		{
			Byte[] SentByte=new Byte[256];

            /*发心跳*/
            /*
            if (!(timer.Enabled))
            {
                timer.Elapsed += new System.Timers.ElapsedEventHandler(keepalive);
                timer.Interval = 2000;
                timer.Enabled = true;
            }
          */
            /*如果socket断开 则重连*/

            if (!ChatSocket.Connected)
            {
                ChatSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ChatSocket.Connect(ChatServer);


                RcvPktTimer.Enabled = true;

            }

            if (checkBox1.Checked)
            {
                g_start = true;
            }
            else
            {
                g_start = false;
            }

            //textBox2.AppendText("心跳线程状态 " + thread.ThreadState + "\r\n");
            if (ThreadState.Stopped == thread.ThreadState)
            {
                keepalivethread_shouldstop = false;
                thread = new Thread(new ThreadStart(keepalivethread));
                thread.Start(); 
            }


            string[] str = textBox3.Text.Split('\n');
            for (int i = 0; i < str.Length; i++)
            {
                str[i] = str[i].Replace("\r", string.Empty);
                SentByte = System.Text.Encoding.ASCII.GetBytes(str[i].ToCharArray());
                ChatSocket.Send(SentByte, SentByte.Length, 0);
                if (i < (str.Length - 1))
                {

                    EventSleep(10); //10秒发一个位置报文
            
                } 
            }


		}
        catch (Exception ee)
        { textBox2.AppendText(  "发送其它报文 " + ee.Message + "\r\n"); }
	}



    private void keepalivethread()
    {
        try
        {
            while (!keepalivethread_shouldstop)
            {
                if (g_start)
                {
                    Byte[] SentByte = new Byte[64];
                    SentByte = System.Text.Encoding.ASCII.GetBytes(textBox1.Text.ToCharArray());
                    ChatSocket.Send(SentByte, SentByte.Length, 0);
                    //EventSleep(60);  //10秒发一个心跳包
                    Thread.Sleep(60000);
                }
                else
                {
                    EventSleep(1); 
                }
            }
            Thread.CurrentThread.Abort();
        }
        catch (Exception ee)
        { textBox2.AppendText( "发送心跳 " + ee.Message + "\r\n"); }
    }

    void EventSleep(int t)
    {
        int times = t * 1000;
        for (int j = 0; j < times; j += 10)
        {
            Thread.Sleep(10);
            Application.DoEvents();
        }
    }
    void keepalive(object sender, System.Timers.ElapsedEventArgs e)
    {
        try
        {
            Byte[] SentByte = new Byte[64];
            SentByte = System.Text.Encoding.ASCII.GetBytes(textBox1.Text.ToCharArray());
            ChatSocket.Send(SentByte, SentByte.Length, 0);                  

        }
        catch
        { statusBar1.Text = "The connection has not existed!"; }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        try
        {
            string offline = "SWAP20#";
            Byte[] SentByte = new Byte[64];
            SentByte = System.Text.Encoding.ASCII.GetBytes(offline.ToCharArray());
            ChatSocket.Send(SentByte, SentByte.Length, 0);
            g_start = false;
            keepalivethread_shouldstop = true;
            thread.Abort();
            RcvPktTimer.Enabled = false;
            RcvPktTimer.Close();
            ChatSocket.Close();

        }
        catch (Exception ee)
        { textBox2.AppendText("发送离线报文 " + ee.Message + "\r\n"); }
    }


    private void DiscBtn_click(object sender, System.EventArgs e)
    {
        try
        {
            RcvPktTimer.Enabled = false;
            RcvPktTimer.Close();

            g_start = false;
            keepalivethread_shouldstop = true;
            thread.Abort();

            ChatSocket.Close();
            statusBar1.Text = "The connection is disconnected!";
            
            this.Close();
            Application.Exit();
        }
        catch (Exception ee)
        {textBox2.AppendText( "断开连接 " + ee.Message + "\r\n"); }
    }

    private void textBox3_TextChanged(object sender, EventArgs e)
    {

    }

    private void Form1_Close(object sender, EventArgs e)
    {

    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
        textBox3.Text = "SWAP00111111111111111#";
        if (radioButton1.Checked)
        {
            this.Text = "力豪手表模拟登录   当前登录：111";
        }
      
    }

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {
        textBox3.Text = "SWAP00111111111111112#";
        if (radioButton2.Checked)
        {
            this.Text = "力豪手表模拟登录   当前登录：112";
        }

    }

    private void radioButton3_CheckedChanged(object sender, EventArgs e)
    {
        textBox3.Text = "SWAP00111111111111113#";
        if (radioButton3.Checked)
        {
            this.Text = "力豪手表模拟登录   当前登录：113";
        }
    }

    private void radioButton4_CheckedChanged(object sender, EventArgs e)
    {
        textBox3.Text = "SWAP00333333333333333#";
        if (radioButton4.Checked)
        {
            this.Text = "力豪手表模拟登录   当前登录：333";
        }
    }

    private void radioButton5_CheckedChanged(object sender, EventArgs e)
    {
        textBox3.Text = "SWAP00111111111111114#";
        if (radioButton5.Checked)
        {
            this.Text = "力豪手表模拟登录   当前登录：114";
        }
    }

    private void radioButton6_CheckedChanged(object sender, EventArgs e)
    {
        textBox3.Text = "SWAP00111111111111115#";
        if (radioButton6.Checked)
        {
            this.Text = "力豪手表模拟登录   当前登录：115";
        }
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBox1.Checked)
        {
            g_start = true;
        }
        else
        {
            g_start = false;
        }
    }

 




}
}



