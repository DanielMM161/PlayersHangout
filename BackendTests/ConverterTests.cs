﻿namespace BackendTests;

using Backend.Src.Converter;
using Backend.Src.DTOs;
using Backend.Src.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
public class ConverterTests
{
    [Fact]
    public virtual void ConverterWanted()
    {
        Converter converter = new();
        WantedCreateDTO wantedCreateDTO = new()
        {
            Instrument = new Instrument()
            {
                Id = Guid.NewGuid(),
                Name = "test",
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
            },
            SkillLevel = UserInstrument.SkillLevel.Intermediate,
            Description = "I am initial description",
            User = new User()
            {
                Location = new Location()
                {
                    City = new City()
                    {
                        Name = "test city",
                        Id = Guid.NewGuid(),
                        UpdatedAt = DateTime.Now,
                        CreatedAt = DateTime.Now,
                    },
                    Latitude = 60.0,
                    Longitude = 69.0
                }
            },
            Genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = Guid.NewGuid(),
                    Name = "I am initial genre",
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now
                },
                new Genre()
                {
                    Id = Guid.NewGuid(),
                    Name = "I am second genre",
                    UpdatedAt = DateTime.Now,
                    CreatedAt = DateTime.Now
                }
            },
        };

        converter.CreateModel(wantedCreateDTO, out Wanted model);
        Assert.NotNull(model);
        Assert.Equal(wantedCreateDTO.Description, model.Description);
        Assert.Equal(wantedCreateDTO.Genres.First().Name, model.Genres.First().Name);
        Assert.NotNull(model.User.Location);
        Assert.Equal(wantedCreateDTO.User.Location.City.Name, model.User.Location.City.Name);
        
        WantedUpdateDTO wantedUpdateDTO = new()
        {
            Description = "I have been changed",
            Fullfilled = true
        };
        converter.UpdateModel(model, wantedUpdateDTO);
        Assert.True(model.Fullfilled);
        Assert.Equal(wantedUpdateDTO.Description, model.Description);

        WantedReadDTO wantedReadDTO = converter.ConvertReadDTO<Wanted, WantedReadDTO>(model);
        Assert.Equal(model.User.Location.City.Name, wantedReadDTO.City);
        Assert.Equal(model.Instrument.Name, wantedReadDTO.Instrument);
        Assert.Equal(model.Description, wantedReadDTO.Description);
        Assert.Equal(model.SkillLevel, wantedReadDTO.SkillLevel);
        if (model.Genres is not null && wantedReadDTO.Genres is not null)
        {
            foreach (var genre in model.Genres)
            {
                Assert.Contains(genre, wantedReadDTO.Genres);
            }
        }
    }
    [Fact]
    public void ConverterUser()
    {
        Converter converter = new();
        
        City city = new City()
        {
            Id = Guid.NewGuid(),
            Name = "I am city",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        Location location = new Location()
        {
            Id = Guid.NewGuid(),
            Latitude = 0,
            Longitude = 0,
            City = city,
            CityId = city.Id,
        };

        ICollection<Genre> genres = new List<Genre>()
        {
            new Genre()
            {
                Id = Guid.NewGuid(),
                Name = "Test genre 1",
                CreatedAt= DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
            new Genre()
            {
                Id = Guid.NewGuid(),
                Name = "Test genre 2",
                CreatedAt= DateTime.Now,
                UpdatedAt = DateTime.Now,
            },
        };

        Instrument instrument = new Instrument()
        {
            Id = Guid.NewGuid(),
            Name = "I am instrument",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        User mockUser = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Test First Name",
            LastName = "Test Last Name",
            Email = "email",
            PasswordHash = "123456wqert",
            PhoneNumber = "1234567890",
            AccessFailedCount = 0,
            ActiveSession = false,
            ConcurrencyStamp = "concurrency_stamp",
            EmailConfirmed = true,
            LockoutEnabled = true,
            Location = location,
            LocationId = location.Id,
            Genres = genres,
            LockoutEnd = DateTime.Now,
            NormalizedEmail = "EMAIL",
            NormalizedUserName = "TEST FIRST NAME",
            PhoneNumberConfirmed = true,
            SecurityStamp = "security_stamp",
            TwoFactorEnabled = false,
            UserName = "So Nice",
            MainInstrument = instrument,
            Instruments = new List<UserInstrument>(),
            Wanteds = new List<Wanted>(),
        };
        UserInstrument userInstrument = new UserInstrument()
        {
            UserId = mockUser.Id,
            User = mockUser,
            InstrumentId = instrument.Id,
            Instrument = instrument,
            LookingToPlay = true,
            Skill = UserInstrument.SkillLevel.Experienced
        };
        Wanted wanted = new Wanted()
        {
            UserId = mockUser.Id,
            User = mockUser,
            Instrument = new Instrument()
            {
                Id = Guid.NewGuid(),
                Name = "Wanted Instrument",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            }
        };
        mockUser.Wanteds.Add(wanted);
        mockUser.Instruments.Add(userInstrument);
        UserReadDTO result = converter.ConvertReadDTO<User, UserReadDTO>(mockUser);
        foreach(var property in result.GetType().GetProperties())
        {
            var equalToThisProperty = mockUser.GetType().GetProperty(property.Name);
            if (equalToThisProperty is not null)
            {
                Assert.Equal(property.GetValue(result), equalToThisProperty.GetValue(mockUser));
            }
        }
    }
}
