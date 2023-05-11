using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using SnackMachine.Logic;
using Xunit;

namespace SnackMachine.Tests
{
    public class SnackPileTest
    {
        [Fact]
        public void ShouldThrowExceptionWhenCreatingWithQuantityLessThan0()
        {
            var action = new Action(() => { new SnackPile(null, -1, 1m); });

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingWithPriceLessThan0()
        {
            var action = new Action(() => { new SnackPile(null, 1, -1m); });

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingWithAPriceMorePreciseThanOneCent()
        {
            var action = new Action(() => { new SnackPile(null, 1, 0.001m); });

            action.Should().ThrowExactly<InvalidOperationException>();
        }
    }
}
