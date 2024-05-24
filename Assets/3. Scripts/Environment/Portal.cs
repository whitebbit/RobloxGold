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
        [SerializeField] private int price;

        [Tab("Buttons")] [SerializeField] private ObjectButton coinsBuy;
        [SerializeField] private ObjectButton adBuy;
        [Tab("Texts")] [SerializeField] private TMP_Text priceText;


        private void Start()
        {
            priceText.text = WalletManager.ConvertToWallet(price);

            coinsBuy.OnClick += Buy;
            adBuy.OnClick += Ad;
        }

        private void Teleport()
        {
            var stageId = type is PortalType.LastStage ? GBGames.saves.stageID : 0;
            Player.Player.Instance.Teleport(StageController.Instance.GetSpawnPoint(stageId));
        }

        private void Buy()
        {
            if (!WalletManager.TrySpend(CurrencyType.Third, price)) return;

            Teleport();
        }

        private void Ad()
        {
            GBGames.ShowRewarded(Teleport);
        }
    }
}