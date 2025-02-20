// <ppj name="Moveta.Intern" date="1/29/2024 3:39:48 AM" id="F4EC85BAD2BF79AC25C9F8643540E90F9BE1DAF0"/>
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
	
	public partial class dlgTextHalter
	{
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		#region Window Controls
		protected SalBackgroundText bkgd3;
		public SalComboBox cmbHerkunft;
		public SalDataField dfEin;
		public SalComboBox cmbEin;
		public SalPushbutton pbOk;
		protected SalBackgroundText bkgd1;
		protected SalBackgroundText bkgdFrage;
		#endregion
		
		#region Windows Form Designer generated code
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.bkgd3 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.cmbHerkunft = new PPJ.Runtime.Windows.SalComboBox();
			this.dfEin = new PPJ.Runtime.Windows.SalDataField();
			this.cmbEin = new PPJ.Runtime.Windows.SalComboBox();
			this.pbOk = new PPJ.Runtime.Windows.SalPushbutton();
			this.bkgd1 = new PPJ.Runtime.Windows.SalBackgroundText();
			this.bkgdFrage = new PPJ.Runtime.Windows.SalBackgroundText();
			this.SuspendLayout();
			// 
			// bkgd3
			// 
			this.bkgd3.Name = "bkgd3";
			this.bkgd3.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd3.Text = "Herkunft der Adressänderung:";
			this.bkgd3.Location = new System.Drawing.Point(15, 21);
			this.bkgd3.Size = new System.Drawing.Size(400, 20);
			this.bkgd3.TabIndex = 0;
			// 
			// cmbHerkunft
			// 
			this.cmbHerkunft.Name = "cmbHerkunft";
			this.cmbHerkunft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.cmbHerkunft.BackColor = System.Drawing.Color.White;
			this.cmbHerkunft.MaxLength = 100;
			this.cmbHerkunft.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.cmbHerkunft.Sorted = true;
			this.cmbHerkunft.Location = new System.Drawing.Point(15, 49);
			this.cmbHerkunft.Size = new System.Drawing.Size(480, 200);
			this.cmbHerkunft.DropDownHeight = 200;
			this.cmbHerkunft.Items.Clear();
			this.cmbHerkunft.Items.AddRange(new string[] {
						"Einwohnermeldeamt",
						"Rechnungsempfänger",
						"Tierarztpraxis",
						"Gerichtsvollzieher"});
			this.cmbHerkunft.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.cmbHerkunft_WindowActions);
			this.cmbHerkunft.TabIndex = 1;
			// 
			// dfEin
			// 
			this.dfEin.Name = "dfEin";
			this.dfEin.BackColor = System.Drawing.Color.White;
			this.dfEin.MaxLength = 100;
			this.dfEin.Font = new Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
			this.dfEin.Location = new System.Drawing.Point(15, 349);
			this.dfEin.Size = new System.Drawing.Size(488, 30);
			this.dfEin.TabIndex = 2;
			// 
			// cmbEin
			// 
			this.cmbEin.Name = "cmbEin";
			this.cmbEin.BackColor = System.Drawing.Color.White;
			this.cmbEin.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.cmbEin.Sorted = true;
			this.cmbEin.Location = new System.Drawing.Point(15, 349);
			this.cmbEin.Size = new System.Drawing.Size(488, 30);
			this.cmbEin.DropDownHeight = 202;
			this.cmbEin.Items.Clear();
			this.cmbEin.Items.AddRange(new string[] {
						"Info von Tierarzt",
						"Angabe vom Tierarzt in Abrechnung",
						"Einwohnermeldeamt",
						"Post-Anschriftenberichtigungskarte",
						"Post",
						"Telefonbuch",
						"Schuldner",
						"VN/NN falsch"});
			this.cmbEin.TabIndex = 3;
			// 
			// pbOk
			// 
			this.pbOk.Name = "pbOk";
			this.pbOk.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbOk.AcceleratorKey = Keys.F12;
			this.pbOk.Text = "&Ok";
			this.pbOk.Location = new System.Drawing.Point(207, 399);
			this.pbOk.Size = new System.Drawing.Size(80, 30);
			this.pbOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbOk.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbOk_WindowActions);
			this.pbOk.TabIndex = 4;
			// 
			// bkgd1
			// 
			this.bkgd1.Name = "bkgd1";
			this.bkgd1.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgd1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.bkgd1.ForeColor = System.Drawing.Color.Red;
			this.bkgd1.Text = "F12";
			this.bkgd1.Location = new System.Drawing.Point(207, 431);
			this.bkgd1.Size = new System.Drawing.Size(80, 20);
			this.bkgd1.TabIndex = 5;
			// 
			// bkgdFrage
			// 
			this.bkgdFrage.Name = "bkgdFrage";
			this.bkgdFrage.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.bkgdFrage.Text = "";
			this.bkgdFrage.Location = new System.Drawing.Point(15, 311);
			this.bkgdFrage.Size = new System.Drawing.Size(480, 30);
			this.bkgdFrage.TabIndex = 6;
			// 
			// dlgTextHalter
			// 
			this.Controls.Add(this.pbOk);
			this.Controls.Add(this.cmbEin);
			this.Controls.Add(this.dfEin);
			this.Controls.Add(this.cmbHerkunft);
			this.Controls.Add(this.bkgdFrage);
			this.Controls.Add(this.bkgd1);
			this.Controls.Add(this.bkgd3);
			this.Name = "dlgTextHalter";
			this.ClientSize = new System.Drawing.Size(513, 490);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Font = new Font("Tahoma", 12f, System.Drawing.FontStyle.Regular);
			this.Text = "Bitte eingeben :";
			this.Location = new System.Drawing.Point(2, 30);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.dlgTextHalter_WindowActions);
			this.ResumeLayout(false);
		}
		#endregion
		
		#region System Methods/Properties
		
		/// <summary>
		/// Release global reference.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null) 
			{
				components.Dispose();
			}
			if (disposing && App.dlgTextHalter == this) 
			{
				App.dlgTextHalter = null;
			}
			base.Dispose(disposing);
		}
		#endregion
	}
}
