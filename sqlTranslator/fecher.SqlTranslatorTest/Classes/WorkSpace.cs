using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.ComponentModel;

namespace SQLTranslatorTest
{
    public class WorkSpace
    {
        #region Properties

        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private string sqlBaseConnectionString;

        [DefaultValue("")]
        public string SqlBaseConnectionString
        {
            get { return sqlBaseConnectionString; }
            set { sqlBaseConnectionString = value; }
        }

        private string sqlServerConnectionString;

        [DefaultValue("")]
        public string SqlServerConnectionString
        {
            get { return sqlServerConnectionString; }
            set { sqlServerConnectionString = value; }
        }

        private string oracleConnectionString;

        [DefaultValue("")]
        public string OracleConnectionString
        {
            get { return oracleConnectionString; }
            set { oracleConnectionString = value; }
        }

        private string sqlBaseScriptFile;

        [DefaultValue("")]
        public string SqlBaseScriptFile
        {
            get { return sqlBaseScriptFile; }
            set { sqlBaseScriptFile = value; }
        }

        private string dateFormat;

        [DefaultValue("")]
        public string DateFormat
        {
            get { return dateFormat; }
            set { dateFormat = value; }
        }

        private int sqlBaseVersion;

        [DefaultValue(0)]
        public int SqlBaseVersion
        {
            get { return sqlBaseVersion; }
            set { sqlBaseVersion = value; }
        }

        private int rowid;

        [DefaultValue(0)]
        public int Rowid
        {
            get { return rowid; }
            set { rowid = value; }
        }

        private bool unicode;

        [DefaultValue(false)]
        public bool Unicode
        {
            get { return unicode; }
            set { unicode = value; }
        }

        private bool readStructure;

        [DefaultValue(false)]
        public bool ReadStructure
        {
            get { return readStructure; }
            set { readStructure = value; }
        }

        private int parseError;

        [DefaultValue(0)]
        public int ParseError
        {
            get { return parseError; }
            set { parseError = value; }
        }

        private bool executeOption;

        [DefaultValue(false)]
        public bool ExecuteOption
        {
            get { return executeOption; }
            set { executeOption = value; }
        }

        private bool compareOption;

        [DefaultValue(false)]
        public bool CompareOption
        {
            get { return compareOption; }
            set { compareOption = value; }
        }

        #endregion

        #region Methods
        public void LoadFromXml(string filename)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(filename);
                XmlNode xmlnode = xmldoc.SelectSingleNode("workspace");

                XmlNode dummynode;

                dummynode = xmlnode.SelectSingleNode("SqlBaseConnectionString");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    this.SqlBaseConnectionString = dummynode.Attributes["value"].Value;
                }

                dummynode = xmlnode.SelectSingleNode("SqlServerConnectionString");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    this.sqlServerConnectionString = dummynode.Attributes["value"].Value;
                }

                dummynode = xmlnode.SelectSingleNode("OracleConnectionString");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    this.OracleConnectionString = dummynode.Attributes["value"].Value;
                }

                dummynode = xmlnode.SelectSingleNode("SqlBaseScriptFile");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    this.SqlBaseScriptFile = dummynode.Attributes["value"].Value;
                }

                dummynode = xmlnode.SelectSingleNode("DateFormat");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    this.DateFormat = dummynode.Attributes["value"].Value;
                }

                dummynode = xmlnode.SelectSingleNode("SqlBaseVersion");
                if (dummynode != null && dummynode.Attributes["value"] != null && dummynode.Attributes["value"].Value != String.Empty)
                {
                    this.SqlBaseVersion = Convert.ToInt32(dummynode.Attributes["value"].Value);
                }

                dummynode = xmlnode.SelectSingleNode("Rowid");
                if (dummynode != null && dummynode.Attributes["value"] != null && dummynode.Attributes["value"].Value != String.Empty)
                {
                    this.Rowid = Convert.ToInt32(dummynode.Attributes["value"].Value);
                }

                dummynode = xmlnode.SelectSingleNode("Unicode");
                if (dummynode != null && dummynode.Attributes["value"] != null && dummynode.Attributes["value"].Value != String.Empty)
                {
                    this.Unicode = Convert.ToBoolean(dummynode.Attributes["value"].Value);
                }

                dummynode = xmlnode.SelectSingleNode("ReadStructure");
                if (dummynode != null && dummynode.Attributes["value"] != null && dummynode.Attributes["value"].Value != String.Empty)
                {
                    this.ReadStructure = Convert.ToBoolean(dummynode.Attributes["value"].Value);
                }

                dummynode = xmlnode.SelectSingleNode("ParseError");
                if (dummynode != null && dummynode.Attributes["value"] != null && dummynode.Attributes["value"].Value != String.Empty)
                {
                   this.ParseError = Convert.ToInt32(dummynode.Attributes["value"].Value);
                }

                XmlNode childnode;
                dummynode = xmlnode.SelectSingleNode("Options");
                
                childnode = dummynode.SelectSingleNode("ExecuteOption");
                if (childnode != null && childnode.Attributes["value"] != null && childnode.Attributes["value"].Value != String.Empty)
                {
                    this.ExecuteOption = Convert.ToBoolean(childnode.Attributes["value"].Value);
                }

                childnode = dummynode.SelectSingleNode("CompareOption");
                if (childnode != null && childnode.Attributes["value"] != null && childnode.Attributes["value"].Value != String.Empty)
                {
                    this.CompareOption = Convert.ToBoolean(childnode.Attributes["value"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SaveToXml(string filename)
        {
            //MU --> this line is needed in the event that the default workspace schema changes(by adding a new node setting),
            //the previously saved workspaces won't find the new added node setting to save data
            CreateXmlSchemaWkSpace(filename);
            //<--
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(filename);
                XmlNode xmlnode = xmldoc.SelectSingleNode("workspace");

                XmlNode dummynode;

                dummynode = xmlnode.SelectSingleNode("SqlBaseConnectionString");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    dummynode.Attributes["value"].Value = this.SqlBaseConnectionString;
                }

                dummynode = xmlnode.SelectSingleNode("SqlServerConnectionString");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    dummynode.Attributes["value"].Value = this.sqlServerConnectionString;
                }

                dummynode = xmlnode.SelectSingleNode("OracleConnectionString");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    dummynode.Attributes["value"].Value = this.OracleConnectionString;
                }

                dummynode = xmlnode.SelectSingleNode("SqlBaseScriptFile");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    dummynode.Attributes["value"].Value = this.SqlBaseScriptFile;
                }

                dummynode = xmlnode.SelectSingleNode("DateFormat");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    dummynode.Attributes["value"].Value = this.DateFormat;
                }

                dummynode = xmlnode.SelectSingleNode("SqlBaseVersion");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    dummynode.Attributes["value"].Value = this.SqlBaseVersion.ToString();
                }

                dummynode = xmlnode.SelectSingleNode("Rowid");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    dummynode.Attributes["value"].Value = this.Rowid.ToString();
                }

                dummynode = xmlnode.SelectSingleNode("Unicode");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    dummynode.Attributes["value"].Value = this.Unicode.ToString();
                }

                dummynode = xmlnode.SelectSingleNode("ReadStructure");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    dummynode.Attributes["value"].Value = this.ReadStructure.ToString();
                }

                dummynode = xmlnode.SelectSingleNode("ParseError");
                if (dummynode != null && dummynode.Attributes["value"] != null)
                {
                    dummynode.Attributes["value"].Value = this.ParseError.ToString();
                }

                XmlNode childnode;
                dummynode = xmlnode.SelectSingleNode("Options");

                childnode = dummynode.SelectSingleNode("ExecuteOption");
                if (childnode != null && childnode.Attributes["value"] != null)
                {
                    childnode.Attributes["value"].Value=this.ExecuteOption.ToString();
                }

                childnode = dummynode.SelectSingleNode("CompareOption");
                if (childnode != null && childnode.Attributes["value"] != null)
                {
                    childnode.Attributes["value"].Value=this.CompareOption.ToString();
                }

                xmldoc.Save(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CreateXmlSchemaWkSpace(string filename)
        {
            XmlDocument xmldoc = new XmlDocument();
            
            XmlDeclaration xmlDeclaration = xmldoc.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
            xmldoc.InsertBefore(xmlDeclaration, xmldoc.DocumentElement); 
            
            //the root element
            XmlElement xmlRoot = xmldoc.CreateElement("", "workspace", "");
            xmldoc.AppendChild(xmlRoot);

            XmlElement xmlElemt, xmlElemt1, xmlElemt2;
            XmlAttribute xmlAttr;            

            xmlElemt = xmldoc.CreateElement("", "SqlBaseConnectionString", "");
            xmlAttr = xmldoc.CreateAttribute("value");
            xmlElemt.Attributes.Append(xmlAttr);
            xmlRoot.AppendChild(xmlElemt);

            xmlElemt = xmldoc.CreateElement("", "SqlServerConnectionString", "");
            xmlAttr = xmldoc.CreateAttribute("value");
            xmlElemt.Attributes.Append(xmlAttr);
            xmlRoot.AppendChild(xmlElemt);

            xmlElemt = xmldoc.CreateElement("", "OracleConnectionString", "");
            xmlAttr = xmldoc.CreateAttribute("value");
            xmlElemt.Attributes.Append(xmlAttr);
            xmlRoot.AppendChild(xmlElemt);

            xmlElemt = xmldoc.CreateElement("", "SqlBaseScriptFile", "");
            xmlAttr = xmldoc.CreateAttribute("value");
            xmlElemt.Attributes.Append(xmlAttr);
            xmlRoot.AppendChild(xmlElemt);

            xmlElemt = xmldoc.CreateElement("", "DateFormat", "");
            xmlAttr = xmldoc.CreateAttribute("value");
            xmlElemt.Attributes.Append(xmlAttr);
            xmlRoot.AppendChild(xmlElemt);

            xmlElemt = xmldoc.CreateElement("", "SqlBaseVersion", "");
            xmlAttr = xmldoc.CreateAttribute("value");
            xmlElemt.Attributes.Append(xmlAttr);
            xmlRoot.AppendChild(xmlElemt);

            xmlElemt = xmldoc.CreateElement("", "Rowid", "");
            xmlAttr = xmldoc.CreateAttribute("value");
            xmlElemt.Attributes.Append(xmlAttr);
            xmlRoot.AppendChild(xmlElemt);

            xmlElemt = xmldoc.CreateElement("", "Unicode", "");
            xmlAttr = xmldoc.CreateAttribute("value");
            xmlElemt.Attributes.Append(xmlAttr);
            xmlRoot.AppendChild(xmlElemt);

            xmlElemt = xmldoc.CreateElement("", "ReadStructure", "");
            xmlAttr = xmldoc.CreateAttribute("value");
            xmlElemt.Attributes.Append(xmlAttr);
            xmlRoot.AppendChild(xmlElemt);

            xmlElemt = xmldoc.CreateElement("", "ParseError", "");
            xmlAttr = xmldoc.CreateAttribute("value");
            xmlElemt.Attributes.Append(xmlAttr);

            xmlElemt = xmldoc.CreateElement("", "Options", "");

            xmlElemt1 = xmldoc.CreateElement("", "ExecuteOption", "");
            xmlAttr = xmldoc.CreateAttribute("value");
            xmlElemt1.Attributes.Append(xmlAttr);

            xmlElemt2 = xmldoc.CreateElement("", "CompareOption", "");
            xmlAttr = xmldoc.CreateAttribute("value");
            xmlElemt2.Attributes.Append(xmlAttr);

            xmlElemt.AppendChild(xmlElemt1);
            xmlElemt.AppendChild(xmlElemt2);

            xmlRoot.AppendChild(xmlElemt);

            try 
            {
                xmldoc.Save(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
