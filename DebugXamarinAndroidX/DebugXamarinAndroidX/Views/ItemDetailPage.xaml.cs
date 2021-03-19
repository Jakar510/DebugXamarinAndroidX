using DebugXamarinAndroidX.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace DebugXamarinAndroidX.Views
{
	public partial class ItemDetailPage : ContentPage
	{
		public ItemDetailPage()
		{
			InitializeComponent();
			BindingContext = new ItemDetailViewModel();
		}
	}
}