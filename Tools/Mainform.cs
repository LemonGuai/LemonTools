using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Services;

namespace Tools
{
	public partial class Mainform : DevExpress.XtraEditors.XtraForm
	{
		public Mainform()
		{
			InitializeComponent();
		}
		#region 事件方法
		private void Mainform_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
			{
				this.btnOption.PerformClick();
			}
		}
		private void btnOption_Click(object sender, EventArgs e)
		{
			if (this.btnOption.Text == "暂停")
			{
				this.timer1.Stop();
				this.tbMinutes.Enabled = true;
				this.btnOption.Text = "开始";
				return;
			}
			if (this.labelTime.Text == "倒计时剩余时间: ")
			{
				GetTime();
				TimeShow();
			}
			this.timer1.Start();
			this.tbMinutes.Enabled = false;
			this.btnOption.Text = "暂停";
		}

		private void Mainform_Load(object sender, EventArgs e)
		{
			//this.labelTime.Text = "倒计时剩余时间: ";
			GetTime();
			TimeShow();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			TimeShow();
			this.m_Time -= 1;
			if (this.m_Time < 0)
			{
				this.timer1.Stop();
				this.tbMinutes.Enabled = true;
				this.btnOption.Text = "开始";
			}
		}
		private void btnReset_Click(object sender, EventArgs e)
		{
			if (this.timer1.Enabled)
			{
				XtraMessageBox.Show("倒计时进行中, 请先暂定当前倒计时! ", "小工具", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			GetTime();
			TimeShow();
		}
		private void tbMinutes_MouseClick(object sender, MouseEventArgs e)
		{
			if (this.timer1.Enabled)
			{
				XtraMessageBox.Show("倒计时进行中, 请先暂定当前倒计时! ", "小工具", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
		}
		private void tbMinutes_TextChanged(object sender, EventArgs e)
		{
			if (this.timer1.Enabled)
			{
				XtraMessageBox.Show("倒计时进行中, 请先暂定当前倒计时! ", "小工具", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			GetTime();
			TimeShow();
		}
		#endregion

		#region 私有变量
		/// <summary>
		/// 存放倒计时时间
		/// </summary>
		private double m_Time = 0.00;
		#endregion

		#region 私有方法
		/// <summary>
		/// 倒计时
		/// </summary>
		/// <param name="time">秒数</param>
		private void TimeShow()
		{
			if (this.m_Time <= 30)
			{
				this.labelTime.ForeColor = Color.Red;
			}
			double minutes = Math.Floor(this.m_Time / 60);
			double seconds = this.m_Time % 60;
			this.labelTime.Text = $"倒计时剩余时间: {minutes}分{seconds}秒";
		}
		/// <summary>
		/// 获取倒计时时间
		/// </summary>
		private void GetTime()
		{
			if (this.tbMinutes.Text.Length <= 0)
			{
				XtraMessageBox.Show("请输入倒计时时间", "小工具", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				double time = 0;
				if (!double.TryParse(this.tbMinutes.Text, out time))
				{
					XtraMessageBox.Show("请输入正确的时间", "小工具", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else
				{
					this.timer1.Interval = 1000;
					this.m_Time = time * 60;
				}
			}
		}


		#endregion


	}
}