using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager _instance;

        public static LevelManager Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                _instance = FindObjectOfType<LevelManager>();
                if (_instance == null)
                {
                    Debug.LogError("Level Manager not found!");
                }

                return _instance;
            }
        }
    
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Image progressBar;

        private float _fillTarget;
        private bool _isLoading;

        [SerializeField] private Levels levels;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            var levelScenesJson = Resources.Load("LevelScenes");
            levels = JsonUtility.FromJson<Levels>(levelScenesJson.ToString());
        }

        private void Update()
        {
            if (_isLoading)
            {
                progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, _fillTarget, 2 * Time.deltaTime);
            }
        }

        public void StartLoadLevel(string level)
        {
            var sceneNamesToLoad = levels.levelSetups.Find(setup => setup.rootScene == level);

            StartCoroutine(LoadLevel(sceneNamesToLoad));
        }

        private IEnumerator LoadLevel(LevelSetup levelSetup)
        {
            _isLoading = true;
            progressBar.fillAmount = 0;
            loadingScreen.SetActive(true);
        
            List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

            scenesToLoad.Add(SceneManager.LoadSceneAsync(levelSetup.rootScene));
        
            for (int i = 0; i < levelSetup.additionalScenes.Length; i++)
            {
                scenesToLoad.Add(SceneManager.LoadSceneAsync(levelSetup.additionalScenes[i], LoadSceneMode.Additive));
            }

            do
            {
                float newTarget = 0;
                foreach (var scenes in scenesToLoad)
                {
                    newTarget += scenes.progress;
                }
                _fillTarget = newTarget / scenesToLoad.Count;

                yield return null;
            } while (_fillTarget < 0.9f);
            
            yield return new WaitUntil(() => progressBar.fillAmount >= 0.9f);

            loadingScreen.SetActive(false);
            _isLoading = false;
        }

        public void StartChangeScene(string scene)
        {
            StartCoroutine(ChangeScene(scene));
        }

        private IEnumerator ChangeScene(string scene)
        {
            _isLoading = true;
            progressBar.fillAmount = 0;
            loadingScreen.SetActive(true);
            
            var sceneToLoad = SceneManager.LoadSceneAsync(scene);
            sceneToLoad.allowSceneActivation = false;
        
            do
            {
                _fillTarget = sceneToLoad.progress;
                yield return null;
                
            } while (_fillTarget < 0.9f);
            
            yield return new WaitUntil(() => progressBar.fillAmount >= 0.9f);

            sceneToLoad.allowSceneActivation = true;
            loadingScreen.SetActive(false);
            _isLoading = false;
        }

        public void ExitGame() {
            Application.Quit();
        }
    }
}
