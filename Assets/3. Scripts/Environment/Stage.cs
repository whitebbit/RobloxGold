using System;
using _3._Scripts.Currency.Enums;
using _3._Scripts.Wallet;
using GBGamesPlugin;
using UnityEngine;
using VInspector;

namespace _3._Scripts.Environment
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private Transform spawnPoint;
        [Tab("Unlock")] [SerializeField] private int unlockPrice;
        [SerializeField] private Transform lockDoor;

        public int UnlockPrice => unlockPrice;

        public int ID => id;
        public Transform SpawnPoint => spawnPoint;

        private void Awake()
        {
            name = $"Stage_{id}";
        }

        private void Start()
        {
            if (GBGames.saves.stageID <= id) return;
            lockDoor.gameObject.SetActive(false);
        }

        public void Open()
        {
            if (!WalletManager.TrySpend(CurrencyType.Third, unlockPrice)) return;

            lockDoor.gameObject.SetActive(false);

            GBGames.saves.stageID += 1;
            WalletManager.SecondCurrency += 1;
            GBGames.instance.Save();
        }
    }
}