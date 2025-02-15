using System;
using System.IO;
using System.Linq;

class CharacterWriter
{
    private string filePath;

    public CharacterWriter(string filePath)
    {
        this.filePath = filePath;
    }

    private string EscapeNameForCSV(string name)
    {
        if (name.Contains(",") || name.Contains("\""))
        {
            return $"\"{name.Replace("\"", "\"\"")}\"";
        }
        return name;
    }

    public void AddCharacter(Character character)
    {
        try
        {
            string escapedName = EscapeNameForCSV(character.Name);
            string newCharacter = $"{escapedName},{character.CharacterClass},{character.Level},{character.HitPoints},{character.Equipment}";

            File.AppendAllText(filePath, Environment.NewLine + newCharacter);
            Console.WriteLine("Character added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while adding the character: {ex.Message}");
        }
    }

    public void LevelUpCharacter(string nameToLevelUp)
    {
        try
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            bool characterFound = false;

            for (int i = 1; i < lines.Count; i++)
            {
                if (i == 0) continue;

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

                if (name.Equals(nameToLevelUp, StringComparison.OrdinalIgnoreCase))
                {
                    string[] fields = line.Substring(commaIndex + 1).Split(',');

                    int level = int.Parse(fields[1]);
                    level++;
                    fields[1] = level.ToString();

                    if (line.StartsWith("\""))
                    {
                        lines[i] = $"\"{name}\",{string.Join(",", fields)}";
                    }
                    else
                    {
                        lines[i] = $"{name},{string.Join(",", fields)}";
                    }

                    Console.WriteLine($"Character {name} leveled up to level {level}!");

                    File.WriteAllLines(filePath, lines.ToArray());
                    characterFound = true;
                    break;
                }
            }
            if (!characterFound)
            {
                Console.WriteLine($"Character {nameToLevelUp} not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while adding the character: {ex.Message}");
        }
    }
}
