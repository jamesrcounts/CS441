//-----------------------------------------------------------------------
// <copyright file="UploadViewForm.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei
// Date: Sept 28 2011
// Modified date: Oct 23 2011
// Description: this class is responsible to show the upload photo in particular album that is
//              currently being viewed by the user.
//-----------------------------------------------------------------------
namespace PhotoBuddy
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using PhotoBuddy.Common;
    using PhotoBuddy.Models;
    using PhotoBuddy.Screens;

    /// <summary>
    /// Allows the user to see the photo they propose to add to ensure that it is the correct photo.
    /// </summary>
    public partial class UploadViewForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadViewForm"/> class.
        /// </summary>
        /// <param name="photo">The photo to display.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-25</para>
        ///   <para>Modified: 2011-10-25</para>
        ///   <para>
        /// Creates an instance of the UploadViewForm that is configured to rename a photo.
        ///   </para>
        /// </remarks>
        public UploadViewForm(IPhoto photo)
        {
            this.InitializeComponent();

            this.Text = Format.Culture("{0} {1} - Photo Buddy", "Rename", photo.DisplayName);
            this.pictureBox1.Image = photo.Image;
            this.displayNameTextBox.Text = photo.DisplayName;
            this.messageLabel.Text = "This is the photo you have selected to rename.";
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
            
            
            this.DisplayName = this.displayNameTextBox.Text;
            // Did user enter a blank name?
            if (string.IsNullOrWhiteSpace(this.DisplayName))
            {
                CultureAwareMessageBox.Show(
                    this,
                    "Photo name must not be empty.",
                    "Photo Buddy - " + Application.ProductVersion,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Did user enter too long of a name?
            if (Constants.MaxNameLength < this.DisplayName.Length)
            {
                CultureAwareMessageBox.Show(
                    this,
                    Format.Culture("Photo name is too long.  Please enter a name up to {0} characters.", Constants.MaxNameLength),
                    "Photo Buddy - " + Application.ProductVersion,
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

        /// <summary>
        /// Change the color of button controls when the mouse enters.
        /// </summary>
        /// <param name="sender">Any Button on this form.</param>
        /// <param name="e">The event args,</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleButtonMouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.ForeColor = Color.Black;
        }

        /// <summary>
        /// Change the color of button controls when the mouse leaves.
        /// </summary>
        /// <param name="sender">Any Button on this form.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleButtonMouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.ForeColor = Color.White;
        }
    }
}