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
            current = Mathf.Clamp(current, 0, current);
            counter.text = ((int) current).ToString();
            slider.DOFillAmount(value, 0.1f);
        }

        public void SetState(bool state)
        {
            if (_state == state) return;
            
            if (state)
            {
                gameObject.SetActive(true);
                _canvasGroup.DOFade(1, 0.15f);
            }
            else
            {
                _canvasGroup.DOFade(0, 0.15f).OnComplete(() => gameObject.SetActive(false));
            }

            _state = state;
        }
    }
}