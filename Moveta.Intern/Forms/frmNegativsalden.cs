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
	
	/// <summary>
	/// 14.05.21 Ä1957
	/// </summary>
	public partial class frmNegativsalden : SalFormWindow
	{
		#region Window Variables
		public SalSqlHandle hSqlNegativ = SalSqlHandle.Null;
		public SalSqlHandle hSqlZahl = SalSqlHandle.Null;
		public SalSqlHandle hSqlVorschuss = SalSqlHandle.Null;
		public SalSqlHandle hSqlZahlVM = SalSqlHandle.Null;
		public SalSqlHandle hSqlVorschussVM = SalSqlHandle.Null;
		public SalSqlHandle hSqlTK = SalSqlHandle.Null;
		public SalSqlHandle hSqlTKVM = SalSqlHandle.Null;
		public SalString strSelect = "";
		public SalNumber nFetch = 0;
		public SalNumber nFetch1 = 0;
		public SalNumber nErr = 0;
		public SalWindowHandle hWndReport = SalWindowHandle.Null;
		public SalNumber nRow = 0;
		public SalNumber nArztNr = 0;
		public SalNumber nAVor = 0;
		public SalNumber nTK = 0;
		public SalNumber nZahl = 0;
		public SalNumber nZahlVM = 0;
		public SalNumber nVorschuss = 0;
		public SalNumber nVorschussVM = 0;
		public SalString strName = "";
		public SalString strOrt = "";
		public SalNumber nDummy = 0;
		public SalNumber nTKAuswahl = 0;
		public MTblPrintParams PrintParams = new MTblPrintParams();
		public SalString strDevice = "";
		public SalNumber nResult = 0;
		public SalNumber nAnz = 0;
		public SalNumber nOff = 0;
		public SalArray<SalString> strPrinters = new SalArray<SalString>();
		public MTblPrintLine PrintHeader = new MTblPrintLine();
		public MTblPrintLinePosText PrintHeaderText = new MTblPrintLinePosText();
		public SalString strHeader = "";
		#endregion
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public frmNegativsalden()
		{
			// Assign global reference.
			App.frmNegativsalden = this;
			// This call is required by the Windows Form Designer.
			InitializeComponent();
		}
		#endregion
		
		#region System Methods/Properties
		
		/// <summary>
		/// Shows the form window.
		/// </summary>
		/// <param name="owner"></param>
		/// <returns></returns>
		public static frmNegativsalden CreateWindow(Control owner)
		{
			frmNegativsalden frm = new frmNegativsalden();
			frm.Show(owner);
			return frm;
		}
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static frmNegativsalden FromHandle(SalWindowHandle handle)
		{
			return ((frmNegativsalden)SalWindow.FromHandle(handle, typeof(frmNegativsalden)));
		}
		#endregion
		
		#region Window Actions
		
		/// <summary>
		/// frmNegativsalden WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmNegativsalden_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_CreateComplete:
					this.frmNegativsalden_OnSAM_CreateComplete(sender, e);
					break;
				
				case Sys.SAM_Close:
					this.frmNegativsalden_OnSAM_Close(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_CreateComplete event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmNegativsalden_OnSAM_CreateComplete(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			Int.SqlConnection(ref this.hSqlNegativ);
			Int.SqlConnection(ref this.hSqlZahl);
			Int.SqlConnection(ref this.hSqlVorschuss);
			Int.SqlConnection(ref this.hSqlZahlVM);
			Int.SqlConnection(ref this.hSqlVorschussVM);
			Int.SqlConnection(ref this.hSqlTK);
			Int.SqlConnection(ref this.hSqlTKVM);
			Int.SqlPrepar(this.hSqlZahl, "SELECT SUM(tkhaben-tksoll) INTO :frmNegativsalden.nZahl FROM tk WHERE tkarztnr = :frmNegativsalden.nArztNr AND tksa=1");
			Int.SqlPrepar(this.hSqlZahlVM, @"SELECT SUM(tkhaben-tksoll) INTO :frmNegativsalden.nZahlVM FROM tka
WHERE tkaarztnr = :frmNegativsalden.nArztNr AND tkasa=1 AND tkadruckdatum > SYSDATE - 6 MONTHS");
			Int.SqlPrepar(this.hSqlVorschuss, "SELECT SUM(tkhaben-tksoll) INTO :frmNegativsalden.nVorschuss FROM tk WHERE tkarztnr = :frmNegativsalden.nArztNr AND tksa=2 AND @UPPER(tkbuchtext) like \'%DARL%\'");
			Int.SqlPrepar(this.hSqlVorschussVM, @"SELECT SUM(tkhaben-tksoll) INTO :frmNegativsalden.nVorschussVM FROM tka
WHERE tkaarztnr = :frmNegativsalden.nArztNr AND tkasa=2 AND @UPPER(tkabuchtext) like '%DARL%' AND tkadruckdatum > SYSDATE - 6 MONTHS");
			Int.SqlPrepar(this.hSqlTK, @"SELECT SUM(tkhaben-tksoll) INTO :frmNegativsalden.nDummy FROM tk WHERE tkarztnr = :frmNegativsalden.nArztNr
AND (tkbuchtext LIKE '%TSE%' OR tkbuchtext LIKE '%Sicherheitseinr%' OR tkbuchtext LIKE '%Mitgliedsbeitrag%' OR tkbuchtext LIKE '%Pegasus%') ");
			Int.SqlPrepar(this.hSqlTKVM, @"SELECT SUM(tkhaben-tksoll) INTO :frmNegativsalden.nDummy FROM tka WHERE tkaarztnr = :frmNegativsalden.nArztNr
AND (tkabuchtext LIKE '%TSE%' OR tkabuchtext LIKE '%Sicherheitseinr%' OR tkabuchtext LIKE '%Mitgliedsbeitrag%' OR tkabuchtext LIKE '%Pegasus%') ");

			this.strSelect = @"SELECT aarztnr, aname1, aort, avorhaben - avorsoll, sum(tkhaben) - sum(tksoll)
INTO :frmNegativsalden.nArztNr, :frmNegativsalden.strName, :frmNegativsalden.strOrt, :frmNegativsalden.nAVor, :frmNegativsalden.nTK
FROM a, tk WHERE aarztnr=tkarztnr(+) GROUP BY 1,2,3,4";
			Int.SqlHandleExec(this.hSqlNegativ, this.strSelect, "Hole Mitglieder mit negativen Salden", ref this.nErr);
			this.nFetch = this.hSqlNegativ.FetchNext();
			while (this.nFetch != Sys.FETCH_EOF) 
			{
				if ((this.nAVor + this.nTK) < 0) 
				{
					Int.SqlHandleExecuteXError(this.hSqlTK, "", "Hole TK");
					this.nFetch1 = this.hSqlTK.FetchNext();
					this.nTKAuswahl = this.nDummy;
					Int.SqlHandleExecuteXError(this.hSqlTKVM, "", "Hole TK Vormonate");
					this.nFetch1 = this.hSqlTKVM.FetchNext();
					this.nTKAuswahl = this.nTKAuswahl + this.nDummy;
					if (this.nDummy <= 0 && this.nDummy != SalNumber.Null)  // 28.05.21 YI: nur wenn TSE, Pegasus oder Beiträge gebucht wurden
					{
						Int.SqlHandleExecuteXError(this.hSqlZahl, "", "Hole Zahlungen");
						this.nFetch1 = this.hSqlZahl.FetchNext();
						Int.SqlHandleExecuteXError(this.hSqlZahlVM, "", "Hole Zahlungen Vormonate");
						this.nFetch1 = this.hSqlZahlVM.FetchNext();
						Int.SqlHandleExecuteXError(this.hSqlVorschuss, "", "Hole Vorschüsse");
						this.nFetch1 = this.hSqlVorschuss.FetchNext();
						Int.SqlHandleExecuteXError(this.hSqlVorschussVM, "", "Hole Vorschüsse Vormonate");
						this.nFetch1 = this.hSqlVorschussVM.FetchNext();
						if ((this.nZahl + this.nZahlVM + this.nVorschuss + this.nVorschussVM) < (this.nAVor + this.nTK) * -2) 
						{
							this.nRow = this.tblNegativ.InsertRow(Sys.TBL_MaxRow);
							this.tblNegativ.colArztNr.Number = this.nArztNr;
							this.tblNegativ.colEingang.Number = this.nZahl + this.nZahlVM + this.nVorschuss + this.nVorschussVM;
							this.tblNegativ.colName.Text = this.strName;
							if (this.strOrt != "") 
							{
								this.tblNegativ.colName.Text = this.tblNegativ.colName.Text + ", " + this.strOrt;
							}
							this.tblNegativ.colSaldo.Number = this.nAVor + this.nTK;
							this.tblNegativ.colSperre.Number = 0;
						}
					}
				}
				this.SetStatusBarText("Prüfe Mitglied " + this.nArztNr.ToString(0));
				this.nFetch = this.hSqlNegativ.FetchNext();
			}
			// Call SalTblPopulate( tblNegativ, hSqlNegativ, strSelect,TBL_FillAll )
			// Call SalTblSetFocusCell( tblNegativ, 0, tblNegativ.colSperre, 0, -1 )
			Sal.WaitCursor(false);
			#endregion
		}
		
		/// <summary>
		/// SAM_Close event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmNegativsalden_OnSAM_Close(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.tblNegativ.KillCellEdit();
			this.hSqlTK.Disconnect();
			this.hSqlTKVM.Disconnect();
			this.hSqlZahlVM.Disconnect();
			this.hSqlVorschussVM.Disconnect();
			this.hSqlZahl.Disconnect();
			this.hSqlVorschuss.Disconnect();
			this.hSqlNegativ.Disconnect();
			#endregion
		}
		
		/// <summary>
		/// tblNegativ WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tblNegativ_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Create:
					this.tblNegativ_OnSAM_Create(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Create event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tblNegativ_OnSAM_Create(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			Int.PalPrepareMTbl(this.tblNegativ, 0xffffd8);
			#endregion
		}
		
		/// <summary>
		/// pbDrucken WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbDrucken_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Create:
					this.pbDrucken_OnSAM_Create(sender, e);
					break;
				
				case Sys.SAM_Click:
					this.pbDrucken_OnSAM_Click(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Create event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbDrucken_OnSAM_Create(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.nAnz = MT.MTbl.PrintGetPrinterNames(this.strPrinters);
			this.strDevice = MT.MTbl.MTblPrintGetDefPrinterName();
			this.nOff = -1;
			this.nResult = 0;
			while (this.nResult < this.nAnz) 
			{
				this.lbPrinters.AddListItem(this.strPrinters[this.nResult]);
				if (this.strPrinters[this.nResult] == this.strDevice) 
				{
					this.nOff = this.nResult;
				}
				this.nResult = this.nResult + 1;
			}
			if (this.nOff >= 0) 
			{
				this.lbPrinters.SetListSelectedIndex(this.nOff);
			}

			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbDrucken_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			// Set nErr = 1
			// Set strReportName = 'ABSCHVOR'
			// Set hWndReport = SalReportTableView(tblAbschVor,hWndNULL,'ABSCHVOR.QRP',nErr)
			this.PrintParams.Init();

			this.strHeader = "Negativliste " + Int.PalDateToStrDE(SalDateTime.Current);
			this.PrintHeaderText.Init();
			this.PrintHeader.CenterText = this.strHeader;
			this.PrintParams.AddPageHeader(this.PrintHeader);
			this.PrintHeaderText.Init();
			this.PrintHeader.CenterText = " ";
			this.PrintParams.AddPageHeader(this.PrintHeader);

			this.PrintParams.DocName = "Negativliste";
			this.PrintParams.Orientation = MT.MTbl.MTP_OR_LANDSCAPE;
			this.strDevice = MT.MTbl.MTblPrintGetDefPrinterName();
			this.nResult = this.lbPrinters.GetListSelectedIndex();
			if (this.nResult == Sys.LB_Err) 
			{
				this.strDevice = MT.MTbl.MTblPrintGetDefPrinterName();
			}
			else
			{
				this.strDevice = this.lbPrinters.GetListItemText(this.nResult);
			}
			this.PrintParams.PrinterName = this.strDevice;
			// 07.11.13 F1760
			this.PrintParams.Language = MT.MTbl.MTP_LNG_GERMAN;
			this.PrintParams.GridType = MT.MTbl.MTP_GT_STANDARD4;

			MT.MTbl.Print(this.tblNegativ, this.PrintParams);

			#endregion
		}
		#endregion
		
		#region tblNegativ
		
		/// <summary>
		/// Child Table Window implementation.
		/// </summary>
		public partial class tblNegativTableWindow : SalTableWindow
		{
			// reference to the container form.
			private frmNegativsalden _frmNegativsalden = null;
			
			
			#region Constructors/Destructors
			
			/// <summary>
			/// Default Constructor.
			/// </summary>
			public tblNegativTableWindow()
			{
				// This call is required by the Windows Form Designer.
				InitializeComponent();
			}
			#endregion
			
			#region System Methods/Properties
			
			/// <summary>
			/// Parent form.
			/// </summary>
			private frmNegativsalden frmNegativsalden
			{
				[DebuggerStepThrough]
				get
				{
					if (_frmNegativsalden == null) 
					{
						_frmNegativsalden = (frmNegativsalden)this.FindForm();
					}
					return _frmNegativsalden;
				}
			}
			
			/// <summary>
			/// Returns the object instance associated with the window handle.
			/// </summary>
			/// <param name="handle"></param>
			/// <returns></returns>
			[DebuggerStepThrough]
			public static tblNegativTableWindow FromHandle(SalWindowHandle handle)
			{
				return ((tblNegativTableWindow)SalWindow.FromHandle(handle, typeof(tblNegativTableWindow)));
			}
			#endregion
		}
		#endregion
	}
}
