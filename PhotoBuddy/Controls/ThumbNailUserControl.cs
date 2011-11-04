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
        public event EventHandler<PhotoEventArgs> DeletePhotoEvent;

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
                return this.NameTextBox.Text;
            }

            set
            {
                this.NameTextBox.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the thumbnail.
        /// </summary>
        /// <value>
        /// The thumbnail.
        /// </value>
        public PictureBox Thumbnail
        {
            get
            {
                return this.thumbnail;
            }
 
            set
            {
                this.thumbnail = value;
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
        public virtual void OnDeletePhotoEvent(object sender, PhotoEventArgs e)
        {
            EventHandler<PhotoEventArgs> handler = this.DeletePhotoEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
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
            this.panel1.BackColor = Color.Blue;
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
            this.panel1.BackColor = Color.White;
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

            var photo = (PhotoBuddy.Models.IPhoto)this.Thumbnail.Tag;
            photo.Album.Repository.DeletePhoto(photo.Album, photo);
  
            this.OnDeletePhotoEvent(this, new PhotoEventArgs(photo.PhotoId));
        }
    }
}
