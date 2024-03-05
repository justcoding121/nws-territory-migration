using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Models;

public class TerritoryHelperDoNotCall
{
    [Description("Territory type")]
    public string TerritoryType { get; set; }

    [Description("Territory number")]
    public string TerritoryNumber { get; set; }

    [Description("Location type")]
    public string LocationType { get; set; }

    [Description("Status")]
    public string Status { get; set; }

    [Description("Latitude")]
    public string Latitude { get; set; }

    [Description("Longitude")]
    public string Longitude { get; set; }

    [Description("Address")]
    public string Address { get; set; }

    [Description("Number")]
    public string Number { get; set; }

    [Description("Street")]
    public string Street { get; set; }

    [Description("Unit")]
    public string Unit { get; set; }


    [Description("Floor")]
    public string Floor { get; set; }


    [Description("City")]
    public string City { get; set; }


    [Description("Country")]
    public string Country { get; set; }

    [Description("Postal code")]
    public string PostalCode { get; set; }

    [Description("State")]
    public string State { get; set; }

    [Description("Country code")]
    public string CountryCode { get; set; }

    [Description("Last visited")]
    public string LastVisited { get; set; }

    [Description("Last updated")]
    public string LastUpdated { get; set; }


    [Description("Date created")]
    public string DateCreated { get; set; }
}


