using System;
using UnityEngine;

namespace Code.Scripts.Character
{
    [Serializable]
    public class CharacterInstance : MonoBehaviour
    {
        public string characterName;

        public CharacterAnimator characterAnimator;

        private CharacterManager _characterManager;

        private void Start()
        {
            if (characterName == "")
            {
                characterName = gameObject.name;
            }
            if (characterAnimator == null)
            {
                characterAnimator = GetComponent<CharacterAnimator>();
            }

            _characterManager = FindObjectOfType<CharacterManager>();
            var dupe = _characterManager.characterInstances.Find(c => c.characterName == characterName);
            if (dupe == null)
            {
                _characterManager.characterInstances.Add(this);
            }
        }

        private void OnDestroy()
        {
            _characterManager.characterInstances.Remove(this);
        }
    }
}