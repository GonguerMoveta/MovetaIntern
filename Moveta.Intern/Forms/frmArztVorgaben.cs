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
	public partial class frmArztVorgaben : SalFormWindow
	{
		#region Window Variables
		public SalBoolean bOK = false;
		#endregion
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public frmArztVorgaben()
		{
			// Assign global reference.
			App.frmArztVorgaben = this;
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
		public static frmArztVorgaben CreateWindow(Control owner)
		{
			frmArztVorgaben frm = new frmArztVorgaben();
			frm.Show(owner);
			return frm;
		}
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static frmArztVorgaben FromHandle(SalWindowHandle handle)
		{
			return ((frmArztVorgaben)SalWindow.FromHandle(handle, typeof(frmArztVorgaben)));
		}
		#endregion
		
		#region Methods
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalNumber PalHoleArzt()
		{
			#region Actions
			using (new SalContext(this))
			{
				Int.SqlIstDa(@" FROM a
WHERE aarztnr = :frmArztVorgaben.dfArztNr ", ref bOK);
				if (bOK) 
				{
					Int.SqlImmedSel(@"SELECT aname1, aname2, astr, aort, afaelltg, atitel, amahnint, askonto
INTO :frmArztVorgaben.dfName1, :frmArztVorgaben.dfName2, :frmArztVorgaben.dfStr, :frmArztVorgaben.dfOrt,
:frmArztVorgaben.dfFaellTg,  :frmArztVorgaben.dfTitel, :frmArztVorgaben.dfMahnInt, :frmArztVorgaben.dfSkonto
FROM a WHERE aarztnr = :frmArztVorgaben.dfArztNr");
					// 07.07.03 rausgenommen, da Fr.Seil dachte, alle hätten bereits die angegebenen Werte.
					// Set dfMahnIntA = dfMahnInt
					// Set dfFaellTgA = dfFaellTg
				}
				else
				{
					Sal.MessageBox("Dieser Arzt ist nicht vorhanden", "Nr. ungültig", Sys.MB_Ok);
					Sal.SetFocus(dfArztNr);
				}
			}

			return 0;
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalNumber PalLoescheMaske()
		{
			#region Actions
			using (new SalContext(this))
			{
				Sal.ClearField(dfArztNr);
				Sal.ClearField(dfName1);
				Sal.ClearField(dfName2);
				Sal.ClearField(dfStr);
				Sal.ClearField(dfOrt);
				Sal.ClearField(dfFaellTg);
				Sal.ClearField(dfMahnInt);
				Sal.ClearField(dfTitel);
				Sal.ClearField(dfSkonto);
				Sal.SetFocus(dfArztNr);
			}

			return 0;
			#endregion
		}
		#endregion
		
		#region Window Actions
		
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
			this.PalHoleArzt();
			#endregion
		}
		
		/// <summary>
		/// dfSkonto WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dfSkonto_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Validate:
					this.dfSkonto_OnSAM_Validate(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Validate event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dfSkonto_OnSAM_Validate(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			if (this.dfSkonto.Number > 0) 
			{
				Sal.MessageBox(@"""Mahngebühren ausbuchen"" wurde automatisch
auf 1 gesetzt, da sonst Skonto nicht abgezogen werden kann.", "Achtung", (Sys.MB_Ok | Sys.MB_IconExclamation));
				Int.SqlImmed(@"UPDATE a SET amgausbuchen = 1
WHERE aarztnr = :frmArztVorgaben.dfArztNr");
				Int.PalLog("ArztVorgaben MGausbuchen = 1 : Arzt" + Int.PalStrNum(this.dfArztNr.Number, 4, 0));
			}
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
			if (this.dfFaellTg.IsEmpty()) 
			{
				this.dfFaellTg.Number = 14;
			}
			if (this.dfMahnInt.IsEmpty()) 
			{
				this.dfMahnInt.Number = 1;
			}
			if (this.dfSkonto.IsEmpty()) 
			{
				this.dfSkonto.Number = 0;
			}
			Sal.WaitCursor(true);
			Int.SqlImmed(@"UPDATE a SET afaelltg = :frmArztVorgaben.dfFaellTg, amahnint = :frmArztVorgaben.dfMahnInt,
askonto = :frmArztVorgaben.dfSkonto
WHERE aarztnr = :frmArztVorgaben.dfArztNr");
			Sal.WaitCursor(false);
			Sal.MessageBeep(0);
			#endregion
		}
		
		/// <summary>
		/// pbFOk WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbFOk_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Click:
					this.pbFOk_OnSAM_Click(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbFOk_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			if (this.dfFaellTgA.IsEmpty()) 
			{
				this.dfFaellTgA.Number = this.dfFaellTg.Number;
			}
			if (Sys.IDYES == Sal.MessageBox(@"Wollen Sie wirklich die
Fälligkeitstage aller Halter von
Arzt " + Int.PalStrNum(this.dfArztNr.Number, 4, 0) + " auf " + Int.PalStrNum(this.dfFaellTgA.Number, 4, 0) + " setzen ?", "?", (Sys.MB_YesNo | Sys.MB_IconQuestion))) 
			{
				Sal.WaitCursor(true);
				Int.SqlImmed(@"UPDATE h SET hfaelltg = :frmArztVorgaben.dfFaellTgA
WHERE harztnr = :frmArztVorgaben.dfArztNr");
				Sal.WaitCursor(false);
				Sal.MessageBeep(0);
			}
			#endregion
		}
		
		/// <summary>
		/// pbSOk WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbSOk_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Click:
					this.pbSOk_OnSAM_Click(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbSOk_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			if (this.dfSkontoA.IsEmpty()) 
			{
				this.dfSkontoA.Number = this.dfSkonto.Number;
			}
			if (Sys.IDYES == Sal.MessageBox(@"Wollen Sie wirklich die
Skonto aller Halter von
Arzt " + Int.PalStrNum(this.dfArztNr.Number, 4, 0) + " auf " + Int.PalStrNum(this.dfSkontoA.Number, 4, 0) + " setzen ?", "?", (Sys.MB_YesNo | Sys.MB_IconQuestion))) 
			{
				Sal.WaitCursor(true);
				Int.SqlImmed(@"UPDATE h SET hSkonto = :frmArztVorgaben.dfSkontoA
WHERE harztnr = :frmArztVorgaben.dfArztNr");
				Sal.WaitCursor(false);
				Sal.MessageBeep(0);
			}
			#endregion
		}
		
		/// <summary>
		/// pbMOk WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbMOk_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Click:
					this.pbMOk_OnSAM_Click(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbMOk_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			if (this.dfMahnIntA.IsEmpty()) 
			{
				this.dfMahnIntA.Number = this.dfMahnInt.Number;
			}
			if (Sys.IDYES == Sal.MessageBox(@"Wollen Sie wirklich die
Mahnintervalle aller Halter von
Arzt " + Int.PalStrNum(this.dfArztNr.Number, 4, 0) + " auf " + Int.PalStrNum(this.dfMahnIntA.Number, 4, 0) + " setzen ?", "?", (Sys.MB_YesNo | Sys.MB_IconQuestion))) 
			{
				Sal.WaitCursor(true);
				Int.SqlImmed(@"UPDATE h SET hmahnint = :frmArztVorgaben.dfMahnIntA
WHERE harztnr = :frmArztVorgaben.dfArztNr");
				// 02.12.08
				Int.PalLog("AV: MI " + Int.PalStrNum(this.dfArztNr.Number, 4, 0) + " auf " + Int.PalStrNum(this.dfMahnIntA.Number, 4, 0));

				Sal.WaitCursor(false);
				Sal.MessageBeep(0);
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
			this.DestroyWindow();
			#endregion
		}
		#endregion
	}
}
