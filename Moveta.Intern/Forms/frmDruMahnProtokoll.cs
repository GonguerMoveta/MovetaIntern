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
	/// </summary>
	public partial class frmDruMahnProtokoll : SalFormWindow
	{
		#region Window Variables
		public SalSqlHandle hSqlMV = SalSqlHandle.Null;
		public SalString strSelect = "";
		public SalNumber nFetchMV = 0;
		#endregion
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public frmDruMahnProtokoll()
		{
			// Assign global reference.
			App.frmDruMahnProtokoll = this;
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
		public static frmDruMahnProtokoll CreateWindow(Control owner)
		{
			frmDruMahnProtokoll frm = new frmDruMahnProtokoll();
			frm.Show(owner);
			return frm;
		}
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static frmDruMahnProtokoll FromHandle(SalWindowHandle handle)
		{
			return ((frmDruMahnProtokoll)SalWindow.FromHandle(handle, typeof(frmDruMahnProtokoll)));
		}
		#endregion
		
		#region Window Actions
		
		/// <summary>
		/// pbFuellen WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbFuellen_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Click:
					this.pbFuellen_OnSAM_Click(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbFuellen_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			Int.SqlConnection(ref this.hSqlMV);
			this.strSelect = "SELECT madatum FROM ma WHERE madeaktiv IS NULL ";
			if (!(this.dfArztNr.IsEmpty()) && this.dfArztNr.Number != 0) 
			{
				this.strSelect = this.strSelect + "AND maarztnr = :frmDruMahnProtokoll.dfArztNr ";
				if (!(this.dfA1von.IsEmpty()) && this.dfA1von.Number != 0) 
				{
					this.strSelect = this.strSelect + "AND mahalternr = :frmDruMahnProtokoll.dfA1von ";
				}
			}
			this.strSelect = this.strSelect + " GROUP BY 1 ORDER BY madatum DESC";
			this.cmbMahnDat.PopulateList(this.hSqlMV, this.strSelect);
			this.hSqlMV.Disconnect();
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
			// XXXXX

			Sal.WaitCursor(false);

			if (this.cmbMahnDat.IsEmpty() && (this.dfVon.IsEmpty() || this.dfBis.IsEmpty())) 
			{
				Sal.MessageBox("Bitte erst Mahn-Datum wählen !", "Achtung", Sys.MB_Ok);
				this.cmbMahnDat.SetFocus();
			}
			else
			{
				if (!(Int.PalFormFrei("Übergaben"))) 
				{
					Sal.MessageBox("Abschluß läuft. Protokolldruck nicht erlaubt !", "Achtung", Sys.MB_Ok);
				}
				else
				{
					App.frmMahn.dtDat1 = ((SalString)this.cmbMahnDat.Text).ToDate();
					App.frmMahn.dtMahnDatVon = this.dfVon.DateTime;
					App.frmMahn.dtMahnDatBis = this.dfBis.DateTime;
					App.frmMahn.nArztNr = this.dfArztNr.Number;
					App.frmMahn.nHalterNr = this.dfA1von.Number;
					// Set frmMahn.nErr = 1
					// Set frmMahn.strReportName = 'MAHNPROT'
					// Call PalReport(frmMahn,'MAHNPROT.QRP',
					// ':frmMahn.nTANr,:frmMahn.strTAN1,:frmMahn.strTAN2,:frmMahn.strTAStr,:frmMahn.strTAOrt,
					// :frmMahn.nTHNr,:frmMahn.strTHN1,
					// :frmMahn.dtRechDat, :frmMahn.dtMahnDat, :frmMahn.nRechNr,
					// :frmMahn.nDMRech, :frmMahn.nDMMahn, :frmMahn.nDMZins,
					// :frmMahn.nMS',
					// 'ArztNr, AName1, AName2, AStr, AOrt,
					// HalterNr, HName1,
					// RechDat, MahnDat, RechNr,
					// DMRech, DMMahn, DMZins, MS',nErr)
					// LL
					if (this.cbExtra.Checked) 
					{
						dlgLlDruck.ModalDialog(App.frmMain, "Mahnprotokoll.lst", "ED");
					}
					else
					{
						dlgLlDruck.ModalDialog(App.frmMain, "Mahnprotokoll.lst", "");
					}
				}

				this.DestroyWindow();
			}
			#endregion
		}
		#endregion
	}
}
