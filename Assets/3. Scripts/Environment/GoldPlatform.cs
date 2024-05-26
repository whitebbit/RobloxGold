using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.Interactive;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.Serialization;

namespace _3._Scripts.Environment
{
    public class GoldPlatform : MonoBehaviour
    {
        [SerializeField] private float multiplier;
        [SerializeField] private LocalizeStringEvent recommendation;

        private List<GoldInteractive> _goldInteractive = new();

        private void Awake()
        {
            _goldInteractive = GetComponentsInChildren<GoldInteractive>().ToList();
            foreach (var item in _goldInteractive)
            {
                item.Multiplier = multiplier;
            }
        }

        private void Start()
        {
            var req = _goldInteractive.Sum(g => g.GoldAmount) / _goldInteractive.Count * 0.5f;
            var stringReference = recommendation.StringReference;
            if (stringReference["amount"] is IntVariable variables) variables.Value = (int) req;
            recommendation.RefreshString();
        }
    }
}