//-----------------------------------------------------------------------
// <copyright file="CreateAlbumUserControlTest.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Tests
{
    using ApprovalTests.Reporters;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Container for tests related to CreateAlbumUserControl
    /// </summary>
    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    public class CreateAlbumUserControlTest
    {
        /// <summary>
        /// Approves the album name too long error.
        /// </summary>
        [TestMethod]
        public void ApproveAlbumNameTooLongError()
        {
            return;
            ////// Do
            ////var target = new CreateAlbumUserControl();
            ////var albumName = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAaaaa";
            ////target.AlbumNameTextBox.Text = albumName;
            ////target.ContinueButton.PerformClick();

            ////// Verify
            ////Approvals.Approve(target);
        }
    }
}
