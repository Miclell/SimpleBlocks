namespace SimpleBlocks.Client.Helpers
{
    public class LanguageFiles
    {
        public string LanguageKey { get; set; } = default!;
        public string BlocksJson { get; set; } = default!;
        public string SemanticsJson { get; set; } = default!;
        public LanguageSource Source { get; set; } = LanguageSource.Local;
    }

    public enum LanguageSource
    {
        Server,
        Local
    }
} 