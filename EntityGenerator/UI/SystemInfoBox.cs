using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace EntityGenerator.UI
{
    public partial class SystemInfoBox : Form
    {
        /// <summary>
        /// 构造方法.
        /// </summary>
        /// <param name="header">对话框的标题</param>
        /// <param name="info">对话框的信息内容</param>
        public SystemInfoBox(string header,string info)
        {
            InitializeComponent();
            this.Text = header;
            this.lblMessage.Text = info;
        }

        /// <summary>
        /// 显示模式对话框.
        /// </summary>
        public void ShowModelDialog()
        {
            Thread obj = new Thread(new ThreadStart(ThreadBody));
            obj.Start();
            Thread.Sleep(5000);
            obj.Abort();
        }

        /// <summary>
        /// '确定'按钮的事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.Close();
        }      

        /// <summary>
        /// 本方法表示线程体.
        /// </summary>
        private void ThreadBody()
        {
            this.ShowDialog();            
            this.Close();            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}