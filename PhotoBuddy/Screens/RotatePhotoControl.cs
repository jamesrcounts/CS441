using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhotoBuddy.Screens
{
    public partial class RotatePhotoControl : UserControl
    {
        public RotatePhotoControl()
        {
            InitializeComponent();
        }

        public Image originalPhoto { get; set; }

        /// <summary>
        /// Occurs when the user wants to abandon rotation without saving changes.
        /// </summary>
        public event EventHandler CancelEvent;

        /// <summary>
        /// Occurs when the user wants to save the rotation result.
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
                return this.photoRotateBox.Image;
            }

            set
            {
                this.photoRotateBox.Image = value;
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

        private void SaveRotateButton_Click(object sender, EventArgs e)
        {
            this.OnContinueEvent(this, new EventArgs<Image>(this.Image));
        }

        private void CancelRotateButton_Click(object sender, EventArgs e)
        {
            this.OnCancelEvent(this, e);
        }

        private void FlipHorizontalButton_Click(object sender, EventArgs e)
        {
            Image flipped = this.photoRotateBox.Image;
            if (this.Image != null)
            {
                this.photoRotateBox.Image = null;
                flipped.RotateFlip(RotateFlipType.RotateNoneFlipX);
                this.photoRotateBox.Image = flipped;
            }
        }

        private void FlipVerticalButton_Click(object sender, EventArgs e)
        {
            Image flipped = this.photoRotateBox.Image;
            if (this.Image != null)
            {
                this.photoRotateBox.Image = null;
                flipped.RotateFlip(RotateFlipType.RotateNoneFlipY);
                this.photoRotateBox.Image = flipped;
            }
        }

        private void RotateLeftButton_Click(object sender, EventArgs e)
        {
            Image flipped = this.photoRotateBox.Image;
            if (this.Image != null)
            {
                this.photoRotateBox.Image = null;
                flipped.RotateFlip(RotateFlipType.Rotate270FlipNone);
                this.photoRotateBox.Image = flipped;
            }
        }

        private void RotateRightButton_Click(object sender, EventArgs e)
        {
            Image flipped = this.photoRotateBox.Image;
            if (this.Image != null)
            {
                this.photoRotateBox.Image = null;
                flipped.RotateFlip(RotateFlipType.Rotate90FlipNone);
                this.photoRotateBox.Image = flipped;
            }
        }
    }
}
