﻿using System;
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
        [SerializeField] private DamageCounterEffect damageEffect;
        private float _currentGoldAmount;
        public float Multiplier { get; set; }
        public int GoldAmount => (int) Math.Ceiling(goldAmount * Multiplier);

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
            _currentGoldAmount = GoldAmount;
            healthBar.UpdateHealthBar(GoldAmount, _currentGoldAmount);
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

            var damageObj = EffectPanel.Instance.SpawnEffect(damageEffect);
            damageObj.Initialize(damage);

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
                transform.DOScale(scale, 0.25f).SetDelay(RemoteConfiguration.GoldRespawn).SetEase(Ease.OutBack)
                    .OnComplete(SetAmount);
            }).SetEase(Ease.InBack);

            WalletManager.ThirdCurrency += GoldAmount;
            CreateEffect(GoldAmount);
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
            var pets = Configuration.Instance.AllPets.Where(p => GBGames.saves.petSaves.IsCurrent(p.ID)).ToList()
                .Sum(p => p.Booster);
            var strength = WalletManager.FirstCurrency;
            var result = (int) Math.Ceiling(strength + strength * (pets / 100));
            return result * x2;
        }
    }
}