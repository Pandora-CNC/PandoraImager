using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libw5nand;

namespace PandoraImager
{
    public partial class formSegment : Form
    {
        public ImageHeader Header { get; set; }
        public bool EditMode { get; private set; }
        public bool OnlyName { get; private set; }

        public formSegment(bool EditMode, bool OnlyName = false)
            : this(new ImageHeader() { ImageType = ImageType.Execute }, EditMode, OnlyName)
        {
        }

        public formSegment(ImageHeader Header, bool EditMode, bool OnlyName = false)
        {
            InitializeComponent();

            this.Header = Header;
            this.EditMode = EditMode;
            this.OnlyName = OnlyName;

            cbType.DataSource = Enum.GetValues(typeof(ImageType));

            if (EditMode)
            {
                this.Text = "Modify data segment";
                pbIcon.Image = Properties.Resources.big_modify;
            }
            else
            {
                this.Text = "Add new data segment";
                pbIcon.Image = Properties.Resources.big_add;
            }

            tbName.Text = Header.ImageName;
            cbType.SelectedIndex = (int)Header.ImageType;
            ndExecAddr.Value = Header.ExecuteAddress;

            if (OnlyName)
            {
                cbType.Enabled = false;
                ndExecAddr.ReadOnly = true;
            }
        }

        private void cbOK_Click(object sender, EventArgs e)
        {
            ImageHeader head = new ImageHeader();
            head.ImageName = tbName.Text;
            if (!OnlyName)
            {
                head.ImageType = (ImageType)cbType.SelectedIndex;
                head.ExecuteAddress = (uint)ndExecAddr.Value;
            }
            else
            {
                head.ImageType = Header.ImageType;
                head.ExecuteAddress = Header.ExecuteAddress;
            }

            Header = head;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cbCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
