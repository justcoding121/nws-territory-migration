using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Helpers;

public static class ExcelReader
{
    public static List<T> ReadExcel<T>(string fileName) where T : new()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using ExcelPackage package = new ExcelPackage(fileName);
        ExcelWorkbook workbook = package.Workbook;
        if (workbook != null)
        {
            ExcelWorksheet worksheet = workbook.Worksheets.First();
            if (worksheet != null)
            {
                return worksheet.Read<T>();
            }
        }

        throw new NotImplementedException();
    }

    private static List<T> Read<T>(this ExcelWorksheet worksheet) where T : new()
    {
        List<T> collection = new List<T>();

        DataTable dt = new DataTable();
        foreach (var firstRowCell in new T().GetType().GetProperties().ToList())
        {
            //Add table colums with properties of T
            dt.Columns.Add(firstRowCell.Name);
        }
        for (int rowNum = 2; rowNum <= worksheet.Dimension.End.Row; rowNum++)
        {
            var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
            DataRow row = dt.Rows.Add();
            foreach (var cell in wsRow)
            {
                row[cell.Start.Column - 1] = cell.Text;
            }
        }

        //Get the colums of table
        var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();

        //Get the properties of T
        List<PropertyInfo> properties = new T().GetType().GetProperties().ToList();

        collection = dt.AsEnumerable().Select(row =>
        {
            T item = Activator.CreateInstance<T>();
            foreach (var pro in properties)
            {
                if (columnNames.Contains(pro.Name) || columnNames.Contains(pro.Name.ToUpper()))
                {
                    PropertyInfo pI = item.GetType().GetProperty(pro.Name);
                    pro.SetValue(item, (row[pro.Name] == DBNull.Value) ? null : Convert.ChangeType(row[pro.Name], (Nullable.GetUnderlyingType(pI.PropertyType) == null) ? pI.PropertyType : Type.GetType(pI.PropertyType.GenericTypeArguments[0].FullName)));
                }
            }
            return item;
        }).ToList();



        return collection;
    }
}
