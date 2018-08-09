using Android.App;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace LiveHTS.Droid.Custom
{
    public class ObsRecyclerListener : Java.Lang.Object, AbsListView.IOnScrollListener
    {
        private Activity _activity;

        public ObsRecyclerListener(Activity activity)
        {
            _activity = activity;
        }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            // do nothing 
        }

        public void OnScrollStateChanged(AbsListView view, ScrollState scrollState)
        {
            if (scrollState == ScrollState.TouchScroll)
            {
                View currentFocus = _activity.CurrentFocus;
                if (null!=currentFocus)
                {
                    currentFocus.ClearFocus();
                }
            }
        }
    }
}