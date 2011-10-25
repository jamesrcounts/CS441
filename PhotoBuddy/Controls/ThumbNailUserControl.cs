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

    /// <summary>
    /// Displays a photo thumbnail with its name.
    /// </summary>
    /// <remarks>
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// Date: Oct 13 2011
    /// Modified date: Oct 14 2011
    /// </remarks>
    public partial class ThumbNailUserControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThumbNailUserControl"/> class.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Oct 13 2011
        /// Modified date: Oct 23 2011
        /// </remarks>
        public ThumbNailUserControl()
        {
            this.InitializeComponent();
        }

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
                return thumbnail;
            }
            set
            {
                thumbnail = value;
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
    }
}
