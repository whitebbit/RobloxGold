using System;
using _3._Scripts.Boosters;
using _3._Scripts.Currency.Enums;
using _3._Scripts.Interactive.Interfaces;
using _3._Scripts.UI.Effects;
using _3._Scripts.UI.Panels;
using _3._Scripts.Wallet;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.Interactive
{
    public class GoldInteractive : MonoBehaviour, IInteractive
    {
        [SerializeField] private float goldAmount;
        [SerializeField] private Transform model;
        [SerializeField] private CurrencyCounterEffect effect;
        private float _currentGoldAmount;
        public float Multiplier { get; set; }

        public float GoldAmount => goldAmount;

        private void Start()
        {
            SetAmount();
        }

        public void Interact()
        {
            TakeDamage();
        }

        private void SetAmount() => _currentGoldAmount = goldAmount * Multiplier;

        private void TakeDamage()
        {
            if (_currentGoldAmount <= 0) return;

            var damage = GetDamage();
            if (damage <= 0) return;

            var obj = EffectPanel.Instance.SpawnEffect(effect);
            var position = model.localPosition;

            obj.Initialize(CurrencyType.First, damage);
            WalletManager.ThirdCurrency += damage;
            _currentGoldAmount -= damage;
            model.DOShakePosition(0.25f, 0.25f, 50).OnComplete(() => model.localPosition = position);
            if (_currentGoldAmount <= 0)
            {
                model.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
                {
                    model.DOScale(Vector3.one, 0.25f).SetDelay(10).SetEase(Ease.OutBack)
                        .OnComplete(SetAmount);
                }).SetEase(Ease.InBack);
            }
        }


        private int GetDamage()
        {
            var x2 = BoostersHandler.Instance.X2Currency ? 2 : 1;
            var result = (int) (goldAmount / (goldAmount / WalletManager.FirstCurrency)) * x2;
            return (int) Mathf.Clamp(result, 0, _currentGoldAmount);
        }
    }
}