using System.Threading.Tasks;

namespace GameActions
{
    public class SetActivationAction : GameActionWithTargetObject
    {
        public bool ActivationState = true;

        protected override Task Act(ActParameters Parameters)
        {
            Parameters.Object.SetActive(ActivationState);
            return Task.CompletedTask;
        }
    }
}
