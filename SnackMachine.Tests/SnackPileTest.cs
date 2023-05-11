using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnackMachine.Logic;
using Xunit;

namespace SnackMachine.Tests
{
    public class SnackPileTest
    {
        [Fact]
        public void ShouldThrowExceptionWhenCreatingWithQuantityLessThan0()
        {
            Assert.Throws<InvalidOperationException>(() => { new SnackPile(null, -1, 1m); });
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingWithPriceLessThan0()
        {
            Assert.Throws<InvalidOperationException>(() => { new SnackPile(null, 1, -1); });
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingWithAPriceMorePreciseThanOneCent()
        {
            Assert.Throws<InvalidOperationException>(() => { new SnackPile(null, 1, 0.001m); });
        }
    }
}
