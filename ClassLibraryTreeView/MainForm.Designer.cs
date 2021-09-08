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
            this.toolStripInfo = new System.Windows.Forms.ToolStrip();
            this.labelInfo = new System.Windows.Forms.ToolStripLabel();
            this.buttonOpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExportPermissibleGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.layoutMain = new System.Windows.Forms.SplitContainer();
            this.layoutProperties = new System.Windows.Forms.SplitContainer();
            this.layoutSplitter = new System.Windows.Forms.SplitContainer();
            this.tabControlTrees = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuBar.SuspendLayout();
            this.toolStripInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).BeginInit();
            this.layoutMain.Panel1.SuspendLayout();
            this.layoutMain.Panel2.SuspendLayout();
            this.layoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutProperties)).BeginInit();
            this.layoutProperties.Panel1.SuspendLayout();
            this.layoutProperties.Panel2.SuspendLayout();
            this.layoutProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSplitter)).BeginInit();
            this.layoutSplitter.Panel1.SuspendLayout();
            this.layoutSplitter.Panel2.SuspendLayout();
            this.layoutSplitter.SuspendLayout();
            this.tabControlTrees.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlProperties.Location = new System.Drawing.Point(0, 0);
            this.tabControlProperties.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(971, 496);
            this.tabControlProperties.TabIndex = 0;
            // 
            // menuBar
            // 
            this.menuBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelModelName,
            this.buttonOpenFile,
            this.toolStripButtonExportPermissibleGrid,
            this.toolStripButton1,
            this.toolStripButton3});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.MinimumSize = new System.Drawing.Size(0, 64);
            this.menuBar.Name = "menuBar";
            this.menuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuBar.Size = new System.Drawing.Size(1304, 64);
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
            // toolStripInfo
            // 
            this.toolStripInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripInfo.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelInfo,
            this.toolStripButton7,
            this.toolStripButton6});
            this.toolStripInfo.Location = new System.Drawing.Point(0, 0);
            this.toolStripInfo.Name = "toolStripInfo";
            this.toolStripInfo.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripInfo.Size = new System.Drawing.Size(971, 79);
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
            // toolStripButton7
            // 
            this.toolStripButton7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = global::ClassLibraryTreeView.Properties.Resources.delete;
            this.toolStripButton7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(52, 76);
            this.toolStripButton7.Text = "toolStripButton7";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::ClassLibraryTreeView.Properties.Resources.revert;
            this.toolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(52, 76);
            this.toolStripButton6.Text = "toolStripButton6";
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
            this.layoutMain.Size = new System.Drawing.Size(1304, 647);
            this.layoutMain.SplitterDistance = 64;
            this.layoutMain.TabIndex = 9;
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
            this.layoutProperties.Size = new System.Drawing.Size(971, 579);
            this.layoutProperties.SplitterDistance = 79;
            this.layoutProperties.TabIndex = 11;
            // 
            // layoutSplitter
            // 
            this.layoutSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.layoutSplitter.Size = new System.Drawing.Size(1304, 579);
            this.layoutSplitter.SplitterDistance = 329;
            this.layoutSplitter.TabIndex = 12;
            // 
            // tabControlTrees
            // 
            this.tabControlTrees.Controls.Add(this.tabPage1);
            this.tabControlTrees.Controls.Add(this.tabPage2);
            this.tabControlTrees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTrees.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlTrees.Location = new System.Drawing.Point(0, 0);
            this.tabControlTrees.Name = "tabControlTrees";
            this.tabControlTrees.SelectedIndex = 0;
            this.tabControlTrees.Size = new System.Drawing.Size(329, 579);
            this.tabControlTrees.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(321, 550);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 647);
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
            this.layoutProperties.Panel1.ResumeLayout(false);
            this.layoutProperties.Panel1.PerformLayout();
            this.layoutProperties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutProperties)).EndInit();
            this.layoutProperties.ResumeLayout(false);
            this.layoutSplitter.Panel1.ResumeLayout(false);
            this.layoutSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutSplitter)).EndInit();
            this.layoutSplitter.ResumeLayout(false);
            this.tabControlTrees.ResumeLayout(false);
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
        private ToolStripButton toolStripButton7;
        private ToolStripButton toolStripButton6;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton3;
        private SplitContainer layoutMain;
        private SplitContainer layoutSplitter;
        private SplitContainer layoutProperties;
        private TabControl tabControlTrees;
        private TabPage tabPage1;
        private TabPage tabPage2;
    }
}

