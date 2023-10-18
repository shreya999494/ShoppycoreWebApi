using System;
using System.Collections.Generic;

namespace ShoppycoreWebApi.Database;

public partial class Brand
{
    public int BrandId { get; set; }

    public string? Brands { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
