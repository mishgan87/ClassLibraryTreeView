
namespace ClassLibraryTreeView.Forms
{
    partial class ClassAttributesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassAttributesForm));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxFilterBy = new System.Windows.Forms.GroupBox();
            this.checkBoxFilterByAttributeName = new System.Windows.Forms.CheckBox();
            this.checkBoxFilterByAttributeId = new System.Windows.Forms.CheckBox();
            this.checkBoxFilterByClassName = new System.Windows.Forms.CheckBox();
            this.checkBoxFilterByClassId = new System.Windows.Forms.CheckBox();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.listViewFIlter = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxFilterBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1012, 297);
            this.dataGridView.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewFIlter);
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxFilterBy);
            this.splitContainer1.Panel1.Controls.Add(this.btnResetFilter);
            this.splitContainer1.Panel1.Controls.Add(this.btnApplyFilter);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(1012, 654);
            this.splitContainer1.SplitterDistance = 353;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBoxFilterBy
            // 
            this.groupBoxFilterBy.Controls.Add(this.checkBoxFilterByAttributeName);
            this.groupBoxFilterBy.Controls.Add(this.checkBoxFilterByAttributeId);
            this.groupBoxFilterBy.Controls.Add(this.checkBoxFilterByClassName);
            this.groupBoxFilterBy.Controls.Add(this.checkBoxFilterByClassId);
            this.groupBoxFilterBy.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxFilterBy.Location = new System.Drawing.Point(0, 0);
            this.groupBoxFilterBy.Name = "groupBoxFilterBy";
            this.groupBoxFilterBy.Size = new System.Drawing.Size(1012, 106);
            this.groupBoxFilterBy.TabIndex = 9;
            this.groupBoxFilterBy.TabStop = false;
            // 
            // checkBoxFilterByAttributeName
            // 
            this.checkBoxFilterByAttributeName.AutoSize = true;
            this.checkBoxFilterByAttributeName.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxFilterByAttributeName.Location = new System.Drawing.Point(3, 76);
            this.checkBoxFilterByAttributeName.Name = "checkBoxFilterByAttributeName";
            this.checkBoxFilterByAttributeName.Size = new System.Drawing.Size(1006, 19);
            this.checkBoxFilterByAttributeName.TabIndex = 3;
            this.checkBoxFilterByAttributeName.Text = "Filter by Attribute Name";
            this.checkBoxFilterByAttributeName.UseVisualStyleBackColor = true;
            this.checkBoxFilterByAttributeName.CheckedChanged += new System.EventHandler(this.CheckBoxCheckedChanged);
            // 
            // checkBoxFilterByAttributeId
            // 
            this.checkBoxFilterByAttributeId.AutoSize = true;
            this.checkBoxFilterByAttributeId.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxFilterByAttributeId.Location = new System.Drawing.Point(3, 57);
            this.checkBoxFilterByAttributeId.Name = "checkBoxFilterByAttributeId";
            this.checkBoxFilterByAttributeId.Size = new System.Drawing.Size(1006, 19);
            this.checkBoxFilterByAttributeId.TabIndex = 2;
            this.checkBoxFilterByAttributeId.Text = "Filter by Attribute Id";
            this.checkBoxFilterByAttributeId.UseVisualStyleBackColor = true;
            this.checkBoxFilterByAttributeId.CheckedChanged += new System.EventHandler(this.CheckBoxCheckedChanged);
            // 
            // checkBoxFilterByClassName
            // 
            this.checkBoxFilterByClassName.AutoSize = true;
            this.checkBoxFilterByClassName.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxFilterByClassName.Location = new System.Drawing.Point(3, 38);
            this.checkBoxFilterByClassName.Name = "checkBoxFilterByClassName";
            this.checkBoxFilterByClassName.Size = new System.Drawing.Size(1006, 19);
            this.checkBoxFilterByClassName.TabIndex = 1;
            this.checkBoxFilterByClassName.Text = "Filter by Class Name";
            this.checkBoxFilterByClassName.UseVisualStyleBackColor = true;
            this.checkBoxFilterByClassName.CheckedChanged += new System.EventHandler(this.CheckBoxCheckedChanged);
            // 
            // checkBoxFilterByClassId
            // 
            this.checkBoxFilterByClassId.AutoSize = true;
            this.checkBoxFilterByClassId.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxFilterByClassId.Location = new System.Drawing.Point(3, 19);
            this.checkBoxFilterByClassId.Name = "checkBoxFilterByClassId";
            this.checkBoxFilterByClassId.Size = new System.Drawing.Size(1006, 19);
            this.checkBoxFilterByClassId.TabIndex = 0;
            this.checkBoxFilterByClassId.Text = "Filter by Class Id";
            this.checkBoxFilterByClassId.UseVisualStyleBackColor = true;
            this.checkBoxFilterByClassId.CheckedChanged += new System.EventHandler(this.CheckBoxCheckedChanged);
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.Image = global::ClassLibraryTreeView.Properties.Resources.cancel;
            this.btnResetFilter.Location = new System.Drawing.Point(925, 289);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(75, 62);
            this.btnResetFilter.TabIndex = 8;
            this.btnResetFilter.UseVisualStyleBackColor = true;
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Image = global::ClassLibraryTreeView.Properties.Resources.apply;
            this.btnApplyFilter.Location = new System.Drawing.Point(832, 288);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(75, 62);
            this.btnApplyFilter.TabIndex = 7;
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.BtnApplyFilter_Click);
            // 
            // listViewFIlter
            // 
            this.listViewFIlter.Dock = System.Windows.Forms.DockStyle.Top;
            this.listViewFIlter.FullRowSelect = true;
            this.listViewFIlter.GridLines = true;
            this.listViewFIlter.HideSelection = false;
            this.listViewFIlter.Location = new System.Drawing.Point(0, 106);
            this.listViewFIlter.Name = "listViewFIlter";
            this.listViewFIlter.Size = new System.Drawing.Size(1012, 167);
            this.listViewFIlter.TabIndex = 10;
            this.listViewFIlter.UseCompatibleStateImageBehavior = false;
            this.listViewFIlter.View = System.Windows.Forms.View.Details;
            // 
            // ClassAttributesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 654);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ClassAttributesForm";
            this.Text = "Classes Attributes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxFilterBy.ResumeLayout(false);
            this.groupBoxFilterBy.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.GroupBox groupBoxFilterBy;
        private System.Windows.Forms.CheckBox checkBoxFilterByAttributeName;
        private System.Windows.Forms.CheckBox checkBoxFilterByAttributeId;
        private System.Windows.Forms.CheckBox checkBoxFilterByClassName;
        private System.Windows.Forms.CheckBox checkBoxFilterByClassId;
        private System.Windows.Forms.ListView listViewFIlter;
    }
}