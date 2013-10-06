//-----------------------------------------------------------------------
// <copyright file="MultiPhotoImportForm.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Screens
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// Provides a dialog which allows the user to review multiple photos before importing them.
    /// </summary>
    /// <remarks>
    ///   <para>Authors: Jim Counts and Eric Wei</para>
    ///   <para>Created: 2011-10-29</para>
    /// </remarks>
    public partial class MultiPhotoImportForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPhotoImportForm"/> class.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-29</para>
        /// </remarks>
        /// <seealso cref="http://msdn.microsoft.com/en-us/library/system.windows.forms.listview.aspx"/>
        public MultiPhotoImportForm()
        {
            this.InitializeComponent();

            // The default image size is 16 x 16, which sets up a larger
            // image size. 
            this.SelectedImageList.ImageSize = new Size(128, 128);
            this.SelectedImageList.TransparentColor = Color.White;
            this.SelectedImageList.ColorDepth = ColorDepth.Depth32Bit;

            // Set the view to show details.
            this.SelectedImageListView.View = View.LargeIcon;

            // Allow the user to edit item text.
            this.SelectedImageListView.LabelEdit = true;

            // Display check boxes.
            this.SelectedImageListView.CheckBoxes = true;
        }

        /// <summary>
        /// Gets the files that the user has confirmed they want to import..
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-29</para>
        /// </remarks>
        public IEnumerable<KeyValuePair<string, string>> SelectedFiles { get; private set; }

        /// <summary>
        /// Adds an image to the list view from a file. 
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-29</para>
        /// </remarks>
        /// <seealso cref="http://msdn.microsoft.com/en-us/library/system.windows.forms.listview.aspx"/>
        public void AddImageFromFile(string fileName)
        {
            // Create the item and a subitem for each file.
            var item1 = new ListViewItem(
                Path.GetFileNameWithoutExtension(fileName),
                this.SelectedImageListView.Items.Count);
            item1.SubItems.Add(new ListViewItem.ListViewSubItem(item1, fileName));

            // Place a check mark next to the item.
            item1.Checked = true;

            // Add the items to the ListView.
            this.SelectedImageListView.Items.Add(item1);

            // Initialize the ImageList objects with bitmaps.
            Image thumbnail = Image.FromFile(fileName);
            this.SelectedImageListView.LargeImageList.Images.Add(thumbnail);
        }

        /// <summary>
        /// Handles the continue button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-29</para>
        /// </remarks>
        private void HandleContinueButtonClick(object sender, System.EventArgs e)
        {
            this.SelectedFiles = from ListViewItem listItem in this.SelectedImageListView.CheckedItems
                                 select new KeyValuePair<string, string>(
                                     listItem.SubItems[0].Text,
                                     listItem.SubItems[1].Text);

            var tooLong = this.SelectedFiles.Where(file => PhotoBuddy.Common.Constants.MaxNameLength < file.Key.Length);
            if (tooLong.Any())
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine("The list of selected photos contains names that are too long:");
                foreach (var file in tooLong)
                {
                    sb.AppendLine(file.Key);
                }

                sb.AppendLine("Please use names up to 32 characters long.");

                MessageBox.Show(
                    sb.ToString(),
                    "Could Not Add Photos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Handles the cancel button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-29</para>
        /// </remarks>
        private void HandleCancelButtonClick(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
