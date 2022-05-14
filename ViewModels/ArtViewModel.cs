using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ArtRaid.ViewModels {

    public abstract class BaseViewModel : INotifyPropertyChanged {
        /// <summary>
        ///     On property changed handler </summary>
        /// <param name="propertyName">Changed property name</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            // Raise event
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Occurs when property has been changed </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ArtWrapper {
        public ArtWrapper(string key) {
            Key = key;
        }
        public string Key { get; }
        public List<ArtViewModel> Value { get; set; } = new List<ArtViewModel>();
    }

    public class RootViewModel : BaseViewModel {

        public string ErrorMessage {
            get => errorMessage;
            set {
                errorMessage = value;
                OnPropertyChanged();
            }
        }
        public bool Loading {
            get => loading;
            set {
                loading = value;
                OnPropertyChanged();
            }
        }

        private ArtViewModel selection;
        private string errorMessage;
        private bool loading;

        public ObservableCollection<HeroViewModel> Heroes { get; set; } = new ObservableCollection<HeroViewModel>();

        public ObservableCollection<ArtWrapper> Arts { get; set; } = new ObservableCollection<ArtWrapper>();
        public List<ArtViewModel> Favorits => Arts.SelectMany(e => e.Value).Where(e => e.Hero != null).ToList();

        public void UpdateFavorits() {
            OnPropertyChanged(nameof(Favorits));
        }

        public ArtViewModel Selection {
            get => selection;
            set {
                selection = value;
                SelectionChanging = true;
                OnPropertyChanged();
                SelectionChanging = false;
            }
        }
        public bool SelectionChanging { get; private set; }
    }

    public class HeroViewModel {
        public int Id { get; set; }
        public BitmapImage Icon { get; set; }
    }

    public class ArtViewModel : BaseViewModel {
        private HeroViewModel hero;

        public int id { get; set; }
        public int level { get; set; }
        public bool isActivated { get; set; }
        public ArtKindEnum kind { get; set; }
        public ArtRankEnum rank { get; set; }
        public ArtRarityEnum rarity { get; set; }
        public ArtSetKindEnum setKind { get; set; }
        public ArtFractionEnum requiredFraction { get; set; }

        public bool Visible { get; set; } = true;

        public BonusViewModel primaryBonus { get; set; } = new BonusViewModel();
        public List<BonusViewModel> secondaryBonuses { get; set; } = new List<BonusViewModel>();

        public HeroViewModel Hero {
            get => hero;
            set {
                hero = value;
                OnPropertyChanged();
            }
        }

        public override string ToString() {
            return $"Kind:{kind}\nSetKind:{setKind}\nFraction{requiredFraction}";
        }
    }

    public class BonusViewModel {
        public BonusKindEnum kind { get; set; }
        public bool isAbsolute { get; set; }
        public float value { get; set; }
        public float enhancement { get; set; }
        public int level { get; set; }
        public float maxEnhancement { get; set; }
        public float percentEnhancement => enhancement / maxEnhancement;
    }

    public enum BonusKindEnum {
        [Description("Атк")]
        Attack,
        [Description("Здр")]
        Health,
        [Description("Зщт")]
        Defense,
        [Description("Крит.ш")]
        CriticalChance,
        [Description("Крит.ур")]
        CriticalDamage,
        [Description("Метк")]
        Accuracy,
        [Description("Скр")]
        Speed,
        [Description("Сопр")]
        Resistance,
    }

    public enum ArtFractionEnum {
        None,
        Skinwalkers,
        DarkElves,
        HighElves,
        Dwarves,
        UndeadHordes,
        KnightsRevenant,
        Barbarians,
        SacredOrder,
        Orcs,
        LizardMen,
        OgrynTribes,
        BannerLords,
        Samurai,
        Demonspawn,
    }

    public enum ArtKindEnum {
        None,
        Weapon,
        Helmet,
        Shield,
        Gloves,
        Chest,
        Boots,
        Ring,
        Amulet,
        Banner,
        Cloak,
    }

    public enum ArtRankEnum {
        None,
        One,
        Two,
        Three,
        Four,
        Five,
        Six
    }

    public enum ArtRarityEnum {
        None,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
    }

    public enum ArtSetKindEnum {
        [Description("Life")]
        Hp,
        [Description("Offense")]
        AttackPower,
        Defense,
        [Description("Speed")]
        AttackSpeed,
        [Description("Critical_Rate")]
        CriticalChance,
        [Description("Berserker")]
        CriticalDamage,
        Accuracy,
        Resistance,
        [Description("Lifesteal")]
        LifeDrain,
        [Description("Fury")]
        DamageIncreaseOnHpDecrease,
        [Description("Daze")]
        SleepChance,
        [Description("Cursed")]
        BlockHealChance,
        [Description("Frost")]
        FreezeRateOnDamageReceived,
        [Description("Healing")] // Регенерация
        Heal,
        [Description("Immunity")]// Иммунитет
        BlockDebuff,
        Shield,
        [Description("Relentless")]
        GetExtraTurn,
        [Description("Savage")]
        IgnoreDefense,
        [Description("Destroy")] // Гибель
        DecreaseMaxHp,
        [Description("Stun")] // Оглушение
        StunChance,
        [Description("Toxic")]  // Отрава
        DotRate,
        [Description("Taunting")] // Насмешка
        ProvokeChance,
        [Description("Retaliation")]// Месть
        Counterattack,
        [Description("Avenging")]// Возмездие
        CounterattackOnCrit,
        [Description("Frenzy")]
        Stamina, // Бешенство
        [Description("Stalwart")] // Доблесть
        AoeDamageDecrease,
        [Description("Reflex")] // Реакция
        CooldownReductionChance,
        [Description("Curing")]
        CriticalHealMultiplier,
        [Description("Warrior")] // Беспощадность
        AttackPowerAndIgnoreDefense,
        [Description("Immortal")]
        HpAndHeal, // Бессмертие
        [Description("ShieldAndAttackPower")] // Небесная Атака
        ShieldAndAttackPower,
        [Description("ShieldAndCriticalChance")] // Небесный крит
        ShieldAndCriticalChance,
        [Description("ShieldAndHp")] // Небесная жизнь
        ShieldAndHp,
        [Description("ShieldAndSpeed")] // Небесная скорость
        ShieldAndSpeed,
        UnkillableAndSpdAndCrDmg, // Парирование
        BlockReflectDebuffAndHpAndDef, // Отражение
        [Description("Resilience")] // Живучесть
        HpAndDefence,
        [Description("Perception")] // Расторопность
        AccuracyAndSpeed,

        [Description("Fatal")]
        AttackAndCritRate,



        CritDmgAndTransformWeekIntoCritHit,
        PassiveShareDamageAndHeal,
        //[Description("Relentless")]
        ChangeHitType,

        ResistanceAndBlockDebuff,
        FreezeResistAndRate,
        ShieldAccessory,
        CritRateAndLifeDrain,
        None,
    }
}
