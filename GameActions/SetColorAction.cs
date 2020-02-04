using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameActions
{
    public class SetColorAction : GameActionWithTargetObject
    {
        public Color Target;

        private void SetColor(GameObject Object, Color color)
        {
            var graphic = Object.GetComponent<Graphic>();
            if (graphic != null)
            {
                graphic.color = color;
                return;
            }

            var sprite_renderer = Object.GetComponent<SpriteRenderer>();
            if (sprite_renderer != null)
            {
                sprite_renderer.color = color;
                return;
            }

            var renderer = Object.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
                return;
            }

            // else
            Debug.unityLogger.LogWarning("No Renderer or Graphic component found on the object", this);
        }

        protected override Task Act(ActParameters Parameters)
        {
            SetColor(Parameters.Object, Target);
            return Task.CompletedTask;
        }
    }
}
