﻿using Backend.Src.Db.TestFixtures;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Backend.Src.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTests;

[CollectionDefinition("TransactionalTests")]
public class TransactionalTestCollection : ICollectionFixture<TransactionalDbTestFixture>
{
}
[Collection("TransactionalTests")]
public class TransactionalRepoTests : IDisposable
{
    public TransactionalRepoTests(TransactionalDbTestFixture fixture)
    {
        Fixture = fixture;
    }
    public TransactionalDbTestFixture Fixture { get; set; }
    public void Dispose()
    {
        Fixture.Cleanup();
    }
    [Fact]
    public async void BaseRepoUpdate()
    {
        using var context = Fixture.CreateContext();

        await TestRun<Genre, GenreRepo>(new GenreRepo(context));
        await TestRun<Instrument, InstrumentRepo>(new InstrumentRepo(context));
        await TestRun<City, CityRepo> (new CityRepo(context));
        static async Task TestRun<TModel, TRepo>(TRepo repo) where TModel : HasName, new() where TRepo : BaseRepoName<TModel>
        {
            IEnumerable<TModel> items = await repo.GetAllAsync(null);
            TModel itemToUpdate = items.First();
            string originalName = itemToUpdate.Name;

            itemToUpdate.Name = "Test";
            var result = await repo.UpdateOneAsync(itemToUpdate);
            Assert.NotEqual(originalName, result.Name);

            items = await repo.GetAllAsync(new NameFilter() { Name = originalName});
            Assert.True(!items.Any());   
        }
    }
    [Fact]
    public async void BaseRepoDelete()
    {
        using var context = Fixture.CreateContext();

        await TestRun<Genre, GenreRepo>(new GenreRepo(context));
        await TestRun<Instrument, InstrumentRepo>(new InstrumentRepo(context));
        await TestRun<City, CityRepo>(new CityRepo(context));

        static async Task TestRun<TModel, TRepo>(TRepo repo) where TModel : HasName, new() where TRepo : BaseRepoName<TModel>
        {
            IEnumerable<TModel> items = await repo.GetAllAsync(null);
            TModel itemToDelete = items.First();
            int originalLength = items.Count();

            var result = await repo.DeleteOneAsync(itemToDelete.Id);
            Assert.True(result);

            items = await repo.GetAllAsync(new NameFilter() { Name = itemToDelete.Name });
            Assert.True(!items.Any());
            var result2 = await repo.DeleteOneAsync(new Guid(new byte[16] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}));
            Assert.False(result2);
        }
    }
}
