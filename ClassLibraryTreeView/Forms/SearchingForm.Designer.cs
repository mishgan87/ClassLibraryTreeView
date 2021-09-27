
using System.Windows.Forms;

namespace ClassLibraryTreeView.Forms
{
    partial class SearchingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchingForm));
            this.listViewResult = new System.Windows.Forms.ListView();
            this.textBox = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxMeasureUnits = new System.Windows.Forms.CheckBox();
            this.checkBoxClasses = new System.Windows.Forms.CheckBox();
            this.checkBoxId = new System.Windows.Forms.CheckBox();
            this.checkBoxName = new System.Windows.Forms.CheckBox();
            this.checkBoxAttributes = new System.Windows.Forms.CheckBox();
            this.checkBoxMeasureClasses = new System.Windows.Forms.CheckBox();
            this.checkBoxEnumerations = new System.Windows.Forms.CheckBox();
            this.checkBoxTaxonomies = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewResult
            // 
            this.listViewResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewResult.FullRowSelect = true;
            this.listViewResult.GridLines = true;
            this.listViewResult.HideSelection = false;
            this.listViewResult.Location = new System.Drawing.Point(246, 2);
            this.listViewResult.Margin = new System.Windows.Forms.Padding(2);
            this.listViewResult.Name = "listViewResult";
            this.listViewResult.Size = new System.Drawing.Size(975, 557);
            this.listViewResult.TabIndex = 1;
            this.listViewResult.UseCompatibleStateImageBehavior = false;
            this.listViewResult.View = System.Windows.Forms.View.Details;
            this.listViewResult.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseDoubleClick);
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox.Location = new System.Drawing.Point(0, 152);
            this.textBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(238, 23);
            this.textBox.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(0, 175);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSearch.MaximumSize = new System.Drawing.Size(0, 26);
            this.btnSearch.MinimumSize = new System.Drawing.Size(0, 26);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(238, 26);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listViewResult, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1223, 561);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // checkBoxMeasureUnits
            // 
            this.checkBoxMeasureUnits.AutoSize = true;
            this.checkBoxMeasureUnits.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxMeasureUnits.Location = new System.Drawing.Point(0, 114);
            this.checkBoxMeasureUnits.Name = "checkBoxMeasureUnits";
            this.checkBoxMeasureUnits.Size = new System.Drawing.Size(238, 19);
            this.checkBoxMeasureUnits.TabIndex = 17;
            this.checkBoxMeasureUnits.Text = "Search in Measure Units";
            this.checkBoxMeasureUnits.UseVisualStyleBackColor = true;
            // 
            // checkBoxClasses
            // 
            this.checkBoxClasses.AutoSize = true;
            this.checkBoxClasses.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxClasses.Location = new System.Drawing.Point(0, 38);
            this.checkBoxClasses.Name = "checkBoxClasses";
            this.checkBoxClasses.Size = new System.Drawing.Size(238, 19);
            this.checkBoxClasses.TabIndex = 18;
            this.checkBoxClasses.Text = "Search in Classes";
            this.checkBoxClasses.UseVisualStyleBackColor = true;
            // 
            // checkBoxId
            // 
            this.checkBoxId.AutoSize = true;
            this.checkBoxId.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxId.Location = new System.Drawing.Point(0, 0);
            this.checkBoxId.Name = "checkBoxId";
            this.checkBoxId.Size = new System.Drawing.Size(238, 19);
            this.checkBoxId.TabIndex = 19;
            this.checkBoxId.Text = "Search Id";
            this.checkBoxId.UseVisualStyleBackColor = true;
            // 
            // checkBoxName
            // 
            this.checkBoxName.AutoSize = true;
            this.checkBoxName.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxName.Location = new System.Drawing.Point(0, 19);
            this.checkBoxName.Name = "checkBoxName";
            this.checkBoxName.Size = new System.Drawing.Size(238, 19);
            this.checkBoxName.TabIndex = 20;
            this.checkBoxName.Text = "Search Name";
            this.checkBoxName.UseVisualStyleBackColor = true;
            // 
            // checkBoxAttributes
            // 
            this.checkBoxAttributes.AutoSize = true;
            this.checkBoxAttributes.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxAttributes.Location = new System.Drawing.Point(0, 57);
            this.checkBoxAttributes.Name = "checkBoxAttributes";
            this.checkBoxAttributes.Size = new System.Drawing.Size(238, 19);
            this.checkBoxAttributes.TabIndex = 21;
            this.checkBoxAttributes.Text = "Search in Attributes";
            this.checkBoxAttributes.UseVisualStyleBackColor = true;
            // 
            // checkBoxMeasureClasses
            // 
            this.checkBoxMeasureClasses.AutoSize = true;
            this.checkBoxMeasureClasses.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxMeasureClasses.Location = new System.Drawing.Point(0, 133);
            this.checkBoxMeasureClasses.Name = "checkBoxMeasureClasses";
            this.checkBoxMeasureClasses.Size = new System.Drawing.Size(238, 19);
            this.checkBoxMeasureClasses.TabIndex = 22;
            this.checkBoxMeasureClasses.Text = "Search in Measure Classes";
            this.checkBoxMeasureClasses.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnumerations
            // 
            this.checkBoxEnumerations.AutoSize = true;
            this.checkBoxEnumerations.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxEnumerations.Location = new System.Drawing.Point(0, 95);
            this.checkBoxEnumerations.Name = "checkBoxEnumerations";
            this.checkBoxEnumerations.Size = new System.Drawing.Size(238, 19);
            this.checkBoxEnumerations.TabIndex = 23;
            this.checkBoxEnumerations.Text = "Search in Enumerations";
            this.checkBoxEnumerations.UseVisualStyleBackColor = true;
            // 
            // checkBoxTaxonomies
            // 
            this.checkBoxTaxonomies.AutoSize = true;
            this.checkBoxTaxonomies.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxTaxonomies.Location = new System.Drawing.Point(0, 76);
            this.checkBoxTaxonomies.Name = "checkBoxTaxonomies";
            this.checkBoxTaxonomies.Size = new System.Drawing.Size(238, 19);
            this.checkBoxTaxonomies.TabIndex = 24;
            this.checkBoxTaxonomies.Text = "Search in Taxonomies";
            this.checkBoxTaxonomies.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.textBox);
            this.panel1.Controls.Add(this.checkBoxMeasureClasses);
            this.panel1.Controls.Add(this.checkBoxMeasureUnits);
            this.panel1.Controls.Add(this.checkBoxEnumerations);
            this.panel1.Controls.Add(this.checkBoxTaxonomies);
            this.panel1.Controls.Add(this.checkBoxAttributes);
            this.panel1.Controls.Add(this.checkBoxClasses);
            this.panel1.Controls.Add(this.checkBoxName);
            this.panel1.Controls.Add(this.checkBoxId);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 555);
            this.panel1.TabIndex = 17;
            // 
            // SearchingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 561);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(16, 600);
            this.Name = "SearchingForm";
            this.Text = "Search";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListView listViewResult;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TableLayoutPanel tableLayoutPanel1;
        private CheckBox checkBoxTaxonomies;
        private CheckBox checkBoxEnumerations;
        private CheckBox checkBoxMeasureClasses;
        private CheckBox checkBoxMeasureUnits;
        private CheckBox checkBoxAttributes;
        private CheckBox checkBoxClasses;
        private CheckBox checkBoxName;
        private CheckBox checkBoxId;
        private Panel panel1;
    }
}