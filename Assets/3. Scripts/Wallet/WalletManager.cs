using System;
using _3._Scripts.Currency.Enums;
using GBGamesPlugin;

namespace _3._Scripts.Wallet
{
    public static class WalletManager
    {
        public static event Action<int, int> OnFirstCurrencyChange;

        public static float FirstCurrency
        {
            get => GBGames.saves.walletSave.firstCurrency;
            set
            {
                OnFirstCurrencyChange?.Invoke((int) FirstCurrency, (int) value);
                GBGames.saves.walletSave.firstCurrency = value;
            }
        }

        public static event Action<int, int> OnSecondCurrencyChange;

        public static float SecondCurrency
        {
            get => GBGames.saves.walletSave.secondCurrency;
            set
            {
                OnSecondCurrencyChange?.Invoke((int) SecondCurrency, (int) value);
                GBGames.saves.walletSave.secondCurrency = value;
            }
        }


        public static event Action<int, int> OnThirdCurrencyChange;

        public static float ThirdCurrency
        {
            get => GBGames.saves.walletSave.thirdCurrency;
            set
            {
                OnThirdCurrencyChange?.Invoke((int) ThirdCurrency, (int) value);
                GBGames.saves.walletSave.thirdCurrency = value;
            }
        }


        private static void SpendByType(CurrencyType currencyType, float count)
        {
            switch (currencyType)
            {
                case CurrencyType.First:
                    FirstCurrency -= count;
                    break;
                case CurrencyType.Second:
                    SecondCurrency -= count;
                    break;
                case CurrencyType.Third:
                    ThirdCurrency -= count;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currencyType), currencyType, null);
            }
        }

        public static bool TrySpend(CurrencyType currencyType, float count)
        {
            var canSpend = currencyType switch
            {
                CurrencyType.First => FirstCurrency >= count,
                CurrencyType.Second => SecondCurrency >= count,
                CurrencyType.Third => ThirdCurrency >= count,
                _ => throw new ArgumentOutOfRangeException(nameof(currencyType), currencyType, null)
            };

            if (canSpend)
                SpendByType(currencyType, count);

            return canSpend;
        }

        public static string ConvertToWallet(long number)
        {
            return number switch
            {
                < 1000 => number.ToString(),
                < 1_000_000 => (number / 1_000D).ToString("0.#") + "k",
                < 1_000_000_000 => (number / 1_000_000D).ToString("0.#") + "M",
                < 1_000_000_000_000 => (number / 1_000_000_000D).ToString("0.#") + "B",
                _ => (number / 1_000_000_000_000D).ToString("0.#") + "T"
            };
        }
    }
}