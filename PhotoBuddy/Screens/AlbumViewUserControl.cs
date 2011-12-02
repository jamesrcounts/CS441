//-----------------------------------------------------------------------
// <copyright file="AlbumViewUserControl.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei
// Date: Sept 28 2011
// Modified date: Oct 24 2011
// Description: this class is responsible for the use control in album view which 
//              is called from mainForm to do the state changes.
//-----------------------------------------------------------------------
namespace PhotoBuddy.Screens
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Forms;
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
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        /// <remarks>Author: Jim Counts and Eric Wei</remarks>
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
            if (this.currentAlbum == null)
            {
                return;
            }

            using (Cursor.Current = Cursors.WaitCursor)
            {
                this.AddPhotos(this.currentAlbum.Photos);
            }
        }

        /// <summary>
        /// Called when the user wants to go back to the home screen.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
        /// Initializes a new instance of the <see cref="AlbumViewUserControl"/> class.
        /// </summary>
        /// <remarks><para>Author(s): Miguel Gonzales and Andrea Tan</para></remarks>
        /// <summary>
        /// Updates the album count.
        /// </summary>
        /// <param name="count">The count.</param>
        private void UpdateAlbumCount(int count)
        {
            if (this.InvokeRequired)
            {
                Action<int> invoker = this.UpdateAlbumCount;
                this.BeginInvoke(invoker, count);
                return;
            }

            this.albumSizeLabel.Text = Format.Culture("{0} photos", count);
        }

        /// <summary>
        /// Creates the thumbnail user control.
        /// </summary>
        /// <returns>A new thumbnail user control, created on the UI thread.</returns>
        private ThumbnailUserControl CreateThumbnailUserControl()
        {
            if (this.InvokeRequired)
            {
                Func<ThumbnailUserControl> invoker = this.CreateThumbnailUserControl;
                return this.Invoke(invoker) as ThumbnailUserControl;
            }

            return new ThumbnailUserControl();
        }

        /// <summary>
        /// Adds the thumbnail controls.
        /// </summary>
        /// <param name="controls">The controls.</param>
        private void AddThumbnailControls(BlockingCollection<ThumbnailUserControl> controls)
        {
            foreach (var control in controls.GetConsumingEnumerable())
            {
                this.AddThumbnail(control);
            }
        }

        /// <summary>
        /// Configures the thumbnail control.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns>A thumbnail control configured with the supplied photo.</returns>
        private ThumbnailUserControl ConfigureThumbnailControl(IPhoto photo)
        {
            var thumbnailControl = this.CreateThumbnailUserControl();
            thumbnailControl.ThumbnailClick += this.HandlePhotoClick;
            thumbnailControl.DeletePhotoEvent += this.HandleDeletePhotoEvent;
            thumbnailControl.DisplayName = photo.DisplayName;
            thumbnailControl.Photo = photo;
            return thumbnailControl;
        }

        /// <summary>
        /// Generates the thumbnail controls.
        /// </summary>
        /// <param name="photos">The photos.</param>
        /// <param name="controls">The controls.</param>
        private void GenerateThumbnailControls(BlockingCollection<IPhoto> photos, BlockingCollection<ThumbnailUserControl> controls)
        {
            try
            {
                foreach (var photo in photos.GetConsumingEnumerable())
                {
                    ThumbnailUserControl thumbnailControl = this.ConfigureThumbnailControl(photo);
                    controls.Add(thumbnailControl);
                }
            }
            finally
            {
                controls.CompleteAdding();
            }
        }

        /// <summary>
        /// Adds the thumbnail to the photo panel, invoking on the UI thread if necessary.
        /// </summary>
        /// <param name="thumb">The thumb.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
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
            this.UpdateAlbumCount(this.photosFlowPanel.Controls.Count);
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

                // Setup a pipeline
                var pathBuffer = new BlockingCollection<string>();
                var filteredPathBuffer = new BlockingCollection<Tuple<string, string>>(32);
                Task.Factory.StartNew(
                    () => this.CalculateKeys(pathBuffer, filteredPathBuffer),
                    TaskCreationOptions.LongRunning);

                var photoBuffer = new BlockingCollection<IPhoto>(32);
                Task.Factory.StartNew(
                    () => this.AddPhotos(filteredPathBuffer, photoBuffer),
                    TaskCreationOptions.LongRunning);

                var thumbnailBuffer = new BlockingCollection<ThumbnailUserControl>(32);
                Task.Factory.StartNew(
                    () => this.GenerateThumbnailControls(photoBuffer, thumbnailBuffer),
                    TaskCreationOptions.LongRunning);

                Task.Factory.StartNew(
                    () => this.AddThumbnailControls(thumbnailBuffer),
                    TaskCreationOptions.LongRunning);

                // Start filling the pipeline.
                try
                {
                    foreach (var fullPath in this.addPhotosFileDialog.FileNames)
                    {
                        pathBuffer.Add(fullPath);
                    }
                }
                finally
                {
                    pathBuffer.CompleteAdding();
                }
            }
        }

        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        private void AddPhoto(IPhoto photo)
        {
            this.AddThumbnail(this.ConfigureThumbnailControl(photo));
        }

        /// <summary>
        /// Adds the photos.
        /// </summary>
        /// <param name="photosToAdd">The photos to add.</param>
        private void AddPhotos(IEnumerable<IPhoto> photosToAdd)
        {
            var photoBuffer = new BlockingCollection<IPhoto>();
            var thumbnailBuffer = new BlockingCollection<ThumbnailUserControl>(32);
            this.photosFlowPanel.Controls.Clear();

            Task.Factory.StartNew(
                () => this.GenerateThumbnailControls(photoBuffer, thumbnailBuffer),
                TaskCreationOptions.LongRunning);
            Task.Factory.StartNew(
                () => this.AddThumbnailControls(thumbnailBuffer),
                TaskCreationOptions.LongRunning);
            try
            {
                foreach (var photo in photosToAdd)
                {
                    photoBuffer.Add(photo);
                }
            }
            finally
            {
                photoBuffer.CompleteAdding();
            }
        }

        /// <summary>
        /// Adds the photos.
        /// </summary>
        /// <param name="keysAndPaths">The paths.</param>
        /// <param name="photos">The photos.</param>
        private void AddPhotos(BlockingCollection<Tuple<string, string>> keysAndPaths, BlockingCollection<IPhoto> photos)
        {
            try
            {
                foreach (Tuple<string, string> keyAndPath in keysAndPaths.GetConsumingEnumerable())
                {
                    if (!this.CurrentAlbum.ContainsPhoto(keyAndPath.Item1))
                    {
                        var photo = this.CurrentAlbum.AddPhoto(keyAndPath.Item2);
                        photos.Add(photo);
                    }
                }
            }
            finally
            {
                photos.CompleteAdding();
            }
        }

        /// <summary>
        /// Calculates the hash key for each photo in <paramref name="paths"/>.
        /// </summary>
        /// <param name="paths">The paths.</param>
        /// <param name="keysAndPaths">The paths with thier hash keys.</param>
        /// <remarks>Inefficient bug fix: 2011-11-08 10:42 AM</remarks>
        private void CalculateKeys(BlockingCollection<string> paths, BlockingCollection<Tuple<string, string>> keysAndPaths)
        {
            try
            {
                foreach (var path in paths.GetConsumingEnumerable())
                {
                    string photoId = Photo.GeneratePhotoKey(path);
                    keysAndPaths.Add(new Tuple<string, string>(photoId, path));
                }
            }
            finally
            {
                keysAndPaths.CompleteAdding();
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
                photoForm.PhotoAddedEvent += this.AddPhoto;
                if (this.currentAlbum.AlbumId == "Search Results")
                {
                    photoForm.DisableEdit();
                }

                photoForm.ShowDialog();
                photoForm.PhotoAddedEvent -= this.AddPhoto;
            }
        }

        /// <summary>
        /// Adds the photo.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventArgs&lt;PhotoBuddy.Models.IPhoto&gt;"/> instance containing the event data.</param>
        private void AddPhoto(object sender, EventArgs<IPhoto> e)
        {
            this.AddPhoto(e.Data);
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
            this.albumSizeLabel.Text = Format.Culture("{0} photos", this.CurrentAlbum.Count);
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