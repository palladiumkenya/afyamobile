using System;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.App;
using MvvmCross.Droid.Support.V4;

namespace LiveHTS.Droid.Adapters
{
    public class RegAdapter:MvxFragmentPagerAdapter
    {
        public RegAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public RegAdapter(Context context, FragmentManager fragmentManager, IEnumerable<FragmentInfo> fragments) : base(context, fragmentManager, fragments)
        {
        }
    }
}