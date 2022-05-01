using ColorPicker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ColorPicker.ViewModels
{
  public class CustomPopupViewModel : BaseViewModel
  {
    public CustomPopupViewModel()
    {
      this.AcceptPopupCommand = new Command(() => this.AcceptPopup());

      //this.Point = new Point(250, 44);
    }

    private void AcceptPopup()
    {
      throw new NotImplementedException();
    }

    public ICommand AcceptPopupCommand { get; }


    private Color outlineColor;
    public Color OutlineColor
    {
      get
      {
        //Debug.WriteLine(outlineColor.ToHex());
        return outlineColor;
      }
      set
      {
        outlineColor = value;
        this.OnPropertyChanged();
      }
    }

    private ColorPickerModel pickedColorData;
    public ColorPickerModel PickedColorData
    {
      get
      {
        //Preferences.Get("OutlineColorData", string.Format("{0};{1};{2}", "#FFFFFF", 50, 50));
        return pickedColorData;
      }
      set
      {
        if (value != null)
        {
          Preferences.Set("OutlineColorData", string.Format("{0};{1};{2}", value.ColorHex, value.ColorPoint.X, value.ColorPoint.Y));
          pickedColorData = value;
        }

        this.OnPropertyChanged();
      }
    }
  }
}
