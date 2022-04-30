﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using ColorPicker.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace ColorPicker.Controls
{
  public partial class ColorPicker : SKCanvasView
  {
    /// <summary>
    /// Occurs when the Picked Color changes
    /// </summary>
    public event EventHandler<Color> PickedColorChanged;
    private SKCanvas SKCanvas { get; set; }


    public static readonly BindableProperty PickedColorProperty
      = BindableProperty.Create(
        propertyName: nameof(PickedColor),
        returnType: typeof(Color),
        declaringType: typeof(ColorPicker),
        defaultValue: Color.Green,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: OnColorChanged);

    private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
      ColorPicker control = (ColorPicker)bindable;
      control.PickedColor = (Color)newValue;
    }

    /// <summary>
    /// Get the current Picked Color
    /// </summary>
    public int PointerStrokeWidth
    {
      get { return (int)GetValue(PointerStrokeWidthProperty); }
      set { SetValue(PointerStrokeWidthProperty, value); }
    }

    public static readonly BindableProperty PointerStrokeWidthProperty
      = BindableProperty.Create(
            propertyName: nameof(PointerStrokeWidth),
            returnType: typeof(int),
            declaringType: typeof(ColorPicker),
            defaultValue: 15);

    /// <summary>
    /// Get the current Picked Color
    /// </summary>
    public Point SelectedPoint
    {
      get { return (Point)GetValue(SelectedPointProperty); }
      set { SetValue(SelectedPointProperty, value); }
    }

    public static readonly BindableProperty SelectedPointProperty
      = BindableProperty.Create(
            propertyName: nameof(SelectedPoint),
            returnType: typeof(Point),
            declaringType: typeof(ColorPicker),
            defaultBindingMode: BindingMode.TwoWay);

    //private static void OnPointChanged(BindableObject bindable, object oldValue, object newValue)
    //{
    //  CPicker control = (CPicker)bindable;
    //  Point newPoint = (Point)newValue;
    //  control.SelectedPoint = new Point(newPoint.X, newPoint.Y);
    //}

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
            propertyName: nameof(GradientColorStyle),
            returnType: typeof(GradientColorStyle),
            declaringType: typeof(ColorPicker),
            defaultValue: GradientColorStyle.ColorsToDarkStyle,
            defaultBindingMode: BindingMode.OneTime, null);

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
            propertyName: nameof(ColorList),
            returnType: typeof(string[]),
            declaringType: typeof(ColorPicker),
            defaultValue: new string[]
            {
              new Color(255, 0, 0).ToHex(), // Red
			        new Color(255, 255, 0).ToHex(), // Yellow
			        new Color(0, 255, 0).ToHex(), // Green (Lime)
			        new Color(0, 255, 255).ToHex(), // Aqua
			        new Color(0, 0, 255).ToHex(), // Blue
			        new Color(255, 0, 255).ToHex(), // Fuchsia
			        new Color(255, 0, 0).ToHex(), // Red
            },
            defaultBindingMode: BindingMode.OneTime, null);

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
            propertyName: nameof(ColorListDirection),
            returnType: typeof(ColorListDirection),
            declaringType: typeof(ColorPicker),
            defaultValue: ColorListDirection.Horizontal,
            defaultBindingMode: BindingMode.OneTime);

    /// <summary>
    /// Sets the Color List flow Direction
    /// </summary>
    public ColorListDirection ColorListDirection
    {
      get { return (ColorListDirection)GetValue(ColorListDirectionProperty); }
      set { SetValue(ColorListDirectionProperty, value); }
    }

    public static readonly BindableProperty PointerDiameterProperty
        = BindableProperty.Create(
            propertyName: nameof(PointerDiameter),
            returnType: typeof(double),
            declaringType: typeof(ColorPicker),
            defaultValue: 0.6,
            defaultBindingMode: BindingMode.OneTime);

    /// <summary>
    /// Sets the Picker Pointer Size
    /// Value must be between 0-1
    /// Calculated against the View Canvas size
    /// </summary>
    public double PointerDiameter
    {
      get { return (double)GetValue(PointerDiameterProperty); }
      set { SetValue(PointerDiameterProperty, value); }
    }

    public ColorPicker()
    {
      EnableTouchEvents = true;

      Touch += (sender, e) =>
      {
        SKSize canvasSize = CanvasSize;

        // Check for each touch point XY position to be inside Canvas
        // Ignore any Touch event ocurred outside the Canvas region 
        if (e.Location.X > 0 && e.Location.X < canvasSize.Width &&
            e.Location.Y > 0 && e.Location.Y < canvasSize.Height)
        {
          //Debug.WriteLine(e.Location.X);
          //Debug.WriteLine(e.Location.Y);

          SelectedPoint = new Point(e.Location.X, e.Location.Y);
          e.Handled = true;

          // update the Canvas as you wish
          InvalidateSurface();
        }
      };
    }

    //private void GetPixelCoordinatesAsync(SKBitmap bitmap)
    //{
    //  Task.Run(async () => await this.GetPixelCoordinates(bitmap));
    //}

    private void GetPixelCoordinates(SKBitmap bitmap)
    {
      //List<SKColor> colorList = new List<SKColor>();
      IEnumerable<PixelCoordinate> pixelList = new List<PixelCoordinate>();

      //for (int y = 0; y < bitmap.Height; y++)
      //{
      //  for (int x = 0; x < bitmap.Width; x++)
      //  {
      //    colorList.Add(bitmap.GetPixel(x, y));
      //  }
      //}

      for (int row = 0; row < bitmap.Height - 1; row++)
      {
        for (int col = 0; col < bitmap.Width - 1; col++)
        {
          SKColor pixel = bitmap.GetPixel(col, row);

          pixelList.Prepend(new PixelCoordinate
          {
            Color = pixel,
            X = col,
            Y = row
          });

          //imgOut.SetPixel(col, row, this.colorWhite);

        }
      }
    }

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
      SKImageInfo skImageInfo = e.Info;
      SKSurface skSurface = e.Surface;
      SKCanvas = skSurface.Canvas;

      int skCanvasWidth = skImageInfo.Width;
      int skCanvasHeight = skImageInfo.Height;

      SKCanvas.Clear();

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
          SKCanvas.DrawPaint(paint);
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
          SKCanvas.DrawPaint(paint);
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
            (int)SelectedPoint.X,
            (int)SelectedPoint.Y);

        // access the color
        touchPointColor = bitmap.GetPixel(0, 0);

        //this.GetPixelCoordinates(bitmap);

        //bitmap.SetPixel(50,50, this.PickedColor.ToSKColor());
      }

      // Painting the Touch point
      using (SKPaint paintTouchPoint = new SKPaint())
      {
        paintTouchPoint.Style = SKPaintStyle.Stroke;
        paintTouchPoint.StrokeWidth = PointerStrokeWidth;
        paintTouchPoint.Color = SKColors.White;
        paintTouchPoint.IsAntialias = true;

        int valueToCalcAgainst = skCanvasWidth > skCanvasHeight ? skCanvasWidth : skCanvasHeight;

        double pointerCircleDiameterUnits = PointerDiameter; // 0.6 (Default)
        pointerCircleDiameterUnits = (float)pointerCircleDiameterUnits / 10f; //  calculate 1/10th of that value
        float pointerCircleDiameter = (float)(valueToCalcAgainst * pointerCircleDiameterUnits);

        // Outer circle of the Pointer (Ring)
        SKCanvas.DrawCircle(
            (float)SelectedPoint.X,
            (float)SelectedPoint.Y,
            pointerCircleDiameter / 2, paintTouchPoint);

        // Draw another circle with picked color
        paintTouchPoint.Color = touchPointColor;
      }

      // Set selected color
      PickedColor = touchPointColor.ToFormsColor();
      PickedColorChanged?.Invoke(this, PickedColor);
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