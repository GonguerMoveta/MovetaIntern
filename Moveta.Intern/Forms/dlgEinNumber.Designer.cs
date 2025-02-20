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
	
	public partial class dlgEinNumber
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
		public SalDataField dfEin;
		public SalDataField dfFrage;
		public SalPushbutton pbOk;
		protected SalBackgroundText bkgd1;
		#endregion
		
		#region Windows Form Designer generated code
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ToolBar = new PPJ.Runtime.Windows.SalFormToolBar();
			this.ClientArea = new PPJ.Runtime.Windows.SalFormClientArea();
			this.StatusBar = new PPJ.Runtime.Windows.SalFormStatusBar();
			this.pbAbbruch = new PPJ.Runtime.Windows.SalPushbutton();
			this.dfEin = new PPJ.Runtime.Windows.SalDataField();
			this.dfFrage = new PPJ.Runtime.Windows.SalDataField();
			this.pbOk = new PPJ.Runtime.Windows.SalPushbutton();
			this.bkgd1 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.ToolBar.SuspendLayout();
			this.ClientArea.SuspendLayout();
			this.SuspendLayout();
			// 
			// ToolBar
			// 
			this.ToolBar.Name = "ToolBar";
			this.ToolBar.Font = new Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
			this.ToolBar.TabStop = true;
			this.ToolBar.Visible = false;
			this.ToolBar.Controls.Add(this.pbAbbruch);
			// 
			// ClientArea
			// 
			this.ClientArea.Name = "ClientArea";
			this.ClientArea.AutoScroll = false;
			this.ClientArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ClientArea.Controls.Add(this.pbOk);
			this.ClientArea.Controls.Add(this.dfFrage);
			this.ClientArea.Controls.Add(this.dfEin);
			this.ClientArea.Controls.Add(this.bkgd1);
			// 
			// StatusBar
			// 
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.Visible = false;
			// 
			// pbAbbruch
			// 
			this.pbAbbruch.Name = "pbAbbruch";
			this.pbAbbruch.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbAbbruch.AcceleratorKey = Keys.Escape;
			this.pbAbbruch.Text = "Abbruch";
			this.pbAbbruch.Location = new System.Drawing.Point(-25, -42);
			this.pbAbbruch.Size = new System.Drawing.Size(25, 42);
			this.pbAbbruch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbAbbruch.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbAbbruch_WindowActions);
			this.pbAbbruch.TabIndex = 0;
			// 
			// dfEin
			// 
			this.dfEin.Name = "dfEin";
			this.dfEin.BackColor = System.Drawing.Color.White;
			this.dfEin.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfEin.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfEin.Format = "#0";
			this.dfEin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dfEin.Location = new System.Drawing.Point(63, 49);
			this.dfEin.Size = new System.Drawing.Size(80, 30);
			this.dfEin.TabIndex = 0;
			// 
			// dfFrage
			// 
			this.dfFrage.Name = "dfFrage";
			this.dfFrage.BackColor = System.Drawing.SystemColors.Control;
			this.dfFrage.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dfFrage.ReadOnly = true;
			this.dfFrage.Font = new Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular);
			this.dfFrage.Location = new System.Drawing.Point(7, 9);
			this.dfFrage.Size = new System.Drawing.Size(208, 42);
			this.dfFrage.TabIndex = 1;
			// 
			// pbOk
			// 
			this.pbOk.Name = "pbOk";
			this.pbOk.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbOk.AcceleratorKey = Keys.F12;
			this.pbOk.Text = "&Ok";
			this.pbOk.Location = new System.Drawing.Point(63, 99);
			this.pbOk.Size = new System.Drawing.Size(80, 30);
			this.pbOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbOk.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbOk_WindowActions);
			this.pbOk.TabIndex = 2;
			// 
			// bkgd1
			// 
			this.bkgd1.Name = "bkgd1";
			this.bkgd1.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.bkgd1.ForeColor = System.Drawing.Color.Red;
			this.bkgd1.Text = "F12";
			this.bkgd1.Location = new System.Drawing.Point(63, 131);
			this.bkgd1.Size = new System.Drawing.Size(80, 20);
			this.bkgd1.TabIndex = 3;
			// 
			// dlgEinNumber
			// 
			this.Controls.Add(this.ClientArea);
			this.Controls.Add(this.ToolBar);
			this.Controls.Add(this.StatusBar);
			this.Name = "dlgEinNumber";
			this.ClientSize = new System.Drawing.Size(228, 157);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Font = new Font("Tahoma", 12f, System.Drawing.FontStyle.Regular);
			this.Text = "Bitte eingeben :";
			this.Location = new System.Drawing.Point(2, 30);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.dlgEinNumber_WindowActions);
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
			if (disposing && App.dlgEinNumber == this) 
			{
				App.dlgEinNumber = null;
			}
			base.Dispose(disposing);
		}
		#endregion
	}
}
