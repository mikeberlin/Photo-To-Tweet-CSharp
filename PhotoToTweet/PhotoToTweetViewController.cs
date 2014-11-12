using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreImage;
using Xamarin.Social;
using Xamarin.Social.Services;

namespace PhotoToTweet
{
	public partial class PhotoToTweetViewController : UIViewController
	{
		private UIImagePickerController _camera = new UIImagePickerController();
		private UIImage _originalImage;
		private UIImage _sepiaImage;
		private CISepiaTone _sepiaFilter;
		private bool _filterApplied = false;
		private UIAlertView _selectImageFirst = new UIAlertView ("Select an image y0!", "Please make sure you've selected an image before toggling the filter or tweeting.", null, "kthxbye", null);

		private bool HasPhotoBeenSelected { get { return (ivPhotoToTweet.Image != null); } }

		public PhotoToTweetViewController (IntPtr handle) : base (handle) {
			// check to see if we're on the simulator or an actual device
			// simulator will crash if we try to use the camera directly (there is no camera on the simulator)
			if (MonoTouch.ObjCRuntime.Runtime.Arch == MonoTouch.ObjCRuntime.Arch.DEVICE) {
				_camera.SourceType = UIImagePickerControllerSourceType.Camera;
				_camera.CameraDevice = UIImagePickerControllerCameraDevice.Rear;
				_camera.CameraCaptureMode = UIImagePickerControllerCameraCaptureMode.Photo;
				_camera.ShowsCameraControls = true;
			} else {
				_camera.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			}

			_camera.Canceled += (object sender, EventArgs e) => {
				var imagePicker = sender as UIImagePickerController;
				imagePicker.DismissViewController(true, null);
			};

			_camera.FinishedPickingMedia += (object sender, UIImagePickerMediaPickedEventArgs e) => {
				_camera.DismissViewController(false, () => {
					ivPhotoToTweet.Image = e.OriginalImage;
					_filterApplied = false;

					_originalImage = e.OriginalImage;

					_sepiaFilter = new CISepiaTone ();
					_sepiaFilter.Image = new CIImage (_originalImage.CGImage);
					_sepiaFilter.Intensity = 0.8f;

					var ciContext = CIContext.FromOptions (null);
					_sepiaImage = new UIImage (ciContext.CreateCGImage (_sepiaFilter.OutputImage, _sepiaFilter.Image.Extent));
				});
			};
		}

		partial void btnTakePhoto_TouchUpInside (UIButton btnSender) {
			this.NavigationController.PresentViewController(_camera, true, null);
		}

		partial void btnToggleFilter_TouchUpInside (UIButton sender) {
			if (HasPhotoBeenSelected) {
				if (_filterApplied) {
					ivPhotoToTweet.Image = new UIImage(_originalImage.CGImage, 1.0f, _originalImage.Orientation);
					_filterApplied = false;
					btnToggleFilter.BackgroundColor = UIColor.Blue;
				} else {
					ivPhotoToTweet.Image = new UIImage(_sepiaImage.CGImage, 1.0f, _originalImage.Orientation);
					_filterApplied = true;
					btnToggleFilter.BackgroundColor = UIColor.Orange;
				}
			} else {
				_selectImageFirst.Show();
			}
		}

		partial void btnTweetPhoto_TouchUpInside (UIButton sender) {
			if (HasPhotoBeenSelected) {
				var tweet = new Item();
				tweet.Text = "I'm tweeting using #csharp! #xamarin";
				// add links if you'd like or anything else to the tweet
				// tweet.Links.Add(new Uri("http://xamarin.com"));
				tweet.Images.Add(new ImageData(ivPhotoToTweet.Image));

				var twitterService = new Twitter5Service();
				var vcTwitter = twitterService.GetShareUI(tweet, (shareResult) => {
					this.DismissViewController(true, null);
				});

				this.PresentViewController(vcTwitter, true, null);
			} else {
				_selectImageFirst.Show();
			}
		}
	}
}