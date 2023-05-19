using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SnackMachine.Logic;
using SnackMachine.Logic.Persistence;
using FluentAssertions;

namespace SnackMachine.Tests
{
    public class EntityFrameworkSanityTests
    {

        [Fact]
        public async void ShouldReturnSavedSnackMachine()
        {
            System.Environment.SetEnvironmentVariable("DATABASE_URL", "Server=localhost; Port=5432; User Id=postgres; Password=postgres; Database=practical_ddd_db; CommandTimeout=20;");
            var id = Guid.NewGuid();
            var sut = new SnackMachineEntity(id);
            var repository = new SnackMachineRepository(new DataContextFactory());

            repository.Save(sut);

            var actual = repository.GetById(id);
            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(sut);

            repository.Delete(actual);
        }

        [Fact]
        public async void SnackPileShouldProperlyLoad()
        {
            System.Environment.SetEnvironmentVariable("DATABASE_URL", "Server=localhost; Port=5432; User Id=postgres; Password=postgres; Database=practical_ddd_db; CommandTimeout=20;");
            var id = Guid.NewGuid();
            var sut = new SnackMachineEntity(id);
            var repository = new SnackMachineRepository(new DataContextFactory());

            sut.LoadSnacks(1, new SnackPile(new Snack(Guid.NewGuid(), "Chocolate Bar"), 1, 3m));
            sut.InsertMoney(Money.Dollar);
            sut.InsertMoney(Money.Dollar);
            sut.InsertMoney(Money.Dollar);
            sut.BuySnack(1);

            repository.Save(sut);

            var actual = repository.GetById(id);
            actual.GetSnackPile(1).Quantity.Should().Be(0);
            actual.GetSnackPile(1).Snack.Name.Should().Be("Chocolate Bar");

            repository.Delete(actual);
        }
    }
}
