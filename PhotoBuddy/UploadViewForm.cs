/***********************************************************************************
 * Author(s): Miguel Gonzales & Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: this class is responsible to show the upload photo in particular album that is
 *              currently being viewed by the user.
 * 
 ************************************************************************************/

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PhotoBuddy.Resources;

namespace TheNewPhotoBuddy
{
  public partial class UploadViewForm : Form
  {
    private string photoFilename;
    private string photoName;

    public string PhotoName { get { return photoName; } }

    public UploadViewForm()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Author(s): Miguel Gonzales & Andrea Tan
    /// 
    /// uploadViewForm is a method which takes a string type of filePath of the picture
    /// precondition: string file path
    /// postCondition: display the picture and its entities. when failed it show a warning message saying that
    ///                the picture could not be found.
    /// </summary>
    /// <param name="photoFilename">The photo to verify.</param>
    public UploadViewForm(string photoFilename)
    {
      InitializeComponent();
      this.photoFilename = photoFilename;
      this.Text = "Upload " + Path.GetFileName(photoFilename) + " - Photo Buddy";
      // Try to open the image.
      try
      {
        pictureBox1.Image = Image.FromFile(photoFilename);
      }
      catch
      {
        // File was not a valid image so abort the upload & warn the user.
        MessageBox.Show(Strings.ErrorNotPictureFile,
            Strings.AppName, MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        cancelButton_Click(this, new EventArgs());
      }
      textBox1.Text = Path.GetFileName(photoFilename);
    }

    /// <summary>
    /// Author(s): Miguel Gonzales & Andrea Tan
    /// 
    /// when cancel button is pressed the photo view form will be closed.
    /// preCondition: cancel event is executed
    /// postCondition: close the viewForm.
    /// </summary>
    /// <param name="sender">Cancel button.</param>
    /// <param name="e">The event args.</param>
    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    /// <summary>
    /// Author(s): Miguel Gonzales & Andrea Tan
    /// 
    /// method which invoked from button clicked to set the
    /// photo name from the user specified in the textbox.
    /// preCondition: continue button is pressed
    /// postCondition: assigned the photoName from textbox and close the dialog.
    /// </summary>
    /// <param name="sender">The Continue button</param>
    /// <param name="e">the event args.</param>
    private void continueButton_Click(object sender, EventArgs e)
    {
      this.photoName = textBox1.Text;
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    /// <summary>
    /// Author(s): Miguel Gonzales & Andrea Tan
    /// 
    /// Check for the enter key press and execute the continue button if it was pressed.
    /// </summary>
    /// <param name="sender">Textbox</param>
    /// <param name="e">the event args.</param>
    private void textBox1_KeyDown(object sender, KeyEventArgs e)
    {
      // See if the user pressed the enter key and if so execute the continue button.
      if (e.KeyCode == Keys.Enter)
      {
        continueButton_Click(sender, e);
      }
    }

  }
}
