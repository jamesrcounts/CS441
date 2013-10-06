//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 18 2011
// Description: this class is responsible of the defined global static strings
//-----------------------------------------------------------------------
namespace PhotoBuddy.Common
{
    using System;
    using PhotoBuddy.Properties;

    /// <summary>
    /// Container for application-wide constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Path to the data file, which contains information about albums and photos.
        /// </summary>
        public static readonly string XmlDataFilePath = Environment.ExpandEnvironmentVariables(Settings.Default.DataFilePath);

        /// <summary>
        /// Path to the storage location, which contains copies of photo files imported into the application.
        /// </summary>
        public static readonly string PhotosFolderPath = Environment.ExpandEnvironmentVariables(Settings.Default.PhotosFolderPath);

        /// <summary>
        /// This is the maximum length for the name of albums and photos
        /// </summary>
        public static readonly int MaxNameLength = Settings.Default.MaxNameLength;
    }
}
