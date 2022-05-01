using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;

namespace ColorPicker.Controls
{
  public class CirclePicker : SKCanvasView
  {
    public event EventHandler<ColorChangedEventArgs> ColorChanged;

    private List<ColorModel> ColorPicks { get; set; }
    private bool _colorPicksInitialized;
    private ColorModel _pickedColor;

    private int ColorsPerRow = 5;
    private int CanvasPadding = 5;
    private bool _colorChanged;

    private readonly SKPaint _clrPickPaint = new SKPaint
    {
      Style = SKPaintStyle.Fill,
      IsAntialias = true
    };

    private readonly SKPaint _pickedClrPaint = new SKPaint
    {
      Style = SKPaintStyle.Stroke,
      StrokeWidth = 5,
      IsAntialias = true,
    };

    public CirclePicker()
    {
      this.InitializeColors();

      this.EnableTouchEvents = true;

      this.Touch += (sender, e) =>
      {
        if (e.ActionType == SKTouchAction.Pressed)
        {
          // get the sk point pixel
          SKPoint pnt = this.ConvertToPixel(e.Location);

          // loop through all colors
          foreach (ColorModel cp in this.ColorPicks)
          {
            // check if selecting a color
            if (cp.IsTouched(pnt))
            {
              this._colorChanged = true;
              this._pickedColor = cp;
              break; // get out of loop
            }
          }
          this.InvalidateSurface();
        }
      };
    }

    private void InitializeColors()
    {
      //this.ColorPicks = new List<ColorModel>();

      //for (int i = 0; i < 30; i++)
      //{
      //  this.ColorPicks.Add(new ColorModel(i, String.Format("#{0:X6}", i * 1000000)));
      //}

      this.ColorPicks = new List<ColorModel> {
            new ColorModel(1, "#25c5db"),
            new ColorModel(2, "#0098a6"),
            new ColorModel(3, "#0e47a1"),
            new ColorModel(4, "#1665c1"),
            new ColorModel(5, "#039be6"),

            new ColorModel(6, "#64b5f6"),
            new ColorModel(7, "#ff7000"),
            new ColorModel(8, "#ff9f00"),
            new ColorModel(9, "#ffb200"),
            new ColorModel(10, "#cf9702"),

            new ColorModel(11, "#8c6e63"),
            new ColorModel(12, "#6e4c42"),
            new ColorModel(13, "#d52f31"),
            new ColorModel(14, "#ff1643"),
            new ColorModel(15, "#f44236"),

            new ColorModel(16, "#ec407a"),
            new ColorModel(17, "#ad1457"),
            new ColorModel(18, "#6a1b9a"),
            new ColorModel(19, "#ab48bf"),
            new ColorModel(20, "#b968c7"),

            new ColorModel(21, "#00695b"),
            new ColorModel(22, "#00887a"),
            new ColorModel(23, "#4cb6ac"),
            new ColorModel(24, "#307c32"),
            new ColorModel(25, "#43a047"),

            new ColorModel(26, "#64dd16"),
            new ColorModel(27, "#222222"),
            new ColorModel(28, "#5f7c8c"),
            new ColorModel(29, "#b1bec6"),
            new ColorModel(30, "#465a65"),
        };
    }

    protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
    {
      SKImageInfo info = e.Info;
      SKSurface surface = e.Surface;
      SKCanvas canvas = surface.Canvas;

      canvas.Clear();

      if (!this._colorPicksInitialized)
      {
        this.InitializeColorPicks(info.Width);
      }

      // draw the colors
      foreach (ColorModel cp in this.ColorPicks)
      {
        this._clrPickPaint.Color = cp.Color.ToSKColor();
        canvas.DrawCircle(cp.Position.X, cp.Position.Y, cp.Radius, this._clrPickPaint);
      }

      // check if there is a selected color
      if (this._pickedColor == null) { return; }

      // draw the highlight for the picked color
      this._pickedClrPaint.Color = this._pickedColor.Color.ToSKColor();
      canvas.DrawCircle(this._pickedColor.Position.X, this._pickedColor.Position.Y, this._pickedColor.Radius + 10, this._pickedClrPaint);

      if (this._colorChanged)
      {
        this.ColorChanged?.Invoke(this, new ColorChangedEventArgs(this._pickedColor.Color));
        this._colorChanged = false;
      }
    }

    private void InitializeColorPicks(int skImageWidth)
    {
      int contentWidth = skImageWidth - (this.CanvasPadding * 2);
      int colorWidth = contentWidth / this.ColorsPerRow;

      SKPoint sp = new SKPoint((colorWidth / 2) + this.CanvasPadding, (colorWidth / 2) + this.CanvasPadding);
      int col = 1;
      int row = 1;
      int radius = (colorWidth / 2) - 10;

      foreach (ColorModel cp in this.ColorPicks)
      {
        if (col > this.ColorsPerRow)
        {
          col = 1;
          row += 1;
        }

        // calc current position
        float x = sp.X + (colorWidth * (col - 1));
        float y = sp.Y + (colorWidth * (row - 1));

        cp.Radius = radius;
        cp.Position = new SKPoint(x, y);
        col += 1;
      }

      this._colorPicksInitialized = true;
    }

    private SKPoint ConvertToPixel(SKPoint pt)
    {
      return new SKPoint((float)(this.CanvasSize.Width * pt.X / this.Width),
          (float)(this.CanvasSize.Height * pt.Y / this.Height));
    }
  }
}
