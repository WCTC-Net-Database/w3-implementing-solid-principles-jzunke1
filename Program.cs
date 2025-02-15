using System;
using System.IO;

class Program
{
    static string filePath = "input.csv";
    static CharacterReader reader = new CharacterReader(filePath);
    static CharacterWriter writer = new CharacterWriter(filePath);

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Find Character");
            Console.WriteLine("3. Add Character");
            Console.WriteLine("4. Level Up Character");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters();
                    break;
                case "2":
                    FindCharacter();
                    break;
                case "3":
                    AddCharacter();
                    break;
                case "4":
                    LevelUpCharacter();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void DisplayAllCharacters()
    {
        reader.DisplayAllCharacters();
    }

    static void FindCharacter()
    {
        Console.Write("Enter the name of the character to find: ");
        string nameToFind = Console.ReadLine();
        Character foundCharacter = reader.FindCharacterByName(nameToFind);
        if (foundCharacter != null)
        {
            Console.WriteLine(foundCharacter);
        }
        else
        {
            Console.WriteLine("Character not found.");
        }
    }

    static void AddCharacter()
    {
        Console.Write("Enter character name: ");
        string name = Console.ReadLine();

        Console.Write("Enter character class: ");
        string characterClass = Console.ReadLine();

        Console.Write("Enter character level: ");
        if (!int.TryParse(Console.ReadLine(), out int level))
        {
            Console.WriteLine("Invalid level. Please enter a number.");
            return;
        }

        Console.Write("Enter character hit points: ");
        if (!int.TryParse(Console.ReadLine(), out int hitPoints))
        {
            Console.WriteLine("Invalid hit points. Please enter a number.");
            return;
        }

        Console.Write("Enter character equipment (separated by '|'): ");
        string equipment = Console.ReadLine();

        Character newCharacter = new Character(name, characterClass, level, hitPoints, equipment);
        writer.AddCharacter(newCharacter);
    }

    static void LevelUpCharacter()
    {
        Console.Write("Enter the name of the character to level up: ");
        string nameToLevelUp = Console.ReadLine();
        writer.LevelUpCharacter(nameToLevelUp);
    }
}
