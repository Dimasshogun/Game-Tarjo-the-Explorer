using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Scripts.Level
{
    public class ScenePartLoader : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float loadRange = 20.0f;
        
        private bool _isLoaded;

        private void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
            var boxColliderSize = GetComponent<BoxCollider>().size;
            if (boxColliderSize.x >= boxColliderSize.z)
            {
                loadRange = boxColliderSize.x + loadRange;
            }
            else
            {
                loadRange = boxColliderSize.z + loadRange;
            }
            if (SceneManager.sceneCount > 0)
            {
                for (int i = 0; i < SceneManager.sceneCount; ++i)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.name == gameObject.name)
                    {
                        _isLoaded = true;
                    }
                }
            }
        }

        private void Update()
        {
            DistanceCheck();
        }

        private void DistanceCheck()
        {
            if (Vector3.Distance(player.position, transform.position) < loadRange)
            {
                LoadScene();
            }
            else
            {
                UnloadScene();
            }
        }

        private void LoadScene()
        {
            if (!_isLoaded)
            {
                SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
                _isLoaded = true;
            }
        }

        private void UnloadScene()
        {
            if (_isLoaded)
            {
                SceneManager.UnloadSceneAsync(gameObject.name);
                _isLoaded = false;
            }
        }
        
        
    }
}