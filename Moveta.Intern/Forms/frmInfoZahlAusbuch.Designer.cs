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
using System.Windows.Forms.DataVisualization.Charting;

namespace Moveta.Intern
{
	
	public partial class frmInfoZahlAusbuch
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
		protected SalBackgroundText bkgd4;
		protected SalBackgroundText bkgd5;
		public SalDataField dfvon;
		public SalDataField dfbis;
		public VisCalendarDropDown dfDvon;
		public VisCalendarDropDown dfDbis;
		protected SalBackgroundText bkgd6;
		protected SalBackgroundText bkgd7;
		public SalPushbutton pbOk;
		public frmInfoZahlAusbuch.tblZahlTableWindow tblZahl;
        //FC:FINAL:: replace cQuickGraph
        public cQuickGraph ccZahl;
        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
		{
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tblZahl = ((Moveta.Intern.frmInfoZahlAusbuch.tblZahlTableWindow)(CreateTableWindow(typeof(Moveta.Intern.frmInfoZahlAusbuch.tblZahlTableWindow))));
            this.ToolBar = new PPJ.Runtime.Windows.SalFormToolBar();
            this.pbAbbruch = new PPJ.Runtime.Windows.SalPushbutton();
            this.ClientArea = new PPJ.Runtime.Windows.SalFormClientArea();
            this.ccZahl = new Moveta.Intern.cQuickGraph();
            this.pbOk = new PPJ.Runtime.Windows.SalPushbutton();
            this.dfDbis = new PPJ.Runtime.Vis.VisCalendarDropDown();
            this.dfDvon = new PPJ.Runtime.Vis.VisCalendarDropDown();
            this.dfbis = new PPJ.Runtime.Windows.SalDataField();
            this.dfvon = new PPJ.Runtime.Windows.SalDataField();
            this.bkgd7 = new PPJ.Runtime.Windows.SalBackgroundText();
            this.bkgd6 = new PPJ.Runtime.Windows.SalBackgroundText();
            this.bkgd5 = new PPJ.Runtime.Windows.SalBackgroundText();
            this.bkgd4 = new PPJ.Runtime.Windows.SalBackgroundText();
            this.StatusBar = new PPJ.Runtime.Windows.SalFormStatusBar();
            this.tblZahl.SuspendLayout();
            this.ToolBar.SuspendLayout();
            this.ClientArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ccZahl)).BeginInit();
            this.SuspendLayout();
            // 
            // tblZahl
            // 
            this.tblZahl.BackColor = System.Drawing.Color.White;
            // 
            // tblZahl.coldtDatum
            // 
            this.tblZahl.coldtDatum.DataType = PPJ.Runtime.Windows.DataType.DateTime;
            this.tblZahl.coldtDatum.Enabled = false;
            this.tblZahl.coldtDatum.Format = "dd.MM.yyyy";
            this.tblZahl.coldtDatum.Name = "coldtDatum";
            this.tblZahl.coldtDatum.Position = 1;
            this.tblZahl.coldtDatum.Title = "Datum";
            this.tblZahl.coldtDatum.Width = 136;
            // 
            // tblZahl.colnAusbuch
            // 
            this.tblZahl.colnAusbuch.DataType = PPJ.Runtime.Windows.DataType.Number;
            this.tblZahl.colnAusbuch.Enabled = false;
            this.tblZahl.colnAusbuch.Format = "#,##0.00";
            this.tblZahl.colnAusbuch.Name = "colnAusbuch";
            this.tblZahl.colnAusbuch.Position = 3;
            this.tblZahl.colnAusbuch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tblZahl.colnAusbuch.Title = "Ausbuch-€";
            this.tblZahl.colnAusbuch.Width = 163;
            // 
            // tblZahl.colnZahl
            // 
            this.tblZahl.colnZahl.DataType = PPJ.Runtime.Windows.DataType.Number;
            this.tblZahl.colnZahl.Enabled = false;
            this.tblZahl.colnZahl.Format = "#,##0.00";
            this.tblZahl.colnZahl.Name = "colnZahl";
            this.tblZahl.colnZahl.Position = 2;
            this.tblZahl.colnZahl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tblZahl.colnZahl.Title = "Zahl-€";
            this.tblZahl.colnZahl.Width = 173;
            this.tblZahl.Font = new System.Drawing.Font("Tahoma", 9F);
            this.tblZahl.Location = new System.Drawing.Point(15, 472);
            this.tblZahl.Name = "tblZahl";
            this.tblZahl.Size = new System.Drawing.Size(736, 134);
            this.tblZahl.TabIndex = 9;
            this.tblZahl.UseVisualStyles = true;
            // 
            // ToolBar
            // 
            this.ToolBar.Controls.Add(this.pbAbbruch);
            this.ToolBar.Font = new System.Drawing.Font("Tahoma", 8F);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.Size = new System.Drawing.Size(779, 77);
            this.ToolBar.TabIndex = 1;
            this.ToolBar.TabStop = true;
            this.ToolBar.Visible = false;
            // 
            // pbAbbruch
            // 
            this.pbAbbruch.AcceleratorKey = System.Windows.Forms.Keys.Escape;
            this.pbAbbruch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.pbAbbruch.Location = new System.Drawing.Point(-33, -54);
            this.pbAbbruch.Name = "pbAbbruch";
            this.pbAbbruch.Size = new System.Drawing.Size(33, 54);
            this.pbAbbruch.TabIndex = 0;
            this.pbAbbruch.Text = "Abbruch";
            this.pbAbbruch.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbAbbruch_WindowActions);
            // 
            // ClientArea
            // 
            this.ClientArea.Controls.Add(this.ccZahl);
            this.ClientArea.Controls.Add(this.tblZahl);
            this.ClientArea.Controls.Add(this.pbOk);
            this.ClientArea.Controls.Add(this.dfDbis);
            this.ClientArea.Controls.Add(this.dfDvon);
            this.ClientArea.Controls.Add(this.dfbis);
            this.ClientArea.Controls.Add(this.dfvon);
            this.ClientArea.Controls.Add(this.bkgd7);
            this.ClientArea.Controls.Add(this.bkgd6);
            this.ClientArea.Controls.Add(this.bkgd5);
            this.ClientArea.Controls.Add(this.bkgd4);
            this.ClientArea.Location = new System.Drawing.Point(0, 77);
            this.ClientArea.Name = "ClientArea";
            this.ClientArea.Size = new System.Drawing.Size(779, 546);
            this.ClientArea.TabIndex = 0;
            // 
            // ccZahl
            // 
            this.ccZahl.BackColor = System.Drawing.Color.White;
            chartArea1.BackColor = Color.Transparent;
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.IsLabelAutoFit = true;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.IsLabelAutoFit = true;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MinorTickMark.Enabled = true;
            chartArea1.AxisY.LabelStyle.Format = "#,0,\' k\'";
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 100F;
            chartArea1.Position.Width = 100F;
            this.ccZahl.ChartAreas.Add(chartArea1);
            legend1.IsDockedInsideChartArea = false;
            legend1.Name = "Legend1";
            legend1.Enabled = false;
            this.ccZahl.ActivateLegend = true;
            this.ccZahl.Legends.Add(legend1);
            this.ccZahl.Location = new System.Drawing.Point(15, 116);
            this.ccZahl.Name = "ccZahl";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.Legend = "Legend1";
            series1.LegendText = "Zahl";
            series1.Name = "SeriesZahl";
            series1.XValueMember = "coldtDatum";
            series1.YValueMembers = "colnZahl";
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.Legend = "Legend1";
            series2.LegendText = "Ausbuch";
            series2.Name = "SeriesAusbuch";
            series2.XValueMember = "coldtDatum";
            series2.YValueMembers = "colnAusbuch";
            this.ccZahl.Series.Add(series1);
            this.ccZahl.Series.Add(series2);
            this.ccZahl.Size = new System.Drawing.Size(736, 348);
            this.ccZahl.TabIndex = 10;
            this.ccZahl.TableName = tblZahl;
            this.ccZahl.Text = "ccZahl";
            // 
            // pbOk
            // 
            this.pbOk.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.pbOk.Image = global::Moveta.Intern.Properties.Resources.ok;
            this.pbOk.Location = new System.Drawing.Point(647, 30);
            this.pbOk.Name = "pbOk";
            this.pbOk.Size = new System.Drawing.Size(80, 72);
            this.pbOk.TabIndex = 8;
            this.pbOk.Text = "&Ok";
            this.pbOk.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.pbOk_WindowActions);
            // 
            // dfDbis
            // 
            this.dfDbis.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.dfDbis.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dfDbis.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(233)))), ((int)(((byte)(235)))));
            this.dfDbis.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.dfDbis.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.dfDbis.Format = null;
            this.dfDbis.Location = new System.Drawing.Point(463, 63);
            this.dfDbis.Name = "dfDbis";
            this.dfDbis.Size = new System.Drawing.Size(155, 26);
            this.dfDbis.TabIndex = 5;         
            // 
            // dfDvon
            // 
            this.dfDvon.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.dfDvon.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dfDvon.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(233)))), ((int)(((byte)(235)))));
            this.dfDvon.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.dfDvon.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.dfDvon.Format = null;
            this.dfDvon.Location = new System.Drawing.Point(127, 63);
            this.dfDvon.Name = "dfDvon";
            this.dfDvon.Size = new System.Drawing.Size(155, 26);
            this.dfDvon.TabIndex = 4;            
            // 
            // dfbis
            // 
            this.dfbis.BackColor = System.Drawing.Color.White;
            this.dfbis.DataType = PPJ.Runtime.Windows.DataType.Number;
            this.dfbis.Font = new System.Drawing.Font("Tahoma", 9F);
            this.dfbis.Location = new System.Drawing.Point(463, 19);
            this.dfbis.MaxLength = 4;
            this.dfbis.Name = "dfbis";
            this.dfbis.Size = new System.Drawing.Size(52, 31);
            this.dfbis.TabIndex = 3;
            // 
            // dfvon
            // 
            this.dfvon.BackColor = System.Drawing.Color.White;
            this.dfvon.DataType = PPJ.Runtime.Windows.DataType.Number;
            this.dfvon.Font = new System.Drawing.Font("Tahoma", 9F);
            this.dfvon.Location = new System.Drawing.Point(127, 19);
            this.dfvon.MaxLength = 4;
            this.dfvon.Name = "dfvon";
            this.dfvon.Size = new System.Drawing.Size(52, 31);
            this.dfvon.TabIndex = 2;
            // 
            // bkgd7
            // 
            this.bkgd7.Font = new System.Drawing.Font("Tahoma", 9F);
            this.bkgd7.Location = new System.Drawing.Point(351, 63);
            this.bkgd7.Name = "bkgd7";
            this.bkgd7.Size = new System.Drawing.Size(104, 21);
            this.bkgd7.TabIndex = 7;
            this.bkgd7.Text = "bis Datum";
            // 
            // bkgd6
            // 
            this.bkgd6.Font = new System.Drawing.Font("Tahoma", 9F);
            this.bkgd6.Location = new System.Drawing.Point(15, 63);
            this.bkgd6.Name = "bkgd6";
            this.bkgd6.Size = new System.Drawing.Size(88, 21);
            this.bkgd6.TabIndex = 6;
            this.bkgd6.Text = "von Datum";
            // 
            // bkgd5
            // 
            this.bkgd5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.bkgd5.Location = new System.Drawing.Point(351, 22);
            this.bkgd5.Name = "bkgd5";
            this.bkgd5.Size = new System.Drawing.Size(96, 21);
            this.bkgd5.TabIndex = 1;
            this.bkgd5.Text = "bis Arzt-Nr.";
            // 
            // bkgd4
            // 
            this.bkgd4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.bkgd4.Location = new System.Drawing.Point(15, 22);
            this.bkgd4.Name = "bkgd4";
            this.bkgd4.Size = new System.Drawing.Size(96, 21);
            this.bkgd4.TabIndex = 0;
            this.bkgd4.Text = "von Arzt-Nr.";
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(0, 623);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(779, 28);
            this.StatusBar.TabIndex = 2;
            // 
            // frmInfoZahlAusbuch
            // 
            this.AutoScaleBaseDpi = new System.Drawing.Size(120, 120);
            this.ClientSize = new System.Drawing.Size(779, 651);
            this.Controls.Add(this.ClientArea);
            this.Controls.Add(this.ToolBar);
            this.Controls.Add(this.StatusBar);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Location = new System.Drawing.Point(35, 46);
            this.Name = "frmInfoZahlAusbuch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Infos über Zahlungen / Ausbuchungen";
            this.WindowActions += new PPJ.Runtime.Windows.WindowActionsEventHandler(this.frmInfoZahlAusbuch_WindowActions);
            this.tblZahl.ResumeLayout(false);
            this.ToolBar.ResumeLayout(false);
            this.ClientArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ccZahl)).EndInit();
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
			if (disposing && App.frmInfoZahlAusbuch == this) 
			{
				App.frmInfoZahlAusbuch = null;
			}
			base.Dispose(disposing);
		}        
        #endregion

        #region tblZahl

        public partial class tblZahlTableWindow
		{
			#region Window Controls
			public SalTableColumn coldtDatum;
			public SalTableColumn colnZahl;
			public SalTableColumn colnAusbuch;
			#endregion
			
			#region Windows Form Designer generated code
			
			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent()
			{
				this.coldtDatum = new PPJ.Runtime.Windows.SalTableColumn();
				this.colnZahl = new PPJ.Runtime.Windows.SalTableColumn();
				this.colnAusbuch = new PPJ.Runtime.Windows.SalTableColumn();
				this.SuspendLayout();
				// 
				// coldtDatum
				// 
				this.coldtDatum.Name = "coldtDatum";
				// 
				// colnZahl
				// 
				this.colnZahl.Name = "colnZahl";
				// 
				// colnAusbuch
				// 
				this.colnAusbuch.Name = "colnAusbuch";
				// 
				// tblZahl
				// 
				this.Controls.Add(this.coldtDatum);
				this.Controls.Add(this.colnZahl);
				this.Controls.Add(this.colnAusbuch);
				this.Name = "tblZahl";
				this.ResumeLayout(false);
			}
			#endregion
		}
		#endregion
	}
}
