using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhotoBuddy.Controls
{
    public partial class AlbumIconUserControl : UserControl
    {
        public AlbumIconUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Highlights the photo when the mouse is over it.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Oct 13 2011
        /// Modified date: Oct 23 2011
        /// </remarks>
        private void HighlightPhoto(object sender, EventArgs e)
        {
            this.panel1.BackColor = Color.Gold;
        }

        /// <summary>
        /// Removes the highlight from the photo when the mouse is no longer over it.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// Date: Oct 13 2011
        /// Modified date: Oct 23 2011
        /// </remarks>
        private void RemovePhotoHighlight(object sender, EventArgs e)
        {
            this.panel1.BackColor = Color.White;
        }
    }
}
