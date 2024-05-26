using _3._Scripts.Singleton;
using GBGamesPlugin;
using UnityEngine;
using UnityEngine.Serialization;
using VInspector;

namespace _3._Scripts.Config
{
    public class RemoteConfiguration : Singleton<RemoteConfiguration>
    {
        [SerializeField] private RemoteConfig<float> shopPriceMultiplier;
        [SerializeField] private RemoteConfig<float> goldRespawn;
        [SerializeField] private RemoteConfig<float> stagePriceMultiplier;
        [SerializeField] private RemoteConfig<float> boostTime;
        [SerializeField] private RemoteConfig<float> stageAdditionalMultiplier;

        public static float ShopPriceMultiplier => Instance.shopPriceMultiplier.Value;
        public static float GoldRespawn => Instance.goldRespawn.Value;
        public static float StagePriceMultiplier => Instance.stagePriceMultiplier.Value;
        public static float BoostTime => Instance.boostTime.Value;
        public static float StageAdditionalMultiplier => Instance.stageAdditionalMultiplier.Value;
    }
}