using System;
using System.Linq;
using _3._Scripts.Config;
using _3._Scripts.Currency.Enums;
using _3._Scripts.Wallet;
using DG.Tweening;
using GBGamesPlugin;
using UnityEngine;
using VInspector;

namespace _3._Scripts.Environment
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private int unlockPrice;
        [Tab("Components")] [SerializeField] private Transform spawnPoint;
        [Tab("Doors")] [SerializeField] private Collider doorCollider;
        [SerializeField] private Transform rightDoor;
        [SerializeField] private Transform leftDoor;
        [SerializeField] private float doorRotation;

        public bool Opened => GBGames.saves.stageID >= id;
        public bool Unlocked => GBGames.saves.stageID + 1 >= id;
        public int UnlockPrice => (int) Math.Ceiling(unlockPrice * RemoteConfiguration.StagePriceMultiplier);

        public int ID => id;
        public Transform SpawnPoint => spawnPoint;


        private void OnValidate()
        {
            name = $"Stage_{id}";
        }

        public void Initialize()
        {
            SetVisibility(Unlocked);
            SetDoorState(Opened);
        }

        public void SetVisibility(bool state)
        {
            gameObject.SetActive(state);
        }

        public void Open()
        {
            if (!WalletManager.TrySpend(CurrencyType.Third, unlockPrice)) return;

            SetDoorState(true);
            GBGames.saves.stageID += 1;
            WalletManager.SecondCurrency += 1;
            StageController.Instance.UnlockStage(id + 1);
            GBGames.instance.Save();
        }

        private void SetDoorState(bool state)
        {
            if (state)
            {
                doorCollider.gameObject.SetActive(false);
                rightDoor.DOLocalRotate(new Vector3(0, doorRotation, 0), 1f);
                leftDoor.DOLocalRotate(new Vector3(0, -doorRotation, 0), 1f);
            }
            else
            {
                doorCollider.gameObject.SetActive(true);
                rightDoor.DOLocalRotate(Vector3.zero, 1f);
                leftDoor.DOLocalRotate(Vector3.zero, 1f);
            }
        }
    }
}