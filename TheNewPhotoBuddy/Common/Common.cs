/***********************************************************************************
 * Author(s): Miguel Gonzales & Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: this class is responsible of the defined global static strings.
 * 
 *
 ************************************************************************************/

namespace TheNewPhotoBuddy.Common.CommonClass
{
    public sealed class Constants
    {
        public static string XMLDataFilePath = 
            System.Windows.Forms.Application.StartupPath + @"\infoData.xml";
        public static string photosFolderPath = 
            System.Windows.Forms.Application.StartupPath + @"\photoPuppy\";
        public static string filetype = "XML (.xml)|*.xml";
    }
}
