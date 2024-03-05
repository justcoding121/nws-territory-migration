using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NwsTerritoryMigration.Mappings
{
    public class TerritoryMap
    {
        private static Dictionary<string, string> territoryNameMap = new()
        {
            { "Apartments", "Apartment" },
            { "Business", "Business" },
            { "Rural", "Rural" }
        };

        private static Dictionary<string, string> territoryTypeMap = new()
        {
             { "Apartments", "A" },
             { "Business", "B" },
             { "Rural", "R" },

             { "Copperfield", "CF" },
             { "Douglas Glen", "DG" },
             { "Mahogany", "MH" },
             { "McKenzie Towne", "MT" },
             { "New Brighton", "NB" },
             { "Prestwick", "PW" },
             { "Quarry Park", "QP" },
             { "Riverbend", "RB" },
        };

        public static string GetTerritoryTypeName(string territoryType)
        {
            if (territoryNameMap.ContainsKey(territoryType))
                return territoryNameMap[territoryType];

            return "House";
        }

        public static string GetTerritoryTypeCode(string territoryTypeName)
        {
            return territoryTypeMap[territoryTypeName];
        }
    }
}
