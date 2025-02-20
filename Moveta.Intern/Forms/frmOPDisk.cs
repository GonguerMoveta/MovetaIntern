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
	/// <param name="nArztNr"></param>
	public partial class frmOPDisk : SalFormWindow
	{
		#region Window Parameters
		public SalNumber nArztNr;
		#endregion
		
		#region Window Variables
		public SalSqlHandle hSqlOPDisk = SalSqlHandle.Null;
		public SalFileHandle hFile = SalFileHandle.Null;
		public SalString strSelect = "";
		public SalNumber nFetch = 0;
		public SalNumber nArztNr2 = 0;
		public SalNumber nHalterNr = 0;
		public SalNumber nRest = 0;
		public SalNumber nMS = 0;
		#endregion
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public frmOPDisk(SalNumber nArztNr)
		{
			// Assign global reference.
			App.frmOPDisk = this;
			// Window Parameters initialization.
			this.nArztNr = nArztNr;
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
		public static frmOPDisk CreateWindow(Control owner, SalNumber nArztNr)
		{
			frmOPDisk frm = new frmOPDisk(nArztNr);
			frm.Show(owner);
			return frm;
		}
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static frmOPDisk FromHandle(SalWindowHandle handle)
		{
			return ((frmOPDisk)SalWindow.FromHandle(handle, typeof(frmOPDisk)));
		}
		#endregion
		
		#region Window Actions
		
		/// <summary>
		/// frmOPDisk WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmOPDisk_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Create:
					this.frmOPDisk_OnSAM_Create(sender, e);
					break;
				
				case Sys.SAM_Destroy:
					this.frmOPDisk_OnSAM_Destroy(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Create event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmOPDisk_OnSAM_Create(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.dfArztNr.Number = this.nArztNr;
			this.dfLw.Text = "A";
			Int.SqlImmedSel(@"SELECT ANR2 INTO :frmOPDisk.nArztNr2 FROM A
WHERE AARZTNR = :frmOPDisk.nArztNr");
			if (this.nArztNr2 == 0) 
			{
				this.nArztNr2 = this.nArztNr;
			}
			Int.SqlConnection(ref this.hSqlOPDisk);
			Sal.WaitCursor(false);
			#endregion
		}
		
		/// <summary>
		/// SAM_Destroy event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmOPDisk_OnSAM_Destroy(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.hSqlOPDisk.Disconnect();
			#endregion
		}
		
		/// <summary>
		/// pbErstellen WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbErstellen_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Click:
					this.pbErstellen_OnSAM_Click(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbErstellen_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.strSelect = @"SELECT  rkhalternr,
sum(rkdmleis+rkdmarzn+rkdmmahn+rkmbdmopl+rkdmzins - rkzaleis-rkzaarzn-rkzamahn-rkmbzaopl-rkzazins), max(rkmahnstufe)
INTO :frmOPDisk.nHalterNr, :frmOPDisk.nRest, :frmOPDisk.nMS
FROM rk
WHERE " + Int.PalArztNr("rk", this.nArztNr, this.nArztNr2) + @"
AND rkkzzahl<2 AND rkdeaktiv IS NULL
GROUP BY 1";
			Var.bOK = this.hFile.Open(this.dfLw.Text + ":\\OP" + Int.PalNullen(this.nArztNr, 4) + ".TVN", (Sys.OF_Create | Sys.OF_Write));
			if (Var.bOK) 
			{
				Sal.WaitCursor(true);
				Int.SqlHandleExec(this.hSqlOPDisk, this.strSelect, "OP-Disk erstellen", ref Var.nErr);
				this.nFetch = this.hSqlOPDisk.FetchNext();
				while (this.nFetch != Sys.FETCH_EOF) 
				{
					this.hFile.PutString(Int.PalNullen(this.nHalterNr, 5) + " " + Int.PalStrNumDecPoint(this.nRest, 10, 2) + " " + Int.PalStrNumDecPoint(this.nMS, 2, 0));
					this.nFetch = this.hSqlOPDisk.FetchNext();
				}
				this.hSqlOPDisk.Commit();
				this.hFile.Close();
				Sal.WaitCursor(false);
			}
			else
			{
				Sal.MessageBox("Diskette kann nicht beschrieben werden !", "Diskettenfehler !!", (Sys.MB_Ok | Sys.MB_IconAsterisk));
			}
			Sal.FileSetCurrentDirectory(Const.PATH_TVN32);
			Sal.FileSetDrive(Const.PATH_DRIVE);
			this.DestroyWindow();
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
			// Call SalPostMsg(hWndForm, SAM_Close, 0, 0)
			this.DestroyWindow();
			#endregion
		}
		#endregion
	}
}
