//-----------------------------------------------------------------------
// <copyright file="UploadViewForm.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 23 2011
// Description: this class is responsible to show the upload photo in particular album that is
//              currently being viewed by the user.
//-----------------------------------------------------------------------
namespace PhotoBuddy
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using PhotoBuddy.Common.CommonClass;
    using PhotoBuddy.Resources;

    /// <summary>
    /// Allows the user to see the photo they propose to add to ensure that it is the correct photo.
    /// </summary>
    public partial class UploadViewForm : Form
    {
        /// <summary>
        /// The photo path.
        /// </summary>
        private readonly string photoFilename;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadViewForm"/> class.
        /// </summary>
        public UploadViewForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadViewForm"/> class.
        /// </summary>
        /// <param name="photoFilename">The photo to verify.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public UploadViewForm(string photoFilename)
        {
            this.InitializeComponent();
            this.photoFilename = photoFilename;
            this.Text = string.Format("Upload {0} - Photo Buddy", Path.GetFileName(photoFilename));

            // Try to open the image.
            try
            {
                this.pictureBox1.Image = Image.FromFile(photoFilename);
            }
            catch
            {
                // File was not a valid image so abort the upload & warn the user.
                MessageBox.Show(
                    Strings.ErrorNotPictureFile,
                    Strings.AppName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.HandleCancelButtonClick(this, new EventArgs());
            }

            this.displayNameTextbox.Text = Path.GetFileName(photoFilename);
        }

        /// <summary>
        /// Gets the name of the photo.
        /// </summary>
        /// <value>
        /// The name of the photo.
        /// </value>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Closes the form without accepting the photo.
        /// </summary>
        /// <param name="sender">Cancel button.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleCancelButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Accepts the photo then closes the form.
        /// </summary>
        /// <param name="sender">The Continue button</param>
        /// <param name="e">the event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleContinueButtonClick(object sender, EventArgs e)
        {
            this.DisplayName = this.displayNameTextbox.Text;

            // Did user enter a blank name?
            if (string.IsNullOrWhiteSpace(this.DisplayName))
            {
                MessageBox.Show(
                    "Photo name must not be empty!",
                    "Empty Photo Name Issue",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Did user enter too long of a name?
            if (this.DisplayName.Length > Constants.MaxNameLength)
            {
                MessageBox.Show(
                    "Photo name is too long.  Please enter a name less than " + Constants.MaxNameLength,
                    "Photo Name Length Issue",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Check for the enter key press and execute the continue button if it was pressed.
        /// </summary>
        /// <param name="sender">The display name textbox.</param>
        /// <param name="e">the event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan        
        /// </remarks>
        private void HandleDisplayNameTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            // See if the user pressed the enter key and if so execute the continue button.
            if (e.KeyCode == Keys.Enter)
            {
                this.HandleContinueButtonClick(sender, e);
            }
        }
    }
}