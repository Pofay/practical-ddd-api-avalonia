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
            var repository = new SnackMachineRepository(new DbContextFactory());

            repository.Save(sut);

            var actual = repository.GetById(id);
            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(sut);

            repository.Delete(actual);
        }

        [Fact(Skip = "Known Bug, Default Snackmachine should start with 3 SnackPiles all pointing to Empty. Yet only one snackpile points to it in the database.")]
        public async void DefaultSnackMachineShouldHaveEmptySnackPiles()
        {
            System.Environment.SetEnvironmentVariable("DATABASE_URL", "Server=localhost; Port=5432; User Id=postgres; Password=postgres; Database=practical_ddd_db; CommandTimeout=20;");
            var id = Guid.NewGuid();
            var sut = new SnackMachineEntity(id);
            var repository = new SnackMachineRepository(new DbContextFactory());

            repository.Save(sut);

            var actual = repository.GetById(id);
            // Empty SnackPiles are just referencing SnackPile.Empty
            
            actual.GetSnackPile(1).Snack.Should().NotBeNull();
            actual.GetSnackPile(2).Snack.Should().NotBeNull();
            actual.GetSnackPile(3).Snack.Should().NotBeNull();

            repository.Delete(actual);
        }

        [Fact]
        public async void SnackPileShouldProperlyLoad()
        {
            System.Environment.SetEnvironmentVariable("DATABASE_URL", "Server=localhost; Port=5432; User Id=postgres; Password=postgres; Database=practical_ddd_db; CommandTimeout=20;");
            var id = Guid.Parse("6445db32-5a2f-407c-8a54-83b0828ffd34");
            var sut = new SnackMachineEntity(id);
            var chocolate = Snack.Chocolate;
            var repository = new SnackMachineRepository(new DbContextFactory());

            sut.LoadSnacks(1, new SnackPile(Snack.Chocolate, 1, 3m));
            sut.LoadSnacks(2, new SnackPile(Snack.Soda, 1, 3m));
            sut.LoadSnacks(3, new SnackPile(Snack.Gum, 1, 3m));

            sut.InsertMoney(Money.Dollar);
            sut.InsertMoney(Money.Dollar);
            sut.InsertMoney(Money.Dollar);
            sut.BuySnack(1);

            repository.Save(sut);

            var actual = repository.GetById(id);
            actual.GetSnackPile(1).Quantity.Should().Be(0);
            actual.GetSnackPile(1).Snack.Name.Should().Be(chocolate.Name);
            actual.GetSnackPile(1).Snack.Id.Should().Be(chocolate.Id);

            repository.Delete(actual);
        }
    }
}
