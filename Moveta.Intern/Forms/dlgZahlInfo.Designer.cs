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
	
	public partial class dlgZahlInfo
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
		protected SalBackgroundText bkgdA;
		public SalDataField dfA;
		protected SalBackgroundText bkgdH;
		public SalDataField dfH;
		protected SalBackgroundText bkgdR;
		public SalDataField dfR;
		protected SalBackgroundText bkgdPR;
		public SalDataField dfPR;
		public SalMultilineField mlKunde;
		protected SalBackgroundText bkgd5;
		public SalMultilineField mlNachricht;
		public SalPushbutton pbOk;
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
			this.bkgdA = new PPJ.Runtime.Windows.SalBackgroundText();
			this.dfA = new PPJ.Runtime.Windows.SalDataField();
			this.bkgdH = new PPJ.Runtime.Windows.SalBackgroundText();
			this.dfH = new PPJ.Runtime.Windows.SalDataField();
			this.bkgdR = new PPJ.Runtime.Windows.SalBackgroundText();
			this.dfR = new PPJ.Runtime.Windows.SalDataField();
			this.bkgdPR = new PPJ.Runtime.Windows.SalBackgroundText();
			this.dfPR = new PPJ.Runtime.Windows.SalDataField();
			this.mlKunde = new PPJ.Runtime.Windows.SalMultilineField();
			this.bkgd5 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.mlNachricht = new PPJ.Runtime.Windows.SalMultilineField();
			this.pbOk = new PPJ.Runtime.Windows.SalPushbutton();
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
			this.ClientArea.Controls.Add(this.pbOk);
			this.ClientArea.Controls.Add(this.mlNachricht);
			this.ClientArea.Controls.Add(this.mlKunde);
			this.ClientArea.Controls.Add(this.dfPR);
			this.ClientArea.Controls.Add(this.dfR);
			this.ClientArea.Controls.Add(this.dfH);
			this.ClientArea.Controls.Add(this.dfA);
			this.ClientArea.Controls.Add(this.bkgd5);
			this.ClientArea.Controls.Add(this.bkgdPR);
			this.ClientArea.Controls.Add(this.bkgdR);
			this.ClientArea.Controls.Add(this.bkgdH);
			this.ClientArea.Controls.Add(this.bkgdA);
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
			// bkgdA
			// 
			this.bkgdA.Name = "bkgdA";
			this.bkgdA.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgdA.Text = "A:";
			this.bkgdA.Location = new System.Drawing.Point(15, 21);
			this.bkgdA.Size = new System.Drawing.Size(24, 20);
			this.bkgdA.TabIndex = 0;
			// 
			// dfA
			// 
			this.dfA.Name = "dfA";
			this.dfA.BackColor = System.Drawing.Color.White;
			this.dfA.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfA.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfA.Format = "#0";
			this.dfA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.dfA.Location = new System.Drawing.Point(47, 19);
			this.dfA.Size = new System.Drawing.Size(80, 20);
			this.dfA.TabIndex = 1;
			// 
			// bkgdH
			// 
			this.bkgdH.Name = "bkgdH";
			this.bkgdH.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgdH.Text = "H:";
			this.bkgdH.Location = new System.Drawing.Point(15, 41);
			this.bkgdH.Size = new System.Drawing.Size(24, 20);
			this.bkgdH.TabIndex = 2;
			// 
			// dfH
			// 
			this.dfH.Name = "dfH";
			this.dfH.BackColor = System.Drawing.Color.White;
			this.dfH.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfH.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfH.Format = "#0";
			this.dfH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.dfH.Location = new System.Drawing.Point(47, 39);
			this.dfH.Size = new System.Drawing.Size(80, 20);
			this.dfH.TabIndex = 3;
			// 
			// bkgdR
			// 
			this.bkgdR.Name = "bkgdR";
			this.bkgdR.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgdR.Text = "R:";
			this.bkgdR.Location = new System.Drawing.Point(15, 61);
			this.bkgdR.Size = new System.Drawing.Size(24, 20);
			this.bkgdR.TabIndex = 4;
			// 
			// dfR
			// 
			this.dfR.Name = "dfR";
			this.dfR.BackColor = System.Drawing.Color.White;
			this.dfR.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfR.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfR.Format = "#0";
			this.dfR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.dfR.Location = new System.Drawing.Point(47, 59);
			this.dfR.Size = new System.Drawing.Size(80, 20);
			this.dfR.TabIndex = 5;
			// 
			// bkgdPR
			// 
			this.bkgdPR.Name = "bkgdPR";
			this.bkgdPR.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgdPR.Text = "PR:";
			this.bkgdPR.Location = new System.Drawing.Point(15, 81);
			this.bkgdPR.Size = new System.Drawing.Size(24, 20);
			this.bkgdPR.TabIndex = 6;
			// 
			// dfPR
			// 
			this.dfPR.Name = "dfPR";
			this.dfPR.BackColor = System.Drawing.Color.White;
			this.dfPR.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfPR.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfPR.Format = "#0";
			this.dfPR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.dfPR.Location = new System.Drawing.Point(47, 79);
			this.dfPR.Size = new System.Drawing.Size(80, 20);
			this.dfPR.TabIndex = 7;
			// 
			// mlKunde
			// 
			this.mlKunde.Name = "mlKunde";
			this.mlKunde.BackColor = System.Drawing.Color.White;
			this.mlKunde.ReadOnly = true;
			this.mlKunde.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.mlKunde.Location = new System.Drawing.Point(135, 19);
			this.mlKunde.Size = new System.Drawing.Size(536, 80);
			this.mlKunde.TabIndex = 8;
			// 
			// bkgd5
			// 
			this.bkgd5.Name = "bkgd5";
			this.bkgd5.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.bkgd5.Text = "Textnachricht:";
			this.bkgd5.Location = new System.Drawing.Point(15, 111);
			this.bkgd5.Size = new System.Drawing.Size(112, 20);
			this.bkgd5.TabIndex = 9;
			// 
			// mlNachricht
			// 
			this.mlNachricht.Name = "mlNachricht";
			this.mlNachricht.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.mlNachricht.BackColor = System.Drawing.Color.White;
			this.mlNachricht.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.mlNachricht.Location = new System.Drawing.Point(135, 109);
			this.mlNachricht.Size = new System.Drawing.Size(536, 60);
			this.mlNachricht.TabIndex = 10;
			// 
			// pbOk
			// 
			this.pbOk.Name = "pbOk";
			this.pbOk.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbOk.AcceleratorKey = Keys.F12;
			this.pbOk.Text = "&Nachricht absenden (F12)";
			this.pbOk.Location = new System.Drawing.Point(135, 179);
			this.pbOk.Size = new System.Drawing.Size(536, 35);
			this.pbOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbOk.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbOk_WindowActions);
			this.pbOk.TabIndex = 11;
			// 
			// dlgZahlInfo
			// 
			this.Controls.Add(this.ClientArea);
			this.Controls.Add(this.ToolBar);
			this.Controls.Add(this.StatusBar);
			this.Name = "dlgZahlInfo";
			this.ClientSize = new System.Drawing.Size(698, 253);
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
			this.Font = new Font("Tahoma", 12f, System.Drawing.FontStyle.Regular);
			this.Text = "Zahlmeldung senden";
			this.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.dlgZahlInfo_WindowActions);
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
			if (disposing && App.dlgZahlInfo == this) 
			{
				App.dlgZahlInfo = null;
			}
			base.Dispose(disposing);
		}
		#endregion
	}
}
