using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NwsTerritoryMigration.Models
{
    public class NwsAssignment
    {
        public string TerritoryID { get; set; }

        public string TypeCode { get; set; }

        public int Number { get; set; }

        public string DateCompleted { get; set; }

        public string DateAssigned { get; set; }

        public string Campaign { get; set; }

        public string Publisher { get; set; }

        internal string TerritoryLabel { get; set; }
    }
}
