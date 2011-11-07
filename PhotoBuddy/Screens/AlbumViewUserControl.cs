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
    using System.Linq;
    using System.Windows.Forms;
    using PhotoBuddy.Common;
    using PhotoBuddy.Controls;
    using PhotoBuddy.Models;
    using PhotoBuddy.Properties;

    /// <summary>
    /// Displays an album
    /// </summary>
    [DebuggerDisplay("{DisplayName}")]
    public partial class AlbumViewUserControl : UserControl, IScreen
    {
        /// <summary>
        /// A value indicating whether adding photos to the album is allowed.
        /// </summary>
        private bool addPhotosEnabled;

        /// <summary>
        /// The current album.
        /// </summary>
        private IAlbum currentAlbum;

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

        /// <summary>
        /// Occurs when the back button is clicked.
        /// </summary>
        public event EventHandler BackEvent;

        /// <summary>
        /// Gets or sets a value indicating whether adding photos is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if adding photos is enabled; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        public bool AddPhotosEnabled
        {
            get
            {
                return this.addPhotosEnabled;
            }

            set
            {
                this.addPhotosEnabled = value;
                if (!this.addPhotosEnabled)
                {
                    this.addPhotosButton.Hide();
                }
                else
                {
                    this.addPhotosButton.Show();
                }
            }
        }

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
        public IAlbum CurrentAlbum
        {
            get
            {
                return this.currentAlbum;
            }

            set
            { 
                this.currentAlbum = value;
                this.AddPhotosEnabled = true;
                if (this.currentAlbum != null)
                {
                    this.albumNameLabel.Text = this.currentAlbum.AlbumId.Replace("&", "&&");
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
            using (Cursor.Current = Cursors.WaitCursor)
            {
                if (this.currentAlbum == null)
                {
                    return;
                }

                this.photosFlowPanel.Controls.Clear();
                var albumPhotos = this.currentAlbum.Photos.ToList();
                for (int i = 0; i < albumPhotos.Count; i++)
                {
                    var thumbnailControl = new ThumbnailUserControl();
                    thumbnailControl.ThumbnailClick += this.HandlePhotoClick;
                    thumbnailControl.DeletePhotoEvent += this.HandleDeletePhotoEvent;
                    this.photosFlowPanel.Controls.Add(thumbnailControl);
                    var thumb = (ThumbnailUserControl)this.photosFlowPanel.Controls[i];
                    var photo = albumPhotos[i];
                    thumb.DisplayName = photo.DisplayName;
                    thumb.Photo = photo;
                }
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
        /// Called when the user wants to go back to the home screen.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        public virtual void OnBackEvent(object sender, EventArgs e)
        {
            EventHandler handler = this.BackEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Configures the thumbnail control.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <param name="thumb">The thumb.</param>
        private void ConfigureThumbnailControl(IPhoto photo, ThumbnailUserControl thumb)
        {
            thumb.DisplayName = photo.DisplayName;
            thumb.Photo = photo;

            // Wire the click event to the picturebox
            thumb.ThumbnailClick += this.HandlePhotoClick;
            thumb.DeletePhotoEvent += this.HandleDeletePhotoEvent;

            // Add the thumb control to the flow panel.
            this.AddThumbnail(thumb);
        }

        /// <summary>
        /// Adds the thumbnail to the photo panel, invoking on the UI thread if necessary.
        /// </summary>
        /// <param name="thumb">The thumb.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-04</para>
        /// </remarks>
        private void AddThumbnail(ThumbnailUserControl thumb)
        {
            if (this.InvokeRequired || thumb.InvokeRequired)
            {
                Action<ThumbnailUserControl> invoker = thb => this.AddThumbnail(thb);
                this.BeginInvoke(invoker, thumb);
                return;
            }

            this.photosFlowPanel.Controls.Add(thumb);
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
            this.OnBackEvent(this, e);
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
            // Get the startup directory from the settings file
            this.addPhotosFileDialog.InitialDirectory = Environment.ExpandEnvironmentVariables(Settings.Default.LastImportDirectory);
            DialogResult fileDialogResult = this.addPhotosFileDialog.ShowDialog();
            if (fileDialogResult == DialogResult.OK)
            {
                // Cache the directory the user just picked a file from.
                Settings.Default.LastImportDirectory = Path.GetDirectoryName(this.addPhotosFileDialog.FileName);
                Settings.Default.Save();
                foreach (var fullPath in this.addPhotosFileDialog.FileNames)
                {
                    this.CurrentAlbum.AddPhoto(fullPath);
                }

                this.RefreshPhotoList();
            }
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
            var thumbnailControl = (ThumbnailUserControl)sender;
            using (var photoForm = new ViewPhotoForm(this.currentAlbum, thumbnailControl.Photo))
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

        /// <summary>
        /// Handles the delete photo event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventObjects.PhotoEventArgs"/> instance containing the event data.</param>
        private void HandleDeletePhotoEvent(object sender, EventArgs e)
        {
            this.photosFlowPanel.Controls.Remove((Control)sender);
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