//-----------------------------------------------------------------------
// <copyright file="ConstantsTest.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author: Jim Counts and Eric Wei
// Date: October 18, 2011
// Modified: October 18, 2011
// Description: Unit tests for Constants
//-----------------------------------------------------------------------
namespace PhotoBuddy.Tests
{
    using PhotoBuddy.Common;
    using System;
    using Xunit;

    /// <summary>
    /// A container for tests related to <see cref="Constants"/>
    /// </summary>
    public class ConstantsTest
    {
        /// <summary>
        /// Should read maximum album name length from configuration
        /// </summary>
        /// <remarks>
        /// Author: Jim Counts and Eric Wei
        /// </remarks>
        [Fact]
        public void ShouldReadMaxAlbumLengthFromConfig()
        {
            int actual = Constants.MaxNameLength;
            Assert.Equal(32, actual);
        }

        /// <summary>
        /// Refers to app data folder in photos folder path.
        /// </summary>
        /// <remarks>
        /// <para>Author: Jim Counts and Eric Wei</para>
        /// </remarks>
        [Fact]
        public void ReferToAppDataFolderInPhotosFolderPath()
        {
            // Arrange
            string expected = Environment.ExpandEnvironmentVariables(@"%APPDATA%\PhotoBuddy");

            // Act
            string actual = Environment.ExpandEnvironmentVariables(Constants.PhotosFolderPath);

            // Assert
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Refers to app data folder in XML path.
        /// </summary>
        /// <remarks>
        /// <para>Author: Jim Counts and Eric Wei</para>
        /// </remarks>
        [Fact]
        public void ReferToAppDataFolderInXmlPath()
        {
            // Arrange
            string expected = Environment.ExpandEnvironmentVariables(@"%APPDATA%\PhotoBuddy\PhotoBuddyData.xml");

            // Act
            string actual = Environment.ExpandEnvironmentVariables(Constants.XmlDataFilePath);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}