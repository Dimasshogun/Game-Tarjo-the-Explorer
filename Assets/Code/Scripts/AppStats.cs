using System;
using TMPro;
using UnityEngine;

namespace Code.Scripts
{
    public class AppStats : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fpsText;
        [SerializeField] private TextMeshProUGUI versionText;
        [SerializeField] private float hudRefreshRate = 1f;
 
        private float _timer;
        private void Start()
        {
            
            versionText.text = "App Version: " + Application.version;
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