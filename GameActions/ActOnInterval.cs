using UnityEngine;

namespace GameActions
{
    public class ActOnInterval : MonoBehaviour
    {
        public float Interval = 1;
        public bool ActOnAwake = true;
        public GameAction Action;

        private float LastTime;

        private void Awake()
        {
            LastTime = Time.time;
            if (ActOnAwake)
                Action?.ActReturningVoid();
        }

        private void Update()
        {
            if (LastTime + Interval < Time.time)
            {
                LastTime = Time.time;
                Action?.ActReturningVoid();
            }
        }
    }
}
