
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.searchString = new System.Windows.Forms.TextBox();
            this.searchWhatBox = new System.Windows.Forms.GroupBox();
            this.matchCase = new System.Windows.Forms.CheckBox();
            this.searchName = new System.Windows.Forms.CheckBox();
            this.searchId = new System.Windows.Forms.CheckBox();
            this.searchInBox = new System.Windows.Forms.GroupBox();
            this.searchMeasureClasses = new System.Windows.Forms.CheckBox();
            this.searchMeasureUnits = new System.Windows.Forms.CheckBox();
            this.searchEnumerations = new System.Windows.Forms.CheckBox();
            this.searchTaxonomies = new System.Windows.Forms.CheckBox();
            this.searchAttributes = new System.Windows.Forms.CheckBox();
            this.searchClasses = new System.Windows.Forms.CheckBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnSearch = new System.Windows.Forms.Button();
            this.searchWhatBox.SuspendLayout();
            this.searchInBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewResult
            // 
            this.listViewResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewResult.FullRowSelect = true;
            this.listViewResult.GridLines = true;
            this.listViewResult.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewResult.HideSelection = false;
            this.listViewResult.Location = new System.Drawing.Point(0, 0);
            this.listViewResult.Margin = new System.Windows.Forms.Padding(2);
            this.listViewResult.Name = "listViewResult";
            this.listViewResult.Size = new System.Drawing.Size(685, 561);
            this.listViewResult.TabIndex = 1;
            this.listViewResult.UseCompatibleStateImageBehavior = false;
            this.listViewResult.View = System.Windows.Forms.View.Details;
            // 
            // searchString
            // 
            this.searchString.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchString.Location = new System.Drawing.Point(0, 234);
            this.searchString.Name = "searchString";
            this.searchString.Size = new System.Drawing.Size(246, 23);
            this.searchString.TabIndex = 19;
            // 
            // searchWhatBox
            // 
            this.searchWhatBox.Controls.Add(this.matchCase);
            this.searchWhatBox.Controls.Add(this.searchName);
            this.searchWhatBox.Controls.Add(this.searchId);
            this.searchWhatBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchWhatBox.Location = new System.Drawing.Point(0, 0);
            this.searchWhatBox.Name = "searchWhatBox";
            this.searchWhatBox.Size = new System.Drawing.Size(246, 90);
            this.searchWhatBox.TabIndex = 20;
            this.searchWhatBox.TabStop = false;
            this.searchWhatBox.Text = "Search what";
            // 
            // matchCase
            // 
            this.matchCase.Checked = true;
            this.matchCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.matchCase.Dock = System.Windows.Forms.DockStyle.Top;
            this.matchCase.Location = new System.Drawing.Point(3, 59);
            this.matchCase.Name = "matchCase";
            this.matchCase.Size = new System.Drawing.Size(240, 20);
            this.matchCase.TabIndex = 2;
            this.matchCase.Text = "Match Case";
            this.matchCase.UseVisualStyleBackColor = true;
            // 
            // searchName
            // 
            this.searchName.Checked = true;
            this.searchName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchName.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchName.Location = new System.Drawing.Point(3, 39);
            this.searchName.Name = "searchName";
            this.searchName.Size = new System.Drawing.Size(240, 20);
            this.searchName.TabIndex = 1;
            this.searchName.Text = "Search Name";
            this.searchName.UseVisualStyleBackColor = true;
            // 
            // searchId
            // 
            this.searchId.Checked = true;
            this.searchId.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchId.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchId.Location = new System.Drawing.Point(3, 19);
            this.searchId.Name = "searchId";
            this.searchId.Size = new System.Drawing.Size(240, 20);
            this.searchId.TabIndex = 0;
            this.searchId.Text = "Search Id";
            this.searchId.UseVisualStyleBackColor = true;
            // 
            // searchInBox
            // 
            this.searchInBox.Controls.Add(this.searchMeasureClasses);
            this.searchInBox.Controls.Add(this.searchMeasureUnits);
            this.searchInBox.Controls.Add(this.searchEnumerations);
            this.searchInBox.Controls.Add(this.searchTaxonomies);
            this.searchInBox.Controls.Add(this.searchAttributes);
            this.searchInBox.Controls.Add(this.searchClasses);
            this.searchInBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchInBox.Location = new System.Drawing.Point(0, 90);
            this.searchInBox.Name = "searchInBox";
            this.searchInBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.searchInBox.Size = new System.Drawing.Size(246, 144);
            this.searchInBox.TabIndex = 21;
            this.searchInBox.TabStop = false;
            this.searchInBox.Text = "Seach In";
            // 
            // searchMeasureClasses
            // 
            this.searchMeasureClasses.Checked = true;
            this.searchMeasureClasses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchMeasureClasses.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchMeasureClasses.Location = new System.Drawing.Point(3, 115);
            this.searchMeasureClasses.Name = "searchMeasureClasses";
            this.searchMeasureClasses.Size = new System.Drawing.Size(240, 19);
            this.searchMeasureClasses.TabIndex = 5;
            this.searchMeasureClasses.Text = "Search Measure Classes";
            this.searchMeasureClasses.UseVisualStyleBackColor = true;
            // 
            // searchMeasureUnits
            // 
            this.searchMeasureUnits.Checked = true;
            this.searchMeasureUnits.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchMeasureUnits.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchMeasureUnits.Location = new System.Drawing.Point(3, 96);
            this.searchMeasureUnits.Name = "searchMeasureUnits";
            this.searchMeasureUnits.Size = new System.Drawing.Size(240, 19);
            this.searchMeasureUnits.TabIndex = 4;
            this.searchMeasureUnits.Text = "Search Measure Units";
            this.searchMeasureUnits.UseVisualStyleBackColor = true;
            // 
            // searchEnumerations
            // 
            this.searchEnumerations.Checked = true;
            this.searchEnumerations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchEnumerations.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchEnumerations.Location = new System.Drawing.Point(3, 77);
            this.searchEnumerations.Name = "searchEnumerations";
            this.searchEnumerations.Size = new System.Drawing.Size(240, 19);
            this.searchEnumerations.TabIndex = 3;
            this.searchEnumerations.Text = "Search Enumerations";
            this.searchEnumerations.UseVisualStyleBackColor = true;
            // 
            // searchTaxonomies
            // 
            this.searchTaxonomies.Checked = true;
            this.searchTaxonomies.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchTaxonomies.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchTaxonomies.Location = new System.Drawing.Point(3, 58);
            this.searchTaxonomies.Name = "searchTaxonomies";
            this.searchTaxonomies.Size = new System.Drawing.Size(240, 19);
            this.searchTaxonomies.TabIndex = 2;
            this.searchTaxonomies.Text = "Search Taxonomies";
            this.searchTaxonomies.UseVisualStyleBackColor = true;
            // 
            // searchAttributes
            // 
            this.searchAttributes.Checked = true;
            this.searchAttributes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchAttributes.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchAttributes.Location = new System.Drawing.Point(3, 39);
            this.searchAttributes.Name = "searchAttributes";
            this.searchAttributes.Size = new System.Drawing.Size(240, 19);
            this.searchAttributes.TabIndex = 1;
            this.searchAttributes.Text = "Search Attributes";
            this.searchAttributes.UseVisualStyleBackColor = true;
            // 
            // searchClasses
            // 
            this.searchClasses.Checked = true;
            this.searchClasses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchClasses.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchClasses.Location = new System.Drawing.Point(3, 19);
            this.searchClasses.Name = "searchClasses";
            this.searchClasses.Size = new System.Drawing.Size(240, 20);
            this.searchClasses.TabIndex = 0;
            this.searchClasses.Text = "Search Classes";
            this.searchClasses.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnSearch);
            this.splitContainer2.Panel1.Controls.Add(this.searchString);
            this.splitContainer2.Panel1.Controls.Add(this.searchInBox);
            this.splitContainer2.Panel1.Controls.Add(this.searchWhatBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listViewResult);
            this.splitContainer2.Size = new System.Drawing.Size(935, 561);
            this.splitContainer2.SplitterDistance = 246;
            this.splitContainer2.TabIndex = 22;
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSearch.Location = new System.Drawing.Point(0, 257);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(246, 47);
            this.btnSearch.TabIndex = 23;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // SearchingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 561);
            this.Controls.Add(this.splitContainer2);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(16, 600);
            this.Name = "SearchingForm";
            this.Text = "Search";
            this.searchWhatBox.ResumeLayout(false);
            this.searchInBox.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView listViewResult;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TextBox searchString;
        private GroupBox searchWhatBox;
        private CheckBox matchCase;
        private CheckBox searchName;
        private CheckBox searchId;
        private GroupBox searchInBox;
        private CheckBox searchMeasureClasses;
        private CheckBox searchMeasureUnits;
        private CheckBox searchEnumerations;
        private CheckBox searchTaxonomies;
        private CheckBox searchAttributes;
        private CheckBox searchClasses;
        private SplitContainer splitContainer2;
        private Button btnSearch;
    }
}