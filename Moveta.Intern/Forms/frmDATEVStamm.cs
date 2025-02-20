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
	public partial class frmDATEVStamm : SalFormWindow
	{
		#region Window Variables
		public SalSqlHandle hSqlUser = SalSqlHandle.Null;
		public SalString strSelect = "";
		public SalNumber nColor = 0;
		public SalNumber nRow = 0;
		public SalString lsDummy = "";
		#endregion
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public frmDATEVStamm()
		{
			// Assign global reference.
			App.frmDATEVStamm = this;
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
		public static frmDATEVStamm CreateWindow(Control owner)
		{
			frmDATEVStamm frm = new frmDATEVStamm();
			frm.Show(owner);
			return frm;
		}
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static frmDATEVStamm FromHandle(SalWindowHandle handle)
		{
			return ((frmDATEVStamm)SalWindow.FromHandle(handle, typeof(frmDATEVStamm)));
		}
		#endregion
		
		#region Methods
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalNumber PalNewRow()
		{
			#region Actions
			using (new SalContext(this))
			{
				if (PalSave()) 
				{
				}
				// If SalTblPopulate( tbldat,hSqlUser,strSelect,TBL_FillAll )
				PalHoleTabelle();
				nRow = tblDATEV.InsertRow(Sys.TBL_MaxRow);
				tblDATEV.col5Stellen.Number = 0;
				tblDATEV.colSKR.Text = "03";
				tblDATEV.SetFocusCell(nRow, tblDATEV.colCode, 0, -1);
			}

			return 0;
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalNumber PalSave()
		{
			#region Actions
			using (new SalContext(this))
			{
				Sal.WaitCursor(true);
				// Call SqlConnection( hSqlLogBugUpd)
				tblDATEV.KillCellEdit();
				if (tblDATEV.AnyRows((Sys.ROW_New | Sys.ROW_Edited), 0)) 
				{
					nRow = Sys.TBL_MinRow;
					while (true)
					{
						if (!(tblDATEV.FindNextRow(ref nRow, (Sys.ROW_New | Sys.ROW_Edited), 0))) 
						{
							break;
						}
						tblDATEV.SetFocusRow(nRow);
						if (tblDATEV.col5Stellen.Number == Sys.NUMBER_Null) 
						{
							tblDATEV.col5Stellen.Number = 0;
						}


						Int.SqlIstDa("FROM ds WHERE dscode = :frmDATEVStamm.tblDATEV.colCode", ref Var.bExists);
						if (Var.bExists) 
						{
							Int.SqlImmed(@"UPDATE ds SET
ds5stellig = :frmDATEVStamm.tblDATEV.col5Stellen,
dsberaternr = :frmDATEVStamm.tblDATEV.colBeraternr,
dsmandantennr = :frmDATEVStamm.tblDATEV.colMandantennr,
dsforderung = :frmDATEVStamm.tblDATEV.colRPForderung,
dsgeg0 = :frmDATEVStamm.tblDATEV.colRP0,
dsgeg5 = :frmDATEVStamm.tblDATEV.colRP5,
dsgeg7 = :frmDATEVStamm.tblDATEV.colRP7,
dsgeg16 = :frmDATEVStamm.tblDATEV.colRP16,
dsgeg19 = :frmDATEVStamm.tblDATEV.colRP19,
dsskr = :frmDATEVStamm.tblDATEV.colSKR
WHERE dscode = :frmDATEVStamm.tblDATEV.colCode");
						}
						else
						{
							Int.SqlImmed(@"INSERT INTO ds
(dscode, ds5stellig, dsberaternr, dsmandantennr, dsforderung, dsgeg0, dsgeg5, dsgeg7, dsgeg16, dsgeg19, dsskr)
VALUES( :frmDATEVStamm.tblDATEV.colCode, :frmDATEVStamm.tblDATEV.col5Stellen, :frmDATEVStamm.tblDATEV.colBeraternr, :frmDATEVStamm.tblDATEV.colMandantennr,
:frmDATEVStamm.tblDATEV.colRPForderung, :frmDATEVStamm.tblDATEV.colRP0, :frmDATEVStamm.tblDATEV.colRP5, :frmDATEVStamm.tblDATEV.colRP7,
:frmDATEVStamm.tblDATEV.colRP16, :frmDATEVStamm.tblDATEV.colRP19, :frmDATEVStamm.tblDATEV.colSKR )");
						}
						tblDATEV.SetRowFlags(nRow, (Sys.ROW_New | Sys.ROW_Edited), false);
					}
				}
				Sal.WaitCursor(false);
			}

			return 0;
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalNumber PalHoleTabelle()
		{
			#region Local Variables
			SqlLocals.PalHoleTabelleLocals locals = new SqlLocals.PalHoleTabelleLocals();
			#endregion
			
			#region Actions
			using (new SalContext(this, locals))
			{
				Int.SqlConnection(ref hSqlUser);
				strSelect = @"SELECT dscode, ds5stellig, dsberaternr, dsmandantennr, dsforderung, dsgeg0, dsgeg5, dsgeg7, dsgeg16, dsgeg19, dsskr
INTO :frmDATEVStamm.tblDATEV.colCode, :frmDATEVStamm.tblDATEV.col5Stellen, :frmDATEVStamm.tblDATEV.colBeraternr, :frmDATEVStamm.tblDATEV.colMandantennr,
:frmDATEVStamm.tblDATEV.colRPForderung, :frmDATEVStamm.tblDATEV.colRP0, :frmDATEVStamm.tblDATEV.colRP5, :frmDATEVStamm.tblDATEV.colRP7,
:frmDATEVStamm.tblDATEV.colRP16, :frmDATEVStamm.tblDATEV.colRP19, :frmDATEVStamm.tblDATEV.colSKR
FROM ds ORDER BY dscode ";
				if (tblDATEV.Populate(hSqlUser, strSelect, Sys.TBL_FillAll)) 
				{
					hSqlUser.Commit();
				}
				hSqlUser.Disconnect();

				return 0;
			}
			#endregion
		}
		#endregion
		
		#region Window Actions
		
		/// <summary>
		/// frmDATEVStamm WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmDATEVStamm_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Create:
					this.frmDATEVStamm_OnSAM_Create(sender, e);
					break;
				
				case Sys.SAM_Close:
					this.frmDATEVStamm_OnSAM_Close(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Create event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmDATEVStamm_OnSAM_Create(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			Sal.WaitCursor(true);
			// Call SqlConnection(hSqlUser)
			this.PalHoleTabelle();

			// Call SalContextMenuSetPopup( tblDATEV, 'frmDATEVStammRightClick', 0)

			Sal.WaitCursor(false);
			#endregion
		}
		
		/// <summary>
		/// SAM_Close event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmDATEVStamm_OnSAM_Close(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			// Call SqlDisconnect(hSqlUser)
			this.PalSave();
			#endregion
		}
		
		/// <summary>
		/// pbNeu WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbNeu_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Click:
					this.pbNeu_OnSAM_Click(sender, e);
					break;
				
				case Const.WM_KEYUP:
					this.pbNeu_OnWM_KEYUP(sender, e);
					break;
				
				case Sys.SAM_Create:
					this.pbNeu_OnSAM_Create(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbNeu_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.PalNewRow();
			#endregion
		}
		
		/// <summary>
		/// WM_KEYUP event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbNeu_OnWM_KEYUP(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			if (Sys.wParam == 45)  // Einf.
			{
				this.PalNewRow();
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Create event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbNeu_OnSAM_Create(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			e.Return = Int.XSalTooltipSetTextActive(this.pbNeu, "Neue Zeile hinzufügen.");
			return;
			#endregion
		}
		
		/// <summary>
		/// pbLoe WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbLoe_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Click:
					this.pbLoe_OnSAM_Click(sender, e);
					break;
				
				case Sys.SAM_Create:
					this.pbLoe_OnSAM_Create(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbLoe_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			if (this.tblDATEV.AnyRows((Sys.ROW_Selected | Sys.ROW_MarkDeleted), 0)) 
			{
				this.nRow = Sys.TBL_MinRow;
				while (true)
				{
					if (!(this.tblDATEV.FindNextRow(ref this.nRow, (Sys.ROW_Selected | Sys.ROW_MarkDeleted), 0))) 
					{
						break;
					}
					this.tblDATEV.SetFocusRow(this.nRow);
					Int.SqlImmed("DELETE FROM ds WHERE dscode = :frmDATEVStamm.tblDATEV.colCode");
					this.tblDATEV.DeleteRow(this.nRow, Sys.TBL_NoAdjust);
				}
				this.PalSave();
				this.tblDATEV.ResetTable();
				// If SalTblPopulate( tblBEZS,hSqlUser,strSelect,TBL_FillAll )
				this.PalHoleTabelle();
			}
			else
			{
				Sal.MessageBox(@"Vorher müssen die zu löschenden
Zeilen markiert werden !", "Markieren !", Sys.MB_IconAsterisk);
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Create event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbLoe_OnSAM_Create(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			e.Return = Int.XSalTooltipSetTextActive(this.pbLoe, "Schwarz markierte Zeilen löschen.");
			return;
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
			// Call SalDestroyWindow (hWndForm)
			this.PostMessage(Sys.SAM_Close, 0, 0);
			#endregion
		}
		#endregion
		
		#region tblDATEV
		
		/// <summary>
		/// Child Table Window implementation.
		/// </summary>
		public partial class tblDATEVTableWindow : SalTableWindow
		{
			// reference to the container form.
			private frmDATEVStamm _frmDATEVStamm = null;
			
			
			#region Constructors/Destructors
			
			/// <summary>
			/// Default Constructor.
			/// </summary>
			public tblDATEVTableWindow()
			{
				// This call is required by the Windows Form Designer.
				InitializeComponent();
			}
			#endregion
			
			#region System Methods/Properties
			
			/// <summary>
			/// Parent form.
			/// </summary>
			private frmDATEVStamm frmDATEVStamm
			{
				[DebuggerStepThrough]
				get
				{
					if (_frmDATEVStamm == null) 
					{
						_frmDATEVStamm = (frmDATEVStamm)this.FindForm();
					}
					return _frmDATEVStamm;
				}
			}
			
			/// <summary>
			/// Returns the object instance associated with the window handle.
			/// </summary>
			/// <param name="handle"></param>
			/// <returns></returns>
			[DebuggerStepThrough]
			public static tblDATEVTableWindow FromHandle(SalWindowHandle handle)
			{
				return ((tblDATEVTableWindow)SalWindow.FromHandle(handle, typeof(tblDATEVTableWindow)));
			}
			#endregion
		}
		#endregion
		
		#region SqlLocals
		
		/// <summary>
		/// Container class used to group the inner classes that contain
		/// the local variables that have been extracted from methods that use sql calls.
		/// </summary>
		private class SqlLocals
		{
			
			/// <summary>
			/// Contains the local variables that have been extracted from the
			/// method that uses sql calls and might need to access local bind variables.
			/// </summary>
			public class PalHoleTabelleLocals
			{
				public SalString strSelect1 = "";
			}
		}
		#endregion
	}
}
