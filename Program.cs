using System.Globalization;
using System.Text;
using CsvHelper;
using NetTopologySuite.Geometries;
using NwsTerritoryMigration.Helpers;
using NwsTerritoryMigration.Mappings;
using NwsTerritoryMigration.Models;
using Sandbox.Helpers;
using Sandbox.Models;
using XmlHelper = Sandbox.Helpers.XmlHelper;

namespace Sandbox;

public partial class Program
{
    public static void Main(string[] args)
    {
        var territories = ImportTerritories();

        //territories = territories.GroupBy(x => $"{x.TypeCode} {x.Number}").SelectMany(x => x.Skip(1)).ToList();

        //territories.ForEach(x => Console.WriteLine($"{x.TypeCode} {x.Number} {x.Suffix}"));

        //territories.ForEach(territory => { territory.TypeCode = "CA"; territory.TypeName = "Conflict Again"; });

        //territories = territories.Where(x => x.TypeCode == "CA" && x.Number == 16 && x.Suffix == "3").ToList();

        WriteCsvRecords("territories.csv", territories);

        var assignments = ImportAssignments(territories)
            .Select(x =>
            {
                var territory = territories.Find(y => y.TerritoryLabel == x.TerritoryLabel);

                if (territory != null)
                {
                    x.TypeCode = territory.TypeCode;
                    x.Number = territory.Number;
                }

                return x;

            })
            .Where(x => x.Number != 0 && !string.IsNullOrEmpty(x.TypeCode))
            .ToList();

        WriteCsvRecords("assignments.csv", assignments);

        ResizeMaps("H:\\.shortcut-targets-by-id\\1dhTO8e8JpZAOmik3DnO85rEMpZr19tYf\\Territory png");
    }

    public static List<NwsTerritory> ImportTerritories()
    {
        var xml = File.ReadAllText("New Brighton Congregation.kml");
        var kmlFile = XmlHelper.Deserialize<TerritoryHelperKmlRoot>(xml);

        var placeMarks = kmlFile!.Document.Placemark.ToList();

        kmlFile.Document.Placemark.Clear();

        var outputDir = "Territories";

        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);

            foreach (var placeMark in placeMarks)
            {
                kmlFile!.Document.Placemark.Add(placeMark);

                var serialized = XmlHelper.Serialize(placeMark);

                File.WriteAllText(Path.Combine(outputDir, placeMark.Name + ".kml"), serialized);

                kmlFile!.Document.Placemark.Remove(placeMark);
            }
        }

        var allDoNotCalls = ExcelReader.ReadExcel<TerritoryHelperDoNotCall>("New Brighton Congregation - Do not call.xlsx");

        var areas = string.Join(Environment.NewLine, placeMarks.Select(x => x.TerritoryType).Distinct());

        var territories = placeMarks.Select(x =>
        {
            int territoryNumber;
            string suffix = null;

            if (x.TerritoryNumber.Contains("-"))
            {
                var hiphenSplit = x.TerritoryNumber.Split('-');

                territoryNumber = int.Parse(hiphenSplit[0].Trim());
                suffix = hiphenSplit.Length > 1 ? hiphenSplit[1].Contains("(") ? hiphenSplit[1].Substring(0, hiphenSplit[1].IndexOf("(")).Trim() : hiphenSplit[1].Trim() : null;
            }
            else
            {
                territoryNumber = int.Parse(x.TerritoryNumber.Contains("(") ? x.TerritoryNumber.Substring(0, x.TerritoryNumber.IndexOf("(")).Trim() : x.TerritoryNumber.Trim());
            }

            var territoryTypeName = TerritoryMap.GetTerritoryTypeName(x.TerritoryType);
            var territoryTypeCode = TerritoryMap.GetTerritoryTypeCode(x.TerritoryType);

            var areaName = AreaMap.GetAreaName(x.TerritoryType);
            var areaCode = AreaMap.GetAreaCode(x.TerritoryType);

            var area = !string.IsNullOrEmpty(areaCode) ? areaName : null;

            //Console.WriteLine($"{x.TerritoryNumber} {x.TerritoryType} => {territoryNumber}{(suffix == null ? string.Empty : $"-{suffix}")} {territoryTypeCode} - {territoryTypeName} {area}");

            var points = x.MultiGeometry.Polygon.OuterBoundaryIs.LinearRing.Coordinates.Split(" ")
                                .SkipLast(1)
                                .Select(x =>
                                {
                                    var split = x.Split(",");
                                    return new Point(new Coordinate(double.Parse(split[0]), double.Parse(split[1])));
                                }).ToArray();

            var centroid = GeoHelper.GetCentroid(points);

            var doNotCalls = string.Join(Environment.NewLine, allDoNotCalls
                        .FindAll(y => y.TerritoryNumber == x.TerritoryNumber && y.TerritoryType == x.TerritoryType)
                        .Select(x => x.Address));

            var territoryNotes = HtmlHelper.ConvertToPlainText(x.TerritoryNotes);

            return new NwsTerritory()
            {
                Number = territoryNumber,
                Suffix = suffix,
                TypeCode = territoryTypeCode,
                TypeName = territoryTypeName,
                Area = area ?? "Not Assigned",
                Link = $"https://www.google.com/maps/dir/?api=1&destination={centroid.Y},{centroid.X}",
                CustomNotes1 = territoryNotes,
                CustomNotes2 = doNotCalls,
                TerritoryLabel = x.TerritoryLabel
            };

        }).ToList();


        return territories;

    }


    private static List<NwsAssignment> ImportAssignments(List<NwsTerritory> territories)
    {
        var nwsPublishers = CsvFileParser<NwsPublisher>.ParseLines("New Brighton Persons Everyone.csv", CsvFileParser<NwsPublisher>.CsvConfiguration)
                           .Where(x => !string.IsNullOrEmpty(x.DisplayName))
                           .Select(x => x.DisplayName.Trim())
                           .ToList();

        nwsPublishers.Add("");

        var assignments = ExcelReader.ReadExcel<TerritoryHelperAssignment>("New Brighton Congregation - Assignments.xlsx");

        var nameMap = NameMap.Map.Split(Environment.NewLine)
                       .Select(x => x.Trim())
                       .Where(x => x != string.Empty)
                       .Select(x => x.Split("=>"))
                       .ToDictionary(x => x[0].Trim(), x => x[1].Trim());

        foreach (var assignment in assignments)
        {
            if (!nwsPublishers.Contains(assignment.Name))
            {
                assignment.Name = nameMap[assignment.Name];
            }
        }


        return assignments.Where(x => !string.IsNullOrEmpty(x.Assigned)).Select(x =>
        {
            return new NwsAssignment()
            {
                Campaign = x.Notes.ToLower().Contains("memorial") || x.Notes.ToLower().Contains("convention") || x.Notes.ToLower().Contains("campaign") ? "TRUE" : "FALSE",
                TerritoryLabel = x.TerritoryLabel,
                Publisher = x.Name,
                DateAssigned = DateOnly.Parse(x.Assigned).ToString("yyyyMMdd"),
                DateCompleted = !string.IsNullOrEmpty(x.Returned) ? DateOnly.Parse(x.Returned).ToString("yyyyMMdd") : null
            };

        }).ToList();
    }

    private static void ResizeMaps(string src)
    {
        double minKbFileSize = 450;
        double maxKbFileSize = 2500;

        var srcDir = new DirectoryInfo(src);
        var srcFiles = srcDir.EnumerateFiles("*.png", SearchOption.TopDirectoryOnly)
           .Where(file => file.Length / 1024d > minKbFileSize && file.Length / 1024d < maxKbFileSize)
           .Select(x => x.FullName)
           .ToList();

        //var dstDir = Path.Combine(src, "NWS");

        //if(!Directory.Exists(dstDir))
        //{
        //    throw new DirectoryNotFoundException(dstDir);
        //}

        //foreach(var srcFile in srcFiles)
        //{
        //    File.Copy(srcFile, Path.Combine(dstDir, Path.GetFileName(srcFile)));
        //}

    }


    private static void WriteCsvRecords<T>(string fileName, List<T> records)
    {
        using var writer = new StreamWriter(fileName, false, new UTF8Encoding(true));
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.WriteRecords(records);
    }

}