using UnityEngine;

namespace Code.Scripts.Character
{
    public class CharacterInstance : MonoBehaviour
    {
        public string characterName;

        private void Start()
        {
            if (characterName != "")
            {
                return;
            }

            characterName = gameObject.name;
        }
    }
}