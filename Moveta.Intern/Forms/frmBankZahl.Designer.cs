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
	
	public partial class frmBankZahl
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
		public VisCalendarDropDown dfBuchDat;
		public SalPushbutton pbZahlOk;
		public SalDataField dfUmsaetze;
		public SalDataField dfStand;
		protected SalBackgroundText bkgd4;
		protected SalBackgroundText bkgd5;
		protected SalBackgroundText bkgd6;
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
            this.pbAbbruch = new PPJ.Runtime.Windows.SalPushbutton();
            this.ClientArea = new PPJ.Runtime.Windows.SalFormClientArea();
            this.dfStand = new PPJ.Runtime.Windows.SalDataField();
            this.dfUmsaetze = new PPJ.Runtime.Windows.SalDataField();
            this.pbZahlOk = new PPJ.Runtime.Windows.SalPushbutton();
            this.dfBuchDat = new PPJ.Runtime.Vis.VisCalendarDropDown();
            this.bkgd7 = new PPJ.Runtime.Windows.SalBackgroundText();
            this.bkgd6 = new PPJ.Runtime.Windows.SalBackgroundText();
            this.bkgd5 = new PPJ.Runtime.Windows.SalBackgroundText();
            this.bkgd4 = new PPJ.Runtime.Windows.SalBackgroundText();
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
            this.ToolBar.Size = new System.Drawing.Size(276, 0);
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
            this.ClientArea.Controls.Add(this.dfStand);
            this.ClientArea.Controls.Add(this.dfUmsaetze);
            this.ClientArea.Controls.Add(this.pbZahlOk);
            this.ClientArea.Controls.Add(this.dfBuchDat);
            this.ClientArea.Controls.Add(this.bkgd7);
            this.ClientArea.Controls.Add(this.bkgd6);
            this.ClientArea.Controls.Add(this.bkgd5);
            this.ClientArea.Controls.Add(this.bkgd4);
            this.ClientArea.Name = "ClientArea";
            this.ClientArea.Size = new System.Drawing.Size(276, 290);
            this.ClientArea.TabIndex = 0;
            // 
            // dfStand
            // 
            this.dfStand.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dfStand.DataType = PPJ.Runtime.Windows.DataType.DateTime;
            this.dfStand.Font = new System.Drawing.Font("Tahoma", 9F);
            this.dfStand.Format = "G";
            this.dfStand.Location = new System.Drawing.Point(101, 25);
            this.dfStand.Name = "dfStand";
            this.dfStand.ReadOnly = true;
            this.dfStand.Size = new System.Drawing.Size(123, 16);
            this.dfStand.TabIndex = 3;
            // 
            // dfUmsaetze
            // 
            this.dfUmsaetze.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dfUmsaetze.DataType = PPJ.Runtime.Windows.DataType.Number;
            this.dfUmsaetze.Font = new System.Drawing.Font("Tahoma", 9F);
            this.dfUmsaetze.Location = new System.Drawing.Point(101, 49);
            this.dfUmsaetze.Name = "dfUmsaetze";
            this.dfUmsaetze.ReadOnly = true;
            this.dfUmsaetze.Size = new System.Drawing.Size(117, 16);
            this.dfUmsaetze.TabIndex = 2;
            this.dfUmsaetze.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pbZahlOk
            // 
            this.pbZahlOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.pbZahlOk.Image = global::Moveta.Intern.Properties.Resources.ok;
            this.pbZahlOk.Location = new System.Drawing.Point(23, 175);
            this.pbZahlOk.Name = "pbZahlOk";
            this.pbZahlOk.Size = new System.Drawing.Size(204, 72);
            this.pbZahlOk.TabIndex = 1;
            this.pbZahlOk.Text = "Bank-Clearing starten";
            this.pbZahlOk.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbZahlOk_WindowActions);
            // 
            // dfBuchDat
            // 
            this.dfBuchDat.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.dfBuchDat.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dfBuchDat.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(233)))), ((int)(((byte)(235)))));
            this.dfBuchDat.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.dfBuchDat.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.dfBuchDat.Format = null;
            this.dfBuchDat.Location = new System.Drawing.Point(114, 133);
            this.dfBuchDat.Name = "dfBuchDat";
            this.dfBuchDat.Size = new System.Drawing.Size(116, 22);
            this.dfBuchDat.TabIndex = 0;
            this.dfBuchDat.Text = "1/29/2024";
            this.dfBuchDat.Value = new PPJ.Runtime.SalDateTime(2024, 1, 29, 7, 56, 59, 832);
            // 
            // bkgd7
            // 
            this.bkgd7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.bkgd7.Location = new System.Drawing.Point(23, 137);
            this.bkgd7.Name = "bkgd7";
            this.bkgd7.Size = new System.Drawing.Size(84, 16);
            this.bkgd7.TabIndex = 7;
            this.bkgd7.Text = "Buch-Datum :";
            // 
            // bkgd6
            // 
            this.bkgd6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.bkgd6.Location = new System.Drawing.Point(17, 49);
            this.bkgd6.Name = "bkgd6";
            this.bkgd6.Size = new System.Drawing.Size(63, 16);
            this.bkgd6.TabIndex = 6;
            this.bkgd6.Text = "Umsätze :";
            // 
            // bkgd5
            // 
            this.bkgd5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.bkgd5.Location = new System.Drawing.Point(17, 81);
            this.bkgd5.Name = "bkgd5";
            this.bkgd5.Size = new System.Drawing.Size(216, 33);
            this.bkgd5.TabIndex = 5;
            this.bkgd5.Text = "Bitte mit DFÜ-Protokoll vergleichen !";
            // 
            // bkgd4
            // 
            this.bkgd4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.bkgd4.Location = new System.Drawing.Point(17, 25);
            this.bkgd4.Name = "bkgd4";
            this.bkgd4.Size = new System.Drawing.Size(51, 16);
            this.bkgd4.TabIndex = 4;
            this.bkgd4.Text = "Datum :";
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(0, 290);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(276, 22);
            this.StatusBar.TabIndex = 2;
            // 
            // frmBankZahl
            // 
            this.ClientSize = new System.Drawing.Size(276, 312);
            this.Controls.Add(this.ClientArea);
            this.Controls.Add(this.ToolBar);
            this.Controls.Add(this.StatusBar);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Location = new System.Drawing.Point(223, 172);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBankZahl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Bank - Clearing";
            this.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.frmBankZahl_WindowActions);
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
			if (disposing && App.frmBankZahl == this) 
			{
				App.frmBankZahl = null;
			}
			base.Dispose(disposing);
		}
		#endregion
	}
}
