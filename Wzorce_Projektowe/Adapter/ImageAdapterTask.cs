using System;

// Testowanie implementacji
IImage jpeg = new JpegImage();
jpeg.Load("photo.jpg");
jpeg.Display();

Console.WriteLine();

IImage png = new PngImage();
png.Load("logo.png");
png.Display();

Console.WriteLine();


GifHandler gifHandler = new GifHandler();
IImage gifAdapter = new GifImageAdapter(gifHandler);
gifAdapter.Load("animation.gif");
gifAdapter.Display();

public interface IImage
{
    void Load(string filename);
    void Display();
}

public class JpegImage : IImage
{
    public void Load(string filename)
    {
        Console.WriteLine($"Loading JPEG image from file: {filename}");
    }

    public void Display()
    {
        Console.WriteLine("Displaying JPEG image.");
    }
}

public class PngImage : IImage
{
    public void Load(string filename)
    {
        Console.WriteLine($"Loading PNG image from file: {filename}");
    }

    public void Display()
    {
        Console.WriteLine("Displaying PNG image.");
    }
}

public class GifHandler
{
    public void OpenFile(string filename)
    {
        Console.WriteLine($"Opening GIF file: {filename} using GifHandler");
    }

    public void RenderGif()
    {
        Console.WriteLine("Rendering GIF animation using GifHandler.");
    }
}

// Adapter
public class GifImageAdapter : IImage
{
    private readonly GifHandler _gifHandler;

    public GifImageAdapter(GifHandler gifHandler)
    {
        _gifHandler = gifHandler;
    }

    public void Load(string filename)
    {
        _gifHandler.OpenFile(filename);
    }

    public void Display()
    {
        _gifHandler.RenderGif();
    }
}
