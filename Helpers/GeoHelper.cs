using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NwsTerritoryMigration.Helpers
{
    public class GeoHelper
    {
        public static Point GetCentroid(Point[] nodes)
        {
            int count = nodes.Length;

            double x = 0, y = 0, area = 0, k;
            Point a, b = nodes[count - 1];

            for (int i = 0; i < count; i++)
            {
                a = nodes[i];

                k = a.Y * b.X - a.X * b.Y;
                area += k;
                x += (a.X + b.X) * k;
                y += (a.Y + b.Y) * k;

                b = a;
            }
            area *= 3;

            return (area == 0) ? Point.Empty : new Point(x /= area, y /= area);
        }
    }
}
