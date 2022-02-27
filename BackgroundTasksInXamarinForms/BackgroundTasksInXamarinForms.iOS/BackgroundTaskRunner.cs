using Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackgroundTasksInXamarinForms.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(BackgroundTaskRunner))]
namespace BackgroundTasksInXamarinForms.iOS
{
    public class BackgroundTaskRunner : IBackgroundTaskRunner
    {
        public void RunFakeSynchronizationTask()
        {
            Task.Run(async () =>
            {
                nint taskId = 0;
                taskId = UIApplication.SharedApplication.BeginBackgroundTask(() =>
                {
                    Debug.WriteLine("### Time execution limit reached. Stopping the background task.");
                    UIApplication.SharedApplication.EndBackgroundTask(taskId);
                });

                await RunLongSynchronizationProcess();
                MessagingCenter.Instance.Send<object>(this, "SyncCompleted");

                UIApplication.SharedApplication.EndBackgroundTask(taskId);
            });
        }

        async Task RunLongSynchronizationProcess()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}