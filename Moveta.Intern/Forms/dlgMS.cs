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
	public partial class dlgMS : SalDialogBox
	{
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public dlgMS()
		{
			// Assign global reference.
			App.dlgMS = this;
			// This call is required by the Windows Form Designer.
			InitializeComponent();
		}
		#endregion
		
		#region System Methods/Properties
		
		/// <summary>
		/// Shows the modal dialog.
		/// </summary>
		/// <param name="owner"></param>
		/// <returns></returns>
		public static SalNumber ModalDialog(Control owner)
		{
			dlgMS dlg = new dlgMS();
			SalNumber ret = dlg.ShowDialog(owner);
			return ret;
		}
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static dlgMS FromHandle(SalWindowHandle handle)
		{
			return ((dlgMS)SalWindow.FromHandle(handle, typeof(dlgMS)));
		}
		#endregion
		
		#region Window Actions
		
		/// <summary>
		/// dlgMS WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dlgMS_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Create:
					this.dlgMS_OnSAM_Create(sender, e);
					break;
				
				case Sys.SAM_Close:
					this.dlgMS_OnSAM_Close(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Create event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dlgMS_OnSAM_Create(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			Sal.WaitCursor(false);
			this.dfMS.Number = 0;
			this.dfMSbis.Number = 9;
			#endregion
		}
		
		/// <summary>
		/// SAM_Close event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dlgMS_OnSAM_Close(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			Sal.WaitCursor(true);
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
			if (this.dfMS.Number == Sys.NUMBER_Null) 
			{
				this.dfMS.Number = 0;
			}
			if (this.dfMSbis.Number == Sys.NUMBER_Null) 
			{
				this.dfMSbis.Number = 9;
			}
			App.frmMain.nOPMS = this.dfMS.Number;
			App.frmMain.nOPMSbis = this.dfMSbis.Number;
			this.EndDialog(0);
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
	}
}
