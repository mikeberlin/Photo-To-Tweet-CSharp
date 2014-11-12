// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace PhotoToTweet
{
	[Register ("PhotoToTweetViewController")]
	partial class PhotoToTweetViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnTakePhoto { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnToggleFilter { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnTweetPhoto { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView ivPhotoToTweet { get; set; }

		[Action ("btnTakePhoto_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnTakePhoto_TouchUpInside (UIButton sender);

		[Action ("btnToggleFilter_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnToggleFilter_TouchUpInside (UIButton sender);

		[Action ("btnTweetPhoto_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnTweetPhoto_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (btnTakePhoto != null) {
				btnTakePhoto.Dispose ();
				btnTakePhoto = null;
			}
			if (btnToggleFilter != null) {
				btnToggleFilter.Dispose ();
				btnToggleFilter = null;
			}
			if (btnTweetPhoto != null) {
				btnTweetPhoto.Dispose ();
				btnTweetPhoto = null;
			}
			if (ivPhotoToTweet != null) {
				ivPhotoToTweet.Dispose ();
				ivPhotoToTweet = null;
			}
		}
	}
}
