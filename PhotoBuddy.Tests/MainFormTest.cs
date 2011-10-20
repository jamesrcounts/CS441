//-----------------------------------------------------------------------
// <copyright file="MainFormTest.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author: Jim Counts
// Date: October 16, 2011
// Modified: October 17, 2011
// Description: Unit tests for MainForm
//-----------------------------------------------------------------------
namespace PhotoBuddy.Tests
{
    using ApprovalTests.Reporters;
    using ApprovalTests.WinForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TheNewPhotoBuddy;
    using TheNewPhotoBuddy.Screens;

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
            using (var mainForm = new MainForm())
            {
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
                Assert.AreEqual(target.HomeView, target.PreviousViews.Peek());
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
                Assert.AreEqual(target.HomeView, target.PreviousViews.Peek());
            }
        }

        /////// <summary>
        /////// Refreshes the album view list when showing opening view.
        /////// </summary>
        /////// <remarks>
        /////// <para>Author: Jim Counts</para>
        /////// </remarks>
        ////[TestMethod]
        ////public void RefreshAlbumViewListWhenShowingOpeningView()
        ////{
        ////    // Arrange
        ////    mock
        ////    int callCount = 0;
        ////    var homeScreenStub = new SHomeScreenUserControl()
        ////    {
        ////        CallBase = true
        ////    };
        ////    var homeScreenMole = new MHomeScreenUserControl(homeScreenStub)
        ////    {
        ////        RefreshAlbumViewListAlbums = albums => callCount++
        ////    };
        ////    var target = new MainFormAccessor();
        ////    var mainFormMole = new MMainForm(target)
        ////    {
        ////        HomeViewGet = () => homeScreenMole
        ////    };

        ////    // Act
        ////    target.ShowScreenAccessor(target.HomeView);

        ////    // Assert
        ////    Assert.AreEqual(1, callCount);
        ////}

        /////// <summary>
        /////// Skips the refresh album view list when showing other views.
        /////// </summary>
        /////// <remarks>
        /////// <para>Author: Jim Counts</para>
        /////// </remarks>
        ////[TestMethod]
        ////public void SkipRefreshAlbumViewListWhenShowingOtherViews()
        ////{
        ////    // Arrange
        ////    var viewMock = new Mock<IScreen> { DefaultValue = DefaultValue.Mock };
        ////        RefreshAlbumViewListAlbums = albums => callCount++
        ////    };
        ////    var target = new MainFormAccessor();
        ////    var mainFormMole = new MMainForm(target)
        ////    {
        ////        HomeViewGet = () => homeScreenMole
        ////    };

        ////    // Act
        ////    target.ShowScreenAccessor(target.AlbumView);

        ////    // Assert
        ////    Assert.AreEqual(0, callCount);
        ////}

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
