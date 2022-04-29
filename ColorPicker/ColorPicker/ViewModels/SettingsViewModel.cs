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

      this.OutlineColor = Color.FromHex("#FF40B87D");
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

    public Color outlineColor;
    public Color OutlineColor
    {
      get
      {
        Debug.WriteLine(outlineColor.ToHex());
        return outlineColor;
      }
      set
      {
        outlineColor = value;
        Debug.WriteLine(value.ToHex());
        this.OnPropertyChanged();
      }
    }
  }
}