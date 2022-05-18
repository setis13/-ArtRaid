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

        private IEnumerable<ArtViewModel> excludeAccessorials => AllArts
            .Where(e => e.kind == ArtKindEnum.Weapon || e.kind == ArtKindEnum.Helmet || e.kind == ArtKindEnum.Shield ||
                        e.kind == ArtKindEnum.Gloves || e.kind == ArtKindEnum.Chest || e.kind == ArtKindEnum.Boots);

        public int Total10 => AllArts.Any() ? (excludeAccessorials.Count(e => e.level == 10)) : 0;
        public int Total10Percent => AllArts.Any() ? (100 * excludeAccessorials.Count(e => e.level == 10) / excludeAccessorials.Count(e => e.level >= 10)) : 0;
        public int Total11 => AllArts.Any() ? (excludeAccessorials.Count(e => e.level == 11)) : 0;
        public int Total11Percent => AllArts.Any() ? (100 * excludeAccessorials.Count(e => e.level == 11) / excludeAccessorials.Count(e => e.level >= 10)) : 0;
        public int Total10_11 => AllArts.Any() ? (excludeAccessorials.Count(e => e.level == 10 || e.level == 11)) : 0;
        public int Total10_11Percent => AllArts.Any() ? (100 * excludeAccessorials.Count(e => e.level == 10 || e.level == 11) / excludeAccessorials.Count(e => e.level >= 10)) : 0;
        public int Total => excludeAccessorials.Count(e => e.level >= 10);

        public bool Filter10_12 {
            get => filter10_12; set {
                filter10_12 = value;
                OnPropertyChanged();
                UpdateAtrs();
            }
        }
        public bool Filter13 {
            get => filter13; set {
                filter13 = value;
                OnPropertyChanged();
                UpdateAtrs();
            }
        }
        public bool Filter14 {
            get => filter14; set {
                filter14 = value;
                OnPropertyChanged();
                UpdateAtrs();
            }
        }
        public bool Filter15 {
            get => filter15; set {
                filter15 = value;
                OnPropertyChanged();
                UpdateAtrs();
            }
        }
        public bool Filter16 {
            get => filter16; set {
                filter16 = value;
                OnPropertyChanged();
                UpdateAtrs();
            }
        }
        public bool FilterNoEquiped {
            get => filterNoEquiped; set {
                filterNoEquiped = value;
                OnPropertyChanged();
                UpdateAtrs();
            }
        }
        public bool FilterEquiped {
            get => filterEquiped; set {
                filterEquiped = value;
                OnPropertyChanged();
                UpdateAtrs();
            }
        }

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
        private bool filter10_12;
        private bool filter13;
        private bool filter14;
        private bool filter15;
        private bool filter16;
        private bool filterNoEquiped;
        private bool filterEquiped;

        public void UpdateAtrs() {
            var arts = new List<ArtWrapper>();
            foreach (var art in AllArts
                  //.Where(e => e.isActivated == false)
                  .OrderBy(e => e.setKind).ThenBy(e => e.kind).ThenByDescending(e => e.rank).ThenByDescending(e => e.level)) {
                var key = (art.setKind == ArtSetKindEnum.ChangeHitType || art.setKind == ArtSetKindEnum.ShieldAccessory ? ArtSetKindEnum.None : art.setKind).ToString() + "_" + art.requiredFraction.ToString();
                ArtWrapper item = arts.FirstOrDefault(e => e.Key == key);
                if (item == default) {
                    item = new ArtWrapper(key);
                    arts.Add(item);
                }
                if (filterNoEquiped && art.isActivated == true) {
                    continue;
                }
                if (filterEquiped && art.isActivated == false) {
                    continue;
                }
                if ((filter10_12 && (art.level == 10 || art.level == 11 || art.level == 12)) ||
                    (filter13 && art.level == 13) ||
                    (filter14 && art.level == 14) ||
                    (filter15 && art.level == 15) ||
                    (filter16 && art.level == 16) ||
                    (!filter10_12 && !filter13 && !filter14 && !filter15 && !filter16)) {
                    item.Value.Add(art);
                }
            }

            foreach (var art in arts.ToList()) {
                if (art.Value.Count == 0) {
                    arts.Remove(art);
                }
            }

            Arts.Clear();
            foreach (ArtWrapper art in arts) {
                Arts.Add(art);
            }

            this.OnPropertyChanged(nameof(Total10));
            this.OnPropertyChanged(nameof(Total10Percent));
            this.OnPropertyChanged(nameof(Total11));
            this.OnPropertyChanged(nameof(Total11Percent));
            this.OnPropertyChanged(nameof(Total10_11));
            this.OnPropertyChanged(nameof(Total10_11Percent));
            this.OnPropertyChanged(nameof(Total));
        }

        public ObservableCollection<HeroViewModel> Heroes { get; set; } = new ObservableCollection<HeroViewModel>();
        public List<ArtViewModel> AllArts { get; set; } = new List<ArtViewModel>();
        public ObservableCollection<ArtWrapper> Arts { get; set; } = new ObservableCollection<ArtWrapper>();
        public List<ArtViewModel> Favorits => AllArts.Where(e => e.Hero != null).OrderByDescending(e => e.Order).ThenByDescending(e => e.level).ToList();

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
        private bool visible = true;

        public int id { get; set; }
        public int level { get; set; }
        public bool isActivated { get; set; }
        public ArtKindEnum kind { get; set; }
        public ArtRankEnum rank { get; set; }
        public ArtRarityEnum rarity { get; set; }
        public ArtSetKindEnum setKind { get; set; }
        public ArtFractionEnum requiredFraction { get; set; }

        public float percentEnhancement => secondaryBonuses.Where(e=>e.maxEnhancement != 0).Select(e => e.percentEnhancement).DefaultIfEmpty(0).Average(e => e);

        public short Order { get; set; }
        public string Comment { get; set; }

        public bool Visible {
            get => visible; set {
                visible = value;
                OnPropertyChanged();
            }
        }
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
            return $"Kind:{kind}\nSetKind:{setKind}\nFraction{requiredFraction}\n{id}";
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
        [Description("Shadowkin")]
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
        Cloak,
        Banner,
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
