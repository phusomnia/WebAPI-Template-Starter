using System;
using System.Collections.Generic;

namespace WebAPI_Template_Starter.Domain.Entities;

public partial class Role
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
