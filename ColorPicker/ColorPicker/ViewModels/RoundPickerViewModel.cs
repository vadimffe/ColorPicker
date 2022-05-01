using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ColorPicker.ViewModels
{
  public class RoundPickerViewModel : BaseViewModel
  {
    public RoundPickerViewModel()
    {
      this.AcceptPopupCommand = new Command(() => this.AcceptPopup());
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
        return outlineColor;
      }
      set
      {
        outlineColor = value;
        this.OnPropertyChanged();
      }
    }
  }
}
