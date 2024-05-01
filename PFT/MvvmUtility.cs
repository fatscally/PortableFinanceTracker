using System;

namespace PFT.ViewModels.MvvmUtlis
{

        public interface ICommand
        {
            void Execute(object parameter);
            bool CanExecute(object parameter);

            event EventHandler CanExecuteChanged;
        }



        public class DelegateCommand : ICommand
        {
            public DelegateCommand(Action<object> execute)
                : this(execute, null)
            {

            }

            public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            public void Execute(object parameter)
            {
                _execute(parameter);
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute(parameter);
            }


            public event EventHandler CanExecuteChanged;

            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;
        }

}



