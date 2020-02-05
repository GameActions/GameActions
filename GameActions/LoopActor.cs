using System.Threading.Tasks;

namespace GameActions
{
    public class LoopActor : SingleActionActor
    {
        private Task ActTask = Task.CompletedTask;

        private void Update()
        {
            if (ActTask.IsCompleted)
            {
                ActTask = Action?.Act();
                if (ActTask == null)
                    ActTask = Task.CompletedTask;
            }
        }
    }
}
