using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using PropertyTrimmer.Extensions;
using PropertyTrimmer.UnitTests.Helpers;

namespace PropertyTrimmer.UnitTests.ExtensionsTests
{
    [TestFixture]
    public class TypeExtensionsTests
    {
        private FakeUser _user;

        [SetUp]
        public void SetUp() => _user = new FakeUser
        {
            Id = 1,
            FirstName = "Luke",
            LastName = "Skywalker"
        };

        [TearDown]
        public void TearDown() => _user = null;

        [Test]
        public void GetStringProperties_ReturnsAllStringProperties()
        {
            //arrange
            var type = _user.GetType();

            //act
            var result = type.GetStringProperties();

            //assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result.Select(p => p.Should().BeOfType<string>());
        }

        [Test]
        public void GetStringProperties_NoStringProperties_ReturnsEmptyCollection()
        {
            //arrange
            var obj = new { Id = 1 };

            //act
            var result = obj.GetType().GetStringProperties();

            //assert
            result.Count().Should().Be(0);
        }

        [Test]
        public void GetPropertyByName_EmptyString_Throws()
        {
            //arrange
            var fakeProp = string.Empty;

            //act
            Action result = () => _user.GetType().GetPropertyByName(fakeProp);

            //assert
            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetPropertyByName_NonExistingProperty_ReturnsNull()
        {
            //arrange
            var fakeProp = "ComeToTheDarkSideWeHaveCookies";

            //act
            var result = _user.GetType().GetPropertyByName(fakeProp);

            //assert
            result.Should().BeNull();
        }

        [Test]
        public void GetPropertyByName_ValidPropertyName_ShouldReturnProperty()
        {
            //arrange
            var firstname = "FirstName";

            //act
            var result = _user.GetType().GetPropertyByName(firstname);

            //assert
            result.Name.Should().Be(firstname);
        }
    }
}
