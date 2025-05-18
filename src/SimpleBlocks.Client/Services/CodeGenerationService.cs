using System.Text;
using System.Globalization;
using Newtonsoft.Json.Linq;
using SimpleBlocks.Client.Helpers;

namespace SimpleBlocks.Client.Services;

public class CodeGenerationService(Config config)
{
    private readonly HashSet<string> _collectedImports = [];
    
    public async Task<string> GenerateAsync(JArray input)
    {
        _collectedImports.Clear();
        var codes = await Task.WhenAll(
            input.OfType<JObject>().Select(GenerateBlockAsync)
        );

        var body = string.Join("\n", codes.Where(c => !string.IsNullOrEmpty(c)));
        var importBlock = string.Join("\n", _collectedImports);

        return $"{importBlock}\n\n{body}";
    }

    private async Task<string> GenerateBlockAsync(JObject block)
    {
        try
        {
            var blockName = block["name"]?.ToString()
                             ?? throw new ArgumentNullException($"Block name is missing in {block}");

            if (!config.Semantics.TryGetValue(blockName, out var blockSemantics) || blockSemantics["schema"] == null)
            {
                if (block["inputFields"] is not JObject { Count: 1 } fields)
                    throw new Exception($"Schema for {blockName} not found");

                var field = fields.Properties().First();
                var value = field.Value.ToString();

                return IsQuotedBlock(blockName) ? $"\"{value}\"" : value;
            }

            var schema = blockSemantics["schema"]?.ToString()
                         ?? throw new Exception($"Schema for {blockName} not found");
            
            if (blockSemantics is JObject semanticsObj &&
                semanticsObj.TryGetValue("languageImports", out var importsToken) &&
                importsToken is JArray imports)
            {
                foreach (var import in imports.OfType<JValue>().Select(v => v.ToString(CultureInfo.InvariantCulture)))
                {
                    _collectedImports.Add(import);
                }
            }


            var parameters = await ExtractParametersAsync(block);
            return await InterpolateSchemaAsync(schema, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Block error: {block.ToString(Newtonsoft.Json.Formatting.None)}\n{ex.Message}");
            return string.Empty;
        }
    }

    private async Task<Dictionary<string, string>> ExtractParametersAsync(JObject block)
    {
        var parameters = new Dictionary<string, string>();

        foreach (var prop in block.Properties())
        {
            switch (prop.Name)
            {
                case "name":
                    continue;
                case "inputFields" when prop.Value is JObject fields:
                {
                    foreach (var field in fields.Properties())
                    {
                        parameters[$"inputFields.{field.Name}"] = await ProcessTokenAsync(field.Value);
                    }

                    break;
                }
                default:
                    parameters[prop.Name] = await ProcessTokenAsync(prop.Value);
                    break;
            }
        }

        foreach (var syntaxParam in config.SyntaxConfiguration)
        {
            parameters[syntaxParam.Key] = syntaxParam.Value;
        }

        return parameters;
    }

    private async Task<string> ProcessTokenAsync(JToken token)
    {
        return token switch
        {
            JObject obj when obj.TryGetValue("name", out _) => await GenerateBlockAsync(obj),
            JObject {Count: 1, First: JObject inner} when inner["name"] != null =>
                await GenerateBlockAsync(inner),
            JArray arr => await GenerateBodyAsync(arr),
            JValue val => val.ToString(CultureInfo.InvariantCulture),
            _ => string.Empty
        };
    }

    private async Task<string> GenerateBodyAsync(JArray body)
    {
        var codes = await Task.WhenAll(
            body.OfType<JObject>().Select(GenerateBlockAsync)
        );

        return AddIndentation(string.Join("\n", codes.Where(c => !string.IsNullOrEmpty(c))));
    }

    private static Task<string> InterpolateSchemaAsync(string schema, Dictionary<string, string> parameters)
    {
        var sb = new StringBuilder(schema);

        foreach (var param in parameters)
        {
            sb.Replace($"{{{param.Key}}}", param.Value);
        }

        return Task.FromResult(sb.ToString());
    }

    private static string AddIndentation(string code, int indentSize = 4)
    {
        if (string.IsNullOrWhiteSpace(code))
            return string.Empty;

        var indent = new string(' ', indentSize);
        return string.Join("\n", code
            .Split('\n')
            .Select(line => indent + line));
    }

    private static bool IsQuotedBlock(string name) =>
        name.Equals("Text", StringComparison.OrdinalIgnoreCase) ||
        name.Equals("String", StringComparison.OrdinalIgnoreCase);
}