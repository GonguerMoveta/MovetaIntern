namespace SQLTranslatorTest
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.SqlStatementslbl = new System.Windows.Forms.Label();
            this.openbtn = new System.Windows.Forms.Button();
            this.sqlbasetbx = new System.Windows.Forms.TextBox();
            this.connectsqlbasebtn = new System.Windows.Forms.Button();
            this.sqlserverrdb = new System.Windows.Forms.RadioButton();
            this.Oraclerdb = new System.Windows.Forms.RadioButton();
            this.oracletbx = new System.Windows.Forms.TextBox();
            this.sqlservertbx = new System.Windows.Forms.TextBox();
            this.Oraclebtn = new System.Windows.Forms.Button();
            this.SQLServerbtn = new System.Windows.Forms.Button();
            this.Startbtn = new System.Windows.Forms.Button();
            this.Translatecbx = new System.Windows.Forms.CheckBox();
            this.Executecbx = new System.Windows.Forms.CheckBox();
            this.Comparecbx = new System.Windows.Forms.CheckBox();
            this.tbTranslatedSql = new System.Windows.Forms.RichTextBox();
            this.tbSourceSql = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.tbxDtFormat = new System.Windows.Forms.MaskedTextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.rbReadStructureTrue = new System.Windows.Forms.RadioButton();
            this.rbReadStructureFalse = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.rbUnicodeTrue = new System.Windows.Forms.RadioButton();
            this.rbUnicodeFalse = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbThrowException = new System.Windows.Forms.RadioButton();
            this.rbIgnore = new System.Windows.Forms.RadioButton();
            this.rbReturnSource = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbTimestamp = new System.Windows.Forms.RadioButton();
            this.rbUniqueIdentifier = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbSqlBaseVersion1 = new System.Windows.Forms.RadioButton();
            this.rbSqlBaseVersion2 = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsOpenBtn = new System.Windows.Forms.ToolStripButton();
            this.tsNewBtn = new System.Windows.Forms.ToolStripButton();
            this.tsSaveBtn = new System.Windows.Forms.ToolStripButton();
            this.tsSaveAsBtn = new System.Windows.Forms.ToolStripButton();
            this.Stopbtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.rbSpTranslator = new System.Windows.Forms.RadioButton();
            this.rbSqlTranslator = new System.Windows.Forms.RadioButton();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btnInformix = new System.Windows.Forms.Button();
            this.cboSourceDB = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.SuspendLayout();
            // 
            // SqlStatementslbl
            // 
            this.SqlStatementslbl.AutoSize = true;
            this.SqlStatementslbl.Location = new System.Drawing.Point(22, 231);
            this.SqlStatementslbl.Name = "SqlStatementslbl";
            this.SqlStatementslbl.Size = new System.Drawing.Size(68, 13);
            this.SqlStatementslbl.TabIndex = 0;
            this.SqlStatementslbl.Text = "Source SQL:";
            // 
            // openbtn
            // 
            this.openbtn.Location = new System.Drawing.Point(522, 375);
            this.openbtn.Name = "openbtn";
            this.openbtn.Size = new System.Drawing.Size(84, 23);
            this.openbtn.TabIndex = 1;
            this.openbtn.Text = "<< From file";
            this.openbtn.UseVisualStyleBackColor = true;
            this.openbtn.Click += new System.EventHandler(this.openbtn_Click);
            // 
            // sqlbasetbx
            // 
            this.sqlbasetbx.Location = new System.Drawing.Point(81, 26);
            this.sqlbasetbx.Name = "sqlbasetbx";
            this.sqlbasetbx.ReadOnly = true;
            this.sqlbasetbx.Size = new System.Drawing.Size(198, 20);
            this.sqlbasetbx.TabIndex = 2;
            // 
            // connectsqlbasebtn
            // 
            this.connectsqlbasebtn.Location = new System.Drawing.Point(285, 25);
            this.connectsqlbasebtn.Name = "connectsqlbasebtn";
            this.connectsqlbasebtn.Size = new System.Drawing.Size(68, 23);
            this.connectsqlbasebtn.TabIndex = 3;
            this.connectsqlbasebtn.Text = "Connect";
            this.connectsqlbasebtn.UseVisualStyleBackColor = true;
            this.connectsqlbasebtn.Click += new System.EventHandler(this.connectsqlbasebtn_Click);
            // 
            // sqlserverrdb
            // 
            this.sqlserverrdb.AutoSize = true;
            this.sqlserverrdb.Checked = true;
            this.sqlserverrdb.Location = new System.Drawing.Point(6, 25);
            this.sqlserverrdb.Name = "sqlserverrdb";
            this.sqlserverrdb.Size = new System.Drawing.Size(71, 17);
            this.sqlserverrdb.TabIndex = 4;
            this.sqlserverrdb.TabStop = true;
            this.sqlserverrdb.Text = "SqlServer";
            this.sqlserverrdb.UseVisualStyleBackColor = true;
            this.sqlserverrdb.CheckedChanged += new System.EventHandler(this.sqlserverrdb_CheckedChanged);
            // 
            // Oraclerdb
            // 
            this.Oraclerdb.AutoSize = true;
            this.Oraclerdb.Location = new System.Drawing.Point(6, 58);
            this.Oraclerdb.Name = "Oraclerdb";
            this.Oraclerdb.Size = new System.Drawing.Size(56, 17);
            this.Oraclerdb.TabIndex = 7;
            this.Oraclerdb.Text = "Oracle";
            this.Oraclerdb.UseVisualStyleBackColor = true;
            this.Oraclerdb.CheckedChanged += new System.EventHandler(this.Oraclerdb_CheckedChanged);
            // 
            // oracletbx
            // 
            this.oracletbx.Location = new System.Drawing.Point(83, 57);
            this.oracletbx.Name = "oracletbx";
            this.oracletbx.ReadOnly = true;
            this.oracletbx.Size = new System.Drawing.Size(198, 20);
            this.oracletbx.TabIndex = 8;
            // 
            // sqlservertbx
            // 
            this.sqlservertbx.Location = new System.Drawing.Point(83, 22);
            this.sqlservertbx.Name = "sqlservertbx";
            this.sqlservertbx.ReadOnly = true;
            this.sqlservertbx.Size = new System.Drawing.Size(198, 20);
            this.sqlservertbx.TabIndex = 5;
            // 
            // Oraclebtn
            // 
            this.Oraclebtn.Enabled = false;
            this.Oraclebtn.Location = new System.Drawing.Point(287, 55);
            this.Oraclebtn.Name = "Oraclebtn";
            this.Oraclebtn.Size = new System.Drawing.Size(68, 23);
            this.Oraclebtn.TabIndex = 9;
            this.Oraclebtn.Text = "Connect";
            this.Oraclebtn.UseVisualStyleBackColor = true;
            this.Oraclebtn.Click += new System.EventHandler(this.Oraclebtn_Click);
            // 
            // SQLServerbtn
            // 
            this.SQLServerbtn.Location = new System.Drawing.Point(287, 21);
            this.SQLServerbtn.Name = "SQLServerbtn";
            this.SQLServerbtn.Size = new System.Drawing.Size(68, 23);
            this.SQLServerbtn.TabIndex = 6;
            this.SQLServerbtn.Text = "Connect";
            this.SQLServerbtn.UseVisualStyleBackColor = true;
            this.SQLServerbtn.Click += new System.EventHandler(this.SQLServerbtn_Click);
            // 
            // Startbtn
            // 
            this.Startbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Startbtn.Location = new System.Drawing.Point(522, 262);
            this.Startbtn.Name = "Startbtn";
            this.Startbtn.Size = new System.Drawing.Size(83, 23);
            this.Startbtn.TabIndex = 13;
            this.Startbtn.Text = "Start";
            this.Startbtn.UseVisualStyleBackColor = true;
            this.Startbtn.Click += new System.EventHandler(this.Startbtn_Click);
            // 
            // Translatecbx
            // 
            this.Translatecbx.AutoSize = true;
            this.Translatecbx.Checked = true;
            this.Translatecbx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Translatecbx.Enabled = false;
            this.Translatecbx.Location = new System.Drawing.Point(11, 19);
            this.Translatecbx.Name = "Translatecbx";
            this.Translatecbx.Size = new System.Drawing.Size(70, 17);
            this.Translatecbx.TabIndex = 15;
            this.Translatecbx.Text = "Translate";
            this.Translatecbx.UseVisualStyleBackColor = true;
            // 
            // Executecbx
            // 
            this.Executecbx.AutoSize = true;
            this.Executecbx.Location = new System.Drawing.Point(11, 41);
            this.Executecbx.Name = "Executecbx";
            this.Executecbx.Size = new System.Drawing.Size(65, 17);
            this.Executecbx.TabIndex = 16;
            this.Executecbx.Text = "Execute";
            this.Executecbx.UseVisualStyleBackColor = true;
            this.Executecbx.CheckedChanged += new System.EventHandler(this.Executecbx_CheckedChanged);
            // 
            // Comparecbx
            // 
            this.Comparecbx.AutoSize = true;
            this.Comparecbx.Location = new System.Drawing.Point(11, 63);
            this.Comparecbx.Name = "Comparecbx";
            this.Comparecbx.Size = new System.Drawing.Size(68, 17);
            this.Comparecbx.TabIndex = 17;
            this.Comparecbx.Text = "Compare";
            this.Comparecbx.UseVisualStyleBackColor = true;
            this.Comparecbx.CheckedChanged += new System.EventHandler(this.Comparecbx_CheckedChanged);
            // 
            // tbTranslatedSql
            // 
            this.tbTranslatedSql.BackColor = System.Drawing.Color.White;
            this.tbTranslatedSql.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTranslatedSql.Location = new System.Drawing.Point(611, 247);
            this.tbTranslatedSql.Name = "tbTranslatedSql";
            this.tbTranslatedSql.Size = new System.Drawing.Size(490, 420);
            this.tbTranslatedSql.TabIndex = 18;
            this.tbTranslatedSql.Text = "";
            // 
            // tbSourceSql
            // 
            this.tbSourceSql.BackColor = System.Drawing.Color.White;
            this.tbSourceSql.Location = new System.Drawing.Point(25, 247);
            this.tbSourceSql.Name = "tbSourceSql";
            this.tbSourceSql.Size = new System.Drawing.Size(490, 420);
            this.tbSourceSql.TabIndex = 21;
            this.tbSourceSql.Text = "";
            this.tbSourceSql.TextChanged += new System.EventHandler(this.tbSourceSql_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Executecbx);
            this.groupBox1.Controls.Add(this.Translatecbx);
            this.groupBox1.Controls.Add(this.Comparecbx);
            this.groupBox1.Location = new System.Drawing.Point(445, 119);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(146, 98);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.sqlserverrdb);
            this.groupBox2.Controls.Add(this.Oraclerdb);
            this.groupBox2.Controls.Add(this.oracletbx);
            this.groupBox2.Controls.Add(this.sqlservertbx);
            this.groupBox2.Controls.Add(this.SQLServerbtn);
            this.groupBox2.Controls.Add(this.Oraclebtn);
            this.groupBox2.Location = new System.Drawing.Point(25, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(393, 92);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Target Database";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(608, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Translated SQL:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox9);
            this.groupBox3.Controls.Add(this.groupBox8);
            this.groupBox3.Controls.Add(this.groupBox7);
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new System.Drawing.Point(611, 31);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(490, 186);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Settings";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.tbxDtFormat);
            this.groupBox9.Location = new System.Drawing.Point(229, 123);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(91, 45);
            this.groupBox9.TabIndex = 7;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "DateFormat";
            // 
            // tbxDtFormat
            // 
            this.tbxDtFormat.Location = new System.Drawing.Point(5, 19);
            this.tbxDtFormat.Mask = "LLL";
            this.tbxDtFormat.Name = "tbxDtFormat";
            this.tbxDtFormat.Size = new System.Drawing.Size(80, 20);
            this.tbxDtFormat.TabIndex = 20;
            this.tbxDtFormat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbxDtFormat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbxDtFormat_KeyUp);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.rbReadStructureTrue);
            this.groupBox8.Controls.Add(this.rbReadStructureFalse);
            this.groupBox8.Location = new System.Drawing.Point(229, 19);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(91, 98);
            this.groupBox8.TabIndex = 7;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "ReadStructure";
            // 
            // rbReadStructureTrue
            // 
            this.rbReadStructureTrue.AutoSize = true;
            this.rbReadStructureTrue.Checked = true;
            this.rbReadStructureTrue.Location = new System.Drawing.Point(6, 60);
            this.rbReadStructureTrue.Name = "rbReadStructureTrue";
            this.rbReadStructureTrue.Size = new System.Drawing.Size(47, 17);
            this.rbReadStructureTrue.TabIndex = 1;
            this.rbReadStructureTrue.TabStop = true;
            this.rbReadStructureTrue.Text = "True";
            this.rbReadStructureTrue.UseVisualStyleBackColor = true;
            // 
            // rbReadStructureFalse
            // 
            this.rbReadStructureFalse.AutoSize = true;
            this.rbReadStructureFalse.Location = new System.Drawing.Point(6, 22);
            this.rbReadStructureFalse.Name = "rbReadStructureFalse";
            this.rbReadStructureFalse.Size = new System.Drawing.Size(50, 17);
            this.rbReadStructureFalse.TabIndex = 0;
            this.rbReadStructureFalse.Text = "False";
            this.rbReadStructureFalse.UseVisualStyleBackColor = true;
            this.rbReadStructureFalse.CheckedChanged += new System.EventHandler(this.rbReadStructureFalse_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rbUnicodeTrue);
            this.groupBox7.Controls.Add(this.rbUnicodeFalse);
            this.groupBox7.Location = new System.Drawing.Point(12, 123);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(204, 45);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Unicode";
            // 
            // rbUnicodeTrue
            // 
            this.rbUnicodeTrue.AutoSize = true;
            this.rbUnicodeTrue.Location = new System.Drawing.Point(107, 22);
            this.rbUnicodeTrue.Name = "rbUnicodeTrue";
            this.rbUnicodeTrue.Size = new System.Drawing.Size(47, 17);
            this.rbUnicodeTrue.TabIndex = 1;
            this.rbUnicodeTrue.Text = "True";
            this.rbUnicodeTrue.UseVisualStyleBackColor = true;
            // 
            // rbUnicodeFalse
            // 
            this.rbUnicodeFalse.AutoSize = true;
            this.rbUnicodeFalse.Checked = true;
            this.rbUnicodeFalse.Location = new System.Drawing.Point(6, 22);
            this.rbUnicodeFalse.Name = "rbUnicodeFalse";
            this.rbUnicodeFalse.Size = new System.Drawing.Size(50, 17);
            this.rbUnicodeFalse.TabIndex = 0;
            this.rbUnicodeFalse.TabStop = true;
            this.rbUnicodeFalse.Text = "False";
            this.rbUnicodeFalse.UseVisualStyleBackColor = true;
            this.rbUnicodeFalse.CheckedChanged += new System.EventHandler(this.rbUnicodeFalse_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbThrowException);
            this.groupBox6.Controls.Add(this.rbIgnore);
            this.groupBox6.Controls.Add(this.rbReturnSource);
            this.groupBox6.Location = new System.Drawing.Point(334, 19);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(128, 149);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "On Parse Error";
            // 
            // rbThrowException
            // 
            this.rbThrowException.AutoSize = true;
            this.rbThrowException.Location = new System.Drawing.Point(8, 116);
            this.rbThrowException.Name = "rbThrowException";
            this.rbThrowException.Size = new System.Drawing.Size(105, 17);
            this.rbThrowException.TabIndex = 2;
            this.rbThrowException.Text = "Throw Exception";
            this.rbThrowException.UseVisualStyleBackColor = true;
            // 
            // rbIgnore
            // 
            this.rbIgnore.AutoSize = true;
            this.rbIgnore.Checked = true;
            this.rbIgnore.Location = new System.Drawing.Point(8, 22);
            this.rbIgnore.Name = "rbIgnore";
            this.rbIgnore.Size = new System.Drawing.Size(55, 17);
            this.rbIgnore.TabIndex = 1;
            this.rbIgnore.TabStop = true;
            this.rbIgnore.Text = "Ignore";
            this.rbIgnore.UseVisualStyleBackColor = true;
            this.rbIgnore.CheckedChanged += new System.EventHandler(this.rbReturnSource_CheckedChanged);
            // 
            // rbReturnSource
            // 
            this.rbReturnSource.AutoSize = true;
            this.rbReturnSource.Location = new System.Drawing.Point(8, 69);
            this.rbReturnSource.Name = "rbReturnSource";
            this.rbReturnSource.Size = new System.Drawing.Size(94, 17);
            this.rbReturnSource.TabIndex = 0;
            this.rbReturnSource.Text = "Return Source";
            this.rbReturnSource.UseVisualStyleBackColor = true;
            this.rbReturnSource.CheckedChanged += new System.EventHandler(this.rbReturnSource_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbTimestamp);
            this.groupBox5.Controls.Add(this.rbUniqueIdentifier);
            this.groupBox5.Location = new System.Drawing.Point(12, 72);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(205, 45);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Rowid";
            // 
            // rbTimestamp
            // 
            this.rbTimestamp.AutoSize = true;
            this.rbTimestamp.Checked = true;
            this.rbTimestamp.Location = new System.Drawing.Point(6, 22);
            this.rbTimestamp.Name = "rbTimestamp";
            this.rbTimestamp.Size = new System.Drawing.Size(76, 17);
            this.rbTimestamp.TabIndex = 1;
            this.rbTimestamp.TabStop = true;
            this.rbTimestamp.Text = "Timestamp";
            this.rbTimestamp.UseVisualStyleBackColor = true;
            // 
            // rbUniqueIdentifier
            // 
            this.rbUniqueIdentifier.AutoSize = true;
            this.rbUniqueIdentifier.Location = new System.Drawing.Point(105, 22);
            this.rbUniqueIdentifier.Name = "rbUniqueIdentifier";
            this.rbUniqueIdentifier.Size = new System.Drawing.Size(99, 17);
            this.rbUniqueIdentifier.TabIndex = 0;
            this.rbUniqueIdentifier.Text = "UniqueIdentifier";
            this.rbUniqueIdentifier.UseVisualStyleBackColor = true;
            this.rbUniqueIdentifier.CheckedChanged += new System.EventHandler(this.rbUniqueIdentifier_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbSqlBaseVersion1);
            this.groupBox4.Controls.Add(this.rbSqlBaseVersion2);
            this.groupBox4.Location = new System.Drawing.Point(12, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(205, 44);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SqlBase version";
            // 
            // rbSqlBaseVersion1
            // 
            this.rbSqlBaseVersion1.AutoSize = true;
            this.rbSqlBaseVersion1.Checked = true;
            this.rbSqlBaseVersion1.Location = new System.Drawing.Point(6, 22);
            this.rbSqlBaseVersion1.Name = "rbSqlBaseVersion1";
            this.rbSqlBaseVersion1.Size = new System.Drawing.Size(49, 17);
            this.rbSqlBaseVersion1.TabIndex = 1;
            this.rbSqlBaseVersion1.TabStop = true;
            this.rbSqlBaseVersion1.Text = "> 9.0";
            this.rbSqlBaseVersion1.UseVisualStyleBackColor = true;
            // 
            // rbSqlBaseVersion2
            // 
            this.rbSqlBaseVersion2.AutoSize = true;
            this.rbSqlBaseVersion2.Location = new System.Drawing.Point(105, 22);
            this.rbSqlBaseVersion2.Name = "rbSqlBaseVersion2";
            this.rbSqlBaseVersion2.Size = new System.Drawing.Size(49, 17);
            this.rbSqlBaseVersion2.TabIndex = 0;
            this.rbSqlBaseVersion2.Text = "< 9.0";
            this.rbSqlBaseVersion2.UseVisualStyleBackColor = true;
            this.rbSqlBaseVersion2.CheckedChanged += new System.EventHandler(this.rbSqlBaseVersion2_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(522, 404);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 23);
            this.btnSave.TabIndex = 26;
            this.btnSave.Text = "To file >>";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsOpenBtn,
            this.tsNewBtn,
            this.tsSaveBtn,
            this.tsSaveAsBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1115, 25);
            this.toolStrip1.TabIndex = 27;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsOpenBtn
            // 
            this.tsOpenBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsOpenBtn.Image = ((System.Drawing.Image)(resources.GetObject("tsOpenBtn.Image")));
            this.tsOpenBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpenBtn.Name = "tsOpenBtn";
            this.tsOpenBtn.Size = new System.Drawing.Size(23, 22);
            this.tsOpenBtn.Text = "Open Workspace";
            this.tsOpenBtn.Click += new System.EventHandler(this.tsOpenBtn_Click);
            // 
            // tsNewBtn
            // 
            this.tsNewBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNewBtn.Image = ((System.Drawing.Image)(resources.GetObject("tsNewBtn.Image")));
            this.tsNewBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNewBtn.Name = "tsNewBtn";
            this.tsNewBtn.Size = new System.Drawing.Size(23, 22);
            this.tsNewBtn.Text = "New Workspace";
            this.tsNewBtn.Click += new System.EventHandler(this.tsNewBtn_Click);
            // 
            // tsSaveBtn
            // 
            this.tsSaveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSaveBtn.Enabled = false;
            this.tsSaveBtn.Image = ((System.Drawing.Image)(resources.GetObject("tsSaveBtn.Image")));
            this.tsSaveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSaveBtn.Name = "tsSaveBtn";
            this.tsSaveBtn.Size = new System.Drawing.Size(23, 22);
            this.tsSaveBtn.Text = "Save current Workspace";
            this.tsSaveBtn.Click += new System.EventHandler(this.tsSaveBtn_Click);
            // 
            // tsSaveAsBtn
            // 
            this.tsSaveAsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSaveAsBtn.Image = ((System.Drawing.Image)(resources.GetObject("tsSaveAsBtn.Image")));
            this.tsSaveAsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSaveAsBtn.Name = "tsSaveAsBtn";
            this.tsSaveAsBtn.Size = new System.Drawing.Size(23, 22);
            this.tsSaveAsBtn.Text = "Save WorkSpace As";
            this.tsSaveAsBtn.Click += new System.EventHandler(this.tsSaveAsBtn_Click);
            // 
            // Stopbtn
            // 
            this.Stopbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stopbtn.Location = new System.Drawing.Point(522, 291);
            this.Stopbtn.Name = "Stopbtn";
            this.Stopbtn.Size = new System.Drawing.Size(83, 23);
            this.Stopbtn.TabIndex = 28;
            this.Stopbtn.Text = "Stop";
            this.Stopbtn.UseVisualStyleBackColor = true;
            this.Stopbtn.Click += new System.EventHandler(this.Stopbtn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(521, 643);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(84, 23);
            this.progressBar1.TabIndex = 29;
            this.progressBar1.Visible = false;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(525, 624);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 13);
            this.lblProgress.TabIndex = 30;
            this.lblProgress.Visible = false;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.rbSpTranslator);
            this.groupBox10.Controls.Add(this.rbSqlTranslator);
            this.groupBox10.Location = new System.Drawing.Point(445, 31);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(146, 81);
            this.groupBox10.TabIndex = 31;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Type";
            // 
            // rbSpTranslator
            // 
            this.rbSpTranslator.AutoSize = true;
            this.rbSpTranslator.Location = new System.Drawing.Point(11, 45);
            this.rbSpTranslator.Name = "rbSpTranslator";
            this.rbSpTranslator.Size = new System.Drawing.Size(85, 17);
            this.rbSpTranslator.TabIndex = 1;
            this.rbSpTranslator.Text = "SpTranslator";
            this.rbSpTranslator.UseVisualStyleBackColor = true;
            // 
            // rbSqlTranslator
            // 
            this.rbSqlTranslator.AutoSize = true;
            this.rbSqlTranslator.Checked = true;
            this.rbSqlTranslator.Location = new System.Drawing.Point(11, 22);
            this.rbSqlTranslator.Name = "rbSqlTranslator";
            this.rbSqlTranslator.Size = new System.Drawing.Size(87, 17);
            this.rbSqlTranslator.TabIndex = 0;
            this.rbSqlTranslator.TabStop = true;
            this.rbSqlTranslator.Text = "SqlTranslator";
            this.rbSqlTranslator.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.cboSourceDB);
            this.groupBox11.Controls.Add(this.sqlbasetbx);
            this.groupBox11.Controls.Add(this.connectsqlbasebtn);
            this.groupBox11.Location = new System.Drawing.Point(25, 31);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(393, 87);
            this.groupBox11.TabIndex = 13;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Source Database";
            // 
            // btnInformix
            // 
            this.btnInformix.Enabled = false;
            this.btnInformix.Location = new System.Drawing.Point(522, 433);
            this.btnInformix.Name = "btnInformix";
            this.btnInformix.Size = new System.Drawing.Size(84, 23);
            this.btnInformix.TabIndex = 32;
            this.btnInformix.Text = "View result";
            this.btnInformix.UseVisualStyleBackColor = true;
            this.btnInformix.Click += new System.EventHandler(this.btnInformix_Click);
            // 
            // cboSourceDB
            // 
            this.cboSourceDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSourceDB.FormattingEnabled = true;
            this.cboSourceDB.Items.AddRange(new object[] {
            "SqlBase",
            "Informix",
            "Access"});
            this.cboSourceDB.Location = new System.Drawing.Point(0, 25);
            this.cboSourceDB.Name = "cboSourceDB";
            this.cboSourceDB.Size = new System.Drawing.Size(80, 21);
            this.cboSourceDB.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 676);
            this.Controls.Add(this.btnInformix);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Stopbtn);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbSourceSql);
            this.Controls.Add(this.tbTranslatedSql);
            this.Controls.Add(this.Startbtn);
            this.Controls.Add(this.openbtn);
            this.Controls.Add(this.SqlStatementslbl);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SqlTranslator Test";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SqlStatementslbl;
        private System.Windows.Forms.Button openbtn;
        private System.Windows.Forms.TextBox sqlbasetbx;
        private System.Windows.Forms.Button connectsqlbasebtn;
        private System.Windows.Forms.RadioButton sqlserverrdb;
        private System.Windows.Forms.RadioButton Oraclerdb;
        private System.Windows.Forms.TextBox oracletbx;
        private System.Windows.Forms.TextBox sqlservertbx;
        private System.Windows.Forms.Button Oraclebtn;
        private System.Windows.Forms.Button SQLServerbtn;
        private System.Windows.Forms.Button Startbtn;
        private System.Windows.Forms.CheckBox Translatecbx;
        private System.Windows.Forms.CheckBox Executecbx;
        private System.Windows.Forms.CheckBox Comparecbx;
        private System.Windows.Forms.RichTextBox tbTranslatedSql;
        private System.Windows.Forms.RichTextBox tbSourceSql;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbSqlBaseVersion1;
        private System.Windows.Forms.RadioButton rbSqlBaseVersion2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbTimestamp;
        private System.Windows.Forms.RadioButton rbUniqueIdentifier;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rbIgnore;
        private System.Windows.Forms.RadioButton rbReturnSource;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton rbUnicodeTrue;
        private System.Windows.Forms.RadioButton rbUnicodeFalse;
        private System.Windows.Forms.RadioButton rbThrowException;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsOpenBtn;
        private System.Windows.Forms.ToolStripButton tsNewBtn;
        private System.Windows.Forms.ToolStripButton tsSaveBtn;
        private System.Windows.Forms.Button Stopbtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton rbReadStructureTrue;
        private System.Windows.Forms.RadioButton rbReadStructureFalse;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.MaskedTextBox tbxDtFormat;
        private System.Windows.Forms.ToolStripButton tsSaveAsBtn;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.RadioButton rbSpTranslator;
        private System.Windows.Forms.RadioButton rbSqlTranslator;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button btnInformix;
        private System.Windows.Forms.ComboBox cboSourceDB;
    }
}

