/***********************************************************************************
 * Author(s): Miguel Gonzales & Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: this class is responsible to read xml data elements and data attributes.
 *              in addition it provides a feature to
 *              initialized the xml data when the file xml does not exist.
 * 
 ************************************************************************************/

using System;
using System.Xml.Linq;

namespace TheNewPhotoBuddy.DataAccessLayer
{
    public class DataAccessBaseXML
    {
        private XDocument doc;

        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// this function either load the existing file into xdocument
        /// OR
        /// if the info file cannot be found then we will create the infoxml
        /// and load
        /// preCondition : takes in predifined file path from another function and if the file does not exists,
        ///                it will make one :)
        /// postCondition: return the xmlDocument to whichever function invoked this method.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public XDocument loadORinitializeInfoXML(String filepath)
        {
            try
            {
               doc = XDocument.Load(@filepath);
            }
            catch
            { 
                initializedInfoDataXMl(@filepath);

                doc = XDocument.Load(@filepath);
            }

            return doc;
        } 


        /// <summary>
        /// Author(s): Miguel Gonzales & Andrea Tan
        /// 
        /// initialized xml when the date is not there
        /// precondition: file name gets passed in from constructor
        /// postCondition: create an body template and save it into xml.
        /// </summary>
        /// <returns>XDocument with the Starbuzz data</returns>
        static void initializedInfoDataXMl(String filename)
        {


            XDocument doc = new XDocument(
                new XElement("photo_buddy",
                    new XElement("albums"
                        )));

            doc.Save(filename);
        }
    }
}
