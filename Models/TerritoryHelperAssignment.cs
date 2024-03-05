using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Models;

public class TerritoryHelperAssignment
{
    [Description("Territory type")]
    public string TerritoryType { get; set; }

    [Description("Territory number")]
    public string TerritoryNumber { get; set; }

    [Description("Territory label")]
    public string TerritoryLabel { get; set; }

    [Description("Territory notes")]
    public string TerritoryNotes { get; set; }

    [Description("Name")]
    public string Name { get; set; }

    [Description("Email")]
    public string Email { get; set; }

    [Description("Phone")]
    public string Phone { get; set; }

    [Description("Assigned")]
    public string Assigned { get; set; }

    [Description("Returned")]
    public string Returned { get; set; }

    [Description("Notes")]
    public string Notes { get; set; }
}
