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
	/// <param name="strFrage"></param>
	/// <param name="strN1"></param>
	/// <param name="strN2"></param>
	/// <param name="strN3"></param>
	/// <param name="strN4"></param>
	/// <param name="strN5"></param>
	public partial class dlgAnschrift : SalDialogBox
	{
		#region Window Parameters
		public SalString strFrage;
		public SalString strN1;
		public SalString strN2;
		public SalString strN3;
		public SalString strN4;
		public SalString strN5;
		#endregion
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public dlgAnschrift(SalString strFrage, SalString strN1, SalString strN2, SalString strN3, SalString strN4, SalString strN5)
		{
			// Assign global reference.
			App.dlgAnschrift = this;
			// Window Parameters initialization.
			this.strFrage = strFrage;
			this.strN1 = strN1;
			this.strN2 = strN2;
			this.strN3 = strN3;
			this.strN4 = strN4;
			this.strN5 = strN5;
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
		public static SalNumber ModalDialog(Control owner, SalString strFrage, ref SalString strN1, ref SalString strN2, ref SalString strN3, ref SalString strN4, ref SalString strN5)
		{
			dlgAnschrift dlg = new dlgAnschrift(strFrage, strN1, strN2, strN3, strN4, strN5);
			SalNumber ret = dlg.ShowDialog(owner);
			strN1 = dlg.strN1;
			strN2 = dlg.strN2;
			strN3 = dlg.strN3;
			strN4 = dlg.strN4;
			strN5 = dlg.strN5;
			return ret;
		}
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static dlgAnschrift FromHandle(SalWindowHandle handle)
		{
			return ((dlgAnschrift)SalWindow.FromHandle(handle, typeof(dlgAnschrift)));
		}
		#endregion
		
		#region Window Actions
		
		/// <summary>
		/// dlgAnschrift WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dlgAnschrift_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Create:
					this.dlgAnschrift_OnSAM_Create(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Create event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dlgAnschrift_OnSAM_Create(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.dfFrage.Text = this.strFrage;
			this.dfN1.Text = this.strN1;
			this.dfN2.Text = this.strN2;
			this.dfN3.Text = this.strN3;
			this.dfN4.Text = this.strN4;
			this.dfN5.Text = this.strN5;
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
			this.strN1 = this.dfN1.Text;
			this.strN2 = this.dfN2.Text;
			this.strN3 = this.dfN3.Text;
			this.strN4 = this.dfN4.Text;
			this.strN5 = this.dfN5.Text;
			this.EndDialog(1);
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
