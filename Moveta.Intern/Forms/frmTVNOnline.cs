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
	public partial class frmTVNOnline : SalFormWindow
	{
		#region Window Variables
		public SalNumber nZulassung = 0;
		public SalSqlHandle hSqlUser = SalSqlHandle.Null;
		public SalString strSelect = "";
		public SalNumber nColor = 0;
		public SalNumber nRow = 0;
		#endregion
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public frmTVNOnline()
		{
			// Assign global reference.
			App.frmTVNOnline = this;
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
		public static frmTVNOnline CreateWindow(Control owner)
		{
			frmTVNOnline frm = new frmTVNOnline();
			frm.Show(owner);
			return frm;
		}
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static frmTVNOnline FromHandle(SalWindowHandle handle)
		{
			return ((frmTVNOnline)SalWindow.FromHandle(handle, typeof(frmTVNOnline)));
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
				tblUser.Populate(hSqlUser, strSelect, Sys.TBL_FillAll);
				nRow = tblUser.InsertRow(Sys.TBL_MaxRow);
				tblUser.colArztNr.Number = dfArztNr.Number;
				tblUser.SetFocusCell(nRow, tblUser.colPw, 0, -1);
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
				tblUser.KillCellEdit();
				if (tblUser.AnyRows((Sys.ROW_New | Sys.ROW_Edited), 0)) 
				{
					nRow = Sys.TBL_MinRow;
					while (true)
					{
						if (!(tblUser.FindNextRow(ref nRow, (Sys.ROW_New | Sys.ROW_Edited), 0))) 
						{
							break;
						}
						tblUser.SetFocusRow(nRow);

						if (this.tblUser.colPw.Text != "") 
						{
							Int.SqlIstDa("FROM tauser WHERE taarztnr = :frmTVNOnline.tblUser.colArztNr AND tapw = :frmTVNOnline.tblUser.colPw", ref Var.bExists);
							if (Var.bExists) 
							{
								Int.SqlImmed("DELETE FROM tauser WHERE taarztnr = :frmTVNOnline.tblUser.colArztNr AND tapw = :frmTVNOnline.tblUser.colPw");
								Var.bExists = false;
							}
							if (!(Var.bExists)) 
							{
								Int.SqlImmed(@"INSERT INTO tauser
( taarztnr, tapw, tazulassung )
VALUES(:frmTVNOnline.tblUser.colArztNr, :frmTVNOnline.tblUser.colPw,  :frmTVNOnline.tblUser.colZulassung )");
							}
						}
						tblUser.SetRowFlags(nRow, (Sys.ROW_New | Sys.ROW_Edited), false);
					}
				}
				Sal.WaitCursor(false);
			}

			return 0;
			#endregion
		}
		#endregion
		
		#region Window Actions
		
		/// <summary>
		/// frmTVNOnline WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmTVNOnline_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Create:
					this.frmTVNOnline_OnSAM_Create(sender, e);
					break;
                //FC:FINAL: use SAM_Close instead of SAM_Destroy
                case Sys.SAM_Close:
					this.frmTVNOnline_OnSAM_Destroy(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Create event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmTVNOnline_OnSAM_Create(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			Sal.WaitCursor(true);
			this.strSelect = @"SELECT taarztnr, tapw, tazulassung
INTO :frmTVNOnline.tblUser.colArztNr, :frmTVNOnline.tblUser.colPw, :frmTVNOnline.tblUser.colZulassung
FROM tauser WHERE taarztnr = :frmTVNOnline.dfArztNr ORDER BY tazulassung, tapw";
			Int.SqlConnection(ref this.hSqlUser);
			// Call SalTblPopulate( tblUser,hSqlUser,strSelect,TBL_FillAll )

			Sal.WaitCursor(false);
			#endregion
		}
		
		/// <summary>
		/// SAM_Destroy event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmTVNOnline_OnSAM_Destroy(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.hSqlUser.Disconnect();
			this.PalSave();
			#endregion
		}
		
		/// <summary>
		/// dfArztNr WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dfArztNr_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Validate:
					this.dfArztNr_OnSAM_Validate(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Validate event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dfArztNr_OnSAM_Validate(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.PalSave();
			Int.SqlImmedSel(@"SELECT aname1,aname2
INTO :frmTVNOnline.dfSArztName,:frmTVNOnline.dfSArztName2
from A where aarztnr=:frmTVNOnline.dfArztNr");
			// 20.08.08 A268
			// Call SqlImmedSel('SELECT  tapw, tazulassung
			// INTO  :frmTVNOnline.dfPasswort, :frmTVNOnline.nZulassung
			// from SYSADM.TAUSER where taarztnr=:frmTVNOnline.dfArztNr')
			// If nZulassung = 0 or nZulassung = NUMBER_Null
			// Set rbInhaber = TRUE
			// Else
			// Set rbMitarbeiter = TRUE
			this.tblUser.Populate(this.hSqlUser, this.strSelect, Sys.TBL_FillAll);

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
			if (this.tblUser.AnyRows((Sys.ROW_Selected | Sys.ROW_MarkDeleted), 0)) 
			{
				this.nRow = Sys.TBL_MinRow;
				while (true)
				{
					if (!(this.tblUser.FindNextRow(ref this.nRow, (Sys.ROW_Selected | Sys.ROW_MarkDeleted), 0))) 
					{
						break;
					}
					this.tblUser.SetFocusRow(this.nRow);
					Int.SqlImmed("DELETE FROM tauser WHERE taarztnr = :frmTVNOnline.tblUser.colArztNr AND tapw = :frmTVNOnline.tblUser.colPw");
				}
				this.PalSave();
				this.tblUser.ResetTable();
				this.tblUser.Populate(this.hSqlUser, this.strSelect, Sys.TBL_FillAll);
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
			this.DestroyWindow();
			#endregion
		}
		#endregion
		
		#region tblUser
		
		/// <summary>
		/// Child Table Window implementation.
		/// </summary>
		public partial class tblUserTableWindow : SalTableWindow
		{
			// reference to the container form.
			private frmTVNOnline _frmTVNOnline = null;
			
			
			#region Constructors/Destructors
			
			/// <summary>
			/// Default Constructor.
			/// </summary>
			public tblUserTableWindow()
			{
				// This call is required by the Windows Form Designer.
				InitializeComponent();
			}
			#endregion
			
			#region System Methods/Properties
			
			/// <summary>
			/// Parent form.
			/// </summary>
			private frmTVNOnline frmTVNOnline
			{
				[DebuggerStepThrough]
				get
				{
					if (_frmTVNOnline == null) 
					{
						_frmTVNOnline = (frmTVNOnline)this.FindForm();
					}
					return _frmTVNOnline;
				}
			}
			
			/// <summary>
			/// Returns the object instance associated with the window handle.
			/// </summary>
			/// <param name="handle"></param>
			/// <returns></returns>
			[DebuggerStepThrough]
			public static tblUserTableWindow FromHandle(SalWindowHandle handle)
			{
				return ((tblUserTableWindow)SalWindow.FromHandle(handle, typeof(tblUserTableWindow)));
			}
			#endregion
		}
		#endregion
	}
}
