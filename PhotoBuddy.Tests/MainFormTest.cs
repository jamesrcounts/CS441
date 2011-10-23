﻿//-----------------------------------------------------------------------
// <copyright file="MainFormTest.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author: Jim Counts
// Date: October 16, 2011
// Modified: October 20, 2011
// Description: Unit tests for MainForm
//-----------------------------------------------------------------------
namespace PhotoBuddy.Tests
{
    using System.IO;
    using ApprovalTests.Reporters;
    using ApprovalTests.WinForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PhotoBuddy;
    using PhotoBuddy.Common.CommonClass;
    using PhotoBuddy.Screens;

    /// <summary>
    /// A container for tests related to <see cref="MainForm"/>
    /// </summary>
    /// <remarks>
    /// <para>Author: Jim Counts</para>
    /// </remarks>
    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    public class MainFormTest
    {
        /// <summary>
        /// Generates an image of the Opening View then compares it to a previously
        /// approved image of the Opening View to confirm that the view matches its
        /// approved specification.
        /// </summary>
        /// <remarks>
        /// <para>Author: Jim Counts</para>
        /// </remarks>
        [TestMethod]
        public void ApproveOpeningView()
        {
            // Do
            var data = new FileInfo(Constants.XMLDataFilePath);
            if (data.Exists)
            {
                data.Delete();
            }

            using (var mainForm = new MainForm())
            {
                // Approve
                Approvals.Approve(mainForm);
            }
        }

        /// <summary>
        /// Approve Create Album View
        /// </summary>
        [TestMethod]
        public void ApproveCreateAlbumView()
        {
            // Do
            var data = new FileInfo(Constants.XMLDataFilePath);
            if (data.Exists)
            {
                data.Delete();
            }

            using (var mainForm = new MainFormAccessor())
            {
                mainForm.ShowScreenAccessor(mainForm.CreateAlbumView);

                // Approve
                Approvals.Approve(mainForm);
            }
        }

        /// <summary>
        /// Approve Album View
        /// </summary>
        [TestMethod]
        public void ApproveAlbumView()
        {
            // Do
            var data = new FileInfo(Constants.XMLDataFilePath);
            if (data.Exists)
            {
                data.Delete();
            }

            using (var mainForm = new MainFormAccessor())
            {
                mainForm.ShowScreenAccessor(mainForm.AlbumView);

                // Approve
                Approvals.Approve(mainForm);
            }
        }

        /// <summary>
        /// Sets the previous screen when showing new screen.
        /// </summary>
        /// <remarks>
        /// <para>Author: Jim Counts</para>
        /// </remarks>
        [TestMethod]
        public void SetPreviousScreenWhenShowingNewScreen()
        {
            // Arrange
            using (MainFormAccessor target = new MainFormAccessor())
            {
                // Act
                target.ShowScreenAccessor(target.HomeView);
                target.ShowScreenAccessor(target.AlbumView);

                // Assert
                Assert.AreEqual(3, target.PreviousViews.Count);
                Assert.AreEqual(target.AlbumView, target.PreviousViews.Pop());
                Assert.AreEqual(target.HomeView, target.PreviousViews.Pop());
                Assert.AreEqual(target.HomeView, target.PreviousViews.Pop());
            }
        }

        /// <summary>
        /// Skips the set previous screen when current screen is create album view.
        /// </summary>
        /// <remarks>
        /// <para>Author: Jim Counts</para>
        /// </remarks>
        [TestMethod]
        public void SkipSetPreviousScreenWhenCurrentScreenIsCreateAlbumView()
        {
            // Arrange
            using (var target = new MainFormAccessor())
            {
                // Act
                target.ShowScreenAccessor(target.HomeView);
                target.ShowScreenAccessor(target.CreateAlbumView);
                target.ShowScreenAccessor(target.AlbumView);

                // Assert
                Assert.AreEqual(3, target.PreviousViews.Count);
                Assert.AreEqual(target.AlbumView, target.PreviousViews.Pop());
                Assert.AreEqual(target.HomeView, target.PreviousViews.Pop());
                Assert.AreEqual(target.HomeView, target.PreviousViews.Pop());
            }
        }

        /// <summary>
        /// Sets the current view when showing view.
        /// </summary>
        /// <remarks>
        /// <para>Author: Jim Counts</para>
        /// </remarks>
        [TestMethod]
        public void SetCurrentViewWhenShowingView()
        {
            // Arrange
            using (var target = new MainFormAccessor())
            {
                // Act
                target.ShowScreenAccessor(target.AlbumView);

                // Assert
                Assert.AreEqual(target.CurrentView, target.AlbumView);
            }
        }

        /// <summary>
        /// Provides unit tests with methods to call protected <see cref="MainForm"/> methods directly.
        /// </summary>
        /// <remarks>
        /// <para>Author: Jim Counts</para>
        /// </remarks>
        private class MainFormAccessor : MainForm
        {
            /// <summary>
            /// Provides unit tests with a method to call <see cref="MainForm.ShowScreen"/> directly.
            /// </summary>
            /// <param name="screenToShow">The screen to show.</param>
            /// <remarks>
            /// <para>Author: Jim Counts</para>
            /// </remarks>
            public void ShowScreenAccessor(IScreen screenToShow)
            {
                ShowView(screenToShow);
            }
        }
    }
}
