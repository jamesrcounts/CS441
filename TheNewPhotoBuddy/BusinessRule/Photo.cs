/***********************************************************************************
 * Author(s): Miguel Gonzales & Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: this Photo class is responsible in instation of the photo object.
 *              this class also provides the mean of accessing the photo contents
 *              as well as updating its contents as well. additional feature for this is
 *              creating a unique hashkey for a unique picture filename.
 * 
 *
 ************************************************************************************/


using System;
using System.Text;
using System.Security.Cryptography;  // This is where the hash functions reside

namespace TheNewPhotoBuddy.BussinessRule
{
    public class Photo 
    {
        private string _ID;
        private string _display_name;
        private string _copiedPath;

        /// <summary>
        /// 
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// Photo constructor
        /// initialized photo id, display name, and the copiedPath (which is the dir of secret folder)
        /// 
        /// preCondition : none
        /// postCondition: class photo variables get initialized.
        /// </summary>
        public Photo()
        {
            _ID = "";
            _display_name = "";
            _copiedPath = "";
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// public method ID to get & assign private class 
        /// variable _ID
        /// 
        /// </summary>
        public String ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// public method ID to get & assign private class 
        /// variable _ID
        /// 
        /// </summary>
        public String display_name
        {
            get { return _display_name; }
            set { _display_name = value; }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// public method ID to get & assign private class 
        /// variable _copiedPath
        /// 
        /// </summary>
        public String copiedPath
        {
            get { return _copiedPath; }
            set { _copiedPath = value; }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
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
        public string generateUniqueHashPhotoKey(String fileDirectory)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] tmpSource;
            byte[] tmpHash;

            tmpSource = ASCIIEncoding.ASCII.GetBytes(fileDirectory); // Turn password into byte array
            tmpHash = md5.ComputeHash(tmpSource);

            StringBuilder sOutput = new StringBuilder(tmpHash.Length);
            for (int i = 0; i < tmpHash.Length; i++)
            {
                sOutput.Append(tmpHash[i].ToString());  // X2 formats to hexadecimal
            }
            return sOutput.ToString();
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
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
        public void copyOverThefileToSecretDir(String filepath, String destFile)
        {
            try
            {
                // To copy a file to another location and 
                // overwrite the destination file if it already exists.
                System.IO.File.Copy(filepath, destFile, true);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// overidde method to return album id in string type 
        /// for a generic get of the album object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this._display_name;
        }
    }
}
