//-----------------------------------------------------------------------
// <copyright file="CreateAlbumUserControl.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 9 2011
// Description: this class is responsible for the use control in create album which 
//             is called from mainForm to do the state changes.
using PhotoBuddy.Common.CommonClass;
//-----------------------------------------------------------------------
namespace PhotoBuddy.Screens
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using System.Collections.Generic;

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
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.DisplayName = "Create New Album";
            this.createHeaderLabel.Text = this.DisplayName;
        }

        /// <summary>
        /// Defines a delegate to handle the Cancel Event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public delegate void CancelEventHandler(object sender, EventArgs e);

        /// <summary>
        /// Defines a delegate to handle the Continue Event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public delegate void ContinueEventHandler(object sender, EventArgs e);

        /// <summary>
        /// Occurs when the user cancels album create/edit
        /// </summary>
        public event CancelEventHandler CancelEvent;

        /// <summary>
        /// Occurs when user decides to complete the create/edit action.
        /// </summary>
        public event ContinueEventHandler ContinueEvent;

        /// <summary>
        /// Gets the name of the album.
        /// </summary>
        /// <value>
        /// The name of the album.
        /// </value>
        public string AlbumName { get; private set; }

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
        public bool InCreateMode { get; private set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

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
            this.InCreateMode = isCreateAlbum;
            if (this.InCreateMode)
            {
                // Creating a new album.
                createAlbumLabel.Text = "Please enter the name of the new album:";
                albumNameTextBox.Text = string.Empty;
                this.createHeaderLabel.Text = this.DisplayName;
            }
            else
            {
                // Editing an existing album.
                albumNameTextBox.Text = theAlbumName;
                createAlbumLabel.Text = "Please enter the new album name for: " + this.AlbumName;
                this.createHeaderLabel.Text = "Edit Album: " + theAlbumName;
            }

            albumNameTextBox.Focus();
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
            if (string.IsNullOrWhiteSpace(albumNameTextBox.Text))
            {
                MessageBox.Show(
                    "Album name must not be empty!",
                    "Empty Album ID Issue",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (albumNameTextBox.Text.Length > Constants.MaxAlbumLength)
            {
                MessageBox.Show(
                    "Album name is too long.  Please enter a name less than " + Constants.MaxAlbumLength,
                    "Album Name Length Issue",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!this.InCreateMode)
            {
                // Renaming album - user entered the existing name so cancel the rename
                if (this.AlbumName == albumNameTextBox.Text)
                {
                    this.CancelEvent(this, e);
                    return;
                }
            }

            // Store the text that the user entered.
            this.UserEnteredText = albumNameTextBox.Text;

            // raise the create event
            if (this.ContinueEvent != null)
            {
                this.ContinueEvent(this, e);
            }
        }

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
    }
}
