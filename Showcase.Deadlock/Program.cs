
using Showcase.Deadlock.Services;

MonitorSimulation();

//DinningPhilosophersSimulation();

Thread.Sleep(-1);

static void DinningPhilosophersSimulation()
{
    const int ForksQuantity = 5;

    object[] forks = new object[ForksQuantity];

    for (int forkIndex = 0; forkIndex < ForksQuantity; forkIndex++)
        forks[forkIndex] = new object();

    var philosopherLockServices = new PhilosopherLockService[ForksQuantity];

    for (int index = 0; index < ForksQuantity;)
    {
        var rightForkIndex = (index + 1) % 5;

        philosopherLockServices[index] = new PhilosopherLockService(forks[index], forks[rightForkIndex]);
        ThreadPool.QueueUserWorkItem(state => philosopherLockServices[index].Eat());

        if (index + 1 == ForksQuantity) break;

        index++;
    }
}

static void MonitorSimulation()
{
    const int LockTimeoutInMilliseconds = 500;
    var lockObject = new object();
    var lockTaken = false;

    if (!lockTaken)
        Monitor.TryEnter(lockObject, LockTimeoutInMilliseconds, ref lockTaken);

    try
    {
        if (lockTaken)
            Console.WriteLine("Running with the locked object.");
        else
            Console.WriteLine("Running without the locked object.");
    }
    finally
    {
        Monitor.Exit(lockObject);
    }
}