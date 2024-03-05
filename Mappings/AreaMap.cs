using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NwsTerritoryMigration.Mappings
{
    public class AreaMap
    {
        private static Dictionary<string, string> areaCodeMap = new()
        {
             { "Copperfield", "CF" },
             { "Douglas Glen", "DG" },
             { "Mahogany", "MH" },
             { "McKenzie Towne", "MT" },
             { "New Brighton", "NB" },
             { "Prestwick", "PW" },
             { "Quarry Park", "QP" },
             { "Riverbend", "RB" },
        };

        private static Dictionary<string, string> notHouseType = new()
        {
             { "Apartments", "" },
             { "Business", "" },
             { "Rural", "" },
        };

        public static string GetAreaCode(string territoryType)
        {
            if (areaCodeMap.ContainsKey(territoryType))
                return areaCodeMap[territoryType];

            return null;
        }

        public static string GetAreaName(string territoryType)
        {
            if (notHouseType.ContainsKey(territoryType))
                return null;

            return territoryType.Trim();
        }

    }
}
