using ColorPicker.Controls;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ColorPicker.ViewModels
{
  public class SettingsViewModel : BaseViewModel
  {
    public SettingsViewModel()
    {
      this.Title = "Settings";

      //this.Point = new Point(1161, 70);

      //this.PickedColorData = Preferences.Get("OutlineColorData", "");

      this.PopupCommand = new Command(() => this.OpenPopup());
    }

    private void OpenPopup()
    {
      Shell.Current.ShowPopup(new CustomPopup());
    }

    public ICommand PopupCommand { get; }

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
        Debug.WriteLine(string.Format("X: {0} | Y: {1}", value.X, value.Y));
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
        Debug.WriteLine(value.ToHex());
        //this.OutlineColorHex = value.ToHex();
        //this.Coordinates = string.Format("X: {0} | Y: {1}", Point.X, Point.Y);
        this.OnPropertyChanged();
      }
    }

    private string outlineColorHex;
    public string OutlineColorHex
    {
      get
      {
        return outlineColorHex;
      }
      set
      {
        outlineColorHex = value;
        this.OnPropertyChanged();
      }
    }

    private string coordinates;
    public string Coordinates
    {
      get
      {
        return coordinates;
      }
      set
      {
        coordinates = value;
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
        if(value != null)
        {
          string[] colorData = value.Split(';');
          this.Point = new Point(Convert.ToDouble(colorData[1]), Convert.ToDouble(colorData[2]));
          this.OutlineColorHex = colorData[0];
          this.Coordinates = string.Format("X: {0} | Y: {1}", colorData[1], colorData[2]);
        }
        this.OnPropertyChanged();
      }
    }
  }
}