namespace LiveHTS.Presentation
{
    public class VMStore
    {
        public string Store { get; set; }
        public bool HasData { get { return !string.IsNullOrWhiteSpace(Store); }}
    }
}