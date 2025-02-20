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
	public partial class frmHalterDoppelt : SalFormWindow
	{
		#region Window Variables
		public SalSqlHandle hSqlHalter = SalSqlHandle.Null;
		public SalString strSelect = "";
		public SalBoolean bPFarbe = false;
		#endregion
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public frmHalterDoppelt()
		{
			// Assign global reference.
			App.frmHalterDoppelt = this;
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
		public static frmHalterDoppelt CreateWindow(Control owner)
		{
			frmHalterDoppelt frm = new frmHalterDoppelt();
			frm.Show(owner);
			return frm;
		}
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static frmHalterDoppelt FromHandle(SalWindowHandle handle)
		{
			return ((frmHalterDoppelt)SalWindow.FromHandle(handle, typeof(frmHalterDoppelt)));
		}
		#endregion
		
		#region Window Actions
		
		/// <summary>
		/// frmHalterDoppelt WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmHalterDoppelt_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_CreateComplete:
					this.frmHalterDoppelt_OnSAM_CreateComplete(sender, e);
					break;
				
				case Sys.SAM_Close:
					this.frmHalterDoppelt_OnSAM_Close(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_CreateComplete event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmHalterDoppelt_OnSAM_CreateComplete(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			Sal.WaitCursor(true);
			// Set strName=strName||'%'
			this.strSelect = @"select harztnr, hvn, hnn, hstr, hort, count(*)
into :frmHalterDoppelt.tblHalterDoppelt.colnArztNr,
:frmHalterDoppelt.tblHalterDoppelt.colsVN, :frmHalterDoppelt.tblHalterDoppelt.colsNN, :frmHalterDoppelt.tblHalterDoppelt.colsStr,
:frmHalterDoppelt.tblHalterDoppelt.colsOrt, :frmHalterDoppelt.tblHalterDoppelt.colnAnzahl
from H WHERE (hhalternein=0 or hhalternein IS NULL) AND hdeaktiv IS NULL GROUP BY 1,2,3,4,5 HAVING count(*)>1";
			Int.SqlConnection(ref this.hSqlHalter);
			this.tblHalterDoppelt.Populate(this.hSqlHalter, this.strSelect, Sys.TBL_FillAll);
			Sal.WaitCursor(false);
			#endregion
		}
		
		/// <summary>
		/// SAM_Close event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmHalterDoppelt_OnSAM_Close(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.hSqlHalter.Disconnect();
			#endregion
		}
		
		/// <summary>
		/// tblHalterDoppelt WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tblHalterDoppelt_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Create:
					this.tblHalterDoppelt_OnSAM_Create(sender, e);
					break;
				
				case Sys.SAM_RowHeaderClick:
					this.tblHalterDoppelt_OnSAM_RowHeaderClick(sender, e);
					break;
				
				// On SAM_FetchRowDone
				
				// If bPFarbe
				
				// Call XSalTblSetRowBackColor( hWndItem, lParam, 0xFFBFBF )
				
				// Set bPFarbe = FALSE
				
				// Else
				
				// Call XSalTblSetRowBackColor( hWndItem, lParam, 0xFFEFEF )
				
				// Set bPFarbe = TRUE
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Create event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tblHalterDoppelt_OnSAM_Create(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			Int.PalPrepareMTbl(this.tblHalterDoppelt, 0xffbfbf);
			#endregion
		}
		
		/// <summary>
		/// SAM_RowHeaderClick event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tblHalterDoppelt_OnSAM_RowHeaderClick(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			tblHalter.CreateWindow(this.tblHalterDoppelt, "frmHalterDoppelt", this.tblHalterDoppelt.colnArztNr.Number, 0, this.tblHalterDoppelt.colsNN.Text, this.tblHalterDoppelt.colsVN.Text, "", this.tblHalterDoppelt.colsStr.Text, this.tblHalterDoppelt.colsOrt.Text);
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
		
		#region tblHalterDoppelt
		
		/// <summary>
		/// Child Table Window implementation.
		/// </summary>
		public partial class tblHalterDoppeltTableWindow : SalTableWindow
		{
			// reference to the container form.
			private frmHalterDoppelt _frmHalterDoppelt = null;
			
			
			#region Constructors/Destructors
			
			/// <summary>
			/// Default Constructor.
			/// </summary>
			public tblHalterDoppeltTableWindow()
			{
				// This call is required by the Windows Form Designer.
				InitializeComponent();
			}
			#endregion
			
			#region System Methods/Properties
			
			/// <summary>
			/// Parent form.
			/// </summary>
			private frmHalterDoppelt frmHalterDoppelt
			{
				[DebuggerStepThrough]
				get
				{
					if (_frmHalterDoppelt == null) 
					{
						_frmHalterDoppelt = (frmHalterDoppelt)this.FindForm();
					}
					return _frmHalterDoppelt;
				}
			}
			
			/// <summary>
			/// Returns the object instance associated with the window handle.
			/// </summary>
			/// <param name="handle"></param>
			/// <returns></returns>
			[DebuggerStepThrough]
			public static tblHalterDoppeltTableWindow FromHandle(SalWindowHandle handle)
			{
				return ((tblHalterDoppeltTableWindow)SalWindow.FromHandle(handle, typeof(tblHalterDoppeltTableWindow)));
			}
			#endregion
		}
		#endregion
	}
}
