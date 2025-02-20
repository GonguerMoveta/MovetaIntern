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
	
	public partial class frmDATEVStamm
	{
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		#region Window Accessories
		
		/// <summary>
		/// Toolbar control.
		/// </summary>
		protected SalFormToolBar ToolBar;
		
		/// <summary>
		/// Client area panel.
		/// </summary>
		protected SalFormClientArea ClientArea;
		
		/// <summary>
		/// StatusBar control.
		/// </summary>
		protected SalFormStatusBar StatusBar;
		#endregion
		
		
		#region Window Controls
		public SalPushbutton pbAbbruch;
		public frmDATEVStamm.tblDATEVTableWindow tblDATEV;
		public SalPushbutton pbNeu;
		public SalPushbutton pbLoe;
		#endregion
		
		#region Windows Form Designer generated code
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ToolBar = new PPJ.Runtime.Windows.SalFormToolBar();
			this.ClientArea = new PPJ.Runtime.Windows.SalFormClientArea();
			this.StatusBar = new PPJ.Runtime.Windows.SalFormStatusBar();
			this.pbAbbruch = new PPJ.Runtime.Windows.SalPushbutton();
			this.tblDATEV = (frmDATEVStamm.tblDATEVTableWindow)CreateTableWindow(typeof(frmDATEVStamm.tblDATEVTableWindow));
			this.pbNeu = new PPJ.Runtime.Windows.SalPushbutton();
			this.pbLoe = new PPJ.Runtime.Windows.SalPushbutton();
			this.ToolBar.SuspendLayout();
			this.ClientArea.SuspendLayout();
			this.SuspendLayout();
			// 
			// ToolBar
			// 
			this.ToolBar.Name = "ToolBar";
			this.ToolBar.Font = new Font("Tahoma", 8f, System.Drawing.FontStyle.Regular);
			this.ToolBar.TabStop = true;
			this.ToolBar.Visible = false;
			this.ToolBar.Controls.Add(this.pbAbbruch);
			// 
			// ClientArea
			// 
			this.ClientArea.Name = "ClientArea";
			this.ClientArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ClientArea.Controls.Add(this.pbLoe);
			this.ClientArea.Controls.Add(this.pbNeu);
			this.ClientArea.Controls.Add(this.tblDATEV);
			// 
			// StatusBar
			// 
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.Visible = true;
			// 
			// pbAbbruch
			// 
			this.pbAbbruch.Name = "pbAbbruch";
			this.pbAbbruch.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbAbbruch.AcceleratorKey = Keys.Escape;
			this.pbAbbruch.Text = "Abbruch";
			this.pbAbbruch.Location = new System.Drawing.Point(-25, -42);
			this.pbAbbruch.Size = new System.Drawing.Size(25, 42);
			this.pbAbbruch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbAbbruch.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbAbbruch_WindowActions);
			this.pbAbbruch.TabIndex = 0;
			// 
			// tblDATEV
			// 
			this.tblDATEV.Name = "tblDATEV";
			this.tblDATEV.BackColor = System.Drawing.Color.White;
			this.tblDATEV.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.tblDATEV.Location = new System.Drawing.Point(53, 15);
			this.tblDATEV.Size = new System.Drawing.Size(1164, 520);
			this.tblDATEV.UseVisualStyles = true;
			// 
			// tblDATEV.colCode
			// 
			this.tblDATEV.colCode.Name = "colCode";
			this.tblDATEV.colCode.Title = "Code";
			this.tblDATEV.colCode.MaxLength = 5;
			this.tblDATEV.colCode.Format = "";
			this.tblDATEV.colCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.tblDATEV.colCode.Position = 1;
			// 
			// tblDATEV.colSKR
			// 
			this.tblDATEV.colSKR.Name = "colSKR";
			this.tblDATEV.colSKR.Title = "SKR";
			this.tblDATEV.colSKR.Width = 41;
			this.tblDATEV.colSKR.MaxLength = 2;
			this.tblDATEV.colSKR.Format = "";
			this.tblDATEV.colSKR.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.tblDATEV.colSKR.CellType = PPJ.Runtime.Windows.CellType.ComboBox;
			this.tblDATEV.colSKR.ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tblDATEV.colSKR.ComboBox.Items.Clear();
			this.tblDATEV.colSKR.ComboBox.Items.AddRange(new string[] {
						"03",
						"04"});
			this.tblDATEV.colSKR.Position = 2;
			// 
			// tblDATEV.col5Stellen
			// 
			this.tblDATEV.col5Stellen.Name = "col5Stellen";
			this.tblDATEV.col5Stellen.Title = "5-stellig?";
			this.tblDATEV.col5Stellen.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblDATEV.col5Stellen.CellType = PPJ.Runtime.Windows.CellType.CheckBox;
			this.tblDATEV.col5Stellen.CheckBox.CheckedValue = "1";
			this.tblDATEV.col5Stellen.CheckBox.UncheckedValue = "0";
			this.tblDATEV.col5Stellen.Position = 3;
			// 
			// tblDATEV.colBeraternr
			// 
			this.tblDATEV.colBeraternr.Name = "colBeraternr";
			this.tblDATEV.colBeraternr.Visible = false;
			this.tblDATEV.colBeraternr.Title = "DATEV-\r\nBeraternr.";
			this.tblDATEV.colBeraternr.MaxLength = 8;
			this.tblDATEV.colBeraternr.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblDATEV.colBeraternr.Enabled = false;
			this.tblDATEV.colBeraternr.Position = 4;
			// 
			// tblDATEV.colMandantennr
			// 
			this.tblDATEV.colMandantennr.Name = "colMandantennr";
			this.tblDATEV.colMandantennr.Visible = false;
			this.tblDATEV.colMandantennr.Title = "DATEV-\r\nMandantennr.";
			this.tblDATEV.colMandantennr.Width = 89;
			this.tblDATEV.colMandantennr.MaxLength = 8;
			this.tblDATEV.colMandantennr.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblDATEV.colMandantennr.Enabled = false;
			this.tblDATEV.colMandantennr.Position = 5;
			// 
			// tblDATEV.colRPForderung
			// 
			this.tblDATEV.colRPForderung.Name = "colRPForderung";
			this.tblDATEV.colRPForderung.Title = "RechProt \r\nForderungskto";
			this.tblDATEV.colRPForderung.Width = 88;
			this.tblDATEV.colRPForderung.MaxLength = 5;
			this.tblDATEV.colRPForderung.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblDATEV.colRPForderung.Position = 6;
			// 
			// tblDATEV.colRP0
			// 
			this.tblDATEV.colRP0.Name = "colRP0";
			this.tblDATEV.colRP0.Title = "RechProt \r\nGeg.kto0%";
			this.tblDATEV.colRP0.Width = 88;
			this.tblDATEV.colRP0.MaxLength = 5;
			this.tblDATEV.colRP0.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblDATEV.colRP0.Position = 7;
			// 
			// tblDATEV.colRP5
			// 
			this.tblDATEV.colRP5.Name = "colRP5";
			this.tblDATEV.colRP5.Title = "RechProt \r\nGeg.kto  5%";
			this.tblDATEV.colRP5.Width = 88;
			this.tblDATEV.colRP5.MaxLength = 5;
			this.tblDATEV.colRP5.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblDATEV.colRP5.Position = 8;
			// 
			// tblDATEV.colRP7
			// 
			this.tblDATEV.colRP7.Name = "colRP7";
			this.tblDATEV.colRP7.Title = "RechProt \r\nGeg.kto 7%";
			this.tblDATEV.colRP7.Width = 88;
			this.tblDATEV.colRP7.MaxLength = 5;
			this.tblDATEV.colRP7.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblDATEV.colRP7.Position = 9;
			// 
			// tblDATEV.colRP16
			// 
			this.tblDATEV.colRP16.Name = "colRP16";
			this.tblDATEV.colRP16.Title = "RechProt \r\nGeg.kto 16%";
			this.tblDATEV.colRP16.Width = 88;
			this.tblDATEV.colRP16.MaxLength = 5;
			this.tblDATEV.colRP16.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblDATEV.colRP16.Position = 10;
			// 
			// tblDATEV.colRP19
			// 
			this.tblDATEV.colRP19.Name = "colRP19";
			this.tblDATEV.colRP19.Title = "RechProt \r\nGeg.kto 19%";
			this.tblDATEV.colRP19.Width = 88;
			this.tblDATEV.colRP19.MaxLength = 5;
			this.tblDATEV.colRP19.DataType = PPJ.Runtime.Windows.DataType.Number;
			this.tblDATEV.colRP19.Position = 11;
			this.tblDATEV.TabIndex = 0;
			// 
			// pbNeu
			// 
			this.pbNeu.Name = "pbNeu";
			this.pbNeu.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbNeu.Location = new System.Drawing.Point(13, 15);
			this.pbNeu.Size = new System.Drawing.Size(36, 40);
			this.pbNeu.TransparentColor = System.Drawing.Color.White;
			this.pbNeu.Image = global::Moveta.Intern.Properties.Resources.add;
			this.pbNeu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbNeu.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbNeu_WindowActions);
			this.pbNeu.TabIndex = 1;
			// 
			// pbLoe
			// 
			this.pbLoe.Name = "pbLoe";
			this.pbLoe.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Bold);
			this.pbLoe.Location = new System.Drawing.Point(13, 63);
			this.pbLoe.Size = new System.Drawing.Size(36, 40);
			this.pbLoe.TransparentColor = System.Drawing.Color.White;
			this.pbLoe.Image = global::Moveta.Intern.Properties.Resources.delete;
			this.pbLoe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.pbLoe.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbLoe_WindowActions);
			this.pbLoe.TabIndex = 2;
			// 
			// frmDATEVStamm
			// 
			this.Controls.Add(this.ClientArea);
			this.Controls.Add(this.ToolBar);
			this.Controls.Add(this.StatusBar);
			this.Name = "frmDATEVStamm";
			this.ClientSize = new System.Drawing.Size(1245, 594);
			this.Font = new Font("Tahoma", 9f, System.Drawing.FontStyle.Regular);
			this.ForeColor = System.Drawing.SystemColors.WindowText;
			this.Text = "DATEV - Stammdaten";
			this.Location = new System.Drawing.Point(37, 102);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.frmDATEVStamm_WindowActions);
			this.ToolBar.ResumeLayout(false);
			this.ClientArea.ResumeLayout(false);
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
			if (disposing && App.frmDATEVStamm == this) 
			{
				App.frmDATEVStamm = null;
			}
			base.Dispose(disposing);
		}
		#endregion
		
		#region tblDATEV
		
		public partial class tblDATEVTableWindow
		{
			#region Window Controls
			public SalTableColumn colCode;
			public SalTableColumn colSKR;
			public SalTableColumn col5Stellen;
			public SalTableColumn colBeraternr;
			public SalTableColumn colMandantennr;
			public SalTableColumn colRPForderung;
			public SalTableColumn colRP0;
			public SalTableColumn colRP5;
			public SalTableColumn colRP7;
			public SalTableColumn colRP16;
			public SalTableColumn colRP19;
			#endregion
			
			#region Windows Form Designer generated code
			
			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent()
			{
				this.colCode = new PPJ.Runtime.Windows.SalTableColumn();
				this.colSKR = new PPJ.Runtime.Windows.SalTableColumn();
				this.col5Stellen = new PPJ.Runtime.Windows.SalTableColumn();
				this.colBeraternr = new PPJ.Runtime.Windows.SalTableColumn();
				this.colMandantennr = new PPJ.Runtime.Windows.SalTableColumn();
				this.colRPForderung = new PPJ.Runtime.Windows.SalTableColumn();
				this.colRP0 = new PPJ.Runtime.Windows.SalTableColumn();
				this.colRP5 = new PPJ.Runtime.Windows.SalTableColumn();
				this.colRP7 = new PPJ.Runtime.Windows.SalTableColumn();
				this.colRP16 = new PPJ.Runtime.Windows.SalTableColumn();
				this.colRP19 = new PPJ.Runtime.Windows.SalTableColumn();
				this.SuspendLayout();
				// 
				// colCode
				// 
				this.colCode.Name = "colCode";
				// 
				// colSKR
				// 
				this.colSKR.Name = "colSKR";
				// 
				// col5Stellen
				// 
				this.col5Stellen.Name = "col5Stellen";
				// 
				// colBeraternr
				// 
				this.colBeraternr.Name = "colBeraternr";
				// 
				// colMandantennr
				// 
				this.colMandantennr.Name = "colMandantennr";
				// 
				// colRPForderung
				// 
				this.colRPForderung.Name = "colRPForderung";
				// 
				// colRP0
				// 
				this.colRP0.Name = "colRP0";
				// 
				// colRP5
				// 
				this.colRP5.Name = "colRP5";
				// 
				// colRP7
				// 
				this.colRP7.Name = "colRP7";
				// 
				// colRP16
				// 
				this.colRP16.Name = "colRP16";
				// 
				// colRP19
				// 
				this.colRP19.Name = "colRP19";
				// 
				// tblDATEV
				// 
				this.Controls.Add(this.colCode);
				this.Controls.Add(this.colSKR);
				this.Controls.Add(this.col5Stellen);
				this.Controls.Add(this.colBeraternr);
				this.Controls.Add(this.colMandantennr);
				this.Controls.Add(this.colRPForderung);
				this.Controls.Add(this.colRP0);
				this.Controls.Add(this.colRP5);
				this.Controls.Add(this.colRP7);
				this.Controls.Add(this.colRP16);
				this.Controls.Add(this.colRP19);
				this.Name = "tblDATEV";
				this.ResumeLayout(false);
			}
			#endregion
		}
		#endregion
	}
}
