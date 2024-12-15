using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    private readonly Action _execute; // 실행할 메서드
    private readonly Func<bool> _canExecute; // 실행 가능 여부를 확인하는 메서드

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute ?? (() => true); // 기본값은 항상 실행 가능
    }

    public bool CanExecute(object parameter) => _canExecute();

    public void Execute(object parameter) => _execute();

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}
