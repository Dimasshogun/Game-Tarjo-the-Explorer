using System;
using Cinemachine;
using UnityEngine;

namespace Code.Scripts.Character
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
        private CinemachineVirtualCamera _npcCamera;
        private int _originalPriority;

        private void Start()
        {
            _playerCamera = GameObject.FindWithTag("Player").GetComponentInChildren<CinemachineFreeLook>();
        }

        public void SwitchToNpcCam(CinemachineVirtualCamera npcCamera)
        {
            _npcCamera = npcCamera;
            _originalPriority = npcCamera.Priority;
            npcCamera.Priority = _playerCamera.Priority + 1;
        }

        public void SwitchToPlayerCam()
        {
            _npcCamera.Priority = _originalPriority;
        }
    }
}