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

        private float _damageTimer;
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

        private void Update()
        {
            DamageTimer();
        }

        private void TakeDamage()
        {
            if (_currentGoldAmount <= 0) return;

            var damage = GetDamage();
            if (damage <= 0) return;

            
            CreateEffect(damage);
            UpdateWallet(damage);
            DoShake();
            ResetObject();
            _damageTimer = 3;
        }

        private void DamageTimer()
        {
            switch (_damageTimer)
            {
                case > 0:
                    _damageTimer -= Time.deltaTime;
                    return;
                case <= 0:
                    healthBar.SetState(false);
                    break;
            }
        }
        
        private void UpdateWallet(int damage)
        {
            WalletManager.ThirdCurrency += damage;
            _currentGoldAmount -= damage;
            healthBar.SetState(true);
            healthBar.UpdateHealthBar(goldAmount * Multiplier, _currentGoldAmount);
        }

        private void ResetObject()
        {
            if (!(_currentGoldAmount <= 0)) return;

            var scale = transform.localScale;
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
            {
                transform.DOScale(scale, 0.25f).SetDelay(10).SetEase(Ease.OutBack)
                    .OnComplete(SetAmount);
            }).SetEase(Ease.InBack);
        }

        private void CreateEffect(int damage)
        {
            var obj = EffectPanel.Instance.SpawnEffect(effect);
            obj.Initialize(CurrencyType.Third, damage);
        }

        private void DoShake()
        {
            var position = transform.localPosition;
            transform.DOShakePosition(0.25f, 0.25f, 50).OnComplete(() => transform.localPosition = position);
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