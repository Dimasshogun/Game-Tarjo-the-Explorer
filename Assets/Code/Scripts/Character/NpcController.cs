using System.IO;
using Code.Scripts.Quest;
using UnityEngine;
using Yarn.Unity;

namespace Code.Scripts.Character
{
    public class NpcController : MonoBehaviour
    {
        public string characterName = "";
        public string entryNode = "";
        
        public YarnProgram dialogueScript;
        
        public string questFile = "";
        
        public QuestData npcQuest = new QuestData();

        private void Start () {
            if (dialogueScript != null) {
                DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
                dialogueRunner.Add(dialogueScript);                
            }

            if (questFile != "")
            {
                string path = $"Assets/Resources/Quest/{questFile}.json";
                var reader = new StreamReader(path);
                string jsonString = reader.ReadToEnd();
                npcQuest = JsonUtility.FromJson<QuestData>(jsonString);
                FindObjectOfType<QuestManager>().quests.Add(npcQuest);
            }
            
        }
    }
}