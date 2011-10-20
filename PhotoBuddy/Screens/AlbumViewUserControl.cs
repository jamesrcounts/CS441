//-----------------------------------------------------------------------
// <copyright file="AlbumViewUserControl.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 19 2011
// Description: this class is responsible for the use control in album view which 
//              is called from mainForm to do the state changes.
//-----------------------------------------------------------------------
namespace TheNewPhotoBuddy.Screens
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using TheNewPhotoBuddy.BussinessRule;
    using TheNewPhotoBuddy.Common.CommonClass;
    using TheNewPhotoBuddy.Controls;
    using System.Collections.Generic;

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
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.DisplayName = "Album";
        }

        /// <summary>
        /// Defines a delegate to handle "Rename Album" events.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>Rename Album events are typically fired by the rename album button, which indicates the user wants to rename
        /// an album.</remarks>
        public delegate void RenameAlbumHandler(object sender, EventArgs e);

        /// <summary>
        /// Defines a delegate to handle "Back" events.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>Back events are typically fired by back buttons, which return the user to the previous view.</remarks>
        public delegate void BackEventHandler(object sender, EventArgs e);

        /// <summary>
        /// Defines a delegate to handle "Add Photo" events.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>Add Photos events are typically fired by the Add Photos button, which indicates the user wants to add photos.</remarks>
        public delegate void AddPhotosEventHandler(object sender, EventArgs e);

        /// <summary>
        /// Occurs when the back button is clicked.
        /// </summary>
        public event BackEventHandler BackEvent;

        /// <summary>
        /// Occurs when the rename album button is clicked.
        /// </summary>
        public event RenameAlbumHandler RenameAlbumEvent;

        /// <summary>
        /// Occurs when the add photo button is clicked.
        /// </summary>
        public event AddPhotosEventHandler AddPhotosEvent;

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
                    labelAlbumName.Text = this.currentAlbum.albumID;
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
        /// <remarks><para>Author(s): Miguel Gonzales and Andrea Tan</para></remarks>
        public void RefreshPhotoList()
        {
            if (this.currentAlbum == null)
            {
                return;
            }

            // Clear out the photos is the panel.
            photosFlowPanel.Controls.Clear();
            if (this.currentAlbum.photoObjects.photoList.Count == 0)
            {
                return;
            }

            foreach (Photo photo in this.currentAlbum.photoObjects.photoList.Values)
            {
                // Create a thumbnail control for the current photo
                PB_ThumbNailUserControl thumb = new PB_ThumbNailUserControl() { DiaplayName = photo.display_name };

                // Store the photo object in the thumbnail tag.
                // thumbnail is a public property to set the picturebox on the thumbnailUserControl.
                thumb.thumbnail.Tag = photo;

                // Combine the secret location with the secret name to get the full file path.
                string path = System.IO.Path.Combine(Constants.PhotosFolderPath, photo.copiedPath);

                // Load the file
                thumb.thumbnail.Image = Image.FromFile(path);

                // Wire the click event to the picturebox
                thumb.thumbnail.Click += this.HandlePhotoClick;

                // Add the thumb control to the flow panel.
                photosFlowPanel.Controls.Add(thumb);
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
            photosFlowPanel.Focus();
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
            photosFlowPanel.Focus();
        }
    }
}
