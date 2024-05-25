using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text counter;
        [SerializeField] private Image slider;

        public void UpdateHealthBar(float max, float current)
        {
            var value = current / max;
            counter.DOCounter(int.Parse(counter.text), (int)current, 0.1f);
            slider.DOFillAmount(value, 0.1f);
        }
        
    }
}