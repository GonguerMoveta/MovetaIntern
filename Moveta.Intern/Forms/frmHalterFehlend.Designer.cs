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
	
	public partial class frmHalterFehlend
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
		public SalDataField dfArztNr;
		public frmHalterFehlend.tblHalterDoppeltTableWindow tblHalterDoppelt;
		protected SalBackgroundText bkgd1;
		public SalDataField dfArztNr2;
		public SalDataField dfArztName2;
		public SalDataField dfArztName;
		public SalPushbutton pbOk;
		public SalPushbutton pbDrucken;
		public SalListBox lbPrinters;
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
			this.dfArztNr = new PPJ.Runtime.Windows.SalDataField();
			this.tblHalterDoppelt = (frmHalterFehlend.tblHalterDoppeltTableWindow)CreateTableWindow(typeof(frmHalterFehlend.tblHalterDoppeltTableWindow));
			this.bkgd1 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.dfArztNr2 = new PPJ.Runtime.Windows.SalDataField();
			this.dfArztName2 = new PPJ.Runtime.Windows.SalDataField();
			this.dfArztName = new PPJ.Runtime.Windows.SalDataField();
			this.pbOk = new PPJ.Runtime.Windows.SalPushbutton();
			this.pbDrucken = new PPJ.Runtime.Windows.SalPushbutton();
			this.lbPrinters = new PPJ.Runtime.Windows.SalListBox();
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
			this.ClientArea.Controls.Add(this.lbPrinters);
			this.ClientArea.Controls.Add(this.pbDrucken);
			this.ClientArea.Controls.Add(this.pbOk);
			this.ClientArea.Controls.Add(this.dfArztName);
			this.ClientArea.Controls.Add(this.dfArztName2);
			this.ClientArea.Controls.Add(this.dfArztNr2);
			this.ClientArea.Controls.Add(this.tblHalterDoppelt);
			this.ClientArea.Controls.Add(this.dfArztNr);
			this.ClientArea.Controls.Add(this.bkgd1);
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
			// dfArztNr
			// 
			this.dfArztNr.Name = "dfArztNr";
			this.dfArztNr.BackColor = System.Drawing.Color.White;
			this.dfArztNr.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfArztNr.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfArztNr.Format = "#0";
			this.dfArztNr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.dfArztNr.Location = new System.Drawing.Point(95, 15);
			this.dfArztNr.Size = new System.Drawing.Size(78, 24);
			this.dfArztNr.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.dfArztNr_WindowActions);
			this.dfArztNr.TabIndex = 0;
			// 
			// tblHalterDoppelt
			// 
			this.tblHalterDoppelt.Name = "tblHalterDoppelt";
			this.tblHalterDoppelt.BackColor = System.Drawing.Color.White;
			this.tblHalterDoppelt.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.tblHalterDoppelt.Location = new System.Drawing.Point(41, 79);
			this.tblHalterDoppelt.Size = new System.Drawing.Size(648, 464);
			this.tblHalterDoppelt.UseVisualStyles = true;
			// 
			// tblHalterDoppelt.colnHalterNr
			// 
			this.tblHalterDoppelt.colnHalterNr.Name = "colnHalterNr";
			this.tblHalterDoppelt.colnHalterNr.Title = "Halter-Nr.";
			this.tblHalterDoppelt.colnHalterNr.Width = 90;
			this.tblHalterDoppelt.colnHalterNr.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblHalterDoppelt.colnHalterNr.Enabled = false;
			this.tblHalterDoppelt.colnHalterNr.Format = "#0";
			this.tblHalterDoppelt.colnHalterNr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblHalterDoppelt.colnHalterNr.Position = 1;
			// 
			// tblHalterDoppelt.colnArztNr
			// 
			this.tblHalterDoppelt.colnArztNr.Name = "colnArztNr";
			this.tblHalterDoppelt.colnArztNr.Title = "nur vorhanden bei Arzt-Nr.";
			this.tblHalterDoppelt.colnArztNr.Width = 174;
			this.tblHalterDoppelt.colnArztNr.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblHalterDoppelt.colnArztNr.Enabled = false;
			this.tblHalterDoppelt.colnArztNr.Format = "#0";
			this.tblHalterDoppelt.colnArztNr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblHalterDoppelt.colnArztNr.Position = 2;
			// 
			// tblHalterDoppelt.colnAnzahl
			// 
			this.tblHalterDoppelt.colnAnzahl.Name = "colnAnzahl";
			this.tblHalterDoppelt.colnAnzahl.Visible = false;
			this.tblHalterDoppelt.colnAnzahl.Title = "Anzahl";
			this.tblHalterDoppelt.colnAnzahl.Width = 174;
			this.tblHalterDoppelt.colnAnzahl.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblHalterDoppelt.colnAnzahl.Enabled = false;
			this.tblHalterDoppelt.colnAnzahl.Format = "#0";
			this.tblHalterDoppelt.colnAnzahl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblHalterDoppelt.colnAnzahl.Position = 3;
			this.tblHalterDoppelt.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.tblHalterDoppelt_WindowActions);
			this.tblHalterDoppelt.TabIndex = 1;
			// 
			// bkgd1
			// 
			this.bkgd1.Name = "bkgd1";
			this.bkgd1.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd1.Text = "Mitglied.Nr.";
			this.bkgd1.Location = new System.Drawing.Point(11, 17);
			this.bkgd1.Size = new System.Drawing.Size(72, 16);
			this.bkgd1.TabIndex = 2;
			// 
			// dfArztNr2
			// 
			this.dfArztNr2.Name = "dfArztNr2";
			this.dfArztNr2.BackColor = System.Drawing.Color.White;
			this.dfArztNr2.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfArztNr2.ReadOnly = true;
			this.dfArztNr2.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfArztNr2.Format = "#0";
			this.dfArztNr2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.dfArztNr2.Location = new System.Drawing.Point(95, 39);
			this.dfArztNr2.Size = new System.Drawing.Size(78, 24);
			this.dfArztNr2.TabIndex = 3;
			// 
			// dfArztName2
			// 
			this.dfArztName2.Name = "dfArztName2";
			this.dfArztName2.BackColor = System.Drawing.SystemColors.Control;
			this.dfArztName2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dfArztName2.ReadOnly = true;
			this.dfArztName2.Font = new Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
			this.dfArztName2.Location = new System.Drawing.Point(189, 33);
			this.dfArztName2.Size = new System.Drawing.Size(312, 16);
			this.dfArztName2.TabIndex = 4;
			// 
			// dfArztName
			// 
			this.dfArztName.Name = "dfArztName";
			this.dfArztName.BackColor = System.Drawing.SystemColors.Control;
			this.dfArztName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dfArztName.ReadOnly = true;
			this.dfArztName.Font = new Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
			this.dfArztName.Location = new System.Drawing.Point(189, 17);
			this.dfArztName.Size = new System.Drawing.Size(312, 16);
			this.dfArztName.TabIndex = 5;
			// 
			// pbOk
			// 
			this.pbOk.Name = "pbOk";
			this.pbOk.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbOk.Text = "fehlende Halter anlegen";
			this.pbOk.Location = new System.Drawing.Point(515, 15);
			this.pbOk.Size = new System.Drawing.Size(174, 48);
			this.pbOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbOk.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbOk_WindowActions);
			this.pbOk.TabIndex = 6;
			// 
			// pbDrucken
			// 
			this.pbDrucken.Name = "pbDrucken";
			this.pbDrucken.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbDrucken.Text = "&drucken";
			this.pbDrucken.Location = new System.Drawing.Point(39, 555);
			this.pbDrucken.Size = new System.Drawing.Size(72, 60);
			this.pbDrucken.Image = global::Moveta.Intern.Properties.Resources.printer;
			this.pbDrucken.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbDrucken.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbDrucken_WindowActions);
			this.pbDrucken.TabIndex = 7;
			// 
			// lbPrinters
			// 
			this.lbPrinters.Name = "lbPrinters";
			this.lbPrinters.HorizontalScrollbar = false;
			this.lbPrinters.BackColor = System.Drawing.Color.White;
			this.lbPrinters.Font = new Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
			this.lbPrinters.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.lbPrinters.Sorted = false;
			this.lbPrinters.Location = new System.Drawing.Point(125, 559);
			this.lbPrinters.Size = new System.Drawing.Size(564, 56);
			this.lbPrinters.TabIndex = 8;
			// 
			// frmHalterFehlend
			// 
			this.Controls.Add(this.ClientArea);
			this.Controls.Add(this.ToolBar);
			this.Controls.Add(this.StatusBar);
			this.Name = "frmHalterFehlend";
			this.ClientSize = new System.Drawing.Size(854, 630);
			this.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.ForeColor = System.Drawing.SystemColors.WindowText;
			this.Text = "bei L oder A fehlende Halter";
			this.Location = new System.Drawing.Point(126, 150);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.frmHalterFehlend_WindowActions);
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
			if (disposing && App.frmHalterFehlend == this) 
			{
				App.frmHalterFehlend = null;
			}
			base.Dispose(disposing);
		}
		#endregion
		
		#region tblHalterDoppelt
		
		public partial class tblHalterDoppeltTableWindow
		{
			#region Window Controls
			public SalTableColumn colnHalterNr;
			public SalTableColumn colnArztNr;
			public SalTableColumn colnAnzahl;
			#endregion
			
			#region Windows Form Designer generated code
			
			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent()
			{
				this.colnHalterNr = new PPJ.Runtime.Windows.SalTableColumn();
				this.colnArztNr = new PPJ.Runtime.Windows.SalTableColumn();
				this.colnAnzahl = new PPJ.Runtime.Windows.SalTableColumn();
				this.SuspendLayout();
				// 
				// colnHalterNr
				// 
				this.colnHalterNr.Name = "colnHalterNr";
				// 
				// colnArztNr
				// 
				this.colnArztNr.Name = "colnArztNr";
				// 
				// colnAnzahl
				// 
				this.colnAnzahl.Name = "colnAnzahl";
				// 
				// tblHalterDoppelt
				// 
				this.Controls.Add(this.colnHalterNr);
				this.Controls.Add(this.colnArztNr);
				this.Controls.Add(this.colnAnzahl);
				this.Name = "tblHalterDoppelt";
				this.ResumeLayout(false);
			}
			#endregion
		}
		#endregion
	}
}
