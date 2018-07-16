using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using PropertyTrimmer.UnitTests.Helpers;

namespace PropertyTrimmer.UnitTests.ExtensionsTests
{
    [TestFixture]
    public class PropertyTrimmerTests
    {
        private FakeUser _user;

        [SetUp]
        public void SetUp() => _user = new FakeUser
        {
            Id = 1,
            FirstName = "Luke  ",
            LastName = "  Skywalker  "
        };

        [TearDown]
        public void TearDown() => _user = null;

        [Test]
        public void TrimProperty_SingleItemEmptyStringPropertyName_ShouldThrow()
        {
            //arrange
            var propName = string.Empty;

            //act
            Action result = () => PropertyTrimmer.TrimProperty(_user, propName);

            //assert
            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void TrimProperty_SingleItemNullObject_ShouldThrow()
        {
            //arrange
            var propName = "FistName";
            _user = null;

            //act
            Action result = () => PropertyTrimmer.TrimProperty(_user, propName);

            //assert
            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void TrimProperty_SingleItemValidInput_ShouldTrimOnlySpecifiedProperty()
        {
            //arrange
            var propName = "FirstName";

            //act
            PropertyTrimmer.TrimProperty(_user, propName);

            //assert
            _user.FirstName.Should().Be("Luke");
            _user.LastName.Should().Contain(" ");
        }

        [Test]
        public void TrimProperty_SingleItemNonStringProperty_ShouldDoNothing()
        {
            //arrange
            var propName = "Id";

            //act
            PropertyTrimmer.TrimProperty(_user, propName);

            //assert
            _user.FirstName.Should().Contain(" ");
            _user.LastName.Should().Contain(" ");
        }

        [Test]
        public void TrimProperty_SingleItemNonExistingProperty_ShouldDoNothing()
        {
            //arrange
            var propName = "Test";

            //act
            PropertyTrimmer.TrimProperty(_user, propName);

            //assert
            _user.FirstName.Should().Contain(" ");
            _user.LastName.Should().Contain(" ");
        }

        [Test]
        public void TrimProperty_SingleItemPropertyEmptyString_ShouldDoNothing()
        {
            //arrange
            var propName = "FistName";
            _user.FirstName = string.Empty;

            //act
            PropertyTrimmer.TrimProperty(_user, propName);

            //assert
            _user.FirstName.Should().Be(string.Empty);
        }

        [Test]
        public void TrimProperties_SingleItemNullObject_ShouldThrow()
        {
            //arrange
            _user = null;

            //act
            Action result = () => PropertyTrimmer.TrimProperties(_user);

            //assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void TrimProperties_SingleItemPropertyEmptyString_ShouldTrimOnlyPropertiesWithCorrectValues()
        {
            //arrange
            _user.FirstName = string.Empty;

            //act
            PropertyTrimmer.TrimProperties(_user);

            //assert
            _user.FirstName.Should().Be(string.Empty);
            _user.LastName.Should().Be("Skywalker");
        }

        [Test]
        public void TrimProperties_SingleItem_ShouldTrimAllStringProperties()
        {
            //arrange
            var firstNameWithoutWhiteSpaces = "Luke";
            var lastNameWithoutWhiteSpaces = "Skywalker";

            //act
            PropertyTrimmer.TrimProperties(_user);

            //assert
            _user.FirstName.Should().Be(firstNameWithoutWhiteSpaces);
            _user.LastName.Should().Be(lastNameWithoutWhiteSpaces);
        }

        [Test]
        public void TrimProperty_CollectionEmptyStringPropertyName_ShouldThrow()
        {
            //arrange
            var propName = string.Empty;
            var users = new List<FakeUser>
            {
                _user,
                new FakeUser
                {
                    Id =  2,
                    FirstName = "Boba",
                    LastName = "Fett"
                }
            };

            //act
            Action result = () => PropertyTrimmer.TrimProperty(users, propName);

            //assert
            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void TrimProperty_EmptyCollection_ShouldThrow()
        {
            //arrange
            var propName = "FirstName";
            var users = new List<FakeUser>();

            //act
            Action result = () => PropertyTrimmer.TrimProperty(users, propName);

            //assert
            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void TrimProperty_CollectionNonStringProperty_ShouldDoNothing()
        {
            //arrange
            var propName = "Id";
            var users = new List<FakeUser>
            {
                _user,
                new FakeUser
                {
                    Id =  2,
                    FirstName = "  Boba  ",
                    LastName = "Fett"
                }
            };

            //act
            PropertyTrimmer.TrimProperty(users, propName);

            //assert
            users.First().FirstName.Should().Contain(" ");
            users.Last().FirstName.Should().Contain(" ");
        }

        [Test]
        public void TrimProperty_PropertyEmptyString_ShouldDoNothing()
        {
            //arrange
            var propName = "FirstName";
            var users = new List<FakeUser>
            {
                new FakeUser
                {
                    Id =  2,
                    FirstName = string.Empty,
                    LastName = " Fett "
                }
            };

            //act
            PropertyTrimmer.TrimProperty(users, propName);

            //assert
            users.First().FirstName.Should().Be(string.Empty);
            users.Last().LastName.Should().Be(" Fett ");
        }

        [Test]
        public void TrimProperty_CollectionNonExistingProperty_ShouldDoNothing()
        {
            //arrange
            var propName = "Test";
            var users = new List<FakeUser>
            {
                new FakeUser
                {
                    Id =  2,
                    FirstName = " Boba ",
                    LastName = " Fett "
                }
            };

            //act
            PropertyTrimmer.TrimProperty(users, propName);

            //assert
            users.First().FirstName.Should().Be(users.First().FirstName);
            users.Last().LastName.Should().Be(users.First().LastName);
        }

        [Test]
        public void TrimProperty_CollectionValidInput_ShouldTrimSpecifiedProperty()
        {
            //arrange
            var propName = "FirstName";
            var users = new List<FakeUser>
            {
                _user,
                new FakeUser
                {
                    Id =  2,
                    FirstName = "  Boba  ",
                    LastName = "Fett  "
                }
            };

            //act
            PropertyTrimmer.TrimProperty(users, propName);

            //assert
            users.First().FirstName.Should().Be("Luke");
            users.Last().FirstName.Should().Be("Boba");
            users.Last().LastName.Should().Contain(" ");
        }

        [Test]
        public void TrimProperties_EmptyCollection_ShouldThrow()
        {
            //arrange
            var users = new List<FakeUser>();

            //act
            Action result = () => PropertyTrimmer.TrimProperties(users);

            //assert
            result.Should().Throw<ArgumentException>();
        }

        [Test]
        public void TrimProperties_PropertyEmptyString_ShouldTrimOnlyPropertiesWithValues()
        {
            //arrange
            var users = new List<FakeUser>
            {
                new FakeUser
                {
                    Id =  2,
                    FirstName = string.Empty,
                    LastName = " Fett "
                }
            };

            //act
            PropertyTrimmer.TrimProperties(users);

            //assert
            users.First().FirstName.Should().Be(string.Empty);
            users.First().LastName.Should().Be("Fett");
        }

        [Test]
        public void TrimProperties_Collection_ShouldTrimStringProperties()
        {
            //arrange
            var users = new List<FakeUser>
            {
                _user,
                new FakeUser
                {
                    Id =  2,
                    FirstName = "  Boba  ",
                    LastName = "  Fett  "
                }
            };

            //act
            PropertyTrimmer.TrimProperties(users);

            //assert
            users.First().FirstName.Should().Be("Luke");
            users.Last().FirstName.Should().Be("Boba");

            users.First().LastName.Should().Be("Skywalker");
            users.Last().LastName.Should().Be("Fett");
        }
    }
}
