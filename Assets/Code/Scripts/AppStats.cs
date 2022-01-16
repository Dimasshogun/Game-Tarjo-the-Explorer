using System;
using TMPro;
using UnityEngine;

namespace Code.Scripts
{
    public class AppStats : MonoBehaviour
    {
        private static AppStats _instance;

        public static AppStats Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                _instance = FindObjectOfType<AppStats>();
                if (_instance == null)
                {
                    Debug.LogError("AppStats not found!");
                }

                return _instance;
            }
        }
        
        [SerializeField] private TextMeshProUGUI fpsText;
        [SerializeField] private TextMeshProUGUI versionText;
        [SerializeField] private float hudRefreshRate = 1f;
 
        private float _timer;
        private void Start()
        {
            versionText.text = "App Version: " + Application.version;
            
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

        private void Update()
        {
            if (!(Time.unscaledTime > _timer))
            {
                return;
            }

            var fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = fps + " FPS";
            _timer = Time.unscaledTime + hudRefreshRate;
        }
    }
}