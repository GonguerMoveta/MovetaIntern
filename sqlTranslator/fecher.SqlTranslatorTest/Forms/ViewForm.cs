using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLTranslatorTest.Forms
{
    public partial class ViewForm : Form
    {
        private string txtSql;
        private SqlDataAdapter adapter;
        private SqlCommand cmdSql;
        public ViewForm(string sqlMS, SqlConnection connMS, string sqlIFX)
        {
            InitializeComponent();

            txtSql = sqlMS;
            Grid1.AutoGenerateColumns = true;
            Grid1.DataSource = new DataTable();
            string countMS;
            try
            {
                //txtSql = "select * from at_art_pass16";
                cmdSql = new SqlCommand(txtSql, connMS);
                adapter = new SqlDataAdapter(cmdSql);                
                adapter.Fill(Grid1.DataSource as DataTable);
                countMS = Grid1.RowCount.ToString();
            }
            catch (Exception ex)
            {
                countMS = ex.Message;
            }

            //DataTable tbRep = Grid1.DataSource as DataTable;
            //tbRep.Clear();
            //tbRep.Columns.Clear();

            Grid2.AutoGenerateColumns = true;
            Grid2.DataSource = new DataTable();
            string countIFX;
            try
            {
                OdbcConnection odbcconn = new OdbcConnection(Properties.Settings.Default.ConnectionStringTWS);
                odbcconn.Open();
                new OdbcDataAdapter(sqlIFX, odbcconn).Fill(Grid2.DataSource as DataTable);
                countIFX = Grid2.RowCount.ToString();
            }
            catch (Exception ex)
            {
                countIFX = ex.Message;
            }

            statusStrip1.Items[0].Text = "SQL: " + countMS;
            statusStrip2.Items[0].Text = "IFX: " + countIFX;

            for (int i=0; i< Grid1.ColumnCount; i++)
            {
                chkList1.Items.Add(Grid1.Columns[i].Name, Grid2.Columns.Contains(Grid1.Columns[i].Name));
            }           
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.printers' table. You can move, or remove it, as needed.
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            chkList1.Visible = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int IdenticalCount = 0;
            int OldCount = Grid1.RowCount;
            for (int i = Grid1.RowCount - 1; i >= 0; i--) 
            {
                for (int j = Grid2.RowCount - 1; j >= 0; j--)
                {
                    bool equal = true;
                    for (int k = Grid1.ColumnCount - 1; k >= 0; k--) 
                    {
                        if (chkList1.GetItemChecked(k))
                        {
                            string colname = Grid1.Columns[k].Name;
                            //if (!Grid1.Rows[i].Cells[colname].Value.Equals(Grid2.Rows[j].Cells[colname].Value))
                            if (Grid1.Rows[i].Cells[colname].Value.ToString().TrimEnd() != Grid2.Rows[j].Cells[colname].Value.ToString().TrimEnd()) 
                            {
                                equal = false;
                                break;
                            }
                        }
                    }                   
                    if (equal)
                    {
                        IdenticalCount++;
                        if (chkDelrows.Checked)
                        {
                            Grid2.Rows.RemoveAt(j);
                            Grid1.Rows.RemoveAt(i);
                        }
                        break;
                    }
                }
                statusStrip1.Items[0].Text = "SQL: " + i.ToString();
                Application.DoEvents();
            }

            statusStrip1.Items[0].Text = "SQL: " + Grid1.RowCount.ToString();
            statusStrip2.Items[0].Text = "IFX: " + Grid2.RowCount.ToString();

            if (IdenticalCount == OldCount && Grid1.RowCount == Grid2.RowCount)
            {
                MessageBox.Show("Identical OK !");
            }
            else
            {
                MessageBox.Show("Differences found !!!.\n\n(identical rows: " + (chkDelrows.Checked ? "0, deleted " + IdenticalCount.ToString() : IdenticalCount.ToString()) + ")");
            }                                        
        }

        private void Grid1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView grid = (sender as DataGridView);
            if (e.Value is DBNull)
            {
                e.Value = "NULL";
                e.FormattingApplied = true;
            }
        }

        private void chkSql_CheckedChanged(object sender, EventArgs e)
        {
            txbSql.Visible = chkSql.Checked;

            if (chkSql.Checked)
            {
                txbSql.Text = txtSql;
            }
            else
            {
                txtSql = txbSql.Text;
                string countMS;
                try
                {                    
                    (Grid1.DataSource as DataTable).Clear();
                    (Grid1.DataSource as DataTable).Columns.Clear();                    
                    cmdSql.CommandText = txtSql;
                    adapter.Fill(Grid1.DataSource as DataTable);
                    countMS = Grid1.RowCount.ToString();
                    chkList1.Items.Clear();
                    for (int i = 0; i < Grid1.ColumnCount; i++)
                    {
                        chkList1.Items.Add(Grid1.Columns[i].Name, Grid2.Columns.Contains(Grid1.Columns[i].Name));
                    }
                }
                catch (Exception ex)
                {
                    countMS = ex.Message;
                }
                statusStrip1.Items[0].Text = "SQL: " + countMS;
            }                       
        }
    }
}
