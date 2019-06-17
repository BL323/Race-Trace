using System;
using Core.Domain;
using Xunit;

namespace RaceTrace.Tests.Domain
{
    public class NameTest
    {
        [Fact]
        public void CreateName_ShouldThrowException_WhenFirstNameNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Name(null, "Taylor"));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("alison")]
        [InlineData("aLIson")]
        [InlineData("Ali6son")]
        public void CreateName_ShouldThrowException_WhenFirstNameInvalid(string firstName)
        {
            Assert.Throws<ArgumentException>(() => new Name(firstName, "Jones"));
        }

        [Fact]
        public void CreateName_ShouldThrowException_WhenSurnameNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Name("Max", null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("jones")]
        [InlineData("jJones")]
        [InlineData("1jon1es")]
        [InlineData("Jon1es")]
        public void CreateName_ShouldThrowException_WhenSurnameInvalid(string surname)
        {
            Assert.Throws<ArgumentException>(() => new Name("alison", surname));
        }
    }
}
