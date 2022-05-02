using ColorPicker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColorPicker.Controls
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class FixedColorPicker : ContentView
  {
    public static readonly BindableProperty SelectedColorProperty =
      BindableProperty.Create(
        nameof(SelectedColor),
        typeof(object),
        typeof(FixedColorPicker),
        new CollectionViewColorModel(),
        BindingMode.TwoWay);

    public object SelectedColor
    {
      get { return (object)GetValue(SelectedColorProperty); }
      set { SetValue(SelectedColorProperty, value); }
    }

    public static readonly BindableProperty ColorsProperty =
      BindableProperty.Create(
        nameof(Colors),
        typeof(ObservableCollection<CollectionViewColorModel>),
        typeof(FixedColorPicker),
        new ObservableCollection<CollectionViewColorModel>(),
        BindingMode.TwoWay);

    public ObservableCollection<CollectionViewColorModel> Colors
    {
      get { return (ObservableCollection<CollectionViewColorModel>)GetValue(ColorsProperty); }
      set { SetValue(ColorsProperty, value); }
    }

    public FixedColorPicker()
    {
      this.InitializeComponent();

      this.InitializeColors();

      this.collectionView.SelectionChanged += this.OnCollectionViewSelectionChanged;

      this.collectionView.ItemsSource = this.Colors;
    }

    private void InitializeColors()
    {
      this.Colors = new ObservableCollection<CollectionViewColorModel> {
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
    }

    private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var previous = e.PreviousSelection;
      var current = e.CurrentSelection;

      this.SelectedColor = (sender as CollectionView).SelectedItem;
    }
  }
}