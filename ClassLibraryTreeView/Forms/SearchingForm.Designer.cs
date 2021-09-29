
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
            this.searchPanel = new System.Windows.Forms.ToolStrip();
            this.btnSearch = new System.Windows.Forms.ToolStripSplitButton();
            this.searchId = new System.Windows.Forms.ToolStripMenuItem();
            this.searchName = new System.Windows.Forms.ToolStripMenuItem();
            this.matchCase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.searchClasses = new System.Windows.Forms.ToolStripMenuItem();
            this.searchAttributes = new System.Windows.Forms.ToolStripMenuItem();
            this.searchTaxonomies = new System.Windows.Forms.ToolStripMenuItem();
            this.searchEnumerations = new System.Windows.Forms.ToolStripMenuItem();
            this.searchMeasureUnits = new System.Windows.Forms.ToolStripMenuItem();
            this.searchMeasureClasses = new System.Windows.Forms.ToolStripMenuItem();
            this.searchString = new System.Windows.Forms.ToolStripTextBox();
            this.searchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewResult
            // 
            this.listViewResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listViewResult.FullRowSelect = true;
            this.listViewResult.GridLines = true;
            this.listViewResult.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewResult.HideSelection = false;
            this.listViewResult.Location = new System.Drawing.Point(0, 57);
            this.listViewResult.Margin = new System.Windows.Forms.Padding(2);
            this.listViewResult.Name = "listViewResult";
            this.listViewResult.Size = new System.Drawing.Size(851, 504);
            this.listViewResult.TabIndex = 1;
            this.listViewResult.UseCompatibleStateImageBehavior = false;
            this.listViewResult.View = System.Windows.Forms.View.Details;
            // 
            // searchPanel
            // 
            this.searchPanel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.searchPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSearch,
            this.searchString});
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(851, 55);
            this.searchPanel.TabIndex = 17;
            this.searchPanel.Text = "toolStrip1";
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchId,
            this.searchName,
            this.matchCase,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.searchClasses,
            this.searchAttributes,
            this.searchTaxonomies,
            this.searchEnumerations,
            this.searchMeasureUnits,
            this.searchMeasureClasses});
            this.btnSearch.Image = global::ClassLibraryTreeView.Properties.Resources.search;
            this.btnSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(64, 52);
            this.btnSearch.Text = "Search";
            this.btnSearch.ButtonClick += new System.EventHandler(this.BtnSearch_Click);
            // 
            // searchId
            // 
            this.searchId.Checked = true;
            this.searchId.CheckOnClick = true;
            this.searchId.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchId.Name = "searchId";
            this.searchId.Size = new System.Drawing.Size(228, 22);
            this.searchId.Text = "Search Id";
            // 
            // searchName
            // 
            this.searchName.Checked = true;
            this.searchName.CheckOnClick = true;
            this.searchName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchName.Name = "searchName";
            this.searchName.Size = new System.Drawing.Size(228, 22);
            this.searchName.Text = "Search Name";
            // 
            // matchCase
            // 
            this.matchCase.Checked = true;
            this.matchCase.CheckOnClick = true;
            this.matchCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.matchCase.Name = "matchCase";
            this.matchCase.Size = new System.Drawing.Size(228, 22);
            this.matchCase.Text = "Match Case";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(225, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(225, 6);
            // 
            // searchClasses
            // 
            this.searchClasses.Checked = true;
            this.searchClasses.CheckOnClick = true;
            this.searchClasses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchClasses.Name = "searchClasses";
            this.searchClasses.Size = new System.Drawing.Size(228, 22);
            this.searchClasses.Text = "Search Classes";
            // 
            // searchAttributes
            // 
            this.searchAttributes.Checked = true;
            this.searchAttributes.CheckOnClick = true;
            this.searchAttributes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchAttributes.Name = "searchAttributes";
            this.searchAttributes.Size = new System.Drawing.Size(228, 22);
            this.searchAttributes.Text = "Search Attributes";
            // 
            // searchTaxonomies
            // 
            this.searchTaxonomies.Checked = true;
            this.searchTaxonomies.CheckOnClick = true;
            this.searchTaxonomies.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchTaxonomies.Name = "searchTaxonomies";
            this.searchTaxonomies.Size = new System.Drawing.Size(228, 22);
            this.searchTaxonomies.Text = "Search Taxonomies";
            // 
            // searchEnumerations
            // 
            this.searchEnumerations.Checked = true;
            this.searchEnumerations.CheckOnClick = true;
            this.searchEnumerations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchEnumerations.Name = "searchEnumerations";
            this.searchEnumerations.Size = new System.Drawing.Size(228, 22);
            this.searchEnumerations.Text = "Search Enumerations";
            // 
            // searchMeasureUnits
            // 
            this.searchMeasureUnits.Checked = true;
            this.searchMeasureUnits.CheckOnClick = true;
            this.searchMeasureUnits.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchMeasureUnits.Name = "searchMeasureUnits";
            this.searchMeasureUnits.Size = new System.Drawing.Size(228, 22);
            this.searchMeasureUnits.Text = "Search Measure Units";
            // 
            // searchMeasureClasses
            // 
            this.searchMeasureClasses.Checked = true;
            this.searchMeasureClasses.CheckOnClick = true;
            this.searchMeasureClasses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchMeasureClasses.Name = "searchMeasureClasses";
            this.searchMeasureClasses.Size = new System.Drawing.Size(228, 22);
            this.searchMeasureClasses.Text = "Search Measure Classes";
            // 
            // searchString
            // 
            this.searchString.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold);
            this.searchString.Name = "searchString";
            this.searchString.Size = new System.Drawing.Size(500, 55);
            // 
            // SearchingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 561);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.listViewResult);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(16, 600);
            this.Name = "SearchingForm";
            this.Text = "Search";
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listViewResult;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ToolStrip searchPanel;
        private ToolStripSplitButton btnSearch;
        private ToolStripMenuItem searchId;
        private ToolStripMenuItem searchName;
        private ToolStripMenuItem matchCase;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem searchClasses;
        private ToolStripMenuItem searchAttributes;
        private ToolStripMenuItem searchTaxonomies;
        private ToolStripMenuItem searchEnumerations;
        private ToolStripMenuItem searchMeasureUnits;
        private ToolStripMenuItem searchMeasureClasses;
        private ToolStripTextBox searchString;
    }
}