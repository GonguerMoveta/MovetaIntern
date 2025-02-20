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
	public partial class frmSerienbriefH : SalFormWindow
	{
		#region Window Variables
		public SalString strAuswahl = "";
		public SalString strPPfad = "";
		public SalString strDateiName = "";
		public SalString strDateiPfad = "";
		public SalFileHandle hDisk = SalFileHandle.Null;
		public SalString mlText = "";
		public SalNumber nBytes = 0;
		#endregion
		
		#region Constructors/Destructors
		
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public frmSerienbriefH()
		{
			// Assign global reference.
			App.frmSerienbriefH = this;
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
		public static frmSerienbriefH CreateWindow(Control owner)
		{
			frmSerienbriefH frm = new frmSerienbriefH();
			frm.Show(owner);
			return frm;
		}
		
		/// <summary>
		/// Returns the object instance associated with the window handle.
		/// </summary>
		/// <param name="handle"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static frmSerienbriefH FromHandle(SalWindowHandle handle)
		{
			return ((frmSerienbriefH)SalWindow.FromHandle(handle, typeof(frmSerienbriefH)));
		}
		#endregion
		
		#region Methods
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalBoolean PalDateiWahl()
		{
			#region Local Variables
			SalArray<SalString> strArExtensions = new SalArray<SalString>(4);
			SalNumber nIndex = 0;
			SalBoolean xOK = false;
			#endregion
			
			#region Actions
			using (new SalContext(this))
			{
				strArExtensions[0] = "Texte - *.rtf";
				strArExtensions[1] = "*.rtf";
				strArExtensions[2] = "alle Dateien - *.*";
				strArExtensions[3] = "*.*";
				xOK = Sal.DlgOpenFile(this, "Texte", strArExtensions, 4, ref nIndex, ref strDateiName, ref strDateiPfad);
				if (xOK) 
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			#endregion
		}
		
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public SalBoolean PalDateiSpeich()
		{
			#region Local Variables
			SalArray<SalString> strArExtensions = new SalArray<SalString>(4);
			SalNumber nIndex = 0;
			SalBoolean xOK = false;
			#endregion
			
			#region Actions
			using (new SalContext(this))
			{
				strArExtensions[0] = "Texte - *.rtf";
				strArExtensions[1] = "*.rtf";
				strArExtensions[2] = "alle Dateien - *.*";
				strArExtensions[3] = "*.*";
				xOK = Sal.DlgSaveFile(this, "SELECT-Statements", strArExtensions, 4, ref nIndex, ref strDateiName, ref strDateiPfad);
				if (xOK) 
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			#endregion
		}
		#endregion
		
		#region Window Actions
		
		/// <summary>
		/// frmSerienbriefH WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmSerienbriefH_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Create:
					this.frmSerienbriefH_OnSAM_Create(sender, e);
					break;
				
				case Sys.SAM_Destroy:
					this.frmSerienbriefH_OnSAM_Destroy(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Create event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmSerienbriefH_OnSAM_Create(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			this.dfVon.Number = 1;
			this.dfBis.Number = 9999;
			Sal.MapEnterToTab(false);
			// 20.01.14 Ä1005
			// Set bOK = SqlCreateSession(hSqlSession,'')
			// Set bOK=SqlCreateStatement (hSqlSession, hSql)
			// Call SqlSetIsolationLevel(hSql,'RO')
			Int.SqlConnection(ref Var.hSql);

			this.cmbLC.PopulateList(Var.hSql, "SELECT lccode || \' \' || lcname FROM lc order by lccode");
			// 20.01.14 Ä1005
			Var.hSql.Disconnect();
			// Call SqlFreeSession( hSqlSession )

			#endregion
		}
		
		/// <summary>
		/// SAM_Destroy event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmSerienbriefH_OnSAM_Destroy(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			Sal.MapEnterToTab(true);
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
			this.strAuswahl = "";
			if (this.rbSQL.Checked) 
			{
				this.strAuswahl = " " + this.mlSQL.Text + " AND ";
			}
			if (this.rbBEZ.Checked) 
			{
				this.strAuswahl = " hbankeinzug = 1 AND ";
			}

			if (this.cbRech.Checked) 
			{
				this.strAuswahl = " harztnr*1000000+hhalternr IN (SELECT DISTINCT rkarztnr*1000000+rkhalternr FROM rk WHERE rkrechnr<999990 AND rkrechdat = :frmSerienbriefH.dfRech) AND hdeaktiv IS NULL AND ";
			}
			if (this.cbPLZ.Checked) 
			{
				this.strAuswahl = " hort >= \'" + this.dfPLZvon.Text + "\' AND hort <= \'" + App.frmSerienbrief.dfPLZbis.Text + "ßßß\' AND ";
			}
			if (this.cbLand.Checked) 
			{
				this.strAuswahl = " hland =  \'" + ((SalString)this.cmbLC.Text).Left(2) + "\' AND ";
			}

			this.strAuswahl = this.strAuswahl + " harztnr BETWEEN :frmSerienbriefH.dfVon AND :frmSerienbriefH.dfBis AND ";

			dlgLlDruck.ModalDialog(App.frmMain, "SerienbriefH.crd", "0");
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
			this.axRTF.PropSetText("");
			#endregion
		}
		
		/// <summary>
		/// pbOeffne WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbOeffne_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Click:
					this.pbOeffne_OnSAM_Click(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbOeffne_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			if (this.PalDateiWahl()) 
			{
				this.hDisk.Open(this.strDateiPfad, Sys.OF_Read);
				this.mlText = this.hDisk.Read(32000000);
				this.hDisk.Close();
				this.axRTF.PropSetText(this.mlText);
			}
			#endregion
		}
		
		/// <summary>
		/// pbSpeich WindowActions Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbSpeich_WindowActions(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			switch (e.ActionType)
			{
				case Sys.SAM_Click:
					this.pbSpeich_OnSAM_Click(sender, e);
					break;
			}
			#endregion
		}
		
		/// <summary>
		/// SAM_Click event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbSpeich_OnSAM_Click(object sender, WindowActionsEventArgs e)
		{
			#region Actions
			e.Handled = true;
			if (this.PalDateiSpeich()) 
			{
				if (this.hDisk.Open(this.strDateiPfad, Sys.OF_Create)) 
				{
					this.hDisk.Close();
					if (this.hDisk.Open(this.strDateiPfad, Sys.OF_Write)) 
					{
						this.axRTF.PropGetText(ref this.mlText);
						this.nBytes = this.hDisk.Write(this.mlText, 32000000);
						if (this.nBytes >= 0) 
						{
							this.hDisk.Close();
						}
					}
					else
					{
						Sal.MessageBox("Die Datei " + this.strDateiPfad + " kann nicht geöffnet werden !", "Fehler", Sys.MB_Ok);
					}
				}
				else
				{
					Sal.MessageBox("Die Datei " + this.strDateiPfad + " kann nicht erstellt werden !", "Fehler", Sys.MB_Ok);
				}
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
