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
        private CanvasGroup _canvasGroup;
        private bool _state;
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            _state = true;
            SetState(false);
        }

        public void UpdateHealthBar(float max, float current)
        {
            var value = current / max;
            counter.text = ((int)current).ToString();
            slider.DOFillAmount(value, 0.1f);
        }

        public void SetState(bool state)
        {
            if (_state == state) return;
            var value = state ? 1 : 0;
            _canvasGroup.DOFade(value, 0.15f);
            _state = state;
        }
    }
}