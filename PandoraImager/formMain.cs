using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using libnvtnand;
using System.IO;

namespace PandoraImager
{
    public partial class formMain : Form
    {
        NandImage image = null;
        string fileName = "";
        FileStream stream = null;
        bool imageModified = false;
        Dictionary<int, ushort> ListToID = new Dictionary<int, ushort>();

        public formMain()
        {
            InitializeComponent();
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            SetNoFileLoaded();
        }

        private void cbAbout_Click(object sender, EventArgs e)
        {
            new formAbout().ShowDialog();
        }

        private void cbLoad_Click(object sender, EventArgs e)
        {
            if(imageModified) {
                DialogResult result;
                if((result = MessageBox.Show("Your last changes are not saved!\nDo you want to save them now or cancel opening?",
                    "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)) == System.Windows.Forms.DialogResult.Cancel)
                    return;
                else if(result == System.Windows.Forms.DialogResult.Yes)
                    cbSave.PerformClick();
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.Filter = "Image files|*.img|All files|*.*";
            ofd.FilterIndex = 0;
            ofd.Title = "Select NAND image to open";
            ofd.CheckFileExists = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists(ofd.FileName))
                {
                    MessageBox.Show("File not found!", "An error occured!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    image = null;
                    stream = null;
                    SetNoFileLoaded();
                    return;
                }

                try
                {
                    fileName = ofd.FileName;
                    stream = File.Open(fileName, FileMode.Open);
                    image = new NandImage();
                    if (!image.ReadImage(stream))
                    {
                        MessageBox.Show("Parsing the image failed!\nCheck whether you have provided a valid image.",
                            "An error occured!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        stream.Close();
                        image = null;
                        stream = null;
                        SetNoFileLoaded();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionBox(ex);
                    image = null;
                    stream = null;
                    SetNoFileLoaded();
                    return;
                }

                SetFileLoaded();
            }
        }

        private void ExceptionBox(Exception ex)
        {
            MessageBox.Show(string.Format("Opening failed due to an error!\n\nMessage:\n{0}\n\nStack trace:\n{1}",
                        ex.Message, ex.StackTrace), "An error occured!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SetNoFileLoaded()
        {
            this.Text = "PandoraImager";
            lvImages.Items.Clear();
            lvImages.Enabled = false;
            cbSave.Enabled = false;
            cbSaveAs.Enabled = false;
            tsddImage.Enabled = false;
        }

        private void SetFileLoaded()
        {
            this.Text = string.Format("PandoraImager - {0}", fileName);
            lvImages.Enabled = true;
            cbSave.Enabled = true;
            cbSaveAs.Enabled = true;
            RefreshMenus();
            RefreshList();
        }

        private void RefreshMenus()
        {
            if (tabControl.SelectedTab == tabImages)
            {
                tsddImage.Enabled = true;
                if (lvImages.SelectedItems.Count == 1)
                {
                    if(ListToID.ContainsKey(lvImages.SelectedIndices[0]))
                        cbImageRemove.Enabled = (ListToID[lvImages.SelectedIndices[0]] != 0);
                    else
                        cbImageRemove.Enabled = true;

                    cbImageModify.Enabled = true;
                }
                else
                {
                    cbImageModify.Enabled = false;
                    cbImageRemove.Enabled = false;
                }
            }
            else
            {
                tsddImage.Enabled = false;
            }
        }

        private void RefreshList()
        {
            lvImages.Items.Clear();
            ListToID.Clear();

            int item = 0;
            foreach (ImageEntry entry in image.GetImages())
            {
                string[] subItems = new string[lvImages.Columns.Count < 6 ? 6 : lvImages.Columns.Count];
                subItems[0] = entry.ImageID.ToString();
                subItems[1] = entry.Name.ToString();
                subItems[2] = entry.ImageType.ToString();
                subItems[3] = entry.StartBlock.ToString();
                subItems[4] = (entry.EndBlock - entry.StartBlock).ToString();
                subItems[5] = string.Format("0x{0}", entry.FileSize.ToString("X4"));

                lvImages.Items.Add(new ListViewItem(subItems, 0));
                ListToID.Add(item, entry.ImageID);

                item++;
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMenus();
        }

        private void lvImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMenus();
        }
    }
}
