﻿namespace CRB.TPM.Data.Sharding
{
    internal class PostgreQuery : IQuery
    {
        public PostgreQuery(IDatabase db) : base(db)
        {

        }

        internal override IQuery Add<T>(ITable<T> table, string asName = null)
        {
            var primaryKey = table.SqlField.PrimaryKey;
            if (string.IsNullOrEmpty(asName))
            {
                sqlTable = $"{table.Name}";
                returnFields = table.SqlField.AllFields;
                sqlOrderBy = $" ORDER BY {primaryKey}";
            }
            else
            {
                sqlTable = $"{table.Name} AS {asName}";
                returnFields = $"{asName}.*";
                sqlOrderBy = $" ORDER BY {asName}.{primaryKey}";
            }
            _sqlTable = sqlTable;
            _returnFields = returnFields;
            _sqlOrderBy = sqlOrderBy;
            return this;
        }

        public override IQuery InnerJoin<T>(ITable<T> table, string asName, string on)
        {
            sqlTable += $" INNER JOIN {table.Name} AS {asName} ON {on}";
            return this;
        }

        public override IQuery LeftJoin<T>(ITable<T> table, string asName, string on)
        {
            sqlTable += $" LEFT JOIN {table.Name} AS {asName} ON {on}";
            return this;
        }

        public override IQuery RightJoin<T>(ITable<T> table, string asName, string on)
        {
            sqlTable += $" RIGHT JOIN {table.Name} AS {asName} ON {on}";
            return this;
        }

        public override string GetSql()
        {
            if (take == 0)
            {
                return $"SELECT {returnFields} FROM {string.Concat(sqlTable, sqlWhere, sqlGroupBy, sqlHaving, sqlOrderBy)}";
            }
            else
            {
                return $"SELECT {returnFields} FROM {string.Concat(sqlTable, sqlWhere, sqlGroupBy, sqlHaving, sqlOrderBy)} LIMIT {take} OFFSET {skip}";
            }
        }

        public override string GetSqlCount()
        {
            return $"SELECT COUNT(1) FROM {string.Concat(sqlTable, sqlWhere, sqlGroupBy, sqlHaving)}";
        }
    }
}
