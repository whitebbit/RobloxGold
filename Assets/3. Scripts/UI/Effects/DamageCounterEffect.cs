using _3._Scripts.Config;
using _3._Scripts.Currency.Enums;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _3._Scripts.UI.Effects
{
    public class DamageCounterEffect : UIEffect
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private TMP_Text counter;

        public void Initialize(float count)
        {
            var rect = transform as RectTransform;
            counter.text = $"-{count}";

            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, 0.25f).SetLink(gameObject);
            if (rect is null) return;
            
            rect.DOScale(rect.localScale, 0.25f).From();
            rect.DOShakeAnchorPos(0.75f, 50).OnComplete(() =>
            {
                canvasGroup.DOFade(0, 0.25f)
                    .OnComplete(() => Destroy(gameObject));
            }).SetLink(gameObject);
        }
    }
}