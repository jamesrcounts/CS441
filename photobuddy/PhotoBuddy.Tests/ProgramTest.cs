using ApprovalTests;
using ApprovalTests.Namers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PhotoBuddy.Tests
{
    public class ProgramTest
    {
        [Fact]
        public void VerifyMutexName()
        {
            ApprovalResults.UniqueForUserName();
            Approvals.Verify(Program.MutexName);
        }
    }
}