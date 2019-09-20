using System;
using Whiteboard.Registration.Domain.Tiny;
using Xunit;

namespace Whiteboard.Registration.Domain.Tests.Tiny
{
    public class TinyTypeTest
    {
        // integer tiny types
        private readonly IntTinyType _three = new IntTinyType(3);
        private readonly IntTinyType _threeAgain = new IntTinyType(3);
        private readonly IntTinyType _two = new IntTinyType(2);
        private readonly IntTinyType _nullInt = null;

        // string tiny types
        private readonly StringTinyType _foo = new StringTinyType("foo");
        private readonly StringTinyType _fooAgain = new StringTinyType("foo");
        private readonly StringTinyType _bar = new StringTinyType("bar");

        // bool tiny types
        private readonly BoolTinyType _true = new BoolTinyType(true);
        private readonly BoolTinyType _trueAgain = new BoolTinyType(true);
        private readonly BoolTinyType _false = new BoolTinyType(false);

        [Fact]
        public void EqualsMethodWorks()
        {
            Assert.True(_three.Equals(_threeAgain));
            Assert.True(_three.Equals(3));
            Assert.False(_three.Equals(_two));
            Assert.False(_three.Equals(2));

            Assert.True(_foo.Equals(_fooAgain));
            Assert.True(_foo.Equals("foo"));
            Assert.False(_foo.Equals(_bar));
            Assert.False(_foo.Equals("bar"));

            Assert.True(_true.Equals(_trueAgain));
            Assert.True(_true.Equals(true));
            Assert.False(_true.Equals(_false));
            Assert.False(_true.Equals(false));
        }

        [Fact]
        public void EqualityOperatorWorks()
        {
            Assert.True(_three == _threeAgain);
            Assert.True(_three == 3);
            Assert.True(_three != _two);
            Assert.True(_three != 2);

            Assert.True(_foo == _fooAgain);
            Assert.True(_foo == "foo");
            Assert.False(_foo == _bar);
            Assert.False(_foo == "bar");

            Assert.True(_true == _trueAgain);
            Assert.True(_true == true);
            Assert.False(_true == _false);
            Assert.False(_true == false);

            Assert.False(_three == _nullInt);
            Assert.False(_nullInt == _three);
        }

        [Fact]
        public void InequalityOperatorWorks()
        {
            Assert.False(_three != _threeAgain);
            Assert.False(_three != 3);
            Assert.True(_three != _two);
            Assert.True(_three != 2);

            Assert.False(_foo != _fooAgain);
            Assert.False(_foo != "foo");
            Assert.True(_foo != _bar);
            Assert.True(_foo != "bar");

            Assert.False(_true != _trueAgain);
            Assert.False(_true != true);
            Assert.True(_true != _false);
            Assert.True(_true != false);

            Assert.True(_three != _nullInt);
            Assert.True(_nullInt != _three);
        }

        [Fact]
        public void DateTinyTypesWork()
        {
            var date1 = new DateTime(1987, 6, 3);
            var date2 = new DateTime(1987, 6, 3);
            var anotherDate = new DateTime(2009, 7, 8);

            Assert.True(date1.Equals(date2));
            Assert.False(date1.Equals(anotherDate));
        }

        private class IntTinyType : TinyType<int>
        {
            public IntTinyType(int value) : base (value) {}
        }
        private class StringTinyType : TinyType<string>
        {
            public StringTinyType(string value) : base (value) {}
        }
        private class BoolTinyType : TinyType<bool>
        {
            public BoolTinyType(bool value) : base (value) {}
        }

    }
}
