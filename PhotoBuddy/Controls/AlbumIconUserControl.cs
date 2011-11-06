//-----------------------------------------------------------------------
// <copyright file="AlbumIconUserControl.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Controls
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using PhotoBuddy.Models;
    using PhotoBuddy.Screens;

    /// <summary>
    /// Displays an attractive Icon/Preview for albums.
    /// </summary>
    /// <remarks>
    ///   <para>Authors: Jim Counts, Miguel Gonzales.</para>
    ///   <para>Modified: 2011-11-05</para>
    /// </remarks>
    public partial class AlbumIconUserControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumIconUserControl"/> class.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts, Miguel Gonzales.</para>
        ///   <para>Modified: 2011-11-05</para>
        /// </remarks>
        public AlbumIconUserControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Occurs when an album is clicked.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public event EventHandler<EventArgs<string>> AlbumSelectedEvent;

        /// <summary>
        /// Occurs when the user makes a request to delete an album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public event EventHandler<EventArgs<string>> DeleteAlbumEvent;

        /// <summary>
        /// Occurs when a request is made to rename an album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-04</para>
        /// </remarks>
        public event EventHandler<EventArgs<IAlbum>> RenameAlbumEvent;

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        /// <value>
        /// The album.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public IAlbum Album { get; set; }

        /// <summary>
        /// Gets or sets the name of the album.
        /// </summary>
        /// <value>
        /// The name of the album.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public string AlbumName
        {
            get
            {
                return this.albumNameTextBox.Text;
            }

            set
            {
                this.albumNameTextBox.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public int Count
        {
            get
            {
                return Convert.ToInt32(this.albumCountLabel.Text, CultureInfo.CurrentCulture);
            }

            set
            {
                this.albumCountLabel.Text = value.ToString(CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Gets the width of the thumbnail.
        /// </summary>
        /// <value>
        /// The width of the thumbnail.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        public int ThumbnailWidth
        {
            get
            {
                return this.thumbnailPictureBox.Width;
            }
        }

        /// <summary>
        /// Gets the height of the thumbnail.
        /// </summary>
        /// <value>
        /// The height of the thumbnail.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-06</para>
        /// </remarks>
        public int ThumbnailHeight
        {
            get
            {
                return this.thumbnailPictureBox.Height;
            }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public Image Image
        {
            get
            {
                return this.coverPhotoPictureBox.Image;
            }

            set
            {
                this.coverPhotoPictureBox.Image = value;
            }
        }

        /// <summary>
        /// Called when an album rename is requested.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.Models.AlbumEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-04</para>
        /// </remarks>
        protected virtual void OnRenameAlbumEvent(object sender, EventArgs<IAlbum> e)
        {
            EventHandler<EventArgs<IAlbum>> handler = this.RenameAlbumEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Called when the album is selected.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventObjects.AlbumEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        protected virtual void OnAlbumSelectedEvent(object sender, EventArgs<string> e)
        {
            EventHandler<EventArgs<string>> handler = this.AlbumSelectedEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Called when the delete album event is triggered.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventObjects.AlbumEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        protected virtual void OnDeleteAlbumEvent(object sender, EventArgs<string> e)
        {
            EventHandler<EventArgs<string>> handler = this.DeleteAlbumEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
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
            this.thumbnailPanel.BackColor = Color.White;
        }

        /// <summary>
        /// Handles the cover image picture box click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        private void HandleCoverImagePictureBoxClick(object sender, EventArgs e)
        {
            this.OnAlbumSelectedEvent(this, new EventArgs<string>(this.AlbumName));
        }

        /// <summary>
        /// Handles the delete tool strip item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        ///   <para>Modified: 2011-11-05</para>
        /// </remarks>
        private void HandleDeleteToolStripItemClick(object sender, EventArgs e)
        {
            var result = CultureAwareMessageBox.Show(
                this,
                "Are you sure you want to delete this album?",
                "Delete Album?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            this.Album.Delete();
            this.OnDeleteAlbumEvent(this, new EventArgs<string>(this.AlbumName));
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
            this.thumbnailPanel.BackColor = Color.Gold;
        }

        /// <summary>
        /// Handles the rename album click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-04</para>
        /// </remarks>
        private void HandleRenameAlbumClick(object sender, EventArgs e)
        {
            this.OnRenameAlbumEvent(this, new EventArgs<IAlbum>(this.Album));
        }
    }
}
