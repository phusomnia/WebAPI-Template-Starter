using System;
using System.Collections.Generic;

namespace WebAPI_Template_Starter.Domain.Entities;

public partial class EfmigrationsHistory
{
    public string MigrationId { get; set; } = null!;

    public string ProductVersion { get; set; } = null!;
}
