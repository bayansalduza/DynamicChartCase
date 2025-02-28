using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DynamicChartCase.Domain.Entities;
using DynamicChartCase.Domain.Repositories;

namespace DynamicChartCase.Infrastructure.Repositories
{
    public class ChartRepository : IChartRepository
    {
        private string BuildConnectionString(ConnectionParameters conn)
        {
            return $"Server={conn.ServerName};Database={conn.DatabaseName};" +
                   $"User Id={conn.UserName};Password={conn.Password};TrustServerCertificate=True;";
        }

        public bool TestConnection(ConnectionParameters connParams)
        {
            try
            {
                using var connection = new SqlConnection(BuildConnectionString(connParams));
                connection.Open();
                return connection.State == ConnectionState.Open;
            }
            catch
            {
                return false;
            }
        }

        public List<DataSourceInfo> GetDataSources(ConnectionParameters connParams)
        {
            var result = new List<DataSourceInfo>();

            string sql = @"
                SELECT 
                    name AS ObjectName,
                    CASE 
                        WHEN type_desc = 'USER_TABLE' THEN 'Table'
                        WHEN type_desc = 'VIEW' THEN 'View'
                        WHEN type_desc LIKE '%FUNCTION%' THEN 'Function'
                        WHEN type_desc = 'SQL_STORED_PROCEDURE' THEN 'StoredProcedure'
                        ELSE type_desc
                    END AS ObjectType
                FROM sys.objects
                WHERE type_desc IN (
                    'USER_TABLE',
                    'VIEW',
                    'SQL_STORED_PROCEDURE',
                    'SQL_SCALAR_FUNCTION',
                    'SQL_TABLE_VALUED_FUNCTION'
                )
                ORDER BY name;
            ";

            try
            {
                using var connection = new SqlConnection(BuildConnectionString(connParams));
                connection.Open();

                var queryResult = connection.Query(sql).ToList();
                foreach (var row in queryResult)
                {
                    string name = row.ObjectName;
                    string type = row.ObjectType;

                    result.Add(new DataSourceInfo
                    {
                        Name = name,
                        ObjectType = type
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetDataSources Error: " + ex.Message);
            }

            return result;
        }

        public List<Dictionary<string, object>> ExecuteDataSource(
            ConnectionParameters connParams,
            string dataSourceName,
            string objectType,
            Dictionary<string, object> parameters
        )
        {
            var result = new List<Dictionary<string, object>>();

            using var connection = new SqlConnection(BuildConnectionString(connParams));
            connection.Open();

            if (objectType == "Table" || objectType == "View")
            {
                string sql = $"SELECT * FROM [{dataSourceName}]";
                var rows = connection.Query(sql);

                foreach (var row in rows)
                {
                    var dictRow = row as IDictionary<string, object>;
                    var rowDict = new Dictionary<string, object>();
                    foreach (var kv in dictRow)
                    {
                        rowDict[kv.Key] = kv.Value;
                    }
                    result.Add(rowDict);
                }
            }
            else if (objectType == "StoredProcedure")
            {
                var dynamicParams = new DynamicParameters();
                if (parameters != null)
                {
                    foreach (var kv in parameters)
                    {
                        dynamicParams.Add("@" + kv.Key, kv.Value);
                    }
                }

                var queryResult = connection.Query(
                    dataSourceName,
                    dynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                foreach (var item in queryResult)
                {
                    var dictItem = item as IDictionary<string, object>;
                    var rowDict = new Dictionary<string, object>();
                    foreach (var kv in dictItem)
                    {
                        rowDict[kv.Key] = kv.Value;
                    }
                    result.Add(rowDict);
                }
            }
            else if (objectType == "Function")
            {
                string sql = $"SELECT * FROM [{dataSourceName}]()";
                var rows = connection.Query(sql);

                foreach (var row in rows)
                {
                    var dictRow = row as IDictionary<string, object>;
                    var rowDict = new Dictionary<string, object>();
                    foreach (var kv in dictRow)
                    {
                        rowDict[kv.Key] = kv.Value;
                    }
                    result.Add(rowDict);
                }
            }
            else
            {
                throw new Exception($"Unsupported object type: {objectType}");
            }

            return result;
        }
    }
}
