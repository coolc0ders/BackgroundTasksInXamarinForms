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
using System.Threading.Tasks;
using AndroidX.Concurrent.Futures;
using AndroidX.Work;
using Google.Common.Util.Concurrent;
using Xamarin.Forms;
using Object = Java.Lang.Object;

namespace BackgroundTasksInXamarinForms.Droid
{
    public class FakeSynchronysationWorker : ListenableWorker, CallbackToFutureAdapter.IResolver
    {
        public const string TAG = "SyncWorker";

        public FakeSynchronysationWorker(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public FakeSynchronysationWorker(Context appContext, WorkerParameters workerParams) : base(appContext, workerParams)
        {
        }

        //Signals that the work has started.
        public override IListenableFuture StartWork()
        {
            return CallbackToFutureAdapter.GetFuture(this);
        }

        public Object AttachCompleter(CallbackToFutureAdapter.Completer p0)
        {
            Task.Run(async () =>
            {
                await RunLongSynchronizationProcess();

                MessagingCenter.Instance.Send<object>(this, "SyncCompleted");
                p0.Set(Result.InvokeSuccess());
            });
            return TAG;
        }

        async Task RunLongSynchronizationProcess()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}