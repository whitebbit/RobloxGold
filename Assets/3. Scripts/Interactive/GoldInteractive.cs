using System;
using System.Linq;
using _3._Scripts.Boosters;
using _3._Scripts.Config;
using _3._Scripts.Currency.Enums;
using _3._Scripts.Interactive.Interfaces;
using _3._Scripts.UI.Effects;
using _3._Scripts.UI.Panels;
using _3._Scripts.Wallet;
using DG.Tweening;
using GBGamesPlugin;
using UnityEngine;

namespace _3._Scripts.Interactive
{
    public class GoldInteractive : MonoBehaviour, IInteractive
    {
        [SerializeField] private float goldAmount;
        [SerializeField] private HealthBar healthBar;
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

        private void SetAmount()
        {
             _currentGoldAmount = goldAmount * Multiplier;
             healthBar.UpdateHealthBar(goldAmount * Multiplier, _currentGoldAmount);
        }

        private void TakeDamage()
        {
            if (_currentGoldAmount <= 0) return;

            var damage = GetDamage();
            if (damage <= 0) return;

            var obj = EffectPanel.Instance.SpawnEffect(effect);
            var position = transform.localPosition;

            obj.Initialize(CurrencyType.First, damage);
            WalletManager.ThirdCurrency += damage;
            _currentGoldAmount -= damage;
            healthBar.UpdateHealthBar(goldAmount * Multiplier, _currentGoldAmount);

            transform.DOShakePosition(0.25f, 0.25f, 50).OnComplete(() => transform.localPosition = position);
            if (_currentGoldAmount <= 0)
            {
                transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
                {
                    transform.DOScale(Vector3.one, 0.25f).SetDelay(10).SetEase(Ease.OutBack)
                        .OnComplete(SetAmount);
                }).SetEase(Ease.InBack);
            }
        }


        private int GetDamage()
        {
            var x2 = BoostersHandler.Instance.X2Currency ? 2 : 1;
            var pets = Configuration.Instance.AllPets.Where(p => GBGames.saves.petSaves.IsCurrent(p.ID)).ToList();
            var strength = WalletManager.FirstCurrency + pets.Sum(p => p.Booster);
            var result = (int) (goldAmount / (goldAmount / strength)) * x2;
            return (int) Mathf.Clamp(result, 0, _currentGoldAmount);
        }
    }
}