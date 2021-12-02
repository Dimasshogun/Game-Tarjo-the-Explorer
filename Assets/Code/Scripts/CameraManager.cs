using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Yarn.Unity;

namespace Code.Scripts
{
    public class CameraManager : MonoBehaviour
    {
        private static CameraManager _instance;
        public static CameraManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CameraManager>();
                    if (_instance == null)
                    {
                        Debug.LogError("CameraManager Not Found!");
                    }
                }
                return _instance;
            }
        }

        private CinemachineFreeLook _playerCamera;
        private CameraInstance _currentVirtualCam;

        public List<CameraInstance> cameraInstances = new List<CameraInstance>();

        private void Start()
        {
            _playerCamera = GameObject.FindWithTag("Player").GetComponentInChildren<CinemachineFreeLook>();
            cameraInstances.AddRange(FindObjectsOfType<CameraInstance>());
        }

        public void SwitchVirtualCam(CameraInstance npcCamera)
        {
            if (_currentVirtualCam != null)
            {
                _currentVirtualCam.virtualCam.Priority = _currentVirtualCam.originalPriority;
            }
            npcCamera.virtualCam.Priority = _playerCamera.Priority + 1;
            _currentVirtualCam = npcCamera;
        }

        public void SwitchToPlayerCam()
        {
            _currentVirtualCam.virtualCam.Priority = _currentVirtualCam.originalPriority;
        }
    }
}