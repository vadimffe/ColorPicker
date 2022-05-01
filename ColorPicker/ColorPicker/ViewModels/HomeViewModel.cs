using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ColorPicker.ViewModels
{
  public class HomeViewModel : BaseViewModel
  {
    public HomeViewModel()
    {
      this.Title = "Main";
      this.RefreshCommand = new Command(() => this.UpdateColor());
    }

    private void UpdateColor()
    {
      string pickedColorData = Preferences.Get("OutlineColorData", "");
      string[] colorData = pickedColorData.Split(';');

      this.GaugeOutlineColor = Color.FromHex(colorData[0]);
    }

    public Color GaugeOutlineColor
    {
      get
      {
        string pickedColorData = Preferences.Get("OutlineColorData", "");
        string[] colorData = pickedColorData.Split(';');

        return Color.FromHex(colorData[0]);
      }
      set
      {
        Preferences.Set("OutlineColorData", value.ToHex());
        this.OnPropertyChanged();
      }
    }

    public ICommand RefreshCommand { get; }
  }
}