namespace PandoraImager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMain));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsddProject = new System.Windows.Forms.ToolStripDropDownButton();
            this.cbNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cbLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.cbSave = new System.Windows.Forms.ToolStripMenuItem();
            this.cbSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddImage = new System.Windows.Forms.ToolStripDropDownButton();
            this.cbImageAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cbImageRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.cbModify = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.cbImageImport = new System.Windows.Forms.ToolStripMenuItem();
            this.cbImageExport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cbImageUp = new System.Windows.Forms.ToolStripMenuItem();
            this.cbImageDown = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddUtility = new System.Windows.Forms.ToolStripDropDownButton();
            this.cbAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cbQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabImages = new System.Windows.Forms.TabPage();
            this.lvImages = new System.Windows.Forms.ListView();
            this.chIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBlocks = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chExecAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.tabBootloader = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblWarning = new System.Windows.Forms.Label();
            this.lblExecAddr = new System.Windows.Forms.Label();
            this.lblCLKQSDS = new System.Windows.Forms.Label();
            this.lblDRAMSize = new System.Windows.Forms.Label();
            this.lblDQSODS = new System.Windows.Forms.Label();
            this.ndDRAMSize = new System.Windows.Forms.NumericUpDown();
            this.ndExecAddr = new System.Windows.Forms.NumericUpDown();
            this.ndDQSODS = new System.Windows.Forms.NumericUpDown();
            this.ndCLKQSDS = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabImages.SuspendLayout();
            this.tabBootloader.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndDRAMSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndExecAddr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndDQSODS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndCLKQSDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddProject,
            this.tsddImage,
            this.tsddUtility});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(504, 25);
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
            this.cbNew.Size = new System.Drawing.Size(112, 22);
            this.cbNew.Text = "&New";
            this.cbNew.Click += new System.EventHandler(this.cbNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
            // 
            // cbLoad
            // 
            this.cbLoad.Image = global::PandoraImager.Properties.Resources.open;
            this.cbLoad.Name = "cbLoad";
            this.cbLoad.Size = new System.Drawing.Size(112, 22);
            this.cbLoad.Text = "&Load";
            this.cbLoad.Click += new System.EventHandler(this.cbLoad_Click);
            // 
            // cbSave
            // 
            this.cbSave.Image = global::PandoraImager.Properties.Resources.save;
            this.cbSave.Name = "cbSave";
            this.cbSave.Size = new System.Drawing.Size(112, 22);
            this.cbSave.Text = "&Save";
            this.cbSave.Click += new System.EventHandler(this.cbSave_Click);
            // 
            // cbSaveAs
            // 
            this.cbSaveAs.Image = global::PandoraImager.Properties.Resources.save_as;
            this.cbSaveAs.Name = "cbSaveAs";
            this.cbSaveAs.Size = new System.Drawing.Size(112, 22);
            this.cbSaveAs.Text = "Save &as";
            this.cbSaveAs.Click += new System.EventHandler(this.cbSaveAs_Click);
            // 
            // tsddImage
            // 
            this.tsddImage.AutoToolTip = false;
            this.tsddImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsddImage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbImageAdd,
            this.toolStripSeparator2,
            this.cbImageRemove,
            this.cbModify,
            this.toolStripSeparator5,
            this.cbImageImport,
            this.cbImageExport,
            this.toolStripSeparator4,
            this.cbImageUp,
            this.cbImageDown});
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
            this.cbImageAdd.Size = new System.Drawing.Size(152, 22);
            this.cbImageAdd.Text = "&Add";
            this.cbImageAdd.Click += new System.EventHandler(this.cbImageAdd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // cbImageRemove
            // 
            this.cbImageRemove.Image = global::PandoraImager.Properties.Resources.remove;
            this.cbImageRemove.Name = "cbImageRemove";
            this.cbImageRemove.Size = new System.Drawing.Size(152, 22);
            this.cbImageRemove.Text = "&Remove";
            this.cbImageRemove.Click += new System.EventHandler(this.cbImageRemove_Click);
            // 
            // cbModify
            // 
            this.cbModify.Image = global::PandoraImager.Properties.Resources.modify;
            this.cbModify.Name = "cbModify";
            this.cbModify.Size = new System.Drawing.Size(152, 22);
            this.cbModify.Text = "&Modify";
            this.cbModify.Visible = false;
            this.cbModify.Click += new System.EventHandler(this.cbModify_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // cbImageImport
            // 
            this.cbImageImport.Image = global::PandoraImager.Properties.Resources.open;
            this.cbImageImport.Name = "cbImageImport";
            this.cbImageImport.Size = new System.Drawing.Size(152, 22);
            this.cbImageImport.Text = "Re&place data";
            this.cbImageImport.Visible = false;
            this.cbImageImport.Click += new System.EventHandler(this.cbImageImport_Click);
            // 
            // cbImageExport
            // 
            this.cbImageExport.Image = global::PandoraImager.Properties.Resources.save_as;
            this.cbImageExport.Name = "cbImageExport";
            this.cbImageExport.Size = new System.Drawing.Size(152, 22);
            this.cbImageExport.Text = "E&xport data";
            this.cbImageExport.Click += new System.EventHandler(this.cbImageExport_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // cbImageUp
            // 
            this.cbImageUp.Image = global::PandoraImager.Properties.Resources.up;
            this.cbImageUp.Name = "cbImageUp";
            this.cbImageUp.Size = new System.Drawing.Size(152, 22);
            this.cbImageUp.Text = "Move &up";
            this.cbImageUp.Click += new System.EventHandler(this.cbImageUp_Click);
            // 
            // cbImageDown
            // 
            this.cbImageDown.Image = global::PandoraImager.Properties.Resources.down;
            this.cbImageDown.Name = "cbImageDown";
            this.cbImageDown.Size = new System.Drawing.Size(152, 22);
            this.cbImageDown.Text = "Move &down";
            this.cbImageDown.Click += new System.EventHandler(this.cbImageDown_Click);
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
            this.cbQuit.Click += new System.EventHandler(this.cbQuit_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabImages);
            this.tabControl.Controls.Add(this.tabBootloader);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 25);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(504, 316);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabImages
            // 
            this.tabImages.Controls.Add(this.lvImages);
            this.tabImages.Location = new System.Drawing.Point(4, 22);
            this.tabImages.Name = "tabImages";
            this.tabImages.Padding = new System.Windows.Forms.Padding(3);
            this.tabImages.Size = new System.Drawing.Size(496, 290);
            this.tabImages.TabIndex = 0;
            this.tabImages.Text = "Images";
            this.tabImages.UseVisualStyleBackColor = true;
            // 
            // lvImages
            // 
            this.lvImages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chIcon,
            this.chID,
            this.chName,
            this.chType,
            this.chBlocks,
            this.chBytes,
            this.chExecAddr});
            this.lvImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvImages.FullRowSelect = true;
            this.lvImages.GridLines = true;
            this.lvImages.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvImages.Location = new System.Drawing.Point(3, 3);
            this.lvImages.MultiSelect = false;
            this.lvImages.Name = "lvImages";
            this.lvImages.Size = new System.Drawing.Size(490, 284);
            this.lvImages.SmallImageList = this.ilIcons;
            this.lvImages.TabIndex = 2;
            this.lvImages.UseCompatibleStateImageBehavior = false;
            this.lvImages.View = System.Windows.Forms.View.Details;
            this.lvImages.SelectedIndexChanged += new System.EventHandler(this.lvImages_SelectedIndexChanged);
            // 
            // chIcon
            // 
            this.chIcon.Text = "";
            this.chIcon.Width = 23;
            // 
            // chID
            // 
            this.chID.Text = "#";
            this.chID.Width = 25;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 115;
            // 
            // chType
            // 
            this.chType.Text = "Type";
            this.chType.Width = 66;
            // 
            // chBlocks
            // 
            this.chBlocks.Text = "Blocks";
            this.chBlocks.Width = 78;
            // 
            // chBytes
            // 
            this.chBytes.Text = "Bytes";
            this.chBytes.Width = 85;
            // 
            // chExecAddr
            // 
            this.chExecAddr.Text = "Execute Address";
            this.chExecAddr.Width = 93;
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilIcons.Images.SetKeyName(0, "unknown.png");
            this.ilIcons.Images.SetKeyName(1, "system.png");
            this.ilIcons.Images.SetKeyName(2, "executable.png");
            this.ilIcons.Images.SetKeyName(3, "data.png");
            this.ilIcons.Images.SetKeyName(4, "image.png");
            // 
            // tabBootloader
            // 
            this.tabBootloader.Controls.Add(this.tableLayoutPanel1);
            this.tabBootloader.Location = new System.Drawing.Point(4, 22);
            this.tabBootloader.Name = "tabBootloader";
            this.tabBootloader.Padding = new System.Windows.Forms.Padding(3);
            this.tabBootloader.Size = new System.Drawing.Size(496, 290);
            this.tabBootloader.TabIndex = 1;
            this.tabBootloader.Text = "Bootloader";
            this.tabBootloader.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblWarning, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblExecAddr, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblCLKQSDS, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblDRAMSize, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblDQSODS, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.ndDRAMSize, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.ndExecAddr, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.ndDQSODS, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.ndCLKQSDS, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(490, 284);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblWarning
            // 
            this.lblWarning.AutoEllipsis = true;
            this.lblWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Location = new System.Drawing.Point(119, 10);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(448, 80);
            this.lblWarning.TabIndex = 10;
            this.lblWarning.Text = "Warning!\r\nTampering with these values could potentially led to an actual hard bri" +
                "cked device!\r\n\r\nDO NOT CHANGE ANYTHING UNLESS YOU KNOW WHAT YOU ARE DOING!";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblExecAddr
            // 
            this.lblExecAddr.AutoSize = true;
            this.lblExecAddr.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblExecAddr.Location = new System.Drawing.Point(16, 105);
            this.lblExecAddr.Name = "lblExecAddr";
            this.lblExecAddr.Size = new System.Drawing.Size(87, 26);
            this.lblExecAddr.TabIndex = 0;
            this.lblExecAddr.Text = "Execute Address";
            this.lblExecAddr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCLKQSDS
            // 
            this.lblCLKQSDS.AutoSize = true;
            this.lblCLKQSDS.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCLKQSDS.Location = new System.Drawing.Point(46, 157);
            this.lblCLKQSDS.Name = "lblCLKQSDS";
            this.lblCLKQSDS.Size = new System.Drawing.Size(57, 26);
            this.lblCLKQSDS.TabIndex = 3;
            this.lblCLKQSDS.Text = "CLKQSDS";
            this.lblCLKQSDS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDRAMSize
            // 
            this.lblDRAMSize.AutoSize = true;
            this.lblDRAMSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDRAMSize.Location = new System.Drawing.Point(41, 183);
            this.lblDRAMSize.Name = "lblDRAMSize";
            this.lblDRAMSize.Size = new System.Drawing.Size(62, 26);
            this.lblDRAMSize.TabIndex = 4;
            this.lblDRAMSize.Text = "DRAM Size";
            this.lblDRAMSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDQSODS
            // 
            this.lblDQSODS.AutoSize = true;
            this.lblDQSODS.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDQSODS.Location = new System.Drawing.Point(50, 131);
            this.lblDQSODS.Name = "lblDQSODS";
            this.lblDQSODS.Size = new System.Drawing.Size(53, 26);
            this.lblDQSODS.TabIndex = 2;
            this.lblDQSODS.Text = "DQSODS";
            this.lblDQSODS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ndDRAMSize
            // 
            this.ndDRAMSize.Location = new System.Drawing.Point(119, 186);
            this.ndDRAMSize.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.ndDRAMSize.Name = "ndDRAMSize";
            this.ndDRAMSize.Size = new System.Drawing.Size(120, 20);
            this.ndDRAMSize.TabIndex = 5;
            // 
            // ndExecAddr
            // 
            this.ndExecAddr.Location = new System.Drawing.Point(119, 108);
            this.ndExecAddr.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.ndExecAddr.Name = "ndExecAddr";
            this.ndExecAddr.Size = new System.Drawing.Size(120, 20);
            this.ndExecAddr.TabIndex = 2;
            // 
            // ndDQSODS
            // 
            this.ndDQSODS.Location = new System.Drawing.Point(119, 134);
            this.ndDQSODS.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.ndDQSODS.Name = "ndDQSODS";
            this.ndDQSODS.Size = new System.Drawing.Size(120, 20);
            this.ndDQSODS.TabIndex = 3;
            // 
            // ndCLKQSDS
            // 
            this.ndCLKQSDS.Location = new System.Drawing.Point(119, 160);
            this.ndCLKQSDS.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.ndCLKQSDS.Name = "ndCLKQSDS";
            this.ndCLKQSDS.Size = new System.Drawing.Size(120, 20);
            this.ndCLKQSDS.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::PandoraImager.Properties.Resources.warning;
            this.pictureBox1.Location = new System.Drawing.Point(3, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 341);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(520, 380);
            this.Name = "formMain";
            this.Text = "PandoraImager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formMain_FormClosing);
            this.Load += new System.EventHandler(this.formMain_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabImages.ResumeLayout(false);
            this.tabBootloader.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndDRAMSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndExecAddr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndDQSODS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndCLKQSDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem cbImageImport;
        private System.Windows.Forms.ToolStripMenuItem cbSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem cbImageUp;
        private System.Windows.Forms.ToolStripMenuItem cbImageDown;
        private System.Windows.Forms.ToolStripMenuItem cbImageExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem cbModify;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblExecAddr;
        private System.Windows.Forms.Label lblCLKQSDS;
        private System.Windows.Forms.Label lblDRAMSize;
        private System.Windows.Forms.Label lblDQSODS;
        private System.Windows.Forms.NumericUpDown ndDRAMSize;
        private System.Windows.Forms.NumericUpDown ndExecAddr;
        private System.Windows.Forms.NumericUpDown ndDQSODS;
        private System.Windows.Forms.NumericUpDown ndCLKQSDS;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ImageList ilIcons;
        private System.Windows.Forms.ListView lvImages;
        private System.Windows.Forms.ColumnHeader chIcon;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chBlocks;
        private System.Windows.Forms.ColumnHeader chBytes;
        private System.Windows.Forms.ColumnHeader chExecAddr;
    }
}