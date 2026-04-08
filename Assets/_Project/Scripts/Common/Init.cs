using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace Ryadev
{
    internal class Init : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Image progressBar;
        [SerializeField] private TMP_Text progressText;

        [Header("Loading Settings")]
        [SerializeField] private string sceneToLoad = "GameScene";
        [SerializeField] private float _minLoadTime = 3f;
        [SerializeField] private bool loadOnStart = true;

        private AsyncOperation asyncOperation;
        private bool isLoading = false;

        private void Start()
        {
            if (loadOnStart)
                StartLoadingSceneWithMinTime(sceneToLoad, _minLoadTime);
        }

        public void StartLoadingScene(string sceneName)
        {
            if (!isLoading)
            {
                StartCoroutine(LoadSceneAsync(sceneName));
            }
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            isLoading = true;

            if (progressBar != null)
                progressBar.fillAmount = 0f;

            if (progressText != null)
                progressText.text = "0.00%";

            asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            float loadStartTime = Time.time;
            float minimumLoadTime = 1f;

            while (!asyncOperation.isDone)
            {
                float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

                if (progressBar != null)
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, progress, Time.deltaTime * 5f);

                if (progressText != null)
                {
                    float percentage = progress * 100f;
                    progressText.text = percentage.ToString("F2") + "%";
                }

                if (asyncOperation.progress >= 0.9f && Time.time - loadStartTime >= minimumLoadTime)
                {
                    if (progressBar != null)
                        progressBar.fillAmount = 1f;

                    if (progressText != null)
                        progressText.text = "100.00%";

                    yield return new WaitForSeconds(0.2f);
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }

            isLoading = false;
        }

        public void StartLoadingSceneWithMinTime(string sceneName, float minLoadTime)
        {
            if (!isLoading)
            {
                StartCoroutine(LoadSceneAsyncWithMinTime(sceneName, minLoadTime));
            }
        }

        private IEnumerator LoadSceneAsyncWithMinTime(string sceneName, float minLoadTime)
        {
            isLoading = true;

            if (progressBar != null)
                progressBar.fillAmount = 0f;

            if (progressText != null)
                progressText.text = "0.00%";

            asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            float loadStartTime = Time.time;

            while (!asyncOperation.isDone)
            {
                float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

                if (progressBar != null)
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, progress, Time.deltaTime * 5f);

                if (progressText != null)
                {
                    float percentage = progress * 100f;
                    progressText.text = percentage.ToString("F2") + "%";
                }

                if (asyncOperation.progress >= 0.9f && Time.time - loadStartTime >= minLoadTime)
                {
                    if (progressBar != null)
                        progressBar.fillAmount = 1f;

                    if (progressText != null)
                        progressText.text = "100.00%";

                    yield return new WaitForSeconds(0.2f);
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }

            isLoading = false;
        }

        public float GetCurrentProgress()
        {
            if (asyncOperation != null && !asyncOperation.isDone)
            {
                return Mathf.Clamp01(asyncOperation.progress / 0.9f);
            }
            return 0f;
        }

        public bool IsLoading()
        {
            return isLoading;
        }
    }
}