namespace DevPro_CardManager.Components
{
    partial class FormatConverter
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.convertList = new System.Windows.Forms.ListBox();
            this.SearchBox = new DevPro_CardManager.Components.SearchBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.rbTCG = new System.Windows.Forms.RadioButton();
            this.rbTCGOCG = new System.Windows.Forms.RadioButton();
            this.rbOCG = new System.Windows.Forms.RadioButton();
            this.addBtn = new System.Windows.Forms.Button();
            this.convertBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.SearchBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.convertBtn, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.32024F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.679764F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(733, 509);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.convertList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(369, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 469);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Convert List";
            // 
            // convertList
            // 
            this.convertList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.convertList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.convertList.FormattingEnabled = true;
            this.convertList.Location = new System.Drawing.Point(3, 16);
            this.convertList.Name = "convertList";
            this.convertList.Size = new System.Drawing.Size(355, 450);
            this.convertList.TabIndex = 0;
            // 
            // SearchBox
            // 
            this.SearchBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchBox.Location = new System.Drawing.Point(3, 3);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(360, 469);
            this.SearchBox.TabIndex = 0;
            this.SearchBox.TabStop = false;
            this.SearchBox.Text = "Search";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.22523F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.77477F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel2.Controls.Add(this.rbTCG, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.rbTCGOCG, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.rbOCG, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.addBtn, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 478);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(360, 28);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // rbTCG
            // 
            this.rbTCG.AutoSize = true;
            this.rbTCG.Location = new System.Drawing.Point(3, 3);
            this.rbTCG.Name = "rbTCG";
            this.rbTCG.Size = new System.Drawing.Size(47, 17);
            this.rbTCG.TabIndex = 0;
            this.rbTCG.TabStop = true;
            this.rbTCG.Text = "TCG";
            this.rbTCG.UseVisualStyleBackColor = true;
            // 
            // rbTCGOCG
            // 
            this.rbTCGOCG.AutoSize = true;
            this.rbTCGOCG.Location = new System.Drawing.Point(112, 3);
            this.rbTCGOCG.Name = "rbTCGOCG";
            this.rbTCGOCG.Size = new System.Drawing.Size(75, 17);
            this.rbTCGOCG.TabIndex = 2;
            this.rbTCGOCG.TabStop = true;
            this.rbTCGOCG.Text = "TCG/OCG";
            this.rbTCGOCG.UseVisualStyleBackColor = true;
            // 
            // rbOCG
            // 
            this.rbOCG.AutoSize = true;
            this.rbOCG.Location = new System.Drawing.Point(56, 3);
            this.rbOCG.Name = "rbOCG";
            this.rbOCG.Size = new System.Drawing.Size(48, 17);
            this.rbOCG.TabIndex = 1;
            this.rbOCG.TabStop = true;
            this.rbOCG.Text = "OCG";
            this.rbOCG.UseVisualStyleBackColor = true;
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(277, 3);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 22);
            this.addBtn.TabIndex = 2;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // convertBtn
            // 
            this.convertBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.convertBtn.Location = new System.Drawing.Point(655, 478);
            this.convertBtn.Name = "convertBtn";
            this.convertBtn.Size = new System.Drawing.Size(75, 28);
            this.convertBtn.TabIndex = 4;
            this.convertBtn.Text = "Convert";
            this.convertBtn.UseVisualStyleBackColor = true;
            this.convertBtn.Click += new System.EventHandler(this.convertBtn_Click);
            // 
            // FormatConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormatConverter";
            this.Size = new System.Drawing.Size(733, 509);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private SearchBox SearchBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton rbTCG;
        private System.Windows.Forms.RadioButton rbTCGOCG;
        private System.Windows.Forms.RadioButton rbOCG;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox convertList;
        private System.Windows.Forms.Button convertBtn;


    }
}
