using UnityEngine;

namespace GameActions
{
    public abstract class GameActionWithTargetObject : GameAction
    {
        [Tooltip("The target object to act on; This is set to this GameObject by default when null.")]
        [SerializeField] private GameObject _Object;

        protected override GameObject GetObject()
        {
            if (_Object == null)
                _Object = this.gameObject;
            return _Object;
        }

        public GameObject Object
        {
            get => GetObject();
            set => _Object = value;
        }
    }
}
