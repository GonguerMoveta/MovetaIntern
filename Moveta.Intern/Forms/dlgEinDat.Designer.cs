// <ppj name="Moveta.Intern" date="1/29/2024 3:39:48 AM" id="F4EC85BAD2BF79AC25C9F8643540E90F9BE1DAF0"/>
// ======================================================================================================
// This code was generated by the Ice Porter(tm) Tool version 4.8.15.0
// Ice Porter is part of The Porting Project (PPJ) by Ice Tea Group, LLC.
// The generated code is not guaranteed to be accurate and to compile without
// manual modifications.
// 
// ICE TEA GROUP LLC SHALL IN NO EVENT BE LIABLE FOR ANY DAMAGES WHATSOEVER
// (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF BUSINESS PROFITS, BUSINESS
// INTERRUPTION, LOSS OF BUSINESS INFORMATION, OR ANY OTHER LOSS OF ANY KIND)
// ARISING OUT OF THE USE OR INABILITY TO USE THE GENERATED CODE, WHETHER
// DIRECT, INDIRECT, INCIDENTAL, CONSEQUENTIAL, SPECIAL OR OTHERWISE, REGARDLESS
// OF THE FORM OF ACTION, EVEN IF ICE TEA GROUP LLC HAS BEEN ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGES.
// =====================================================================================================
using System;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using MT;
using PPJ.Runtime;
using PPJ.Runtime.Com;
using PPJ.Runtime.Sql;
using PPJ.Runtime.Vis;
using PPJ.Runtime.Windows;
using PPJ.Runtime.Windows.QO;
using PPJ.Runtime.XSal;

namespace Moveta.Intern
{
	
	public partial class dlgEinDat
	{
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		#region Window Accessories
		
		/// <summary>
		/// Toolbar control.
		/// </summary>
		protected SalFormToolBar ToolBar;
		
		/// <summary>
		/// Client area panel.
		/// </summary>
		protected SalFormClientArea ClientArea;
		
		/// <summary>
		/// StatusBar control.
		/// </summary>
		protected SalFormStatusBar StatusBar;
		#endregion
		
		
		#region Window Controls
		public SalPushbutton pbAbbruch;
		public VisCalendarDropDown dfEin1;
		public VisCalendarDropDown dfEin2;
		public SalDataField dfFrage;
		public SalPushbutton pbOk;
		protected SalBackgroundText bkgd1;
		protected SalBackgroundText bkgd2;
		#endregion
		
		#region Windows Form Designer generated code
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.ToolBar = new PPJ.Runtime.Windows.SalFormToolBar();
            this.pbAbbruch = new PPJ.Runtime.Windows.SalPushbutton();
            this.ClientArea = new PPJ.Runtime.Windows.SalFormClientArea();
            this.pbOk = new PPJ.Runtime.Windows.SalPushbutton();
            this.dfFrage = new PPJ.Runtime.Windows.SalDataField();
            this.dfEin1 = new PPJ.Runtime.Vis.VisCalendarDropDown();
            this.dfEin2 = new PPJ.Runtime.Vis.VisCalendarDropDown();
            this.bkgd2 = new PPJ.Runtime.Windows.SalBackgroundText();
            this.bkgd1 = new PPJ.Runtime.Windows.SalBackgroundText();
            this.StatusBar = new PPJ.Runtime.Windows.SalFormStatusBar();
            this.ToolBar.SuspendLayout();
            this.ClientArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolBar
            // 
            this.ToolBar.Controls.Add(this.pbAbbruch);
            this.ToolBar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.Size = new System.Drawing.Size(371, 60);
            this.ToolBar.TabIndex = 1;
            this.ToolBar.TabStop = true;
            this.ToolBar.Visible = false;
            // 
            // pbAbbruch
            // 
            this.pbAbbruch.AcceleratorKey = System.Windows.Forms.Keys.Escape;
            this.pbAbbruch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.pbAbbruch.Location = new System.Drawing.Point(-25, -42);
            this.pbAbbruch.Name = "pbAbbruch";
            this.pbAbbruch.Size = new System.Drawing.Size(25, 42);
            this.pbAbbruch.TabIndex = 0;
            this.pbAbbruch.Text = "Abbruch";
            this.pbAbbruch.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbAbbruch_WindowActions);
            // 
            // ClientArea
            // 
            this.ClientArea.AutoScroll = false;
            this.ClientArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ClientArea.Controls.Add(this.pbOk);
            this.ClientArea.Controls.Add(this.dfFrage);
            this.ClientArea.Controls.Add(this.dfEin1);
            this.ClientArea.Controls.Add(this.dfEin2);
            this.ClientArea.Controls.Add(this.bkgd2);
            this.ClientArea.Controls.Add(this.bkgd1);
            this.ClientArea.Location = new System.Drawing.Point(0, 60);
            this.ClientArea.Name = "ClientArea";
            this.ClientArea.Size = new System.Drawing.Size(371, 107);
            this.ClientArea.TabIndex = 0;
            // 
            // pbOk
            // 
            this.pbOk.AcceleratorKey = System.Windows.Forms.Keys.F12;
            this.pbOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.pbOk.Location = new System.Drawing.Point(135, 99);
            this.pbOk.Name = "pbOk";
            this.pbOk.Size = new System.Drawing.Size(80, 30);
            this.pbOk.TabIndex = 3;
            this.pbOk.Text = "&Ok";
            this.pbOk.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbOk_WindowActions);
            // 
            // dfFrage
            // 
            this.dfFrage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dfFrage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dfFrage.Location = new System.Drawing.Point(7, 9);
            this.dfFrage.Name = "dfFrage";
            this.dfFrage.ReadOnly = true;
            this.dfFrage.Size = new System.Drawing.Size(208, 42);
            this.dfFrage.TabIndex = 2;
            // 
            // dfEin1
            // 
            this.dfEin1.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.dfEin1.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dfEin1.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(233)))), ((int)(((byte)(235)))));
            this.dfEin1.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.dfEin1.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.dfEin1.Format = null;
            this.dfEin1.Location = new System.Drawing.Point(14, 51);
            this.dfEin1.Name = "dfEin1";
            this.dfEin1.Size = new System.Drawing.Size(132, 27);
            this.dfEin1.TabIndex = 1;
            this.dfEin1.Text = "1/29/2024";
            this.dfEin1.Value = new PPJ.Runtime.SalDateTime(2024, 1, 29, 4, 45, 26, 948);
            // 
            // dfEin2
            // 
            this.dfEin2.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.dfEin2.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dfEin2.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(233)))), ((int)(((byte)(235)))));
            this.dfEin2.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.dfEin2.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.dfEin2.Format = null;
            this.dfEin2.Location = new System.Drawing.Point(216, 51);
            this.dfEin2.Name = "dfEin2";
            this.dfEin2.Size = new System.Drawing.Size(132, 27);
            this.dfEin2.TabIndex = 0;
            this.dfEin2.Text = "1/29/2024";
            this.dfEin2.Value = new PPJ.Runtime.SalDateTime(2024, 1, 29, 4, 45, 26, 951);
            // 
            // bkgd2
            // 
            this.bkgd2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.bkgd2.Location = new System.Drawing.Point(183, 61);
            this.bkgd2.Name = "bkgd2";
            this.bkgd2.Size = new System.Drawing.Size(32, 20);
            this.bkgd2.TabIndex = 5;
            this.bkgd2.Text = "bis";
            // 
            // bkgd1
            // 
            this.bkgd1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.bkgd1.ForeColor = System.Drawing.Color.Red;
            this.bkgd1.Location = new System.Drawing.Point(135, 131);
            this.bkgd1.Name = "bkgd1";
            this.bkgd1.Size = new System.Drawing.Size(80, 20);
            this.bkgd1.TabIndex = 4;
            this.bkgd1.Text = "F12";
            this.bkgd1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(0, 167);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.ShowKeyboardStatus = false;
            this.StatusBar.Size = new System.Drawing.Size(371, 22);
            this.StatusBar.TabIndex = 2;
            this.StatusBar.Visible = false;
            // 
            // dlgEinDat
            // 
            this.ClientSize = new System.Drawing.Size(371, 189);
            this.Controls.Add(this.ClientArea);
            this.Controls.Add(this.ToolBar);
            this.Controls.Add(this.StatusBar);
            this.Font = new System.Drawing.Font("Tahoma", 12F);
            this.Location = new System.Drawing.Point(2, 30);
            this.Name = "dlgEinDat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Bitte eingeben :";
            this.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.dlgEinDat_WindowActions);
            this.ToolBar.ResumeLayout(false);
            this.ClientArea.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		
		#region System Methods/Properties
		
		/// <summary>
		/// Release global reference.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null) 
			{
				components.Dispose();
			}
			if (disposing && App.dlgEinDat == this) 
			{
				App.dlgEinDat = null;
			}
			base.Dispose(disposing);
		}
		#endregion
	}
}
