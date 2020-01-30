using System.Threading.Tasks;

namespace GameActions
{
    public class ToggleActivationAction : GameActionWithTargetObject
    {
        protected override Task Act(ActParameters Parameters)
        {
            Parameters.Object.SetActive(!Parameters.Object.activeSelf);
            return Task.CompletedTask;
        }
    }
}
