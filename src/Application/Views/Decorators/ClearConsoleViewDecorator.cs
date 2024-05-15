using Journal.Application.Abstractions;
using Journal.Application.Panels.States.Abstractions;

namespace Journal.Application.Views.Decorators;

public class ClearConsoleViewDecorator : IPanelState
{
    private readonly IView _view;
    public ClearConsoleViewDecorator(IView view)
    {
        _view = view;
    }
    public void Render()
    {
        Console.Clear();
        _view.Render();
    }
}