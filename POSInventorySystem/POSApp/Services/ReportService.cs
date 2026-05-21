using System.Data;
using System.IO;
using System.Text;

namespace POSApp.Services
{
    public class ReportService
    {
        public bool ExportToCSV(DataTable data, string filePath)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                // Headers
                IEnumerable<string> columnNames = data.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                sb.AppendLine(string.Join(",", columnNames));

                // Rows
                foreach (DataRow row in data.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field =>
                        string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                    sb.AppendLine(string.Join(",", fields));
                }

                File.WriteAllText(filePath, sb.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
