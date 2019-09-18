using System;

namespace Exercises
{
    public class MockTimeProvider : ITimeProvider
    {
        private DateTime _now;
        public MockTimeProvider(DateTime now)
        {
            _now = now;
        }
        public DateTime Now()
        {
            return _now;
        }
    }
}