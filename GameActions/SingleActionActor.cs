using UnityEngine;

namespace GameActions
{
    public abstract class SingleActionActor : MonoBehaviour
    {
        [SerializeField] private GameAction _Action;
        public GameAction Action
        {
            set { _Action = value; }
            get
            {
                if (_Action == null)
                    _Action = GetComponent<GameAction>();
                return _Action;
            }
        }
    }
}
