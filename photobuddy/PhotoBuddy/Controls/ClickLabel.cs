//-----------------------------------------------------------------------
// <copyright file="ClickLabel.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 22 2011
// High level Description: this class is a label control used to display the names of albums and photos 
//                         in the albums and photo lists. It defines mouseover effects of the label 
//                         so each label properties do not need to be set individually.
//-----------------------------------------------------------------------
namespace PhotoBuddy.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Customized label control with mouse over effects.
    /// </summary>
    /// <remarks>
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// Date: Sept 28 2011
    /// Modified date: Oct 22 2011
    /// </remarks>
    public class ClickLabel : Label
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClickLabel"/> class.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Sept 28 2011
        /// Modified date: Oct 9 2011
        /// </remarks>
        public ClickLabel()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Sept 28 2011
        /// Modified date: Oct 9 2011
        /// </remarks>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // PB_ClickLabel           
            this.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AutoEllipsis = true;
            this.Font = new System.Drawing.Font(
                "Microsoft Sans Serif", 
                10.8F, 
                System.Drawing.FontStyle.Regular, 
                System.Drawing.GraphicsUnit.Point, 
                (byte)0);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(10);
            this.MaximumSize = new System.Drawing.Size(210, 30);
            this.Size = new System.Drawing.Size(210, 30);
            this.MouseEnter += new System.EventHandler(this.HandleClickableLabelMouseEnter);
            this.MouseLeave += new System.EventHandler(this.HandleClickableLabelMouseLeave);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Changes label text forecolor on mouseover.
        /// </summary>
        /// <param name="sender">the label</param>
        /// <param name="e">event args</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Sept 28 2011
        /// Modified date: Oct 9 2011
        /// </remarks>
        private void HandleClickableLabelMouseEnter(object sender, EventArgs e)
        {
            this.ForeColor = Color.RoyalBlue;
        }

        /// <summary>
        /// Changes label text forecolor back when the mouse is not over the label.
        /// </summary>
        /// <param name="sender">the label</param>
        /// <param name="e">event args</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Sept 28 2011
        /// Modified date: Oct 9 2011
        /// </remarks>
        private void HandleClickableLabelMouseLeave(object sender, EventArgs e)
        {
            this.ForeColor = Color.Black;
        }        
    }
}