using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.Minigame
{
    public class MinigameManager : MonoBehaviour
    {
        private static MinigameManager _instance;

        public static MinigameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MinigameManager>();
                    if (_instance == null)
                    {
                        Debug.LogError("Minigame Manager Not Found!");
                    }
                }

                return _instance;
            }
        }

        [SerializeField] private GameObject endScreen;
        
        public AudioSource source;
        public AudioClip[] correct;
        public AudioClip incorrect;

        public List<MinigameDropTarget> dropTargets = new List<MinigameDropTarget>();

        public bool completed;
        
        public void EndMinigame()
        {
            completed = true;
        }
        
        private void Start()
        {
            // Set endScreen dengan objek bernama EndScreen kalau belum diset dari Inspector
            if (endScreen == null)
            {
                endScreen = GameObject.Find("EndScreen");
            }

            if (dropTargets.Count == 0)
            {
                dropTargets.AddRange(FindObjectsOfType<MinigameDropTarget>());
            }
            if (dropTargets.Count == 0)
            {
                endScreen.SetActive(true);
                return; 
            }
            endScreen.SetActive(false);
        }

        public void PlayCorrect()
        {
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            CheckComplete();
        }
        
        public void PlayIncorrect()
        {
            source.clip = incorrect;
            source.Play();
        }

        private void CheckComplete()
        {
            if (endScreen.activeSelf)
            {
                return;
            }

            foreach (var target in dropTargets)
            {
                if (!target.isCorrect)
                {
                    return;
                }
            }
            endScreen.SetActive(true);
        }
    }
}