using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Hadouken.Tests
{
    [TestClass]
    public class FileValidatorTests
    {
        [TestMethod]
        public void IsValidFile_WithValidFile_ShouldReturnTrue()
        {
            var validFileFactory = Substitute.For<IValidFileFactory>();
            validFileFactory.Get(".csproj").Returns(new KeyValuePair<string, bool>(".csproj", true));

            var fileValidator = new FileValidator(validFileFactory);
            var validFile = fileValidator.IsValidFile(".csproj");

            Assert.IsTrue(validFile);
        }

        [TestMethod]
        public void IsValidFile_WithInValidFile_ShouldReturnFalse()
        {
            var validFileFactory = Substitute.For<IValidFileFactory>();
            validFileFactory.Get(".exe").Returns(new KeyValuePair<string, bool>(".exe", false));

            var fileValidator = new FileValidator(validFileFactory);
            var validFile = fileValidator.IsValidFile("exe");

            Assert.IsFalse(validFile);
        }
    }
}
