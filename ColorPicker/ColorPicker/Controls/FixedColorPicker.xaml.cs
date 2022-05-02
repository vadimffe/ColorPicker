using ColorPicker.Models;
using System.Collections.ObjectModel;

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
        BindingMode.TwoWay,
        propertyChanged: OnSelectedColorChanged);

    private static void OnSelectedColorChanged(BindableObject bindable, object oldValue, object newValue)
    {
      var control = bindable as FixedColorPicker;
      control.collectionView.SelectedItem = newValue;
    }

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
        BindingMode.TwoWay,
        propertyChanged: OnColorCollectionChanged);

    private static void OnColorCollectionChanged(BindableObject bindable, object oldValue, object newValue)
    {
      var control = bindable as FixedColorPicker;
      control.collectionView.ItemsSource = control.Colors;
    }

    public ObservableCollection<CollectionViewColorModel> Colors
    {
      get { return (ObservableCollection<CollectionViewColorModel>)GetValue(ColorsProperty); }
      set { SetValue(ColorsProperty, value); }
    }

    public FixedColorPicker()
    {
      this.InitializeComponent();

      this.collectionView.SelectionChanged += this.OnCollectionViewSelectionChanged;
    }

    private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var previous = e.PreviousSelection;
      var current = e.CurrentSelection;

      this.SelectedColor = (sender as CollectionView).SelectedItem;
    }
  }
}