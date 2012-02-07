using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hadouken.Tests
{
    [TestClass]
    public class ValidFileFactoryTests
    {
        [TestMethod]
        public void Get_UnknownFileExtension_ShouldReturnUnknownFalseKeyValuePair()
        {
            var validFileFactory = new ValidFileFactory();
            var kvp = validFileFactory.Get(".exe");

            Assert.IsFalse(kvp.Value);
        }

        [TestMethod]
        public void Get_KnownFileExtension_ShouldReturnUnknownTrueKeyValuePair()
        {
            var validFileFactory = new ValidFileFactory();
            var kvp = validFileFactory.Get(".csproj");

            Assert.IsTrue(kvp.Value);
        }
    }
}
