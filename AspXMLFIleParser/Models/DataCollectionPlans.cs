using System.Xml.Serialization;

[XmlRoot(ElementName = "ParameterRequest")]
public class ParameterRequest
{

    [XmlAttribute(AttributeName = "parameterName")]
    public string ParameterName { get; set; }

    [XmlAttribute(AttributeName = "sourceId")]
    public string SourceId { get; set; }
}

[XmlRoot(ElementName = "EventRequest")]
public class EventRequest
{

    [XmlElement(ElementName = "ParameterRequest")]
    public List<ParameterRequest> ParameterRequest { get; set; }

    [XmlAttribute(AttributeName = "eventId")]
    public string EventId { get; set; }

    [XmlAttribute(AttributeName = "sourceId")]
    public string SourceId { get; set; }
}

[XmlRoot(ElementName = "TraceRequest")]
public class TraceRequest
{

    [XmlElement(ElementName = "ParameterRequest")]
    public List<ParameterRequest> ParameterRequest { get; set; }

    [XmlAttribute(AttributeName = "collectionCount")]
    public int CollectionCount { get; set; }

    [XmlAttribute(AttributeName = "groupSize")]
    public int GroupSize { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public int Id { get; set; }

    [XmlAttribute(AttributeName = "intervalInSeconds")]
    public int IntervalInSeconds { get; set; }

    [XmlAttribute(AttributeName = "isCyclical")]
    public bool IsCyclical { get; set; }
}

[XmlRoot(ElementName = "DataCollectionPlan")]
public class DataCollectionPlan
{

    [XmlElement(ElementName = "Description")]
    public object Description { get; set; }

    [XmlElement(ElementName = "EventRequest")]
    public List<EventRequest> EventRequest { get; set; }

    [XmlElement(ElementName = "TraceRequest")]
    public TraceRequest TraceRequest { get; set; }

    [XmlAttribute(AttributeName = "id")]
    public string Id { get; set; }

    [XmlAttribute(AttributeName = "intervalInMinutes")]
    public int IntervalInMinutes { get; set; }

    [XmlAttribute(AttributeName = "isPersistent")]
    public bool IsPersistent { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
}

[XmlRoot(ElementName = "DataCollectionPlans")]
public class DataCollectionPlans
{

    [XmlElement(ElementName = "DataCollectionPlan")]
    public List<DataCollectionPlan> DataCollectionPlan { get; set; }

    [XmlAttribute(AttributeName = "xmlns")]
    public string Xmlns { get; set; }
}