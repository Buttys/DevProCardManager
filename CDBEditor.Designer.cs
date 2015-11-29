namespace DevPro_CardManager
{
    sealed partial class CDBEditor
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.CardID = new System.Windows.Forms.TextBox();
            this.Alias = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CardFormats = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SetCodeLst = new System.Windows.Forms.ComboBox();
            this.OtherSetCodeLst = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Level = new System.Windows.Forms.ComboBox();
            this.Race = new System.Windows.Forms.ComboBox();
            this.CardAttribute = new System.Windows.Forms.ComboBox();
            this.ATK = new System.Windows.Forms.TextBox();
            this.DEF = new System.Windows.Forms.MaskedTextBox();
            this.chkPre = new System.Windows.Forms.CheckBox();
            this.PreLbl = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.CardName = new System.Windows.Forms.TextBox();
            this.CardDescription = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.Clearbtn = new System.Windows.Forms.Button();
            this.SaveCardbtn = new System.Windows.Forms.Button();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.EffectList = new System.Windows.Forms.ListBox();
            this.EffectInput = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.DeleteEffectbtn = new System.Windows.Forms.Button();
            this.MoveEffectUp = new System.Windows.Forms.Button();
            this.MoveEffectDown = new System.Windows.Forms.Button();
            this.AddEffectbtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CardTypeList = new System.Windows.Forms.CheckedListBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.CategoryList = new System.Windows.Forms.CheckedListBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.OpenScriptBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.CardImg = new System.Windows.Forms.PictureBox();
            this.LoadImageBtn = new System.Windows.Forms.Button();
            this.SearchBox = new DevPro_CardManager.Components.SearchBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CardImg)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 177F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(803, 600);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.90071F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.09929F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox4, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel2, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.groupBox5, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel3, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(180, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 299F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 245F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(620, 594);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(328, 293);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Card Info";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.06897F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.93103F));
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.CardID, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.Alias, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.CardFormats, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.SetCodeLst, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.OtherSetCodeLst, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel4.Controls.Add(this.label7, 0, 7);
            this.tableLayoutPanel4.Controls.Add(this.label8, 0, 8);
            this.tableLayoutPanel4.Controls.Add(this.label9, 0, 9);
            this.tableLayoutPanel4.Controls.Add(this.Level, 1, 5);
            this.tableLayoutPanel4.Controls.Add(this.Race, 1, 6);
            this.tableLayoutPanel4.Controls.Add(this.CardAttribute, 1, 7);
            this.tableLayoutPanel4.Controls.Add(this.ATK, 1, 8);
            this.tableLayoutPanel4.Controls.Add(this.DEF, 1, 9);
            this.tableLayoutPanel4.Controls.Add(this.chkPre, 1, 10);
            this.tableLayoutPanel4.Controls.Add(this.PreLbl, 0, 10);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 11;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(322, 274);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Alias";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CardID
            // 
            this.CardID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CardID.Location = new System.Drawing.Point(122, 3);
            this.CardID.Name = "CardID";
            this.CardID.Size = new System.Drawing.Size(197, 20);
            this.CardID.TabIndex = 0;
            // 
            // Alias
            // 
            this.Alias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Alias.Location = new System.Drawing.Point(122, 28);
            this.Alias.Name = "Alias";
            this.Alias.Size = new System.Drawing.Size(197, 20);
            this.Alias.TabIndex = 1;
            this.Alias.Text = "0";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CardFormats
            // 
            this.CardFormats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CardFormats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CardFormats.FormattingEnabled = true;
            this.CardFormats.Location = new System.Drawing.Point(122, 53);
            this.CardFormats.Name = "CardFormats";
            this.CardFormats.Size = new System.Drawing.Size(197, 21);
            this.CardFormats.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Card Format";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 25);
            this.label4.TabIndex = 18;
            this.label4.Text = "Set Codes";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SetCodeLst
            // 
            this.SetCodeLst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SetCodeLst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SetCodeLst.FormattingEnabled = true;
            this.SetCodeLst.Location = new System.Drawing.Point(122, 78);
            this.SetCodeLst.Name = "SetCodeLst";
            this.SetCodeLst.Size = new System.Drawing.Size(197, 21);
            this.SetCodeLst.TabIndex = 19;
            // 
            // OtherSetCodeLst
            // 
            this.OtherSetCodeLst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OtherSetCodeLst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OtherSetCodeLst.FormattingEnabled = true;
            this.OtherSetCodeLst.Location = new System.Drawing.Point(122, 103);
            this.OtherSetCodeLst.Name = "OtherSetCodeLst";
            this.OtherSetCodeLst.Size = new System.Drawing.Size(197, 21);
            this.OtherSetCodeLst.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 25);
            this.label5.TabIndex = 21;
            this.label5.Text = "Level";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 25);
            this.label6.TabIndex = 22;
            this.label6.Text = "Race";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 25);
            this.label7.TabIndex = 23;
            this.label7.Text = "Attribute";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 200);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 25);
            this.label8.TabIndex = 24;
            this.label8.Text = "ATK";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 25);
            this.label9.TabIndex = 25;
            this.label9.Text = "DEF";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Level
            // 
            this.Level.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Level.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Level.FormattingEnabled = true;
            this.Level.Location = new System.Drawing.Point(122, 128);
            this.Level.Name = "Level";
            this.Level.Size = new System.Drawing.Size(197, 21);
            this.Level.TabIndex = 26;
            // 
            // Race
            // 
            this.Race.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Race.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Race.FormattingEnabled = true;
            this.Race.Location = new System.Drawing.Point(122, 153);
            this.Race.Name = "Race";
            this.Race.Size = new System.Drawing.Size(197, 21);
            this.Race.TabIndex = 27;
            // 
            // CardAttribute
            // 
            this.CardAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CardAttribute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CardAttribute.FormattingEnabled = true;
            this.CardAttribute.Location = new System.Drawing.Point(122, 178);
            this.CardAttribute.Name = "CardAttribute";
            this.CardAttribute.Size = new System.Drawing.Size(197, 21);
            this.CardAttribute.TabIndex = 28;
            // 
            // ATK
            // 
            this.ATK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ATK.Location = new System.Drawing.Point(122, 203);
            this.ATK.Name = "ATK";
            this.ATK.Size = new System.Drawing.Size(197, 20);
            this.ATK.TabIndex = 29;
            this.ATK.Text = "0";
            // 
            // DEF
            // 
            this.DEF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DEF.Location = new System.Drawing.Point(122, 228);
            this.DEF.Name = "DEF";
            this.DEF.Size = new System.Drawing.Size(197, 20);
            this.DEF.TabIndex = 30;
            this.DEF.Text = "0";
            // 
            // chkPre
            // 
            this.chkPre.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkPre.AutoSize = true;
            this.chkPre.Location = new System.Drawing.Point(213, 255);
            this.chkPre.Name = "chkPre";
            this.chkPre.Size = new System.Drawing.Size(15, 14);
            this.chkPre.TabIndex = 31;
            this.chkPre.UseVisualStyleBackColor = true;
            // 
            // PreLbl
            // 
            this.PreLbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PreLbl.AutoSize = true;
            this.PreLbl.Location = new System.Drawing.Point(48, 255);
            this.PreLbl.Name = "PreLbl";
            this.PreLbl.Size = new System.Drawing.Size(68, 13);
            this.PreLbl.TabIndex = 32;
            this.PreLbl.Text = "Is Prerelease";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel6);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(337, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(280, 293);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Card Text";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.58124F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.41875F));
            this.tableLayoutPanel6.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label11, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.CardName, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.CardDescription, 1, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(274, 274);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(51, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Name";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(26, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Description";
            // 
            // CardName
            // 
            this.CardName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CardName.Location = new System.Drawing.Point(92, 3);
            this.CardName.Name = "CardName";
            this.CardName.Size = new System.Drawing.Size(179, 20);
            this.CardName.TabIndex = 2;
            // 
            // CardDescription
            // 
            this.CardDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CardDescription.Location = new System.Drawing.Point(92, 28);
            this.CardDescription.Multiline = true;
            this.CardDescription.Name = "CardDescription";
            this.CardDescription.Size = new System.Drawing.Size(179, 243);
            this.CardDescription.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.Clearbtn);
            this.flowLayoutPanel2.Controls.Add(this.SaveCardbtn);
            this.flowLayoutPanel2.Controls.Add(this.DeleteBtn);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(337, 547);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(280, 34);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // Clearbtn
            // 
            this.Clearbtn.Location = new System.Drawing.Point(194, 3);
            this.Clearbtn.Name = "Clearbtn";
            this.Clearbtn.Size = new System.Drawing.Size(83, 23);
            this.Clearbtn.TabIndex = 0;
            this.Clearbtn.Text = "Clear";
            this.Clearbtn.UseVisualStyleBackColor = true;
            this.Clearbtn.Click += new System.EventHandler(this.Clearbtn_Click);
            // 
            // SaveCardbtn
            // 
            this.SaveCardbtn.Location = new System.Drawing.Point(105, 3);
            this.SaveCardbtn.Name = "SaveCardbtn";
            this.SaveCardbtn.Size = new System.Drawing.Size(83, 23);
            this.SaveCardbtn.TabIndex = 1;
            this.SaveCardbtn.Text = "Save Card";
            this.SaveCardbtn.UseVisualStyleBackColor = true;
            this.SaveCardbtn.Click += new System.EventHandler(this.SaveCardbtn_Click);
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Location = new System.Drawing.Point(16, 3);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(83, 23);
            this.DeleteBtn.TabIndex = 2;
            this.DeleteBtn.Text = "Delete Card";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel7);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(337, 302);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(280, 239);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Card Effect Text";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.95454F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.04546F));
            this.tableLayoutPanel7.Controls.Add(this.EffectList, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.EffectInput, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.AddEffectbtn, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.38342F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.61658F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(274, 220);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // EffectList
            // 
            this.EffectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EffectList.FormattingEnabled = true;
            this.EffectList.Location = new System.Drawing.Point(93, 3);
            this.EffectList.Name = "EffectList";
            this.EffectList.Size = new System.Drawing.Size(178, 175);
            this.EffectList.TabIndex = 1;
            // 
            // EffectInput
            // 
            this.EffectInput.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.EffectInput.Location = new System.Drawing.Point(93, 190);
            this.EffectInput.Name = "EffectInput";
            this.EffectInput.Size = new System.Drawing.Size(172, 20);
            this.EffectInput.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.DeleteEffectbtn);
            this.flowLayoutPanel1.Controls.Add(this.MoveEffectUp);
            this.flowLayoutPanel1.Controls.Add(this.MoveEffectDown);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(84, 175);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // DeleteEffectbtn
            // 
            this.DeleteEffectbtn.Location = new System.Drawing.Point(6, 3);
            this.DeleteEffectbtn.Name = "DeleteEffectbtn";
            this.DeleteEffectbtn.Size = new System.Drawing.Size(75, 23);
            this.DeleteEffectbtn.TabIndex = 1;
            this.DeleteEffectbtn.Text = "Delete";
            this.DeleteEffectbtn.UseVisualStyleBackColor = true;
            this.DeleteEffectbtn.Click += new System.EventHandler(this.DeleteEffectbtn_Click);
            // 
            // MoveEffectUp
            // 
            this.MoveEffectUp.Location = new System.Drawing.Point(6, 32);
            this.MoveEffectUp.Name = "MoveEffectUp";
            this.MoveEffectUp.Size = new System.Drawing.Size(75, 23);
            this.MoveEffectUp.TabIndex = 2;
            this.MoveEffectUp.Text = "Up";
            this.MoveEffectUp.UseVisualStyleBackColor = true;
            this.MoveEffectUp.Click += new System.EventHandler(this.MoveEffectUp_Click);
            // 
            // MoveEffectDown
            // 
            this.MoveEffectDown.Location = new System.Drawing.Point(6, 61);
            this.MoveEffectDown.Name = "MoveEffectDown";
            this.MoveEffectDown.Size = new System.Drawing.Size(75, 23);
            this.MoveEffectDown.TabIndex = 3;
            this.MoveEffectDown.Text = "Down";
            this.MoveEffectDown.UseVisualStyleBackColor = true;
            this.MoveEffectDown.Click += new System.EventHandler(this.MoveEffectDown_Click);
            // 
            // AddEffectbtn
            // 
            this.AddEffectbtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.AddEffectbtn.Location = new System.Drawing.Point(12, 189);
            this.AddEffectbtn.Name = "AddEffectbtn";
            this.AddEffectbtn.Size = new System.Drawing.Size(75, 23);
            this.AddEffectbtn.TabIndex = 4;
            this.AddEffectbtn.Text = "Add";
            this.AddEffectbtn.UseVisualStyleBackColor = true;
            this.AddEffectbtn.Click += new System.EventHandler(this.AddEffectbtn_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.groupBox6, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 302);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(328, 239);
            this.tableLayoutPanel5.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CardTypeList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 233);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Card Types";
            // 
            // CardTypeList
            // 
            this.CardTypeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CardTypeList.FormattingEnabled = true;
            this.CardTypeList.Location = new System.Drawing.Point(3, 16);
            this.CardTypeList.Name = "CardTypeList";
            this.CardTypeList.Size = new System.Drawing.Size(152, 214);
            this.CardTypeList.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.CategoryList);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(167, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(158, 233);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Card Category";
            // 
            // CategoryList
            // 
            this.CategoryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CategoryList.FormattingEnabled = true;
            this.CategoryList.Items.AddRange(new object[] {
            "S/T Destory",
            "Destory Monster",
            "Banish",
            "Graveyard",
            "Back to Hand",
            "Back to Deck",
            "Destory Hand",
            "Destory Deck",
            "Draw",
            "Search",
            "Recovery",
            "Position",
            "Control",
            "Change ATK/DEF",
            "Piercing",
            "Repeat Attack",
            "Limit Attack",
            "Direct Attack",
            "Special Summon",
            "Token",
            "Type-Related",
            "Property-Related",
            "Damage LP",
            "Recover LP",
            "Destory",
            "Select",
            "Counter",
            "Gamble",
            "Fusion-Related",
            "Tuner-Related",
            "Xyz-Related",
            "Negate Effect"});
            this.CategoryList.Location = new System.Drawing.Point(3, 16);
            this.CategoryList.Name = "CategoryList";
            this.CategoryList.Size = new System.Drawing.Size(152, 214);
            this.CategoryList.TabIndex = 0;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.OpenScriptBtn);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 547);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(328, 34);
            this.flowLayoutPanel3.TabIndex = 7;
            // 
            // OpenScriptBtn
            // 
            this.OpenScriptBtn.Location = new System.Drawing.Point(213, 3);
            this.OpenScriptBtn.Name = "OpenScriptBtn";
            this.OpenScriptBtn.Size = new System.Drawing.Size(112, 23);
            this.OpenScriptBtn.TabIndex = 0;
            this.OpenScriptBtn.Text = "Open/Create Script";
            this.OpenScriptBtn.UseVisualStyleBackColor = true;
            this.OpenScriptBtn.Click += new System.EventHandler(this.OpenScriptBtn_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.CardImg, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.LoadImageBtn, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.SearchBox, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 254F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 216F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(171, 594);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // CardImg
            // 
            this.CardImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CardImg.ErrorImage = null;
            this.CardImg.Image = global::DevPro_CardManager.Properties.Resources.unknown;
            this.CardImg.Location = new System.Drawing.Point(3, 3);
            this.CardImg.Name = "CardImg";
            this.CardImg.Size = new System.Drawing.Size(165, 248);
            this.CardImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CardImg.TabIndex = 0;
            this.CardImg.TabStop = false;
            // 
            // LoadImageBtn
            // 
            this.LoadImageBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LoadImageBtn.Location = new System.Drawing.Point(48, 257);
            this.LoadImageBtn.Name = "LoadImageBtn";
            this.LoadImageBtn.Size = new System.Drawing.Size(75, 23);
            this.LoadImageBtn.TabIndex = 1;
            this.LoadImageBtn.Text = "Set Image";
            this.LoadImageBtn.UseVisualStyleBackColor = true;
            this.LoadImageBtn.Click += new System.EventHandler(this.LoadImageBtn_Click);
            // 
            // SearchBox
            // 
            this.SearchBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchBox.Location = new System.Drawing.Point(3, 286);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(165, 210);
            this.SearchBox.TabIndex = 2;
            this.SearchBox.TabStop = false;
            this.SearchBox.Text = "Search";
            // 
            // CDBEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 600);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CDBEditor";
            this.Text = "CDBEditor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CardImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CardID;
        private System.Windows.Forms.TextBox Alias;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CardFormats;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox SetCodeLst;
        private System.Windows.Forms.ComboBox OtherSetCodeLst;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox Level;
        private System.Windows.Forms.ComboBox Race;
        private System.Windows.Forms.ComboBox CardAttribute;
        private System.Windows.Forms.TextBox ATK;
        private System.Windows.Forms.MaskedTextBox DEF;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox CardName;
        private System.Windows.Forms.TextBox CardDescription;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button Clearbtn;
        private System.Windows.Forms.Button SaveCardbtn;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.ListBox EffectList;
        private System.Windows.Forms.TextBox EffectInput;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button DeleteEffectbtn;
        private System.Windows.Forms.Button MoveEffectUp;
        private System.Windows.Forms.Button MoveEffectDown;
        private System.Windows.Forms.Button AddEffectbtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox CardTypeList;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckedListBox CategoryList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox CardImg;
        private System.Windows.Forms.Button LoadImageBtn;
        private Components.SearchBox SearchBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button OpenScriptBtn;
        private System.Windows.Forms.CheckBox chkPre;
        private System.Windows.Forms.Label PreLbl;
    }
}