using System;
using ClassyHeist.Interfaces;

namespace ClassyHeist.Models
{
    public class LockSpecialist : IRobber
    {
        public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int PercentageCut { get; set; }

        public LockSpecialist(string name, int skillLevel, int percentageCut)
        {
            Name = name;
            SkillLevel = skillLevel;
            PercentageCut = percentageCut;
        }

        public void PerformSkill(Bank bank)
        {
            bank.VaultScore -= SkillLevel;
            Console.WriteLine($"{Name} is cracking the vault.  Decreased security by {SkillLevel} points.");
            if (bank.AlarmScore <= 0)
            {
                Console.WriteLine($"{Name} has opened the vault!");
            }
        }

        public override string ToString()
        {
            return $"{Name}, Lock Specialist - Skill: {SkillLevel}, Cut: {PercentageCut}";
        }
    }
}