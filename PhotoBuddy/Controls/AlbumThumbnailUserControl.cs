//-----------------------------------------------------------------------
// <copyright file="AlbumThumbnailUserControl.cs" company="Gold Rush">
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

    /// <summary>
    /// Displays an album on the home screen.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts</para>
    ///   <para>Created: 2011-10-26</para>
    /// </remarks>
    public partial class AlbumThumbnailUserControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumThumbnailUserControl"/> class.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public AlbumThumbnailUserControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Occurs when a request is made to rename an album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-11-04</para>
        /// </remarks>
        public event EventHandler<AlbumEventArgs> RenameAlbumEvent;

        /// <summary>
        /// Occurs when an album is clicked.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public event EventHandler<AlbumNameEventArgs> AlbumSelectedEvent;

        /// <summary>
        /// Occurs when the user makes a request to delete an album.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        public event EventHandler<AlbumNameEventArgs> DeleteAlbumEvent;

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
                return this.AlbumNameLabel.Text.Replace("&&", "&");
            }

            set
            {
                this.AlbumNameLabel.Text = value.Replace("&", "&&");
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
                return Convert.ToInt32(this.AlbumCountLabel.Text, CultureInfo.CurrentCulture);
            }

            set
            {
                this.AlbumCountLabel.Text = value.ToString(CultureInfo.CurrentCulture);
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
                return this.coverImagePictureBox.Image;
            }

            set
            {
                this.coverImagePictureBox.Image = value;
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
        public virtual void OnRenameAlbumEvent(object sender, AlbumEventArgs e)
        {
            EventHandler<AlbumEventArgs> handler = this.RenameAlbumEvent;
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
        public virtual void OnAlbumSelectedEvent(object sender, AlbumNameEventArgs e)
        {
            EventHandler<AlbumNameEventArgs> handler = this.AlbumSelectedEvent;
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
        public virtual void OnDeleteAlbumEvent(object sender, AlbumNameEventArgs e)
        {
            EventHandler<AlbumNameEventArgs> handler = this.DeleteAlbumEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
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
            this.OnAlbumSelectedEvent(this, new AlbumNameEventArgs(this.AlbumName));
        }

        /// <summary>
        /// Handles the delete tool strip item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-27</para>
        /// </remarks>
        private void HandleDeleteToolStripItemClick(object sender, EventArgs e)
        {
            this.OnDeleteAlbumEvent(this, new AlbumNameEventArgs(this.AlbumName));
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
            this.OnRenameAlbumEvent(this, new AlbumEventArgs(this.Album));
            this.AlbumName = this.Album.AlbumId;
        }
    }
}
