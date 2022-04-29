using System.Diagnostics;
using Xamarin.Forms;

namespace ColorPicker.ViewModels
{
  public class SettingsViewModel : BaseViewModel
  {
    public SettingsViewModel()
    {
      this.Title = "Settings";

      this.Point = new Point(744, 251);
    }

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
        Debug.WriteLine(string.Format("{0} {1}", value.X, value.Y));
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
        this.OnPropertyChanged();
      }
    }
  }
}