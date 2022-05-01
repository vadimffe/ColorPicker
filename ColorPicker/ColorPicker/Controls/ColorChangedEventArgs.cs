using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ColorPicker.Controls
{
  public class ColorChangedEventArgs : EventArgs
  {

    public ColorChangedEventArgs(Color color)
    {
      this.Color = color;
    }

    public Color Color { get; private set; }
  }
}
