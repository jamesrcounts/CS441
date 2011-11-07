//-----------------------------------------------------------------------
// <copyright file="ThumbnailUserControl.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Oct 13 2011
// Modified date: Oct 23 2011
// High level Description: This is a custom control that displays a thumbnail size image with a textbox.
//-----------------------------------------------------------------------
namespace PhotoBuddy.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using PhotoBuddy.EventObjects;
    using PhotoBuddy.Models;

    /// <summary>
    /// Displays a photo thumbnail with its name.
    /// </summary>
    /// <remarks>
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// Date: Oct 13 2011
    /// Modified date: Oct 14 2011
    /// </remarks>
    public partial class ThumbnailUserControl : UserControl
    {
        /// <summary>
        /// The photo entity
        /// </summary>
        /// <remarks>
        /// Author: Jim Counts
        /// Created 2011-11-06
        /// </remarks>
        private IPhoto photo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThumbnailUserControl"/> class.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Oct 13 2011
        /// Modified date: Oct 23 2011
        /// </remarks>
        public ThumbnailUserControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Occurs when the user selects the delete action for a photo.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei.</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public event EventHandler DeletePhotoEvent;

        /// <summary>
        /// Occurs when the thumbnail is clicked.
        /// </summary>
        public event EventHandler ThumbnailClick;

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName
        {
            get
            {
                return this.photoNameTextBox.Text;
            }

            set
            {
                if (this.InvokeRequired)
                {
                    Action<string> invoker = s => this.DisplayName = s;
                    this.BeginInvoke(invoker, value);
                    return;
                }

                this.photoNameTextBox.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        public IPhoto Photo
        {
            get
            {
                return this.photo;
            }

            set
            {
                if (this.InvokeRequired)
                {
                    Action<IPhoto> invoker = p => this.Photo = p;
                    this.BeginInvoke(invoker, value);
                    return;
                }

                this.photo = value;
                if (this.photo != null)
                {
                    this.thumbnailPictureBox.Image = this.photo.CreateThumbnail(
                    this.thumbnailPictureBox.Width,
                    this.thumbnailPictureBox.Height);
                }
                else
                {
                    this.thumbnailPictureBox.Image = this.thumbnailPictureBox.ErrorImage;
                }
            }
        }

        /// <summary>
        /// Called when the thumbnail is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        public virtual void OnThumbnailClick(object sender, EventArgs e)
        {
            EventHandler handler = this.ThumbnailClick;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Called when the delete photo event is triggered.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventObjects.PhotoEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public virtual void OnDeletePhotoEvent(object sender, EventArgs e)
        {
            EventHandler handler = this.DeletePhotoEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Photo.Close();
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Highlights the photo when the mouse is over it.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Oct 13 2011
        /// Modified date: Oct 23 2011
        /// </remarks>
        private void HighlightPhoto(object sender, EventArgs e)
        {
            this.photoPanel.BackColor = Color.Blue;
        }

        /// <summary>
        /// Removes the highlight from the photo when the mouse is no longer over it.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Oct 13 2011
        /// Modified date: Oct 23 2011
        /// </remarks>
        private void RemovePhotoHighlight(object sender, EventArgs e)
        {
            this.photoPanel.BackColor = Color.White;
        }

        /// <summary>
        /// Handles the delete tool strip menu item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        private void HandleDeleteToolStripMenuItemClick(object sender, EventArgs e)
        {
            ////DialogResult result = CultureAwareMessageBox.Show(
            ////    this,
            ////    "Are you sure you want to delete this photo?", 
            ////    "Delete Photo?", 
            ////    MessageBoxButtons.YesNo, 
            ////    MessageBoxIcon.Question);
            ////if (result == DialogResult.No)
            ////{
            ////    return;
            ////}

            this.Photo.Album.Repository.DeletePhoto(this.Photo.Album, this.Photo);
            this.OnDeletePhotoEvent(this, new EventArgs());
        }

        /// <summary>
        /// Handles the request to rename a photo.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-25</para>
        ///   <para>Modified: 2011-11-04</para>
        /// </remarks>
        private void RenamePhoto(object sender, EventArgs e)
        {
            using (var renamePhotoView = new UploadViewForm(this.Photo))
            {
                var result = renamePhotoView.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // User approved so rename the photo.
                    this.Photo.Album.Repository.RenamePhotoInAlbum(
                        this.Photo.Album.AlbumId, 
                        renamePhotoView.DisplayName, 
                        this.photo.PhotoId);
                    this.Photo.Album.Repository.SaveAlbums();

                    // Important to update the new name on the view.                    
                    this.photoNameTextBox.Text = this.Photo.DisplayName;
                }
            }
        }
    }
}