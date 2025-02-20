// <ppj name="Moveta.Intern" date="15.11.2024 14:25:35" id="F4EC85BAD2BF79AC25C9F8643540E90F9BE1DAF0"/>
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
	
	public partial class frmNegativerSaldo
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
		// 25.01.24 ### bis pbOk
		protected SalBackgroundText bkgd3;
		public SalDataField dfAvon;
		protected SalBackgroundText bkgd4;
		public SalDataField dfAbis;
		public SalPushbutton pbOk;
		public frmNegativerSaldo.tblNegTableWindow tblNeg;
		public SalPushbutton pbBuchen;
		public SalPushbutton pbAusgleich;
		protected SalBackgroundText bkgdBW;
		public SalListBox lbPrinters;
		public SalPushbutton pbDruck;
		public SalOptionButton obExcel;
		public SalOptionButton obOO;
		public SalPushbutton pbExport;
		protected SalBackgroundText bkgd2;
		public SalDataField dfJahr;
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
			this.bkgd3 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.dfAvon = new PPJ.Runtime.Windows.SalDataField();
			this.bkgd4 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.dfAbis = new PPJ.Runtime.Windows.SalDataField();
			this.pbOk = new PPJ.Runtime.Windows.SalPushbutton();
			this.tblNeg = (frmNegativerSaldo.tblNegTableWindow)CreateTableWindow(typeof(frmNegativerSaldo.tblNegTableWindow));
			this.pbBuchen = new PPJ.Runtime.Windows.SalPushbutton();
			this.pbAusgleich = new PPJ.Runtime.Windows.SalPushbutton();
			this.bkgdBW = new PPJ.Runtime.Windows.SalBackgroundText();
			this.lbPrinters = new PPJ.Runtime.Windows.SalListBox();
			this.pbDruck = new PPJ.Runtime.Windows.SalPushbutton();
			this.obExcel = new PPJ.Runtime.Windows.SalOptionButton();
			this.obOO = new PPJ.Runtime.Windows.SalOptionButton();
			this.pbExport = new PPJ.Runtime.Windows.SalPushbutton();
			this.bkgd2 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.dfJahr = new PPJ.Runtime.Windows.SalDataField();
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
			this.ClientArea.Controls.Add(this.dfJahr);
			this.ClientArea.Controls.Add(this.pbExport);
			this.ClientArea.Controls.Add(this.obOO);
			this.ClientArea.Controls.Add(this.obExcel);
			this.ClientArea.Controls.Add(this.pbDruck);
			this.ClientArea.Controls.Add(this.lbPrinters);
			this.ClientArea.Controls.Add(this.pbAusgleich);
			this.ClientArea.Controls.Add(this.pbBuchen);
			this.ClientArea.Controls.Add(this.tblNeg);
			this.ClientArea.Controls.Add(this.pbOk);
			this.ClientArea.Controls.Add(this.dfAbis);
			this.ClientArea.Controls.Add(this.dfAvon);
			this.ClientArea.Controls.Add(this.bkgd2);
			this.ClientArea.Controls.Add(this.bkgdBW);
			this.ClientArea.Controls.Add(this.bkgd4);
			this.ClientArea.Controls.Add(this.bkgd3);
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
			// bkgd3
			// 
			this.bkgd3.Name = "bkgd3";
			this.bkgd3.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd3.Text = "Mitglied-Nr. von";
			this.bkgd3.Location = new System.Drawing.Point(11, 17);
			this.bkgd3.Size = new System.Drawing.Size(102, 16);
			this.bkgd3.TabIndex = 0;
			// 
			// dfAvon
			// 
			this.dfAvon.Name = "dfAvon";
			this.dfAvon.BackColor = System.Drawing.Color.White;
			this.dfAvon.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfAvon.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfAvon.Format = "0";
			this.dfAvon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.dfAvon.Location = new System.Drawing.Point(119, 15);
			this.dfAvon.Size = new System.Drawing.Size(78, 24);
			this.dfAvon.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.dfAvon_WindowActions);
			this.dfAvon.TabIndex = 1;
			// 
			// bkgd4
			// 
			this.bkgd4.Name = "bkgd4";
			this.bkgd4.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd4.Text = "bis";
			this.bkgd4.Location = new System.Drawing.Point(209, 17);
			this.bkgd4.Size = new System.Drawing.Size(30, 16);
			this.bkgd4.TabIndex = 2;
			// 
			// dfAbis
			// 
			this.dfAbis.Name = "dfAbis";
			this.dfAbis.BackColor = System.Drawing.Color.White;
			this.dfAbis.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfAbis.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfAbis.Format = "0";
			this.dfAbis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.dfAbis.Location = new System.Drawing.Point(257, 15);
			this.dfAbis.Size = new System.Drawing.Size(78, 24);
			this.dfAbis.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.dfAbis_WindowActions);
			this.dfAbis.TabIndex = 3;
			// 
			// pbOk
			// 
			this.pbOk.Name = "pbOk";
			this.pbOk.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbOk.Text = "Praxen ermitteln";
			this.pbOk.Location = new System.Drawing.Point(353, 15);
			this.pbOk.Size = new System.Drawing.Size(354, 28);
			this.pbOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbOk.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbOk_WindowActions);
			this.pbOk.TabIndex = 4;
			// 
			// tblNeg
			// 
			this.tblNeg.Name = "tblNeg";
			this.tblNeg.BackColor = System.Drawing.Color.White;
			this.tblNeg.Visible = false;
			this.tblNeg.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.tblNeg.Location = new System.Drawing.Point(11, 63);
			this.tblNeg.Size = new System.Drawing.Size(1524, 720);
			this.tblNeg.UseVisualStyles = true;
			// 
			// tblNeg.colArztNr
			// 
			this.tblNeg.colArztNr.Name = "colArztNr";
			this.tblNeg.colArztNr.Title = "Mitglied";
			this.tblNeg.colArztNr.Width = 54;
			this.tblNeg.colArztNr.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblNeg.colArztNr.Enabled = false;
			this.tblNeg.colArztNr.Format = "#0";
			this.tblNeg.colArztNr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblNeg.colArztNr.Position = 1;
			// 
			// tblNeg.colAndereNr
			// 
			this.tblNeg.colAndereNr.Name = "colAndereNr";
			this.tblNeg.colAndereNr.Title = "andere Nrn.";
			this.tblNeg.colAndereNr.Width = 147;
			this.tblNeg.colAndereNr.Enabled = false;
			this.tblNeg.colAndereNr.Position = 2;
			// 
			// tblNeg.colAnschrift
			// 
			this.tblNeg.colAnschrift.Name = "colAnschrift";
			this.tblNeg.colAnschrift.Title = "Anschrift";
			this.tblNeg.colAnschrift.Width = 190;
			this.tblNeg.colAnschrift.Enabled = false;
			this.tblNeg.colAnschrift.Position = 3;
			// 
			// tblNeg.colSoftware
			// 
			this.tblNeg.colSoftware.Name = "colSoftware";
			this.tblNeg.colSoftware.Title = "Software";
			this.tblNeg.colSoftware.Enabled = false;
			this.tblNeg.colSoftware.Position = 4;
			// 
			// tblNeg.colPCs
			// 
			this.tblNeg.colPCs.Name = "colPCs";
			this.tblNeg.colPCs.Title = "PC\r\neig./gest.";
			this.tblNeg.colPCs.Width = 74;
			this.tblNeg.colPCs.Enabled = false;
			this.tblNeg.colPCs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.tblNeg.colPCs.Position = 5;
			// 
			// tblNeg.colGFMemo
			// 
			this.tblNeg.colGFMemo.Name = "colGFMemo";
			this.tblNeg.colGFMemo.Title = "GF-Info";
			this.tblNeg.colGFMemo.Width = 159;
			this.tblNeg.colGFMemo.MaxLength = 999999;
			this.tblNeg.colGFMemo.DataType = PPJ.Runtime.Windows.DataType.String;
			this.tblNeg.colGFMemo.CellType = PPJ.Runtime.Windows.CellType.PopupEdit;
			this.tblNeg.colGFMemo.WordWrap = true;
			this.tblNeg.colGFMemo.Position = 6;
			// 
			// tblNeg.colAbrVJ
			// 
			this.tblNeg.colAbrVJ.Name = "colAbrVJ";
			this.tblNeg.colAbrVJ.Title = "Abr.€ VJ";
			this.tblNeg.colAbrVJ.Width = 90;
			this.tblNeg.colAbrVJ.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblNeg.colAbrVJ.Enabled = false;
			this.tblNeg.colAbrVJ.Format = "#,##0";
			this.tblNeg.colAbrVJ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblNeg.colAbrVJ.Position = 7;
			// 
			// tblNeg.colAbrVVJ
			// 
			this.tblNeg.colAbrVVJ.Name = "colAbrVVJ";
			this.tblNeg.colAbrVVJ.Title = "Abr.€ VVJ";
			this.tblNeg.colAbrVVJ.Width = 88;
			this.tblNeg.colAbrVVJ.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblNeg.colAbrVVJ.Enabled = false;
			this.tblNeg.colAbrVVJ.Format = "#,##0";
			this.tblNeg.colAbrVVJ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblNeg.colAbrVVJ.Position = 8;
			// 
			// tblNeg.colDM
			// 
			this.tblNeg.colDM.Name = "colDM";
			this.tblNeg.colDM.Title = "Belastung €";
			this.tblNeg.colDM.Width = 111;
			this.tblNeg.colDM.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblNeg.colDM.Format = "#,##0.00";
			this.tblNeg.colDM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblNeg.colDM.Position = 9;
			// 
			// tblNeg.colDMBrutto
			// 
			this.tblNeg.colDMBrutto.Name = "colDMBrutto";
			this.tblNeg.colDMBrutto.Title = "Belastung\r\nbrutto €";
			this.tblNeg.colDMBrutto.Width = 93;
			this.tblNeg.colDMBrutto.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblNeg.colDMBrutto.Enabled = false;
			this.tblNeg.colDMBrutto.Format = "#,##0.00";
			this.tblNeg.colDMBrutto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblNeg.colDMBrutto.Position = 10;
			// 
			// tblNeg.colDMVJ
			// 
			this.tblNeg.colDMVJ.Name = "colDMVJ";
			this.tblNeg.colDMVJ.Title = "Belastung € VJ";
			this.tblNeg.colDMVJ.Width = 85;
			this.tblNeg.colDMVJ.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblNeg.colDMVJ.Enabled = false;
			this.tblNeg.colDMVJ.Format = "#,##0.00";
			this.tblNeg.colDMVJ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblNeg.colDMVJ.Position = 11;
			// 
			// tblNeg.colltztBelastung
			// 
			this.tblNeg.colltztBelastung.Name = "colltztBelastung";
			this.tblNeg.colltztBelastung.Title = "letzte\r\nBelastung";
			this.tblNeg.colltztBelastung.DataType = PPJ.Runtime.Windows.DataType.DateTime;
			this.tblNeg.colltztBelastung.Enabled = false;
			this.tblNeg.colltztBelastung.Position = 12;
			// 
			// tblNeg.colSaldo
			// 
			this.tblNeg.colSaldo.Name = "colSaldo";
			this.tblNeg.colSaldo.Visible = false;
			this.tblNeg.colSaldo.Title = "Saldo €";
			this.tblNeg.colSaldo.Width = 111;
			this.tblNeg.colSaldo.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblNeg.colSaldo.Enabled = false;
			this.tblNeg.colSaldo.Format = "#,##0.00";
			this.tblNeg.colSaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblNeg.colSaldo.Position = 13;
			// 
			// tblNeg.colSaldomitBelastung
			// 
			this.tblNeg.colSaldomitBelastung.Name = "colSaldomitBelastung";
			this.tblNeg.colSaldomitBelastung.Title = "Saldo €";
			this.tblNeg.colSaldomitBelastung.Width = 111;
			this.tblNeg.colSaldomitBelastung.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblNeg.colSaldomitBelastung.Enabled = false;
			this.tblNeg.colSaldomitBelastung.Format = "#,##0.00";
			this.tblNeg.colSaldomitBelastung.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.tblNeg.colSaldomitBelastung.Position = 14;
			// 
			// tblNeg.colBrief
			// 
			this.tblNeg.colBrief.Name = "colBrief";
			this.tblNeg.colBrief.Title = "um Ausgleich\r\nbitten";
			this.tblNeg.colBrief.Width = 85;
			this.tblNeg.colBrief.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblNeg.colBrief.CellType = PPJ.Runtime.Windows.CellType.CheckBox;
			this.tblNeg.colBrief.CheckBox.CheckedValue = "1";
			this.tblNeg.colBrief.CheckBox.UncheckedValue = "0";
			this.tblNeg.colBrief.Position = 15;
			this.tblNeg.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.tblNeg_WindowActions);
			this.tblNeg.TabIndex = 5;
			// 
			// pbBuchen
			// 
			this.pbBuchen.Name = "pbBuchen";
			this.pbBuchen.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbBuchen.Text = "Belastungen buchen";
			this.pbBuchen.Location = new System.Drawing.Point(215, 839);
			this.pbBuchen.Size = new System.Drawing.Size(324, 28);
			this.pbBuchen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbBuchen.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbBuchen_WindowActions);
			this.pbBuchen.TabIndex = 6;
			// 
			// pbAusgleich
			// 
			this.pbAusgleich.Name = "pbAusgleich";
			this.pbAusgleich.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbAusgleich.Text = "Zahlungsaufforderung erstellen";
			this.pbAusgleich.Location = new System.Drawing.Point(629, 839);
			this.pbAusgleich.Size = new System.Drawing.Size(330, 28);
			this.pbAusgleich.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbAusgleich.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbAusgleich_WindowActions);
			this.pbAusgleich.TabIndex = 7;
			// 
			// bkgdBW
			// 
			this.bkgdBW.Name = "bkgdBW";
			this.bkgdBW.Visible = false;
			this.bkgdBW.Font = new Font("Tahoma", 40f, System.Drawing.FontStyle.Regular);
			this.bkgdBW.Text = "Bitte warten!";
			this.bkgdBW.Location = new System.Drawing.Point(617, 353);
			this.bkgdBW.Size = new System.Drawing.Size(540, 192);
			this.bkgdBW.TabIndex = 8;
			// 
			// lbPrinters
			// 
			this.lbPrinters.Name = "lbPrinters";
			this.lbPrinters.HorizontalScrollbar = false;
			this.lbPrinters.BackColor = System.Drawing.Color.White;
			this.lbPrinters.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.lbPrinters.SelectionMode = System.Windows.Forms.SelectionMode.One;
			this.lbPrinters.Sorted = true;
			this.lbPrinters.Location = new System.Drawing.Point(1081, 815);
			this.lbPrinters.Size = new System.Drawing.Size(219, 52);
			this.lbPrinters.TabIndex = 9;
			// 
			// pbDruck
			// 
			this.pbDruck.Name = "pbDruck";
			this.pbDruck.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbDruck.Text = "Tabelle";
			this.pbDruck.Location = new System.Drawing.Point(1309, 815);
			this.pbDruck.Size = new System.Drawing.Size(72, 56);
			this.pbDruck.TransparentColor = System.Drawing.Color.White;
            this.pbDruck.Image = global::Moveta.Intern.Properties.Resources.printer2;
            this.pbDruck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbDruck.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbDruck_WindowActions);
			this.pbDruck.TabIndex = 10;
			// 
			// obExcel
			// 
			this.obExcel.Name = "obExcel";
			this.obExcel.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.obExcel.Text = "Excel";
			this.obExcel.Location = new System.Drawing.Point(1390, 815);
			this.obExcel.Size = new System.Drawing.Size(92, 28);
			this.obExcel.ButtonStyle = PPJ.Runtime.Windows.OptionButtonStyle.Radio;
			this.obExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.obExcel.TabIndex = 11;
			// 
			// obOO
			// 
			this.obOO.Name = "obOO";
			this.obOO.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.obOO.Text = "Open Office";
			this.obOO.Location = new System.Drawing.Point(1390, 843);
			this.obOO.Size = new System.Drawing.Size(92, 28);
			this.obOO.ButtonStyle = PPJ.Runtime.Windows.OptionButtonStyle.Radio;
			this.obOO.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.obOO.TabIndex = 12;
			// 
			// pbExport
			// 
			this.pbExport.Name = "pbExport";
			this.pbExport.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbExport.Location = new System.Drawing.Point(1483, 815);
			this.pbExport.Size = new System.Drawing.Size(41, 56);
			this.pbExport.TransparentColor = System.Drawing.Color.White;
            this.pbExport.Image = global::Moveta.Intern.Properties.Resources.export;
            this.pbExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbExport.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbExport_WindowActions);
			this.pbExport.TabIndex = 13;
			// 
			// bkgd2
			// 
			this.bkgd2.Name = "bkgd2";
			this.bkgd2.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd2.Text = "Belastung für Jahr:";
			this.bkgd2.Location = new System.Drawing.Point(215, 809);
			this.bkgd2.Size = new System.Drawing.Size(126, 16);
			this.bkgd2.TabIndex = 14;
			// 
			// dfJahr
			// 
			this.dfJahr.Name = "dfJahr";
			this.dfJahr.BackColor = System.Drawing.Color.White;
			this.dfJahr.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.dfJahr.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.dfJahr.Format = "0";
			this.dfJahr.Location = new System.Drawing.Point(353, 807);
			this.dfJahr.Size = new System.Drawing.Size(84, 24);
			this.dfJahr.TabIndex = 15;
			// 
			// frmNegativerSaldo
			// 
			this.Controls.Add(this.ClientArea);
			this.Controls.Add(this.ToolBar);
			this.Controls.Add(this.StatusBar);
			this.Name = "frmNegativerSaldo";
			this.ClientSize = new System.Drawing.Size(1598, 953);
			this.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.ForeColor = System.Drawing.SystemColors.WindowText;
			this.Text = "Negative Salden";
			this.Location = new System.Drawing.Point(20, 60);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.frmNegativerSaldo_WindowActions);
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
			if (disposing && App.frmNegativerSaldo == this) 
			{
				App.frmNegativerSaldo = null;
			}
			base.Dispose(disposing);
		}
		#endregion
		
		#region tblNeg
		
		public partial class tblNegTableWindow
		{
			#region Window Controls
			public SalTableColumn colArztNr;
			public SalTableColumn colAndereNr;
			public SalTableColumn colAnschrift;
			public SalTableColumn colSoftware;
			public SalTableColumn colPCs;
			public SalTableColumn colGFMemo;
			public SalTableColumn colAbrVJ;
			public SalTableColumn colAbrVVJ;
			public SalTableColumn colDM;
			public SalTableColumn colDMBrutto;
			public SalTableColumn colDMVJ;
			public SalTableColumn colltztBelastung;
			public SalTableColumn colSaldo;
			public SalTableColumn colSaldomitBelastung;
			public SalTableColumn colBrief;
			#endregion
			
			#region Windows Form Designer generated code
			
			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent()
			{
				this.colArztNr = new PPJ.Runtime.Windows.SalTableColumn();
				this.colAndereNr = new PPJ.Runtime.Windows.SalTableColumn();
				this.colAnschrift = new PPJ.Runtime.Windows.SalTableColumn();
				this.colSoftware = new PPJ.Runtime.Windows.SalTableColumn();
				this.colPCs = new PPJ.Runtime.Windows.SalTableColumn();
				this.colGFMemo = new PPJ.Runtime.Windows.SalTableColumn();
				this.colAbrVJ = new PPJ.Runtime.Windows.SalTableColumn();
				this.colAbrVVJ = new PPJ.Runtime.Windows.SalTableColumn();
				this.colDM = new PPJ.Runtime.Windows.SalTableColumn();
				this.colDMBrutto = new PPJ.Runtime.Windows.SalTableColumn();
				this.colDMVJ = new PPJ.Runtime.Windows.SalTableColumn();
				this.colltztBelastung = new PPJ.Runtime.Windows.SalTableColumn();
				this.colSaldo = new PPJ.Runtime.Windows.SalTableColumn();
				this.colSaldomitBelastung = new PPJ.Runtime.Windows.SalTableColumn();
				this.colBrief = new PPJ.Runtime.Windows.SalTableColumn();
				this.SuspendLayout();
				// 
				// colArztNr
				// 
				this.colArztNr.Name = "colArztNr";
				// 
				// colAndereNr
				// 
				this.colAndereNr.Name = "colAndereNr";
				// 
				// colAnschrift
				// 
				this.colAnschrift.Name = "colAnschrift";
				// 
				// colSoftware
				// 
				this.colSoftware.Name = "colSoftware";
				// 
				// colPCs
				// 
				this.colPCs.Name = "colPCs";
				// 
				// colGFMemo
				// 
				this.colGFMemo.Name = "colGFMemo";
				this.colGFMemo.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.colGFMemo_WindowActions);
				// 
				// colAbrVJ
				// 
				this.colAbrVJ.Name = "colAbrVJ";
				// 
				// colAbrVVJ
				// 
				this.colAbrVVJ.Name = "colAbrVVJ";
				// 
				// colDM
				// 
				this.colDM.Name = "colDM";
				this.colDM.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.colDM_WindowActions);
				// 
				// colDMBrutto
				// 
				this.colDMBrutto.Name = "colDMBrutto";
				this.colDMBrutto.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.colDMBrutto_WindowActions);
				// 
				// colDMVJ
				// 
				this.colDMVJ.Name = "colDMVJ";
				// 
				// colltztBelastung
				// 
				this.colltztBelastung.Name = "colltztBelastung";
				// 
				// colSaldo
				// 
				this.colSaldo.Name = "colSaldo";
				// 
				// colSaldomitBelastung
				// 
				this.colSaldomitBelastung.Name = "colSaldomitBelastung";
				// 
				// colBrief
				// 
				this.colBrief.Name = "colBrief";
				// 
				// tblNeg
				// 
				this.Controls.Add(this.colArztNr);
				this.Controls.Add(this.colAndereNr);
				this.Controls.Add(this.colAnschrift);
				this.Controls.Add(this.colSoftware);
				this.Controls.Add(this.colPCs);
				this.Controls.Add(this.colGFMemo);
				this.Controls.Add(this.colAbrVJ);
				this.Controls.Add(this.colAbrVVJ);
				this.Controls.Add(this.colDM);
				this.Controls.Add(this.colDMBrutto);
				this.Controls.Add(this.colDMVJ);
				this.Controls.Add(this.colltztBelastung);
				this.Controls.Add(this.colSaldo);
				this.Controls.Add(this.colSaldomitBelastung);
				this.Controls.Add(this.colBrief);
				this.Name = "tblNeg";
				this.ResumeLayout(false);
			}
			#endregion
		}
		#endregion
	}
}
