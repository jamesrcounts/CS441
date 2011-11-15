//-----------------------------------------------------------------------
// <copyright file="CropPhotoControl.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Screens
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Provides a user interface to crop a photo.
    /// </summary>
    /// <remarks>
    ///   <para>Author: Jim Counts and Eric Wei</para>
    ///   <para>Created: 2011-11-05</para>
    /// </remarks>
    public partial class CropPhotoControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CropPhotoControl"/> class.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei, Miguel Gonzales.</para>
        ///   <para>Modified: 2011-11-05</para>
        /// </remarks>
        public CropPhotoControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Occurs when the user wants to abandon cropping without saving changes.
        /// </summary>
        public event EventHandler CancelEvent;

        /// <summary>
        /// Occurs when the user wants to save the cropping result.
        /// </summary>
        public event EventHandler<EventArgs<Image>> ContinueEvent;

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public Image Image
        {
            get
            {
                return this.photoCropBox.Photo;
            }

            set
            {
                this.photoCropBox.Photo = value;
            }
        }

        /// <summary>
        /// Raises the cancel event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public virtual void OnCancelEvent(object sender, EventArgs e)
        {
            EventHandler handler = this.CancelEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Raises the continue event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventArgs&lt;System.Drawing.Image&gt;"/> instance containing the event data.</param>
        public virtual void OnContinueEvent(object sender, EventArgs<Image> e)
        {
            EventHandler<EventArgs<Image>> handler = this.ContinueEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Handles the Click event of the LeftButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void HandleCancelButtonClick(object sender, EventArgs e)
        {
            this.OnCancelEvent(this, e);
        }

        /// <summary>
        /// Handles the Click event of the RightButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void HandleCropButtonClick(object sender, EventArgs e)
        {
            // Was anything selected?
            if (this.photoCropBox.SelectedRectangle == Rectangle.Empty)
            {
                return;
            }

            Image croppedImage = this.photoCropBox.CreateCroppedImage();
            this.OnContinueEvent(this, new EventArgs<Image>(croppedImage));
        }
    }
}
