using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Base.InterfaceBase
{
    public interface IConfigurationPage
    {
        string SqlServerConnection { get; }

        string GetConnectionString(string connectionString);

        IConfigurationSection GetConfigurationSection(string Key);
    }
}
