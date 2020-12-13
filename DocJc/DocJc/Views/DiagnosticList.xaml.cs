using DocJc.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace DocJc.Views
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