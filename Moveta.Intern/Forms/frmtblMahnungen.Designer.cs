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
	
	public partial class frmtblMahnungen
	{
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		#region Window Controls
		public frmtblMahnungen.tblMahnungenTableWindow tblMahnungen;
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
			this.tblMahnungen = (frmtblMahnungen.tblMahnungenTableWindow)CreateTableWindow(typeof(frmtblMahnungen.tblMahnungenTableWindow));
			this.pbDrucken = new PPJ.Runtime.Windows.SalPushbutton();
			this.lbPrinters = new PPJ.Runtime.Windows.SalListBox();
			this.SuspendLayout();
			// 
			// tblMahnungen
			// 
			this.tblMahnungen.Name = "tblMahnungen";
			this.tblMahnungen.BackColor = System.Drawing.Color.White;
			this.tblMahnungen.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.tblMahnungen.Location = new System.Drawing.Point(11, 15);
			this.tblMahnungen.Size = new System.Drawing.Size(498, 608);
			this.tblMahnungen.UseVisualStyles = true;
			// 
			// tblMahnungen.colnMANr
			// 
			this.tblMahnungen.colnMANr.Name = "colnMANr";
			this.tblMahnungen.colnMANr.Title = "Nr.";
			this.tblMahnungen.colnMANr.Width = 77;
			this.tblMahnungen.colnMANr.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblMahnungen.colnMANr.Enabled = false;
			this.tblMahnungen.colnMANr.Format = "#0";
			this.tblMahnungen.colnMANr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblMahnungen.colnMANr.Position = 1;
			// 
			// tblMahnungen.coldtMADat
			// 
			this.tblMahnungen.coldtMADat.Name = "coldtMADat";
			this.tblMahnungen.coldtMADat.Title = "Mahndatum";
			this.tblMahnungen.coldtMADat.Width = 80;
			this.tblMahnungen.coldtMADat.DataType = PPJ.Runtime.Windows.DataType.DateTime;
			this.tblMahnungen.coldtMADat.Enabled = false;
			this.tblMahnungen.coldtMADat.Format = "d";
			this.tblMahnungen.coldtMADat.Position = 2;
			// 
			// tblMahnungen.colnMADM
			// 
			this.tblMahnungen.colnMADM.Name = "colnMADM";
			this.tblMahnungen.colnMADM.Title = "€ Mahngeb.";
			this.tblMahnungen.colnMADM.Width = 81;
			this.tblMahnungen.colnMADM.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblMahnungen.colnMADM.Enabled = false;
			this.tblMahnungen.colnMADM.Format = "#,##0.00";
			this.tblMahnungen.colnMADM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblMahnungen.colnMADM.Position = 3;
			// 
			// tblMahnungen.colnMAZins
			// 
			this.tblMahnungen.colnMAZins.Name = "colnMAZins";
			this.tblMahnungen.colnMAZins.Title = "€ Zinsen";
			this.tblMahnungen.colnMAZins.Width = 64;
			this.tblMahnungen.colnMAZins.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblMahnungen.colnMAZins.Enabled = false;
			this.tblMahnungen.colnMAZins.Format = "#,##0.00";
			this.tblMahnungen.colnMAZins.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblMahnungen.colnMAZins.Position = 4;
			// 
			// tblMahnungen.colnMAMS
			// 
			this.tblMahnungen.colnMAMS.Name = "colnMAMS";
			this.tblMahnungen.colnMAMS.Visible = false;
			this.tblMahnungen.colnMAMS.Title = "MS";
			this.tblMahnungen.colnMAMS.Width = 35;
			this.tblMahnungen.colnMAMS.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblMahnungen.colnMAMS.Enabled = false;
			this.tblMahnungen.colnMAMS.Position = 5;
			// 
			// tblMahnungen.colsMAMSText
			// 
			this.tblMahnungen.colsMAMSText.Name = "colsMAMSText";
			this.tblMahnungen.colsMAMSText.Title = "MS";
			this.tblMahnungen.colsMAMSText.Width = 35;
			this.tblMahnungen.colsMAMSText.Enabled = false;
			this.tblMahnungen.colsMAMSText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.tblMahnungen.colsMAMSText.Position = 6;
			// 
			// tblMahnungen.colnMARest
			// 
			this.tblMahnungen.colnMARest.Name = "colnMARest";
			this.tblMahnungen.colnMARest.Title = "€ damaliger OP";
			this.tblMahnungen.colnMARest.Width = 102;
			this.tblMahnungen.colnMARest.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblMahnungen.colnMARest.Enabled = false;
			this.tblMahnungen.colnMARest.Format = "#,##0.00";
			this.tblMahnungen.colnMARest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblMahnungen.colnMARest.Position = 7;
			this.tblMahnungen.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.tblMahnungen_WindowActions);
			this.tblMahnungen.TabIndex = 0;
			// 
			// pbDrucken
			// 
			this.pbDrucken.Name = "pbDrucken";
			this.pbDrucken.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbDrucken.Text = "&drucken";
			this.pbDrucken.Location = new System.Drawing.Point(9, 627);
			this.pbDrucken.Size = new System.Drawing.Size(72, 60);
			this.pbDrucken.Image = global::Moveta.Intern.Properties.Resources.printer;
			this.pbDrucken.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbDrucken.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbDrucken_WindowActions);
			this.pbDrucken.TabIndex = 1;
			// 
			// lbPrinters
			// 
			this.lbPrinters.Name = "lbPrinters";
			this.lbPrinters.HorizontalScrollbar = false;
			this.lbPrinters.BackColor = System.Drawing.Color.White;
			this.lbPrinters.Font = new Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
			this.lbPrinters.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.lbPrinters.Sorted = false;
			this.lbPrinters.Location = new System.Drawing.Point(89, 631);
			this.lbPrinters.Size = new System.Drawing.Size(420, 56);
			this.lbPrinters.TabIndex = 2;
			// 
			// frmtblMahnungen
			// 
			this.Controls.Add(this.lbPrinters);
			this.Controls.Add(this.pbDrucken);
			this.Controls.Add(this.tblMahnungen);
			this.Name = "frmtblMahnungen";
			this.ClientSize = new System.Drawing.Size(574, 730);
			this.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.ForeColor = System.Drawing.SystemColors.WindowText;
			this.Text = "Mahnungen";
			this.Location = new System.Drawing.Point(137, 156);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
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
			if (disposing && App.frmtblMahnungen == this) 
			{
				App.frmtblMahnungen = null;
			}
			base.Dispose(disposing);
		}
		#endregion
		
		#region tblMahnungen
		
		public partial class tblMahnungenTableWindow
		{
			#region Window Controls
			public SalTableColumn colnMANr;
			public SalTableColumn coldtMADat;
			public SalTableColumn colnMADM;
			public SalTableColumn colnMAZins;
			public SalTableColumn colnMAMS;
			public SalTableColumn colsMAMSText;
			public SalTableColumn colnMARest;
			#endregion
			
			#region Windows Form Designer generated code
			
			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent()
			{
				this.colnMANr = new PPJ.Runtime.Windows.SalTableColumn();
				this.coldtMADat = new PPJ.Runtime.Windows.SalTableColumn();
				this.colnMADM = new PPJ.Runtime.Windows.SalTableColumn();
				this.colnMAZins = new PPJ.Runtime.Windows.SalTableColumn();
				this.colnMAMS = new PPJ.Runtime.Windows.SalTableColumn();
				this.colsMAMSText = new PPJ.Runtime.Windows.SalTableColumn();
				this.colnMARest = new PPJ.Runtime.Windows.SalTableColumn();
				this.SuspendLayout();
				// 
				// colnMANr
				// 
				this.colnMANr.Name = "colnMANr";
				// 
				// coldtMADat
				// 
				this.coldtMADat.Name = "coldtMADat";
				// 
				// colnMADM
				// 
				this.colnMADM.Name = "colnMADM";
				// 
				// colnMAZins
				// 
				this.colnMAZins.Name = "colnMAZins";
				// 
				// colnMAMS
				// 
				this.colnMAMS.Name = "colnMAMS";
				// 
				// colsMAMSText
				// 
				this.colsMAMSText.Name = "colsMAMSText";
				// 
				// colnMARest
				// 
				this.colnMARest.Name = "colnMARest";
				// 
				// tblMahnungen
				// 
				this.Controls.Add(this.colnMANr);
				this.Controls.Add(this.coldtMADat);
				this.Controls.Add(this.colnMADM);
				this.Controls.Add(this.colnMAZins);
				this.Controls.Add(this.colnMAMS);
				this.Controls.Add(this.colsMAMSText);
				this.Controls.Add(this.colnMARest);
				this.Name = "tblMahnungen";
				this.ResumeLayout(false);
			}
			#endregion
		}
		#endregion
	}
}
