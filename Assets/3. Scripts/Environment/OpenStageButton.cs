using System;
using _3._Scripts.Wallet;
using TMPro;
using UnityEngine;

namespace _3._Scripts.Environment
{
    public class OpenStageButton : MonoBehaviour
    {
        [SerializeField] private Stage stage;
        [SerializeField] private TMP_Text price;

        private void Start()
        {
            price.text = WalletManager.ConvertToWallet(stage.UnlockPrice);
        }

        private void OnMouseDown()
        {
            stage.Open();
        }
    }
}