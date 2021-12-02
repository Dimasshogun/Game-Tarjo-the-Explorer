using System;
using System.Collections;
using Code.Scripts.Character;
using UnityEngine;
using Yarn.Unity;

namespace Code.Scripts
{
    public class CutsceneManager : MonoBehaviour
    {
        private void Start()
        {
            FindObjectOfType<DialogueRunner>().AddCommandHandler("switchCamera", SwitchCutsceneCam);
            FindObjectOfType<DialogueRunner>().AddCommandHandler("switchToPlayerCamera", SwitchToPlayerCamera);
            FindObjectOfType<DialogueRunner>().AddCommandHandler("moveCharacter", StartMoveCharacter);
        }

        public void SwitchCutsceneCam(string[] parameters)
        {
            var cameraName = parameters[0];
            var cutsceneCamera = CameraManager.Instance.cameraInstances.Find(cam => cam.cameraName == cameraName);
            
            CameraManager.Instance.SwitchVirtualCam(cutsceneCamera);
        }
        
        public void SwitchToPlayerCamera(string[] parameters)
        {
            CameraManager.Instance.SwitchToPlayerCam();
        }

        public void StartMoveCharacter(string[] parameters, System.Action onComplete)
        {
            var characterName = parameters[0];
            var targetName = parameters[1];

            var character = GameObject.Find(characterName);
            var targetTransform = GameObject.Find(targetName).GetComponent<Transform>();

            StartCoroutine(MoveCharacter(character, targetTransform, onComplete));
        }

        public IEnumerator MoveCharacter(GameObject character, Transform targetTransform, System.Action onComplete)
        {
            Debug.Log($"move {character.name} to {targetTransform.position}");
            while (Vector3.Distance(character.transform.position, targetTransform.position) > 1)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, targetTransform.position, 0.5f);
                yield return null;
            }
            onComplete();
        }
    }
}