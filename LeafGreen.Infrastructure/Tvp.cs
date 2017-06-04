using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Server;
using System.Reflection;
using System.Linq;

namespace LeafGreen.Infrastructure
{
    public class Tvp<T> : ITvp<T>
    {
        public SqlMetaData[] CreateTvp(IEnumerable<T> items)
        {
            var type = typeof(T);
            var props = type.GetRuntimeProperties();
            var metaDataDictionary = new Dictionary<string, SqlMetaData>();
            foreach(var prop in props)
            {
                var propertyType = prop.PropertyType;
                if(!metaDataDictionary.ContainsKey(prop.Name))
                {
                    metaDataDictionary.Add(prop.Name, new SqlMetaData(prop.Name, SqlTypeMapper.GetDbType(propertyType)));
                }
            }
            var metaDataList = new List<SqlMetaData>();
            var records = new List<SqlDataRecord>();
            foreach(var item in items)
            {
                var record = new SqlDataRecord();
                //record.SetValue()
                //foreach(var prop in item.GetType().GetRuntimeProperties())
            }

            return null;
        }
    }
}
