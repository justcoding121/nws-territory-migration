using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NwsTerritoryMigration.Models
{
    public class NwsTerritory
    {
        public int Number { get; set; }
        public string Suffix { get; set; }

        public string TypeCode { get; set; }
        public string TypeName { get; set; }

        public string Area { get; set; }


        public string Link { get; set; }

        public string CustomNotes1 { get; set; }

        public string CustomNotes2 { get; set;}

        internal string TerritoryLabel { get; set; }

    }
}
