using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AboutSelf.Components;
using AboutSelf.Droid;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(GestureScrollView), typeof(GestureScrollViewRenderer))]

namespace AboutSelf.Droid
{

    
        public class GestureScrollViewRenderer : ScrollViewRenderer
    {
        readonly CustomGestureListener _listener;
        readonly GestureDetector _detector;

        public GestureScrollViewRenderer(Context context) : base(context)
        {
            _listener = new CustomGestureListener();
            _detector = new GestureDetector(context, _listener);
        }

        public override bool DispatchTouchEvent(MotionEvent e)
        {
            if (_detector != null)
            {
                _detector.OnTouchEvent(e);
                base.DispatchTouchEvent(e);
                return true;
            }

            return base.DispatchTouchEvent(e);
        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            base.OnTouchEvent(ev);

            if (_detector != null)
                return _detector.OnTouchEvent(ev);

            return false;
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                _listener.OnSwipeLeft -= HandleOnSwipeLeft;
                _listener.OnSwipeRight -= HandleOnSwipeRight;
            }

            if (e.OldElement == null)
            {
                _listener.OnSwipeLeft += HandleOnSwipeLeft;
                _listener.OnSwipeRight += HandleOnSwipeRight;
            }
        }

        void HandleOnSwipeLeft(object sender, EventArgs e) =>
            ((GestureScrollView)Element).OnSwipeLeft();

        void HandleOnSwipeRight(object sender, EventArgs e) =>
            ((GestureScrollView)Element).OnSwipeRight();
    }
}
