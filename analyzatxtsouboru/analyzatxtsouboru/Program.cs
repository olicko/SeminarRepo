using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class TextAnalyzer : StreamReader
{
    private readonly Dictionary<string, int> _words;
    private readonly string _content;
    public int WordCount { get; private set; }
    public int CharactersNoSpacesCount { get; private set; }
    public int CharactersCount { get; private set; }

    public TextAnalyzer(string filePath) : base(filePath)
    {
        _words = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        try
        {
            _content = ReadToEnd();
            AnalyzeContent(_content);
        }
        catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
        {
            Console.WriteLine("Input File Error");
        }
    }

    private void AnalyzeContent(string content)
    {
        CharactersCount = content.Length;
        CharactersNoSpacesCount = content.Count(c => !char.IsWhiteSpace(c));
        
        string[] words = content
            .Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(word => word.Trim()).ToArray();

        WordCount = words.Length;

        foreach (string word in words)
        {
            if (_words.ContainsKey(word))
                _words[word]++;
            else
                _words[word] = 1;
        }
    }

    public string GetWordsSeparatedBySingleSpace()
    {
        int x = 0;
        string[] lines = _content.Split(new[] { '\r', '\n' });
        string textSeparatedBySingleSpace = "";
        foreach (string line in lines)
        {
            x++;
            string[] words = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                textSeparatedBySingleSpace += word;
                textSeparatedBySingleSpace += " ";
            }
            if (x == 1)
            {
                textSeparatedBySingleSpace += "\n";
                x-=2;
            }
            
        }

        return textSeparatedBySingleSpace;        
    }

    public void WriteAnalysisToFile(string outputFilePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine($"Počet slov: {WordCount}");
                writer.WriteLine($"Počet znaků (bez mezer): {CharactersNoSpacesCount}");
                writer.WriteLine($"Počet znaků (s mezerami): {CharactersCount}");
                writer.WriteLine("");

                foreach (var kvp in _words.OrderBy(kvp => kvp.Key))
                {
                    writer.WriteLine($"{kvp.Key}: {kvp.Value}");
                }

                writer.WriteLine("");
                writer.WriteLine(GetWordsSeparatedBySingleSpace());
            }
        }
        catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
        {
            Console.WriteLine("Output File Error");
        }
    }
}

class Program
{
    static void Main()
    {
        string inputFilePath = "0_vstup.txt";
        string outputFilePath = "0_vystup.txt";

        try
        {
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"Soubor jménem {inputFilePath} neexistuje.");
                return;
            }

            using (TextAnalyzer analyzer = new TextAnalyzer(inputFilePath))
            {
                analyzer.WriteAnalysisToFile(outputFilePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Nastala neočekávaná chyba: " + ex.Message);
        }
    }
}