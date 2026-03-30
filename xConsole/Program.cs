
namespace xConsole;

public enum FontStyle 
{ 
    Undefined, 
    Regular, 
    Italic 
}

public enum FontCategory
{
    Undefined,
    Sans_Serif,
    Serif,
    Handwriting,
    Display,
    Monospace
}

record FontModel(string Name, string Path, FontStyle Type, FontCategory Category, int Weight);


internal class Program
{
    private static readonly string _directory = @"C:\Repositories\google-fonts";

    private static async Task Main(string[] args)
    {
        string x = "The quick brown fox jumps over the lazy dog";
        string[] dirs = await Task.Run(() => Directory.EnumerateFiles(_directory, "*.pb", SearchOption.AllDirectories).ToArray());
        foreach (string dir in dirs)
        {
            await foreach (FontModel font in GetFonts(dir))
            {
                Console.WriteLine(font);
            }

            Console.ReadKey();
            Console.Clear();
        }
    }

    public const string CategoryTag = @"category:";
    public const string NameTag = @"name:";
    public const string FileTag = @"filename:";
    public const string StyleTag = @"style:";
    public const string WeightTag = @"weight:";

    static async IAsyncEnumerable<FontModel> GetFonts(string pb_path)
    {
        if (Path.Exists(pb_path) && Path.GetDirectoryName(pb_path) is string directory)
        {
            FontCategory category = FontCategory.Undefined;
            string name = string.Empty;
            FontStyle style = FontStyle.Regular;
            string path = string.Empty;
            int weight = -1;

            bool in_font_block = false;
            List<string> styles = [];

            await foreach (string line in File.ReadLinesAsync(pb_path))
            {
                string line_trimmed = line.Trim();
                if (in_font_block == false && line.StartsWith(CategoryTag, StringComparison.CurrentCultureIgnoreCase))
                {
                    category = GetValue(line).ToUpper() switch 
                    {
                        "SANS_SERIF" => FontCategory.Sans_Serif,
                        "SERIF" => FontCategory.Serif,
                        "HANDWRITING" => FontCategory.Handwriting,
                        "DISPLAY" => FontCategory.Display,
                        "MONOSPACE" => FontCategory.Monospace,
                        _ => FontCategory.Undefined
                    };
                }

                if (in_font_block)
                {
                    if (line_trimmed.StartsWith(NameTag, StringComparison.CurrentCultureIgnoreCase))
                    {
                        name = GetValue(line);
                    }
                    else if (line_trimmed.StartsWith(StyleTag, StringComparison.CurrentCultureIgnoreCase))
                    {
                        style = GetValue(line).ToLower() switch 
                        {
                            "normal" => FontStyle.Regular,
                            "italic" => FontStyle.Italic,
                            _ => FontStyle.Undefined,
                        };
                    }
                    else if (line_trimmed.StartsWith(FileTag, StringComparison.CurrentCultureIgnoreCase))
                    {
                        path = Path.Combine(directory, GetValue(line));
                    }
                    else if (line_trimmed.StartsWith(WeightTag, StringComparison.CurrentCultureIgnoreCase))
                    {
                        weight = int.Parse(GetValue(line));
                    }
                }

                if (line_trimmed.StartsWith("fonts {", StringComparison.CurrentCultureIgnoreCase))
                {
                    in_font_block = true;
                }

                if (line_trimmed == "}" && in_font_block)
                {
                    if (string.IsNullOrWhiteSpace(name) is false && 
                        File.Exists(path) && weight > 0)
                    {
                        yield return new(name, path, style, category, weight);
                    }

                    in_font_block = false;
                    name = string.Empty;
                    path = string.Empty;
                    style = FontStyle.Undefined;
                    category = FontCategory.Undefined;
                    weight = -1;
                }
            }
        }

        yield break;

        string GetValue(string entry)
        {
            try
            {
                string[] pair = entry.Split(':');
                return pair[1].Trim().Trim('"');
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}

