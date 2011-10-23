/****************************************************************************************************
 * Author(s): Miguel Gonzales and Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * High level Description: this class is a label control used to display the names of albums & photos 
 *                         in the albums and photo lists. It defines mouseover effects of the label 
 *                         so each label properties do not need to be set individually.
 *              
 *****************************************************************************************************/

using System;
using System.Windows.Forms;
using System.Drawing;

namespace PhotoBuddy.Controls
{
    /// <summary>
    /// Author(s): Miguel Gonzales and Andrea Tan
    /// Date: Sept 28 2011
    /// Modified date: Oct 9 2011
    /// </summary>
    public class PB_ClickLabel : Label
    {
        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Sept 28 2011
        /// Modified date: Oct 9 2011
        /// </summary>
        public PB_ClickLabel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Sept 28 2011
        /// Modified date: Oct 9 2011
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PB_ClickLabel
            // 
            this.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.AutoEllipsis = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(10);
            this.MaximumSize = new System.Drawing.Size(210, 30);
            this.Size = new System.Drawing.Size(210, 30);
            this.MouseEnter += new System.EventHandler(this.ClickableLabel_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ClickableLabel_MouseLeave);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Sept 28 2011
        /// Modified date: Oct 9 2011
        /// 
        /// Changes label text forecolor on mouseover.
        /// </summary>
        /// <param name="sender">the label</param>
        /// <param name="e">event args</param>
        private void ClickableLabel_MouseEnter(object sender, EventArgs e)
        {
            this.ForeColor = Color.RoyalBlue;
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Sept 28 2011
        /// Modified date: Oct 9 2011
        /// 
        /// Changes label text forecolor back when the mouse is not over the lable.
        /// </summary>
        /// <param name="sender">the label</param>
        /// <param name="e">event args</param>
        private void ClickableLabel_MouseLeave(object sender, EventArgs e)
        {
            this.ForeColor = Color.Black;
        }
    }
}
