using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PPJ.Runtime;
using System.Windows.Forms.DataVisualization.Charting;
using C1.Win.C1FlexGrid;
using PPJ.Runtime.Windows;
using System.Globalization;
using System.Drawing;

namespace Moveta.Intern
{
    public partial class cQuickGraph : Chart
    {
        public cQuickGraph()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public SalTableWindow TableName { get; set; }

        [Browsable(true)]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ActivateLegend { get; set; }

        /// <summary>
        /// Draw the chart by binding a new data table extracted from a SalTableWindow.
        /// </summary>
        public void Draw(Action<cQuickGraph>[] customActions = null)
        {
            this.Draw(ConvertDataFromSalTableWindowToDataTable(), customActions);           
        }
        
        public void Draw(DataTable table, Action<cQuickGraph>[] customActions = null)
        {
            if (table == null)
                return;

            //if (this.Series.Count <= 0)
            //    return;

            UpdateGraphSettings();

            this.DataSource = table;
            this.DataBind();

            if (customActions != null)
            {
                foreach (var action in customActions)
                {
                    action?.Invoke(this);
                }
            }

            this.Invalidate();
        }

        private void UpdateGraphSettings()
        {         
            this.Legends.Clear();
            this.Annotations.Clear();

            //this.BackColor = Color.White;        // Set chart background to avoid theme conflicts
            //this.ChartAreas[0].BackColor = Color.Transparent;  // Transparent background for chart area (if needed)

            // Legends
            
            if (ActivateLegend)
            {
                Legend legend1 = new Legend();
                legend1.IsTextAutoFit = false;
                legend1.LegendStyle = LegendStyle.Table;
                legend1.Docking = Docking.Right;
                legend1.Alignment = StringAlignment.Near;
                legend1.Name = "Legend1";
                legend1.BackColor = System.Drawing.Color.Transparent;
                this.Legends.Add(legend1);
                this.Legends["Legend1"].IsDockedInsideChartArea = false;
            }            
        }

        private DataTable ConvertDataFromSalTableWindowToDataTable()
        {
            if (TableName != null)
            {
                var c1FlexGrid1 = TableName.MainGrid;
                
                DataTable dt = new DataTable();
                for (int i = 1; i < c1FlexGrid1.Cols.Count; i++)
                {
                    string colName = c1FlexGrid1.Cols[i].Name;
                    dt.Columns.Add(new DataColumn(colName));
                }

                for (int j = 1; j < c1FlexGrid1.Rows.Count; j++)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 1; i < c1FlexGrid1.Cols.Count; i++)
                    {
                        string colName = c1FlexGrid1.Cols[i].Name;
                        string colValue = c1FlexGrid1[j, i].ToString();
                        if (decimal.TryParse(colValue, NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal tempResult))
                        {
                            // Remove thousand separator and replace comma with decimal point
                            colValue = colValue.Replace(".", "").Replace(",", ".");
                        }
                        dr[colName] = colValue;
                    }
                    dt.Rows.Add(dr);
                }

                return dt;
            }
            else return null;
        }       
    }
}
