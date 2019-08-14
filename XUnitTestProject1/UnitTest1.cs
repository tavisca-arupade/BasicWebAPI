using System;
using Xunit;
using WebAPI.Controllers;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test_default_greet_value()
        {
            GreetingController greet = new GreetingController();

            Assert.Equal("say helloo", greet.Get());
        }

        [Fact]
        public void Test_greet_value_when_hello()
        {
            GreetingController greet = new GreetingController();

            Assert.Equal("hi", greet.Get("hello").Value);
        }

        [Fact]
        public void Test_greet_value_when_hi()
        {
            GreetingController greet = new GreetingController();

            Assert.Equal("hello", greet.Get("hi").Value);
        }
    }
}
