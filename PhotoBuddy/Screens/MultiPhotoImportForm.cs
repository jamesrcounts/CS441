using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace PhotoBuddy.Screens
{
    public partial class MultiPhotoImportForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://msdn.microsoft.com/en-us/library/system.windows.forms.listview.aspx"/>
        public MultiPhotoImportForm()
        {
            InitializeComponent();

            // The default image size is 16 x 16, which sets up a larger
            // image size. 
            this.SelectedImageList.ImageSize = new Size(128, 128);
            this.SelectedImageList.TransparentColor = Color.White;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="http://msdn.microsoft.com/en-us/library/system.windows.forms.listview.aspx"/>
        /// <param name="fileName"></param>
        public void AddImageFromFile(string fileName)
        {

            // Create a new ListView control.
            ListView listView1 = this.SelectedImageListView;

            // Set the view to show details.
            listView1.View = View.LargeIcon;
            // Allow the user to edit item text.
            listView1.LabelEdit = true;
            // Allow the user to rearrange columns.
            listView1.AllowColumnReorder = true;
            // Display check boxes.
            listView1.CheckBoxes = true;
            // Select the item and subitems when selection is made.
            listView1.FullRowSelect = true;
            // Display grid lines.
            listView1.GridLines = true;
            // Sort the items in the list in ascending order.
            listView1.Sorting = SortOrder.Ascending;

            // Create three items and three sets of subitems for each item.
            string displayName = System.IO.Path.GetFileNameWithoutExtension(fileName);
            int index = listView1.Items.Count;
            ListViewItem item1 = new ListViewItem(displayName, index);
            item1.SubItems.Add(new ListViewItem.ListViewSubItem(item1, fileName));

            // Place a check mark next to the item.
            item1.Checked = true;

            //Add the items to the ListView.
            listView1.Items.Add(item1);

            // Initialize the ImageList objects with bitmaps.
            Bitmap temp = PhotoBuddy.Properties.Resources.MissingImageIcon.ToBitmap();
            Image thumbnail = Image.FromFile(fileName);
            listView1.LargeImageList.Images.Add(thumbnail);
        }

        ///public DialogResult DialogResult { get; private set; }

        public IEnumerable<KeyValuePair<string, string>> SelectedFiles { get; private set; }

        private void HandleContinueButtonClick(object sender, System.EventArgs e)
        {

            this.DialogResult = DialogResult.OK;
            IEnumerable<KeyValuePair<string, string>> fileNames = from ListViewItem listItem in this.SelectedImageListView.CheckedItems
                                                                  select new KeyValuePair<string, string>(listItem.SubItems[0].Text, listItem.SubItems[1].Text);





            this.SelectedFiles = fileNames;
            this.Close();
        }

        private void HandleCancelButtonClick(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
