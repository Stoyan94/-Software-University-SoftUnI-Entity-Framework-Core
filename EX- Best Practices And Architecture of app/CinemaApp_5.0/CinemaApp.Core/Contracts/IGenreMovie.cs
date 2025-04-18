﻿using CinemaApp.Infrastructure.Data.Models;

namespace CinemaApp.Core.Contracts
{
    public interface IGenreMovie
    {
        Task<List<Movie>> GetGenreMoviesAsync(string genre);
    }
}
