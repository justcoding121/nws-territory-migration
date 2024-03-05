namespace Sandbox.Models;


using System.Xml.Serialization;


[XmlRoot(ElementName = "kml", Namespace = "http://www.opengis.net/kml/2.2")]
public class TerritoryHelperKmlRoot
{

    [XmlElement(ElementName = "Document", Namespace = "http://www.opengis.net/kml/2.2")]
    public Document Document { get; set; }

    [XmlAttribute(AttributeName = "xmlns", Namespace = "")]
    public string Xmlns { get; set; }

    [XmlText]
    public string Text { get; set; }
}

[XmlRoot(ElementName = "IconStyle", Namespace = "http://www.opengis.net/kml/2.2")]
public class IconStyle
{

    [XmlElement(ElementName = "scale", Namespace = "http://www.opengis.net/kml/2.2")]
    public double Scale { get; set; }

    [XmlElement(ElementName = "color", Namespace = "http://www.opengis.net/kml/2.2")]
    public string Color { get; set; }

    [XmlElement(ElementName = "Icon", Namespace = "http://www.opengis.net/kml/2.2")]
    public Icon Icon { get; set; }
}

[XmlRoot(ElementName = "LabelStyle", Namespace = "http://www.opengis.net/kml/2.2")]
public class LabelStyle
{

    [XmlElement(ElementName = "scale", Namespace = "http://www.opengis.net/kml/2.2")]
    public double Scale { get; set; }
}

[XmlRoot(ElementName = "LineStyle", Namespace = "http://www.opengis.net/kml/2.2")]
public class LineStyle
{

    [XmlElement(ElementName = "color", Namespace = "http://www.opengis.net/kml/2.2")]
    public string Color { get; set; }

    [XmlElement(ElementName = "width", Namespace = "http://www.opengis.net/kml/2.2")]
    public int Width { get; set; }
}

[XmlRoot(ElementName = "PolyStyle", Namespace = "http://www.opengis.net/kml/2.2")]
public class PolyStyle
{

    [XmlElement(ElementName = "color", Namespace = "http://www.opengis.net/kml/2.2")]
    public string Color { get; set; }
}

[XmlRoot(ElementName = "Style", Namespace = "http://www.opengis.net/kml/2.2")]
public class Style
{

    [XmlElement(ElementName = "IconStyle", Namespace = "http://www.opengis.net/kml/2.2")]
    public IconStyle IconStyle { get; set; }

    [XmlElement(ElementName = "LabelStyle", Namespace = "http://www.opengis.net/kml/2.2")]
    public LabelStyle LabelStyle { get; set; }

    [XmlElement(ElementName = "LineStyle", Namespace = "http://www.opengis.net/kml/2.2")]
    public LineStyle LineStyle { get; set; }

    [XmlElement(ElementName = "PolyStyle", Namespace = "http://www.opengis.net/kml/2.2")]
    public PolyStyle PolyStyle { get; set; }

    [XmlAttribute(AttributeName = "id", Namespace = "")]
    public string Id { get; set; }

    [XmlText]
    public string Text { get; set; }
}

[XmlRoot(ElementName = "Icon", Namespace = "http://www.opengis.net/kml/2.2")]
public class Icon
{

    [XmlElement(ElementName = "href", Namespace = "http://www.opengis.net/kml/2.2")]
    public string Href { get; set; }
}

[XmlRoot(ElementName = "LinearRing", Namespace = "http://www.opengis.net/kml/2.2")]
public class LinearRing
{

    [XmlElement(ElementName = "coordinates", Namespace = "http://www.opengis.net/kml/2.2")]
    public string Coordinates { get; set; }
}

[XmlRoot(ElementName = "outerBoundaryIs", Namespace = "http://www.opengis.net/kml/2.2")]
public class OuterBoundaryIs
{

    [XmlElement(ElementName = "LinearRing", Namespace = "http://www.opengis.net/kml/2.2")]
    public LinearRing LinearRing { get; set; }
}

[XmlRoot(ElementName = "Polygon", Namespace = "http://www.opengis.net/kml/2.2")]
public class Polygon
{

    [XmlElement(ElementName = "tessellate", Namespace = "http://www.opengis.net/kml/2.2")]
    public int Tessellate { get; set; }

    [XmlElement(ElementName = "outerBoundaryIs", Namespace = "http://www.opengis.net/kml/2.2")]
    public OuterBoundaryIs OuterBoundaryIs { get; set; }

    [XmlElement(ElementName = "altitudeMode", Namespace = "http://www.opengis.net/kml/2.2")]
    public string AltitudeMode { get; set; }
}

[XmlRoot(ElementName = "MultiGeometry", Namespace = "http://www.opengis.net/kml/2.2")]
public class MultiGeometry
{

    [XmlElement(ElementName = "Polygon", Namespace = "http://www.opengis.net/kml/2.2")]
    public Polygon Polygon { get; set; }
}

[XmlRoot(ElementName = "Placemark", Namespace = "http://www.opengis.net/kml/2.2")]
public class Placemark
{

    [XmlElement(ElementName = "name", Namespace = "http://www.opengis.net/kml/2.2")]
    public string Name { get; set; }

    [XmlElement(ElementName = "territoryType", Namespace = "http://www.opengis.net/kml/2.2")]
    public string TerritoryType { get; set; }

    [XmlElement(ElementName = "territoryNumber", Namespace = "http://www.opengis.net/kml/2.2")]
    public string TerritoryNumber { get; set; }

    [XmlElement(ElementName = "territoryTypeCode", Namespace = "http://www.opengis.net/kml/2.2")]
    public object TerritoryTypeCode { get; set; }

    [XmlElement(ElementName = "territoryTypeColor", Namespace = "http://www.opengis.net/kml/2.2")]
    public string TerritoryTypeColor { get; set; }

    [XmlElement(ElementName = "territoryNotes", Namespace = "http://www.opengis.net/kml/2.2")]
    public string TerritoryNotes { get; set; }

    [XmlElement(ElementName = "territoryLabel", Namespace = "http://www.opengis.net/kml/2.2")]
    public string TerritoryLabel { get; set; }

    [XmlElement(ElementName = "styleUrl", Namespace = "http://www.opengis.net/kml/2.2")]
    public string StyleUrl { get; set; }

    [XmlElement(ElementName = "MultiGeometry", Namespace = "http://www.opengis.net/kml/2.2")]
    public MultiGeometry MultiGeometry { get; set; }
}

[XmlRoot(ElementName = "Document", Namespace = "http://www.opengis.net/kml/2.2")]
public class Document
{

    [XmlElement(ElementName = "name", Namespace = "http://www.opengis.net/kml/2.2")]
    public string Name { get; set; }

    [XmlElement(ElementName = "Style", Namespace = "http://www.opengis.net/kml/2.2")]
    public List<Style> Style { get; set; }

    [XmlElement(ElementName = "Placemark", Namespace = "http://www.opengis.net/kml/2.2")]
    public List<Placemark> Placemark { get; set; }
}


