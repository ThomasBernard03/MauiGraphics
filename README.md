# MauiGraphics

Ce repository permet de réaliser facilement des graphiques sur la .net Maui.

## Utilisation

![image](https://user-images.githubusercontent.com/67638928/170673424-fda4b694-7c45-4ef5-a448-4675c49a782d.png)

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
Ce code permet alors de reproduire l'exemple ci-dessus.

![image](https://user-images.githubusercontent.com/67638928/170673251-aef07aac-6608-449b-a9a9-11748cad5c29.png)

On peut aussi changer la couleur du graphique avec ces paramètres : 
```C#
CustomGraphic.PathColor = Colors.Red;
CustomGraphic.BackgroundColor = Colors.Bisque;
```



