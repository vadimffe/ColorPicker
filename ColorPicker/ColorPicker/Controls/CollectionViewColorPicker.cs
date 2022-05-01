using ColorPicker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ColorPicker.Controls
{
  public class CollectionViewColorPicker : CollectionView
  {
    public static readonly BindableProperty ColorsProperty =
      BindableProperty.Create(
        nameof(Colors),
        typeof(List<ListViewColorModel>),
        typeof(CollectionViewColorPicker),
        new List<ListViewColorModel>());

    public List<ListViewColorModel> Colors
    {
      get { return (List<ListViewColorModel>)GetValue(ColorsProperty); }
      set { SetValue(ColorsProperty, value); }
    }

    public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

    public CollectionViewColorPicker()
    {
      this.InitializeColors();

      this.SelectionChanged += this.OnCollectionViewSelectionChanged;
    }

    private void InitializeColors()
    {
      this.Colors = new List<ListViewColorModel> {
          new ListViewColorModel{ Id = 1, Color = "#25c5db" },
          new ListViewColorModel{ Id = 2, Color = "#0098a6" },
          new ListViewColorModel{ Id = 3, Color = "#0e47a1" },
          new ListViewColorModel{ Id = 4, Color = "#1665c1" },
          new ListViewColorModel{ Id = 5, Color = "#039be6" },

          new ListViewColorModel{ Id = 6, Color = "#64b5f6" },
          new ListViewColorModel{ Id = 7, Color = "#ff7000" },
          new ListViewColorModel{ Id = 8, Color = "#ff9f00" },
          new ListViewColorModel{ Id = 9, Color = "#ffb200" },
          new ListViewColorModel{ Id = 10, Color = "#cf9702" },
        };

      this.ItemsSource = this.Colors;
    }

    private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      IReadOnlyList<object> previous = e.PreviousSelection;
      IReadOnlyList<object> current = e.CurrentSelection;


    }
  }
}