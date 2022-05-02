using System;
using System.Collections.Generic;
using System.Linq;
// OR could use System.Drawing.Color.
using Color = Xamarin.Forms.Color;

namespace ColorPicker.Helpers
{
  public class HSV
  {
    #region --- static ---
    public static HSV FromColor(Color color)
    {
      ColorToHSV(color, out double hue, out double saturation, out double value);
      return new HSV(hue, saturation, value);
    }

    public static List<HSV> FromColors(IEnumerable<Color> colors)
    {
      return colors.Select(color => FromColor(color)).ToList();
    }

    const double Epsilon = 0.000001;

    // returns Tuple<int colorIndex, double wgtB>.
    public static Tuple<int, double> FindHueInColors(IList<HSV> colors, double goalHue)
    {
      int colorIndex;
      double wgtB = 0;
      // "- 1": because each iteration needs colors[colorIndex+1].
      for (colorIndex = 0; colorIndex < colors.Count - 1; colorIndex++)
      {
        wgtB = colors[colorIndex].WgtFromHue(colors[colorIndex + 1], goalHue);
        // Epsilon compensates for possible round-off error in WgtFromHue.
        // To ensure the color is considered within one of the ranges.
        if (wgtB >= 0 - Epsilon && wgtB < 1)
          break;
      }

      return new Tuple<int, double>(colorIndex, wgtB);
    }

    // From https://stackoverflow.com/a/1626175/199364.
    public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
    {
      double max = Math.Max(color.R, Math.Max(color.G, color.B));
      double min = Math.Min(color.R, Math.Min(color.G, color.B));

      hue = color.Hue;
      saturation = max == 0 ? 0 : 1d - 1d * min / max;
      value = max / 255d;
    }

    // From https://stackoverflow.com/a/1626175/199364.
    public static Color ColorFromHSV(double hue, double saturation, double value)
    {
      int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
      double f = hue / 60 - Math.Floor(hue / 60);

      value = value * 255;
      int v = Convert.ToInt32(value);
      int p = Convert.ToInt32(value * (1 - saturation));
      int q = Convert.ToInt32(value * (1 - f * saturation));
      int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

      if (hi == 0)
        return Color.FromRgba(255, v, t, p);
      else if (hi == 1)
        return Color.FromRgba(255, q, v, p);
      else if (hi == 2)
        return Color.FromRgba(255, p, v, t);
      else if (hi == 3)
        return Color.FromRgba(255, p, q, v);
      else if (hi == 4)
        return Color.FromRgba(255, t, p, v);
      else
        return Color.FromRgba(255, v, p, q);
    }
    #endregion


    public double H { get; set; }
    public double S { get; set; }
    public double V { get; set; }

    // c'tors
    public HSV()
    {
    }
    public HSV(double h, double s, double v)
    {
      H = h;
      S = s;
      V = v;
    }

    public Color ToColor()
    {
      return ColorFromHSV(H, S, V);
    }

    public HSV Lerp(HSV b, double wgtB)
    {
      return new HSV(
          MathExt.Lerp(H, b.H, wgtB),
          MathExt.Lerp(S, b.S, wgtB),
          MathExt.Lerp(V, b.V, wgtB));
    }

    // Returns "wgtB", such that goalHue = Lerp(H, b.H, wgtB).
    // If a and b have same S and V, then this is a measure of
    // how far to move along segment (a, b), to reach goalHue.
    public double WgtFromHue(HSV b, double goalHue)
    {
      return MathExt.Lerp(H, b.H, goalHue);
    }
    // Returns "wgtB", such that goalValue = Lerp(V, b.V, wgtB).
    public double WgtFromValue(HSV b, double goalValue)
    {
      return MathExt.Lerp(V, b.V, goalValue);
    }
  }

  public static class MathExt
  {
    public static double Lerp(double a, double b, double wgtB)
    {
      return a + wgtB * (b - a);
    }

    // Converse of Lerp:
    // returns "wgtB", such that
    //   result == lerp(a, b, wgtB)
    public static double WgtFromResult(double a, double b, double result)
    {
      double denominator = b - a;

      if (Math.Abs(denominator) < 0.00000001)
      {
        if (Math.Abs(result - a) < 0.00000001)
          // Any value is "valid"; return the average.
          return 0.5;

        // Unsolvable - no weight can return this result.
        return double.NaN;
      }

      double wgtB = (result - a) / denominator;
      return wgtB;
    }
  }
}
