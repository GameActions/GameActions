using System;
using System.Threading.Tasks;
using GameActions.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GameActions
{
    public class FadeAction : GameAction
    {
        public Color Target;
        public float Speed = 1;
        public Interpolators.InterpolationType InterpolationType = Interpolators.InterpolationType.ConstantAcceleration;

        private (Func<Color> GetColor, Action<Color> SetColor) DetermineFunctions(GameObject Object)
        {
            (Func<Color> GetColor, Action<Color> SetColor) result;
            var sprite_renderer = Object.GetComponent<SpriteRenderer>();
            if (sprite_renderer != null)
            {
                result.SetColor = x  => sprite_renderer.color = x;
                result.GetColor = () => sprite_renderer.color;
                return result;
            }

            var image = Object.GetComponent<Image>();
            if (image != null)
            {
                result.SetColor = x  => image.color = x;
                result.GetColor = () => image.color;
                return result;
            }

            var text = Object.GetComponent<Text>();
            if (text != null)
            {
                result.SetColor = x  => text.color = x;
                result.GetColor = () => text.color;
                return result;
            }

            var mesh = Object.GetComponent<MeshRenderer>();
            if (mesh != null)
            {
                result.SetColor = x => mesh.material.color = x;
                result.GetColor = () => mesh.material.color;
                return result;
            }

            // else
            result.SetColor = x  => Debug.LogWarning("No SpriteRenderer or Image component found on the object");
            result.GetColor = () => {
                Debug.LogWarning("No SpriteRenderer or Image component found on the object");
                return new Color();
            };
            return result;
        }

        protected override async Task Act(ActParameters Parameters)
        {
            var functions = DetermineFunctions(Parameters.Object);
            var start_color = functions.GetColor();
            for (float t = 0; t < 1; t += Time.deltaTime * Speed)
            {
                functions.SetColor(Color.Lerp(
                    start_color,
                    Target,
                    Interpolators.Interpolate(InterpolationType, t)
                ));
                await Parameters.Yield();
            }
            functions.SetColor(Target);
        }
    }
}
