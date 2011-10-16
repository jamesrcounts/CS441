//-----------------------------------------------------------------------
// <copyright file="MainFormTest.cs" company="Gold Rush">
//     Copyright (c) Gold Rush 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace PhotoBuddy.Tests
{
  using ApprovalTests.Reporters;
  using ApprovalTests.WinForms;
  using Microsoft.VisualStudio.TestTools.UnitTesting;
  using TheNewPhotoBuddy;

  /// <summary>
  /// A container for tests related to <see cref="MainForm"/>
  /// </summary>
  [TestClass]
  [UseReporter(typeof(DiffReporter))]
  public class MainFormTest
  {
    /// <summary>
    /// A short description of the test.
    /// </summary>
    [TestMethod]
    public void ApproveOpeningView()
    {
      // Do
      using (var mainForm = new MainForm())
      {
        // Approve
        Approvals.Approve(mainForm);
      }
    }
  }
}
