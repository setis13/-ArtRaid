//using System.Collections.Generic;

//namespace ArtRaid.Data {
//    public class Bonus {
//        public string kind { get; set; }
//        public bool isAbsolute { get; set; }
//        public double value { get; set; }
//        public double enhancement { get; set; }
//        public int level { get; set; }
//    }

//    public class Artifact {
//        public int id { get; set; }
//        public int sellPrice { get; set; }
//        public int price { get; set; }
//        public int level { get; set; }
//        public bool isActivated { get; set; }
//        public string kind { get; set; }
//        public string rank { get; set; }
//        public string rarity { get; set; }
//        public string setKind { get; set; }
//        public bool isSeen { get; set; }
//        public int failedUpgrades { get; set; }
//        public Bonus primaryBonus { get; set; }
//        public List<Bonus> secondaryBonuses { get; set; }
//        public string requiredFraction { get; set; }
//    }

//    public class Hero {
//        public int id { get; set; }
//        public int typeId { get; set; }
//        public string grade { get; set; }
//        public int level { get; set; }
//        public int experience { get; set; }
//        public int fullExperience { get; set; }
//        public bool locked { get; set; }
//        public bool inStorage { get; set; }
//        public string marker { get; set; }
//        public string fraction { get; set; }
//        public string rarity { get; set; }
//        public string role { get; set; }
//        public string element { get; set; }
//        public int awakenLevel { get; set; }
//        public string name { get; set; }
//        public double health { get; set; }
//        public double accuracy { get; set; }
//        public double attack { get; set; }
//        public double defense { get; set; }
//        public double criticalChance { get; set; }
//        public double criticalDamage { get; set; }
//        public double criticalHeal { get; set; }
//        public double resistance { get; set; }
//        public double speed { get; set; }
//        public List<int> masteries { get; set; }
//        public List<int> artifacts { get; set; }
//    }

//    public class Root {
//        public List<Artifact> artifacts { get; set; }
//        public List<Hero> heroes { get; set; }
//    }

//}
