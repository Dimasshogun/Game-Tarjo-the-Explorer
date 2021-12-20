using Code.Scripts.Character;
using UnityEngine;

namespace Code.Scripts.Control
{
    public class MoveInput : Command.Command
    {
        private readonly Movement _movement;
        private readonly Vector3 _target;
        private readonly Vector3 _inputDirection;

        public MoveInput(Movement movement, Vector3 target, Vector2 inputDirection)
        {
            _movement = movement;
            _target = target;
            _inputDirection = inputDirection;
        }
        
        public override void Execute()
        {
            _movement.Move(_target, _inputDirection);
        }

        public override void Undo()
        {
            _movement.Move(-_target, -_inputDirection);
        }
    }
}