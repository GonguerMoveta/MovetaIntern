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
	
	public partial class frmStatTK
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
		protected SalBackgroundText bkgd2;
		protected SalBackgroundText bkgd3;
		public SalDataField dfArztNr;
		public SalPushbutton pbOk;
		public frmStatTK.tblTKTableWindow tblTK;
        //FC:FINAL: replace cQuickGraph
        public cQuickGraph cc1;
        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
		{
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tblTK = ((Moveta.Intern.frmStatTK.tblTKTableWindow)(CreateTableWindow(typeof(Moveta.Intern.frmStatTK.tblTKTableWindow))));
            this.ToolBar = new PPJ.Runtime.Windows.SalFormToolBar();
            this.pbAbbruch = new PPJ.Runtime.Windows.SalPushbutton();
            this.ClientArea = new PPJ.Runtime.Windows.SalFormClientArea();
            this.cc1 = new Moveta.Intern.cQuickGraph();
            this.pbOk = new PPJ.Runtime.Windows.SalPushbutton();
            this.dfArztNr = new PPJ.Runtime.Windows.SalDataField();
            this.bkgd3 = new PPJ.Runtime.Windows.SalBackgroundText();
            this.bkgd2 = new PPJ.Runtime.Windows.SalBackgroundText();
            this.StatusBar = new PPJ.Runtime.Windows.SalFormStatusBar();
            this.tblTK.SuspendLayout();
            this.ToolBar.SuspendLayout();
            this.ClientArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cc1)).BeginInit();
            this.SuspendLayout();
            // 
            // tblTK
            // 
            this.tblTK.BackColor = System.Drawing.Color.White;
            // 
            // tblTK.coldtDatum
            // 
            this.tblTK.coldtDatum.DataType = PPJ.Runtime.Windows.DataType.DateTime;
            this.tblTK.coldtDatum.Name = "coldtDatum";
            this.tblTK.coldtDatum.Position = 1;
            this.tblTK.coldtDatum.Title = "Datum";
            // 
            // tblTK.colnAusbuch
            // 
            this.tblTK.colnAusbuch.DataType = PPJ.Runtime.Windows.DataType.Number;
            this.tblTK.colnAusbuch.Name = "colnAusbuch";
            this.tblTK.colnAusbuch.Position = 4;
            this.tblTK.colnAusbuch.Title = "RechSumme";
            // 
            // tblTK.colnHaben
            // 
            this.tblTK.colnHaben.DataType = PPJ.Runtime.Windows.DataType.Number;
            this.tblTK.colnHaben.Name = "colnHaben";
            this.tblTK.colnHaben.Position = 6;
            this.tblTK.colnHaben.Title = "Haben";
            // 
            // tblTK.colnMonat
            // 
            this.tblTK.colnMonat.DataType = PPJ.Runtime.Windows.DataType.Number;
            this.tblTK.colnMonat.Name = "colnMonat";
            this.tblTK.colnMonat.Position = 2;
            this.tblTK.colnMonat.Title = "RechSumme";
            // 
            // tblTK.colnRech
            // 
            this.tblTK.colnRech.DataType = PPJ.Runtime.Windows.DataType.Number;
            this.tblTK.colnRech.Name = "colnRech";
            this.tblTK.colnRech.Position = 3;
            this.tblTK.colnRech.Title = "RechSumme";
            // 
            // tblTK.colnSoll
            // 
            this.tblTK.colnSoll.DataType = PPJ.Runtime.Windows.DataType.Number;
            this.tblTK.colnSoll.Name = "colnSoll";
            this.tblTK.colnSoll.Position = 5;
            this.tblTK.colnSoll.Title = "Soll";
            this.tblTK.Font = new System.Drawing.Font("Tahoma", 9F);
            this.tblTK.Location = new System.Drawing.Point(743, 31);
            this.tblTK.Name = "tblTK";
            this.tblTK.Size = new System.Drawing.Size(324, 344);
            this.tblTK.TabIndex = 4;
            this.tblTK.UseVisualStyles = true;
            this.tblTK.Visible = false;
            // 
            // ToolBar
            // 
            this.ToolBar.Controls.Add(this.pbAbbruch);
            this.ToolBar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.Size = new System.Drawing.Size(740, 60);
            this.ToolBar.TabIndex = 1;
            this.ToolBar.TabStop = true;
            this.ToolBar.Visible = false;
            // 
            // pbAbbruch
            // 
            this.pbAbbruch.AcceleratorKey = System.Windows.Forms.Keys.Escape;
            this.pbAbbruch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.pbAbbruch.Location = new System.Drawing.Point(-25, -42);
            this.pbAbbruch.Name = "pbAbbruch";
            this.pbAbbruch.Size = new System.Drawing.Size(25, 42);
            this.pbAbbruch.TabIndex = 0;
            this.pbAbbruch.Text = "Abbruch";
            this.pbAbbruch.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbAbbruch_WindowActions);
            // 
            // ClientArea
            // 
            this.ClientArea.Controls.Add(this.cc1);
            this.ClientArea.Controls.Add(this.tblTK);
            this.ClientArea.Controls.Add(this.pbOk);
            this.ClientArea.Controls.Add(this.dfArztNr);
            this.ClientArea.Controls.Add(this.bkgd3);
            this.ClientArea.Controls.Add(this.bkgd2);
            this.ClientArea.Location = new System.Drawing.Point(0, 60);
            this.ClientArea.Name = "ClientArea";
            this.ClientArea.Size = new System.Drawing.Size(740, 353);
            this.ClientArea.TabIndex = 0;
            // 
            // cc1
            // 
            this.cc1.ActivateLegend = true;
            this.cc1.BackColor = System.Drawing.Color.White;
            chartArea1.BackColor = Color.Transparent;
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MinorTickMark.Enabled = true;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 100F;
            chartArea1.Position.Width = 100F;
            this.cc1.ChartAreas.Add(chartArea1);
            this.cc1.Location = new System.Drawing.Point(29, 71);
            this.cc1.Name = "cc1";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.Legend = "Legend1";
            series1.LegendText = "Rechnungen";
            series1.MarkerSize = 10;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series1.Name = "SeriesRech";
            series1.XValueMember = "coldtDatum";
            series1.YValueMembers = "colnRech";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.Legend = "Legend1";
            series2.LegendText = "Ausbuchungen";
            series2.MarkerSize = 10;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series2.Name = "SeriesAusbuch";
            series2.XValueMember = "coldtDatum";
            series2.YValueMembers = "colnAusbuch";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series3.Legend = "Legend1";
            series3.LegendText = "Soll";
            series3.MarkerSize = 10;
            series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series3.Name = "SeriesSoll";
            series3.XValueMember = "coldtDatum";
            series3.YValueMembers = "colnSoll";
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series4.Legend = "Legend1";
            series4.LegendText = "Haben";
            series4.MarkerSize = 10;
            series4.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series4.Name = "SeriesHaben";
            series4.XValueMember = "coldtDatum";
            series4.YValueMembers = "colnSoll";
            this.cc1.Series.Add(series1);
            this.cc1.Series.Add(series2);
            this.cc1.Series.Add(series3);
            this.cc1.Series.Add(series4);
            this.cc1.Size = new System.Drawing.Size(690, 320);
            this.cc1.TabIndex = 0;
            this.cc1.TableName = this.tblTK;
            // 
            // pbOk
            // 
            this.pbOk.AcceleratorKey = System.Windows.Forms.Keys.F12;
            this.pbOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.pbOk.Location = new System.Drawing.Point(155, 23);
            this.pbOk.Name = "pbOk";
            this.pbOk.Size = new System.Drawing.Size(72, 29);
            this.pbOk.TabIndex = 3;
            this.pbOk.Text = "&Ok";
            this.pbOk.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbOk_WindowActions);
            // 
            // dfArztNr
            // 
            this.dfArztNr.BackColor = System.Drawing.Color.White;
            this.dfArztNr.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dfArztNr.DataType = PPJ.Runtime.Windows.DataType.Number;
            this.dfArztNr.Font = new System.Drawing.Font("Tahoma", 9F);
            this.dfArztNr.Location = new System.Drawing.Point(89, 25);
            this.dfArztNr.MaxLength = 4;
            this.dfArztNr.Name = "dfArztNr";
            this.dfArztNr.Size = new System.Drawing.Size(56, 17);
            this.dfArztNr.TabIndex = 2;
            // 
            // bkgd3
            // 
            this.bkgd3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.bkgd3.ForeColor = System.Drawing.Color.Red;
            this.bkgd3.Location = new System.Drawing.Point(233, 25);
            this.bkgd3.Name = "bkgd3";
            this.bkgd3.Size = new System.Drawing.Size(30, 21);
            this.bkgd3.TabIndex = 1;
            this.bkgd3.Text = "F12";
            // 
            // bkgd2
            // 
            this.bkgd2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.bkgd2.Location = new System.Drawing.Point(29, 25);
            this.bkgd2.Name = "bkgd2";
            this.bkgd2.Size = new System.Drawing.Size(30, 21);
            this.bkgd2.TabIndex = 0;
            this.bkgd2.Text = "Arzt";
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(0, 413);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(740, 22);
            this.StatusBar.TabIndex = 2;
            // 
            // frmStatTK
            // 
            this.ClientSize = new System.Drawing.Size(740, 435);
            this.Controls.Add(this.ClientArea);
            this.Controls.Add(this.ToolBar);
            this.Controls.Add(this.StatusBar);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Location = new System.Drawing.Point(19, 64);
            this.Name = "frmStatTK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Kontoauszugs-Statistik";
            this.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.frmStatTK_WindowActions);
            this.tblTK.ResumeLayout(false);
            this.ToolBar.ResumeLayout(false);
            this.ClientArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cc1)).EndInit();
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
			if (disposing && App.frmStatTK == this) 
			{
				App.frmStatTK = null;
			}
			base.Dispose(disposing);
		}
		#endregion
		
		#region tblTK
		
		public partial class tblTKTableWindow
		{
			#region Window Controls
			public SalTableColumn coldtDatum;
			public SalTableColumn colnMonat;
			public SalTableColumn colnRech;
			public SalTableColumn colnAusbuch;
			public SalTableColumn colnSoll;
			public SalTableColumn colnHaben;
			#endregion
			
			#region Windows Form Designer generated code
			
			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent()
			{
				this.coldtDatum = new PPJ.Runtime.Windows.SalTableColumn();
				this.colnMonat = new PPJ.Runtime.Windows.SalTableColumn();
				this.colnRech = new PPJ.Runtime.Windows.SalTableColumn();
				this.colnAusbuch = new PPJ.Runtime.Windows.SalTableColumn();
				this.colnSoll = new PPJ.Runtime.Windows.SalTableColumn();
				this.colnHaben = new PPJ.Runtime.Windows.SalTableColumn();
				this.SuspendLayout();
				// 
				// coldtDatum
				// 
				this.coldtDatum.Name = "coldtDatum";
				// 
				// colnMonat
				// 
				this.colnMonat.Name = "colnMonat";
				// 
				// colnRech
				// 
				this.colnRech.Name = "colnRech";
				// 
				// colnAusbuch
				// 
				this.colnAusbuch.Name = "colnAusbuch";
				// 
				// colnSoll
				// 
				this.colnSoll.Name = "colnSoll";
				// 
				// colnHaben
				// 
				this.colnHaben.Name = "colnHaben";
				// 
				// tblTK
				// 
				this.Controls.Add(this.coldtDatum);
				this.Controls.Add(this.colnMonat);
				this.Controls.Add(this.colnRech);
				this.Controls.Add(this.colnAusbuch);
				this.Controls.Add(this.colnSoll);
				this.Controls.Add(this.colnHaben);
				this.Name = "tblTK";
				this.ResumeLayout(false);
			}
			#endregion
		}
		#endregion
	}
}
