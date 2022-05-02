using ColorPicker.Dialogs;
using ColorPicker.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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

      this.ColorCollection = InitializeColors();
      this.SelectedColor = this.ColorCollection.Where(w => w.Id == 1).FirstOrDefault();
    }

    private ObservableCollection<CollectionViewColorModel> InitializeColors()
    {
      return new ObservableCollection<CollectionViewColorModel> {
          new CollectionViewColorModel{ Id = 1, Color = "#25c5db" },
          new CollectionViewColorModel{ Id = 2, Color = "#0098a6" },
          new CollectionViewColorModel{ Id = 3, Color = "#0e47a1" },
          new CollectionViewColorModel{ Id = 4, Color = "#1665c1" },
          new CollectionViewColorModel{ Id = 5, Color = "#039be6" },

          new CollectionViewColorModel{ Id = 6, Color = "#64b5f6" },
          new CollectionViewColorModel{ Id = 7, Color = "#ff7000" },
          new CollectionViewColorModel{ Id = 8, Color = "#ff9f00" },
          new CollectionViewColorModel{ Id = 9, Color = "#ffb200" },
          new CollectionViewColorModel{ Id = 10, Color = "#cf9702" },
        };

      //this.ItemsSource = this.Colors;
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

    private ObservableCollection<CollectionViewColorModel> colorCollection;
    public ObservableCollection<CollectionViewColorModel> ColorCollection
    {
      get
      {
        return colorCollection;
      }
      set
      {
        colorCollection = value;
        this.OnPropertyChanged();
      }
    }

    private CollectionViewColorModel selectedColor;
    public CollectionViewColorModel SelectedColor
    {
      get
      {
        return selectedColor;
      }
      set
      {
        if (value != null)
        {
          selectedColor = value;
          Debug.WriteLine(value.Color);
        }
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