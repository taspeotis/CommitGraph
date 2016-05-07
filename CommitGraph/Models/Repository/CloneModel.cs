namespace CommitGraph.Models.Repository
{
    public sealed class CloneModel
    {
        public string RemoteUrl { get; set; }

        public string WorkingDirectory { get; set; }
    }
}