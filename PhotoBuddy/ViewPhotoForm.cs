//-----------------------------------------------------------------------
// <copyright file="ViewPhotoForm.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 23 2011
// Description: this class is responsible to show photo or possibly multiple photos
//              that are exist in the current album that the user added.
//-----------------------------------------------------------------------
namespace PhotoBuddy
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using PhotoBuddy.Models;
    using PhotoBuddy.Screens;

    /// <summary>
    /// Displays photos full size (up to the limit of the screen size).
    /// </summary>
    public partial class ViewPhotoForm : Form
    {
        /// <summary>
        /// The album the photo belongs to.
        /// </summary>
        private readonly IAlbum album;

        /// <summary>
        /// All photos in the album.
        /// </summary>
        private readonly IList<IPhoto> allPhotosInAlbum;

        /// <summary>
        /// The photo currently displayed.
        /// </summary>
        private IPhoto currentPhoto;

        /// <summary>
        /// The current photo's index.
        /// </summary>
        private int photoIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewPhotoForm"/> class.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public ViewPhotoForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewPhotoForm"/> class.
        /// </summary>
        /// <param name="currentAlbum">The album whose photos the user is viewing.</param>
        /// <param name="photoToDisplay">the specific photo to view.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public ViewPhotoForm(IAlbum currentAlbum, IPhoto photoToDisplay)
            : this()
        {
            this.album = currentAlbum;
            this.Text = this.album.AlbumId + " - Photo Buddy";
            this.allPhotosInAlbum = new List<IPhoto>(this.album.Photos);
            this.photoIndex = this.allPhotosInAlbum.IndexOf(photoToDisplay);
            this.DisplayPhoto(this.photoIndex);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Closed"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> that contains the event data.</param>
        /// <remarks>
        /// Author: Jim Counts and Eric Wei
        /// Created: 2011-11-06
        /// </remarks>
        protected override void OnClosed(EventArgs e)
        {
            this.CloseCurrentPhoto();
            base.OnClosed(e);
        }

        /// <summary>
        /// Displays the photo at the specified index.
        /// </summary>
        /// <param name="index">The photo index in the list of photos.</param>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales, Andrea Tan, Jim Counts and Eric Wei</para>
        ///   <para>Modified: 2011-10-27</para>
        /// </remarks>
        private void DisplayPhoto(int index)
        {
            this.CloseCurrentPhoto();
            this.currentPhoto = this.allPhotosInAlbum[index];
            this.pictureBox1.Image = this.currentPhoto.Image;
            this.currentAlbumLabel.Text = Format.Culture("{0}/{1}", index, this.allPhotosInAlbum.Count);
            this.photoNameLabel.Text = this.currentPhoto.DisplayName.Replace("&", "&&");
        }

        /// <summary>
        /// Closes the current photo.
        /// </summary>
        /// <remarks>
        /// Author: Jim Counts and Eric Wei
        /// Modified: 2011-11-06
        /// </remarks>
        private void CloseCurrentPhoto()
        {
            if (this.currentPhoto != null)
            {
                this.currentPhoto.Close();
                this.currentPhoto = null;
            }
        }

        /// <summary>
        /// Handles the Click event of the backButton control.
        /// </summary>
        /// <param name="sender">The back button.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleBackButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the nextPhotoButton control.
        /// </summary>
        /// <param name="sender">The next photo button.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleNextPhotoButtonClick(object sender, EventArgs e)
        {
            this.photoIndex = this.CanGoForward() ? this.photoIndex + 1 : 0;
            this.DisplayPhoto(this.photoIndex);
        }

        /// <summary>
        /// Handles the Click event of the previousPhotoButton control.
        /// </summary>
        /// <param name="sender">Previous photo button.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandlePreviousPhotoButtonClick(object sender, EventArgs e)
        {
            this.photoIndex = this.CanGoBack() ? this.photoIndex - 1 : this.allPhotosInAlbum.Count - 1;
            this.DisplayPhoto(this.photoIndex);
        }

        /// <summary>
        /// Determines whether this instance can go forward to the next photo in the list.
        /// </summary>
        /// <returns>
        /// true if we are not at the end of the list; otherwise false.
        /// </returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private bool CanGoForward()
        {
            return this.photoIndex < this.allPhotosInAlbum.Count - 1;
        }

        /// <summary>
        /// Determines whether this instance can go back to the previous photo in the list.
        /// </summary>
        /// <returns>
        /// true if we are not at the first photo in the list; otherwise false.
        /// </returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private bool CanGoBack()
        {
            return this.photoIndex > 0;
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
            button.ForeColor = Color.FromArgb(47, 70, 102);
        }

        /// <summary>
        /// Toggles the timer.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        private void ToggleTimer(object sender, EventArgs e)
        {
            this.slideShowTimer.Enabled = !this.slideShowTimer.Enabled;
            if (this.slideShowTimer.Enabled)
            {
                this.playPauseButton.Text = ";";
                this.slideShowTimer.Start();
            }
            else
            {
                this.playPauseButton.Text = "4";
                this.slideShowTimer.Stop();
            }
        }

        /// <summary>
        /// Handles the Click event of the RenamePhotoButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void HandleRenamePhotoButtonClick(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.foundationTableLayoutPanel.Hide();

            var photoControl = new CropPhotoControl();
            photoControl.photoCropBox.Photo = this.pictureBox1.Image;
            photoControl.LeftButton.Text = "Cancel";
            photoControl.RightButton.Text = "Crop";
            photoControl.RightButton.Visible = true;
            photoControl.LeftButton.Click += (o, s) =>
            {
                photoControl.Hide();
                this.foundationTableLayoutPanel.Show();
            };
            photoControl.RightButton.Click += (o, s) =>
            {
                photoControl.Hide();
                this.foundationTableLayoutPanel.Show();

                // Get the rectangle
                Rectangle selectedRectangle = photoControl.photoCropBox.SelectedRectangle;
                if (selectedRectangle == Rectangle.Empty)
                {
                    return;
                }

                // Scale the rectangle size and position.
                var cropBoxSize = photoControl.photoCropBox.Size;
                var imageSize = this.pictureBox1.Image;

                float horizontalScale = (float)cropBoxSize.Width / imageSize.Width;
                float verticalScale = (float)cropBoxSize.Height / imageSize.Height;
                int newWidth = (int)(selectedRectangle.Width / horizontalScale);
                int newHeight = (int)(selectedRectangle.Height / verticalScale);
                int newX = (int)((float)selectedRectangle.X / horizontalScale);
                int newY = (int)((float)selectedRectangle.Y / verticalScale);
                Rectangle scaledRectangle = new Rectangle(
                    newX,
                    newY,
                    newWidth,
                    newHeight);

                // Crop the image.
                IPhoto croppedPhoto = null;
                using (var croppedImage = new Bitmap(scaledRectangle.Width, scaledRectangle.Height))
                {
                    using (var g = Graphics.FromImage(croppedImage))
                    {
                        g.DrawImage(
                            this.pictureBox1.Image,
                            new Rectangle(0, 0, croppedImage.Width, croppedImage.Height),
                            scaledRectangle,
                            GraphicsUnit.Pixel);

                        System.IO.MemoryStream mss = new System.IO.MemoryStream();
                        croppedImage.Save(mss, System.Drawing.Imaging.ImageFormat.Jpeg);
                        string tempPath = System.IO.Path.GetTempFileName();
                        System.IO.File.WriteAllBytes(tempPath, mss.ToArray());

                        croppedPhoto = this.album.AddPhoto(tempPath);
                        System.IO.File.Delete(tempPath);
                    }
                }

                this.allPhotosInAlbum.Add(croppedPhoto);
                int idx = this.allPhotosInAlbum.IndexOf(croppedPhoto);
                if (idx != -1)
                {
                    this.DisplayPhoto(idx);
                }
            };

            this.Controls.Add(photoControl);
            photoControl.Show();
            this.ResumeLayout();
        }
    }
}