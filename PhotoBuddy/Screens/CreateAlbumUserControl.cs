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
    using System.Drawing;
    using System.Windows.Forms;
    using PhotoBuddy.Common;
    using PhotoBuddy.Models;

    /// <summary>
    /// The create album view
    /// </summary>
    [DebuggerDisplay("{DisplayName}")]
    public partial class CreateAlbumUserControl : UserControl, IScreen
    {
        /// <summary>
        /// Provides access to albums and photos.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Modified: 2011-11-05</para>
        /// </remarks>
        private readonly AlbumRepository Model;

        /// <summary>
        /// The album to rename; if any.
        /// </summary>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        private IAlbum album;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAlbumUserControl"/> class.
        /// </summary>
        /// <param name="albumRepository">The album repository.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public CreateAlbumUserControl(AlbumRepository albumRepository)
        {
            this.InitializeComponent();

            this.Model = albumRepository;
            this.Dock = DockStyle.Fill;
            this.DisplayName = "Create New Album";
            this.createHeaderLabel.Text = this.DisplayName;

            // Default to creating a new album.
            this.CreateAlbumLabel.Text = "Please enter the name of the new album:";
            this.albumNameTextBox.Text = string.Empty;
            this.createHeaderLabel.Text = this.DisplayName;
            this.albumNameTextBox.Focus();
        }

        /// <summary>
        /// Occurs when the user cancels album create/edit
        /// </summary>
        public event EventHandler CancelEvent;

        /// <summary>
        /// Occurs when user decides to complete the create album action.
        /// </summary>
        public event EventHandler<EventArgs<string>> CreateAlbumEvent;

        /// <summary>
        /// Occurs when user decides to complete the rename album action.
        /// </summary>
        public event EventHandler<EventArgs<IAlbum>> RenameAlbumEvent;

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        /// <value>
        /// The album.
        /// </value>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        public IAlbum Album
        {
            get
            {
                return this.album;
            }

            set
            {
                this.album = value;
                if (this.album != null)
                {
                    // Editing an existing album.
                    this.albumNameTextBox.Text = this.album.AlbumId;
                    this.CreateAlbumLabel.Text = "Please enter the new album name:";
                    this.createHeaderLabel.Text = "Edit Album: " + this.album.AlbumId.Replace("&", "&&");
                }
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
        /// Gets the control managed by this view.
        /// </summary>
        /// <remarks>Author: Jim Counts and Eric Wei</remarks>
        public UserControl Control
        {
            get
            {
                return this;
            }
        }

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
        /// Called when the user cancels the create or edit album request.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-10-26</para>
        /// </remarks>
        public virtual void OnCancelEvent(object sender, EventArgs e)
        {
            EventHandler handler = this.CancelEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Called when the user completes the create album request.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventArgs&lt;System.String&gt;"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        public virtual void OnCreateAlbumEvent(object sender, EventArgs<string> e)
        {
            EventHandler<EventArgs<string>> handler = this.CreateAlbumEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Called when a rename album event has been initiated.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PhotoBuddy.EventArgs&lt;PhotoBuddy.Models.IAlbum&gt;"/> instance containing the event data.</param>
        /// <remarks>
        ///   <para>Author: Jim Counts and Eric Wei</para>
        ///   <para>Created: 2011-11-05</para>
        /// </remarks>
        public virtual void OnRenameAlbumEvent(object sender, EventArgs<IAlbum> e)
        {
            EventHandler<EventArgs<IAlbum>> handler = this.RenameAlbumEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
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
            this.OnCancelEvent(this, e);
        }

        /// <summary>
        /// Checks to see if the album name exists before allowing the user to rename the album and then
        /// the album is either renamed or an exception is thrown
        /// </summary>
        /// Authors: James Counts and Eric Wei
        /// Modified: Donnnellan and Diaz recognize different case and whitespace. 
        private void RenameAlbum()
        {
            if (this.Model.IsExistingAlbumName(this.albumNameTextBox.Text) == true)
            {
                this.HandleInvalidName();
                return;
            }

            this.Model.RenameAlbum(this.album.AlbumId, this.albumNameTextBox.Text);
            this.Model.SaveAlbums();
            this.OnRenameAlbumEvent(this, new EventArgs<IAlbum>(this.album));
        }

        /// <summary>
        /// REFACTORING - Display invalid name message
        /// </summary>
        /// <remarks>Authors: Kendra Diaz and Thomas Donnellan</remarks>
        private void HandleInvalidName()
        {
            var invalidAlbumNameMessage = new InvalidAlbumNameMessage();
            CultureAwareMessageBox.Show(
                this,
                invalidAlbumNameMessage.Text,
                invalidAlbumNameMessage.Caption,
                invalidAlbumNameMessage.Buttons,
                invalidAlbumNameMessage.Icon);
            return;
        }

        /// <summary>
        /// Handles the empty album name error.
        /// </summary>
        private void HandleEmptyAlbumNameError()
        {
            CultureAwareMessageBox.Show(
                                this,
                                "Album name must not be empty.",
                                "Photo Buddy - " + Application.ProductVersion,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Handles the name too long error.
        /// </summary>
        private void HandleNameTooLongError()
        {
            CultureAwareMessageBox.Show(
                                this,
                                Format.Culture("Album name is too long.  Please enter a name up to {0} characters.", Constants.MaxNameLength),
                                "Photo Buddy - " + Application.ProductVersion,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
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
        /// <para>Alterations:</para>
        /// <para>11/18 Thomas Donnellan applying whitespace check  
        /// to create album added new lines of code</para>
        /// </remarks>
        private void HandleContinueButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.albumNameTextBox.Text))
            {
                this.HandleEmptyAlbumNameError();
                return;
            }

            if (this.albumNameTextBox.Text.Length > Constants.MaxNameLength)
            {
                this.HandleNameTooLongError();
                return;
            }

            if (this.album != null)
            {
                // Renaming album - user entered the existing name so cancel the rename
                if (this.album.AlbumId == this.albumNameTextBox.Text)
                {
                    this.CancelEvent(this, e);
                    return;
                }

                this.RenameAlbum();
                return;
            }

            string rawAlbumName = this.albumNameTextBox.Text;
            if (this.Model.IsExistingAlbumName(rawAlbumName))
            {
                this.HandleInvalidName();
                return;
            }

            //// Creating a new album
            ////trim leading and trailing whitespace 
            rawAlbumName = rawAlbumName.Trim();
            this.Model.AddAlbum(rawAlbumName);
            this.Model.SaveAlbums();

            this.OnCreateAlbumEvent(this, new EventArgs<string>(rawAlbumName));
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
