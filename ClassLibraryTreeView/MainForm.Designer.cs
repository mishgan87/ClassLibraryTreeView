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
            this.propertiesTabs = new System.Windows.Forms.TabControl();
            this.labelModelName = new System.Windows.Forms.ToolStripLabel();
            this.labelInfo = new System.Windows.Forms.ToolStripLabel();
            this.layoutMain = new System.Windows.Forms.SplitContainer();
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
            this.layoutProperties = new System.Windows.Forms.SplitContainer();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.info = new System.Windows.Forms.Label();
            this.btnReport = new System.Windows.Forms.Button();
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
            // propertiesTabs
            // 
            this.propertiesTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesTabs.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propertiesTabs.Location = new System.Drawing.Point(0, 0);
            this.propertiesTabs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.propertiesTabs.Name = "propertiesTabs";
            this.propertiesTabs.SelectedIndex = 0;
            this.propertiesTabs.Size = new System.Drawing.Size(876, 361);
            this.propertiesTabs.TabIndex = 0;
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
            this.layoutMain.Margin = new System.Windows.Forms.Padding(4);
            this.layoutMain.Name = "layoutMain";
            this.layoutMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // layoutMain.Panel1
            // 
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
            this.layoutMain.Size = new System.Drawing.Size(1172, 497);
            this.layoutMain.SplitterDistance = 64;
            this.layoutMain.SplitterWidth = 5;
            this.layoutMain.TabIndex = 9;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(600, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(572, 64);
            this.progressBar.TabIndex = 7;
            this.progressBar.Visible = false;
            // 
            // modelName
            // 
            this.modelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelName.Location = new System.Drawing.Point(600, 0);
            this.modelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.modelName.Name = "modelName";
            this.modelName.Size = new System.Drawing.Size(572, 64);
            this.modelName.TabIndex = 6;
            this.modelName.Text = "Conceptual model";
            this.modelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnUndo
            // 
            this.btnUndo.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnUndo.Enabled = false;
            this.btnUndo.Image = ((System.Drawing.Image)(resources.GetObject("btnUndo.Image")));
            this.btnUndo.Location = new System.Drawing.Point(500, 0);
            this.btnUndo.Margin = new System.Windows.Forms.Padding(4);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(100, 64);
            this.btnUndo.TabIndex = 5;
            this.btnUndo.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(400, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 64);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAdd.Enabled = false;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(300, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 64);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.Enabled = false;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(200, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 64);
            this.btnSave.TabIndex = 4;
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnExportPermissibleGrid
            // 
            this.btnExportPermissibleGrid.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExportPermissibleGrid.Enabled = false;
            this.btnExportPermissibleGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnExportPermissibleGrid.Image")));
            this.btnExportPermissibleGrid.Location = new System.Drawing.Point(100, 0);
            this.btnExportPermissibleGrid.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportPermissibleGrid.Name = "btnExportPermissibleGrid";
            this.btnExportPermissibleGrid.Size = new System.Drawing.Size(100, 64);
            this.btnExportPermissibleGrid.TabIndex = 3;
            this.btnExportPermissibleGrid.UseVisualStyleBackColor = true;
            this.btnExportPermissibleGrid.Click += new System.EventHandler(this.BtnExportPermissibleGrid_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.Image")));
            this.btnOpenFile.Location = new System.Drawing.Point(0, 0);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(100, 64);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.BtnOpenFile_Click);
            // 
            // layoutSplitter
            // 
            this.layoutSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutSplitter.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.layoutSplitter.Location = new System.Drawing.Point(0, 0);
            this.layoutSplitter.Margin = new System.Windows.Forms.Padding(4);
            this.layoutSplitter.Name = "layoutSplitter";
            // 
            // layoutSplitter.Panel1
            // 
            this.layoutSplitter.Panel1.Controls.Add(this.treeTabs);
            // 
            // layoutSplitter.Panel2
            // 
            this.layoutSplitter.Panel2.Controls.Add(this.layoutProperties);
            this.layoutSplitter.Size = new System.Drawing.Size(1172, 428);
            this.layoutSplitter.SplitterDistance = 291;
            this.layoutSplitter.SplitterWidth = 5;
            this.layoutSplitter.TabIndex = 12;
            // 
            // treeTabs
            // 
            this.treeTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeTabs.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeTabs.Location = new System.Drawing.Point(0, 0);
            this.treeTabs.Margin = new System.Windows.Forms.Padding(4);
            this.treeTabs.Name = "treeTabs";
            this.treeTabs.SelectedIndex = 0;
            this.treeTabs.Size = new System.Drawing.Size(291, 428);
            this.treeTabs.TabIndex = 11;
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
            this.layoutProperties.Panel1.Controls.Add(this.button2);
            this.layoutProperties.Panel1.Controls.Add(this.button1);
            this.layoutProperties.Panel1.Controls.Add(this.info);
            this.layoutProperties.Panel1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            // 
            // layoutProperties.Panel2
            // 
            this.layoutProperties.Panel2.Controls.Add(this.propertiesTabs);
            this.layoutProperties.Size = new System.Drawing.Size(876, 428);
            this.layoutProperties.SplitterDistance = 62;
            this.layoutProperties.SplitterWidth = 5;
            this.layoutProperties.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.Image = global::ClassLibraryTreeView.Properties.Resources.add;
            this.button2.Location = new System.Drawing.Point(676, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 62);
            this.button2.TabIndex = 8;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(776, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 62);
            this.button1.TabIndex = 8;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // info
            // 
            this.info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.info.Location = new System.Drawing.Point(0, 0);
            this.info.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(876, 62);
            this.info.TabIndex = 6;
            this.info.Text = "Info";
            this.info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnReport
            // 
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnReport.Enabled = false;
            this.btnReport.Image = global::ClassLibraryTreeView.Properties.Resources.enumerations;
            this.btnReport.Location = new System.Drawing.Point(600, 0);
            this.btnReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(100, 64);
            this.btnReport.TabIndex = 8;
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.BtnReport_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 497);
            this.Controls.Add(this.layoutMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
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
            this.layoutProperties.Panel1.ResumeLayout(false);
            this.layoutProperties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutProperties)).EndInit();
            this.layoutProperties.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl propertiesTabs;
        private ToolStripLabel labelModelName;
        private ToolStripLabel labelInfo;
        private SplitContainer layoutMain;
        private SplitContainer layoutSplitter;
        private SplitContainer layoutProperties;
        private TabControl treeTabs;
        private Button btnSave;
        private Button btnExportPermissibleGrid;
        private Button btnOpenFile;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnUndo;
        private Label info;
        private Label modelName;
        private ProgressBar progressBar;
        private Button button2;
        private Button button1;
        private Button btnReport;
    }
}

