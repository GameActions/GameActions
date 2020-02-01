using System.Threading.Tasks;
using GameActions.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GameActions
{
    public class AnimateMaterialColorAction : GameActionWithTargetObject
    {
        public string PropertyName = "_PropertyName";
        public Color Value;
        public float Duration = 1;
        public Interpolators.InterpolationType InterpolationType = Interpolators.InterpolationType.ConstantAcceleration;

        protected override async Task Act(ActParameters Parameters)
        {
            Material material = null;

            {
                var renderer = Parameters.Object.GetComponent<Renderer>();
                if (renderer != null)
                    material = renderer.material;
            }
            
            if (material == null)
            {
                var graphic = Parameters.Object.GetComponent<Graphic>();
                if (graphic != null)
                    material = graphic.material;
            }

            if (material == null)
            {
                Debug.LogWarning("No Renderer or Graphic component found on the object");
                return;
            }

            if (Duration <= 0)
            {
                material.SetColor(PropertyName, Value);
                return;
            }
            float Speed = 1 / Duration;

            var start_value = material.GetColor(PropertyName);
            for (float t = 0; t < 1; t += Time.deltaTime * Speed)
            {
                material.SetColor(PropertyName, Color.Lerp(
                    start_value,
                    Value,
                    Interpolators.Interpolate(InterpolationType, t)
                ));
                await Parameters.Yield();
            }
            material.SetColor(PropertyName, Value);
        }
    }
}
