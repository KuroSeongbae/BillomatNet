﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Develappers.BillomatNet.Queries;
using Develappers.BillomatNet.Types;
using Xunit;

namespace Develappers.BillomatNet.Tests.IntegrationTests
{
    [Trait(Traits.Category, Traits.Categories.IntegrationTest)]
    public class UnitServiceIntegrationTests : IntegrationTestBase<UnitService>
    {
        public UnitServiceIntegrationTests() : base(c => new UnitService(c))
        {
        }

        [Fact]
        public async Task GetListOfUnits()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new UnitService(config);
            var result = await service.GetListAsync(CancellationToken.None);
            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetFilteredUnits()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new UnitService(config);
            var result = await service.GetListAsync(
                new Query<Unit, UnitFilter>().AddFilter(x => x.Name, "Stunde"));
            Assert.True(result.List.Count > 0);
        }

        [Fact]
        public async Task GetFilteredUnitsNotFound()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new UnitService(config);
            var result = await service.GetListAsync(
                new Query<Unit, UnitFilter>().AddFilter(x => x.Name, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"));
            Assert.True(result.TotalItems == 0);
        }

        [Fact]
        public async Task GetFilteredUnitsNotAuthorized()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            config.ApiKey = "";
            var service = new UnitService(config);
            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.GetListAsync(
                new Query<Unit, UnitFilter>().AddFilter(x => x.Name, "Stunde")));
        }

        [Fact]
        public async Task GetByIdUnits()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new UnitService(config);
            var result = await service.GetByIdAsync(20573);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByIdUnitsWhenNotFound()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new UnitService(config);
            var result = await service.GetByIdAsync(1);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdunitsWhenNotAuthorized()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new UnitService(config);
            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.GetByIdAsync(20573));
        }

        [Fact]
        public async Task DeleteUnitItem()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new UnitService(config);

            var name = "xUnit test";

            var unitItem = new Unit
            {
                Name = name
            };

            var result = await service.CreateAsync(unitItem);
            Assert.Equal(name, result.Name);

            await service.DeleteAsync(result.Id);

            var ex = Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(result.Id));
        }

        [Fact]
        public async Task DeleteUnitItemNotExisting()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new UnitService(config);
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(1));
        }

        [Fact]
        public async Task DeleteUnitItemNotAuthorized()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new UnitService(config);
            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.DeleteAsync(1));
        }

        [Fact]
        public async Task EditUnitItem()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new UnitService(config);

            var name = "xUnit test";

            var unitItem = new Unit
            {
                Name = name
            };

            var result = await service.CreateAsync(unitItem);

            Assert.Equal(name, result.Name);

            var newName = "xUnit test edited";

            var editedUnitItem = new Unit
            {
                Id = result.Id,
                Name = newName,
            };

            var editedResult = await service.EditAsync(editedUnitItem);
            Assert.Equal(newName, editedUnitItem.Name);

            await service.DeleteAsync(editedUnitItem.Id);
        }

        [Fact]
        public async Task EditUnitItemWhenNotFound()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new UnitService(config);
            var unitItem = new Unit
            {
                Id = 1
            };
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => service.EditAsync(unitItem));
        }

        [Fact]
        public async Task EditUnitItemWhenNotAuthorized()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new UnitService(config);
            var unitItem = new Unit
            {
                Id = 20573
            };
            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.EditAsync(unitItem));
        }

        [Fact]
        public async Task CreateUnitItem()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new UnitService(config);

            var name = "xUnit test";

            var unitItem = new Unit
            {
                Name = name
            };

            var result = await service.CreateAsync(unitItem);
            Assert.Equal(name, result.Name);

            await service.DeleteAsync(result.Id);
        }

        [Fact]
        public async Task CreateTaxItemWhenNotAuthorized()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            config.ApiKey = "ajfkjeinodafkejlkdsjklj";
            var service = new UnitService(config);

            var name = "xUnit test";

            var unitItem = new Unit
            {
                Name = name
            };

            var ex = await Assert.ThrowsAsync<NotAuthorizedException>(() => service.CreateAsync(unitItem));
        }

        [Fact]
        public async Task CreateUnitItemWhenNull()
        {
            var config = IntegrationTests.Helpers.GetTestConfiguration();
            var service = new UnitService(config);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(null));
        }
    }
}
