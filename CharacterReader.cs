using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class CharacterReader
{
    private string filePath;

    public CharacterReader(string filePath)
    {
        this.filePath = filePath;
    }

    public Character FindCharacterByName(string name)
    {
        try
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1))
            {
                var character = ParseCharacterFromLine(line);
                if (character != null && character.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return character;
                }
            }
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    private string ExtractNameFromLine(string line)
    {
        if (line.StartsWith("\""))
        {
            int endQuoteIndex = line.IndexOf("\",") + 1;
            return line.Substring(1, endQuoteIndex - 2);
        }
        else
        {
            int commaIndex = line.IndexOf(",");
            return line.Substring(0, commaIndex);
        }
    }

    private Character ParseCharacterFromLine(string line)
    {
        try
        {
            string name = ExtractNameFromLine(line);
            string[] fields = line.Substring(name.Length + (line.StartsWith("\"") ? 3 : 1)).Split(',');
            string characterClass = fields[0];
            int level = int.Parse(fields[1]);
            int hitPoints = int.Parse(fields[2]);
            string equipment = fields[3];
            return new Character(name, characterClass, level, hitPoints, equipment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public void DisplayAllCharacters()
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                if (i == 0) continue;
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                string line = lines[i];
                string name;
                int commaIndex;

                if (line.StartsWith("\""))
                {
                    commaIndex = line.IndexOf("\",") + 1;
                    name = line.Substring(1, commaIndex - 2);
                }
                else
                {
                    commaIndex = line.IndexOf(",");
                    name = line.Substring(0, commaIndex);
                }

                string[] fields = line.Substring(commaIndex + 1).Split(',');

                string characterClass = fields[0];
                int level = int.Parse(fields[1]);
                int hitPoints = int.Parse(fields[2]);
                string equipment = fields[3];

                Character character = new Character(name, characterClass, level, hitPoints, equipment);
                Console.WriteLine(character);
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: The file was not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
