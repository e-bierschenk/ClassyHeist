using System;
using System.Collections.Generic;
using System.Linq;
using ClassyHeist.Interfaces;
using ClassyHeist.Models;

namespace ClassyHeist
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IRobber> rolodex = new List<IRobber>
            {
                new Hacker("ZeroCool", 35, 10),
                new Hacker("Crash0verride", 50, 15),
                new Muscle("Mr Pink", 23, 15),
                new Muscle("Dwayne", 35, 75),
                new LockSpecialist("Cat", 20, 5),
                new LockSpecialist("The Locksmith", 50, 25)
            };

            //add crony loop
            while (true)
            {
                Console.WriteLine($"There are {rolodex.Count} cronies in your rolodex");
                Console.Write("Please enter the name of a new crony.  Enter a blank name to start theiving.");
                string name = Console.ReadLine();

                //breaks out of crony loop here if the user inputs a blank name
                if (name == "")
                {
                    break;
                }
                Console.WriteLine("Please select a specialty for your new crony.");
                Console.WriteLine("1. Hacker (disables alarms)");
                Console.WriteLine("2. Muscle (disarms guards)");
                Console.WriteLine("3. Lock Specialist (cracks vaults)");
                Console.Write(">");
                int specialty = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter their skill level as an integer between 1 - 100");
                int skill = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter their percentage take as an integer between 1 - 100");
                int percentTake = int.Parse(Console.ReadLine());

                switch (specialty)
                {
                    case 1:
                        rolodex.Add(new Hacker(name, skill, percentTake));
                        break;
                    case 2:
                        rolodex.Add(new Muscle(name, skill, percentTake));
                        break;
                    case 3:
                        rolodex.Add(new LockSpecialist(name, skill, percentTake));
                        break;
                    default:
                        break;
                }
            }
            // start heisting
            Bank bank = new Bank
            {
                AlarmScore = new Random().Next(101),
                VaultScore = new Random().Next(101),
                SecurityGuardScore = new Random().Next(101),
                CashOnHand = new Random().Next(50000, 1000001)
            };

            Console.WriteLine("------------------");
            Console.WriteLine("   RECON REPORT   ");
            Console.WriteLine("------------------");
            Console.WriteLine($"Most Secure: {bank.MaxSystem}");
            Console.WriteLine($"Least Secure: {bank.MinSystem}");
            Console.WriteLine("");

            var crew = new List<IRobber>();
            while (true)
            {
                Console.WriteLine("------------------");
                Console.WriteLine("    CREW REPORT   ");
                Console.WriteLine("------------------");
                int index = 0;
                foreach (var crony in rolodex)
                {
                    // only display cronies which we have not yet added to the crew
                    // OR
                    // cronies which will not increase the percentage cut to > 100 
                    if (!crew.Contains(crony) && crew.Select(member => member.PercentageCut).Sum() + crony.PercentageCut <= 100)
                        Console.WriteLine($"{index}. {crony}");
                    index++;
                }
                Console.WriteLine("");
                Console.WriteLine("Who would you like to add to your crew? Press return to exit.");
                Console.Write(">");
                string choice = Console.ReadLine();
                if (choice == "")
                {
                    break;
                }
                crew.Add(rolodex[int.Parse(choice)]);
            }

            // HEIST CHECK
            foreach (var crony in crew)
            {
                switch (crony.GetType().ToString())
                {
                    case "ClassyHeist.Models.Hacker":
                        bank.AlarmScore -= crony.SkillLevel;
                        break;
                    case "ClassyHeist.Models.LockSpecialist":
                        bank.VaultScore -= crony.SkillLevel;
                        break;
                    case "ClassyHeist.Models.Muscle":
                        bank.SecurityGuardScore -= crony.SkillLevel;
                        break;
                }
            }
            if(!bank.IsSecure)
            {
                Console.WriteLine("This...crew...is...good. The heist was a success!");
                Console.WriteLine("");
                Console.WriteLine("------------------");
                Console.WriteLine("     JOB REPORT   ");
                Console.WriteLine("------------------");
                foreach (var crony in crew)
                {
                    var take = bank.CashOnHand * crony.PercentageCut / 100;
                    Console.WriteLine($"Name: {crony.Name} - Take: {take}");
                    bank.CashOnHand -= take;
                }
                Console.WriteLine("");
                Console.WriteLine($"Your take: {bank.CashOnHand}");
            }
            else
            {
                Console.WriteLine("This crew is not good. They died.");
            }
        }
    }
}
