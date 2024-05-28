using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Config;
using _3._Scripts.Currency.Enums;
using _3._Scripts.Currency.Scriptable;
using _3._Scripts.Environment;
using _3._Scripts.Wallet;
using GBGamesPlugin;
using UnityEngine;
using UnityEngine.Serialization;
using VInspector;

namespace _3._Scripts.UI.Scriptable.Roulette
{
    [CreateAssetMenu(fileName = "CurrencyRouletteItem", menuName = "Roulette Item/Currency", order = 0)]
    public class CurrencyGiftItem : GiftItem
    {
        [Tab("Base settings")] [SerializeField]
        private CurrencyType type;

        [SerializeField] private int count;

        private int Count =>
            (int) Math.Ceiling(count * StageController.Instance.GetStageMultiplier(GBGames.saves.stageID + 1));

        public override Sprite Icon()
        {
            return Configuration.Instance.GetCurrency(type)?.Icon;
        }

        public override string Title()
        {
            return Count.ToString();
        }

        public override void OnReward()
        {
            switch (type)
            {
                case CurrencyType.First:
                    WalletManager.FirstCurrency += Count;
                    break;
                case CurrencyType.Second:
                    WalletManager.SecondCurrency += Count;
                    break;
                case CurrencyType.Third:
                    WalletManager.ThirdCurrency += Count;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}