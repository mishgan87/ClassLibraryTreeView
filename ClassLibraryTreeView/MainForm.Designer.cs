using System.Windows.Forms;

namespace ClassLibraryTreeView
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControlProperties = new System.Windows.Forms.TabControl();
            this.menuBar = new System.Windows.Forms.ToolStrip();
            this.labelModelName = new System.Windows.Forms.ToolStripLabel();
            this.buttonOpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExportPermissibleGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.buttonExportXml = new System.Windows.Forms.ToolStripButton();
            this.buttonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripInfo = new System.Windows.Forms.ToolStrip();
            this.labelInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.layoutMain = new System.Windows.Forms.SplitContainer();
            this.layoutSplitter = new System.Windows.Forms.SplitContainer();
            this.tabControlTrees = new System.Windows.Forms.TabControl();
            this.layoutProperties = new System.Windows.Forms.SplitContainer();
            this.menuBar.SuspendLayout();
            this.toolStripInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).BeginInit();
            this.layoutMain.Panel1.SuspendLayout();
            this.layoutMain.Panel2.SuspendLayout();
            this.layoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSplitter)).BeginInit();
            this.layoutSplitter.Panel1.SuspendLayout();
            this.layoutSplitter.Panel2.SuspendLayout();
            this.layoutSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutProperties)).BeginInit();
            this.layoutProperties.Panel1.SuspendLayout();
            this.layoutProperties.Panel2.SuspendLayout();
            this.layoutProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlProperties.Location = new System.Drawing.Point(0, 0);
            this.tabControlProperties.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(646, 337);
            this.tabControlProperties.TabIndex = 0;
            // 
            // menuBar
            // 
            this.menuBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelModelName,
            this.buttonOpenFile,
            this.buttonSave,
            this.buttonExportXml,
            this.toolStripButtonExportPermissibleGrid,
            this.toolStripButton1,
            this.toolStripButton3});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.MinimumSize = new System.Drawing.Size(0, 64);
            this.menuBar.Name = "menuBar";
            this.menuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuBar.Size = new System.Drawing.Size(866, 64);
            this.menuBar.TabIndex = 0;
            this.menuBar.Text = "toolStrip1";
            // 
            // labelModelName
            // 
            this.labelModelName.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.labelModelName.AutoToolTip = true;
            this.labelModelName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelModelName.Name = "labelModelName";
            this.labelModelName.Size = new System.Drawing.Size(21, 61);
            this.labelModelName.Text = "   ";
            this.labelModelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonOpenFile.Image = global::ClassLibraryTreeView.Properties.Resources.open;
            this.buttonOpenFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(52, 61);
            this.buttonOpenFile.ToolTipText = "Open File";
            this.buttonOpenFile.Click += new System.EventHandler(this.ButtonOpenFile_Click);
            // 
            // toolStripButtonExportPermissibleGrid
            // 
            this.toolStripButtonExportPermissibleGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExportPermissibleGrid.Image = global::ClassLibraryTreeView.Properties.Resources.excel;
            this.toolStripButtonExportPermissibleGrid.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonExportPermissibleGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportPermissibleGrid.Name = "toolStripButtonExportPermissibleGrid";
            this.toolStripButtonExportPermissibleGrid.Size = new System.Drawing.Size(52, 61);
            this.toolStripButtonExportPermissibleGrid.ToolTipText = "Export Permissible Grid";
            this.toolStripButtonExportPermissibleGrid.Click += new System.EventHandler(this.ExportPermissibleGrid);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::ClassLibraryTreeView.Properties.Resources.search;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 61);
            this.toolStripButton1.ToolTipText = "Search";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::ClassLibraryTreeView.Properties.Resources.filter;
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(52, 61);
            this.toolStripButton3.ToolTipText = "Filter";
            // 
            // buttonExportXml
            // 
            this.buttonExportXml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonExportXml.Image = global::ClassLibraryTreeView.Properties.Resources.xml;
            this.buttonExportXml.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonExportXml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonExportXml.Name = "buttonExportXml";
            this.buttonExportXml.Size = new System.Drawing.Size(52, 61);
            this.buttonExportXml.ToolTipText = "Export To XML";
            // 
            // buttonSave
            // 
            this.buttonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSave.Image = global::ClassLibraryTreeView.Properties.Resources.save;
            this.buttonSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(52, 61);
            this.buttonSave.ToolTipText = "Save Model";
            // 
            // toolStripInfo
            // 
            this.toolStripInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripInfo.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelInfo,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6});
            this.toolStripInfo.Location = new System.Drawing.Point(0, 0);
            this.toolStripInfo.Name = "toolStripInfo";
            this.toolStripInfo.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripInfo.Size = new System.Drawing.Size(646, 79);
            this.toolStripInfo.TabIndex = 0;
            this.toolStripInfo.Text = "toolStrip2";
            // 
            // labelInfo
            // 
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(41, 76);
            this.labelInfo.Text = "Info";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::ClassLibraryTreeView.Properties.Resources.revert;
            this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(52, 76);
            this.toolStripButton4.ToolTipText = "Revert";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::ClassLibraryTreeView.Properties.Resources.delete;
            this.toolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(52, 76);
            this.toolStripButton5.ToolTipText = "Remove";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::ClassLibraryTreeView.Properties.Resources.add;
            this.toolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(52, 76);
            this.toolStripButton6.ToolTipText = "Add";
            // 
            // layoutMain
            // 
            this.layoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.layoutMain.IsSplitterFixed = true;
            this.layoutMain.Location = new System.Drawing.Point(0, 0);
            this.layoutMain.Name = "layoutMain";
            this.layoutMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // layoutMain.Panel1
            // 
            this.layoutMain.Panel1.Controls.Add(this.menuBar);
            // 
            // layoutMain.Panel2
            // 
            this.layoutMain.Panel2.Controls.Add(this.layoutSplitter);
            this.layoutMain.Size = new System.Drawing.Size(866, 488);
            this.layoutMain.SplitterDistance = 64;
            this.layoutMain.TabIndex = 9;
            // 
            // layoutSplitter
            // 
            this.layoutSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutSplitter.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.layoutSplitter.Location = new System.Drawing.Point(0, 0);
            this.layoutSplitter.Name = "layoutSplitter";
            // 
            // layoutSplitter.Panel1
            // 
            this.layoutSplitter.Panel1.Controls.Add(this.tabControlTrees);
            // 
            // layoutSplitter.Panel2
            // 
            this.layoutSplitter.Panel2.Controls.Add(this.layoutProperties);
            this.layoutSplitter.Size = new System.Drawing.Size(866, 420);
            this.layoutSplitter.SplitterDistance = 216;
            this.layoutSplitter.TabIndex = 12;
            // 
            // tabControlTrees
            // 
            this.tabControlTrees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTrees.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlTrees.Location = new System.Drawing.Point(0, 0);
            this.tabControlTrees.Name = "tabControlTrees";
            this.tabControlTrees.SelectedIndex = 0;
            this.tabControlTrees.Size = new System.Drawing.Size(216, 420);
            this.tabControlTrees.TabIndex = 11;
            // 
            // layoutProperties
            // 
            this.layoutProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutProperties.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.layoutProperties.IsSplitterFixed = true;
            this.layoutProperties.Location = new System.Drawing.Point(0, 0);
            this.layoutProperties.Name = "layoutProperties";
            this.layoutProperties.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // layoutProperties.Panel1
            // 
            this.layoutProperties.Panel1.Controls.Add(this.toolStripInfo);
            // 
            // layoutProperties.Panel2
            // 
            this.layoutProperties.Panel2.Controls.Add(this.tabControlProperties);
            this.layoutProperties.Size = new System.Drawing.Size(646, 420);
            this.layoutProperties.SplitterDistance = 79;
            this.layoutProperties.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 488);
            this.Controls.Add(this.layoutMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "CMViewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.toolStripInfo.ResumeLayout(false);
            this.toolStripInfo.PerformLayout();
            this.layoutMain.Panel1.ResumeLayout(false);
            this.layoutMain.Panel1.PerformLayout();
            this.layoutMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).EndInit();
            this.layoutMain.ResumeLayout(false);
            this.layoutSplitter.Panel1.ResumeLayout(false);
            this.layoutSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutSplitter)).EndInit();
            this.layoutSplitter.ResumeLayout(false);
            this.layoutProperties.Panel1.ResumeLayout(false);
            this.layoutProperties.Panel1.PerformLayout();
            this.layoutProperties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutProperties)).EndInit();
            this.layoutProperties.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControlProperties;
        private ToolStrip menuBar;
        private ToolStripLabel labelModelName;
        private ToolStripButton buttonOpenFile;
        private ToolStripButton toolStripButtonExportPermissibleGrid;
        private ToolStrip toolStripInfo;
        private ToolStripLabel labelInfo;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton3;
        private SplitContainer layoutMain;
        private SplitContainer layoutSplitter;
        private SplitContainer layoutProperties;
        private TabControl tabControlTrees;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripButton toolStripButton6;
        private ToolStripButton buttonExportXml;
        private ToolStripButton buttonSave;
    }
}

