//-----------------------------------------------------------------------
// <copyright file="CreateAlbumUserControl.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 9 2011
// Description: this class is responsible for the use control in create album which 
//             is called from mainForm to do the state changes.
//-----------------------------------------------------------------------
namespace PhotoBuddy.Screens
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Forms;
    using PhotoBuddy.Common;
    using System.Drawing;

    /// <summary>
    /// The create album view
    /// </summary>
    [DebuggerDisplay("{DisplayName}")]
    public partial class CreateAlbumUserControl : UserControl, IScreen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAlbumUserControl"/> class.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public CreateAlbumUserControl()
        {
            this.InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.DisplayName = "Create New Album";
            this.createHeaderLabel.Text = this.DisplayName;
        }

        /// <summary>
        /// Occurs when the user cancels album create/edit
        /// </summary>
        public event EventHandler CancelEvent;

        /// <summary>
        /// Occurs when user decides to complete the create/edit action.
        /// </summary>
        public event EventHandler ContinueEvent;

        /// <summary>
        /// Gets the name of the album.
        /// </summary>
        /// <value>
        /// The name of the album.
        /// </value>
        public string AlbumName { get; private set; }

        /// <summary>
        /// Gets the continue button.
        /// </summary>
        public Button ContinueButton
        {
            get
            {
                return this.continueButton;
            }
        }

        /// <summary>
        /// Gets the control managed by this view.
        /// </summary>
        /// <remarks>Author: Jim Counts</remarks>
        public UserControl Control
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Gets the user entered text.
        /// </summary>
        public string UserEnteredText { get; private set; }

        /// <summary>
        /// Gets a value indicating whether to use this control to create an album.
        /// </summary>
        /// <value>
        ///   <c>true</c> if album creation is needed; otherwise, <c>false</c> if editing is needed.
        /// </value>
        public bool AlbumCreateMode { get; private set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets the album name text box.
        /// </summary>
        public TextBox AlbumNameTextBox
        {
            get
            {
                return this.albumNameTextBox;
            }
        }

        /// <summary>
        /// Resets the form.
        /// </summary>
        /// <param name="isCreateAlbum">Bool value indicates creating a new object or editing existing.</param>
        /// <param name="theAlbumName">Name of the album if editing.</param>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales and Andrea Tan</para>
        ///   <para>reset Create form method which takes a boolean and a string album name.
        /// this function is mainly used to set focus on object label of what user control
        /// is currently at and set the label name for it.
        /// depending if it's on create it will assigned a different kind of text message,
        /// and if it's on the renaming it will use the same usercontrol but change the prompt message for it.
        /// preCondition: 2 parameters string and bool
        /// postCondition: display a necessary label depending of what state it is currently in.</para>
        /// </remarks>
        public void ResetForm(bool isCreateAlbum, string theAlbumName)
        {
            this.AlbumName = theAlbumName;
            this.AlbumCreateMode = isCreateAlbum;
            if (this.AlbumCreateMode)
            {
                // Creating a new album.
                this.createAlbumLabel.Text = "Please enter the name of the new album:";
                this.albumNameTextBox.Text = string.Empty;
                this.createHeaderLabel.Text = this.DisplayName;
            }
            else
            {
                // Editing an existing album.
                this.albumNameTextBox.Text = this.AlbumName;
                this.createAlbumLabel.Text = "Please enter the new album name for: " + this.AlbumName.Replace("&", "&&");
                this.createHeaderLabel.Text = "Edit Album: " + this.AlbumName.Replace("&", "&&");
            }

            this.albumNameTextBox.Focus();
        }

        /// <summary>
        /// Shows the view.
        /// </summary>
        /// <param name="history">The caller's history of previous views.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts</para>
        ///   <para>Note: this view does not push itself onto the history stack.</para>
        /// </remarks>
        public void ShowView(Stack<UserControl> history)
        {
            // Show myself
            this.Visible = true;

            // Important for focusing text boxes.
            this.Focus();
        }

        /// <summary>
        /// Handles the Click event of the cancelButton control.
        /// </summary>
        /// <param name="sender">The cancel button.</param>
        /// <param name="e">the event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleCancelButtonClick(object sender, EventArgs e)
        {
            // raise the create event
            this.CancelEvent(this, e);
        }

        /// <summary>
        /// Handles the Click event of the continueButton control.
        /// </summary>
        /// <param name="sender">Continue button</param>
        /// <param name="e">the event args.</param>
        /// <remarks>
        /// <para>Author(s): Miguel Gonzales and Andrea Tan</para>
        /// <para>this function is made to check when event create album is invoked
        /// the input name must not be empty otherwise there will be a message warning
        /// showing that the label is empty and must be change.
        /// preCondition: input label must not be empty string
        /// postCondition: if the album name has not been created then simply continue and
        /// eventually get added to album list. otherwise it will do nothing.</para>
        /// </remarks>
        private void HandleContinueButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.albumNameTextBox.Text))
            {
                CultureAwareMessageBox.Show(
                    this,
                    "Album name must not be empty!",
                    "Empty Album ID Issue",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (this.albumNameTextBox.Text.Length > Constants.MaxNameLength)
            {
                CultureAwareMessageBox.Show(
                    this,
                    Format.Culture("Album name is too long.  Please enter a name up to {0} characters.", Constants.MaxNameLength),
                    "Please Try Again",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!this.AlbumCreateMode)
            {
                // Renaming album - user entered the existing name so cancel the rename
                if (this.AlbumName == this.albumNameTextBox.Text)
                {
                    this.CancelEvent(this, e);
                    return;
                }
            }

            // Store the text that the user entered.
            this.UserEnteredText = this.albumNameTextBox.Text;

            // raise the create event
            if (this.ContinueEvent != null)
            {
                this.ContinueEvent(this, e);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the albumNameTextBox control.
        /// </summary>
        /// <param name="sender">The album name textbox.</param>
        /// <param name="e">the event args.</param>
        /// <remarks>
        ///   <para>Author(s): Miguel Gonzales and Andrea Tan</para>
        ///   <para>Checks to see if the user pressed the return key and if so it executes the continue button
        /// click event.</para>
        /// </remarks>
        private void HandleAlbumNameTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            // See if the user pressed the enter key.
            if (e.KeyCode == Keys.Enter)
            {
                this.HandleContinueButtonClick(sender, e);
            }
        }

        /// <summary>
        /// Change the color of button controls when the mouse enters.
        /// </summary>
        /// <param name="sender">Any Button on this form.</param>
        /// <param name="e">The event args,</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleButtonMouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.ForeColor = Color.Black;
        }

        /// <summary>
        /// Change the color of button controls when the mouse leaves.
        /// </summary>
        /// <param name="sender">Any Button on this form.</param>
        /// <param name="e">The event args.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private void HandleButtonMouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.ForeColor = Color.White;
        }
    }
}
