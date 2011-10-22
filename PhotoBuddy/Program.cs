/***********************************************************************************
 * Author(s): Miguel Gonzales and Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: the main of the program which called mainForm.cs
 * 
 ************************************************************************************/

// References:
// The MSDN reference library at http://msdn.microsoft.com/en-us/
// The website Dot Net Pearls helped with the textbox enter key functionality
//          as well as many other useful tips:
//          http://www.dotnetperls.com/
// The Stack Overflow site was very helpful as always in getting answers to specific
//          questions, like converting a hashtable to a list.
//          http://stackoverflow.com/

using System;
using System.Windows.Forms;
using PhotoBuddy.Resources;

namespace TheNewPhotoBuddy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
        [STAThread]
        static void Main()
        {
            string title = string.Format("{0}: {1}", Environment.UserName, Strings.AppName);
            if (PhotoBuddy.ProcessChecker.IsOnlyProcess(title))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}
