﻿namespace PandoraImager
{
    partial class formMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMain));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsddProject = new System.Windows.Forms.ToolStripDropDownButton();
            this.cbNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cbLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.cbSave = new System.Windows.Forms.ToolStripMenuItem();
            this.cbSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddUtility = new System.Windows.Forms.ToolStripDropDownButton();
            this.cbAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cbQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddImage = new System.Windows.Forms.ToolStripDropDownButton();
            this.cbImageAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cbImageRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.cbImageModify = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabImages = new System.Windows.Forms.TabPage();
            this.lvImages = new System.Windows.Forms.ListView();
            this.chID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chOffset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBlocks = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabBootloader = new System.Windows.Forms.TabPage();
            this.toolStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabImages.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddProject,
            this.tsddUtility,
            this.tsddImage});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(586, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsddProject
            // 
            this.tsddProject.AutoToolTip = false;
            this.tsddProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbNew,
            this.toolStripSeparator1,
            this.cbLoad,
            this.cbSave,
            this.cbSaveAs});
            this.tsddProject.Image = ((System.Drawing.Image)(resources.GetObject("tsddProject.Image")));
            this.tsddProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddProject.Name = "tsddProject";
            this.tsddProject.Size = new System.Drawing.Size(57, 22);
            this.tsddProject.Text = "&Project";
            // 
            // cbNew
            // 
            this.cbNew.Image = global::PandoraImager.Properties.Resources._new;
            this.cbNew.Name = "cbNew";
            this.cbNew.Size = new System.Drawing.Size(152, 22);
            this.cbNew.Text = "&New";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // cbLoad
            // 
            this.cbLoad.Image = global::PandoraImager.Properties.Resources.open;
            this.cbLoad.Name = "cbLoad";
            this.cbLoad.Size = new System.Drawing.Size(152, 22);
            this.cbLoad.Text = "&Load";
            this.cbLoad.Click += new System.EventHandler(this.cbLoad_Click);
            // 
            // cbSave
            // 
            this.cbSave.Image = global::PandoraImager.Properties.Resources.save;
            this.cbSave.Name = "cbSave";
            this.cbSave.Size = new System.Drawing.Size(152, 22);
            this.cbSave.Text = "&Save";
            // 
            // cbSaveAs
            // 
            this.cbSaveAs.Image = global::PandoraImager.Properties.Resources.save_as;
            this.cbSaveAs.Name = "cbSaveAs";
            this.cbSaveAs.Size = new System.Drawing.Size(152, 22);
            this.cbSaveAs.Text = "Save &as";
            // 
            // tsddUtility
            // 
            this.tsddUtility.AutoToolTip = false;
            this.tsddUtility.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddUtility.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbAbout,
            this.toolStripSeparator3,
            this.cbQuit});
            this.tsddUtility.Image = ((System.Drawing.Image)(resources.GetObject("tsddUtility.Image")));
            this.tsddUtility.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddUtility.Name = "tsddUtility";
            this.tsddUtility.Size = new System.Drawing.Size(51, 22);
            this.tsddUtility.Text = "&Utility";
            // 
            // cbAbout
            // 
            this.cbAbout.Image = global::PandoraImager.Properties.Resources.about;
            this.cbAbout.Name = "cbAbout";
            this.cbAbout.Size = new System.Drawing.Size(107, 22);
            this.cbAbout.Text = "&About";
            this.cbAbout.Click += new System.EventHandler(this.cbAbout_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(104, 6);
            // 
            // cbQuit
            // 
            this.cbQuit.Image = global::PandoraImager.Properties.Resources.exit;
            this.cbQuit.Name = "cbQuit";
            this.cbQuit.Size = new System.Drawing.Size(107, 22);
            this.cbQuit.Text = "&Quit";
            // 
            // tsddImage
            // 
            this.tsddImage.AutoToolTip = false;
            this.tsddImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbImageAdd,
            this.cbImageRemove,
            this.cbImageModify});
            this.tsddImage.Enabled = false;
            this.tsddImage.Image = ((System.Drawing.Image)(resources.GetObject("tsddImage.Image")));
            this.tsddImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddImage.Name = "tsddImage";
            this.tsddImage.Size = new System.Drawing.Size(53, 22);
            this.tsddImage.Text = "&Image";
            // 
            // cbImageAdd
            // 
            this.cbImageAdd.Image = global::PandoraImager.Properties.Resources.add;
            this.cbImageAdd.Name = "cbImageAdd";
            this.cbImageAdd.Size = new System.Drawing.Size(117, 22);
            this.cbImageAdd.Text = "&Add";
            // 
            // cbImageRemove
            // 
            this.cbImageRemove.Image = global::PandoraImager.Properties.Resources.remove;
            this.cbImageRemove.Name = "cbImageRemove";
            this.cbImageRemove.Size = new System.Drawing.Size(117, 22);
            this.cbImageRemove.Text = "&Remove";
            // 
            // cbImageModify
            // 
            this.cbImageModify.Image = global::PandoraImager.Properties.Resources.modify;
            this.cbImageModify.Name = "cbImageModify";
            this.cbImageModify.Size = new System.Drawing.Size(117, 22);
            this.cbImageModify.Text = "&Modify";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabImages);
            this.tabControl.Controls.Add(this.tabBootloader);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 25);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(586, 350);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabImages
            // 
            this.tabImages.Controls.Add(this.lvImages);
            this.tabImages.Location = new System.Drawing.Point(4, 22);
            this.tabImages.Name = "tabImages";
            this.tabImages.Padding = new System.Windows.Forms.Padding(3);
            this.tabImages.Size = new System.Drawing.Size(578, 324);
            this.tabImages.TabIndex = 0;
            this.tabImages.Text = "Images";
            this.tabImages.UseVisualStyleBackColor = true;
            // 
            // lvImages
            // 
            this.lvImages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chName,
            this.chType,
            this.chOffset,
            this.chBlocks,
            this.chBytes});
            this.lvImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvImages.FullRowSelect = true;
            this.lvImages.GridLines = true;
            this.lvImages.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvImages.Location = new System.Drawing.Point(3, 3);
            this.lvImages.MultiSelect = false;
            this.lvImages.Name = "lvImages";
            this.lvImages.Size = new System.Drawing.Size(572, 318);
            this.lvImages.TabIndex = 0;
            this.lvImages.UseCompatibleStateImageBehavior = false;
            this.lvImages.View = System.Windows.Forms.View.Details;
            this.lvImages.SelectedIndexChanged += new System.EventHandler(this.lvImages_SelectedIndexChanged);
            // 
            // chID
            // 
            this.chID.Text = "#";
            this.chID.Width = 25;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 183;
            // 
            // chType
            // 
            this.chType.Text = "Type";
            this.chType.Width = 92;
            // 
            // chOffset
            // 
            this.chOffset.Text = "Offset";
            this.chOffset.Width = 76;
            // 
            // chBlocks
            // 
            this.chBlocks.Text = "Blocks";
            this.chBlocks.Width = 78;
            // 
            // chBytes
            // 
            this.chBytes.Text = "Bytes";
            this.chBytes.Width = 114;
            // 
            // tabBootloader
            // 
            this.tabBootloader.Location = new System.Drawing.Point(4, 22);
            this.tabBootloader.Name = "tabBootloader";
            this.tabBootloader.Padding = new System.Windows.Forms.Padding(3);
            this.tabBootloader.Size = new System.Drawing.Size(578, 324);
            this.tabBootloader.TabIndex = 1;
            this.tabBootloader.Text = "Bootloader";
            this.tabBootloader.UseVisualStyleBackColor = true;
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 375);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formMain";
            this.Text = "PandoraImager";
            this.Load += new System.EventHandler(this.formMain_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabImages.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripDropDownButton tsddUtility;
        private System.Windows.Forms.ToolStripMenuItem cbAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cbQuit;
        private System.Windows.Forms.ToolStripDropDownButton tsddProject;
        private System.Windows.Forms.ToolStripMenuItem cbNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cbLoad;
        private System.Windows.Forms.ToolStripMenuItem cbSave;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabImages;
        private System.Windows.Forms.TabPage tabBootloader;
        private System.Windows.Forms.ToolStripDropDownButton tsddImage;
        private System.Windows.Forms.ToolStripMenuItem cbImageAdd;
        private System.Windows.Forms.ToolStripMenuItem cbImageRemove;
        private System.Windows.Forms.ToolStripMenuItem cbImageModify;
        private System.Windows.Forms.ListView lvImages;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chOffset;
        private System.Windows.Forms.ColumnHeader chBlocks;
        private System.Windows.Forms.ColumnHeader chBytes;
        private System.Windows.Forms.ToolStripMenuItem cbSaveAs;
    }
}