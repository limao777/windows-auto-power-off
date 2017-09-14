using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace aotu_poweroff
{
    public partial class Form1 : Form
    {

        public string cmd(string dosCommand)
        {
            return cmd(dosCommand, 30000);
        }
        /// <summary>
        /// 执行DOS命令，返回DOS命令的输出
        /// </summary>
        /// <param name="dosCommand">dos命令</param>
        /// <param name="milliseconds">等待命令执行的时间（单位：毫秒），
        /// 如果设定为0，则无限等待</param>
        /// <returns>返回DOS命令的输出</returns>
        public static string cmd(string command, int seconds)
        {
            string output = ""; //输出字符串
            if (command != null && !command.Equals(""))
            {
                Process process = new Process();//创建进程对象
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令
                startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动
                startInfo.RedirectStandardInput = false;//不重定向输入
                startInfo.RedirectStandardOutput = true; //重定向输出
                startInfo.CreateNoWindow = true;//不创建窗口
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())//开始进程
                    {
                        if (seconds == 0)
                        {
                            process.WaitForExit();//这里无限等待进程结束
                        }
                        else
                        {
                            process.WaitForExit(seconds); //等待进程结束，等待时间为指定的毫秒
                        }
                        output = process.StandardOutput.ReadToEnd();//读取进程的输出
                    }
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
            }
            return output;
        }


        public Form1(string ss)
        {
            InitializeComponent(); sss = ss;
        }
            int set_hour=18;
            int set_min=2;
            string sss;
        int sign15=0,sign10=0,sign5=0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            int hour = dt.Hour;
            int min = dt.Minute;
           
            int shengxia = (set_hour - hour) * 60 + set_min - min;

            lb4.Text = "距离自动关机还有" + shengxia + "分钟";
            notifyIcon1.Text = "距离自动关机还有" + shengxia + "分钟";

            if (shengxia < 16&&sign15==0) {
                notifyIcon1.Visible = false;  //托盘图标隐藏
                this.WindowState = FormWindowState.Normal;
                this.Show();
                this.ShowInTaskbar = true;
                sign15 = 1;
            }
            else if (shengxia < 11&&sign10==0) {
                notifyIcon1.Visible = false;  //托盘图标隐藏
                this.WindowState = FormWindowState.Normal;
                this.Show();
                this.ShowInTaskbar = true;
                sign10 = 1;
            }
            else if (shengxia < 6&&sign5==0)
            {
                 notifyIcon1.Visible = false;  //托盘图标隐藏
                this.WindowState = FormWindowState.Normal;
                this.Show();
                this.ShowInTaskbar = true;
                sign5 = 1;
              
            }
            else if(shengxia ==1){             
                notifyIcon1.Visible = false;  //托盘图标隐藏
                this.WindowState = FormWindowState.Normal;
                this.Show();
                this.ShowInTaskbar = true;}

            else if (shengxia == 0) {// cmd("shutdown -s /t 300");
             cmd("shutdown -s /t 10");
             timer1.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (sss == "s")
            {
                this.Hide(); 
                this.ShowInTaskbar = false;  //不显示在系统任务栏
                notifyIcon1.Visible = true;  //托盘图标可见
                this.notifyIcon1.ShowBalloonTip(500, "提示", "自动关机小软件运行中...", ToolTipIcon.Info);
            }

            Shortcut sc = new Shortcut();
            //  sc.Path = "目标文件地址";
            // sc.Arguments = "启动参数";
            // sc.WorkingDirectory = "启动文件的文件夹";
            //sc.Description = "描述";
            //sc.Save("这个会计方式保存在哪");
            sc.Path = System.Environment.CurrentDirectory + @"\aotu_poweroff.exe";
            sc.Arguments = "s";
            sc.WorkingDirectory = System.Environment.CurrentDirectory;
            sc.Description = "aotu_poweroff";
            sc.Save(@"C:\Users\" + System.Environment.MachineName.ToLower() + @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\aotu_poweroff.lnk");

        }

        private void button1_Click(object sender, EventArgs e)
        {
             set_hour = Convert.ToInt32(tb1.Text);
            set_min = Convert.ToInt32(tb2.Text);
            lb2.Text="时间已设定为："+set_hour+"时"+set_min+"分";
        }

        private void dclick(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;  //托盘图标隐藏
            this.WindowState = FormWindowState.Normal;
            this.Show();
            this.ShowInTaskbar = true;
        }

        private void Form1_size_changed(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)  //判断是否最小化
            {

                this.ShowInTaskbar = false;  //不显示在系统任务栏
                notifyIcon1.Visible = true;  //托盘图标可见
                this.notifyIcon1.ShowBalloonTip(500, "提示", "自动关机小软件运行中...", ToolTipIcon.Info);
            }
        }

        private void chk_changed(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Shortcut sc = new Shortcut();
                //  sc.Path = "目标文件地址";
                // sc.Arguments = "启动参数";
                // sc.WorkingDirectory = "启动文件的文件夹";
                //sc.Description = "描述";
                //sc.Save("这个会计方式保存在哪");
                sc.Path = System.Environment.CurrentDirectory + @"\aotu_poweroff.exe";
                sc.Arguments = "s";
                sc.WorkingDirectory = System.Environment.CurrentDirectory;
                sc.Description = "aotu_poweroff";
                sc.Save(@"C:\Users\" + System.Environment.MachineName.ToLower() + @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\aotu_poweroff.lnk");
            }
            else {
                FileInfo file2 = new FileInfo(@"C:\Users\" + System.Environment.MachineName.ToLower() + @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\aotu_poweroff.lnk");
                if (file2.Exists)
                {
                    file2.Delete(); //删除单个文件
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            //   string versionStr = String.Format("{0}.{1}", fvi.ProductMajorPart, fvi.ProductMinorPart);
            string versionStr = String.Format("{0}", fvi.FileVersion);

            MessageBox.Show("版本号：" + versionStr + " ╮(╯﹏╰）╭\r\n获取最新程序：http://cc00559.csec.corp/", "关于");
        }
    }
}
