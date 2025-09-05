using System;
using System.Collections.Generic;

namespace WebAPI_Template_Starter.Domain.Entities;

public partial class Manga
{
    public string Id { get; set; } = null!;

    public string? Title { get; set; }
}
