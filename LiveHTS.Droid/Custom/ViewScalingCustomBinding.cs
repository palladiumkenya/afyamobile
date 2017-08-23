using System;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.Target;

namespace LiveHTS.Droid.Custom
{
    public class ViewScalingCustomBinding : MvxAndroidTargetBinding
    {
        public ViewScalingCustomBinding(object target) : base(target)
        {
        }

        public override Type TargetType
        {
            get { return typeof(int); }
        }

        protected override void SetValueImpl(object target, object value)
        {
            var realTarget = target as View;
            if (target == null)
                return;

            var scaling = (int) value;

            ViewGroup.LayoutParams layoutParameters = realTarget.LayoutParameters;
            realTarget.LayoutParameters = new LinearLayout.LayoutParams(layoutParameters.Width,scaling);
        }
    }
}