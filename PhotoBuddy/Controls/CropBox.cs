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
        private Image originalImage;

        /// <summary>
        /// A value indicating whether the control is in drag mode.
        /// </summary>
        private bool isDrag;

        /// <summary>
        /// The selected rectangle.
        /// </summary>
        private Rectangle theRectangle = new Rectangle(new Point(0, 0), new Size(0, 0));

        /// <summary>
        /// The drawn rectangle.
        /// </summary>
        private Rectangle drawnRectangle = Rectangle.Empty;

        /// <summary>
        /// The point where rubber banding started relative to the screen.
        /// </summary>
        private Point startPoint;

        /// <summary>
        /// The point where rubber banding started relative to the form.
        /// </summary>
        private Point rawStartPoint = Point.Empty;

        /// <summary>
        /// The point where rubber banding ended relative to the form.
        /// </summary>
        private Point rawEndPoint = Point.Empty;

        /// <summary>
        /// The original mouse clipping region.
        /// </summary>
        private Rectangle oldClip = Rectangle.Empty;

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
                return this.Image;
            }

            set
            {
                if (value == null)
                {
                    return;
                }

                this.originalImage = value.Clone() as Image;
                this.Image = value;
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
        /// Handles the mouse down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            // Set the isDrag variable to true and get the starting point 
            // by using the PointToScreen method to convert form 
            // coordinates to screen coordinates.
            if (e.Button == MouseButtons.Left)
            {
                this.isDrag = true;
                if (this.drawnRectangle != Rectangle.Empty)
                {
                    this.Image = this.originalImage.Clone() as Image;
                }
            }

            Control control = (Control)sender;

            // Calculate the startPoint by using the PointToScreen 
            // method.
            this.startPoint = control.PointToScreen(new Point(e.X, e.Y));
            this.oldClip = Cursor.Clip;
            control.Capture = true;
            Cursor.Clip = control.RectangleToScreen(this.ClientRectangle);

            // Cache the raw start point
            this.rawStartPoint = e.Location;
        }

        /// <summary>
        /// Handles the mouse move.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            // If the mouse is being dragged, 
            // undraw and redraw the rectangle as the mouse moves.
            if (this.isDrag)
            {
                // Hide the previous rectangle by calling the 
                // DrawReversibleFrame method with the same parameters.
                ControlPaint.DrawReversibleFrame(
                    this.theRectangle,
                    this.BackColor,
                    FrameStyle.Dashed);

                // Calculate the endpoint and dimensions for the new 
                // rectangle, again using the PointToScreen method.
                Point endPoint = ((Control)sender).PointToScreen(new Point(e.X, e.Y));

                // Cache the raw endpoint 
                this.rawEndPoint = e.Location;

                int width = endPoint.X - this.startPoint.X;
                int height = endPoint.Y - this.startPoint.Y;
                this.theRectangle = new Rectangle(
                    this.startPoint.X,
                    this.startPoint.Y,
                    width,
                    height);

                // Draw the new rectangle by calling DrawReversibleFrame
                // again.  
                ControlPaint.DrawReversibleFrame(
                    this.theRectangle,
                    this.BackColor,
                    FrameStyle.Dashed);
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
            ((Control)sender).Capture = false;

            // Free the mouse pointer.
            Cursor.Clip = this.oldClip;

            // Draw the rectangle to be evaluated. Set a dashed frame style 
            // using the FrameStyle enumeration.
            ControlPaint.DrawReversibleFrame(
                this.theRectangle,
                this.BackColor,
                FrameStyle.Dashed);

            // Draw a permanent rectangle on the picture box.
            using (Graphics createGraphics = this.CreateGraphics())
            {
                int width = Math.Abs(this.rawEndPoint.X - this.rawStartPoint.X);
                int height = Math.Abs(this.rawEndPoint.Y - this.rawStartPoint.Y);
                int startX = Math.Min(this.rawStartPoint.X, this.rawEndPoint.X);
                int startY = Math.Min(this.rawStartPoint.Y, this.rawEndPoint.Y);
                Rectangle rawRectangle = new Rectangle(startX, startY, width, height);
                createGraphics.DrawRectangle(Pens.Red, rawRectangle);
                this.drawnRectangle = rawRectangle;
            }

            // Reset the rectangle.
            this.theRectangle = new Rectangle(0, 0, 0, 0);
        }
    }
}
