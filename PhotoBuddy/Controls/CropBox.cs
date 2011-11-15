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
        /// Scales a length or width.
        /// </summary>
        /// <param name="value">Length or width.</param>
        /// <param name="scale">The percent scale.</param>
        /// <returns>The scaled value.</returns>
        public static int ScaleDimension(int value, float scale)
        {
            return (int)(value / scale);
        }

        /// <summary>
        /// Scales the X or Y value of a coordinate.
        /// </summary>
        /// <param name="selectedOffset">The selected rectangle's offset.</param>
        /// <param name="imageOffset">The image rectangle's offset.</param>
        /// <param name="scale">The percent scale.</param>
        /// <returns>The scaled value.</returns>
        public static int ScaleOffset(int selectedOffset, int imageOffset, float scale)
        {
            return (int)((selectedOffset - imageOffset) / scale);
        }
     
        /// <summary>
        /// Gets the percent scale between the image as displayed in the photo crop box,
        /// and the actual image size.
        /// </summary>
        /// <returns>The scaling factor.</returns>
        public float CalculatePercentScale()
        {
            return (float)this.ImageRectangle.Width / this.Image.Width;
        }

        /// <summary>
        /// Gets the scaled selection rectangle.
        /// </summary>
        /// <returns>A cropping rectangle, scaled and positioned for the actual image dimensions</returns>
        public Rectangle CalculateScaledSelectionRectangle()
        {
            float scale = (float)this.ImageRectangle.Width / this.Image.Width;
            return new Rectangle(
                ScaleOffset(this.SelectedRectangle.X, this.ImageRectangle.X, scale),
                ScaleOffset(this.SelectedRectangle.Y, this.ImageRectangle.Y, scale),
                ScaleDimension(this.SelectedRectangle.Width, scale),
                ScaleDimension(this.SelectedRectangle.Height, scale));
        }

        /// <summary>
        /// Gets the cropped image.
        /// </summary>
        /// <returns>A new image, filled with the portion of the image the user selected.</returns>
        public Image CreateCroppedImage()
        {
            var scaledRectangle = this.CalculateScaledSelectionRectangle();
            Image croppedImage = new Bitmap(scaledRectangle.Width, scaledRectangle.Height);
            using (var g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(
                    this.Image,
                    new Rectangle(0, 0, croppedImage.Width, croppedImage.Height),
                    scaledRectangle,
                    GraphicsUnit.Pixel);
            }

            return croppedImage;
        }

        /// <summary>
        /// Scales the image dimensions to fit in the CropBox window.  
        /// </summary>
        /// <returns>A <see cref="Size"/> that describes the width and height of the image as it appears in
        /// the crop window.</returns>
        private Size ScaleImageSizeToFit()
        {
            int height = this.Image.Height * this.Width / this.Image.Width;
            int width = this.Width;
            if (height > this.Height)
            {
                // Resize with height instead
                width = this.Image.Width * this.Height / this.Image.Height;
                height = this.Height;
            }

            return new Size(width, height);
        }

        /// <summary>
        /// Calculate the size and position of the image rectangle as displayed in the crop box.  Then
        /// sets the ImageRectangle property.
        /// </summary>
        private void SetupImageRectangle()
        {
            var scaledSize = this.ScaleImageSizeToFit();

            // Find the position
            int horizontalOffset = (this.Width - scaledSize.Width) / 2;
            var scaledPosition = new Point(horizontalOffset, 0);

            // The image rectangle
            this.imageRectangle = new Rectangle(scaledPosition, scaledSize);
        }

        /// <summary>
        /// Resets the previously drawn rectangle.
        /// </summary>
        private void ResetPreviousDrawnRectangle()
        {
            if (this.drawnRectangle != Rectangle.Empty)
            {
                // Reset the image.
                this.Image = this.cachedImage.Clone() as Image;
            }
        }

        /// <summary>
        /// Starts drawing the rectangle.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="screenImageRectangle">The screen image rectangle.</param>
        /// <param name="screenPoint">The screen point.</param>
        private void StartDrawing(Control control, Rectangle screenImageRectangle, Point screenPoint)
        {
            // Assert drag mode.
            this.isDrag = true;

            // Is this the first rectangle drawn?
            this.ResetPreviousDrawnRectangle();

            // Capture the mouse
            control.Capture = true;
            Cursor.Clip = screenImageRectangle;

            // Cache the start points
            this.startPoint = screenPoint;
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

            // Get the scaled image rectangle
            this.SetupImageRectangle();

            // Is the cursor inside the image rectangle?
            var control = (Control)sender;
            var screenImageRectangle = control.RectangleToScreen(this.ImageRectangle);
            var screenPoint = control.PointToScreen(e.Location);
            if (!screenImageRectangle.Contains(screenPoint))
            {
                return;
            }

            this.StartDrawing(control, screenImageRectangle, screenPoint);
        }

        /// <summary>
        /// Refreshes the selection rectangle.
        /// </summary>
        /// <param name="screenPoint">The screen point.</param>
        private void RefreshSelectionRectangle(Point screenPoint)
        {
            this.selectionRectangle = new Rectangle(
                            this.startPoint.X,
                            this.startPoint.Y,
                            screenPoint.X - this.startPoint.X,
                            screenPoint.Y - this.startPoint.Y);
        }

        /// <summary>
        /// Handles the mouse move.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        /// <remarks>If the mouse is being dragged, undraw and redraw the rectangle as the mouse moves.</remarks>
        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.isDrag)
            {
                return;
            }
                        
            // Hide the previous rectangle by calling the 
            // DrawReversibleFrame method with the same parameters.
            ControlPaint.DrawReversibleFrame(
                this.selectionRectangle,
                this.BackColor,
                FrameStyle.Dashed);

            // Calculate the endpoint and dimensions for the new 
            // rectangle, again using the PointToScreen method.
            var control = (Control)sender;
            var screenPoint = control.PointToScreen(e.Location);
            this.RefreshSelectionRectangle(screenPoint);

            // Draw the new rectangle by calling DrawReversibleFrame
            // again.  
            ControlPaint.DrawReversibleFrame(
                this.selectionRectangle,
                this.BackColor,
                FrameStyle.Dashed);
        }

        /// <summary>
        /// Refreshes the drawn rectangle.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="endPoint">The end point.</param>
        private void RefreshDrawnRectangle(Control control, Point endPoint)
        {
            var screenRectangle = new Rectangle(
                                Math.Min(this.startPoint.X, endPoint.X),
                                Math.Min(this.startPoint.Y, endPoint.Y),
                                Math.Abs(this.selectionRectangle.Width),
                                Math.Abs(this.selectionRectangle.Height));
            this.drawnRectangle = control.RectangleToClient(screenRectangle);
        }

        /// <summary>
        /// Draws the crop rectangle.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="control">The control.</param>
        private void DrawCropRectangle(Point location, Control control)
        {
            // Draw the rectangle to be evaluated.
            using (var g = this.CreateGraphics())
            {
                var endPoint = control.PointToScreen(location);
                this.RefreshDrawnRectangle(control, endPoint);
                g.DrawRectangle(Pens.Red, this.drawnRectangle);
            }
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

            // If the MouseUp event occurs, the user is not dragging.
            this.isDrag = false;

            // Release the mouse
            var control = (Control)sender;
            control.Capture = false;
            Cursor.Clip = Rectangle.Empty;

            // Erase the dashed rectangle             
            ControlPaint.DrawReversibleFrame(
                this.selectionRectangle,
                this.BackColor,
                FrameStyle.Dashed);

            this.DrawCropRectangle(e.Location, control);

            // Reset the selection rectangle
            this.selectionRectangle = Rectangle.Empty;
        }
    }
}
