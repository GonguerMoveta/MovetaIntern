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
	/// neue Halter anlegen
	/// </summary>
	/// <param name="nArztNr"></param>
	/// <param name="strBearbeiter"></param>
	/// <param name="dtBearbDatum"></param>
	public partial class frmDublPruef : SalFormWindow
	{
		#region Window Parameters
		public SalNumber nArztNr;
		public SalString strBearbeiter;
		public SalDateTime dtBearbDatum;
		#endregion
		
		#region Window Variables
		public SalSqlHandle hSqlD1 = SalSqlHandle.Null;
		public SalSqlHandle hSqlD2 = SalSqlHandle.Null;
		public SalString strSelect1 = "";
		public SalString strSelect2 = "";
		public SalNumber nArztNr2 = 0;
		public SalString strPraxArt = "";
		public SalBoolean bExists = false;
		public SalNumber nTA = 0;
		public SalNumber nTH = 0;
		public SalNumber nRechNr = 0;
		public SalDateTime dtSuchDatum = SalDateTime.Null;
		public SalString strSuchRechText = "";
		public SalDateTime dtDatum = SalDateTime.Null;
		public SalString strRechText = "";
		public SalNumber nCount = 0;
		public SalNumber nFetch1 = 0;
		public SalNumber nFetch2 = 0;
		public SalNumber nRow = 0;
		public SalString strMatch = "";
		public SalNumber nErr = 0;
		public SalWindowHandle hWndReport = SalWindowHandle.Null;
		public SalString strDevice = "";
		public SalString strDriver = "";
		public SalString strPort = "";
		public SalString strOldDevice = "";
		public SalString strOldDriver = "";
		public SalString strOldPort = "";
		public SalArray<SalNumber> nArray = new SalArray<SalNumber>(5);
		#endregion
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public frmDublPruef(SalNumber nArztNr, SalString strBearbeiter, SalDateTime dtBearbDatum)
		{
			// Assign global reference.
			App.frmDublPruef = this;
			// Window Parameters initialization.
			this.nArztNr = nArztNr;
			this.strBearbeiter = strBearbeiter;
			this.dtBearbDatum = dtBearbDatum;
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
		public static frmDublPruef CreateWindow(Control owner, SalNumber nArztNr, SalString strBearbeiter, SalDateTime dtBearbDatum)
		{
			frmDublPruef frm = new frmDublPruef(nArztNr, strBearbeiter, dtBearbDatum);
			frm.Show(owner);
			return frm;
		}
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static frmDublPruef FromHandle(SalWindowHandle handle)
		{
			return ((frmDublPruef)SalWindow.FromHandle(handle, typeof(frmDublPruef)));
		}
		#endregion
		
		#region Window Actions
		
		/// <summary>
		/// frmDublPruef WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmDublPruef_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Create:
					this.frmDublPruef_OnSAM_Create(sender, e);
					break;
				
				case Sys.SAM_Close:
					this.frmDublPruef_OnSAM_Close(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Create event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmDublPruef_OnSAM_Create(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.dfArztNr.Number = this.nArztNr;
			Int.SqlImmedSel(@"SELECT AKZPRAXART,ANR2 INTO :frmDublPruef.strPraxArt,:frmDublPruef.nArztNr2
FROM A WHERE AARZTNR = :frmDublPruef.dfArztNr");
			if (this.strPraxArt == "2") 
			{
				this.dfArztNr2.Number = this.nArztNr;
				this.dfArztNr.Number = this.nArztNr2;
				this.nArztNr = this.dfArztNr.Number;
				this.nArztNr2 = this.dfArztNr2.Number;
			}
			else if (this.strPraxArt == "1") 
			{
				this.dfArztNr2.Number = this.nArztNr2;
				this.dfArztNr.Number = this.nArztNr;
			}
			Int.SqlConnection(ref this.hSqlD1);
			Int.SqlConnection(ref this.hSqlD2);
			this.strSelect2 = @"SELECT rparztnr, rprechnr, rpdatum, rprechtext
INTO :frmDublPruef.nTA, :frmDublPruef.nRechNr, :frmDublPruef.dtDatum, :frmDublPruef.strRechText
FROM rp
WHERE (rparztnr = :frmDublPruef.nArztNr OR rparztnr = :frmDublPruef.nArztNr2) AND rphalternr = :frmDublPruef.nTH
AND rpdatum = :frmDublPruef.dtSuchDatum AND rprechtext = :frmDublPruef.strSuchRechText";
			// :frmDublPruef.nTH
			this.hSqlD2.Prepare(this.strSelect2);
			// 11.08.99	! 20.08.12 F1394 auskommentiert
			// If SalPrtGetDefault( strDevice, strDriver, strPort )
			// Set dfDrucker = strDevice || ' an ' || strPort
			// Set strOldDevice = strDevice
			// Set strOldDriver = strDriver
			// Set strOldPort = strPort
			#endregion
		}
		
		/// <summary>
		/// SAM_Close event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmDublPruef_OnSAM_Close(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.hSqlD1.Disconnect();
			this.hSqlD2.Disconnect();
			// 20.08.12 F1394 auskommentiert
			// Call SalPrtSetDefault( strOldDevice, strOldDriver, strOldPort )
			#endregion
		}
		
		/// <summary>
		/// pbOk WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbOk_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Click:
					this.pbOk_OnSAM_Click(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbOk_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			Sal.WaitCursor(true);

			this.strSelect1 = @"SELECT lehalternr, lename1, lepdatum, leprechtext
INTO :frmDublPruef.nTH, :frmDublPruef.strMatch, :frmDublPruef.dtSuchDatum, :frmDublPruef.strSuchRechText
FROM le, lep
WHERE (leparztnr = :frmDublPruef.nArztNr OR leparztnr = :frmDublPruef.nArztNr2)
AND lepbearbeiter = :frmDublPruef.strBearbeiter AND lep.lebearbdatum = :frmDublPruef.dtBearbDatum
AND learztnr=leparztnr AND lehalternr=lephalternr AND leposnr=lepposnr
AND lebearbeiter=lepbearbeiter AND le.lebearbdatum=lep.lebearbdatum";
			Int.SqlHandleExec(this.hSqlD1, this.strSelect1, "Fehler DublPruef Sql1", ref this.nCount);
			this.nFetch1 = this.hSqlD1.FetchNext();
			while (this.nFetch1 != Sys.FETCH_EOF) 
			{
				this.SetStatusBarText("Suche Dubletten für " + this.strMatch + " " + Int.SalDateToStrX(this.dtSuchDatum));

				Int.SqlHandleExecuteXError(this.hSqlD2, this.strSelect2, "Fehler Sql D2");
				// Call SqlExecute(hSqlD2)
				this.nFetch2 = this.hSqlD2.FetchNext();
				while (this.nFetch2 != Sys.FETCH_EOF) 
				{
					this.nRow = this.tblDubl.InsertRow(Sys.TBL_MaxRow);
					this.tblDubl.colArztNr.Number = this.nTA;
					this.tblDubl.colRechNr.Number = this.nRechNr;
					this.tblDubl.colDatum.DateTime = this.dtDatum;
					this.tblDubl.colBez.Text = this.strRechText;

					// Call SqlImmed('DELETE FROM lep
					// WHERE (leparztnr = :frmDublPruef.nArztNr OR leparztnr = :frmDublPruef.nArztNr2) AND lephalternr = :frmDublPruef.nTH
					// AND lepdatum = :frmDublPruef.dtSuchDatum AND leprechtext = :frmDublPruef.strSuchRechText')

					this.nFetch2 = this.hSqlD2.FetchNext();
				}
				this.hSqlD2.Commit();

				this.nFetch1 = this.hSqlD1.FetchNext();
			}
			this.hSqlD1.Commit();
			Int.SqlImmed(@"UPDATE le SET lestatus='DUBL'
WHERE (learztnr = :frmDublPruef.nArztNr OR learztnr = :frmDublPruef.nArztNr2)
AND lebearbeiter = :frmDublPruef.strBearbeiter AND lebearbdatum = :frmDublPruef.dtBearbDatum");

			// 14.03.19 Ä1829
			// Call frmDiskAbr.PalHoleTabelle(  )

			this.SetStatusBarText("Prüfung beendet");
			Sal.MessageBeep(-1);
			Sal.WaitCursor(false);
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
				case Sys.SAM_Click:
					this.pbDrucken_OnSAM_Click(sender, e);
					break;
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
			// 20.08.12 F1394
			// Call SalReportTableCreate('DublPrf', tblDubl, nErr)
			// Set nArray[RPT_PrintParamOptions] = RPT_PrintAll
			// Set nArray[RPT_PrintParamFirstPage] = 1
			// Set nArray[RPT_PrintParamLastPage] = 999
			// Set nArray[RPT_PrintParamCopies] = 1
			// Call SalReportTablePrint(tblDubl,  'DublPrf', nArray, nErr)
			dlgLlDruck.ModalDialog(App.frmMain, "DublPruef.lst", "");
			#endregion
		}
		
		/// <summary>
		/// pbAbbruch WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbAbbruch_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Click:
					this.pbAbbruch_OnSAM_Click(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbAbbruch_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.PostMessage(Sys.SAM_Close, 0, 0);
			#endregion
		}
		#endregion
		
		#region tblDubl
		
		/// <summary>
		/// Child Table Window implementation.
		/// </summary>
		public partial class tblDublTableWindow : SalTableWindow
		{
			// reference to the container form.
			private frmDublPruef _frmDublPruef = null;
			
			
			#region Constructors/Destructors
			
			/// <summary>
			/// Default Constructor.
			/// </summary>
			public tblDublTableWindow()
			{
				// This call is required by the Windows Form Designer.
				InitializeComponent();
			}
			#endregion
			
			#region System Methods/Properties
			
			/// <summary>
			/// Parent form.
			/// </summary>
			private frmDublPruef frmDublPruef
			{
				[DebuggerStepThrough]
				get
				{
					if (_frmDublPruef == null) 
					{
						_frmDublPruef = (frmDublPruef)this.FindForm();
					}
					return _frmDublPruef;
				}
			}
			
			/// <summary>
			/// Returns the object instance associated with the window handle.
			/// </summary>
			/// <param name="handle"></param>
			/// <returns></returns>
			[DebuggerStepThrough]
			public static tblDublTableWindow FromHandle(SalWindowHandle handle)
			{
				return ((tblDublTableWindow)SalWindow.FromHandle(handle, typeof(tblDublTableWindow)));
			}
			#endregion
		}
		#endregion
	}
}
