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
	
	public partial class dlgMS
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
		protected SalBackgroundText bkgd4;
		public SalDataField dfMS;
		public SalPushbutton pbOk;
		protected SalBackgroundText bkgd5;
		protected SalBackgroundText bkgd6;
		public SalDataField dfMSbis;
		protected SalBackgroundText bkgd7;
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
			this.bkgd4 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.dfMS = new PPJ.Runtime.Windows.SalDataField();
			this.pbOk = new PPJ.Runtime.Windows.SalPushbutton();
			this.bkgd5 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.bkgd6 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.dfMSbis = new PPJ.Runtime.Windows.SalDataField();
			this.bkgd7 = new PPJ.Runtime.Windows.SalBackgroundText();
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
			this.ClientArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ClientArea.Controls.Add(this.dfMSbis);
			this.ClientArea.Controls.Add(this.pbOk);
			this.ClientArea.Controls.Add(this.dfMS);
			this.ClientArea.Controls.Add(this.bkgd7);
			this.ClientArea.Controls.Add(this.bkgd6);
			this.ClientArea.Controls.Add(this.bkgd5);
			this.ClientArea.Controls.Add(this.bkgd4);
			// 
			// StatusBar
			// 
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.Visible = true;
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
			// bkgd4
			// 
			this.bkgd4.Name = "bkgd4";
			this.bkgd4.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd4.Text = "Für welche Mahnstufe soll die OP-Liste gedruckt werden ?";
			this.bkgd4.Location = new System.Drawing.Point(35, 49);
			this.bkgd4.Size = new System.Drawing.Size(200, 45);
			this.bkgd4.TabIndex = 0;
			// 
			// dfMS
			// 
			this.dfMS.Name = "dfMS";
			this.dfMS.BackColor = System.Drawing.Color.White;
			this.dfMS.MaxLength = 2;
			this.dfMS.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfMS.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfMS.Format = "#0";
			this.dfMS.Location = new System.Drawing.Point(79, 104);
			this.dfMS.Size = new System.Drawing.Size(40, 30);
			this.dfMS.TabIndex = 1;
			// 
			// pbOk
			// 
			this.pbOk.Name = "pbOk";
			this.pbOk.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbOk.AcceleratorKey = Keys.F12;
			this.pbOk.Text = "&Ok";
			this.pbOk.Location = new System.Drawing.Point(107, 149);
			this.pbOk.Size = new System.Drawing.Size(40, 60);
			this.pbOk.Image = global::Moveta.Intern.Properties.Resources.ok;
			this.pbOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbOk.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbOk_WindowActions);
			this.pbOk.TabIndex = 2;
			// 
			// bkgd5
			// 
			this.bkgd5.Name = "bkgd5";
			this.bkgd5.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd5.ForeColor = System.Drawing.Color.Red;
			this.bkgd5.Text = "F12";
			this.bkgd5.Location = new System.Drawing.Point(155, 159);
			this.bkgd5.Size = new System.Drawing.Size(43, 20);
			this.bkgd5.TabIndex = 3;
			// 
			// bkgd6
			// 
			this.bkgd6.Name = "bkgd6";
			this.bkgd6.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd6.Text = "von";
			this.bkgd6.Location = new System.Drawing.Point(39, 111);
			this.bkgd6.Size = new System.Drawing.Size(32, 20);
			this.bkgd6.TabIndex = 4;
			// 
			// dfMSbis
			// 
			this.dfMSbis.Name = "dfMSbis";
			this.dfMSbis.BackColor = System.Drawing.Color.White;
			this.dfMSbis.MaxLength = 2;
			this.dfMSbis.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfMSbis.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfMSbis.Format = "#0";
			this.dfMSbis.Location = new System.Drawing.Point(201, 104);
			this.dfMSbis.Size = new System.Drawing.Size(40, 30);
			this.dfMSbis.TabIndex = 5;
			// 
			// bkgd7
			// 
			this.bkgd7.Name = "bkgd7";
			this.bkgd7.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd7.Text = "bis";
			this.bkgd7.Location = new System.Drawing.Point(161, 111);
			this.bkgd7.Size = new System.Drawing.Size(32, 20);
			this.bkgd7.TabIndex = 6;
			// 
			// dlgMS
			// 
			this.Controls.Add(this.ClientArea);
			this.Controls.Add(this.ToolBar);
			this.Controls.Add(this.StatusBar);
			this.Name = "dlgMS";
			this.ClientSize = new System.Drawing.Size(257, 275);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Font = new Font("Tahoma", 12f, System.Drawing.FontStyle.Regular);
			this.Text = "Mahnstufe ?";
			this.Location = new System.Drawing.Point(56, 75);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.dlgMS_WindowActions);
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
			if (disposing && App.dlgMS == this) 
			{
				App.dlgMS = null;
			}
			base.Dispose(disposing);
		}
		#endregion
	}
}
