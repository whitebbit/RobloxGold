using System;
using System.Collections;
using _3._Scripts.Config;
using _3._Scripts.UI;
using GBGamesPlugin;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace _3._Scripts.Ads
{
    public class InterstitialsTimer : MonoBehaviour
    {
        [SerializeField] private GameObject secondsPanelObject;
        [SerializeField] private LocalizeStringEvent text;
        private int _objSecCounter = 3;

        private void Start()
        {
            if (secondsPanelObject)
                secondsPanelObject.SetActive(false);

            StartCoroutine(CheckTimerAd());
        }

        private void OnEnable()
        {
            GBGames.InterstitialClosedCallback += OnInterstitialClosedCallback;
        }

        private void OnDisable()
        {
            GBGames.InterstitialClosedCallback -= OnInterstitialClosedCallback;
        }

        private void OnInterstitialClosedCallback()
        {
            secondsPanelObject.SetActive(false);
            _objSecCounter = 0;
            UIManager.Instance.Active = false;

            StopAllCoroutines();
            StartCoroutine(CheckTimerAd());
        }

        private IEnumerator CheckTimerAd()
        {
            var process = true;
            yield return new WaitForSeconds(RemoteConfiguration.InterstitialTimer);

            while (process)
            {
                if (!GBGames.NowAdsShow)
                {
                    process = false;
                    _objSecCounter = 3;

                    if (secondsPanelObject)
                        secondsPanelObject.SetActive(true);

                    UIManager.Instance.Active = true;
                    StartCoroutine(TimerAdShow());
                }

                yield return new WaitForSeconds(1);
            }
        }

        private IEnumerator TimerAdShow()
        {
            var process = true;
            while (process)
            {
                if (_objSecCounter > 0)
                {
                    SetText(_objSecCounter);
                    _objSecCounter--;

                    yield return new WaitForSeconds(1.0f);
                }

                if (_objSecCounter > 0) continue;

                process = false;
                StartCoroutine(BackupTimerClosure());
                GBGames.ShowInterstitial();
            }
        }


        private IEnumerator BackupTimerClosure()
        {
            yield return new WaitForSeconds(5f);

            secondsPanelObject.SetActive(false);
            _objSecCounter = 3;
            UIManager.Instance.Active = false;

            StopAllCoroutines();
            StartCoroutine(CheckTimerAd());
        }

        private void SetText(int value)
        {
            var stringReference = text.StringReference;
            if (stringReference["value"] is IntVariable variables) variables.Value = value;

            text.RefreshString();
        }
    }
}