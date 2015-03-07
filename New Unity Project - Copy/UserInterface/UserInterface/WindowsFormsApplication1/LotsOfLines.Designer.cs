namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collocatedPairedCoordinatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shiftedPairedCoordinatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inlineDimensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.correlatedDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radialPairedCoordinatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeColorSchemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schemeAeroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schemeLustorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schemeIndustrialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Value0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valuen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.selectToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1011, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDataToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // loadDataToolStripMenuItem
            // 
            this.loadDataToolStripMenuItem.Name = "loadDataToolStripMenuItem";
            this.loadDataToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.loadDataToolStripMenuItem.Text = "Load Data";
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectLineToolStripMenuItem,
            this.zoomToolStripMenuItem});
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.selectToolStripMenuItem.Text = "Tools";
            // 
            // selectLineToolStripMenuItem
            // 
            this.selectLineToolStripMenuItem.Name = "selectLineToolStripMenuItem";
            this.selectLineToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.selectLineToolStripMenuItem.Text = "Find trend";
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.zoomToolStripMenuItem.Text = "Zoom";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.collocatedPairedCoordinatesToolStripMenuItem,
            this.shiftedPairedCoordinatesToolStripMenuItem,
            this.inlineDimensionsToolStripMenuItem,
            this.correlatedDataToolStripMenuItem,
            this.radialPairedCoordinatesToolStripMenuItem,
            this.changeColorSchemeToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.settingsToolStripMenuItem.Text = "View";
            // 
            // collocatedPairedCoordinatesToolStripMenuItem
            // 
            this.collocatedPairedCoordinatesToolStripMenuItem.Name = "collocatedPairedCoordinatesToolStripMenuItem";
            this.collocatedPairedCoordinatesToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.collocatedPairedCoordinatesToolStripMenuItem.Text = "Parallel Coordinates";
            // 
            // shiftedPairedCoordinatesToolStripMenuItem
            // 
            this.shiftedPairedCoordinatesToolStripMenuItem.Name = "shiftedPairedCoordinatesToolStripMenuItem";
            this.shiftedPairedCoordinatesToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.shiftedPairedCoordinatesToolStripMenuItem.Text = "Shifted Paired Coordinates";
            // 
            // inlineDimensionsToolStripMenuItem
            // 
            this.inlineDimensionsToolStripMenuItem.Name = "inlineDimensionsToolStripMenuItem";
            this.inlineDimensionsToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.inlineDimensionsToolStripMenuItem.Text = "Collocated Paired Coordinates";
            // 
            // correlatedDataToolStripMenuItem
            // 
            this.correlatedDataToolStripMenuItem.Name = "correlatedDataToolStripMenuItem";
            this.correlatedDataToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.correlatedDataToolStripMenuItem.Text = "Horizontal Coordinates";
            this.correlatedDataToolStripMenuItem.Click += new System.EventHandler(this.correlatedDataToolStripMenuItem_Click);
            // 
            // radialPairedCoordinatesToolStripMenuItem
            // 
            this.radialPairedCoordinatesToolStripMenuItem.Name = "radialPairedCoordinatesToolStripMenuItem";
            this.radialPairedCoordinatesToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.radialPairedCoordinatesToolStripMenuItem.Text = "Radial Paired Coordinates";
            // 
            // changeColorSchemeToolStripMenuItem
            // 
            this.changeColorSchemeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.schemeAeroToolStripMenuItem,
            this.schemeLustorToolStripMenuItem,
            this.schemeIndustrialToolStripMenuItem});
            this.changeColorSchemeToolStripMenuItem.Name = "changeColorSchemeToolStripMenuItem";
            this.changeColorSchemeToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.changeColorSchemeToolStripMenuItem.Text = "Change Color Scheme";
            // 
            // schemeAeroToolStripMenuItem
            // 
            this.schemeAeroToolStripMenuItem.Name = "schemeAeroToolStripMenuItem";
            this.schemeAeroToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.schemeAeroToolStripMenuItem.Text = "Scheme Aero";
            // 
            // schemeLustorToolStripMenuItem
            // 
            this.schemeLustorToolStripMenuItem.Name = "schemeLustorToolStripMenuItem";
            this.schemeLustorToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.schemeLustorToolStripMenuItem.Text = "Scheme Lustor";
            // 
            // schemeIndustrialToolStripMenuItem
            // 
            this.schemeIndustrialToolStripMenuItem.Name = "schemeIndustrialToolStripMenuItem";
            this.schemeIndustrialToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.schemeIndustrialToolStripMenuItem.Text = "Scheme industrial";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Value0,
            this.Value1,
            this.Value2,
            this.Valuen});
            this.dataGridView1.Location = new System.Drawing.Point(12, 617);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(990, 48);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Value0
            // 
            this.Value0.HeaderText = "Value 0";
            this.Value0.Name = "Value0";
            // 
            // Value1
            // 
            this.Value1.HeaderText = "Value 1";
            this.Value1.Name = "Value1";
            // 
            // Value2
            // 
            this.Value2.HeaderText = "Value 2";
            this.Value2.Name = "Value2";
            // 
            // Valuen
            // 
            this.Valuen.HeaderText = "...Value N";
            this.Valuen.Name = "Valuen";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(990, 586);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.para_coord;
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(982, 560);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Parallel Coordinates";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(315, 478);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(410, 98);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parallel Coordinates";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.colloc_pair;
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(982, 560);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Collocated Piared Coordinates";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.radial_pair;
            this.tabPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(982, 560);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Radial Paired Coordinates";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.shifted_pair;
            this.tabPage4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(982, 560);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Shifted Paired Coordinates";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.inline;
            this.tabPage5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(982, 560);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "In-line Dimensions";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(269, 486);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(480, 39);
            this.label2.TabIndex = 0;
            this.label2.Text = "Collocated Paired Coordinates";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(285, 505);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(416, 39);
            this.label3.TabIndex = 0;
            this.label3.Text = "Radial Paired Coordinates";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(276, 502);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(420, 39);
            this.label4.TabIndex = 0;
            this.label4.Text = "Shifted paired Coordinates";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(342, 505);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(299, 39);
            this.label5.TabIndex = 0;
            this.label5.Text = "In-line Dimensions";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 675);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Lots Of Lines Visualizer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collocatedPairedCoordinatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shiftedPairedCoordinatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inlineDimensionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem correlatedDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem radialPairedCoordinatesToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valuen;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeColorSchemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schemeAeroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schemeLustorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schemeIndustrialToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

