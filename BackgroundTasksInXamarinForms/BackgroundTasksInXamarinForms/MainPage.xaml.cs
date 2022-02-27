using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BackgroundTasksInXamarinForms
{
    public partial class MainPage : ContentPage
    {
        private bool _isWorkRunning;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Instance.Subscribe<object>(this, "SyncCompleted", (obj) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Label1.Text = "Sync completed";
                    _isWorkRunning = false;
                });
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Instance.Unsubscribe<object>(this, "SyncCompleted");
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            if (!_isWorkRunning)
            {
                DependencyService.Get<IBackgroundTaskRunner>().RunFakeSynchronizationTask();
                _isWorkRunning = true;
                Label1.Text = "Started the Background task";
            }
            else
            {
                Label1.Text = "The Background task is already running";
            }
        }
    }
}
