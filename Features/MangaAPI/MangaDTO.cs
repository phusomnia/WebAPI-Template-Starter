using System.ComponentModel.DataAnnotations;

namespace WebAPI_Template_Starter.Features.MangaAPI;

public class MangaDTO
{
    [StringLength(255, MinimumLength = 3, ErrorMessage = "Title is required, characters are between 3 and 255")]
    public string Title { get; set; } = null!;
}