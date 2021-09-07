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
            this.tabControlProperties = new System.Windows.Forms.TabControl();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripTabs = new System.Windows.Forms.ToolStrip();
            this.splitContainerProperties = new System.Windows.Forms.SplitContainer();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.buttonClasses = new System.Windows.Forms.ToolStripButton();
            this.buttonAttributes = new System.Windows.Forms.ToolStripButton();
            this.buttonMeasure = new System.Windows.Forms.ToolStripButton();
            this.buttonTaxonomies = new System.Windows.Forms.ToolStripButton();
            this.buttonEnumerations = new System.Windows.Forms.ToolStripButton();
            this.buttonOpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExportPermissibleGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripMenu.SuspendLayout();
            this.toolStripTabs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerProperties)).BeginInit();
            this.splitContainerProperties.Panel1.SuspendLayout();
            this.splitContainerProperties.Panel2.SuspendLayout();
            this.splitContainerProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlProperties.Location = new System.Drawing.Point(0, 0);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(632, 722);
            this.tabControlProperties.TabIndex = 0;
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonOpenFile,
            this.toolStripButtonExportPermissibleGrid,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStripMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.MaximumSize = new System.Drawing.Size(65535, 65535);
            this.toolStripMenu.MinimumSize = new System.Drawing.Size(64, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(73, 722);
            this.toolStripMenu.Stretch = true;
            this.toolStripMenu.TabIndex = 1;
            this.toolStripMenu.Text = "Main Menu";
            // 
            // toolStripTabs
            // 
            this.toolStripTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripTabs.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripTabs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonClasses,
            this.buttonAttributes,
            this.buttonMeasure,
            this.buttonTaxonomies,
            this.buttonEnumerations});
            this.toolStripTabs.Location = new System.Drawing.Point(0, 0);
            this.toolStripTabs.MaximumSize = new System.Drawing.Size(65535, 65535);
            this.toolStripTabs.MinimumSize = new System.Drawing.Size(0, 55);
            this.toolStripTabs.Name = "toolStripTabs";
            this.toolStripTabs.Size = new System.Drawing.Size(354, 72);
            this.toolStripTabs.TabIndex = 3;
            this.toolStripTabs.Text = "Menu";
            // 
            // splitContainerProperties
            // 
            this.splitContainerProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerProperties.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerProperties.IsSplitterFixed = true;
            this.splitContainerProperties.Location = new System.Drawing.Point(0, 0);
            this.splitContainerProperties.Name = "splitContainerProperties";
            // 
            // splitContainerProperties.Panel1
            // 
            this.splitContainerProperties.Panel1.Controls.Add(this.toolStripMenu);
            this.splitContainerProperties.Panel1MinSize = 70;
            // 
            // splitContainerProperties.Panel2
            // 
            this.splitContainerProperties.Panel2.Controls.Add(this.tabControlProperties);
            this.splitContainerProperties.Size = new System.Drawing.Size(709, 722);
            this.splitContainerProperties.SplitterDistance = 73;
            this.splitContainerProperties.TabIndex = 7;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainer);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerProperties);
            this.splitContainerMain.Size = new System.Drawing.Size(1067, 722);
            this.splitContainerMain.SplitterDistance = 354;
            this.splitContainerMain.TabIndex = 8;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.toolStripTabs);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.splitContainer.Size = new System.Drawing.Size(354, 722);
            this.splitContainer.SplitterDistance = 72;
            this.splitContainer.TabIndex = 4;
            // 
            // buttonClasses
            // 
            this.buttonClasses.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonClasses.Image = global::ClassLibraryTreeView.Properties.Resources.classes;
            this.buttonClasses.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonClasses.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonClasses.Name = "buttonClasses";
            this.buttonClasses.Size = new System.Drawing.Size(52, 69);
            this.buttonClasses.ToolTipText = "Classes";
            this.buttonClasses.Click += new System.EventHandler(this.ButtonClasses_Click);
            // 
            // buttonAttributes
            // 
            this.buttonAttributes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAttributes.Image = global::ClassLibraryTreeView.Properties.Resources.attributes;
            this.buttonAttributes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAttributes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAttributes.Name = "buttonAttributes";
            this.buttonAttributes.Size = new System.Drawing.Size(52, 69);
            this.buttonAttributes.ToolTipText = "Attributes";
            this.buttonAttributes.Click += new System.EventHandler(this.ButtonAttributes_Click);
            // 
            // buttonMeasure
            // 
            this.buttonMeasure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonMeasure.Image = global::ClassLibraryTreeView.Properties.Resources.measure;
            this.buttonMeasure.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonMeasure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonMeasure.Name = "buttonMeasure";
            this.buttonMeasure.Size = new System.Drawing.Size(52, 69);
            this.buttonMeasure.ToolTipText = "Measure";
            this.buttonMeasure.Click += new System.EventHandler(this.ButtonMeasure_Click);
            // 
            // buttonTaxonomies
            // 
            this.buttonTaxonomies.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonTaxonomies.Image = global::ClassLibraryTreeView.Properties.Resources.taxonomies;
            this.buttonTaxonomies.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonTaxonomies.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonTaxonomies.Name = "buttonTaxonomies";
            this.buttonTaxonomies.Size = new System.Drawing.Size(52, 69);
            this.buttonTaxonomies.ToolTipText = "Taxonomies";
            this.buttonTaxonomies.Click += new System.EventHandler(this.ButtonTaxonomies_Click);
            // 
            // buttonEnumerations
            // 
            this.buttonEnumerations.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEnumerations.Image = global::ClassLibraryTreeView.Properties.Resources.enumerations;
            this.buttonEnumerations.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonEnumerations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEnumerations.Name = "buttonEnumerations";
            this.buttonEnumerations.Size = new System.Drawing.Size(52, 69);
            this.buttonEnumerations.ToolTipText = "Enumerations";
            this.buttonEnumerations.Click += new System.EventHandler(this.ButtonEnumerations_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonOpenFile.Image = global::ClassLibraryTreeView.Properties.Resources.open;
            this.buttonOpenFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(71, 52);
            this.buttonOpenFile.Text = "Open File";
            this.buttonOpenFile.Click += new System.EventHandler(this.OpenFile);
            // 
            // toolStripButtonExportPermissibleGrid
            // 
            this.toolStripButtonExportPermissibleGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExportPermissibleGrid.Image = global::ClassLibraryTreeView.Properties.Resources.excel;
            this.toolStripButtonExportPermissibleGrid.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonExportPermissibleGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExportPermissibleGrid.Name = "toolStripButtonExportPermissibleGrid";
            this.toolStripButtonExportPermissibleGrid.Size = new System.Drawing.Size(71, 52);
            this.toolStripButtonExportPermissibleGrid.Text = "Export Permissible Grid";
            this.toolStripButtonExportPermissibleGrid.Click += new System.EventHandler(this.ExportPermissibleGrid);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::ClassLibraryTreeView.Properties.Resources.upload;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(71, 52);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::ClassLibraryTreeView.Properties.Resources.search;
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(71, 52);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 722);
            this.Controls.Add(this.splitContainerMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "ClassLibraryView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.toolStripTabs.ResumeLayout(false);
            this.toolStripTabs.PerformLayout();
            this.splitContainerProperties.Panel1.ResumeLayout(false);
            this.splitContainerProperties.Panel1.PerformLayout();
            this.splitContainerProperties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerProperties)).EndInit();
            this.splitContainerProperties.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton buttonOpenFile;
        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.ToolStripButton toolStripButtonExportPermissibleGrid;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStrip toolStripTabs;
        private ToolStripButton buttonClasses;
        private ToolStripButton buttonAttributes;
        private ToolStripButton buttonMeasure;
        private ToolStripButton buttonTaxonomies;
        private ToolStripButton buttonEnumerations;
        private SplitContainer splitContainerProperties;
        private SplitContainer splitContainerMain;
        private SplitContainer splitContainer;
    }
}

