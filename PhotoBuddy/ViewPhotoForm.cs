﻿//-----------------------------------------------------------------------
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
        private readonly Album album;

        /////// <summary>
        /////// The photo id.
        /////// </summary>
        ////private readonly string photoID;

        /// <summary>
        /// The photo
        /// </summary>
        private readonly Photo picture;

        /// <summary>
        /// All photos in the album.
        /// </summary>
        private readonly IList<Photo> allPhotosInAlbum;

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
            this.currentAlbumLabel.Text = this.album.AlbumId.Replace("&", "&&");
            /////this.allPhotosInAlbum = this.album.PhotoList.PhotoTable.Values.Cast<Photo>().ToList();
            this.allPhotosInAlbum = this.album.Photos.ToList();
            this.picture = this.allPhotosInAlbum.SingleOrDefault(photo => photo.PhotoId == photoToDisplay.PhotoId);
            this.photoIndex = this.allPhotosInAlbum.IndexOf(this.picture);
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
            Photo photo = this.allPhotosInAlbum[index];
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

        /// <summary>
        /// Handles the request to rename a photo.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-25</para>
        ///   <para>Modified: 2011-10-25</para>
        /// </remarks>
        private void HandleRenamePhotoButtonClick(object sender, EventArgs e)
        {
            // Givens:
            //  We have the album name from the form
            var albumName = this.currentAlbumLabel.Text.Replace("&&", "&");

            // First we need to find the secret file name
            var currentPhoto = this.allPhotosInAlbum[this.photoIndex];

            // Give the full secret path to the UploadView Form here...
            using (var renamePhotoView = new UploadViewForm(currentPhoto))
            {
                var result = renamePhotoView.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // User approved so rename the photo.
                    this.album.Repository.RenamePhotoInAlbum(albumName, renamePhotoView.DisplayName, currentPhoto.PhotoId);

                    // Important to update the new name on the view.
                    this.DisplayPhoto(this.photoIndex);
                }
            }
        }
    }
}
