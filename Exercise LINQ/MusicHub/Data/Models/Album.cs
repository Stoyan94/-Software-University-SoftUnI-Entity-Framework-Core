using System.ComponentModel.DataAnnotations;

namespace MusicHub.Data.Models;

public class Album
{
    [Key]
    public int Id { get; set; }

    [MaxLength(ValidationsConstants.AlbumNameMaxLength)]
    public string Name { get; set; } = null!;

    public DateTime ReleaseDate  { get; set; }

    // public decimal Price { get; set; }

    public int? ProducerId  { get; set; }


}
