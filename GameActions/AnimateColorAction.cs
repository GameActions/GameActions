using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameActions
{
    [AddComponentMenu("GameActions/Actions/Animate Color Action")]
    public class AnimateColorAction : AnimateAction<Color, (Func<Color> GetColor, Action<Color> SetColor)>
    {
        protected override (Func<Color> GetColor, Action<Color> SetColor) InitializeContext(ActParameters Parameters, ref bool Success)
        {
            (Func<Color> GetColor, Action<Color> SetColor) result;

            var graphic = Parameters.Object.GetComponent<Graphic>();
            if (graphic != null)
            {
                result.SetColor = x  => graphic.color = x;
                result.GetColor = () => graphic.color;
                return result;
            }

            var sprite_renderer = Parameters.Object.GetComponent<SpriteRenderer>();
            if (sprite_renderer != null)
            {
                result.SetColor = x  => sprite_renderer.color = x;
                result.GetColor = () => sprite_renderer.color;
                return result;
            }

            var renderer = Parameters.Object.GetComponent<Renderer>();
            if (renderer != null)
            {
                result.SetColor = x  => renderer.material.color = x;
                result.GetColor = () => renderer.material.color;
                return result;
            }

            // else
            Debug.LogWarning("No Renderer or Graphic component found on the object", this);
            Success = false;
            result.SetColor = null;
            result.GetColor = null;
            return result;
        }

        protected override Color EvaluateStartPoint(ref (Func<Color> GetColor, Action<Color> SetColor) Context)
        {
            return Context.GetColor();
        }

        protected override Color Lerp(Color A, Color B, float t)
        {
            return Color.Lerp(A, B, t);
        }

        protected override void Set(ref (Func<Color> GetColor, Action<Color> SetColor) Context, Color Data)
        {
            Context.SetColor(Data);
        }
    }
}
