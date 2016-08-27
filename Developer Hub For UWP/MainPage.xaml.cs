using System;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;
using Core.Animation;

namespace Developer_Hub_For_UWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var compositor = backDrop.VisualProperties.Compositor;
            var blurAnim = compositor.CreateScalarKeyFrameAnimation();
            blurAnim.Duration = TimeSpan.FromSeconds(10);
            blurAnim.InsertKeyFrame(0.0f, 0);
            blurAnim.InsertKeyFrame(0.5f, (float)backDrop.BlurAmount); // animate around the specified value
            blurAnim.InsertKeyFrame(1.0f, 0);
            blurAnim.IterationBehavior = AnimationIterationBehavior.Forever;

            backDrop.VisualProperties.StartAnimation(BackDrop.BlurAmountProperty, blurAnim);
        }
    }
}
