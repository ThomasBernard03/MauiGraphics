using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiGraphics;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    private IEnumerable<float> _values = new List<float>() { 59, 60.4f, 58, 61, 63, 62 };


    private CustomGraphic _customGraphic;
    public CustomGraphic CustomGraphic
    {
        get => _customGraphic;
        set
        {
            NotifyPropertyChanged();
            _customGraphic = value;
        }
    }


    public MainPage()
    {
        BindingContext = this;
        InitializeComponent();
        LoadGraph();
    }

    private void LoadGraph(float? selectedX = null)
    {
        CustomGraphic = new CustomGraphic(_values, selectedX);
        CustomGraphic.Unit = "kg";
        MyGraph.Drawable = CustomGraphic;
    }

    private void MyGraph_DragInteraction(object sender, TouchEventArgs e)
    {
        LoadGraph(e.Touches.FirstOrDefault().X);
    }


    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}

