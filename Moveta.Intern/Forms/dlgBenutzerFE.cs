// <ppj name="SqlTranslatorTestN" date="25.08.2024 08:40:01" id="FC1F642056F771FC8BE3BB9BEDBC7AFF0AD58901"/>
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
using PPJ.Runtime;
using PPJ.Runtime.Sql;
using PPJ.Runtime.Windows;

namespace Moveta.Intern
{
	
	/// <summary>
	/// </summary>
	/// <param name="strBenutzer"></param>
	public partial class dlgBenutzerFE : SalDialogBox
	{
		#region Window Parameters
		public SalString strBenutzer;
		#endregion
		
		#region Window Variables
		public SalString sSql = "";
		public SalSqlHandle hSqlBenutzer = SalSqlHandle.Null;
		#endregion
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public dlgBenutzerFE(SalString strBenutzer)
		{
			// Assign global reference.
			App.dlgBenutzerFE = this;
			// Window Parameters initialization.
			this.strBenutzer = strBenutzer;
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
		public static SalNumber ModalDialog(Control owner, ref SalString strBenutzer)
		{
			dlgBenutzerFE dlg = new dlgBenutzerFE(strBenutzer);
			SalNumber ret = dlg.ShowDialog(owner);
			strBenutzer = dlg.strBenutzer;
			return ret;
		}
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static dlgBenutzerFE FromHandle(SalWindowHandle handle)
		{
			return ((dlgBenutzerFE)SalWindow.FromHandle(handle, typeof(dlgBenutzerFE)));
		}
		#endregion
		
		#region Window Actions
		
		/// <summary>
		/// dlgBenutzerFE WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dlgBenutzerFE_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_CreateComplete:
					this.dlgBenutzerFE_OnSAM_CreateComplete(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_CreateComplete event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dlgBenutzerFE_OnSAM_CreateComplete(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			if (this.hSqlBenutzer.Connect()) 
			{
				this.cmbBenutzer.PopulateList(this.hSqlBenutzer, "SELECT DISTINCT BNCODE FROM BN");
				this.cmbBenutzer.Text = strBenutzer;
			}
			this.hSqlBenutzer.Disconnect();
			#endregion
		}
		
		/// <summary>
		/// pbOK WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbOK_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Click:
					this.pbOK_OnSAM_Click(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbOK_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.strBenutzer = this.cmbBenutzer.Text;
			this.EndDialog(1);
			#endregion
		}
		
		/// <summary>
		/// pbCancel WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbCancel_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Click:
					this.pbCancel_OnSAM_Click(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbCancel_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.EndDialog(0);
			#endregion
		}
		#endregion
	}
}
