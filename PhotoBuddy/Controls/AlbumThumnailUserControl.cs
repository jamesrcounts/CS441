//-----------------------------------------------------------------------
// <copyright file="AlbumThumnailUserControl.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using PhotoBuddy.EventObjects;

    /// <summary>
    /// Displays an album on the home screen.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts</para>
    ///   <para>Created: 2011-10-26</para>
    /// </remarks>
    public partial class AlbumThumnailUserControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumThumnailUserControl"/> class.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public AlbumThumnailUserControl()
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
        public event EventHandler<AlbumEventArgs> AlbumSelectedEvent;

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
                return Convert.ToInt32(this.AlbumCountLabel.Text);
            }

            set
            {
                this.AlbumCountLabel.Text = value.ToString();
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
        /// Called when the album is selected.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventObjects.AlbumEventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public virtual void OnAlbumSelectedEvent(object sender, AlbumEventArgs e)
        {
            EventHandler<AlbumEventArgs> handler = this.AlbumSelectedEvent;
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
            this.OnAlbumSelectedEvent(this, new AlbumEventArgs(this.AlbumName));
        }
    }
}
