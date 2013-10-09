using ApprovalTests;
using ApprovalTests.Namers;
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

        [Fact]
        public void StartNewWhenFirst()
        {
            bool called = false;
            Program.StartApplication(
                true,
                () => { },
                () => { called = true; });
            Assert.True(called);
        }

        [Fact]
        public void ShowExistingWhenSecond()
        {
            bool called = false;
            Program.StartApplication(
                false,
                () => { called = true; },
                () => { });
            Assert.True(called);
        }
    }
}