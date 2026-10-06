using System.Text.Json;

namespace TestTask5_2.Api;

public class JsonSnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return string.Concat(
            name.Select((c, i) =>
                i > 0 && char.IsUpper(c)
                    ? "_" + char.ToLower(c)
                    : char.ToLower(c).ToString()));
    }
}
