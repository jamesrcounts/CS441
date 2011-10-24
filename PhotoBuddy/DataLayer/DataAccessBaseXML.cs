//-----------------------------------------------------------------------
// <copyright file="DataAccessBaseXML.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales and Andrea Tan
// Date: Sept 28 2011
// Modified date: Oct 9 2011
// Description: this class is responsible to read xml data elements and data attributes.
//              in addition it provides a feature to
//              initialized the xml data when the file xml does not exist.
//-----------------------------------------------------------------------
namespace PhotoBuddy.DataAccessLayer
{
    using System;
    using System.Xml.Linq;

    /// <summary>
    /// Loads or creates the XML.
    /// </summary>
    public class DataAccessBaseXML
    {
        /// <summary>
        /// Provides API access to the XML Document.
        /// </summary>
        private XDocument doc;

        /// <summary>
        /// Loads the XML.
        /// </summary>
        /// <param name="xmlFilePath">The xml file path.</param>
        /// <returns>A reference to the <see cref="XDocument"/> which provides access to the XML.</returns>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        public XDocument LoadXml(string xmlFilePath)
        {
            try
            {
                this.doc = XDocument.Load(xmlFilePath);
            }
            catch
            {
                CreateNewXmlStore(xmlFilePath);
                this.doc = XDocument.Load(xmlFilePath);
            }

            return this.doc;
        }

        /// <summary>
        /// Creates the new XML store.
        /// </summary>
        /// <param name="xmlFilePath">The XML file path.</param>
        /// <remarks>
        /// Author(s): Miguel Gonzales and Andrea Tan
        /// </remarks>
        private static void CreateNewXmlStore(string xmlFilePath)
        {
            XDocument doc = new XDocument(new XElement("photo_buddy", new XElement("albums")));
            doc.Save(xmlFilePath);
        }
    }
}