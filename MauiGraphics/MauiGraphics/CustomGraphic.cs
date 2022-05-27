using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiGraphics
{
    public class CustomGraphic : IDrawable, INotifyPropertyChanged
    {
        #region Properties
        public Color PathColor { get; set; } = new Color(62, 176, 255);
        public Color BackgroundColor { get; set; } = new Color(139, 216, 255, 100);
        public string Unit { get; set; }


        #region _currentValue => CurrentValue
        private string _currentValue;
        public string CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #endregion


        public CustomGraphic(IEnumerable<float> values, float? selectedX)
        {
            _path = new PathF();
            _values = values;
            _selectedX = selectedX;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            _currentCanvas = canvas;
            _currentDirtyRect = dirtyRect;




            DrawPath();
            if (_selectedX != null)
                DrawCursor();

            DrawHorizontalyMarker(1.5f);
            DrawHorizontalyMarker(3);
        }

        private void DrawHorizontalyMarker(float ratio = 2)
        {
            _currentCanvas.StrokeSize = 2;
            _currentCanvas.StrokeDashPattern = new float[] { 5, 8 };
            _currentCanvas.DrawLine(0, _currentDirtyRect.Height / ratio, _currentDirtyRect.Width, _currentDirtyRect.Height / ratio);

            var currentValue = Math.Round((((_currentDirtyRect.Height - _currentDirtyRect.Height / ratio) / (_currentDirtyRect.Height / _values.Max()))), 2).ToString();

            _currentCanvas.FontSize = 18;
            _currentCanvas.FontColor = PathColor;
            _currentCanvas.DrawString($"{currentValue}{Unit}", 2, (_currentDirtyRect.Height / ratio) - 5, HorizontalAlignment.Left);
        }

        /// <summary>
        /// Draw graphic path
        /// </summary>
        private void DrawPath()
        {
            _currentCanvas.StrokeColor = PathColor;
            _currentCanvas.StrokeSize = 3;

            _currentCanvas.StrokeColor = Colors.White;
            _path.LineTo(0, _currentDirtyRect.Height);
            _currentCanvas.StrokeColor = PathColor;



            for (int i = 0; i < _values.Count(); i++)
            {
                var y = _currentDirtyRect.Height - (_currentDirtyRect.Height / _values.Max()) * _values.ToArray()[i]; // scale * value
                var x = (_currentDirtyRect.Width / (_values.Count() - 1)) * i;
                _path.LineTo(x, y);
            }
            _path.LineTo(_currentDirtyRect.Width, _currentDirtyRect.Height);

            _currentCanvas.FillColor = BackgroundColor;
            _currentCanvas.FillPath(_path);
            _currentCanvas.DrawPath(_path);
        }

        /// <summary>
        /// Draw verticaly line and cursor
        /// </summary>
        private void DrawCursor()
        {
            _currentCanvas.StrokeColor = PathColor;
            _currentCanvas.StrokeSize = 2;
            _currentCanvas.StrokeDashPattern = new float[] { 2, 3 };

            var selectedLinePoint = _path.Points.Skip(1).SkipLast(1).OrderBy(point => Math.Abs(_selectedX.Value - point.X)).FirstOrDefault();

            _currentCanvas.FillColor = PathColor;
            _currentCanvas.FillCircle(selectedLinePoint, 10);
            _currentCanvas.DrawLine(selectedLinePoint, new Point(selectedLinePoint.X, _currentDirtyRect.Height));

            CurrentValue = Math.Round((((_currentDirtyRect.Height - selectedLinePoint.Y) / (_currentDirtyRect.Height / _values.Max()))), 2).ToString();
        }


        #region privates
        private IEnumerable<float> _values { get; set; }
        private RectF _currentDirtyRect;
        private ICanvas _currentCanvas;
        private PathF _path;
        private float? _selectedX;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
