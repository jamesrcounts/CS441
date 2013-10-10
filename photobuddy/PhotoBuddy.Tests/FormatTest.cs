using Xunit;

namespace PhotoBuddy.Tests
{
    public class FormatTest
    {
        [Fact]
        public void FormatWithCulture()
        {
            Assert.Equal("Hello World!", Format.Culture("Hello {0}!", "World"));
        }

        [Fact]
        public void FormatWithInvariant()
        {
            Assert.Equal("Goodbye... cruel world!", Format.Invariant("{0}... cruel world!", "Goodbye"));
        }
    }
}