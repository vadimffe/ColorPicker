using System;
using SkiaSharp;
using Xamarin.Forms;

namespace ColorPicker.Controls
{
  public class ColorModel
  {
    public ColorModel(int id, string hex)
    {
      Id = id;
      Color = Color.FromHex(hex);
    }

    public int Id { get; set; }
    public Color Color { get; set; }
    public int Radius { get; set; }
    public SKPoint Position { get; set; }

    public bool IsTouched(SKPoint pt)
    {

      return (Math.Pow(pt.X - Position.X, 2) / Math.Pow(Radius, 2) +
              Math.Pow(pt.Y - Position.Y, 2) / Math.Pow(Radius, 2)) < 1;
    }
  }
}
