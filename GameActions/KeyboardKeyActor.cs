using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    [AddComponentMenu("GameActions/Actors/Keyboard Key Actor")]
    public class KeyboardKeyActor : SingleActionActor
    {
        public enum KeyAction { KeyDown, KeyUp }

        public KeyCode Key;
        public KeyAction _KeyAction;
        public bool InterruptLastAction;

        private Task ActTask = Task.CompletedTask;

        private void Act()
        {
            if (InterruptLastAction)
            {
                Action?.StartActing();
                ActTask = Task.CompletedTask;
            }
            if (ActTask.IsCompleted)
            {
                ActTask = Action?.Act();
                if (ActTask == null)
                    ActTask = Task.CompletedTask;
            }
        }

        private void Update()
        {
            if (_KeyAction == KeyAction.KeyDown)
            {
                if (Input.GetKeyDown(Key))
                {
                    Act();
                }
            }
            else if (_KeyAction == KeyAction.KeyUp)
            {
                if (Input.GetKeyUp(Key))
                {
                    Act();
                }
            }
        }
    }
}
