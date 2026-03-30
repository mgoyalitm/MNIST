using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaSharp.Views.WPF;
using System.Windows;
using static MNIST.Utilities.FontManager;

namespace MNIST.Controls;

public class FontCanvas : SKElement
{
    
    public const int CharSize = 24;
    public const int CharSpacing = 2;
    public const int CharBoxSize = CharSize + CharSpacing;

    private const float CanvasWidth = CharBoxSize * CharacterCount;
    private const float CanvasHeight = CharBoxSize * RotationSteps;


    public static readonly DependencyProperty SelectedFontProperty = DependencyProperty.Register(nameof(SelectedFont), typeof(FontModel), typeof(FontCanvas), new PropertyMetadata(null, OnFontSelectionChanged));
    
    public FontCanvas()
        : base()
    {
        PaintSurface += DrawMontage;
        SizeChanged += (s, e) => 
        {
            SetDimensions();
        };
        Loaded += (s, e) => SetDimensions();
    }

    
    public FontModel SelectedFont
    {
        get => (FontModel)GetValue(SelectedFontProperty); 
        set => SetValue(SelectedFontProperty, value);
    }

    public void SetDimensions()
    {
        PresentationSource source = PresentationSource.FromVisual(this);
        double scaleX = source.CompositionTarget.TransformToDevice.M11;
        double scaley = source.CompositionTarget.TransformToDevice.M22;

        Width = CanvasWidth / scaleX;
        Height = CanvasHeight / scaley;
        InvalidateVisual();
    }

    private static void OnFontSelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is SKElement element)
        {
            element.InvalidateVisual();
        }
    }

    private void DrawMontage(object sender, SKPaintSurfaceEventArgs e)
    {
        SKCanvas canvas = e.Surface.Canvas;
        canvas.Clear(SKColors.Transparent);

        if (SelectedFont is null)
        {
            return;
        }

        using SKTypeface typeface = SKTypeface.FromFile(SelectedFont.Path);

        using SKFont font = new(typeface, CharSize);
        using SKPaint paint = new()
        {
            Color = SKColors.White,
            IsAntialias = true
        };

        float offset_rotation = SelectedFont.Style is Model.FontStyle.Italic ? -7.5f : 0f;

        for (int x = 0; x < CharacterCount; x++)
        {
            char character = Characters[x];
            float x_cord = (x + 0.5f) * CharBoxSize;

            for (int y = 0; y < RotationSteps; y++)
            {
                canvas.Save();
                float rotation = MinRotation + y * RotationStepSize + offset_rotation;
                float y_cord = (y + 0.75f) * CharBoxSize;
                canvas.Translate(x_cord, y_cord);
                canvas.RotateDegrees(rotation);
                canvas.DrawText(character.ToString(), 0, 0, SKTextAlign.Center, font, paint);
                canvas.Restore();
                canvas.Save();
            }
        }

    }

}
