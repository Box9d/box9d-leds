using System.Threading.Tasks;

namespace Box9.Leds.Core.Multitasking
{
    public static class TaskExtensions
    {
        public static void Forget(this Task task)
        {
            // This extension method exists to surpress warnings for not awaiting tasks
        }
    }
}
