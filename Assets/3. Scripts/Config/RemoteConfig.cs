using _3._Scripts.Singleton;
using GBGamesPlugin;
using UnityEngine;

namespace _3._Scripts.Config
{
    public class RemoteConfig : Singleton<RemoteConfig>
    {
        [SerializeField] private float defaultShopPriceMultiplier = 1;
        [SerializeField] private float defaultGoldRespawn = 15;
        [SerializeField] private float defaultStagePriceMultiplier = 1;
        
        public static float ShopPriceMultiplier =>
            GBGames.GetRemoteValue("skinPriceMultiplier", Instance.defaultShopPriceMultiplier);
        public static float GoldRespawn =>
            GBGames.GetRemoteValue("goldRespawn", Instance.defaultGoldRespawn);
        
        public static float StagePriceMultiplier =>
            GBGames.GetRemoteValue("stagePriceMultiplier", Instance.defaultStagePriceMultiplier);
    }
}