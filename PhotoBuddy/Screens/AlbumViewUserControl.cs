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
    using PhotoBuddy.Controls;
    using PhotoBuddy.EventObjects;
    using PhotoBuddy.Models;
    using PhotoBuddy.Properties;

    /// <summary>
    /// Displays an album
    /// </summary>
    [DebuggerDisplay("{DisplayName}")]
    public partial class AlbumViewUserControl : UserControl, IScreen
    {
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

        /////// <summary>
        /////// Occurs when the rename album button is clicked.
        /////// </summary>
        ////public event EventHandler RenameAlbumEvent;

        ///// <summary>
        ///// Occurs when the add photo button is clicked.
        ///// </summary>
        //// public event EventHandler AddPhotosEvent;

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

            this.photosFlowPanel.Controls.Clear();
            foreach (IPhoto photo in this.currentAlbum.Photos)
            {
                ThumbnailUserControl thumb = new ThumbnailUserControl()
                {
                    DisplayName = photo.DisplayName
                };

                // Store the photo object in the thumbnail tag.
                // thumbnail is a public property to set the picture box on the thumbnailUserControl.
                thumb.Thumbnail.Tag = photo;
                thumb.Thumbnail.Image = photo.Image;
                
                // Wire the click event to the picturebox
                thumb.Thumbnail.Click += this.HandlePhotoClick;
                thumb.DeletePhotoEvent += this.HandleDeletePhotoEvent;
                // Add the thumb control to the flow panel.
                this.AddThumbnail(thumb);
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
            // Get the startup directory from the settings file
            this.addPhotosFileDialog.InitialDirectory = Environment.ExpandEnvironmentVariables(Settings.Default.LastImportDirectory);
            DialogResult fileDialogResult = this.addPhotosFileDialog.ShowDialog();
            if (fileDialogResult == DialogResult.OK)
            {
                // Cache the directory the user just picked a file from.
                Settings.Default.LastImportDirectory = Path.GetDirectoryName(this.addPhotosFileDialog.FileName);
                Settings.Default.Save();

                // Put the selected files a collection of anonymous objects
                //      o := displayName, fileName
                int maxLen = Settings.Default.MaxNameLength;
                var selectedItems = from file in this.addPhotosFileDialog.FileNames
                                    select new
                                    {
                                        DisplayName = Path.GetFileNameWithoutExtension(file),
                                        Path = file
                                    };

                var selectedItemsIndex = new Dictionary<string, string>();
                foreach (var photo in this.currentAlbum.Photos)
                {
                    selectedItemsIndex.Add(photo.DisplayName, photo.FullPath);
                }

                // Loop through the anon-objs 
                foreach (var item in selectedItems)
                {
                    // select count of each displayname - 4 from dictionary.
                    int subKeyLen = Math.Min(maxLen - 4, item.DisplayName.Length);
                    string subKey = item.DisplayName.Substring(0, subKeyLen);
                    var prefixMatches = from key in selectedItemsIndex.Keys
                                        where key.StartsWith(subKey, StringComparison.OrdinalIgnoreCase)
                                        select key;

                    int prefixMatchesCount = prefixMatches.Count();
                    int keyLen = Math.Min(maxLen, item.DisplayName.Length);
                    string displayName = item.DisplayName.Substring(0, keyLen);
                    if (prefixMatchesCount != 0)
                    {
                        // replace last 4 digits with count of prefix matches and add to dictionary.
                        displayName = subKey + prefixMatchesCount;
                    }

                    selectedItemsIndex.Add(displayName, item.Path);
                }

                foreach (var item in selectedItemsIndex)
                {
                    string photoKey = Photo.GeneratePhotoKey(item.Value);
                    if (this.CurrentAlbum.ContainsPhoto(photoKey))
                    {
                        continue;
                    }

                    this.CurrentAlbum.AddPhoto(item.Key, item.Value);
                    ////IPhoto addedPhoto = this.CurrentAlbum.GetPhoto(photoKey);
                    ////this.CreateThumbnailControlForCurrentPhoto(addedPhoto);
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
            PictureBox uc = sender as PictureBox;
            using (ViewPhotoForm photoForm = new ViewPhotoForm(this.currentAlbum, (IPhoto)uc.Tag))
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
        private void HandleDeletePhotoEvent(object sender, PhotoEventArgs e)
        {
            this.photosFlowPanel.Controls.Remove((Control)sender);

            ////string photoDisplayName = e.PhotoDisplayName;
            ////IAlbum currentAlbum = this.CurrentAlbum;

            ////IPhoto photoToDelete = currentAlbum.Photos.Where(photo => photo.DisplayName == photoDisplayName).Single();
            ////currentAlbum.Repository.DeletePhoto(currentAlbum, photoToDelete);
            ////this.RefreshPhotoList();
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