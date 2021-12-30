using System;
using UnityEngine;

namespace Code.Scripts.Character
{
    [Serializable]
    public class CharacterInstance : MonoBehaviour
    {
        public string characterName;

        public CharacterAnimator characterAnimator;

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
        }
    }
}