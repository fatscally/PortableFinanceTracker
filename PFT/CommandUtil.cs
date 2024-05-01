namespace PFT.WpfUtils
{

    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Input;

    public class CommandWrapper : ICommand
    {
        // Public.

        public CommandWrapper(PFT.ViewModels.MvvmUtlis.ICommand source)
        {
            _source = source;
            _source.CanExecuteChanged += OnSource_CanExecuteChanged;
            CommandManager.RequerySuggested += OnCommandManager_RequerySuggested;
        }

        public void Execute(object parameter)
        {
            _source.Execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _source.CanExecute(parameter);
        }

        public event System.EventHandler CanExecuteChanged = delegate { };


        // Implementation.

        private void OnSource_CanExecuteChanged(object sender, EventArgs args)
        {
            CanExecuteChanged(sender, args);
        }

        private void OnCommandManager_RequerySuggested(object sender, EventArgs args)
        {
            CanExecuteChanged(sender, args);
        }

        private readonly PFT.ViewModels.MvvmUtlis.ICommand _source;
    }




    public class CommandConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new CommandWrapper((PFT.ViewModels.MvvmUtlis.ICommand)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }

    public class CommandBindingExtension : Binding
    {
        public CommandBindingExtension(string path)
            : base(path)
        {
            Converter = new CommandConverter();
        }
    }

}