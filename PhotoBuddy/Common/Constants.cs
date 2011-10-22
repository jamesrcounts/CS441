//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
/***********************************************************************************
 * Author(s): Miguel Gonzales and Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 18 2011
 * Description: this class is responsible of the defined global static strings
 ************************************************************************************/
namespace TheNewPhotoBuddy.Common.CommonClass
{
  using System;
  using System.Windows.Forms;
  using PhotoBuddy.Properties;

  /// <summary>
  /// Container for application-wide constants.
  /// </summary>
  public sealed class Constants
  {
    /// <summary>
    /// Path to the data file, which contains information about albums and photos.
    /// </summary>
    public static readonly string XMLDataFilePath = Environment.ExpandEnvironmentVariables(Settings.Default.DataFilePath);

    /// <summary>
    /// Path to the storage location, which contains copies of photo files imported into the application.
    /// </summary>
    public static readonly string PhotosFolderPath = Environment.ExpandEnvironmentVariables(Settings.Default.PhotosFolderPath);

    /// <summary>
    /// A file-type filter for use with an <see cref="OpenFileDialog"/>
    /// </summary>
    public static readonly string Filetype = "XML (.xml)|*.xml";

      /// <summary>
      /// This is the maximum length for the name of albums and photos
      /// </summary>
    public static readonly int MaxAlbumLength = Settings.Default.MaxAlbumLength;
  }
}
