﻿namespace Backend.Src.Converter.User;

using Backend.Src.DTOs;
using Backend.Src.Models;

public class UserConverter : IUserConverter
{
    public UserReadDTO ConvertReadDTO(User model)
    {
        return new UserReadDTO
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Instruments = model.Instruments,
            MainInstrument = model.MainInstrument,
            City = model.Location.City
        };
    }

    public void CreateModel(User model, UserCreateDTO create)
    {
        model.FirstName = create.FirstName;
        model.LastName = create.LastName;
        model.Email = create.Email;
        model.Location = create.Location;
    }

    public void UpdateModel(User model, UserUpdateDTO update)
    {
        model.FirstName = update.FirstName;
        model.LastName = update.LastName;
        model.Email = update.Email;
        model.Location = update.Location;
        model.Instruments = update.Instruments;
        model.Genres = update.Genres;
    }
}
