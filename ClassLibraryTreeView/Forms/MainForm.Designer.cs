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
            this.labelModelName = new System.Windows.Forms.ToolStripLabel();
            this.labelInfo = new System.Windows.Forms.ToolStripLabel();
            this.layoutMain = new System.Windows.Forms.SplitContainer();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.modelName = new System.Windows.Forms.Label();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExportPermissibleGrid = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.layoutSplitter = new System.Windows.Forms.SplitContainer();
            this.treeTabs = new System.Windows.Forms.TabControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabControl = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).BeginInit();
            this.layoutMain.Panel1.SuspendLayout();
            this.layoutMain.Panel2.SuspendLayout();
            this.layoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutSplitter)).BeginInit();
            this.layoutSplitter.Panel1.SuspendLayout();
            this.layoutSplitter.Panel2.SuspendLayout();
            this.layoutSplitter.SuspendLayout();
            this.SuspendLayout();
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
            // labelInfo
            // 
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(41, 76);
            this.labelInfo.Text = "Info";
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
            this.layoutMain.Panel1.Controls.Add(this.btnSearch);
            this.layoutMain.Panel1.Controls.Add(this.btnReport);
            this.layoutMain.Panel1.Controls.Add(this.progressBar);
            this.layoutMain.Panel1.Controls.Add(this.modelName);
            this.layoutMain.Panel1.Controls.Add(this.btnUndo);
            this.layoutMain.Panel1.Controls.Add(this.btnDelete);
            this.layoutMain.Panel1.Controls.Add(this.btnAdd);
            this.layoutMain.Panel1.Controls.Add(this.btnSave);
            this.layoutMain.Panel1.Controls.Add(this.btnExportPermissibleGrid);
            this.layoutMain.Panel1.Controls.Add(this.btnOpenFile);
            this.layoutMain.Panel1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            // 
            // layoutMain.Panel2
            // 
            this.layoutMain.Panel2.Controls.Add(this.layoutSplitter);
            this.layoutMain.Size = new System.Drawing.Size(1411, 750);
            this.layoutMain.SplitterDistance = 70;
            this.layoutMain.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSearch.Enabled = false;
            this.btnSearch.Image = global::ClassLibraryTreeView.Properties.Resources.search;
            this.btnSearch.Location = new System.Drawing.Point(525, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 70);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnReport
            // 
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnReport.Enabled = false;
            this.btnReport.Image = global::ClassLibraryTreeView.Properties.Resources.enumerations;
            this.btnReport.Location = new System.Drawing.Point(450, 0);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 70);
            this.btnReport.TabIndex = 8;
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.BtnReport_Click);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(450, 0);
            this.progressBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(961, 70);
            this.progressBar.TabIndex = 7;
            this.progressBar.Visible = false;
            // 
            // modelName
            // 
            this.modelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelName.Location = new System.Drawing.Point(450, 0);
            this.modelName.Name = "modelName";
            this.modelName.Size = new System.Drawing.Size(961, 70);
            this.modelName.TabIndex = 6;
            this.modelName.Text = "Conceptual model";
            this.modelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnUndo
            // 
            this.btnUndo.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnUndo.Enabled = false;
            this.btnUndo.Image = ((System.Drawing.Image)(resources.GetObject("btnUndo.Image")));
            this.btnUndo.Location = new System.Drawing.Point(375, 0);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 70);
            this.btnUndo.TabIndex = 5;
            this.btnUndo.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(300, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 70);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAdd.Enabled = false;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(225, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 70);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.Enabled = false;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(150, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 70);
            this.btnSave.TabIndex = 4;
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnExportPermissibleGrid
            // 
            this.btnExportPermissibleGrid.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExportPermissibleGrid.Enabled = false;
            this.btnExportPermissibleGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnExportPermissibleGrid.Image")));
            this.btnExportPermissibleGrid.Location = new System.Drawing.Point(75, 0);
            this.btnExportPermissibleGrid.Name = "btnExportPermissibleGrid";
            this.btnExportPermissibleGrid.Size = new System.Drawing.Size(75, 70);
            this.btnExportPermissibleGrid.TabIndex = 3;
            this.btnExportPermissibleGrid.UseVisualStyleBackColor = true;
            this.btnExportPermissibleGrid.Click += new System.EventHandler(this.BtnExportPermissibleGrid_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.Image")));
            this.btnOpenFile.Location = new System.Drawing.Point(0, 0);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 70);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.BtnOpenFile_Click);
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
            this.layoutSplitter.Panel1.Controls.Add(this.treeTabs);
            // 
            // layoutSplitter.Panel2
            // 
            this.layoutSplitter.Panel2.Controls.Add(this.tabControl);
            this.layoutSplitter.Size = new System.Drawing.Size(1411, 676);
            this.layoutSplitter.SplitterDistance = 337;
            this.layoutSplitter.TabIndex = 12;
            // 
            // treeTabs
            // 
            this.treeTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeTabs.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeTabs.Location = new System.Drawing.Point(0, 0);
            this.treeTabs.Name = "treeTabs";
            this.treeTabs.SelectedIndex = 0;
            this.treeTabs.Size = new System.Drawing.Size(337, 676);
            this.treeTabs.TabIndex = 11;
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1070, 676);
            this.tabControl.TabIndex = 12;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1411, 750);
            this.Controls.Add(this.layoutMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "CMViewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.layoutMain.Panel1.ResumeLayout(false);
            this.layoutMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).EndInit();
            this.layoutMain.ResumeLayout(false);
            this.layoutSplitter.Panel1.ResumeLayout(false);
            this.layoutSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutSplitter)).EndInit();
            this.layoutSplitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ToolStripLabel labelModelName;
        private ToolStripLabel labelInfo;
        private SplitContainer layoutMain;
        private SplitContainer layoutSplitter;
        private TabControl treeTabs;
        private Button btnSave;
        private Button btnExportPermissibleGrid;
        private Button btnOpenFile;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnUndo;
        private Button btnReport;
        private Button btnSearch;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label modelName;
        private ProgressBar progressBar;
        private TabControl tabControl;
    }
}

