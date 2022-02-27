using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidX.Work;
using BackgroundTasksInXamarinForms.Droid;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(BackgroundTaskRunner))]
namespace BackgroundTasksInXamarinForms.Droid
{
    public class BackgroundTaskRunner : IBackgroundTaskRunner
    {
        public void RunFakeSynchronizationTask()
        {
            OneTimeWorkRequest syncWorkerRequest = new OneTimeWorkRequest
                    .Builder(typeof(FakeSynchronysationWorker))
                .AddTag(FakeSynchronysationWorker.TAG)
                .Build();
            WorkManager.GetInstance(Platform.CurrentActivity).BeginUniqueWork(FakeSynchronysationWorker.TAG, ExistingWorkPolicy.Keep, syncWorkerRequest)
                .Enqueue();
        }
    }
}