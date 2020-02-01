using System.Threading.Tasks;
using GameActions.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GameActions
{
    public class AnimateMaterialFloatAction : GameActionWithTargetObject
    {
        public string PropertyName;
        public float Value;
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
                material.SetFloat(PropertyName, Value);
                return;
            }
            float Speed = 1 / Duration;

            var start_value = material.GetFloat(PropertyName);
            for (float t = 0; t < 1; t += Time.deltaTime * Speed)
            {
                material.SetFloat(PropertyName, Mathf.Lerp(
                    start_value,
                    Value,
                    Interpolators.Interpolate(InterpolationType, t)
                ));
                await Parameters.Yield();
            }
            material.SetFloat(PropertyName, Value);
        }
    }
}
