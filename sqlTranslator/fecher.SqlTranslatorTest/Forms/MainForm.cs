using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Xml;
using System.Collections.Specialized;
using System.Threading;
using Gupta.SQLBase.Data;
using fecher.Common;

namespace SQLTranslatorTest
{
    public partial class MainForm : Form
    {

        private List<StringDictionary> FailedExecutedStatementsList;

        private List<StringDictionary> FailedComparedStatementsList;

        private Thread trd;
        

        public MainForm()
        {
            InitializeComponent();

            Globals.WkSpace = new WorkSpace();
            Globals.SqlBaseConnProp = new StringDictionary();
            Globals.SqlServerConnProp = new StringDictionary();
            Globals.OracleConnProp = new StringDictionary();

            FailedExecutedStatementsList = new List<StringDictionary>();
            FailedComparedStatementsList = new List<StringDictionary>();
            cboSourceDB.SelectedIndex = 0;
        }

        #region Window variables
        string sourceSql = "";
        TranslateStatements translateClass;
        string[] statementsAfter;
        string[] statementsInitial;
        ExecuteCompareStatement executeClass = new ExecuteCompareStatement();
        OpenFileDialog ofd1 = new OpenFileDialog();
        #endregion

        #region Buttons Click event

        private void openbtn_Click(object sender, EventArgs e)
        {
            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                tbSourceSql.Text = File.ReadAllText(ofd1.FileName);
                //MU -->
                Globals.WkSpace.SqlBaseScriptFile = ofd1.FileName;
                //<--
            }
        }

        private void connectsqlbasebtn_Click(object sender, EventArgs e)
        {
            TestConnectionForm frm = new TestConnectionForm(DatabaseBrand.SqlBase);
            frm.ShowDialog();
            if (Globals.SqlBaseConnProp.ContainsKey("DATA SOURCE"))
            {
                sqlbasetbx.Text = Globals.SqlBaseConnProp["DATA SOURCE"];
            }
        }

        private void SQLServerbtn_Click(object sender, EventArgs e)
        {
            TestConnectionForm frm = new TestConnectionForm(DatabaseBrand.SqlServer);
            frm.ShowDialog();
            if (Globals.SqlServerConnProp.ContainsKey("INITIAL CATALOG"))
            {
                sqlservertbx.Text = Globals.SqlServerConnProp["INITIAL CATALOG"];
            }
        }

        private void Oraclebtn_Click(object sender, EventArgs e)
        {
            TestConnectionForm frm = new TestConnectionForm(DatabaseBrand.Oracle);
            frm.ShowDialog();
            if (Globals.OracleConnProp.ContainsKey("DATA SOURCE"))
            {
                oracletbx.Text = Globals.OracleConnProp["DATA SOURCE"];
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //MU
            //File.WriteAllText(Application.StartupPath + "\\TranslatedSql.txt", tbSourceSql.Text);
            File.WriteAllText(Application.StartupPath + "\\TranslatedSql.txt", tbTranslatedSql.Text);
        }

        private void Startbtn_Click(object sender, EventArgs e)
        {
            //MU without threading
            //fecher.Common.DatabaseSettings.Default.DbStructure.InitSqlServer("NB-PB-RH06", "TVN1MSSQL", "SYSADM", "SYSADM");
            Globals.Settings.ConvertDateValues = true;
            Globals.Settings.Collation = "Latin1_General_CS_AS";
            TranslateExecuteCompare(tbSourceSql.Text);
            //trd = new Thread(new ParameterizedThreadStart(TranslateExecuteCompare));
            //trd.Start(tbSourceSql.Text);
        }

        private void Stopbtn_Click(object sender, EventArgs e)
        {
            if (trd != null && trd.IsAlive)
            {
                trd.Abort();
            }
        }

        #endregion

        #region CheckBoxes CheckedChanged event

        private void Comparecbx_CheckedChanged(object sender, EventArgs e)
        {
            if (Comparecbx.Checked)
                Executecbx.Checked = true;
        }

        private void Executecbx_CheckedChanged(object sender, EventArgs e)
        {
            if (Comparecbx.Checked)
                if (Executecbx.Checked == false)
                    Executecbx.Checked = true;
        }

        #endregion

        #region RadioButtons CheckedChanged event

        private void sqlserverrdb_CheckedChanged(object sender, EventArgs e)
        {
            if (sqlserverrdb.Checked)
            {
                Oraclerdb.Checked = false;
                Oraclebtn.Enabled = false;
            }
            else
            {
                Oraclerdb.Checked = true;
                Oraclebtn.Enabled = true;
            }
        }

        private void Oraclerdb_CheckedChanged(object sender, EventArgs e)
        {
            if (Oraclerdb.Checked)
            {
                sqlserverrdb.Checked = false;
                SQLServerbtn.Enabled = false;
            }
            else
            {
                sqlserverrdb.Checked = true;
                SQLServerbtn.Enabled = true;
            }
        }

        private void rbSqlBaseVersion2_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.SqlBaseVersion = rbSqlBaseVersion2.Checked ? DatabaseBrand.SqlBaseOld : DatabaseBrand.SqlBase;
        }

        private void rbUniqueIdentifier_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.Rowid = rbUniqueIdentifier.Checked ? RowidType.UniqueIdentifier : RowidType.Timestamp;
        }

        private void rbUnicodeFalse_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.Unicode = !rbUnicodeFalse.Checked;
        }

        private void rbReturnSource_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.OnParseError = rbReturnSource.Checked ? ParseErrorAction.ReturnSource :
                rbIgnore.Checked ? ParseErrorAction.Ignore : ParseErrorAction.ThrowException;
        }

        private void rbReadStructureFalse_CheckedChanged(object sender, EventArgs e)
        {
            Globals.Settings.ReadStructure = !rbReadStructureFalse.Checked;
        }

        #endregion 
 
        #region TextBoxes Events

        private void tbxDtFormat_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue != 8 && e.KeyValue != 20 && e.KeyValue != 37 && e.KeyValue != 39) && !(e.KeyCode == Keys.D || e.KeyCode == Keys.M || e.KeyCode == Keys.Y))
            {
                MessageBox.Show("Date format must be entered in  Y,M,D", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                tbxDtFormat.Clear();
            }

            if (tbxDtFormat.Text.Length == tbxDtFormat.Mask.Length)
            {
                Globals.Settings.DateFormat = tbxDtFormat.Text;
            }
        }

        #endregion

        #region ToolStrip event handlers

        private void tsOpenBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Select the workspace to open";
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Globals.WkSpace.LoadFromXml(openFileDialog1.FileName);
                WorkSpaceToSettings();
                Globals.WkSpace.FileName = openFileDialog1.FileName;

                this.tsSaveBtn.Enabled = true;
                this.Text = "sqlTranslator Test -> Workspace -> " + openFileDialog1.FileName.Remove(0, openFileDialog1.FileName.LastIndexOf('\\') + 1);
            }
        }

        private void tsNewBtn_Click(object sender, EventArgs e)
        {
            CreateNewWorkSpace("New");
        }

        private void tsSaveBtn_Click(object sender, EventArgs e)
        {
            SaveWorkSpace();
        }

        private void tsSaveAsBtn_Click(object sender, EventArgs e)
        {
            CreateNewWorkSpace("SaveAs");
        }

        #endregion

        #region Methods

        private void CreateNewWorkSpace(string option)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Title = (option == "New") ? "Select the name of the new workspace file" : "Save workspace as";
            saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Globals.WkSpace.CreateXmlSchemaWkSpace(saveFileDialog1.FileName);
                Globals.WkSpace = new WorkSpace();
                Globals.WkSpace.FileName = saveFileDialog1.FileName;

                switch (option)
                {
                    case "New":
                        {
                            WorkSpaceToSettings();
                            break;
                        }

                    case "SaveAs":
                        {
                            SaveWorkSpace();
                            break;
                        }

                }

                this.tsSaveBtn.Enabled = true;
                this.Text = "sqlTranslator Test -> Workspace -> " + saveFileDialog1.FileName.Remove(0, saveFileDialog1.FileName.LastIndexOf('\\') + 1);
            }
        }

        private void SaveWorkSpace()
        {
            SettingsToWorkSpace();
            Globals.WkSpace.SaveToXml(Globals.WkSpace.FileName);
        }

        private void TranslateExecuteCompare(object sourceSql)
        {
            TranslateExecuteCompare((string)sourceSql);
        }

        private void TranslateExecuteCompare(string sourceSql)
        {
            bool connectCond1 = false, connectCond2 = false, b = false;
            int comparedStatements = 0;
            int dataComparingExceptions = 0;
            string sharpLine = "#####################################################################";
            string FinalResults = sharpLine + Environment.NewLine + "### Final Results" + Environment.NewLine + sharpLine + Environment.NewLine;
            string DBType = "";

            if (sqlserverrdb.Checked)
            {
                DBType = "SQLSERVER";
            }
            else if (Oraclerdb.Checked)
            {
                DBType = "ORACLE";
            }

            FailedExecutedStatementsList.Clear();
            FailedComparedStatementsList.Clear();

            //statementsInitial = sourceSql.Split(';');
            statementsInitial = sourceSql.Split(new string[] { "\n/" }, 1000, StringSplitOptions.RemoveEmptyEntries);
            translateClass = new TranslateStatements(rbSqlTranslator.Checked ? TranslatorType.SqlTranslator : TranslatorType.SpTranslator);

            FinalResults += "### Initial Statements : " + statementsInitial.Length.ToString() + Environment.NewLine;

            connectCond1 = (sqlbasetbx.Text.Length != 0);

            switch (DBType)
            {
                case "SQLSERVER": 
                    connectCond2 = (sqlservertbx.Text.Length != 0);
                    switch (cboSourceDB.SelectedItem)
                    {
                        case "SqlBase":
                            {
                                statementsAfter = translateClass.TranslateSQLBaseToSQLServer(statementsInitial);
                                break;
                            }
                        case "Informix":
                            {
                                btnInformix.Enabled = (Globals.WkSpace.SqlServerConnectionString != null);
                                statementsAfter = translateClass.TranslateInformixToSQLServer(statementsInitial);
                                break;
                            }
                        case "Access":
                            {
                                statementsAfter = translateClass.TranslateAccessToSQLServer(statementsInitial);
                                break;
                            }
                    }
                    break;

                case "ORACLE":
                    connectCond2 = (oracletbx.Text.Length != 0);
                    statementsAfter = translateClass.TranslateSQLBaseToOracle(statementsInitial);
                    break;
            }

            FinalResults += "### Translated Statements : " + statementsAfter.Length.ToString() + Environment.NewLine;
            FinalResults += sharpLine + Environment.NewLine;

            WriteToTranslatedSql("");
            PrgBarInitVal("Translating...");

            foreach (string statement in statementsAfter)
            {
                WriteToTranslatedSql(statement + "\r\nGO\r\n");
                PrgPerformStep();
            }

            if (Comparecbx.Checked)
            {
                if (connectCond1 && connectCond2)
                {
                    PrgBarInitVal("Comparing...");

                    for (int i = 0; i < statementsAfter.Length; i++)
                    {
                        try
                        {
                            switch (DBType)
                            {
                                case "SQLSERVER":
                                    b = executeClass.ExecuteCompareSQLBaseSQLServer(statementsAfter[i].ToString(), statementsInitial[i].ToString());
                                    break;

                                case "ORACLE":
                                    b = executeClass.ExecuteCompareSQLBaseOracle(statementsAfter[i].ToString(), statementsInitial[i].ToString());
                                    break;
                            }

                            if (!b)
                            {
                                WriteToTranslatedSql("Comparing statement " + statementsAfter[i].ToString() + " failed " + "\r\n\r\n");
                                FailedComparedStatementsList.Add(CreateStatement(statementsInitial[i], statementsAfter[i], String.Empty));
                            }
                            else
                            {
                                comparedStatements++;
                                WriteToTranslatedSql("Comparing statement: " + statementsAfter[i].ToString() + " finished" + "\r\n\r\n");
                            }

                            PrgPerformStep();
                        }
                        catch (DataTablesException dtex)
                        {
                            dataComparingExceptions++;
                            PrgPerformStep();
                        }
                        catch (Exception ex)
                        {
                            FailedExecutedStatementsList.Add(CreateStatement(statementsInitial[i], statementsAfter[i], ex.Message));
                            WriteToTranslatedSql("Executing statement : " + statementsAfter[i].ToString() + " failed \r\n" + "Details: " + ex.Message);
                            PrgPerformStep();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please connect to databases first");
                }
            }
            else if (Executecbx.Checked)
            {
                if (connectCond2)
                {
                    PrgBarInitVal("Executing...");
                                                          
                    for (int i = 0; i < statementsAfter.Length; i++)
                    {
                        try
                        {
                            switch (DBType)
                            {
                                case "SQLSERVER":
                                    executeClass.ExecuteSQLBaseSQLServer(statementsAfter[i].ToString());
                                    break;

                                case "ORACLE":
                                    executeClass.ExecuteSQLBaseOracle(statementsAfter[i].ToString());
                                    break;
                            }

                            WriteToTranslatedSql("Executing statement: " + statementsAfter[i].ToString() + " finished" + "\r\n\r\n");
                            PrgPerformStep();
                        }
                        catch (Exception ex)
                        {
                            FailedExecutedStatementsList.Add(CreateStatement(statementsInitial[i], statementsAfter[i], ex.Message));
                            WriteToTranslatedSql("Executing statement " + statementsAfter[i].ToString() + " failed\r\nDetails: " + ex.Message);
                            PrgPerformStep();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please connect to databases first");
                }
            }
            
            FinalResults += "### Succesfully Executed Statements : " + (Executecbx.Checked ? (statementsAfter.Length - FailedExecutedStatementsList.Count).ToString() : "0") + Environment.NewLine;
            FinalResults += "### Failed Executed Statements : " + FailedExecutedStatementsList.Count.ToString() + Environment.NewLine + sharpLine + Environment.NewLine;

            FinalResults += "### Succesfully Compared  Statements : " + comparedStatements + Environment.NewLine;
            FinalResults += "### Failed Compared Statements : " + (FailedComparedStatementsList.Count).ToString() + Environment.NewLine;
            FinalResults += "### Exceptions On DataTables Comparation : " + (dataComparingExceptions).ToString() + Environment.NewLine + sharpLine + Environment.NewLine;

            WriteToTranslatedSql(Environment.NewLine + Environment.NewLine + FinalResults);


            try
            {
                SaveToResultsXml(Application.StartupPath + @"\ResultsLog.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while creating the ResultsLog.xml file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void PrgBarInitVal(string txt)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                lblProgress.Text = txt;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = statementsAfter.Length;
                progressBar1.Value = 0;
                progressBar1.Step = 1;
                lblProgress.Visible = true;
                progressBar1.Visible = true;
            });
        }

        private void PrgPerformStep()
        {
            this.Invoke((MethodInvoker)delegate()
            {
                progressBar1.PerformStep();
                if (progressBar1.Value == progressBar1.Maximum)
                {
                    lblProgress.Visible = false;
                    progressBar1.Visible = false;
                }
            });
        }

        private void WriteToTranslatedSql(string txt)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                if (txt.Length == 0)
                {
                    this.tbTranslatedSql.Clear();
                }
                else
                {
                    this.tbTranslatedSql.Text += txt;
                    this.tbTranslatedSql.SelectionStart = tbTranslatedSql.Text.Length;
                    this.tbTranslatedSql.ScrollToCaret();
                }
            });
        }

        private StringDictionary CreateStatement(string initialSt, string translatedSt, string errMessage)
        {
            StringDictionary myStatement = new StringDictionary();

            myStatement["Original Statement"] = initialSt;
            myStatement["Translated Statement"] = translatedSt;

            if (errMessage.Length > 0)
            {
                myStatement["Error Message"] = errMessage;
            }

            return myStatement;
        }

        private void SaveToResultsXml(string filename)
        {
            XmlDocument xmldoc = new XmlDocument();

            XmlDeclaration xmlDeclaration = xmldoc.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
            xmldoc.InsertBefore(xmlDeclaration, xmldoc.DocumentElement);

            //the root element
            XmlElement xmlRoot = xmldoc.CreateElement("", "root", "");


            XmlElement xmlFailedExecuted, xmlFailedCompared, xmlStatement, xmlOriginalStatement, xmlTranslatedStatement, xmlErrorStatement;
            XmlAttribute xmlAttr;

            xmlFailedExecuted = xmldoc.CreateElement("", "FailedExecutedStatements", "");
            xmlAttr = xmldoc.CreateAttribute("Count");
            xmlAttr.Value = this.FailedExecutedStatementsList.Count.ToString();
            xmlFailedExecuted.Attributes.Append(xmlAttr);

            xmlFailedCompared = xmldoc.CreateElement("", "FailedComparedStatements", "");
            xmlAttr = xmldoc.CreateAttribute("Count");
            xmlAttr.Value = this.FailedComparedStatementsList.Count.ToString();
            xmlFailedCompared.Attributes.Append(xmlAttr);

            for (int i = 0; i < this.FailedExecutedStatementsList.Count; i++)
            {
                xmlStatement = xmldoc.CreateElement("", "Failed_Statement", "");
                xmlAttr = xmldoc.CreateAttribute("ID");
                xmlAttr.Value = i.ToString();

                xmlOriginalStatement= xmldoc.CreateElement("", "Original_Statement", "");
                xmlOriginalStatement.InnerText = ((StringDictionary)this.FailedExecutedStatementsList[i])["Original Statement"];
                
                xmlTranslatedStatement = xmldoc.CreateElement("", "Translated_Statement", "");
                xmlTranslatedStatement.InnerText = ((StringDictionary)this.FailedExecutedStatementsList[i])["Translated Statement"];

                xmlErrorStatement = xmldoc.CreateElement("", "Error_Message", "");
                xmlErrorStatement.InnerText = ((StringDictionary)this.FailedExecutedStatementsList[i])["Error Message"];

                xmlStatement.AppendChild(xmlErrorStatement);
                xmlStatement.AppendChild(xmlOriginalStatement);
                xmlStatement.AppendChild(xmlTranslatedStatement);
                xmlStatement.Attributes.Append(xmlAttr);

                xmlFailedExecuted.AppendChild(xmlStatement);
            }

            for (int i = 0; i < this.FailedComparedStatementsList.Count; i++)
            {
                xmlStatement = xmldoc.CreateElement("", "Failed_Statement", "");
                xmlAttr = xmldoc.CreateAttribute("ID");
                xmlAttr.Value = i.ToString();

                xmlOriginalStatement = xmldoc.CreateElement("", "Original_Statement", "");
                xmlOriginalStatement.InnerText = ((StringDictionary)this.FailedComparedStatementsList[i])["Original Statement"];

                xmlTranslatedStatement = xmldoc.CreateElement("", "Translated_Statement", "");
                xmlTranslatedStatement.InnerText = ((StringDictionary)this.FailedComparedStatementsList[i])["Translated Statement"];

                xmlStatement.AppendChild(xmlOriginalStatement);
                xmlStatement.AppendChild(xmlTranslatedStatement);
                xmlStatement.Attributes.Append(xmlAttr);

                xmlFailedCompared.AppendChild(xmlStatement);
            }

            xmlRoot.AppendChild(xmlFailedExecuted);
            xmlRoot.AppendChild(xmlFailedCompared);
            xmldoc.AppendChild(xmlRoot);

            xmldoc.Save(filename);
        }

        private void WorkSpaceToSettings()
        {
            Globals.Settings.DateFormat = Globals.WkSpace.DateFormat;
            //MU possible not needed here, Settings are also set in CheckedChanged eventHandler of checkboxes
            //Settings.Rowid = (RowidType)Globals.WkSpace.Rowid;
            //Settings.Unicode = Globals.WkSpace.Unicode;
            //Settings.OnParseError = (ParseErrorAction)Globals.WkSpace.ParseError;
            //Settings.SqlBaseVersion = (DatabaseBrand)Globals.WkSpace.SqlBaseVersion;
           
            SetConnProp();
            MapWkSpValuesToCtrls();
        }

        private void SettingsToWorkSpace()
        {
            Globals.WkSpace.SqlBaseConnectionString = Globals.WkSpace.SqlBaseConnectionString;
            Globals.WkSpace.SqlServerConnectionString = Globals.WkSpace.SqlServerConnectionString;
            Globals.WkSpace.OracleConnectionString = Globals.WkSpace.OracleConnectionString;
            Globals.WkSpace.Rowid = (int)Globals.Settings.Rowid;
            Globals.WkSpace.Unicode = Globals.Settings.Unicode;
            Globals.WkSpace.ReadStructure = Globals.Settings.ReadStructure;
            Globals.WkSpace.ParseError = (int)Globals.Settings.OnParseError;
            Globals.WkSpace.SqlBaseVersion = (int)Globals.Settings.SqlBaseVersion;
            Globals.WkSpace.DateFormat = Globals.Settings.DateFormat;
            Globals.WkSpace.ExecuteOption = this.Executecbx.Checked;
            Globals.WkSpace.CompareOption = this.Comparecbx.Checked;
        }

        private void MapWkSpValuesToCtrls()
        {
            if (Globals.WkSpace.SqlBaseScriptFile != null && Globals.WkSpace.SqlBaseScriptFile.Length > 0)
            {
                try
                {
                    tbSourceSql.Text = File.ReadAllText(Globals.WkSpace.SqlBaseScriptFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                tbSourceSql.Text = string.Empty;
            }

            switch (Globals.WkSpace.SqlBaseVersion)
            {
                case 0:
                    {
                        rbSqlBaseVersion2.Checked = true;
                        break;
                    }

                case 1:
                    {
                        rbSqlBaseVersion1.Checked = true;
                        break;
                    }
            }

            switch (Globals.WkSpace.Rowid)
            {
                case 0:
                    {
                        rbTimestamp.Checked = true;
                        break;
                    }

                case 1:
                    {
                        rbUniqueIdentifier.Checked = true;
                        break;
                    }
            }

            if (Globals.WkSpace.Unicode)
            {
                rbUnicodeTrue.Checked = true;   
            }
            else
            {
                rbUnicodeFalse.Checked = true;
            }

            if (Globals.WkSpace.ReadStructure)
            {
                rbReadStructureTrue.Checked = true;
            }
            else
            {
                rbReadStructureFalse.Checked = true;
            }

            switch(Globals.WkSpace.ParseError) 
            {
                case 0 :
                    {
                        rbIgnore.Checked = true;
                        break;
                    }

                case 1:
                    {
                        rbReturnSource.Checked = true;
                        break;
                    }

                case 2:
                    {
                        rbThrowException.Checked = true;
                        break;
                    }
            }

            this.Comparecbx.Checked = Globals.WkSpace.CompareOption;

            this.Executecbx.Checked = Globals.WkSpace.ExecuteOption;

            tbxDtFormat.Text = Globals.WkSpace.DateFormat;

            if (Globals.SqlBaseConnProp.ContainsKey("DATA SOURCE"))
            {
                sqlbasetbx.Text = Globals.SqlBaseConnProp["DATA SOURCE"];
            }
            else
            {
                sqlbasetbx.Text = string.Empty;
            }

            if (Globals.SqlServerConnProp.ContainsKey("INITIAL CATALOG"))
            {
                sqlservertbx.Text = Globals.SqlServerConnProp["INITIAL CATALOG"];
            }
            else
            {
                sqlservertbx.Text = string.Empty;
            }

            if (Globals.OracleConnProp.ContainsKey("DATA SOURCE"))
            {
                oracletbx.Text = Globals.OracleConnProp["DATA SOURCE"];
            }
            else
            {
                oracletbx.Text = string.Empty;
            }
        }

        private void SetConnProp()
        {

            char[] c = new char[] { '=', ';' };
            string[] m_prop;

            Globals.SqlBaseConnProp.Clear();
            Globals.SqlServerConnProp.Clear();
            Globals.OracleConnProp.Clear();

            if (Globals.WkSpace.SqlBaseConnectionString != null && Globals.WkSpace.SqlBaseConnectionString != String.Empty)
            {
                m_prop = Globals.WkSpace.SqlBaseConnectionString.Split(c);

                for (int i = 0; i < m_prop.Length - 1; i += 2)
                {
                    Globals.SqlBaseConnProp[m_prop[i].ToUpper()] = m_prop[i + 1];
                }
            }

            if (Globals.WkSpace.SqlServerConnectionString != null && Globals.WkSpace.SqlServerConnectionString != String.Empty)
            {
                m_prop = Globals.WkSpace.SqlServerConnectionString.Split(c);

                for (int i = 0; i < m_prop.Length - 1; i += 2)
                {
                    Globals.SqlServerConnProp[m_prop[i].ToUpper()] = m_prop[i + 1];
                }
            }

            if (Globals.WkSpace.OracleConnectionString != null && Globals.WkSpace.OracleConnectionString != String.Empty)
            {
                m_prop = Globals.WkSpace.OracleConnectionString.Split(c);

                for (int i = 0; i < m_prop.Length - 1; i += 2)
                {
                    Globals.OracleConnProp[m_prop[i].ToUpper()] = m_prop[i + 1];
                }
            }
        }
        #endregion

        private void btnInformix_Click(object sender, EventArgs e)
        {
            if (statementsAfter[0].ToString().Replace(" ", "").Substring(0, 6).ToUpper() == "SELECT") 
            {
                new Forms.ViewForm(statementsAfter[0].ToString(), executeClass.SqlServerConn, statementsInitial[0].ToString()).Show();
            }
        }

        private void tbSourceSql_TextChanged(object sender, EventArgs e)
        {
            btnInformix.Enabled = false;
        }
    }
}