using System.Collections.Generic;
using Code.Scripts.Character;
using Command;
using UnityEngine;

namespace Code.Scripts.Control
{
    public class MovementInputHandler: MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject gameplayUI;
        [SerializeField] private float baseSpeed = 10f;
        
        private float _speed;
        
        private Movement _playerMovement;
        private Transform _mainCameraTransform;
        
        private MovementControls _controls;
        
        private Queue<Command.Command> commands = new Queue<Command.Command>();
        
        private void Awake()
        {
            _controls = new MovementControls();
        }

        private void Start()
        {
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }

            _playerMovement = player.GetComponent<Movement>();
            _mainCameraTransform = Camera.main.transform;
            _speed = baseSpeed;
        }

        private void FixedUpdate()
        {
            Command.Command moveCommand = HandlePlayerMovement();
            if (moveCommand == null)
            {
                return;
            }

            commands.Enqueue(moveCommand);
            moveCommand.Execute();
        }

        private Command.Command HandlePlayerMovement()
        {
            var inputDirection = _controls.Player.Movement.ReadValue<Vector2>();
            if (inputDirection.x == 0f && inputDirection.y == 0f)
            {
                return new MoveInput(_playerMovement, player.transform.position, inputDirection);
            }

            var right = _mainCameraTransform.right;
            var forward = _mainCameraTransform.forward;
                
            right.y = 0f;
            forward.y = 0f;
                
            right.Normalize();
            forward.Normalize();
                
            var direction = forward * inputDirection.y + right * inputDirection.x;
                
            Vector3 target = transform.position + direction * _speed * Time.deltaTime;
                
            return new MoveInput(_playerMovement, target, inputDirection);

        }

        private void OnEnable()
        {
            _controls.Player.Enable();
            gameplayUI.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            CameraManager.Instance.SwitchToPlayerCam();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
            gameplayUI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
