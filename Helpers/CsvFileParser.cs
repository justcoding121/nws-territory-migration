using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Sandbox.Helpers;

public static class CsvFileParser<T>
{
    public static IEnumerable<T> ParseLines(string filePath, CsvConfiguration csvConfiguration)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, csvConfiguration);
        var records = csv.GetRecords<T>();

        foreach (var record in records)
        {
            yield return record;
        }
    }

    public static CsvConfiguration CsvConfiguration = new CsvConfiguration(CultureInfo.GetCultureInfo("en-US"))
    {
        HasHeaderRecord = true,

        BadDataFound = args =>
        {
            throw new Exception();
        }
    };
}
