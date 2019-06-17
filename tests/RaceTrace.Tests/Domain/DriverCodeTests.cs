using System;
using System.Collections.Generic;
using Core.Domain;
using RaceTrace.Tests.Attributes;
using RaceTrace.Tests.Domain.Generators;
using Xunit;

namespace RaceTrace.Tests.Domain
{
    public class DriverCodeTests
    {
        [Theory]
        [InlineData("VER")]
        [InlineData("COU")]
        public void CreateDriverCode_ShouldSucceed_WhenCodeIsValid(string code)
        {
            new DriverCode(code);
        }

        [Fact]
        public void CreateDriverCode_ShouldThrowNullArgException_WhenCodeIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DriverCode(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("CO")]
        [InlineData("COLU")]
        [InlineData("cou")]
        [InlineData("!?@")]
        [InlineData("CI9")]
        [InlineData("F_9")]
        public void CreateDriverCode_ShouldThrowArgumentException_WhenCodeIsNotThreeValidCharacters(string code)
        {
            Assert.Throws<ArgumentException>(() => new DriverCode(code));
        }

        [Theory, DomainAutoData]
        public void MatchDriverCode_ShouldSucceed_WhenCodeTheSame(DriverCodeGenerator driverCodeGenerator)
        {
            var driverCode = driverCodeGenerator.Generate();
            Assert.Equal(driverCode, new DriverCode(driverCode.Code));
        }

        [Fact]
        public void MatchDriverCode_ShouldFails_WhenCodeIsNotTheSame()
        {
            Assert.NotEqual(new DriverCode("VET"), new DriverCode("GRO"));
        }

        [Theory, DomainAutoData]
        public void SelectEntry_WithDriverCodeAsKey_WhenInDictionary(DriverCodeGenerator driverCodeGenerator, string stringValue)
        {
            var keyCode = driverCodeGenerator.Generate();
            var expectedValue = "To Match";
            var dictionary = new Dictionary<DriverCode, string>
            {
                {keyCode, expectedValue},
                {driverCodeGenerator.Generate(), stringValue},
                {driverCodeGenerator.Generate(), stringValue},
                {driverCodeGenerator.Generate(), stringValue},
            };

            var resultValue = dictionary[new DriverCode(keyCode.Code)];
            Assert.NotNull(resultValue);
            Assert.Equal(expectedValue, resultValue);
        }
    }
}
