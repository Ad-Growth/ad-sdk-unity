public abstract class IClientProfile
{
    abstract public int age { get; set; }
    abstract public int minAge { get; set; }
    abstract public int maxAge { get; set; }
    abstract public Gender gender { get; set; }
    abstract public IClientAddress clientAddress { get; }

    public abstract void AddInterest(string interest);
    public abstract void RemoveInterest(string interest);
    public enum Gender
    {
        ALL, MALE, FEMALE
    }
}