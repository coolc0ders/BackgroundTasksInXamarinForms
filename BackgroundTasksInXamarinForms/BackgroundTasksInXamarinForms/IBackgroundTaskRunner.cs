using System;
using System.Collections.Generic;
using System.Text;

namespace BackgroundTasksInXamarinForms
{
    public interface IBackgroundTaskRunner
    {
        void RunFakeSynchronizationTask();
    }
}
