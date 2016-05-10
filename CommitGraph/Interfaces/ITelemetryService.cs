namespace CommitGraph.Interfaces
{
    public interface ITelemetryService
    {
        string EmailAddress { get; set; }

        bool Enabled { get; set; }

        string UserName { get; set; }
    }
}