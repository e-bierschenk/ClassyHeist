using System;

namespace ClassyHeist.Models
{
    public class Bank
    {
        public int CashOnHand { get; set; }
        public int AlarmScore { get; set; }
        public int VaultScore { get; set; }
        public int SecurityGuardScore { get; set; }
        public bool IsSecure
        {
            get
            {
                return AlarmScore <= 0 && VaultScore <= 0 && SecurityGuardScore <= 0 ? false : true;
            }
        }

        public string MaxSystem
        {
            get
            {
                if (AlarmScore >= VaultScore && AlarmScore >= SecurityGuardScore)
                {
                    return "Alarm";
                }
                else if (VaultScore >= SecurityGuardScore)
                {
                    return "Vault";
                }
                else
                {
                    return "Guard";
                }
            }

        }
        public string MinSystem
        {
            get
            {
                if (AlarmScore <= VaultScore && AlarmScore <= SecurityGuardScore)
                {
                    return "Alarm";
                }
                else if (VaultScore <= SecurityGuardScore)
                {
                    return "Vault";
                }
                else
                {
                    return "Guard";
                }
            }
        }

    }
}