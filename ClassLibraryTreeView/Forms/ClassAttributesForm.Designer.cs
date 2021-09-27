
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
            this.checkBoxFilterByAttributeName = new System.Windows.Forms.CheckBox();
            this.checkBoxFilterByAttributeId = new System.Windows.Forms.CheckBox();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.checkBoxFilterByClassName = new System.Windows.Forms.CheckBox();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.checkBoxFilterByClassId = new System.Windows.Forms.CheckBox();
            this.comboBoxAttributeName = new System.Windows.Forms.ComboBox();
            this.comboBoxAttributeId = new System.Windows.Forms.ComboBox();
            this.comboBoxClassName = new System.Windows.Forms.ComboBox();
            this.comboBoxClassId = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.dataGridView.Size = new System.Drawing.Size(684, 547);
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
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(684, 792);
            this.splitContainer1.SplitterDistance = 241;
            this.splitContainer1.TabIndex = 1;
            // 
            // checkBoxFilterByAttributeName
            // 
            this.checkBoxFilterByAttributeName.AutoSize = true;
            this.checkBoxFilterByAttributeName.Location = new System.Drawing.Point(3, 147);
            this.checkBoxFilterByAttributeName.Name = "checkBoxFilterByAttributeName";
            this.checkBoxFilterByAttributeName.Size = new System.Drawing.Size(194, 18);
            this.checkBoxFilterByAttributeName.TabIndex = 3;
            this.checkBoxFilterByAttributeName.Text = "Filter by Attribute Name";
            this.checkBoxFilterByAttributeName.UseVisualStyleBackColor = true;
            this.checkBoxFilterByAttributeName.CheckedChanged += new System.EventHandler(this.CheckBoxCheckedChanged);
            // 
            // checkBoxFilterByAttributeId
            // 
            this.checkBoxFilterByAttributeId.AutoSize = true;
            this.checkBoxFilterByAttributeId.Location = new System.Drawing.Point(3, 99);
            this.checkBoxFilterByAttributeId.Name = "checkBoxFilterByAttributeId";
            this.checkBoxFilterByAttributeId.Size = new System.Drawing.Size(180, 18);
            this.checkBoxFilterByAttributeId.TabIndex = 2;
            this.checkBoxFilterByAttributeId.Text = "Filter by Attribute Id";
            this.checkBoxFilterByAttributeId.UseVisualStyleBackColor = true;
            this.checkBoxFilterByAttributeId.CheckedChanged += new System.EventHandler(this.CheckBoxCheckedChanged);
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnResetFilter.Location = new System.Drawing.Point(103, 3);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(94, 37);
            this.btnResetFilter.TabIndex = 8;
            this.btnResetFilter.Text = "Reset";
            this.btnResetFilter.UseVisualStyleBackColor = true;
            this.btnResetFilter.Click += new System.EventHandler(this.BtnResetFilter_Click);
            // 
            // checkBoxFilterByClassName
            // 
            this.checkBoxFilterByClassName.AutoSize = true;
            this.checkBoxFilterByClassName.Location = new System.Drawing.Point(3, 51);
            this.checkBoxFilterByClassName.Name = "checkBoxFilterByClassName";
            this.checkBoxFilterByClassName.Size = new System.Drawing.Size(166, 18);
            this.checkBoxFilterByClassName.TabIndex = 1;
            this.checkBoxFilterByClassName.Text = "Filter by Class Name";
            this.checkBoxFilterByClassName.UseVisualStyleBackColor = true;
            this.checkBoxFilterByClassName.CheckedChanged += new System.EventHandler(this.CheckBoxCheckedChanged);
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnApplyFilter.Location = new System.Drawing.Point(3, 3);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(94, 37);
            this.btnApplyFilter.TabIndex = 7;
            this.btnApplyFilter.Text = "Apply";
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.BtnApplyFilter_Click);
            // 
            // checkBoxFilterByClassId
            // 
            this.checkBoxFilterByClassId.AutoSize = true;
            this.checkBoxFilterByClassId.Location = new System.Drawing.Point(3, 3);
            this.checkBoxFilterByClassId.Name = "checkBoxFilterByClassId";
            this.checkBoxFilterByClassId.Size = new System.Drawing.Size(152, 18);
            this.checkBoxFilterByClassId.TabIndex = 0;
            this.checkBoxFilterByClassId.Text = "Filter by Class Id";
            this.checkBoxFilterByClassId.UseVisualStyleBackColor = true;
            this.checkBoxFilterByClassId.CheckedChanged += new System.EventHandler(this.CheckBoxCheckedChanged);
            // 
            // comboBoxAttributeName
            // 
            this.comboBoxAttributeName.FormattingEnabled = true;
            this.comboBoxAttributeName.Location = new System.Drawing.Point(3, 171);
            this.comboBoxAttributeName.Name = "comboBoxAttributeName";
            this.comboBoxAttributeName.Size = new System.Drawing.Size(267, 23);
            this.comboBoxAttributeName.TabIndex = 9;
            // 
            // comboBoxAttributeId
            // 
            this.comboBoxAttributeId.FormattingEnabled = true;
            this.comboBoxAttributeId.Location = new System.Drawing.Point(3, 123);
            this.comboBoxAttributeId.Name = "comboBoxAttributeId";
            this.comboBoxAttributeId.Size = new System.Drawing.Size(267, 23);
            this.comboBoxAttributeId.TabIndex = 10;
            // 
            // comboBoxClassName
            // 
            this.comboBoxClassName.FormattingEnabled = true;
            this.comboBoxClassName.Location = new System.Drawing.Point(3, 75);
            this.comboBoxClassName.Name = "comboBoxClassName";
            this.comboBoxClassName.Size = new System.Drawing.Size(267, 23);
            this.comboBoxClassName.TabIndex = 11;
            // 
            // comboBoxClassId
            // 
            this.comboBoxClassId.FormattingEnabled = true;
            this.comboBoxClassId.Location = new System.Drawing.Point(3, 27);
            this.comboBoxClassId.Name = "comboBoxClassId";
            this.comboBoxClassId.Size = new System.Drawing.Size(267, 23);
            this.comboBoxClassId.TabIndex = 12;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.Controls.Add(this.checkBoxFilterByClassId, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxClassId, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxFilterByClassName, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxClassName, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxFilterByAttributeId, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxAttributeId, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxFilterByAttributeName, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxAttributeName, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(684, 241);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnResetFilter, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnApplyFilter, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 195);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 43);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // ClassAttributesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 792);
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.CheckBox checkBoxFilterByAttributeName;
        private System.Windows.Forms.CheckBox checkBoxFilterByAttributeId;
        private System.Windows.Forms.CheckBox checkBoxFilterByClassName;
        private System.Windows.Forms.CheckBox checkBoxFilterByClassId;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBoxAttributeId;
        private System.Windows.Forms.ComboBox comboBoxClassName;
        private System.Windows.Forms.ComboBox comboBoxAttributeName;
        private System.Windows.Forms.ComboBox comboBoxClassId;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}