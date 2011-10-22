/***********************************************************************************
 * Author(s): Miguel Gonzales and Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: this Photo class is responsible in instantiation of the photo object.
 *              this class also provides the mean of accessing the photo contents
 *              as well as updating its contents as well. additional feature for this is
 *              creating a unique hashkey for a unique picture filename.
 * 
 *
 ************************************************************************************/
namespace TheNewPhotoBuddy.BussinessRule
{
    using System;
    using System.Text;
    using System.Security.Cryptography;  // This is where the hash functions reside
    using System.IO;

    public class Photo
    {

        /// <summary>
        /// 
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// Photo constructor
        /// initialized photo id, display name, and the copiedPath (which is the dir of secret folder)
        /// 
        /// preCondition : none
        /// postCondition: class photo variables get initialized.
        /// </summary>
        public Photo()
        {
            PhotoId = string.Empty;
            display_name = string.Empty;
            copiedPath = string.Empty;
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
        public String PhotoId { get; set; }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// public method ID to get & assign private class 
        /// variable _ID
        /// 
        /// </summary>
        public String display_name { get; set; }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// public method ID to get & assign private class 
        /// variable _copiedPath
        /// 
        /// </summary>
        public String copiedPath { get; set; }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// this method takes in a file path of the file
        /// and calculate a unique hashkey MD5 checksum 
        /// 
        /// preCondition: fileDirectory path
        /// postCondition: create a unique hash key and return the string
        /// 
        /// </summary>
        /// <param name="fileDirectory"></param>
        /// <returns></returns>
        public static string GenerateUniqueHashPhotoKey(String filePath)
        {
            // Reading the bytes of the actual file contents
            byte[] tmpSource = File.ReadAllBytes(filePath);

            // Computing the SHA 256 Hash value
            using (SHA256Managed hashAlgorithm = new SHA256Managed())
            {
                byte[] tmpHash = hashAlgorithm.ComputeHash(tmpSource);
                // Convert to HEX encoded string
                StringBuilder sOutput = new StringBuilder(tmpHash.Length);
                for (int i = 0; i < tmpHash.Length; i++)
                {
                    sOutput.Append(tmpHash[i].ToString("X2")); // X2 formats to hexadecimal
                }
                return sOutput.ToString();
            }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// this method is made is to copy over the picture files to 
        /// our secret directory.
        /// 
        /// preCondition: sourceFilePath and destinationPath as input params
        /// 
        /// postCondition: the file from sourceDir gets copied over to the 
        ///                destinatione file path
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="destFile"></param>
        public static void CopyOverThefileToSecretDir(String filepath, String destFile)
        {
            if (File.Exists(destFile))
            {
                return;
            }

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            File.Copy(filepath, destFile, true);
        }

        /// <summary>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// 
        /// overidde method to return album id in string type 
        /// for a generic get of the album object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.display_name;
        }
    }
}
