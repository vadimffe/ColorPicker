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

      this.Point = new Point(250, 44);
    }

    private void AcceptPopup()
    {
      throw new NotImplementedException();
    }

    public ICommand AcceptPopupCommand { get; }

    private Point point;
    public Point Point
    {
      get
      {
        return point;
      }
      set
      {
        point = value;
        this.OnPropertyChanged();
      }
    }

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

    private string pickedColorData;
    public string PickedColorData
    {
      get
      {
        Preferences.Get("OutlineColorData", "");
        return pickedColorData;
      }
      set
      {
        Preferences.Set("OutlineColorData", value);
        pickedColorData = value;
        this.OnPropertyChanged();
      }
    }
  }
}
