using ColorPicker.Models;
using ColorPicker.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ColorPicker.ViewModels
{
  public class SettingsViewModel : BaseViewModel
  {
    public SettingsViewModel()
    {
      this.Title = "Settings";

      //this.OutlineColor = Color.Blue;  
    }

    //private Color outlineColor;
    //public Color OutlineColor
    //{
    //  get
    //  {
    //    return outlineColor;
    //  }
    //  set
    //  {
    //    outlineColor = value;
    //    this.OnPropertyChanged();
    //  }
    //}

    public Color OutlineColor
    {
      get
      {
        Debug.WriteLine(Preferences.Get("OutlineColorHex", "#17805d"));
        return Color.FromHex(Preferences.Get("OutlineColorHex", "#17805d"));
      }
      set
      {
        Preferences.Set("OutlineColorHex", value.ToHex());
        //Debug.WriteLine(value.ToHex());
        this.OnPropertyChanged();
      }
    }
  }
}