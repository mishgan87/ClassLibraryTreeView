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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.modelName = new System.Windows.Forms.Label();
            this.treeTabs = new System.Windows.Forms.TabControl();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.layoutMenu = new System.Windows.Forms.SplitContainer();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnExportPermissibleGrid = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.layoutWorkplace = new System.Windows.Forms.SplitContainer();
            this.layoutMain = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.layoutMenu)).BeginInit();
            this.layoutMenu.Panel1.SuspendLayout();
            this.layoutMenu.Panel2.SuspendLayout();
            this.layoutMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutWorkplace)).BeginInit();
            this.layoutWorkplace.Panel1.SuspendLayout();
            this.layoutWorkplace.Panel2.SuspendLayout();
            this.layoutWorkplace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).BeginInit();
            this.layoutMain.Panel1.SuspendLayout();
            this.layoutMain.Panel2.SuspendLayout();
            this.layoutMain.SuspendLayout();
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
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar.Location = new System.Drawing.Point(0, 0);
            this.progressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1052, 50);
            this.progressBar.TabIndex = 7;
            this.progressBar.Visible = false;
            // 
            // modelName
            // 
            this.modelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelName.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.modelName.Location = new System.Drawing.Point(0, 0);
            this.modelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.modelName.Name = "modelName";
            this.modelName.Size = new System.Drawing.Size(1052, 59);
            this.modelName.TabIndex = 6;
            this.modelName.Text = "Conceptual model";
            this.modelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // treeTabs
            // 
            this.treeTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeTabs.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeTabs.Location = new System.Drawing.Point(0, 0);
            this.treeTabs.Margin = new System.Windows.Forms.Padding(4);
            this.treeTabs.Name = "treeTabs";
            this.treeTabs.SelectedIndex = 0;
            this.treeTabs.Size = new System.Drawing.Size(321, 629);
            this.treeTabs.TabIndex = 11;
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControl.ItemSize = new System.Drawing.Size(0, 30);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(20, 3);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(640, 629);
            this.tabControl.TabIndex = 12;
            this.tabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabControl_DrawItem);
            // 
            // layoutMenu
            // 
            this.layoutMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutMenu.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.layoutMenu.IsSplitterFixed = true;
            this.layoutMenu.Location = new System.Drawing.Point(0, 0);
            this.layoutMenu.Margin = new System.Windows.Forms.Padding(4);
            this.layoutMenu.Name = "layoutMenu";
            // 
            // layoutMenu.Panel1
            // 
            this.layoutMenu.Panel1.Controls.Add(this.btnReport);
            this.layoutMenu.Panel1.Controls.Add(this.btnSearch);
            this.layoutMenu.Panel1.Controls.Add(this.btnExportPermissibleGrid);
            this.layoutMenu.Panel1.Controls.Add(this.btnUndo);
            this.layoutMenu.Panel1.Controls.Add(this.btnDelete);
            this.layoutMenu.Panel1.Controls.Add(this.btnAdd);
            this.layoutMenu.Panel1.Controls.Add(this.btnSave);
            this.layoutMenu.Panel1.Controls.Add(this.btnOpenFile);
            // 
            // layoutMenu.Panel2
            // 
            this.layoutMenu.Panel2.Controls.Add(this.layoutWorkplace);
            this.layoutMenu.Size = new System.Drawing.Size(1052, 629);
            this.layoutMenu.SplitterDistance = 81;
            this.layoutMenu.SplitterWidth = 5;
            this.layoutMenu.TabIndex = 13;
            // 
            // btnReport
            // 
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReport.Enabled = false;
            this.btnReport.Image = global::ClassLibraryTreeView.Properties.Resources.enumerations;
            this.btnReport.Location = new System.Drawing.Point(0, 602);
            this.btnReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(81, 86);
            this.btnReport.TabIndex = 8;
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.BtnReport_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSearch.Enabled = false;
            this.btnSearch.Image = global::ClassLibraryTreeView.Properties.Resources.search;
            this.btnSearch.Location = new System.Drawing.Point(0, 516);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(81, 86);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnExportPermissibleGrid
            // 
            this.btnExportPermissibleGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnExportPermissibleGrid.Enabled = false;
            this.btnExportPermissibleGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnExportPermissibleGrid.Image")));
            this.btnExportPermissibleGrid.Location = new System.Drawing.Point(0, 430);
            this.btnExportPermissibleGrid.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportPermissibleGrid.Name = "btnExportPermissibleGrid";
            this.btnExportPermissibleGrid.Size = new System.Drawing.Size(81, 86);
            this.btnExportPermissibleGrid.TabIndex = 3;
            this.btnExportPermissibleGrid.UseVisualStyleBackColor = true;
            this.btnExportPermissibleGrid.Click += new System.EventHandler(this.BtnExportPermissibleGrid_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUndo.Enabled = false;
            this.btnUndo.Image = ((System.Drawing.Image)(resources.GetObject("btnUndo.Image")));
            this.btnUndo.Location = new System.Drawing.Point(0, 344);
            this.btnUndo.Margin = new System.Windows.Forms.Padding(4);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(81, 86);
            this.btnUndo.TabIndex = 5;
            this.btnUndo.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(0, 258);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(81, 86);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAdd.Enabled = false;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(0, 172);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(81, 86);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSave.Enabled = false;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(0, 86);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 86);
            this.btnSave.TabIndex = 4;
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOpenFile.Image = global::ClassLibraryTreeView.Properties.Resources.open;
            this.btnOpenFile.Location = new System.Drawing.Point(0, 0);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(81, 86);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.BtnOpenFile_Click);
            // 
            // layoutWorkplace
            // 
            this.layoutWorkplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutWorkplace.Location = new System.Drawing.Point(0, 0);
            this.layoutWorkplace.Margin = new System.Windows.Forms.Padding(4);
            this.layoutWorkplace.Name = "layoutWorkplace";
            // 
            // layoutWorkplace.Panel1
            // 
            this.layoutWorkplace.Panel1.Controls.Add(this.treeTabs);
            // 
            // layoutWorkplace.Panel2
            // 
            this.layoutWorkplace.Panel2.Controls.Add(this.tabControl);
            this.layoutWorkplace.Size = new System.Drawing.Size(966, 629);
            this.layoutWorkplace.SplitterDistance = 321;
            this.layoutWorkplace.SplitterWidth = 5;
            this.layoutWorkplace.TabIndex = 14;
            // 
            // layoutMain
            // 
            this.layoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.layoutMain.IsSplitterFixed = true;
            this.layoutMain.Location = new System.Drawing.Point(0, 50);
            this.layoutMain.Margin = new System.Windows.Forms.Padding(4);
            this.layoutMain.Name = "layoutMain";
            this.layoutMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // layoutMain.Panel1
            // 
            this.layoutMain.Panel1.Controls.Add(this.modelName);
            // 
            // layoutMain.Panel2
            // 
            this.layoutMain.Panel2.Controls.Add(this.layoutMenu);
            this.layoutMain.Size = new System.Drawing.Size(1052, 693);
            this.layoutMain.SplitterDistance = 59;
            this.layoutMain.SplitterWidth = 5;
            this.layoutMain.TabIndex = 14;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 743);
            this.Controls.Add(this.layoutMain);
            this.Controls.Add(this.progressBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "CMViewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.layoutMenu.Panel1.ResumeLayout(false);
            this.layoutMenu.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutMenu)).EndInit();
            this.layoutMenu.ResumeLayout(false);
            this.layoutWorkplace.Panel1.ResumeLayout(false);
            this.layoutWorkplace.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutWorkplace)).EndInit();
            this.layoutWorkplace.ResumeLayout(false);
            this.layoutMain.Panel1.ResumeLayout(false);
            this.layoutMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutMain)).EndInit();
            this.layoutMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ToolStripLabel labelModelName;
        private ToolStripLabel labelInfo;
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
        private ProgressBar progressBar;
        private TabControl tabControl;
        private SplitContainer layoutMenu;
        private SplitContainer layoutWorkplace;
        private Label modelName;
        private SplitContainer layoutMain;
    }
}

