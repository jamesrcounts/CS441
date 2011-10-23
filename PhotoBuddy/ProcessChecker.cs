// -----------------------------------------------------------------------
// <copyright file="ProcessChecker.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace PhotoBuddy
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Checks to see if there is already an instance of Photo Buddy running for 
    /// this user.  If there is, it will bring that instance to the front of the 
    /// screen.
    /// </summary>
    /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
    /// <seealso cref="http://www.dotnetperls.com/single-instance-windows-form"/>
    internal static class ProcessChecker
    {
        /// <summary>
        /// The window title assigned to photo buddy for this user.
        /// </summary>
        private static string photoBuddyTitle;

        /// <summary>
        /// Contains signatures for C++ functions
        /// </summary>
        /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
        internal static class NativeMethods
        {

            /// <summary>
            /// Shows a window created by a different thread.
            /// </summary>
            /// <param name="hWnd">A window handle.</param>
            /// <param name="nCmdShow">How to show the window (minimized, hidden, etc...)</param>
            /// <returns>False if the window was not hidden; otherwise true.</returns>
            /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
            [DllImport("user32.dll")]
            public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

            /// <summary>
            /// Brings the thread that created the specified window to the foreground
            /// and activates the window.
            /// </summary>
            /// <param name="hWnd">A window handle.</param>
            /// <returns>true if the window was brought to the foreground; otherwise false.</returns>
            /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
            [DllImport("user32.dll")]
            public static extern bool SetForegroundWindow(IntPtr hWnd);

            /// <summary>
            /// Enumerates all the top level windows on screen until <paramref name="lpEnumFunc"/> returns false.
            /// </summary>
            /// <param name="lpEnumFunc">A predicate used to determine when to stop enumerating.</param>
            /// <param name="lParam">An argument that may be passed to the predicate.</param>
            /// <returns>true if the function completed without errors; otherwise false</returns>
            /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
            [DllImport("user32.dll")]
            public static extern bool EnumWindows(EnumWindowsProcDel lpEnumFunc,
                Int32 lParam);

            /// <summary>
            /// Retrieves the identifier of the thread that created the specified window.
            /// </summary>
            /// <param name="hWnd">A user handle.</param>
            /// <param name="lpdwProcessId">Receives the address of the thread that created the window.</param>
            /// <returns>Address of the process that created the window.</returns>
            /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
            [DllImport("user32.dll")]
            public static extern int GetWindowThreadProcessId(IntPtr hWnd, ref Int32 lpdwProcessId);

            /// <summary>
            /// Copies the text of the windows title bar into a buffer.
            /// </summary>
            /// <param name="hWnd">A window handle.</param>
            /// <param name="lpString">A buffer that will receive the text.</param>
            /// <param name="nMaxCount">Max number of characters to copy.</param>
            /// <returns>Number of characters copied, if successful; otherwise 0</returns>
            /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
            [DllImport("user32.dll")]
            public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, Int32 nMaxCount);

            /// <summary>
            /// Indicates that we want the window displayed.
            /// </summary>
            public const int SW_SHOWNORMAL = 1;
        }

        /// <summary>
        /// Allows the .NET to provide a callback to the native method EnumWindowsProc.
        /// </summary>
        /// <param name="hWnd">A window handle.</param>
        /// <param name="lParam">Optional state.</param>
        /// <returns>true if the enumeration should continue; otherwise false.</returns>
        /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
        public delegate bool EnumWindowsProcDel(IntPtr hWnd, Int32 lParam);

        /// <summary>
        /// Find and show a running window.
        /// </summary>
        /// <param name="lpEnumFunc">A predicate used to determine when to stop enumerating.</param>
        /// <param name="lParam">An argument that may be passed to the predicate.</param>
        /// <returns>true if the function completed without errors; otherwise false</returns>
        /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
        private static bool EnumWindowsProc(IntPtr hWnd, Int32 lParam)
        {
            int processId = 0;
            NativeMethods.GetWindowThreadProcessId(hWnd, ref processId);

            StringBuilder caption = new StringBuilder(1024);
            NativeMethods.GetWindowText(hWnd, caption, 1024);

            // Use IndexOf to make sure our required string is in the title.
            if (
                processId == lParam &&
                (caption.ToString().IndexOf(ProcessChecker.photoBuddyTitle, StringComparison.OrdinalIgnoreCase) != -1))
            {
                // Restore the window.

                NativeMethods.ShowWindowAsync(hWnd, NativeMethods.SW_SHOWNORMAL);
                NativeMethods.SetForegroundWindow(hWnd);
            }

            return true; // Keep this.
        }

        /// <summary>
        /// Gets a value indicating whether we are the only instance of photo buddy 
        /// for the current user.  If another instance exists, bring it to the front.
        /// </summary>
        /// <param name="forceTitle">The photo buddy title text.</param>
        /// <returns>false if no previous process was activated; true if another  process
        /// exists and should exit the current one.</returns>
        /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
        static public bool IsOnlyProcess(string forceTitle)
        {
            ProcessChecker.photoBuddyTitle = forceTitle;
            foreach (Process proc in Process.GetProcessesByName(Application.ProductName))
            {
                if (proc.Id != Process.GetCurrentProcess().Id)
                {
                    NativeMethods.EnumWindows(EnumWindowsProc, proc.Id);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Tries to show the PhotoBuddy window with the same window title as the current process.
        /// </summary>
        /// <param name="title">The title to search for.</param>
        /// <remarks>Authors: Jim Counts and Eric Wei</remarks>
        public static void ShowWindow(string title)
        {
            ProcessChecker.photoBuddyTitle = title;
            foreach (Process proc in Process.GetProcessesByName(Application.ProductName))
            {
                if (proc.Id != Process.GetCurrentProcess().Id)
                {
                    NativeMethods.EnumWindows(EnumWindowsProc, proc.Id);
                }
            }
        }
    }
}
