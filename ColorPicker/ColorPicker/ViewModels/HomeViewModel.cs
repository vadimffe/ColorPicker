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
      this.GaugeOutlineColor = Color.FromHex(Preferences.Get("OutlineColorHex", "#17805d"));
    }

    public Color GaugeOutlineColor
    {
      get => Color.FromHex(Preferences.Get("OutlineColorHex", "#17805d"));
      set
      {
        Preferences.Set("OutlineColorHex", value.ToHex());
        this.OnPropertyChanged();
      }
    }

    public ICommand RefreshCommand { get; }
  }
}