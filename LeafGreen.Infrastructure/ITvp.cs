using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafGreen.Infrastructure
{
    public interface ITvp<T>
    {
        SqlMetaData[] CreateTvp(IEnumerable<T> items);
    }
}
