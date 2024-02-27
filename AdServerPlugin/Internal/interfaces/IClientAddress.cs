public abstract class IClientAddress
{
    abstract public string city { get; set; }
    abstract public string state { get; set; }
    abstract public string country { get; set; }
    abstract public double latitude { get; set; }
    abstract public double longitude { get; set; }
}