using System.Collections;
using GBGamesPlugin;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _3._Scripts
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;

        private void OnEnable()
        {
            GBGames.SaveLoadedCallback += LoadGame;
        }

        private void OnDisable()
        {
            GBGames.SaveLoadedCallback -= LoadGame;
        }

        private void LoadGame()
        {
            StartCoroutine(InitializeLocalizationAndLoadScene());
        }

        private IEnumerator InitializeLocalizationAndLoadScene()
        {
            yield return LocalizationSettings.InitializationOperation;
            SetLanguage(GBGames.language);
            Resources.UnloadUnusedAssets();
            yield return LoadGameSceneAsync();
        }

        private void SetLanguage(string languageCode)
        {
            var locale = LocalizationSettings.AvailableLocales.Locales.Find(l => l.Identifier.Code == languageCode);
            if (locale != null)
            {
                LocalizationSettings.SelectedLocale = locale;
            }
        }

        private IEnumerator LoadGameSceneAsync()
        {
            var asyncOperation = SceneManager.LoadSceneAsync(1);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                progressBar.value = Mathf.Clamp01(asyncOperation.progress / 0.9f);

                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}