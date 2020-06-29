using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.VisualBasic.FileIO;
using GDS.Gateway.Constants;
using GDS.Gateway.Models;
using GDS.Gateway.Models.Domain;
using GDS.Gateway.Models.Io;

namespace GDS.Gateway.Adapters.DataAccess
{
    public class SqlConnectionParams
    {
        public string Host { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class SqlDataAdapter : IGatewayAdapter
    {
        private static readonly Dictionary<string, Tuple<DataType, FieldSemantics>> _sqlDataTypeMap = new Dictionary<string, Tuple<DataType, FieldSemantics>>() {

            {"date", new Tuple<DataType, FieldSemantics>( DataType.STRING, new FieldSemantics(){ ConceptType = ConceptType.DIMENSION, SemanticGroup = SemanticGroups.DATETIME, SemanticType = SemanticTypes.YEAR_MONTH_DAY, IsReaggregatable = false  }) },
            {"datetime2", new Tuple<DataType, FieldSemantics>( DataType.STRING, new FieldSemantics(){ ConceptType = ConceptType.DIMENSION, SemanticGroup = SemanticGroups.DATETIME, SemanticType = SemanticTypes.YEAR_MONTH_DAY, IsReaggregatable = false  }) },
            {"smalldatetime", new Tuple<DataType, FieldSemantics>( DataType.STRING, new FieldSemantics(){ ConceptType = ConceptType.DIMENSION, SemanticGroup = SemanticGroups.DATETIME, SemanticType = SemanticTypes.YEAR_MONTH_DAY, IsReaggregatable = false }) },

            {"binary", new Tuple<DataType, FieldSemantics>( DataType.NUMBER, new FieldSemantics(){ ConceptType = ConceptType.DIMENSION, SemanticGroup =null, SemanticType = SemanticTypes.TEXT, IsReaggregatable = false  }) },
            {"bit", new Tuple<DataType, FieldSemantics>( DataType.BOOLEAN, new FieldSemantics(){ ConceptType = ConceptType.DIMENSION, SemanticGroup = null, SemanticType = SemanticTypes.TEXT, IsReaggregatable = false  }) },
            {"char", new Tuple<DataType, FieldSemantics>( DataType.STRING, new FieldSemantics(){ ConceptType = ConceptType.DIMENSION, SemanticGroup = null, SemanticType = SemanticTypes.TEXT, IsReaggregatable = false  }) },
            {"nvarchar", new Tuple<DataType, FieldSemantics>( DataType.STRING, new FieldSemantics(){ ConceptType = ConceptType.DIMENSION, SemanticGroup = null, SemanticType = SemanticTypes.TEXT, IsReaggregatable = false  }) },
            {"uniqueidentifier", new Tuple<DataType, FieldSemantics>( DataType.STRING, new FieldSemantics(){ ConceptType = ConceptType.DIMENSION, SemanticGroup = null, SemanticType = SemanticTypes.TEXT, IsReaggregatable = false  }) },
            {"varchar", new Tuple<DataType, FieldSemantics>( DataType.STRING, new FieldSemantics(){ ConceptType = ConceptType.DIMENSION, SemanticGroup = null, SemanticType = SemanticTypes.TEXT, IsReaggregatable = false  }) },

            {"bigint", new Tuple<DataType, FieldSemantics>( DataType.NUMBER, new FieldSemantics(){ ConceptType = ConceptType.METRIC, SemanticGroup = SemanticGroups.NUMERIC, SemanticType = SemanticTypes.NUMBER, IsReaggregatable = true  }) },
            {"decimal", new Tuple<DataType, FieldSemantics>( DataType.NUMBER, new FieldSemantics(){ ConceptType = ConceptType.METRIC, SemanticGroup = SemanticGroups.NUMERIC, SemanticType = SemanticTypes.NUMBER, IsReaggregatable = true }) },
            {"float", new Tuple<DataType, FieldSemantics>( DataType.NUMBER, new FieldSemantics(){ ConceptType = ConceptType.METRIC, SemanticGroup = SemanticGroups.NUMERIC, SemanticType = SemanticTypes.NUMBER, IsReaggregatable = true  }) },
            {"int", new Tuple<DataType, FieldSemantics>( DataType.NUMBER, new FieldSemantics(){ ConceptType = ConceptType.METRIC, SemanticGroup = SemanticGroups.NUMERIC, SemanticType = SemanticTypes.NUMBER, IsReaggregatable = true  }) },
            {"numeric", new Tuple<DataType, FieldSemantics>( DataType.NUMBER, new FieldSemantics(){ ConceptType = ConceptType.METRIC, SemanticGroup = SemanticGroups.NUMERIC, SemanticType = SemanticTypes.NUMBER, IsReaggregatable = true  }) },
            {"smallint", new Tuple<DataType, FieldSemantics>( DataType.NUMBER, new FieldSemantics(){ ConceptType = ConceptType.METRIC, SemanticGroup = SemanticGroups.NUMERIC, SemanticType = SemanticTypes.NUMBER, IsReaggregatable = true  }) },
            {"tinyint", new Tuple<DataType, FieldSemantics>( DataType.NUMBER, new FieldSemantics(){ ConceptType = ConceptType.METRIC, SemanticGroup = SemanticGroups.NUMERIC, SemanticType = SemanticTypes.NUMBER, IsReaggregatable = true    }) },
        };

        private static readonly Dictionary<FilterOperators, string> _operatorStringMap = new Dictionary<FilterOperators, string>() {
            {FilterOperators.EQUALS, "=" },
            {FilterOperators.CONTAINS, "LIKE" },
            {FilterOperators.REGEXP_PARTIAL_MATCH, "LIKE" },
            {FilterOperators.REGEXP_EXACT_MATCH, "LIKE" },
            {FilterOperators.IN_LIST, "IN" },
            {FilterOperators.IS_NULL, "IS NULL" },
            {FilterOperators.BETWEEN, "BETWEEN" },
            {FilterOperators.NUMERIC_GREATER_THAN, ">" },
            {FilterOperators.NUMERIC_GREATER_THAN_OR_EQUAL, ">=" },
            {FilterOperators.NUMERIC_LESS_THAN, "<" },
            {FilterOperators.NUMERIC_LESS_THAN_OR_EQUAL, "<=" },
        };

       

        public async Task<List<SchemaTableInfo>> GetTablesAsync(string sqlConnStr, string tblOrViewName)
        {

            var getSchemaSQL = @"   SELECT 
                    TABLE_SCHEMA AS SchemaName, 
                    TABLE_NAME AS TableName  
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE 
                    TABLE_SCHEMA <> 'sys' 
                    " +
                   (string.IsNullOrWhiteSpace(tblOrViewName) ? "  " : " AND TABLE_NAME= @TablOrViewName ")

                   + " GROUP BY TABLE_SCHEMA, TABLE_NAME ORDER BY TABLE_SCHEMA DESC, TABLE_NAME ASC      ";

            using (var conn = new SqlConnection(sqlConnStr))
            {
                var results = await conn.QueryAsync<SchemaTableInfo>(getSchemaSQL, new { TablOrViewName = tblOrViewName }, commandType: System.Data.CommandType.Text);
                return results.ToList();
            }
            return default(List<SchemaTableInfo>);
        }

        public async Task<SchemaResponse> GetSchemaAsync(string sqlConnStr, string schemaName = "dbo", string tblOrViewName = null) { 

            var getSchemaSQL = @"   SELECT 
                    TABLE_SCHEMA AS SchemaName, 
                    TABLE_NAME AS TableName, 
                    COLUMN_NAME AS ColumnName, 
                    DATA_TYPE AS DataType, 
                    CHARACTER_MAXIMUM_LENGTH AS CharMax, 
                    NUMERIC_PRECISION AS NumericPrecision, 
                    DATETIME_PRECISION AS DatePrecision, 
                    IS_NULLABLE AS IsNullable       
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE 
                    TABLE_SCHEMA <> 'sys' 
                    " +
                   (string.IsNullOrWhiteSpace(tblOrViewName) ? "  " : " AND TABLE_SCHEMA = @SchemaName AND TABLE_NAME = @TablOrViewName ")
                   + "ORDER BY TABLE_SCHEMA DESC, TABLE_NAME ASC             ";

            using (var conn = new SqlConnection(sqlConnStr))
            {
                var results = await conn.QueryAsync<SchemaDetail>(getSchemaSQL, new { SchemaName = schemaName, TablOrViewName = tblOrViewName }, commandType: System.Data.CommandType.Text);

                var resp = new SchemaResponse()
                {
                    Fields = results.Select(r => new SchemaField()
                    {
                        FieldName = r.ColumnName,
                        FieldLabel = r.ColumnName,
                        Description = r.ColumnName,
                        IsDefault = false,                        
                        DataType = _sqlDataTypeMap[r.DataType].Item1,
                        Semantics = _sqlDataTypeMap[r.DataType].Item2,
                        DefaultAggregationType = AggregationTypes.NONE,
                    }).ToList()
                };
                return resp;
            }
            return default(SchemaResponse);
        }

        public string BuildFilter(List<List<DimensionsFilter>> dimFilters, Dictionary<string,SchemaField> fieldMap) {
            StringBuilder filterBuff = new StringBuilder();
            var i = 0;

            dimFilters?.ForEach(andFilter => {
                if (i != 0)
                {
                    filterBuff.AppendLine(" AND ( ");
                }
                else {
                    filterBuff.AppendLine(" ( ");
                }
                var j = 0;
                andFilter.ForEach(orFilter => {
                    filterBuff.Append("\t ");
                    if (j != 0)
                    {
                        filterBuff.Append(" OR ( ");
                    }
                    else {
                        filterBuff.Append(" ( ");
                    }

                    if (orFilter.FilterType == DimensionFilterType.EXCLUDE)
                    {
                        filterBuff.AppendLine("\t\t ( NOT ( ");
                    }
                    else
                    {
                        filterBuff.AppendLine("\t\t ( ( ");
                    }

                    var filterExpr = BuildFilterExpression(orFilter.FieldName, fieldMap[orFilter.FieldName].DataType ?? DataType.STRING, orFilter.FilterOperator, orFilter.Values);
                    filterBuff.Append(filterExpr);
                    filterBuff.AppendLine("\t\t ) ) ");
                    filterBuff.Append(" ) ");

                    j++;
                });
                filterBuff.AppendLine(" ) ");
                i++;
            });

            var filter = filterBuff.ToString();
            return filter;
        }

        public string BuildFilterExpression(string fieldName, DataType dataType, FilterOperators op, IEnumerable<string> values) {
            if (values == null) { values = new string[0]; }
            var filterExpr = new StringBuilder();

            filterExpr.Append($" ( {fieldName} {_operatorStringMap[op]} ");
            switch (op) {
                case FilterOperators.EQUALS:
                case FilterOperators.NUMERIC_GREATER_THAN:
                case FilterOperators.NUMERIC_GREATER_THAN_OR_EQUAL:
                case FilterOperators.NUMERIC_LESS_THAN:
                case FilterOperators.NUMERIC_LESS_THAN_OR_EQUAL:
                    if (dataType == DataType.NUMBER)
                    {
                        filterExpr.Append($" {values.FirstOrDefault()} ");
                    }
                    else {
                        filterExpr.Append($" '{values.FirstOrDefault()}' ");
                    }
                    break;
                case FilterOperators.CONTAINS:
                case FilterOperators.REGEXP_PARTIAL_MATCH:
                case FilterOperators.REGEXP_EXACT_MATCH:
                    var val = values.FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(val) && val.StartsWith("^") && val.EndsWith(".*")) {
                        val = val.Substring(1, val.Length - 3);
                    }
                    if (dataType == DataType.NUMBER)
                    {
                        filterExpr.Append($" {val} ");
                    }
                    else
                    {
                        filterExpr.Append($" '%{val}%' ");
                    }
                    break;
                case FilterOperators.IN_LIST:
                    var k = 0;
                    filterExpr.Append(" ( ");
                    values.ToList().ForEach(v =>
                    {
                        if (k != 0)
                        {
                            filterExpr.Append($" , ");
                        }
                        if (dataType == DataType.NUMBER)
                        {
                            filterExpr.Append($" {v} ");
                        }
                        else
                        {
                            filterExpr.Append($" '{v}' ");
                        }
                        k++;
                    }); 
                    filterExpr.Append(" ) ");


                    break;
                case FilterOperators.IS_NULL:
                    filterExpr.Append($" IS NULL ");
                    break;
                case FilterOperators.BETWEEN:
                    if (dataType == DataType.NUMBER)
                    {
                        filterExpr.Append($" {values.FirstOrDefault()} AND {values.Skip(1).FirstOrDefault()} ");
                    }
                    else
                    {
                        filterExpr.Append($" '{values.FirstOrDefault()}' AND '{values.Skip(1).FirstOrDefault()}' ");
                    }
                    break;
                default:
                    if (dataType == DataType.NUMBER)
                    {
                        filterExpr.Append($" {values.FirstOrDefault()} ");
                    }
                    else
                    {
                        filterExpr.Append($" '{values.FirstOrDefault()}' ");
                    }
                    break;

            }
    filterExpr.Append(" ) ");
            var exprStr = filterExpr.ToString();
            return exprStr;
        }


        public SchemaTableInfo ParseSchemaTable(string configSchemaTable) {

            var splitted = configSchemaTable.Replace("[","").Replace("]","").Split(".");
            var sqlSchema = splitted[0];
            var sqlTable = splitted[1];
            return new SchemaTableInfo(){SchemaName = sqlSchema, TableName = sqlTable };
        }

        public async Task<DataResponse> GetDataAsync(DataRequest req, string sqlConnStr)
        {
            var sqlTableSchema = req.ConfigParams[ConfigSql.SqlTable.ToString()];
            if (string.IsNullOrEmpty(sqlTableSchema)) {
                return default(DataResponse);
            }

            var tblschemaInfo = ParseSchemaTable(sqlTableSchema);
            var schema = await GetSchemaAsync(sqlConnStr, tblschemaInfo.SchemaName, tblschemaInfo.TableName);

            var schemaFields = req.Fields.Select(f => schema.Fields.FirstOrDefault(ff=> ff.FieldName == f.Name)).ToList();
            var projectionFields = req.Fields.Where(f=> !( f.ForFilterOnly == true ));
            var filters = req?.DimensionsFilters;
            //TODO: filters
            //GetSchemaAsync

            var dataResponse = await RunQueryAsync(sqlConnStr, sqlTableSchema, projectionFields, schemaFields, filters);
            return dataResponse;
        }


        /// 4.5.1 https://docs.microsoft.com/en-us/azure/azure-functions/functions-scenario-database-table-cleanup

        //public async Task<DataResponse> RunQueryAsync(SqlConnectionParams cParams, DataRequest req) { 
        //  var str = $"Server=tcp:{cParams.Host}.database.windows.net,1433;Database={cParams.Database};User ID={cParams.Username};Password={cParams.Password};Trusted_Connection=False;Encrypt=True;";
        //}

        public async Task<DataResponse> RunQueryAsync(string sqlConnStr, string tableName, IEnumerable<Field> projectionFields, List<SchemaField> fields, List<List<DimensionsFilter>> filters)
        {

            var dataResponse = new DataResponse() {
                FiltersApplied = ((filters?.Count() ?? 0) > 0) ? true : false, 
                Schema = projectionFields.Select(f => fields.FirstOrDefault(ff => ff.FieldName == f.Name)).ToList(), 
                Rows = new List<GDS.Gateway.Models.Io.DataRow>() 
            };
            var fieldMap = fields?.ToDictionary( k => k.FieldName, v => v);
            var filterStr = BuildFilter(filters, fieldMap);
            using (var conn = new SqlConnection(sqlConnStr))
            {
                var fieldList = " * ";
                var maybeFields = ( string.Join(", ", (projectionFields?.Select(f => f.Name) ) ?? new string[0]));
                fieldList = string.IsNullOrWhiteSpace(maybeFields) ? fieldList : maybeFields;
                var sqlStr = $"SELECT {fieldList} FROM { tableName} " + ( string.IsNullOrWhiteSpace(filterStr) ? "" : "WHERE " + filterStr );
                using (var reader = await conn.ExecuteReaderAsync(sqlStr))
                {
                    var len = reader.FieldCount;
                    while (reader.Read())
                    {
                        var dr = new GDS.Gateway.Models.Io.DataRow() { Values = new string[len] };
                        for (int i = 0; i < len; i++)
                        {
                            var t = reader.GetFieldType( i );
                            if (t == typeof(DateTime))
                            {
                                dr.Values[i] = reader.GetDateTime(i).ToString("yyyyMMdd");
                            }
                            else {
                                dr.Values[i] = Convert.ToString(reader.GetValue(i));
                            }
                        }
                        dataResponse.Rows.Add(dr);
                    }

                }
            }

            return dataResponse;
        }
    }

    public class SchemaDetail
    {
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }
    }
    public class SchemaTableInfo {
        public string SchemaName { get; set; }
        public string TableName { get; set; }

    }
}
