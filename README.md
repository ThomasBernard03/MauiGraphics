# MauiGraphics

Ce repository permet de réaliser facilement des graphiques sur la .net Maui.

## Utilisation

![Screenshot_1653643766](https://user-images.githubusercontent.com/67638928/170672476-ad0162b3-8066-4f60-9c17-5a88e09479df.png)
Nous allons reproduire cet exemple

```XML
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MauiGraphics.MainPage">

    <ContentPage.Content>
        <StackLayout>
            <Label
                Text="{Binding CustomGraphic.CurrentValue}"
                HeightRequest="100"
                FontSize="30"
                x:Name="MyLabel"/>
            <GraphicsView
                x:Name="MyGraph"
                Margin="50"
                DragInteraction="MyGraph_DragInteraction"
                HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand"/>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```
MainPage.xaml

```C#
public MainPage()
    {
        BindingContext = this;
        InitializeComponent();
        LoadGraph();
    }

    private void LoadGraph(float? selectedX = null)
    {
        CustomGraphic = new CustomGraphic(_values, selectedX);
        MyGraph.Drawable = CustomGraphic;
    }

    private void MyGraph_DragInteraction(object sender, TouchEventArgs e)
    {
        LoadGraph(e.Touches.FirstOrDefault().X);
    }
```



