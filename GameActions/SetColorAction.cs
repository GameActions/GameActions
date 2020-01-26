using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameActions
{
    public class SetColorAction : GameAction
    {
        public Color Target;

        private void SetColor(GameObject Object, Color color)
        {
            var sprite_renderer = Object.GetComponent<SpriteRenderer>();
            if (sprite_renderer != null)
            {
                sprite_renderer.color = color;
                return;
            }

            var image = Object.GetComponent<Image>();
            if (image != null)
            {
                image.color = color;
                return;
            }

            var text = Object.GetComponent<Text>();
            if (text != null)
            {
                text.color = color;
                return;
            }

            var mesh = Object.GetComponent<MeshRenderer>();
            if (mesh != null)
            {
                mesh.material.color = color;
                return;
            }

            // else
            Debug.LogWarning("No SpriteRenderer or Image component found on the object", this);
        }

        protected override Task Act(ActParameters Parameters)
        {
            SetColor(Parameters.Object, Target);
            return Task.CompletedTask;
        }
    }
}
