//-----------------------------------------------------------------------
// <copyright file="Cropbox.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// A picture box that enables cropping.
    /// </summary>
    /// <seealso cref="http://msdn.microsoft.com/en-us/library/system.windows.forms.controlpaint.drawreversibleframe.aspx"/>
    public partial class CropBox : PictureBox
    {
        /// <summary>
        /// The original image used to erase old rectangles.
        /// </summary>
        private Image cachedImage;

        /// <summary>
        /// A value indicating whether the control is in drag mode.
        /// </summary>
        private bool isDrag;

        /// <summary>
        /// The image rectangle scaled to the size of the crop box boundaries.
        /// </summary>
        private Rectangle imageRectangle = Rectangle.Empty;

        /// <summary>
        /// The selected rectangle.
        /// </summary>
        private Rectangle selectionRectangle = Rectangle.Empty;

        /// <summary>
        /// The drawn rectangle.
        /// </summary>
        private Rectangle drawnRectangle = Rectangle.Empty;

        /// <summary>
        /// The point where rubber banding started relative to the screen.
        /// </summary>
        private Point startPoint = Point.Empty;

        /// <summary>
        /// The point where rubber banding started relative to the form.
        /// </summary>
        private Point rawStartPoint = Point.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="CropBox"/> class.
        /// </summary>
        public CropBox()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        public Image Photo
        {
            get
            {
                if (this.Image != null)
                {
                    return this.Image;
                }

                return this.ErrorImage;
            }

            set
            {
                this.Image = value;
                if (this.Image != null)
                {
                    this.cachedImage = value.Clone() as Image;
                }
            }
        }

        /// <summary>
        /// Gets the selected rectangle.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        public Rectangle SelectedRectangle
        {
            get
            {
                return this.drawnRectangle;
            }
        }

        /// <summary>
        /// Gets the image rectangle.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-07</para>
        /// </remarks>
        public Rectangle ImageRectangle
        {
            get
            {
                return this.imageRectangle;
            }
        }

        /// <summary>
        /// Handles the mouse down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            // Only draw for the left mouse button
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            // Extract the control
            Control control = (Control)sender;

            // Get the scaled image rectangle
            int newHeight = this.Image.Height * this.Width / this.Image.Width;
            int newWidth = this.Width;
            if (newHeight > this.Height)
            {
                // Resize with height instead
                newWidth = this.Image.Width * this.Height / this.Image.Height;
                newHeight = this.Height;
            }

            // Find the position
            int horizontalOffset = (this.Width - newWidth) / 2;

            // The image rectangle
            this.imageRectangle = new Rectangle(horizontalOffset, 0, newWidth, newHeight);

            // Is the cursor inside the image rectangle?
            var screenImageRectangle = control.RectangleToScreen(this.imageRectangle);
            var screenPoint = control.PointToScreen(e.Location);
            if (!screenImageRectangle.Contains(screenPoint))
            {
                return;
            }

            // Assert drag mode.
            this.isDrag = true;

            // Is this the first rectangle drawn?
            if (this.drawnRectangle != Rectangle.Empty)
            {
                // Reset the image.
                this.Image = this.cachedImage.Clone() as Image;
            }

            // Capture the mouse
            control.Capture = true;
            Cursor.Clip = screenImageRectangle;

            // Cache the start points
            ////this.startPoint = control.PointToScreen(new Point(e.X, e.Y));
            this.startPoint = screenPoint;
            this.rawStartPoint = e.Location;
        }

        /// <summary>
        /// Handles the mouse move.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.isDrag)
            {
                return;
            }

            // If the mouse is being dragged, undraw and redraw the rectangle as the mouse moves.
            var control = (Control)sender;

            // Hide the previous rectangle by calling the 
            // DrawReversibleFrame method with the same parameters.
            ControlPaint.DrawReversibleFrame(
                this.selectionRectangle,
                this.BackColor,
                FrameStyle.Dashed);

            // Calculate the endpoint and dimensions for the new 
            // rectangle, again using the PointToScreen method.
            ////Point endPoint = ((Control)sender).PointToScreen(new Point(e.X, e.Y));
            var screenPoint = control.PointToScreen(e.Location);
            this.selectionRectangle = new Rectangle(
                this.startPoint.X,
                this.startPoint.Y,
                screenPoint.X - this.startPoint.X,
                screenPoint.Y - this.startPoint.Y);

            // Draw the new rectangle by calling DrawReversibleFrame
            // again.  
            ControlPaint.DrawReversibleFrame(
                this.selectionRectangle,
                this.BackColor,
                FrameStyle.Dashed);
        }

        /// <summary>
        /// Handles the mouse up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            if (!this.isDrag)
            {
                return;
            }

            var control = (Control)sender;

            // If the MouseUp event occurs, the user is not dragging.
            this.isDrag = false;

            // Release the mouse
            control.Capture = false;
            Cursor.Clip = Rectangle.Empty;

            // Erase the dashed rectangle             
            ControlPaint.DrawReversibleFrame(
                this.selectionRectangle,
                this.BackColor,
                FrameStyle.Dashed);

            // Draw the rectangle to be evaluated.
            using (var g = this.CreateGraphics())
            {
                Point endPoint = control.PointToScreen(e.Location);
                int width = Math.Abs(this.selectionRectangle.Width);
                int height = Math.Abs(this.selectionRectangle.Height);
                int horizontalOffset = Math.Min(this.startPoint.X, endPoint.X);
                int verticalOffset = Math.Min(this.startPoint.Y, endPoint.Y);
                var screenRectangle = new Rectangle(
                    horizontalOffset, 
                    verticalOffset,
                    width,
                    height);
                this.drawnRectangle = control.RectangleToClient(screenRectangle);
                g.DrawRectangle(Pens.Red, this.drawnRectangle);
            }

            // Reset the selection rectangle
            this.selectionRectangle = Rectangle.Empty;
        }
    }
}
