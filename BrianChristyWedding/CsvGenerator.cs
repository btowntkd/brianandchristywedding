using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BrianChristyWedding
{
    public class CsvGenerator
    {
        public const string CsvSeperator = ",";

        public static string Generate<TEntity>(IEnumerable<TEntity> items, params CsvColumn<TEntity>[] columns)
        {
            var csvResult = new StringBuilder();

            // Generate column header row
            csvResult.AppendLine(
                string.Join(
                    CsvSeperator, 
                    columns.Select(x => StringToCsvCell(x.ColumnName))));

            // Generate rows
            foreach (var item in items)
            {
                csvResult.AppendLine(
                    string.Join(
                        CsvSeperator,
                        columns.Select(x => GenerateCsvCell(x.ColumnValueSelector(item)))));
            }

            return csvResult.ToString();
        }

        private static string GenerateCsvCell(object obj)
        {
            if(obj == null)
                return string.Empty;
            return StringToCsvCell(obj.ToString());
        }

        /// <summary>
        /// Turn a string into a CSV cell output
        /// </summary>
        /// <param name="str">String to output</param>
        /// <returns>The CSV cell formatted string</returns>
        private static string StringToCsvCell(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;

            bool mustQuote = (str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n"));
            if (mustQuote)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\"");
                foreach (char nextChar in str)
                {
                    sb.Append(nextChar);
                    if (nextChar == '"')
                        sb.Append("\"");
                }
                sb.Append("\"");
                return sb.ToString();
            }

            return str;
        }
    }

    public class CsvColumn<TEntity>
    {
        public CsvColumn(string columnName, Func<TEntity, object> columnValueSelector)
        {
            ColumnName = columnName;
            ColumnValueSelector = columnValueSelector;
        }

        public string ColumnName { get; private set; }
        public Func<TEntity, object> ColumnValueSelector { get; private set; }
    }
}