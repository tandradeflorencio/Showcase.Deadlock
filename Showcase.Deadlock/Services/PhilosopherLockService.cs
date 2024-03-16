using Showcase.Deadlock.Services.Interfaces;

namespace Showcase.Deadlock.Services
{
    internal class PhilosopherLockService(object leftFork, object rightFork) : IPhilosopherService
    {
        public void Eat()
        {
            lock (leftFork)
            {
                Console.WriteLine($"Thread ({Environment.CurrentManagedThreadId}) picked up left fork.");
                SimulateSomeProcessingTime();

                lock (rightFork)
                {
                    Console.WriteLine($"Thread ({Environment.CurrentManagedThreadId}) picked up right fork and is eating.");
                    SimulateEatingTime();
                }
            }            
        }

        private static void SimulateSomeProcessingTime()
        {
            Thread.Sleep(500);
        }

        private static void SimulateEatingTime()
        {
            Thread.Sleep(500);
        }
    }
}
