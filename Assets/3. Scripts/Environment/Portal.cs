using System;
using _3._Scripts.Currency.Enums;
using _3._Scripts.Environment.Enums;
using _3._Scripts.Wallet;
using GBGamesPlugin;
using TMPro;
using UnityEngine;
using VInspector;

namespace _3._Scripts.Environment
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private PortalType type;

        [Tab("Buttons")] [SerializeField] private ObjectButton coinsBuy;
        [SerializeField] private ObjectButton adBuy;
        [Tab("Texts")] [SerializeField] private TMP_Text priceText;

        private static int Price => (int)Math.Round(WalletManager.ThirdCurrency * 0.25f, MidpointRounding.AwayFromZero);

        private void Start()
        {
            priceText.text = WalletManager.ConvertToWallet(Price);

            coinsBuy.OnClick += Buy;
            adBuy.OnClick += Ad;
        }

        private void Teleport()
        {
            var stageId = type is PortalType.LastStage ? GBGames.saves.stageID + 1 : 0;
            Player.Player.Instance.Teleport(StageController.Instance.GetSpawnPoint(stageId));
        }

        private void Buy()
        {
            if (!WalletManager.TrySpend(CurrencyType.Third, Price)) return;

            Teleport();
        }

        private void Ad()
        {
            GBGames.ShowRewarded(Teleport);
        }

        private void OnEnable()
        {
            WalletManager.OnThirdCurrencyChange += (_, _) =>
            {
                priceText.text = WalletManager.ConvertToWallet(Price);
            };
        }
    }
}