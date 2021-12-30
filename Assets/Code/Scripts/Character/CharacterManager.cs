using System;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace Code.Scripts.Character
{
    public class CharacterManager : MonoBehaviour
    {
        private static CharacterManager _instance;

        public static CharacterManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CharacterManager>();
                    if (_instance == null)
                    {
                        Debug.LogError("Character Manager Not Found!");
                    }
                }

                return _instance;
            }
        }
        
        
        public List<CharacterInstance> characterInstances = new List<CharacterInstance>();

        private void Start()
        {
            characterInstances.AddRange(FindObjectsOfType<CharacterInstance>());
            
            FindObjectOfType<DialogueRunner>().AddCommandHandler("AnimateCharacter", AnimateCharacter);
        }

        private void AnimateCharacter(string[] parameters)
        {
            var characterName = parameters[0];
            var animationType = parameters[1];

            bool isIdleUpdated = parameters.Length >= 3;

            var character = characterInstances.Find(c => c.characterName == characterName);
            character.characterAnimator.AnimateCharacter(animationType, isIdleUpdated);
        }
    }
}