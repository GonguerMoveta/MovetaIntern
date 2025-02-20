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
using Jam.Shell;

namespace Moveta.Intern
{
	
	public partial class frmAbrDateiX
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
		protected SalBackgroundText bkgd1;
		public ShellTreeView axTree;
		protected SalBackgroundText bkgd2;
		public SalListBox lbDateien;
		protected SalBackgroundText bkgd3;
		public SalPushbutton pbOk;
		public SalDataField dfOrdner;
		// Combo Box: cmbDateien
		// List Initialization
		// Message Actions
		protected SalBackgroundText bkgd4;
		public SalDataField dfArztNr;
		protected SalBackgroundText bkgd5;
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
			this.bkgd1 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.axTree = new ShellTreeView();
			this.bkgd2 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.lbDateien = new PPJ.Runtime.Windows.SalListBox();
			this.bkgd3 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.pbOk = new PPJ.Runtime.Windows.SalPushbutton();
			this.dfOrdner = new PPJ.Runtime.Windows.SalDataField();
			this.bkgd4 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.dfArztNr = new PPJ.Runtime.Windows.SalDataField();
			this.bkgd5 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.ClientArea.SuspendLayout();
			this.SuspendLayout();
			// 
			// ToolBar
			// 
			this.ToolBar.Name = "ToolBar";
			this.ToolBar.Font = new Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
			this.ToolBar.TabStop = true;
			this.ToolBar.Visible = false;
			this.ToolBar.Create = false;
			// 
			// ClientArea
			// 
			this.ClientArea.Name = "ClientArea";
			this.ClientArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ClientArea.Controls.Add(this.dfArztNr);
			this.ClientArea.Controls.Add(this.dfOrdner);
			this.ClientArea.Controls.Add(this.pbOk);
			this.ClientArea.Controls.Add(this.lbDateien);
			this.ClientArea.Controls.Add(this.axTree);
			this.ClientArea.Controls.Add(this.bkgd5);
			this.ClientArea.Controls.Add(this.bkgd4);
			this.ClientArea.Controls.Add(this.bkgd3);
			this.ClientArea.Controls.Add(this.bkgd2);
			this.ClientArea.Controls.Add(this.bkgd1);
			// 
			// StatusBar
			// 
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.Visible = true;
			// 
			// bkgd1
			// 
			this.bkgd1.Name = "bkgd1";
			this.bkgd1.Font = new Font("Tahoma", 14f, System.Drawing.FontStyle.Bold);
			this.bkgd1.Text = "1.";
			this.bkgd1.Location = new System.Drawing.Point(5, 49);
			this.bkgd1.Size = new System.Drawing.Size(60, 30);
			this.bkgd1.TabIndex = 0;
			// 
			// axTree
			// 
			this.axTree.Name = "axTree";
			this.axTree.Location = new System.Drawing.Point(6, 79);
			this.axTree.Size = new System.Drawing.Size(491, 336);
            this.axTree.FolderUpdated += AxTree_FolderUpdated;
            this.axTree.TabIndex = 1;
			// 
			// bkgd2
			// 
			this.bkgd2.Name = "bkgd2";
			this.bkgd2.Font = new Font("Tahoma", 14f, System.Drawing.FontStyle.Bold);
			this.bkgd2.Text = "2.";
			this.bkgd2.Location = new System.Drawing.Point(525, 49);
			this.bkgd2.Size = new System.Drawing.Size(60, 30);
			this.bkgd2.TabIndex = 2;
			// 
			// lbDateien
			// 
			this.lbDateien.Name = "lbDateien";
			this.lbDateien.HorizontalScrollbar = false;
			this.lbDateien.BackColor = System.Drawing.Color.White;
			this.lbDateien.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.lbDateien.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.lbDateien.Sorted = true;
			this.lbDateien.Location = new System.Drawing.Point(522, 80);
			this.lbDateien.Size = new System.Drawing.Size(120, 324);
			this.lbDateien.TabIndex = 3;
			// 
			// bkgd3
			// 
			this.bkgd3.Name = "bkgd3";
			this.bkgd3.Font = new Font("Tahoma", 14f, System.Drawing.FontStyle.Bold);
			this.bkgd3.Text = "3.";
			this.bkgd3.Location = new System.Drawing.Point(661, 49);
			this.bkgd3.Size = new System.Drawing.Size(60, 30);
			this.bkgd3.TabIndex = 4;
			// 
			// pbOk
			// 
			this.pbOk.Name = "pbOk";
			this.pbOk.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbOk.Text = "Dateien zusammenfügen";
			this.pbOk.Location = new System.Drawing.Point(665, 239);
			this.pbOk.Size = new System.Drawing.Size(240, 28);
			this.pbOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbOk.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbOk_WindowActions);
			this.pbOk.TabIndex = 5;
			// 
			// dfOrdner
			// 
			this.dfOrdner.Name = "dfOrdner";
			this.dfOrdner.BackColor = System.Drawing.Color.White;
			this.dfOrdner.Visible = false;
			this.dfOrdner.ReadOnly = true;
			this.dfOrdner.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfOrdner.Location = new System.Drawing.Point(0, 0);
			this.dfOrdner.Size = new System.Drawing.Size(50, 24);
			this.dfOrdner.TabIndex = 6;
			// 
			// bkgd4
			// 
			this.bkgd4.Name = "bkgd4";
			this.bkgd4.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd4.Text = "Mitglied-Nr.:";
			this.bkgd4.Location = new System.Drawing.Point(5, 9);
			this.bkgd4.Size = new System.Drawing.Size(78, 16);
			this.bkgd4.TabIndex = 7;
			// 
			// dfArztNr
			// 
			this.dfArztNr.Name = "dfArztNr";
			this.dfArztNr.BackColor = System.Drawing.Color.White;
			this.dfArztNr.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfArztNr.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfArztNr.Format = "#0";
			this.dfArztNr.Location = new System.Drawing.Point(101, 7);
			this.dfArztNr.Size = new System.Drawing.Size(108, 24);
			this.dfArztNr.TabIndex = 8;
			// 
			// bkgd5
			// 
			this.bkgd5.Name = "bkgd5";
			this.bkgd5.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd5.Text = "Bitte beachten: Es wird nicht geprüft, ob die Dateien zum selben Mitglied gehören.\r\nWenn Kunden in mehreren Dateien vorhanden sind, werden diese separat abgerechnet.";
			this.bkgd5.Location = new System.Drawing.Point(233, 9);
			this.bkgd5.Size = new System.Drawing.Size(684, 32);
			this.bkgd5.TabIndex = 9;
			// 
			// frmAbrDateiX
			// 
			this.Controls.Add(this.ClientArea);
			this.Controls.Add(this.ToolBar);
			this.Controls.Add(this.StatusBar);
			this.Name = "frmAbrDateiX";
			this.ClientSize = new System.Drawing.Size(958, 510);
			this.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.ForeColor = System.Drawing.SystemColors.WindowText;
			this.Text = "Abrechnungsdateien zusammenfügen";
			this.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.frmAbrDateiX_WindowActions);
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
			if (disposing && App.frmAbrDateiX == this) 
			{
				App.frmAbrDateiX = null;
			}
			base.Dispose(disposing);
		}
		#endregion
	}
}
