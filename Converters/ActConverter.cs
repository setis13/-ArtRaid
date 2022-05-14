//using ArtRaid.Data;
//using ArtRaid.ViewModels;
//using System;

//namespace ArtRaid.Converters {
//    public static class ActConverter {
//        public static ArtViewModel Convert(Artifact data) {
//            var viewModel = new ArtViewModel();
//            viewModel.id = data.id;
//            viewModel.level = data.level;
//            viewModel.isActivated = data.isActivated;
//            viewModel.kind = (ArtKindEnum)Enum.Parse(typeof(ArtKindEnum), data.kind);
//            viewModel.rank = (ArtRankEnum)Enum.Parse(typeof(ArtRankEnum), data.rank);
//            viewModel.rarity = (ArtRarityEnum)Enum.Parse(typeof(ArtRarityEnum), data.rarity);
//            viewModel.setKind = (ArtSetKindEnum)Enum.Parse(typeof(ArtSetKindEnum), data.setKind);
//            if (data.requiredFraction != null) {
//                viewModel.requiredFraction = (ArtFractionEnum)Enum.Parse(typeof(ArtFractionEnum), data.requiredFraction);
//            } else {
//                viewModel.requiredFraction = ArtFractionEnum.None;
//            }
//            viewModel.primaryBonus = ActConverter.Convert(data.primaryBonus);
//            foreach (var bonus in data.secondaryBonuses) {
//                viewModel.secondaryBonuses.Add(ActConverter.Convert(bonus));
//            }
//            return viewModel;
//        }

//        public static BonusViewModel Convert(Bonus data) {
//            return new BonusViewModel() {
//                kind = (BonusKindEnum)Enum.Parse(typeof(BonusKindEnum), data.kind),
//                isAbsolute = data.isAbsolute,
//                value = data.value,
//                enhancement = data.enhancement,
//                level = data.level
//            };
//        }
//    }
//}
