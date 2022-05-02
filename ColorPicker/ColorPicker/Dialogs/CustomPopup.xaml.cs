using System;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace ColorPicker.Dialogs
{
  public partial class CustomPopup : Popup
  {
    public CustomPopup()
    {
      InitializeComponent();
    }

    private void Cancel_Clicked(object sender, EventArgs e)
    {
      Dismiss(null);
    }
  }
}