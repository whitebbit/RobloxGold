using System;
using System.Linq;
using _3._Scripts.Boosters;
using _3._Scripts.Config;
using _3._Scripts.Currency.Enums;
using _3._Scripts.Interactive.Interfaces;
using _3._Scripts.UI;
using _3._Scripts.UI.Effects;
using _3._Scripts.UI.Panels;
using _3._Scripts.Wallet;
using DG.Tweening;
using GBGamesPlugin;
using UnityEngine;

namespace _3._Scripts.Interactive
{
    public class TrainingInteractive : MonoBehaviour, IInteractive
    {
        [SerializeField] private CurrencyCounterEffect effect;
        

        public void Interact()
        {
            DoIncome();
        }
        
        private void DoIncome()
        {
            var obj = EffectPanel.Instance.SpawnEffect(effect);
            var income = GetIncome();
            var position = transform.localPosition;
            obj.Initialize(CurrencyType.First, income);
            transform.DOShakePosition(0.25f, 0.25f, 50).OnComplete(() => transform.localPosition = position);
            WalletManager.FirstCurrency += GetIncome();
        }

        private float GetIncome()
        {
            //var pets = Configuration.Instance.AllPets.Where(p => GBGames.saves.petSaves.IsCurrent(p.ID)).ToList();
            var character =
                Configuration.Instance.AllCharacters.FirstOrDefault(c => GBGames.saves.characterSaves.IsCurrent(c.ID));
            var booster = BoostersHandler.Instance.X2Income ? 2 : 1;
            return (/*pets.Sum(pet => pet.Booster) */+ character.Booster) * booster;
        }
    }
}