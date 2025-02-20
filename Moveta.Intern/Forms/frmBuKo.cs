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
	public partial class frmBuKo : SalFormWindow
	{
		#region Window Variables
		public SalSqlHandle hSqlBuKo = SalSqlHandle.Null;
		public SalSqlHandle hSqlBuKo1 = SalSqlHandle.Null;
		public SalSqlHandle hSqlBuKo2 = SalSqlHandle.Null;
		public SalNumber nSumme = 0;
		#endregion
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public frmBuKo()
		{
			// Assign global reference.
			App.frmBuKo = this;
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
		public static frmBuKo CreateWindow(Control owner)
		{
			frmBuKo frm = new frmBuKo();
			frm.Show(owner);
			return frm;
		}
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static frmBuKo FromHandle(SalWindowHandle handle)
		{
			return ((frmBuKo)SalWindow.FromHandle(handle, typeof(frmBuKo)));
		}
		#endregion
		
		#region Window Actions
		
		/// <summary>
		/// frmBuKo WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmBuKo_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Create:
					this.frmBuKo_OnSAM_Create(sender, e);
					break;
				
				case Sys.SAM_Close:
					this.frmBuKo_OnSAM_Close(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Create event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmBuKo_OnSAM_Create(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			Int.SqlConnection(ref this.hSqlBuKo);
			Int.SqlConnection(ref this.hSqlBuKo1);
			Int.SqlConnection(ref this.hSqlBuKo2);
			this.dfDatum.DateTime = SalDateTime.Current.YearBegin();
			#endregion
		}
		
		/// <summary>
		/// SAM_Close event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmBuKo_OnSAM_Close(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.hSqlBuKo.Disconnect();
			this.hSqlBuKo1.Disconnect();
			this.hSqlBuKo2.Disconnect();
			#endregion
		}
		
		/// <summary>
		/// dfBelNr WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dfBelNr_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Validate:
					this.dfBelNr_OnSAM_Validate(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Validate event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dfBelNr_OnSAM_Validate(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			Sal.WaitCursor(true);
			// 10.11.11 +   AND fibeldat< @YEARBEG(SYSDATE + 1 YEAR)
			// 24.06.20 Ä1892 + 17761
			this.tblFI.Populate(this.hSqlBuKo, @"SELECT fiarztnr, sum(fidm)
INTO :frmBuKo.tblFI.colArztNr, :frmBuKo.tblFI.colDM
FROM fi
WHERE fibelnr = " + this.dfBelNr.Number.ToString(0) + @"  AND fisoll<>16410 AND fihaben IN (16000, 16410, 16510, 18100, 18300, 80110, 80111, 80120, 15000, 17760, 17750)
and fibeldat>= :frmBuKo.dfDatum  AND fibeldat< @YEARBEG(SYSDATE + 1 YEAR)
GROUP BY 1", Sys.TBL_FillAll);
			this.tblFISH.Populate(this.hSqlBuKo1, @"SELECT fisoll, fihaben, sum(fidm)
INTO :frmBuKo.tblFISH.colSoll, :frmBuKo.tblFISH.colHaben, :frmBuKo.tblFISH.colDM
FROM fi
WHERE fibelnr = " + this.dfBelNr.Number.ToString(0) + @"
AND NOT (fisoll=16000 and fihaben IN (81300, 81301, 81340))
AND NOT (fisoll=16410 and fihaben=16000)
AND NOT (fisoll=16420 and fihaben=16000)
and fibeldat>= :frmBuKo.dfDatum    AND fibeldat< @YEARBEG(SYSDATE + 1 YEAR)
GROUP BY 1,2", Sys.TBL_FillAll);
			if (this.cbVM.Checked)  // Vormonat
			{
				this.tblTK.Populate(this.hSqlBuKo2, @"SELECT tkaarztnr, sum(tkhaben)
INTO :frmBuKo.tblTK.colArztNr, :frmBuKo.tblTK.colDM
FROM tkalt
WHERE tkabelnr = :frmBuKo.dfBelNr
and tkabeldat>= :frmBuKo.dfDatum AND tkadeaktiv IS NULL
GROUP BY 1", Sys.TBL_FillAll);
			}
			else
			{
				this.tblTK.Populate(this.hSqlBuKo2, @"SELECT tkarztnr, sum(tkhaben)
INTO :frmBuKo.tblTK.colArztNr, :frmBuKo.tblTK.colDM
FROM tk
WHERE tkbelnr = " + this.dfBelNr.Number.ToString(0) + @"
and tkbeldat>= :frmBuKo.dfDatum
GROUP BY 1", Sys.TBL_FillAll);
			}
			this.dfSumFI.Number = this.tblFI.ColumnSum(2, 0, 0);
			this.dfSumTK.Number = this.tblTK.ColumnSum(2, 0, 0);
			this.dfSumDiff.Number = this.dfSumFI.Number - this.dfSumTK.Number;

			Sal.WaitCursor(false);
			#endregion
		}
		
		/// <summary>
		/// pbSummeFISH WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbSummeFISH_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Click:
					this.pbSummeFISH_OnSAM_Click(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbSummeFISH_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			if (this.tblFISH.AnyRows(Sys.ROW_Selected, 0)) 
			{
				Sal.WaitCursor(true);
				this.dfSumFISH.Number = 0;
				this.nSumme = 0;

				Var.nRow = Sys.TBL_MinRow;
				while (true)
				{
					if (!(this.tblFISH.FindNextRow(ref Var.nRow, Sys.ROW_Selected, 0))) 
					{
						break;
					}
					this.tblFISH.SetFocusRow(Var.nRow);
					this.nSumme = this.nSumme + this.tblFISH.colDM.Number;
				}
				this.dfSumFISH.Number = this.nSumme;

				Sal.WaitCursor(false);
			}
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
		
		#region tblFI
		
		/// <summary>
		/// Child Table Window implementation.
		/// </summary>
		public partial class tblFITableWindow : SalTableWindow
		{
			// reference to the container form.
			private frmBuKo _frmBuKo = null;
			
			
			#region Constructors/Destructors
			
			/// <summary>
			/// Default Constructor.
			/// </summary>
			public tblFITableWindow()
			{
				// This call is required by the Windows Form Designer.
				InitializeComponent();
			}
			#endregion
			
			#region System Methods/Properties
			
			/// <summary>
			/// Parent form.
			/// </summary>
			private frmBuKo frmBuKo
			{
				[DebuggerStepThrough]
				get
				{
					if (_frmBuKo == null) 
					{
						_frmBuKo = (frmBuKo)this.FindForm();
					}
					return _frmBuKo;
				}
			}
			
			/// <summary>
			/// Returns the object instance associated with the window handle.
			/// </summary>
			/// <param name="handle"></param>
			/// <returns></returns>
			[DebuggerStepThrough]
			public static tblFITableWindow FromHandle(SalWindowHandle handle)
			{
				return ((tblFITableWindow)SalWindow.FromHandle(handle, typeof(tblFITableWindow)));
			}
			#endregion
		}
		#endregion
		
		#region tblTK
		
		/// <summary>
		/// Child Table Window implementation.
		/// </summary>
		public partial class tblTKTableWindow : SalTableWindow
		{
			// reference to the container form.
			private frmBuKo _frmBuKo = null;
			
			
			#region Constructors/Destructors
			
			/// <summary>
			/// Default Constructor.
			/// </summary>
			public tblTKTableWindow()
			{
				// This call is required by the Windows Form Designer.
				InitializeComponent();
			}
			#endregion
			
			#region System Methods/Properties
			
			/// <summary>
			/// Parent form.
			/// </summary>
			private frmBuKo frmBuKo
			{
				[DebuggerStepThrough]
				get
				{
					if (_frmBuKo == null) 
					{
						_frmBuKo = (frmBuKo)this.FindForm();
					}
					return _frmBuKo;
				}
			}
			
			/// <summary>
			/// Returns the object instance associated with the window handle.
			/// </summary>
			/// <param name="handle"></param>
			/// <returns></returns>
			[DebuggerStepThrough]
			public static tblTKTableWindow FromHandle(SalWindowHandle handle)
			{
				return ((tblTKTableWindow)SalWindow.FromHandle(handle, typeof(tblTKTableWindow)));
			}
			#endregion
		}
		#endregion
		
		#region tblFISH
		
		/// <summary>
		/// Child Table Window implementation.
		/// </summary>
		public partial class tblFISHTableWindow : SalTableWindow
		{
			// reference to the container form.
			private frmBuKo _frmBuKo = null;
			
			
			#region Constructors/Destructors
			
			/// <summary>
			/// Default Constructor.
			/// </summary>
			public tblFISHTableWindow()
			{
				// This call is required by the Windows Form Designer.
				InitializeComponent();
			}
			#endregion
			
			#region System Methods/Properties
			
			/// <summary>
			/// Parent form.
			/// </summary>
			private frmBuKo frmBuKo
			{
				[DebuggerStepThrough]
				get
				{
					if (_frmBuKo == null) 
					{
						_frmBuKo = (frmBuKo)this.FindForm();
					}
					return _frmBuKo;
				}
			}
			
			/// <summary>
			/// Returns the object instance associated with the window handle.
			/// </summary>
			/// <param name="handle"></param>
			/// <returns></returns>
			[DebuggerStepThrough]
			public static tblFISHTableWindow FromHandle(SalWindowHandle handle)
			{
				return ((tblFISHTableWindow)SalWindow.FromHandle(handle, typeof(tblFISHTableWindow)));
			}
			#endregion
		}
		#endregion
	}
}
