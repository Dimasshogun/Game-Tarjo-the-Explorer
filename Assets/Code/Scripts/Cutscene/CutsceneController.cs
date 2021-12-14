using System.Collections;
using Cinemachine;
using UnityEngine;
using Yarn.Unity;

namespace Code.Scripts.Cutscene
{
    public class CutsceneController : MonoBehaviour
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
            var blendStyle = parameters[1];

            CinemachineBlendDefinition.Style targetBlendStyle;

            switch (blendStyle)
            {
                case "Cut":
                    targetBlendStyle = CinemachineBlendDefinition.Style.Cut;
                    break;
                case "EaseIn":
                    targetBlendStyle = CinemachineBlendDefinition.Style.EaseIn;
                    break;
                default:
                    targetBlendStyle = CinemachineBlendDefinition.Style.EaseIn;
                    break;
            }
            
            var cutsceneCamera = CameraManager.Instance.cameraInstances.Find(cam => cam.cameraName == cameraName);
            
            StartCoroutine(CameraManager.Instance.SwitchVirtualCam(cutsceneCamera, targetBlendStyle));
        }
        
        public void SwitchToPlayerCamera(string[] parameters)
        {
            CameraManager.Instance.SwitchToPlayerCam();
        }

        public void StartMoveCharacter(string[] parameters, System.Action onComplete)
        {
            var characterName = parameters[0];
            var targetName = parameters[1];
            var moveOption = parameters[2];

            var character = GameObject.Find(characterName);
            var targetTransform = GameObject.Find(targetName).GetComponent<Transform>();

            StartCoroutine(moveOption == "teleport"
                ? TeleportCharacter(character, targetTransform, onComplete)
                : MoveCharacter(character, targetTransform, onComplete));
        }
        
        public IEnumerator TeleportCharacter(GameObject character, Transform targetTransform, System.Action onComplete)
        {
            while (Vector3.Distance(character.transform.position, targetTransform.position) > 1)
            {
                character.transform.position = targetTransform.position;
                yield return null;
            }
            onComplete();
        }

        public IEnumerator MoveCharacter(GameObject character, Transform targetTransform, System.Action onComplete)
        {
            while (Vector3.Distance(character.transform.position, targetTransform.position) > 0.2f)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, targetTransform.position, 0.5f);
                yield return null;
            }
            onComplete();
        }
    }
}