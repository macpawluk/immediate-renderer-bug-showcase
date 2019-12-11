using Avalonia;
using Avalonia.Logging.Serilog;
using Avalonia.Rendering;

namespace AvaloniaApplication
{
    internal class Program
    {
        public static void Main(string[] args) => BuildAvaloniaApp().Start<MainWindow>();

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToDebug()
                // Please uncomment this line to check the fix for the bug found.
                //.AfterSetup(b => OverrideRenderer())
                .With(new Win32PlatformOptions
                {
                    UseDeferredRendering = false
                });

        private static void OverrideRenderer()
        {
            var locator = AvaloniaLocator.CurrentMutable;
            locator
                .Bind<IRendererFactory>()
                .ToConstant(new FixedRendererFactory());
        }
    }

    public class FixedRendererFactory : IRendererFactory
    {
        public IRenderer Create(IRenderRoot root, IRenderLoop renderLoop)
            => new ImmediateRenderer(root);
    }
}