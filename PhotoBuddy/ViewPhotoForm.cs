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
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using PhotoBuddy.BussinessRule;
    using PhotoBuddy.Common.CommonClass;

    /// <summary>
    /// Displays photos full size (up to the limit of the screen size).
    /// </summary>
    public partial class ViewPhotoForm : Form
    {
        /// <summary>
        /// The album the photo belongs to.
        /// </summary>
        private readonly Album album;

        /// <summary>
        /// The photo id.
        /// </summary>
        private readonly string photoID;

        /// <summary>
        /// The photo
        /// </summary>
        private readonly Photo picture;

        /// <summary>
        /// All photos in the album.
        /// </summary>
        private List<Photo> allPhotosInAlbum;

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
        public ViewPhotoForm(Album currentAlbum, Photo photoToDisplay)
        {
            this.InitializeComponent();
            this.Text = "Photo Display - Photo Buddy";
            this.album = currentAlbum;
            this.photoID = photoToDisplay.PhotoId;
            this.picture = photoToDisplay;
            this.currentAlbumLabel.Text = this.album.AlbumID;
            this.allPhotosInAlbum = this.album.PhotoList.PhotoTable.Values.Cast<Photo>().ToList();
            this.photoIndex = this.allPhotosInAlbum.IndexOf(this.picture);
            this.DisplayPhoto(this.photoIndex);
        }

        /// <summary>
        /// Displays the photo at the specified index.
        /// </summary>
        /// <param name="index">The photo index in the list of photos.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void DisplayPhoto(int index)
        {
            string filename = Path.GetFileName(this.allPhotosInAlbum[index].CopiedPath);
            string path = Path.Combine(Constants.PhotosFolderPath, filename);
            this.pictureBox1.Image = Image.FromFile(path);
            this.photoNameLabel.Text = this.allPhotosInAlbum[index].DisplayName;
            this.Text = this.allPhotosInAlbum[index].DisplayName + " - Photo Buddy";
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
            button.ForeColor = Color.Azure;
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
            button.ForeColor = Color.DarkGray;
        }
    }
}
