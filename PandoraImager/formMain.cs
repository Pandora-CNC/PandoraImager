﻿using System;
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

        private void cbQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbAbout_Click(object sender, EventArgs e)
        {
            new formAbout().ShowDialog();
        }

        private void cbLoad_Click(object sender, EventArgs e)
        {
            if (imageModified)
            {
                DialogResult result;
                if ((result = MessageBox.Show("Your last changes are not saved!\n" +
                    "Do you want to save them now or cancel opening?",
                    "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    == System.Windows.Forms.DialogResult.Cancel)
                    return;
                else if (result == System.Windows.Forms.DialogResult.Yes)
                    cbSave.PerformClick();
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.Filter = "NAND image files (*.img)|*.img|All files (*.*)|*.*";
            ofd.FilterIndex = 0;
            ofd.Multiselect = false;
            ofd.Title = "Select NAND image to open";
            ofd.CheckFileExists = true;

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            if (!File.Exists(ofd.FileName))
            {
                MessageBox.Show("File not found!", "An error occured!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                image = null;
                stream = null;
                SetNoFileLoaded();
                return;
            }

            try
            {
                fileName = ofd.FileName;
                stream = File.Open(fileName, FileMode.Open);
                
                stream.Seek(0, SeekOrigin.End);
                if (stream.Position != NandImage.NandSize)
                {
                    MessageBox.Show("Image file size missmatch!\n" +
                        "Image file should be 0x800000 bytes large.",
                        "An error occured!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    stream.Close();
                    image = null;
                    stream = null;
                    SetNoFileLoaded();
                    return;
                }
                stream.Seek(0, SeekOrigin.Begin);

                image = new NandImage();
                if (!image.ReadImage(stream))
                {
                    MessageBox.Show("Parsing the image failed!\n" + 
                        "Check whether you have provided a valid image.",
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

            imageModified = false;
            SetFileLoaded();
        }

        private void cbNew_Click(object sender, EventArgs e)
        {
            if (imageModified)
            {
                DialogResult result;
                if ((result = MessageBox.Show("Your last changes are not saved!\n" + 
                    "Do you want to save them now or cancel opening?",
                    "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    == System.Windows.Forms.DialogResult.Cancel)
                    return;
                else if (result == System.Windows.Forms.DialogResult.Yes)
                    cbSave.PerformClick();
            }

            if (MessageBox.Show("Creating a completely new image is discouraged.\n" +
                "Only proceed if you know what you are doing!\n" +
                "Wrongfully created images will result in a bricked device!!!\n\n" +
                "This process will require you to provide a working NANDLoader (aka. ROM).\n" +
                "It is easier and recommended to start off an existing image.\n\n" +
                "Do you really want to continue?", "Warning", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "NANDLoader_192MHz.bin";
            ofd.Multiselect = false;
            ofd.Filter = "NANDLoader ROM images (*.bin)|*.bin|All files (*.*)|*.*";
            ofd.FilterIndex = 0;
            ofd.Title = "Select NANDLoader ROM";
            ofd.CheckFileExists = true;

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                if (!File.Exists(ofd.FileName))
                {
                    MessageBox.Show("File not found!", "An error occured!",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    image = null;
                    stream = null;
                    SetNoFileLoaded();
                    return;
                }

                FileStream romStream = File.OpenRead(ofd.FileName);
                romStream.Seek(0, SeekOrigin.End);

                if(romStream.Position > NandImage.BytesPerBlock / 2) {
                    MessageBox.Show("The supplied NANDLoader ROM is too large!",
                        "An error occured!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    romStream.Close();
                    image = null;
                    stream = null;
                    SetNoFileLoaded();
                    return;
                }

                uint romSize = (uint)romStream.Position;
                string romName = "NANDLoader";
                if(Path.GetFileNameWithoutExtension(ofd.FileName).ToLower().Contains("nandloader"))
                    romName = Path.GetFileNameWithoutExtension(ofd.FileName).Trim();
                if(romName.Length > 30)
                    romName = romName.Substring(0, 30);
                romStream.Seek(0, SeekOrigin.Begin);
                image = new NandImage();
                ImageEntry nandLoader = new ImageEntry(0, ImageType.System, 0, 3, 0, romSize + 0x20,
                    romName);
                nandLoader.Data = new byte[romSize];
                romStream.Read(nandLoader.Data, 0, (int)romSize);
                romStream.Close();

                if (!image.AddImage(nandLoader))
                {
                    MessageBox.Show("The supplied NANDLoader ROM invalid!",
                        "An error occured!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    image = null;
                    stream = null;
                    SetNoFileLoaded();
                    return;
                }

                // Initalize the boot header
                image.Header = new BootHeader();
                image.Header.BootCodeMarker = 0x57425AA5;
                image.Header.ExecuteAddress = 0x900000;
                image.Header.ImageSize = romSize + 0x20;
                image.Header.SkewMarker = 0xA55A4257;
                image.Header.DQSODS = 0x00001010;
                image.Header.CLKQSDS = 0x00888800;
                image.Header.DramMarker = 0;
                image.Header.DramSize = 0;
            }
            catch (Exception ex)
            {
                ExceptionBox(ex);
                image = null;
                stream = null;
                SetNoFileLoaded();
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "";
            sfd.Filter = "NAND image files (*.img)|*.img|All files (*.*)|*.*";
            sfd.FilterIndex = 0;
            sfd.Title = "Save NAND image as";
            sfd.OverwritePrompt = true;

            if (sfd.ShowDialog() != DialogResult.OK)
            {
                image = null;
                stream = null;
                SetNoFileLoaded();
                return;
            }

            try
            {
                fileName = sfd.FileName;
                stream = File.Open(fileName, FileMode.Create);

                if (!NandImage.FormatImage(stream))
                {
                    MessageBox.Show("Couldn't format the new image!",
                        "An error occured!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    stream.Close();
                    image = null;
                    stream = null;
                    SetNoFileLoaded();
                    return;
                }

                if (!image.WriteImage(stream))
                {
                    MessageBox.Show("Couldn't save the new image!",
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

            imageModified = false;
            SetFileLoaded();
        }

        private void ExceptionBox(Exception ex)
        {
            MessageBox.Show(string.Format("Opening failed due to an error!\n\nMessage:\n{0}\n\n" +
                "Stack trace:\n{1}", ex.Message, ex.StackTrace), "An error occured!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    int index = lvImages.SelectedIndices[0];

                    if (ListToID.ContainsKey(index))
                    {
                        cbImageAdd.Enabled = false;
                        cbImageRemove.Enabled = (ListToID[index] != 0);
                        cbImageRename.Enabled = (ListToID[index] != 0);
                        cbImageImport.Enabled = true;
                        cbImageExport.Enabled = true;
                        tsmiImageChangeType.Enabled = (ListToID[index] != 0);
                        cbImageUp.Enabled = (ListToID[index] != 0 && index > 0);
                        cbImageDown.Enabled = (ListToID[index] != 0 && index + 1 < ListToID.Count);
                    }
                    else
                    {
                        cbImageAdd.Enabled = false;
                        cbImageRemove.Enabled = false;
                        cbImageRename.Enabled = false;
                        cbImageImport.Enabled = false;
                        cbImageExport.Enabled = false;
                        tsmiImageChangeType.Enabled = false;
                        cbImageUp.Enabled = false;
                        cbImageDown.Enabled = false;
                    }
                }
                else
                {
                    cbImageAdd.Enabled = true;
                    cbImageRemove.Enabled = false;
                    cbImageRename.Enabled = false;
                    cbImageImport.Enabled = false;
                    cbImageExport.Enabled = false;
                    tsmiImageChangeType.Enabled = false;
                    cbImageUp.Enabled = false;
                    cbImageDown.Enabled = false;
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

        private bool SaveTo(Stream stream)
        {
            if (!NandImage.FormatImage(stream))
            {
                MessageBox.Show("Couldn't format the image!",
                    "An error occured!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!image.WriteImage(stream))
            {
                MessageBox.Show("Couldn't save the image!",
                    "An error occured!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;   
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (imageModified)
            {
                DialogResult result;
                if ((result = MessageBox.Show("Your last changes are not saved!\n" +
                    "Do you want to save them now or cancel opening?",
                    "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    == System.Windows.Forms.DialogResult.Cancel)
                    return;
                else if (result == System.Windows.Forms.DialogResult.Yes)
                    cbSave.PerformClick();
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

        private void cbSave_Click(object sender, EventArgs e)
        {
            if(SaveTo(stream))
                imageModified = false;
        }

        private void cbSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "";
            sfd.Filter = "NAND image files (*.img)|*.img|All files (*.*)|*.*";
            sfd.FilterIndex = 0;
            sfd.Title = "Save NAND image as";
            sfd.OverwritePrompt = true;

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                FileStream stream = File.Open(sfd.FileName, FileMode.Create);
                bool success = SaveTo(stream);
                stream.Close();

                if (!success)
                    return;
            }
            catch (Exception ex)
            {
                ExceptionBox(ex);
                return;
            }

            MessageBox.Show("Image file successfully written!", "Success!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}