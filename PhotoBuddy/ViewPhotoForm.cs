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
    using System.Drawing.Imaging;
    using System.IO;
    using System.Threading.Tasks;
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
        {
            this.InitializeComponent();
            this.album = currentAlbum;
            this.Text = this.album.AlbumId + " - Photo Buddy";
            this.allPhotosInAlbum = new List<IPhoto>(this.album.Photos);
            this.photoIndex = this.allPhotosInAlbum.IndexOf(photoToDisplay);
            this.DisplayPhoto(this.photoIndex);
        }

        /// <summary>
        /// Occurs when [photo added event].
        /// </summary>
        public event EventHandler<EventArgs<IPhoto>> PhotoAddedEvent;

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
        /// Called when [photo added event].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventArgs&lt;PhotoBuddy.Models.IPhoto&gt;"/> instance containing the event data.</param>
        protected virtual void OnPhotoAddedEvent(object sender, EventArgs<IPhoto> e)
        {
            EventHandler<EventArgs<IPhoto>> handler = this.PhotoAddedEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
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
            this.currentAlbumLabel.Text = Format.Culture("{0}/{1}", index + 1, this.allPhotosInAlbum.Count);
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
        /// This function will stop the timer for the slideshow when called
        /// BUG FIX for crash found in cycle 2.
        /// Kendra Diaz
        private void StopTimer()
        {
            this.playPauseButton.Text = "4";
            this.slideShowTimer.Stop();
        }
        /// <summary>
        /// Handles the Click event of the RenamePhotoButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void HandleEditButtonClick(object sender, EventArgs e)
        {
            this.StopTimer();
            //stop slide show
            this.SuspendLayout();
            this.foundationTableLayoutPanel.Hide();

            var photoControl = new CropPhotoControl();
            photoControl.Image = this.pictureBox1.Image;
            photoControl.CancelEvent += this.CancelCrop;
            photoControl.ContinueEvent += this.ContinueCrop;
            this.Controls.Add(photoControl);
            photoControl.Dock = DockStyle.Fill;

            photoControl.Show();
            this.ResumeLayout();
        }

        /// <summary>
        /// Saves and adds an image to the current album.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>A photo object, attached to the current album.</returns>
        private IPhoto AddImageToAlbum(Image image)
        {
            // A temporary save location
            string tempPath = Path.GetTempFileName();

            // This memory stream is probably unnecessary.
            using (MemoryStream mss = new MemoryStream())
            {
                // Write the image to the temp file.
                image.Save(mss, ImageFormat.Jpeg);
                File.WriteAllBytes(tempPath, mss.ToArray());
            }
            
            // Add the new photo to the album.
            IPhoto croppedPhoto = this.album.AddPhoto(tempPath);

            // Clean up the temporary file
            File.Delete(tempPath);

            return croppedPhoto;
        }

        /// <summary>
        /// Displays the specified photo if it is part of the photo list; otherwise does nothing.
        /// </summary>
        /// <param name="photo">The photo.</param>
        private void DisplayPhoto(IPhoto photo)
        {
            this.allPhotosInAlbum.Add(photo);
            int idx = this.allPhotosInAlbum.IndexOf(photo);
            if (idx != -1)
            {
                this.DisplayPhoto(idx);
            }
        }

        /// <summary>
        /// Process a cropped image.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventArgs&lt;System.Drawing.Image&gt;"/> instance containing the event data.</param>
        private void ContinueCrop(object sender, EventArgs<Image> e)
        {
            var photoCropControl = (CropPhotoControl)sender;
            this.SuspendLayout();

            IPhoto croppedPhoto = this.AddImageToAlbum(e.Data);
            Task.Factory.StartNew(() => this.OnPhotoAddedEvent(this, new EventArgs<IPhoto>(croppedPhoto)));
            this.TearDownCropControl(photoCropControl);
            this.DisplayPhoto(croppedPhoto);

            this.ResumeLayout();
        }

        /// <summary>
        /// Tears down a crop control.
        /// </summary>
        /// <param name="photoCropControl">The photo crop control.</param>
        private void TearDownCropControl(CropPhotoControl photoCropControl)
        {
            photoCropControl.ContinueEvent -= this.ContinueCrop;
            photoCropControl.CancelEvent -= this.CancelCrop;
            photoCropControl.Hide();
            photoCropControl.Dispose();
            this.foundationTableLayoutPanel.Show();
        }

        /// <summary>
        /// Cancels the crop and shows the view again.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CancelCrop(object sender, EventArgs e)
        {
            var photoCropControl = (CropPhotoControl)sender;
            this.SuspendLayout();
            this.TearDownCropControl(photoCropControl);
            this.ResumeLayout();
        }
    }
}