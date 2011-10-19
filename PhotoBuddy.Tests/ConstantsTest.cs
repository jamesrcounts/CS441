//-----------------------------------------------------------------------
// <copyright file="ConstantsTest.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Tests
{
  using System;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using TheNewPhotoBuddy.Common.CommonClass;

  /// <summary>
  /// A container for tests related to <see cref="Constants"/>
  /// </summary>
  [TestClass]
  public class ConstantsTest
  {
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
      string actual = Environment.ExpandEnvironmentVariables(Constants.XMLDataFilePath);

      // Assert
      Assert.AreEqual(expected, actual);
    }
  }
}
