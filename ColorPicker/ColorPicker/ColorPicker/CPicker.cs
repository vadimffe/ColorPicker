using System;
using System.Diagnostics;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace CPicker.Controls.CPicker
{
  public partial class CPicker : SKCanvasView
  {
    /// <summary>
    /// Occurs when the Picked Color changes
    /// </summary>
    public event EventHandler<Color> PickedColorChanged;
    private SKCanvas SKCanvas { get; set; }
    private SKPoint _lastTouchPoint = new SKPoint();
    private SKColor _selectedColor;

    public static readonly BindableProperty PickedColorProperty
      = BindableProperty.Create(
        propertyName: nameof(PickedColor),
        returnType: typeof(Color),
        declaringType: typeof(CPicker),
        defaultValue: Color.Green,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: OnColorChanged);

    private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
      CPicker control = (CPicker)bindable;
      //control.PickedColor = (Color)newValue;

      Color col = (Color)newValue;

      SKColor colr = col.ToSKColor();

      control._selectedColor = colr;

      colr.ToHsv(out var h, out var s, out var v);

      Debug.WriteLine(h);
      Debug.WriteLine(s);
      Debug.WriteLine(v);
    }

    /// <summary>
    /// Get the current Picked Color
    /// </summary>
    public float PointX
    {
      get { return (float)GetValue(PointXProperty); }
      set { SetValue(PointXProperty, value); }
    }

    public static readonly BindableProperty PointXProperty
      = BindableProperty.Create(
            nameof(PointX),
            typeof(float),
            typeof(CPicker));

    /// <summary>
    /// Get the current Picked Color
    /// </summary>
    public float PointY
    {
      get { return (float)GetValue(PointYProperty); }
      set { SetValue(PointYProperty, value); }
    }

    public static readonly BindableProperty PointYProperty
      = BindableProperty.Create(
            nameof(PointY),
            typeof(float),
            typeof(CPicker));

    /// <summary>
    /// Get the current Picked Color
    /// </summary>
    public Color PickedColor
    {
      get { return (Color)GetValue(PickedColorProperty); }
      set { SetValue(PickedColorProperty, value); }
    }

    public static readonly BindableProperty GradientColorStyleProperty
      = BindableProperty.Create(
            nameof(GradientColorStyle),
            typeof(GradientColorStyle),
            typeof(CPicker),
            GradientColorStyle.ColorsToDarkStyle,
            BindingMode.OneTime, null);

    /// <summary>
    /// Set the Color Spectrum Gradient Style
    /// </summary>
    public GradientColorStyle GradientColorStyle
    {
      get { return (GradientColorStyle)GetValue(GradientColorStyleProperty); }
      set { SetValue(GradientColorStyleProperty, value); }
    }

    public static readonly BindableProperty ColorListProperty
      = BindableProperty.Create(
            nameof(ColorList),
            typeof(string[]),
            typeof(CPicker),
            new string[]
            {
              new Color(255, 0, 0).ToHex(), // Red
			        new Color(255, 255, 0).ToHex(), // Yellow
			        new Color(0, 255, 0).ToHex(), // Green (Lime)
			        new Color(0, 255, 255).ToHex(), // Aqua
			        new Color(0, 0, 255).ToHex(), // Blue
			        new Color(255, 0, 255).ToHex(), // Fuchsia
			        new Color(255, 0, 0).ToHex(), // Red
            },
            BindingMode.OneTime, null);

    /// <summary>
    /// Sets the Color List
    /// </summary>
    public string[] ColorList
    {
      get { return (string[])GetValue(ColorListProperty); }
      set { SetValue(ColorListProperty, value); }
    }

    public static readonly BindableProperty ColorListDirectionProperty
        = BindableProperty.Create(
            nameof(ColorListDirection),
            typeof(ColorListDirection),
            typeof(CPicker),
            ColorListDirection.Horizontal,
            BindingMode.OneTime);

    /// <summary>
    /// Sets the Color List flow Direction
    /// </summary>
    public ColorListDirection ColorListDirection
    {
      get { return (ColorListDirection)GetValue(ColorListDirectionProperty); }
      set { SetValue(ColorListDirectionProperty, value); }
    }

    public static readonly BindableProperty PointerCircleDiameterUnitsProperty
        = BindableProperty.Create(
            nameof(PointerCircleDiameterUnits),
            typeof(double),
            typeof(CPicker),
            0.6,
            BindingMode.OneTime);

    /// <summary>
    /// Sets the Picker Pointer Size
    /// Value must be between 0-1
    /// Calculated against the View Canvas size
    /// </summary>
    public double PointerCircleDiameterUnits
    {
      get { return (double)GetValue(PointerCircleDiameterUnitsProperty); }
      set { SetValue(PointerCircleDiameterUnitsProperty, value); }
    }

    public static readonly BindableProperty PointerCircleBorderUnitsProperty
        = BindableProperty.Create(
            nameof(PointerCircleBorderUnits),
            typeof(double),
            typeof(CPicker),
            0.3,
            BindingMode.OneTime);

    /// <summary>
    /// Sets the Picker Pointer Border Size
    /// Value must be between 0-1
    /// Calculated against pixel size of Picker Pointer
    /// </summary>
    public double PointerCircleBorderUnits
    {
      get { return (double)GetValue(PointerCircleBorderUnitsProperty); }
      set { SetValue(PointerCircleBorderUnitsProperty, value); }
    }

    public CPicker()
    {
      this.EnableTouchEvents = true;

      this.Touch += (sender, e) =>
      {
        _lastTouchPoint = e.Location;

        SKSize canvasSize = this.CanvasSize;

        this.PointX = e.Location.X;
        this.PointY = e.Location.Y;

        // Check for each touch point XY position to be inside Canvas
        // Ignore any Touch event ocurred outside the Canvas region 
        if (e.Location.X > 0 && e.Location.X < canvasSize.Width &&
            e.Location.Y > 0 && e.Location.Y < canvasSize.Height)
        {
          Debug.WriteLine(e.Location.X);
          Debug.WriteLine(e.Location.Y);

          e.Handled = true;

          // update the Canvas as you wish
          this.InvalidateSurface();
        }
      };
    }

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
      SKImageInfo skImageInfo = e.Info;
      SKSurface skSurface = e.Surface;
      this.SKCanvas = skSurface.Canvas;

      int skCanvasWidth = skImageInfo.Width;
      int skCanvasHeight = skImageInfo.Height;

      this.SKCanvas.Clear();

      // Draw gradient rainbow Color spectrum
      using (SKPaint paint = new SKPaint())
      {
        paint.IsAntialias = true;

        System.Collections.Generic.List<SKColor> colors = new System.Collections.Generic.List<SKColor>();
        ColorList.ForEach((color) => { colors.Add(Color.FromHex(color).ToSKColor()); });

        // create the gradient shader between Colors
        using (SKShader shader = SKShader.CreateLinearGradient(
            new SKPoint(0, 0),
            ColorListDirection == ColorListDirection.Horizontal ?
                new SKPoint(skCanvasWidth, 0) : new SKPoint(0, skCanvasHeight),
            colors.ToArray(),
            null,
            SKShaderTileMode.Clamp))
        {
          paint.Shader = shader;
          this.SKCanvas.DrawPaint(paint);
        }
      }

      // Draw darker gradient spectrum
      using (SKPaint paint = new SKPaint())
      {
        paint.IsAntialias = true;

        // Initiate the darkened primary color list
        SKColor[] colors = GetGradientOrder();

        // create the gradient shader 
        using (SKShader shader = SKShader.CreateLinearGradient(
            new SKPoint(0, 0),
            ColorListDirection == ColorListDirection.Horizontal ?
                new SKPoint(0, skCanvasHeight) : new SKPoint(skCanvasWidth, 0),
            colors,
            null,
            SKShaderTileMode.Clamp))
        {
          paint.Shader = shader;
          this.SKCanvas.DrawPaint(paint);
        }
      }

      // Picking the Pixel Color values on the Touch Point

      // Represent the color of the current Touch point
      SKColor touchPointColor;

      // Efficient and fast
      // https://forums.xamarin.com/discussion/92899/read-a-pixel-info-from-a-canvas
      // create the 1x1 bitmap (auto allocates the pixel buffer)
      using (SKBitmap bitmap = new SKBitmap(skImageInfo))
      {
        // get the pixel buffer for the bitmap
        IntPtr dstpixels = bitmap.GetPixels();

        // read the surface into the bitmap
        skSurface.ReadPixels(skImageInfo,
            dstpixels,
            skImageInfo.RowBytes,
            (int)_lastTouchPoint.X, (int)_lastTouchPoint.Y);

        // access the color
        touchPointColor = bitmap.GetPixel(0, 0);
      }

      // Painting the Touch point
      using (SKPaint paintTouchPoint = new SKPaint())
      {
        paintTouchPoint.Style = SKPaintStyle.Fill;
        paintTouchPoint.Color = SKColors.White;
        paintTouchPoint.IsAntialias = true;

        int valueToCalcAgainst = skCanvasWidth > skCanvasHeight ? skCanvasWidth : skCanvasHeight;

        double pointerCircleDiameterUnits = PointerCircleDiameterUnits; // 0.6 (Default)
        pointerCircleDiameterUnits = (float)pointerCircleDiameterUnits / 10f; //  calculate 1/10th of that value
        float pointerCircleDiameter = (float)(valueToCalcAgainst * pointerCircleDiameterUnits);

        // Outer circle of the Pointer (Ring)
        this.SKCanvas.DrawCircle(
            _lastTouchPoint.X,
            _lastTouchPoint.Y,
            pointerCircleDiameter / 2, paintTouchPoint);

        // Draw another circle with picked color
        paintTouchPoint.Color = touchPointColor;

        double pointerCircleBorderWidthUnits = PointerCircleBorderUnits; // 0.3 (Default)
        float pointerCircleBorderWidth = (float)pointerCircleDiameter * (float)pointerCircleBorderWidthUnits; // Calculate against Pointer Circle

        // Inner circle of the Pointer (Ring)
        this.SKCanvas.DrawCircle(
            _lastTouchPoint.X,
            _lastTouchPoint.Y,
            (pointerCircleDiameter - pointerCircleBorderWidth) / 2, paintTouchPoint);
      }

      // Set selected color
      this.PickedColor = _selectedColor.ToFormsColor();
      this.PickedColorChanged?.Invoke(this, this.PickedColor);
    }

    private SKColor[] GetGradientOrder()
    {
      if (GradientColorStyle == GradientColorStyle.ColorsOnlyStyle)
      {
        return new SKColor[]
        {
          SKColors.Transparent,
        };
      }
      else if (GradientColorStyle == GradientColorStyle.ColorsToDarkStyle)
      {
        return new SKColor[]
        {
          SKColors.Transparent,
          SKColors.Black,
        };
      }
      else if (GradientColorStyle == GradientColorStyle.DarkToColorsStyle)
      {
        return new SKColor[]
        {
          SKColors.Black,
          SKColors.Transparent,
        };
      }
      else if (GradientColorStyle == GradientColorStyle.ColorsToLightStyle)
      {
        return new SKColor[]
        {
          SKColors.Transparent,
          SKColors.White,
        };
      }
      else if (GradientColorStyle == GradientColorStyle.LightToColorsStyle)
      {
        return new SKColor[]
        {
          SKColors.White,
          SKColors.Transparent,
        };
      }
      else if (GradientColorStyle == GradientColorStyle.LightToColorsToDarkStyle)
      {
        return new SKColor[]
        {
          SKColors.White,
          SKColors.Transparent,
          SKColors.Black,
        };
      }
      else if (GradientColorStyle == GradientColorStyle.DarkToColorsToLightStyle)
      {
        return new SKColor[]
        {
          SKColors.Black,
          SKColors.Transparent,
          SKColors.White,
        };
      }
      else
      {
        return new SKColor[]
        {
          SKColors.Transparent,
          SKColors.Black,
        };
      }
    }
  }

  public enum GradientColorStyle
  {
    ColorsOnlyStyle,
    ColorsToDarkStyle,
    DarkToColorsStyle,
    ColorsToLightStyle,
    LightToColorsStyle,
    LightToColorsToDarkStyle,
    DarkToColorsToLightStyle,
  }

  public enum ColorListDirection
  {
    Horizontal,
    Vertical,
  }
}