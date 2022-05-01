using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColorPicker.Controls
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class RoundPickerPopup : Popup
  {
    public RoundPickerPopup()
    {
      InitializeComponent();
    }

    private void Cancel_Clicked(object sender, EventArgs e)
    {
      Dismiss(null);
    }
  }
}