//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// Author(s): Miguel Gonzales, Andrea Tan, Jim Counts, Eric Wei
// Date: Sept 28 2011
// Modified date: Oct 22 2011
// References:
// The MSDN reference library at http://msdn.microsoft.com/en-us/
// The website Dot Net Pearls helped with the textbox enter key functionality
//          as well as many other useful tips:
//          http://www.dotnetperls.com/
// The Stack Overflow site was very helpful as always in getting answers to specific
//          questions, like converting a hashtable to a list.
//          http://stackoverflow.com/
//-----------------------------------------------------------------------
namespace PhotoBuddy
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    /// Container for the main entry point for the application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <remarks>
        ///   <para>Authors: Jim Counts and Eric Wei</para>
        ///   <para>Only one process is allowed per user.  This is accomplished using an inter-process mutex, described by Jon Skeet in the article linked below.</para>
        ///   <para>When the process finds that it is not the first instance, it tries to find and show the other instance using <see cref="PhotoBuddy.ProcessChecker"/> which
        ///   is adapted from a dotnetperls article linked below.</para>
        /// </remarks>
        /// <seealso cref="http://www.yoda.arachsys.com/csharp/threads/waithandles.shtml">Jon Skeet</seealso>
        /// <seealso cref="http://www.dotnetperls.com/single-instance-windows-form">Dotnetperls</seealso>
        [STAThread]
        private static void Main()
        {
            bool firstUserInstance;
            using (Mutex mutex = new Mutex(true, Format.Invariant(@"Local\{0}:{1}", PhotoBuddy.Properties.Resources.AppName, Environment.UserName), out firstUserInstance))
            {
                if (!firstUserInstance)
                {
                    PhotoBuddy.ProcessChecker.ShowWindow();
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}