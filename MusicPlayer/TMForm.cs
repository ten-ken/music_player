﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*作者：向往那片天空
 *联系邮箱：250023777@qq.com
 *打个广告：精品女装https://shop130840104.taobao.com，大家支持一下。^-^^-^^-^^-^
 *由于精力问题，代码只注释了一部分，大家有哪里不懂的，就邮箱或者QQ联系我吧
 */

namespace KenMusicPlayer
{    
    public partial class TMForm : Form
    {

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x01;
        private const int HTCAPTION = 0x02;
        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_MAXMIZE = 0xF030;
        private const int WM_NCLBUTTONDBLCLK = 0xA3;
        private const int WM_MOUSEMOVE = 0x200;

        //字体大小
        private int fontSize = 11;


        /// <summary>
        /// 重写窗口消息处理函数，就是为了实现鼠标拖动窗体和禁止双击最大化
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_MOUSEMOVE:
                    base.WndProc(ref m);
                    this.Focus();
                    break;
                case 0x4e:
                case 0xd:
                case 0xe:
                case 0x14:
                    base.WndProc(ref m);
                    break;
                case WM_NCHITTEST://鼠标点任意位置后可以拖动窗体
                    base.WndProc(ref m);
                    if (m.Result.ToInt32() == HTCLIENT)
                    {
                        m.Result = new IntPtr(HTCAPTION);
                        return;
                    }
                    break;
                case WM_NCLBUTTONDBLCLK://禁止双击最大化
                    //base.WndProc(ref m);
                    Console.WriteLine(this.WindowState);
                    return;
                    break;

                default:

                    base.WndProc(ref m);
                    break;
            }
        }

        public TMForm()
        {         
            InitializeComponent();
            //this.button1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MusicPlayer mainForm = (MusicPlayer)this.Owner;
            mainForm.setWord();
            this.Dispose();
            
        }

        /// <summary>
        /// 透明歌词
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmLrc_Move(object sender, EventArgs e)
        {
            MusicPlayer mainForm = (MusicPlayer)this.Owner;
            if (mainForm != null)
            {
                Point pos = this.Location;
                pos.Y = mainForm.Location.Y + mainForm.Height;
                //this.Location 
                mainForm.moveTmform(pos);
            }
            //this.label1.Text = 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MusicPlayer mainForm = (MusicPlayer)this.Owner;
            //获得当前鼠标位置   
            Point pt = new Point(Form.MousePosition.X, Form.MousePosition.Y);
            //判断鼠标是否在窗体内   
            if (this.Bounds.Contains(pt))
            {
                //this.button1.Visible = true;
                mainForm.showTmform(true);
                //this.TopMost = true;
            }
            else
            {
                //this.button1.Visible = false;
                mainForm.showTmform(false);
                //this.TopMost = true;
            }
        }

        public void setLrc(string l1, string l2, int pos) 
        {
            if ((pos % 2) == 0)
            {
                this.label1.Text = l1;
                this.label2.Text = l2;
                this.label1.ForeColor = Color.OrangeRed;
                this.label2.ForeColor = Color.Black;
            }
            else
            {
                this.label1.Text = l2;
                this.label2.Text = l1;
                this.label1.ForeColor = Color.Black;
                this.label2.ForeColor = Color.OrangeRed;
            }
        }

        private void TMForm_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 改变字体样式
        /// </summary>
        /// <param name="fontName"></param>
        /// <param name="fontSize"></param>
        internal void ChangeLabelFont(string fontName, string fontSizeStr)
        {
            float fontSize = string.IsNullOrEmpty(fontSizeStr) ? this.Font.Size : Convert.ToInt32(fontSizeStr);

            Font font1 =new Font(fontName, fontSize, this.label1.Font.Style);
            this.label1.Font = font1;

            Font font2 =new Font(fontName, fontSize, this.label2.Font.Style);
            this.label2.Font = font2;
        }
    }
}
