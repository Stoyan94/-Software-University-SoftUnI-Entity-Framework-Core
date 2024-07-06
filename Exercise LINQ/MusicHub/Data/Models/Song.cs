using MusicHub.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MusicHub.Data.Models;

public class Song
{
    [Key]
    public int Id { get; set; }

    [MaxLength(ValidationsConstants.SongNameMaxLength)]
    public string Name { get; set; } = null!;

    public TimeSpan Duration { get; set; }

    public DateTime CreatedOn  { get; set; }

    public Genre Genre { get; set; }

    public int? AlbumId { get; set; }

    public int WriterId { get; set; }

    public decimal Price { get; set; }
}
