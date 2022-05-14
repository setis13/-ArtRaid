using ArtRaid.ViewModels;
using RaidExtractor.Core;
using System;
using System.Windows;

namespace ArtRaid.Converters {
    public static class ActConverter {
        public static ArtViewModel Convert(Artifact data) {
            try {
                var viewModel = new ArtViewModel();
                viewModel.id = data.Id;
                viewModel.level = data.Level;
                viewModel.isActivated = data.IsActivated;
                viewModel.kind = (ArtKindEnum)Enum.Parse(typeof(ArtKindEnum), data.Kind);
                viewModel.rank = (ArtRankEnum)Enum.Parse(typeof(ArtRankEnum), data.Rank);
                viewModel.rarity = (ArtRarityEnum)Enum.Parse(typeof(ArtRarityEnum), data.Rarity);
                viewModel.setKind = (ArtSetKindEnum)Enum.Parse(typeof(ArtSetKindEnum), data.SetKind);
                if (data.RequiredFraction != null) {
                    viewModel.requiredFraction = (ArtFractionEnum)Enum.Parse(typeof(ArtFractionEnum), data.RequiredFraction);
                } else {
                    viewModel.requiredFraction = ArtFractionEnum.None;
                }
                viewModel.primaryBonus = ActConverter.Convert(data.PrimaryBonus);
                foreach (var bonus in data.SecondaryBonuses) {
                    viewModel.secondaryBonuses.Add(ActConverter.Convert(bonus));
                }
                return viewModel;

            } catch (Exception e) {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        public static BonusViewModel Convert(ArtifactBonus data) {
            var model = new BonusViewModel() {
                kind = (BonusKindEnum)Enum.Parse(typeof(BonusKindEnum), data.Kind),
                isAbsolute = data.IsAbsolute,
                value = data.Value,
                enhancement = data.Enhancement,
                level = data.Level
            };
            model.maxEnhancement = CalcMaxEnhancement(model);
            return model;
        }

        public static float CalcMaxEnhancement(BonusViewModel bonus) {
            switch (bonus.kind) {
                case BonusKindEnum.Attack:
                    if (bonus.isAbsolute) {
                        return 40;
                    } else {
                        return 0.08f;
                    }
                case BonusKindEnum.Health:
                    if (bonus.isAbsolute) {
                        return 750;
                    } else {
                        return 0.08f;
                    }
                case BonusKindEnum.Defense:
                    if (bonus.isAbsolute) {
                        return 40;
                    } else {
                        return 0.08f;
                    }
                case BonusKindEnum.CriticalChance:
                    return 0;
                case BonusKindEnum.CriticalDamage:
                    return 0;
                case BonusKindEnum.Accuracy:
                    return 16;
                case BonusKindEnum.Speed:
                    return 8;
                case BonusKindEnum.Resistance:
                    return 16;
                default:
                    return 0;
            }
        }
    }
}
