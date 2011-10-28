//-----------------------------------------------------------------------
// <copyright file="AlbumViewUserControl.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts
// Date: Sept 28 2011
// Modified date: Oct 24 2011
// Description: this class is responsible for the use control in album view which 
//              is called from mainForm to do the state changes.
//-----------------------------------------------------------------------
namespace PhotoBuddy.Screens
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using PhotoBuddy.BusinessRule;
    using PhotoBuddy.Common.CommonClass;
    using PhotoBuddy.Controls;
    using PhotoBuddy.EventObjects;
    using System.Linq;

    /// <summary>
    /// Displays an album
    /// </summary>
    [DebuggerDisplay("{DisplayName}")]
    public partial class AlbumViewUserControl : UserControl, IScreen
    {
        /// <summary>
        /// The current album.
        /// </summary>
        private Album currentAlbum;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumViewUserControl"/> class.
        /// </summary>
        /// <remarks><para>Author(s): Miguel Gonzales and Andrea Tan</para></remarks>
        public AlbumViewUserControl()
        {
            this.InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.DisplayName = "Album";
        }

        

        /////// <summary>
        /////// Defines a delegate to handle "Rename Album" events.
        /////// </summary>
        /////// <param name="sender">The sender.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /////// <remarks>Rename Album events are typically fired by the rename album button, which indicates the user wants to rename
        /////// an album.</remarks>
        ////public delegate void RenameAlbumHandler(object sender, EventArgs e);

        /////// <summary>
        /////// Defines a delegate to handle "Back" events.
        /////// </summary>
        /////// <param name="sender">The sender.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /////// <remarks>Back events are typically fired by back buttons, which return the user to the previous view.</remarks>
        ////public delegate void BackEventHandler(object sender, EventArgs e);

        /////// <summary>
        /////// Defines a delegate to handle "Add Photo" events.
        /////// </summary>
        /////// <param name="sender">The sender.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /////// <remarks>Add Photos events are typically fired by the Add Photos button, which indicates the user wants to add photos.</remarks>
        ////public delegate void AddPhotosEventHandler(object sender, EventArgs e);

        /// <summary>
        /// Occurs when the back button is clicked.
        /// </summary>
        public event EventHandler BackEvent;

        /// <summary>
        /// Occurs when the rename album button is clicked.
        /// </summary>
        public event EventHandler RenameAlbumEvent;

        /// <summary>
        /// Occurs when the add photo button is clicked.
        /// </summary>
        public event EventHandler AddPhotosEvent;

        /// <summary>
        /// Gets the control managed by this view.
        /// </summary>
        /// <remarks>Author: Jim Counts</remarks>
        public UserControl Control
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Gets or sets the current album.
        /// </summary>
        /// <value>
        /// The current album.
        /// </value>
        public Album CurrentAlbum
        {
            get
            {
                return this.currentAlbum;
            }

            set
            {
                this.currentAlbum = value;
                if (this.currentAlbum != null)
                {
                    this.labelAlbumName.Text = this.currentAlbum.AlbumId.Replace("&", "&&");
                    this.RefreshPhotoList();
                }
            }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Refreshes the list of photos in the current album.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public void RefreshPhotoList()
        {
            if (this.currentAlbum == null)
            {
                return;
            }

            // Clear out the photos is the panel.
            this.photosFlowPanel.Controls.Clear();
            if (this.currentAlbum.Count == 0)
            {
                return;
            }

            foreach (Photo photo in this.currentAlbum.Photos)
            {
                // Create a thumbnail control for the current photo
                ThumbNailUserControl thumb = new ThumbNailUserControl() { DisplayName = photo.DisplayName };

                // Store the photo object in the thumbnail tag.
                // thumbnail is a public property to set the picture box on the thumbnailUserControl.
                thumb.Thumbnail.Tag = photo;

                ////// Combine the secret location with the secret name to get the full file path.
                ////string path = Path.Combine(Constants.PhotosFolderPath, photo.CopiedPath);

                ////// Load the file
                ////try
                ////{
                ////    thumb.Thumbnail.Image = File.Exists(path) ?
                ////        Image.FromFile(path) :
                ////        PhotoBuddy.Properties.Resources.MissingImageIcon.ToBitmap();
                ////}
                ////catch (OutOfMemoryException)
                ////{
                ////    thumb.Thumbnail.Image = PhotoBuddy.Properties.Resources.MissingImageIcon.ToBitmap();
                ////}
                thumb.Thumbnail.Image = photo.GetImage();

                // Wire the click event to the picturebox
                thumb.Thumbnail.Click += this.HandlePhotoClick;
                thumb.DeletePhotoEvent += this.HandleDeletePhotoEvent;

                // Add the thumb control to the flow panel.
                this.photosFlowPanel.Controls.Add(thumb);
            }
        }

        /// <summary>
        /// Shows the view.
        /// </summary>
        /// <param name="history">The caller's history of previous views.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        /// </remarks>
        public void ShowView(Stack<UserControl> history)
        {
            // Push myself onto the history stack
            history.Push(this);

            // Show myself
            this.Visible = true;

            // To focus text boxes.
            this.Focus();
        }

        /// <summary>
        /// Handles the Click event of the backButton control.
        /// </summary>
        /// <param name="sender">Back button</param>
        /// <param name="e">event args</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleBackButtonClick(object sender, EventArgs e)
        {
            this.BackEvent(this, e);
        }

        /// <summary>
        /// Handles the Click event of the AddPhotosButton control.
        /// </summary>
        /// <param name="sender">Add photos button,</param>
        /// <param name="e">the event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleAddPhotosButtonClick(object sender, EventArgs e)
        {
            this.AddPhotosEvent(this, e);
        }

        /// <summary>  
        /// Handles the click of the rename album button.
        /// </summary>
        /// <param name="sender">Rename album button</param>
        /// <param name="e">the event args.</param>
        /// <remarks><para>Author(s): Miguel Gonzales and Andrea Tan</para></remarks>
        private void HandleRenameAlbumButtonClick(object sender, EventArgs e)
        {
            this.RenameAlbumEvent(this, e);
        }

        /// <summary>
        /// Show the photo that the user clicked on.
        /// </summary>
        /// <param name="sender">The photo name label.</param>
        /// <param name="e">the event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandlePhotoClick(object sender, EventArgs e)
        {
            PictureBox uc = sender as PictureBox;
            using (ViewPhotoForm photoForm = new ViewPhotoForm(this.currentAlbum, (Photo)uc.Tag))
            {
                photoForm.ShowDialog();
                this.RefreshPhotoList();
            }
        }

        /// <summary>
        /// Gives focus to the flow panel on mouse enter.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales and Andrea Tan</para>
        ///   <para>Flow panel needs to be focused in order to scroll using the mouse wheel.</para>
        /// </remarks>
        private void HandlePhotosFlowPanelMouseEnter(object sender, EventArgs e)
        {
            this.photosFlowPanel.Focus();
        }

        /// <summary>
        /// Gives focus to flow panel on mouse click.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales and Andrea Tan</para>
        ///   <para>Flow panel needs to be focused in order to scroll using the mouse wheel.</para>
        /// </remarks>
        private void HandlePhotosFlowPanelMouseClick(object sender, MouseEventArgs e)
        {
            this.photosFlowPanel.Focus();
        }

        public virtual void HandleDeletePhotoEvent(object sender, PhotoEventArgs e)
        {
            string photoDisplayName = e.PhotoName;
            Album currentAlbum = this.CurrentAlbum;

            Photo photoToDelete = currentAlbum.Photos.Where(photo => photo.DisplayName == photoDisplayName).Single();
            currentAlbum.Repository.DeletePhoto(currentAlbum, photoToDelete);
            this.RefreshPhotoList();
        }

        
    }
}