//-----------------------------------------------------------------------
// <copyright file="CropPhotoControl.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Jim Counts and Eric Wei
// Date: Nov 5 2011
// Modified date: Nov 26 2011
// Description: this class is responsible in cropping photo control.
//-----------------------------------------------------------------------

namespace PhotoBuddy.Screens
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using PhotoBuddy.Models;

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
        /// Occurs when Black and White is clicked
        /// </summary>
        public event EventHandler<EventArgs<Image>> ContinueBlackAndWhiteEvent;

        /// <summary>
        /// Occurs when Black and White is clicked
        /// </summary>
        public event EventHandler<EventArgs<Image>> ContinueRotateEvent;

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
        /// This is the caller to the black and white event handler
        /// </summary>
        /// <param name="sender">Black and White Button</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containging the event data.</param>
        ////Kendra Diaz
        public virtual void OnContinueBlackAndWhiteEvent(object sender, EventArgs<Image> e)
        {
            EventHandler<EventArgs<Image>> handler = this.ContinueBlackAndWhiteEvent;
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
        public virtual void OnContinueRotateEvent(object sender, EventArgs<Image> e)
        {
            EventHandler<EventArgs<Image>> handler = this.ContinueRotateEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelEditButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void HandleCancelButtonClick(object sender, EventArgs e)
        {
            this.OnCancelEvent(this, e);
        }

        /// <summary>
        /// Handles the Click event of the ConfirmCropButton control.
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

        /// <summary>
        /// Handles the Click event of the BlackATndWhiteButton control.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data</param>
        private void Click_BlacknWhite(object sender, EventArgs e)
        {
            Bitmap copyImage = (Bitmap)this.photoCropBox.Image;
            copyImage = BlackAndWhite.MakeGrayscale3(copyImage);
            ////this.photoCropBox.Image = copyImage;  
            ////this.Invalidate();
            // NOW SAVE THE IMAGE
            this.OnContinueBlackAndWhiteEvent(this, new EventArgs<Image>(copyImage));
        }

        private void HandleRotateButtonClick(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.foundationTableLayoutPanel.Hide();

            var rotateControl = new RotatePhotoControl();
            rotateControl.Image = (Image)this.photoCropBox.Photo.Clone();
            rotateControl.originalPhoto = this.photoCropBox.Photo;
            rotateControl.CancelEvent += this.CancelRotate;
            rotateControl.ContinueEvent += this.ContinueRotate;
            this.Controls.Add(rotateControl);
            rotateControl.Dock = DockStyle.Fill;

            rotateControl.Show();
            this.ResumeLayout();
        }

        /// <summary>
        /// Cancels the rotate and shows the view again.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CancelRotate(object sender, EventArgs e)
        {
            var rotateControl = (RotatePhotoControl)sender;
            this.SuspendLayout();
            this.photoCropBox.Photo = rotateControl.originalPhoto;
            this.TearDownRotateControl(rotateControl);
            this.ResumeLayout();
        }

        /// <summary>
        /// Cancels the rotate and shows the view again.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ContinueRotate(object sender, EventArgs<Image> e)
        {
            var rotateControl = (RotatePhotoControl)sender;
            this.SuspendLayout();
            this.TearDownRotateControl(rotateControl);
            this.ResumeLayout();
            this.OnContinueRotateEvent(this, new EventArgs<Image>(e.Data));
        }

        /// <summary>
        /// Tears down a crop control.
        /// </summary>
        /// <param name="rotateControl">The photo crop control.</param>
        private void TearDownRotateControl(RotatePhotoControl rotateControl)
        {
            rotateControl.ContinueEvent -= this.ContinueRotate;
            rotateControl.CancelEvent -= this.CancelRotate;
            rotateControl.Hide();
            rotateControl.Dispose();
            this.foundationTableLayoutPanel.Show();
        }
    }
}