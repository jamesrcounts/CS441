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
    public class CropBox : PictureBox
    {
        /// <summary>
        /// The original image used to erase old rectangles.
        /// </summary>
        private readonly Image OriginalImage;

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
            this.OriginalImage = this.Image.Clone() as Image;
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            (this as System.ComponentModel.ISupportInitialize).BeginInit();
            this.SuspendLayout();

            // CropBox
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HandleMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HandleMouseUp);
            (this as System.ComponentModel.ISupportInitialize).EndInit();
            this.ResumeLayout(false);
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
                    this.Image = this.OriginalImage.Clone() as Image;
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

            ////// Find out which controls intersect the rectangle and 
            ////// change their color. The method uses the RectangleToScreen  
            ////// method to convert the Control's client coordinates 
            ////// to screen coordinates.
            ////Rectangle controlRectangle;
            ////for (int i = 0; i < Controls.Count; i++)
            ////{
            ////    controlRectangle = Controls[i].RectangleToScreen
            ////        (Controls[i].ClientRectangle);
            ////    if (controlRectangle.IntersectsWith(theRectangle))
            ////    {
            ////        Controls[i].BackColor = Color.BurlyWood;
            ////    }
            ////}

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
