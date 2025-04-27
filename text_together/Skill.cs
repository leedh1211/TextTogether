using System;
using System.Collections.Generic;
using System.Text;

namespace text_together
{
    public class Skill
{
    public string Name { get; set; }
    public int Cost { get; set; }
    public int Attack { get; set; }
    public int Level { get; set; }
    public string Description { get; set; }

    public Skill(string name, int cost, int attack, int level,string description)
    {
        Name = name;
        Cost = cost;
        Attack = attack;
        Level = level;
        Description = description;
    }

}
}