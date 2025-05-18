using Newtonsoft.Json.Linq;

using SimpleBlocks.Client.Extensions;

namespace SimpleBlocks.Client.Helpers;

public class Config(Dictionary<string, string> nameConfiguration, Dictionary<string, string> syntaxConfiguration, JArray blocks, JObject semantics)
{
    public Dictionary<string, string> NameConfiguration { get; init; } = nameConfiguration;
    public Dictionary<string, string> SyntaxConfiguration { get; init; } = syntaxConfiguration;
    public JArray Blocks { get; init; } = blocks;
    public JObject Semantics { get; init; } = semantics;

    public Config(JArray blocks, JObject semantics) :
        this(nameConfiguration:  (semantics[nameof(NameConfiguration)] as JObject)?.ToDictionary() ?? throw new ArgumentNullException(nameof(NameConfiguration)),
            syntaxConfiguration: (semantics[nameof(SyntaxConfiguration)] as JObject)?.ToDictionary() ?? throw new ArgumentNullException(nameof(SyntaxConfiguration)),
            blocks:              blocks ?? throw new ArgumentNullException(nameof(blocks)),
            semantics:           semantics ?? throw new ArgumentNullException(nameof(semantics)))
    {
        semantics.Remove(nameof(SyntaxConfiguration));
        semantics.Remove(nameof(NameConfiguration));
    }
}