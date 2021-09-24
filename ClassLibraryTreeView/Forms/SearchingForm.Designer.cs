
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
            this.radioButtonSearchName = new System.Windows.Forms.RadioButton();
            this.radioButtonSearchId = new System.Windows.Forms.RadioButton();
            this.textBox = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.radioButtonMeasureUnits = new System.Windows.Forms.RadioButton();
            this.radioButtonClasses = new System.Windows.Forms.RadioButton();
            this.radioButtonAttributes = new System.Windows.Forms.RadioButton();
            this.radioButtonEnumerations = new System.Windows.Forms.RadioButton();
            this.radioButtonTaxonomies = new System.Windows.Forms.RadioButton();
            this.radioButtonMeasureClasses = new System.Windows.Forms.RadioButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBoxSource = new System.Windows.Forms.GroupBox();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxSource.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewResult
            // 
            this.listViewResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listViewResult.FullRowSelect = true;
            this.listViewResult.GridLines = true;
            this.listViewResult.HideSelection = false;
            this.listViewResult.Location = new System.Drawing.Point(0, 154);
            this.listViewResult.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listViewResult.Name = "listViewResult";
            this.listViewResult.Size = new System.Drawing.Size(668, 407);
            this.listViewResult.TabIndex = 1;
            this.listViewResult.UseCompatibleStateImageBehavior = false;
            this.listViewResult.View = System.Windows.Forms.View.Details;
            this.listViewResult.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseDoubleClick);
            // 
            // radioButtonSearchName
            // 
            this.radioButtonSearchName.AutoSize = true;
            this.radioButtonSearchName.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonSearchName.Location = new System.Drawing.Point(3, 38);
            this.radioButtonSearchName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButtonSearchName.Name = "radioButtonSearchName";
            this.radioButtonSearchName.Size = new System.Drawing.Size(224, 19);
            this.radioButtonSearchName.TabIndex = 9;
            this.radioButtonSearchName.Text = "Name";
            this.radioButtonSearchName.UseVisualStyleBackColor = true;
            // 
            // radioButtonSearchId
            // 
            this.radioButtonSearchId.AutoSize = true;
            this.radioButtonSearchId.Checked = true;
            this.radioButtonSearchId.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonSearchId.Location = new System.Drawing.Point(3, 19);
            this.radioButtonSearchId.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButtonSearchId.Name = "radioButtonSearchId";
            this.radioButtonSearchId.Size = new System.Drawing.Size(224, 19);
            this.radioButtonSearchId.TabIndex = 8;
            this.radioButtonSearchId.TabStop = true;
            this.radioButtonSearchId.Text = "ID";
            this.radioButtonSearchId.UseVisualStyleBackColor = true;
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox.Location = new System.Drawing.Point(3, 98);
            this.textBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(224, 23);
            this.textBox.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(3, 121);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSearch.MaximumSize = new System.Drawing.Size(0, 26);
            this.btnSearch.MinimumSize = new System.Drawing.Size(0, 26);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(224, 26);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // radioButtonMeasureUnits
            // 
            this.radioButtonMeasureUnits.AutoSize = true;
            this.radioButtonMeasureUnits.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonMeasureUnits.Location = new System.Drawing.Point(3, 57);
            this.radioButtonMeasureUnits.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButtonMeasureUnits.Name = "radioButtonMeasureUnits";
            this.radioButtonMeasureUnits.Size = new System.Drawing.Size(164, 19);
            this.radioButtonMeasureUnits.TabIndex = 7;
            this.radioButtonMeasureUnits.Text = "Measure Units";
            this.radioButtonMeasureUnits.UseVisualStyleBackColor = true;
            // 
            // radioButtonClasses
            // 
            this.radioButtonClasses.AutoSize = true;
            this.radioButtonClasses.Checked = true;
            this.radioButtonClasses.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonClasses.Location = new System.Drawing.Point(3, 19);
            this.radioButtonClasses.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButtonClasses.Name = "radioButtonClasses";
            this.radioButtonClasses.Size = new System.Drawing.Size(164, 19);
            this.radioButtonClasses.TabIndex = 6;
            this.radioButtonClasses.TabStop = true;
            this.radioButtonClasses.Text = "Classes";
            this.radioButtonClasses.UseVisualStyleBackColor = true;
            // 
            // radioButtonAttributes
            // 
            this.radioButtonAttributes.AutoSize = true;
            this.radioButtonAttributes.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonAttributes.Location = new System.Drawing.Point(3, 38);
            this.radioButtonAttributes.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButtonAttributes.Name = "radioButtonAttributes";
            this.radioButtonAttributes.Size = new System.Drawing.Size(164, 19);
            this.radioButtonAttributes.TabIndex = 5;
            this.radioButtonAttributes.Text = "Attributes";
            this.radioButtonAttributes.UseVisualStyleBackColor = true;
            // 
            // radioButtonEnumerations
            // 
            this.radioButtonEnumerations.AutoSize = true;
            this.radioButtonEnumerations.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonEnumerations.Location = new System.Drawing.Point(3, 95);
            this.radioButtonEnumerations.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButtonEnumerations.Name = "radioButtonEnumerations";
            this.radioButtonEnumerations.Size = new System.Drawing.Size(164, 19);
            this.radioButtonEnumerations.TabIndex = 4;
            this.radioButtonEnumerations.Text = "Enumerations";
            this.radioButtonEnumerations.UseVisualStyleBackColor = true;
            // 
            // radioButtonTaxonomies
            // 
            this.radioButtonTaxonomies.AutoSize = true;
            this.radioButtonTaxonomies.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonTaxonomies.Location = new System.Drawing.Point(3, 114);
            this.radioButtonTaxonomies.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButtonTaxonomies.Name = "radioButtonTaxonomies";
            this.radioButtonTaxonomies.Size = new System.Drawing.Size(164, 19);
            this.radioButtonTaxonomies.TabIndex = 1;
            this.radioButtonTaxonomies.Text = "Taxonomies";
            this.radioButtonTaxonomies.UseVisualStyleBackColor = true;
            // 
            // radioButtonMeasureClasses
            // 
            this.radioButtonMeasureClasses.AutoSize = true;
            this.radioButtonMeasureClasses.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioButtonMeasureClasses.Location = new System.Drawing.Point(3, 76);
            this.radioButtonMeasureClasses.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.radioButtonMeasureClasses.Name = "radioButtonMeasureClasses";
            this.radioButtonMeasureClasses.Size = new System.Drawing.Size(164, 19);
            this.radioButtonMeasureClasses.TabIndex = 1;
            this.radioButtonMeasureClasses.Text = "Measure Classes";
            this.radioButtonMeasureClasses.UseVisualStyleBackColor = true;
            // 
            // groupBoxSource
            // 
            this.groupBoxSource.Controls.Add(this.radioButtonTaxonomies);
            this.groupBoxSource.Controls.Add(this.radioButtonEnumerations);
            this.groupBoxSource.Controls.Add(this.radioButtonMeasureClasses);
            this.groupBoxSource.Controls.Add(this.radioButtonMeasureUnits);
            this.groupBoxSource.Controls.Add(this.radioButtonAttributes);
            this.groupBoxSource.Controls.Add(this.radioButtonClasses);
            this.groupBoxSource.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBoxSource.Location = new System.Drawing.Point(230, 0);
            this.groupBoxSource.MaximumSize = new System.Drawing.Size(170, 0);
            this.groupBoxSource.MinimumSize = new System.Drawing.Size(170, 0);
            this.groupBoxSource.Name = "groupBoxSource";
            this.groupBoxSource.Size = new System.Drawing.Size(170, 150);
            this.groupBoxSource.TabIndex = 14;
            this.groupBoxSource.TabStop = false;
            this.groupBoxSource.Text = "Search in ";
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.textBox);
            this.groupBox.Controls.Add(this.btnSearch);
            this.groupBox.Controls.Add(this.radioButtonSearchName);
            this.groupBox.Controls.Add(this.radioButtonSearchId);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.MaximumSize = new System.Drawing.Size(230, 0);
            this.groupBox.MinimumSize = new System.Drawing.Size(230, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(230, 150);
            this.groupBox.TabIndex = 15;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Search text";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBoxSource);
            this.panel1.Controls.Add(this.groupBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MaximumSize = new System.Drawing.Size(0, 150);
            this.panel1.MinimumSize = new System.Drawing.Size(0, 150);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(668, 150);
            this.panel1.TabIndex = 16;
            // 
            // SearchingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listViewResult);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(0, 600);
            this.Name = "SearchingForm";
            this.Text = "Search";
            this.groupBoxSource.ResumeLayout(false);
            this.groupBoxSource.PerformLayout();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.RadioButton radioButtonMeasureUnits;
        private System.Windows.Forms.RadioButton radioButtonClasses;
        private System.Windows.Forms.RadioButton radioButtonAttributes;
        private System.Windows.Forms.RadioButton radioButtonEnumerations;
        private System.Windows.Forms.RadioButton radioButtonTaxonomies;
        private System.Windows.Forms.RadioButton radioButtonMeasureClasses;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListView listViewResult;
        private RadioButton radioButtonSearchName;
        private RadioButton radioButtonSearchId;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private GroupBox groupBoxSource;
        private GroupBox groupBox;
        private Panel panel1;
    }
}