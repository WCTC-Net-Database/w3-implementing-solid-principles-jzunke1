using System;

public class Character
{
    public string Name { get; set; }
    public string CharacterClass { get; set; }
    public int Level { get; set; }
    public int HitPoints { get; set; }
    public string Equipment { get; set; }

    public Character(string name, string characterClass, int level, int hitPoints, string equipment)
    {
        Name = name;
        CharacterClass = characterClass;
        Level = level;
        HitPoints = hitPoints;
        Equipment = equipment;
    }

    public override string ToString()
    {
        return $"Name: {Name,-2} | Class: {CharacterClass,-2} | Level: {Level,-2} | HP: {HitPoints,-2} | Equipment: {Equipment.Replace("|", ", "),-2}";
    }
}
