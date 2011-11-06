//-----------------------------------------------------------------------
// <copyright file="ViewPhotoForm.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 23 2011
// Description: this class is responsible to show photo or possibly multiple photos
//              that are exist in the current album that the user added.
//-----------------------------------------------------------------------
namespace PhotoBuddy
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using PhotoBuddy.Models;
    using PhotoBuddy.Screens;

    /// <summary>
    /// Displays photos full size (up to the limit of the screen size).
    /// </summary>
    public partial class ViewPhotoForm : Form
    {
        /// <summary>
        /// The album the photo belongs to.
        /// </summary>
        private readonly IAlbum album;

        /// <summary>
        /// All photos in the album.
        /// </summary>
        private readonly IList<IPhoto> allPhotosInAlbum;

        /// <summary>
        /// The current photo's index.
        /// </summary>
        private int photoIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewPhotoForm"/> class.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public ViewPhotoForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewPhotoForm"/> class.
        /// </summary>
        /// <param name="currentAlbum">The album whose photos the user is viewing.</param>
        /// <param name="photoToDisplay">the specific photo to view.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public ViewPhotoForm(IAlbum currentAlbum, IPhoto photoToDisplay)
        {
            this.InitializeComponent();
            this.Text = "Photo Display - Photo Buddy";
            this.album = currentAlbum;
            this.currentAlbumLabel.Text = this.album.AlbumId.Replace("&", "&&");
            this.allPhotosInAlbum = new List<IPhoto>(this.album.Photos);
            this.photoIndex = this.allPhotosInAlbum.IndexOf(photoToDisplay);
            this.DisplayPhoto(this.photoIndex);
        }

        /// <summary>
        /// Displays the photo at the specified index.
        /// </summary>
        /// <param name="index">The photo index in the list of photos.</param>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts, Eric Wei</para>
        ///   <para>Modified: 2011-10-27</para>
        /// </remarks>
        private void DisplayPhoto(int index)
        {
            IPhoto photo = this.allPhotosInAlbum[index];
            this.pictureBox1.Image = photo.Image;
            this.photoNameLabel.Text = photo.DisplayName.Replace("&", "&&");
            this.Text = photo.DisplayName + " - Photo Buddy";
        }

        /// <summary>
        /// Handles the Click event of the backButton control.
        /// </summary>
        /// <param name="sender">The back button.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleBackButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the nextPhotoButton control.
        /// </summary>
        /// <param name="sender">The next photo button.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleNextPhotoButtonClick(object sender, EventArgs e)
        {
            this.photoIndex = this.CanGoForward() ? this.photoIndex + 1 : 0;
            this.DisplayPhoto(this.photoIndex);
        }

        /// <summary>
        /// Handles the Click event of the previousPhotoButton control.
        /// </summary>
        /// <param name="sender">Previous photo button.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandlePreviousPhotoButtonClick(object sender, EventArgs e)
        {
            this.photoIndex = this.CanGoBack() ? this.photoIndex - 1 : this.allPhotosInAlbum.Count - 1;
            this.DisplayPhoto(this.photoIndex);
        }

        /// <summary>
        /// Determines whether this instance can go forward to the next photo in the list.
        /// </summary>
        /// <returns>
        /// true if we are not at the end of the list; otherwise false.
        /// </returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private bool CanGoForward()
        {
            return this.photoIndex < this.allPhotosInAlbum.Count - 1;
        }

        /// <summary>
        /// Determines whether this instance can go back to the previous photo in the list.
        /// </summary>
        /// <returns>
        /// true if we are not at the first photo in the list; otherwise false.
        /// </returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private bool CanGoBack()
        {
            return this.photoIndex > 0;
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
            button.ForeColor = Color.FromArgb(47, 70, 102);
        }

        /// <summary>
        /// Toggles the timer.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        private void ToggleTimer(object sender, EventArgs e)
        {
            this.slideShowTimer.Enabled = !this.slideShowTimer.Enabled;
            if (this.slideShowTimer.Enabled)
            {
                this.slideShowTimer.Start();
            }
            else
            {
                this.slideShowTimer.Stop();
            }
        }

        /// <summary>
        /// Handles the Click event of the RenamePhotoButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void HandleRenamePhotoButtonClick(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.foundationTableLayoutPanel.Hide();

            var photoControl = new CropPhotoControl();
            photoControl.photoCropBox.Photo = this.pictureBox1.Image;
            photoControl.LeftButton.Text = "Cancel";
            photoControl.RightButton.Text = "Crop";
            photoControl.LeftButton.Click += (o, s) =>
            {
                photoControl.Hide();
                this.foundationTableLayoutPanel.Show();
            };
            photoControl.RightButton.Click += (o, s) =>
            {
                photoControl.Hide();

                // Get the rectangle
                Rectangle selectedRectangle = photoControl.photoCropBox.SelectedRectangle;
                CultureAwareMessageBox.Show(
                    this,
                    "Position: " + selectedRectangle.Location.X + "," + selectedRectangle.Location.Y + " Height: " + selectedRectangle.Height + " Width: " + selectedRectangle.Width,
                    "Result",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Do Some math
                // Crop the image.
                // Add new image to album
                // Display image.
            };
            this.Controls.Add(photoControl);
            photoControl.Show();
            this.ResumeLayout();
        }
    }
}
