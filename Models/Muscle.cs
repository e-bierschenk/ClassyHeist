using System;
using ClassyHeist.Interfaces;

namespace ClassyHeist.Models
{
    public class Muscle : IRobber
    {
        public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int PercentageCut { get; set; }

        public Muscle(string name, int skillLevel, int percentageCut)
        {
            Name = name;
            SkillLevel = skillLevel;
            PercentageCut = percentageCut;
        }
        public void PerformSkill(Bank bank)
        {
            bank.SecurityGuardScore -= SkillLevel;
            Console.WriteLine($"{Name} is taking out the guards.  Decreased security by {SkillLevel} points.");
            if (bank.AlarmScore <= 0)
            {
                Console.WriteLine($"{Name} has incapacitated all the guards!");
            }
        }

        public override string ToString()
        {
            return $"{Name}, Muscle - Skill: {SkillLevel}, Cut: {PercentageCut}";
        }
    }
}