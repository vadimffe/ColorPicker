using ColorPicker.Controls;
using ColorPicker.Models;
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
      this.CirclePickerCommand = new Command(() => this.OpenCirclePopup());
    }

    private void OpenCirclePopup()
    {
      Shell.Current.ShowPopup(new RoundPickerPopup());
    }

    private void OpenPopup()
    {
      Shell.Current.ShowPopup(new CustomPopup());
    }

    public ICommand PopupCommand { get; }

    public ICommand CirclePickerCommand { get; }


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

    private ListViewColorModel selectedColor;
    public ListViewColorModel SelectedColor
    {
      get
      {
        return selectedColor;
      }
      set
      {
        selectedColor = value;
        Debug.WriteLine(value.Color);
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
        if(value != null)
        {
          Preferences.Set("OutlineColorData", string.Format("{0};{1};{2}", value.ColorHex, value.ColorPoint.X, value.ColorPoint.Y));
          pickedColorData = value;

          if (value != null)
          {
            this.OutlineColorHex = value.ColorHex;
            this.Coordinates = string.Format("X: {0} | Y: {1}", value.ColorPoint.X, value.ColorPoint.Y);
          }
        }

        this.OnPropertyChanged();
      }
    }
  }
}