using UnityEngine;
using UnityEngine.UI;

namespace GameActions
{
    public abstract class AnimateMaterialPropertyAction<AnimationDataType> : AnimateAction<AnimationDataType, Material>
    {
        [Space]
        public string PropertyName = "_PropertyName";

        protected sealed override Material InitializeContext(ActParameters Parameters, ref bool Success)
        {
            var renderer = Parameters.Object.GetComponent<Renderer>();
            if (renderer != null)
                return renderer.material;

            var graphic = Parameters.Object.GetComponent<Graphic>();
            if (graphic != null)
                return graphic.material;

            Debug.LogWarning("No Renderer or Graphic component found on the object", this);
            Success = false;
            return null;
        }
    }
}
