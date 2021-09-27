
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
            this.comboBoxAttributeName = new System.Windows.Forms.ComboBox();
            this.comboBoxAttributeId = new System.Windows.Forms.ComboBox();
            this.comboBoxClassName = new System.Windows.Forms.ComboBox();
            this.comboBoxClassId = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.dataGridView.Size = new System.Drawing.Size(1012, 621);
            this.dataGridView.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxAttributeName);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxAttributeId);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxClassName);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxClassId);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(1012, 654);
            this.splitContainer1.SplitterDistance = 29;
            this.splitContainer1.TabIndex = 1;
            // 
            // comboBoxAttributeName
            // 
            this.comboBoxAttributeName.Dock = System.Windows.Forms.DockStyle.Left;
            this.comboBoxAttributeName.FormattingEnabled = true;
            this.comboBoxAttributeName.Location = new System.Drawing.Point(750, 0);
            this.comboBoxAttributeName.MaximumSize = new System.Drawing.Size(250, 0);
            this.comboBoxAttributeName.MinimumSize = new System.Drawing.Size(250, 0);
            this.comboBoxAttributeName.Name = "comboBoxAttributeName";
            this.comboBoxAttributeName.Size = new System.Drawing.Size(250, 23);
            this.comboBoxAttributeName.Sorted = true;
            this.comboBoxAttributeName.TabIndex = 3;
            this.comboBoxAttributeName.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAttributeName_SelectedIndexChanged);
            // 
            // comboBoxAttributeId
            // 
            this.comboBoxAttributeId.Dock = System.Windows.Forms.DockStyle.Left;
            this.comboBoxAttributeId.FormattingEnabled = true;
            this.comboBoxAttributeId.Location = new System.Drawing.Point(500, 0);
            this.comboBoxAttributeId.MaximumSize = new System.Drawing.Size(250, 0);
            this.comboBoxAttributeId.MinimumSize = new System.Drawing.Size(250, 0);
            this.comboBoxAttributeId.Name = "comboBoxAttributeId";
            this.comboBoxAttributeId.Size = new System.Drawing.Size(250, 23);
            this.comboBoxAttributeId.Sorted = true;
            this.comboBoxAttributeId.TabIndex = 2;
            this.comboBoxAttributeId.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAttributeId_SelectedIndexChanged);
            // 
            // comboBoxClassName
            // 
            this.comboBoxClassName.Dock = System.Windows.Forms.DockStyle.Left;
            this.comboBoxClassName.FormattingEnabled = true;
            this.comboBoxClassName.Location = new System.Drawing.Point(250, 0);
            this.comboBoxClassName.MaximumSize = new System.Drawing.Size(250, 0);
            this.comboBoxClassName.MinimumSize = new System.Drawing.Size(250, 0);
            this.comboBoxClassName.Name = "comboBoxClassName";
            this.comboBoxClassName.Size = new System.Drawing.Size(250, 23);
            this.comboBoxClassName.Sorted = true;
            this.comboBoxClassName.TabIndex = 1;
            this.comboBoxClassName.SelectedIndexChanged += new System.EventHandler(this.ComboBoxClassName_SelectedIndexChanged);
            // 
            // comboBoxClassId
            // 
            this.comboBoxClassId.Dock = System.Windows.Forms.DockStyle.Left;
            this.comboBoxClassId.FormattingEnabled = true;
            this.comboBoxClassId.Location = new System.Drawing.Point(0, 0);
            this.comboBoxClassId.MaximumSize = new System.Drawing.Size(250, 0);
            this.comboBoxClassId.MinimumSize = new System.Drawing.Size(250, 0);
            this.comboBoxClassId.Name = "comboBoxClassId";
            this.comboBoxClassId.Size = new System.Drawing.Size(250, 23);
            this.comboBoxClassId.Sorted = true;
            this.comboBoxClassId.TabIndex = 0;
            this.comboBoxClassId.SelectedIndexChanged += new System.EventHandler(this.ComboBoxClassId_SelectedIndexChanged);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox comboBoxClassName;
        private System.Windows.Forms.ComboBox comboBoxClassId;
        private System.Windows.Forms.ComboBox comboBoxAttributeName;
        private System.Windows.Forms.ComboBox comboBoxAttributeId;
    }
}