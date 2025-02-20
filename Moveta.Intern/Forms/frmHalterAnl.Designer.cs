// <ppj name="Moveta.Intern" date="1/17/2024 7:59:41 AM" id="F4EC85BAD2BF79AC25C9F8643540E90F9BE1DAF0"/>
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
	
	public partial class frmHalterAnl
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
		public SalPushbutton pbAnlegen;
		public SalDataField dfArztNr;
		protected SalBackgroundText bkgd2;
		public SalDataField dfArztNr2;
		protected SalBackgroundText bkgd3;
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
			this.pbAnlegen = new PPJ.Runtime.Windows.SalPushbutton();
			this.dfArztNr = new PPJ.Runtime.Windows.SalDataField();
			this.bkgd2 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.dfArztNr2 = new PPJ.Runtime.Windows.SalDataField();
			this.bkgd3 = new PPJ.Runtime.Windows.SalBackgroundText();
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
			this.ClientArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ClientArea.Controls.Add(this.dfArztNr2);
			this.ClientArea.Controls.Add(this.dfArztNr);
			this.ClientArea.Controls.Add(this.pbAnlegen);
			this.ClientArea.Controls.Add(this.bkgd3);
			this.ClientArea.Controls.Add(this.bkgd2);
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
			// pbAnlegen
			// 
			this.pbAnlegen.Name = "pbAnlegen";
			this.pbAnlegen.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbAnlegen.AcceleratorKey = Keys.Enter;
			this.pbAnlegen.Text = "OK";
			this.pbAnlegen.Location = new System.Drawing.Point(59, 51);
			this.pbAnlegen.Size = new System.Drawing.Size(102, 60);
			this.pbAnlegen.Image = global::Moveta.Intern.Properties.Resources.ok;
			this.pbAnlegen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbAnlegen.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbAnlegen_WindowActions);
			this.pbAnlegen.TabIndex = 0;
			// 
			// dfArztNr
			// 
			this.dfArztNr.Name = "dfArztNr";
			this.dfArztNr.BackColor = System.Drawing.SystemColors.Control;
			this.dfArztNr.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dfArztNr.MaxLength = 4;
			this.dfArztNr.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfArztNr.ReadOnly = true;
			this.dfArztNr.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfArztNr.Location = new System.Drawing.Point(59, 19);
			this.dfArztNr.Size = new System.Drawing.Size(48, 16);
			this.dfArztNr.TabIndex = 1;
			// 
			// bkgd2
			// 
			this.bkgd2.Name = "bkgd2";
			this.bkgd2.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd2.Text = "Arzt-Nr.";
			this.bkgd2.Location = new System.Drawing.Point(5, 19);
			this.bkgd2.Size = new System.Drawing.Size(56, 16);
			this.bkgd2.TabIndex = 2;
			// 
			// dfArztNr2
			// 
			this.dfArztNr2.Name = "dfArztNr2";
			this.dfArztNr2.BackColor = System.Drawing.SystemColors.Control;
			this.dfArztNr2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dfArztNr2.MaxLength = 4;
			this.dfArztNr2.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfArztNr2.ReadOnly = true;
			this.dfArztNr2.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfArztNr2.Location = new System.Drawing.Point(113, 19);
			this.dfArztNr2.Size = new System.Drawing.Size(48, 16);
			this.dfArztNr2.TabIndex = 3;
			// 
			// bkgd3
			// 
			this.bkgd3.Name = "bkgd3";
			this.bkgd3.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd3.Text = "/";
			this.bkgd3.Location = new System.Drawing.Point(107, 18);
			this.bkgd3.Size = new System.Drawing.Size(6, 16);
			this.bkgd3.TabIndex = 4;
			// 
			// frmHalterAnl
			// 
			this.Controls.Add(this.ClientArea);
			this.Controls.Add(this.ToolBar);
			this.Controls.Add(this.StatusBar);
			this.Name = "frmHalterAnl";
			this.ClientSize = new System.Drawing.Size(238, 159);
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.ForeColor = System.Drawing.SystemColors.WindowText;
			this.Text = "neue Halter anlegen";
			this.Location = new System.Drawing.Point(116, 95);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.frmHalterAnl_WindowActions);
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
			if (disposing && App.frmHalterAnl == this) 
			{
				App.frmHalterAnl = null;
			}
			base.Dispose(disposing);
		}
		#endregion
	}
}
