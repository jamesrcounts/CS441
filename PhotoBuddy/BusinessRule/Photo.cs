//-----------------------------------------------------------------------
// <copyright file="Photo.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 22 2011
// Description: this Photo class is responsible in instantiation of the photo object.
//              this class also provides the mean of accessing the photo contents
//              as well as updating its contents as well. additional feature for this is
//              creating a unique hashkey for a unique picture filename.
//-----------------------------------------------------------------------
namespace PhotoBuddy.BusinessRule
{
    using System;
    using System.IO;
    using System.Security.Cryptography;  
    using System.Text;

    /// <summary>
    /// Provides methods for working with Album photos.
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Photo"/> class.
        /// </summary>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public Photo()
        {
            this.PhotoId = string.Empty;
            this.DisplayName = string.Empty;
            this.CopiedPath = string.Empty;
        }

        /// <summary>
        /// Gets or sets the photo id.
        /// </summary>
        /// <value>
        /// The photo id.
        /// </value>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public string PhotoId { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the copied path.
        /// </summary>
        /// <value>
        /// The copied path.
        /// </value>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public string CopiedPath { get; set; }

        /// <summary>
        /// Generates the photo key for the specified file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A unique string which identifies the file by its contents.</returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public static string GeneratePhotoKey(string filePath)
        {
            // Reading the bytes of the actual file contents
            byte[] source = File.ReadAllBytes(filePath);

            // Computing the SHA 256 Hash value
            using (SHA256Managed hashAlgorithm = new SHA256Managed())
            {
                byte[] hash = hashAlgorithm.ComputeHash(source);

                // Convert to HEX encoded string
                StringBuilder hashString = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    hashString.Append(hash[i].ToString("X2")); 
                }

                return hashString.ToString();
            }
        }

        /// <summary>
        /// Stores the file.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public static void StoreFile(string sourcePath, string destinationPath)
        {
            if (File.Exists(destinationPath))
            {
                return;
            }

            File.Copy(sourceFileName: sourcePath, destFileName: destinationPath, overwrite: true);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}
