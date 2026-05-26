using System;
using System.Collections.Generic;

namespace Infraestructure.Persistence.Data;

/// <summary>
/// Auth: Manages updates to the auth system.
/// </summary>
public partial class SchemaMigration
{
    public string Version { get; set; } = null!;
}
