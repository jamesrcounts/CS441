/****************************************************************************************************
 * Author(s): Miguel Gonzales & Andrea Tan
 * Date: Oct 13 2011
 * Modified date: Oct 14 2011
 * High level Description: This is a custom control that displays a thumbnail size image with a textbox.
 *              
 *****************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TheNewPhotoBuddy.Controls
{
    /// <summary>
    /// Author(s): Miguel Gonzales & Andrea Tan
    /// Date: Oct 13 2011
    /// Modified date: Oct 14 2011
    /// </summary>
    public partial class PB_ThumbNailUserControl : UserControl
    {
        private string displayName;
        
        public string DiaplayName
        {
            get { return displayName; }
            set
            {
                displayName = value;
                this.textBox1.Text = value;
            }
        }

        public PictureBox thumbnail
        {
            get { return pictureBox1; }
            set
            {
                pictureBox1 = value;
            }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// Date: Oct 13 2011
        /// Modified date: Oct 14 2011
        /// </summary>
        public PB_ThumbNailUserControl()
        {
            InitializeComponent();    
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// Date: Oct 13 2011
        /// Modified date: Oct 14 2011
        /// 
        /// Highlights the photo when the mouse is over it.
        /// </summary>
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.panel1.BackColor = Color.Blue;
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// Date: Oct 13 2011
        /// Modified date: Oct 14 2011
        /// 
        /// Unhighlights the photo when the mouse is no longer over it.
        /// </summary>
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.panel1.BackColor = Color.White;
        }       
    }
}
