//-----------------------------------------------------------------------
// <copyright file="ConstantsTest.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author: Jim Counts
// Date: October 18, 2011
// Modified: October 18, 2011
// Description: Unit tests for Constants
//-----------------------------------------------------------------------
namespace PhotoBuddy.Tests
{
  using System;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using PhotoBuddy.Common;

  /// <summary>
  /// A container for tests related to <see cref="Constants"/>
  /// </summary>
  [TestClass]
  public class ConstantsTest
  {
      /// <summary>
      /// Should read maximum album name length from configuration
      /// </summary>
      /// <remarks>
      /// Author: Jim Counts and Eric Wei
      /// </remarks>
      [TestMethod]
      public void ShouldReadMaxAlbumLengthFromConfig()
      {
          int actual = Constants.MaxNameLength;
          Assert.AreEqual(32, actual);
      }

    /// <summary>
    /// Refers to app data folder in photos folder path.
    /// </summary>
    /// <remarks>
    /// <para>Author: Jim Counts</para>
    /// </remarks>
    [TestMethod]
    public void ReferToAppDataFolderInPhotosFolderPath()
    {
      // Arrange
      string expected = Environment.ExpandEnvironmentVariables(@"%APPDATA%\PhotoBuddy");

      // Act
      string actual = Environment.ExpandEnvironmentVariables(Constants.PhotosFolderPath);

      // Assert
      Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Refers to app data folder in XML path.
    /// </summary>
    /// <remarks>
    /// <para>Author: Jim Counts</para>
    /// </remarks>
    [TestMethod]
    public void ReferToAppDataFolderInXmlPath()
    {
      // Arrange
      string expected = Environment.ExpandEnvironmentVariables(@"%APPDATA%\PhotoBuddy\PhotoBuddyData.xml");

      // Act
      string actual = Environment.ExpandEnvironmentVariables(Constants.XmlDataFilePath);

      // Assert
      Assert.AreEqual(expected, actual);
    }
  }
}
