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
            this.toolStripInfo = new System.Windows.Forms.ToolStrip();
            this.labelInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
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
            this.tabControlProperties.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(859, 447);
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
            this.menuBar.MinimumSize = new System.Drawing.Size(0, 79);
            this.menuBar.Name = "menuBar";
            this.menuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuBar.Size = new System.Drawing.Size(1154, 79);
            this.menuBar.TabIndex = 0;
            this.menuBar.Text = "toolStrip1";
            // 
            // labelModelName
            // 
            this.labelModelName.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.labelModelName.AutoToolTip = true;
            this.labelModelName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelModelName.Name = "labelModelName";
            this.labelModelName.Size = new System.Drawing.Size(21, 76);
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
            this.buttonOpenFile.Size = new System.Drawing.Size(52, 76);
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
            this.toolStripButtonExportPermissibleGrid.Size = new System.Drawing.Size(52, 76);
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
            this.toolStripButton1.Size = new System.Drawing.Size(52, 76);
            this.toolStripButton1.ToolTipText = "Search";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::ClassLibraryTreeView.Properties.Resources.filter;
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(52, 76);
            this.toolStripButton3.ToolTipText = "Filter";
            // 
            // toolStripInfo
            // 
            this.toolStripInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripInfo.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelInfo,
            this.toolStripButton7,
            this.toolStripButton6,
            this.toolStripButton4,
            this.toolStripButton2});
            this.toolStripInfo.Location = new System.Drawing.Point(0, 0);
            this.toolStripInfo.Name = "toolStripInfo";
            this.toolStripInfo.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripInfo.Size = new System.Drawing.Size(859, 79);
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
            // toolStripButton4
            // 
            this.toolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::ClassLibraryTreeView.Properties.Resources.add;
            this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(52, 76);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::ClassLibraryTreeView.Properties.Resources.edit;
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(52, 76);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // layoutMain
            // 
            this.layoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.layoutMain.IsSplitterFixed = true;
            this.layoutMain.Location = new System.Drawing.Point(0, 0);
            this.layoutMain.Margin = new System.Windows.Forms.Padding(4);
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
            this.layoutMain.Size = new System.Drawing.Size(1154, 600);
            this.layoutMain.SplitterDistance = 64;
            this.layoutMain.SplitterWidth = 5;
            this.layoutMain.TabIndex = 9;
            // 
            // layoutSplitter
            // 
            this.layoutSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutSplitter.Location = new System.Drawing.Point(0, 0);
            this.layoutSplitter.Margin = new System.Windows.Forms.Padding(4);
            this.layoutSplitter.Name = "layoutSplitter";
            // 
            // layoutSplitter.Panel1
            // 
            this.layoutSplitter.Panel1.Controls.Add(this.tabControlTrees);
            // 
            // layoutSplitter.Panel2
            // 
            this.layoutSplitter.Panel2.Controls.Add(this.layoutProperties);
            this.layoutSplitter.Size = new System.Drawing.Size(1154, 531);
            this.layoutSplitter.SplitterDistance = 290;
            this.layoutSplitter.SplitterWidth = 5;
            this.layoutSplitter.TabIndex = 12;
            // 
            // tabControlTrees
            // 
            this.tabControlTrees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTrees.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlTrees.Location = new System.Drawing.Point(0, 0);
            this.tabControlTrees.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlTrees.Name = "tabControlTrees";
            this.tabControlTrees.SelectedIndex = 0;
            this.tabControlTrees.Size = new System.Drawing.Size(290, 531);
            this.tabControlTrees.TabIndex = 11;
            // 
            // layoutProperties
            // 
            this.layoutProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutProperties.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.layoutProperties.IsSplitterFixed = true;
            this.layoutProperties.Location = new System.Drawing.Point(0, 0);
            this.layoutProperties.Margin = new System.Windows.Forms.Padding(4);
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
            this.layoutProperties.Size = new System.Drawing.Size(859, 531);
            this.layoutProperties.SplitterDistance = 79;
            this.layoutProperties.SplitterWidth = 5;
            this.layoutProperties.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 600);
            this.Controls.Add(this.layoutMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private ToolStripButton toolStripButton7;
        private ToolStripButton toolStripButton6;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton3;
        private SplitContainer layoutMain;
        private SplitContainer layoutSplitter;
        private SplitContainer layoutProperties;
        private TabControl tabControlTrees;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton2;
    }
}

