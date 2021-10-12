using ClassLibraryTreeView.Classes;
using ClassLibraryTreeView.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibraryTreeView.Controls
{
    class SearchPanel : Panel
    {
        private ConceptualModel model = null;
        private System.Windows.Forms.ListView listViewResult;
        // private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private GroupBox searchWhatBox;
        private CheckBox matchCase;
        private CheckBox searchName;
        private CheckBox searchId;
        private GroupBox searchInBox;
        private CheckBox selectAll;
        private CheckBox searchMeasureClasses;
        private CheckBox searchMeasureUnits;
        private CheckBox searchEnumerations;
        private CheckBox searchTaxonomies;
        private CheckBox searchAttributes;
        private CheckBox searchClasses;
        private SplitContainer splitContainer;
        private Button btnSearch;
        private TextBox searchString;
        public SearchPanel(ConceptualModel modelReference) : base()
        {
            model = modelReference;

            this.listViewResult = new System.Windows.Forms.ListView();
            // this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.searchWhatBox = new System.Windows.Forms.GroupBox();
            this.searchString = new System.Windows.Forms.TextBox();
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
            this.selectAll = new System.Windows.Forms.CheckBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.btnSearch = new System.Windows.Forms.Button();
            this.searchWhatBox.SuspendLayout();
            this.searchInBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
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
            this.listViewResult.Size = new System.Drawing.Size(682, 561);
            this.listViewResult.TabIndex = 1;
            this.listViewResult.UseCompatibleStateImageBehavior = false;
            this.listViewResult.View = System.Windows.Forms.View.Details;
            this.listViewResult.Columns.Add("ID", 300, HorizontalAlignment.Left);
            this.listViewResult.Columns.Add("Name", 300, HorizontalAlignment.Left);
            this.listViewResult.Columns.Add("Type", 300, HorizontalAlignment.Left);
            // 
            // searchWhatBox
            // 
            this.searchWhatBox.Controls.Add(this.btnSearch);
            this.searchWhatBox.Controls.Add(this.searchString);
            this.searchWhatBox.Controls.Add(this.matchCase);
            this.searchWhatBox.Controls.Add(this.searchName);
            this.searchWhatBox.Controls.Add(this.searchId);
            this.searchWhatBox.Dock = System.Windows.Forms.DockStyle.Left; //.Top;
            this.searchWhatBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.searchWhatBox.Location = new System.Drawing.Point(0, 0);
            this.searchWhatBox.Name = "searchWhatBox";
            this.searchWhatBox.Size = new System.Drawing.Size(426, 113);
            this.searchWhatBox.TabIndex = 20;
            this.searchWhatBox.TabStop = false;
            this.searchWhatBox.Text = "Search what";
            // 
            // searchString
            // 
            this.searchString.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchString.Location = new System.Drawing.Point(3, 82);
            this.searchString.Name = "searchString";
            this.searchString.Size = new System.Drawing.Size(420, 26);
            this.searchString.TabIndex = 20;
            // 
            // matchCase
            // 
            this.matchCase.Checked = true;
            this.matchCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.matchCase.Dock = System.Windows.Forms.DockStyle.Top;
            this.matchCase.Location = new System.Drawing.Point(3, 62);
            this.matchCase.Name = "matchCase";
            this.matchCase.Size = new System.Drawing.Size(420, 20);
            this.matchCase.TabIndex = 2;
            this.matchCase.Text = "Match Case";
            this.matchCase.UseVisualStyleBackColor = true;
            // 
            // searchName
            // 
            this.searchName.Checked = true;
            this.searchName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchName.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchName.Location = new System.Drawing.Point(3, 42);
            this.searchName.Name = "searchName";
            this.searchName.Size = new System.Drawing.Size(420, 20);
            this.searchName.TabIndex = 1;
            this.searchName.Text = "Search Name";
            this.searchName.UseVisualStyleBackColor = true;
            // 
            // searchId
            // 
            this.searchId.Checked = true;
            this.searchId.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchId.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchId.Location = new System.Drawing.Point(3, 22);
            this.searchId.Name = "searchId";
            this.searchId.Size = new System.Drawing.Size(420, 20);
            this.searchId.TabIndex = 0;
            this.searchId.Text = "Search Id";
            this.searchId.UseVisualStyleBackColor = true;
            // 
            // searchInBox
            // 
            this.searchInBox.Controls.Add(this.selectAll);
            this.searchInBox.Controls.Add(this.searchMeasureClasses);
            this.searchInBox.Controls.Add(this.searchMeasureUnits);
            this.searchInBox.Controls.Add(this.searchEnumerations);
            this.searchInBox.Controls.Add(this.searchTaxonomies);
            this.searchInBox.Controls.Add(this.searchAttributes);
            this.searchInBox.Controls.Add(this.searchClasses);
            this.searchInBox.Dock = System.Windows.Forms.DockStyle.Left; //.Top;
            this.searchInBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.searchInBox.Location = new System.Drawing.Point(0, 113);
            this.searchInBox.Name = "searchInBox";
            this.searchInBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.searchInBox.Size = new System.Drawing.Size(426, 159);
            this.searchInBox.TabIndex = 21;
            this.searchInBox.TabStop = false;
            this.searchInBox.Text = "Seach In";
            // 
            // selectAll
            //
            this.selectAll.Checked = true;
            this.selectAll.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.selectAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectAll.Location = new System.Drawing.Point(3, 118);
            this.selectAll.Name = "selectAll";
            this.selectAll.Size = new System.Drawing.Size(420, 19);
            this.selectAll.TabIndex = 5;
            this.selectAll.Text = "Select All";
            this.selectAll.UseVisualStyleBackColor = true;
            this.selectAll.CheckedChanged += new EventHandler(this.SelectAllSearchIn);
            // 
            // searchMeasureClasses
            // 
            this.searchMeasureClasses.Checked = true;
            this.searchMeasureClasses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchMeasureClasses.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchMeasureClasses.Location = new System.Drawing.Point(3, 118);
            this.searchMeasureClasses.Name = "searchMeasureClasses";
            this.searchMeasureClasses.Size = new System.Drawing.Size(420, 19);
            this.searchMeasureClasses.TabIndex = 5;
            this.searchMeasureClasses.Text = "Search Measure Classes";
            this.searchMeasureClasses.UseVisualStyleBackColor = true;
            // 
            // searchMeasureUnits
            // 
            this.searchMeasureUnits.Checked = true;
            this.searchMeasureUnits.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchMeasureUnits.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchMeasureUnits.Location = new System.Drawing.Point(3, 99);
            this.searchMeasureUnits.Name = "searchMeasureUnits";
            this.searchMeasureUnits.Size = new System.Drawing.Size(420, 19);
            this.searchMeasureUnits.TabIndex = 4;
            this.searchMeasureUnits.Text = "Search Measure Units";
            this.searchMeasureUnits.UseVisualStyleBackColor = true;
            // 
            // searchEnumerations
            // 
            this.searchEnumerations.Checked = true;
            this.searchEnumerations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchEnumerations.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchEnumerations.Location = new System.Drawing.Point(3, 80);
            this.searchEnumerations.Name = "searchEnumerations";
            this.searchEnumerations.Size = new System.Drawing.Size(420, 19);
            this.searchEnumerations.TabIndex = 3;
            this.searchEnumerations.Text = "Search Enumerations";
            this.searchEnumerations.UseVisualStyleBackColor = true;
            // 
            // searchTaxonomies
            // 
            this.searchTaxonomies.Checked = true;
            this.searchTaxonomies.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchTaxonomies.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchTaxonomies.Location = new System.Drawing.Point(3, 61);
            this.searchTaxonomies.Name = "searchTaxonomies";
            this.searchTaxonomies.Size = new System.Drawing.Size(420, 19);
            this.searchTaxonomies.TabIndex = 2;
            this.searchTaxonomies.Text = "Search Taxonomies";
            this.searchTaxonomies.UseVisualStyleBackColor = true;
            // 
            // searchAttributes
            // 
            this.searchAttributes.Checked = true;
            this.searchAttributes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchAttributes.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchAttributes.Location = new System.Drawing.Point(3, 42);
            this.searchAttributes.Name = "searchAttributes";
            this.searchAttributes.Size = new System.Drawing.Size(420, 19);
            this.searchAttributes.TabIndex = 1;
            this.searchAttributes.Text = "Search Attributes";
            this.searchAttributes.UseVisualStyleBackColor = true;
            // 
            // searchClasses
            // 
            this.searchClasses.Checked = true;
            this.searchClasses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.searchClasses.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchClasses.Location = new System.Drawing.Point(3, 22);
            this.searchClasses.Name = "searchClasses";
            this.searchClasses.Size = new System.Drawing.Size(420, 20);
            this.searchClasses.TabIndex = 0;
            this.searchClasses.Text = "Search Classes";
            this.searchClasses.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSearch.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSearch.Location = new System.Drawing.Point(0, 272);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(426, 47);
            this.btnSearch.TabIndex = 23;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = Orientation.Horizontal;
            this.splitContainer.FixedPanel = FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.searchInBox);
            this.splitContainer.Panel1.Controls.Add(this.searchWhatBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.listViewResult);
            this.splitContainer.Size = new System.Drawing.Size(1112, 561);
            this.splitContainer.SplitterDistance = 200;
            this.splitContainer.TabIndex = 22;

            this.selectAll.Checked = true;
            // 
            // SearchPanel
            // 
            this.Controls.Add(this.splitContainer);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(16, 600);
            this.Name = "SearchingPanel";
            this.Text = "Search";
            this.searchWhatBox.ResumeLayout(false);
            this.searchWhatBox.PerformLayout();
            this.searchInBox.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

            this.Dock = DockStyle.Fill;
        }

        private void SelectAllSearchIn(object sender, EventArgs e)
        {
            if (this.selectAll.Checked)
            {
                this.searchClasses.Checked = true;
                this.searchAttributes.Checked = true;
                this.searchTaxonomies.Checked = true;
                this.searchEnumerations.Checked = true;
                this.searchMeasureUnits.Checked = true;
                this.searchMeasureClasses.Checked = true;
            }
            else
            {
                this.searchClasses.Checked = false;
                this.searchAttributes.Checked = false;
                this.searchTaxonomies.Checked = false;
                this.searchEnumerations.Checked = false;
                this.searchMeasureUnits.Checked = false;
                this.searchMeasureClasses.Checked = false;
            }
        }

        private static int SetBit(int data, int bitValue, int position)
        {
            var mask = 0b00000001;
            mask = mask << position;

            if (bitValue == 1)
            {
                return data | mask;
            }
            else
            {
                return data & ~mask;
            }
        }
        private int GetFilter()
        {
            int filter = 0;

            if (searchId.Checked)
            {
                filter = SetBit(filter, 1, 0);
            }

            if (searchName.Checked)
            {
                filter = SetBit(filter, 1, 1);
            }

            if (matchCase.Checked)
            {
                filter = SetBit(filter, 1, 2);
            }

            if (searchClasses.Checked)
            {
                filter = SetBit(filter, 1, 3);
            }

            if (searchAttributes.Checked)
            {
                filter = SetBit(filter, 1, 4);
            }

            if (searchTaxonomies.Checked)
            {
                filter = SetBit(filter, 1, 5);
            }

            if (searchEnumerations.Checked)
            {
                filter = SetBit(filter, 1, 6);
            }

            if (searchMeasureUnits.Checked)
            {
                filter = SetBit(filter, 1, 7);
            }

            if (searchMeasureClasses.Checked)
            {
                filter = SetBit(filter, 1, 8);
            }

            return filter;
        }
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            listViewResult.Items.Clear();
            string text = searchString.Text;
            int filter = GetFilter();

            List<KeyValuePair<string, object>> results = CMSearcher.SearchText(text, model, filter);

            Action<ListView, IIdentifiable, string, Color> AddObject = (listView, element, type, color) =>
            {
                ListViewItem listViewItem = new ListViewItem(new string[] { $"{element.Id}", $"{element.Name}", $"{type}" });
                listViewItem.Tag = element;
                listViewItem.BackColor = color;
                listView.Items.Add(listViewItem);
            };

            if (results.Count > 0)
            {
                foreach (KeyValuePair<string, object> result in results)
                {
                    string type = result.Key;

                    if (type.Equals("class"))
                    {
                        ConceptualModelClass cmClass = (ConceptualModelClass)result.Value;
                        AddObject(listViewResult, cmClass, $"Class", Color.Yellow);
                    }

                    if (type.Equals("attribute"))
                    {
                        ConceptualModelAttribute attribute = (ConceptualModelAttribute)result.Value;
                        AddObject(listViewResult, attribute, $"Attribute", Color.Green);
                    }

                    if (type.Equals("taxonomy"))
                    {
                        Taxonomy taxonomy = (Taxonomy)result.Value;
                        AddObject(listViewResult, taxonomy, $"Taxonomy", Color.Orange);
                    }

                    if (type.Equals("taxonomynode"))
                    {
                        TaxonomyNode taxonomyNode = (TaxonomyNode)result.Value;
                        AddObject(listViewResult, taxonomyNode, $"Taxonomy Node", Color.Aquamarine);
                    }

                    if (type.Equals("enumeration"))
                    {
                        ConceptualModelEnumeration enumeration = (ConceptualModelEnumeration)result.Value;
                        AddObject(listViewResult, enumeration, $"Enumeration", Color.CadetBlue);
                    }

                    if (type.Equals("enumerationitem"))
                    {
                        ConceptualModelEnumerationItem enumerationListItem = (ConceptualModelEnumerationItem)result.Value;
                        AddObject(listViewResult, enumerationListItem, $"Enumeration Item", Color.LightSkyBlue);
                    }

                    if (type.Equals("measureunit"))
                    {
                        MeasureUnit measureUnit = (MeasureUnit)result.Value;
                        AddObject(listViewResult, measureUnit, $"Measure Unit", Color.LightGreen);
                    }

                    if (type.Equals("measureclass"))
                    {
                        MeasureClass measureClass = (MeasureClass)result.Value;
                        AddObject(listViewResult, measureClass, $"Measure Class", Color.SpringGreen);
                    }
                }
            }
        }
    }
}
