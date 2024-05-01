using System.Windows.Input;
using PFT.ViewModels;

namespace PFT
{
    public static class CommandRouter
    {
        static CommandRouter()
        {
            IncrementCounter = new RoutedCommand();
            DecrementCounter = new RoutedCommand();
        }

        public static RoutedCommand IncrementCounter { get; private set; }
        public static RoutedCommand DecrementCounter { get; private set; }

        public static void WireMainView(HomeWindow view, VM_Main viewModel)
        {
            if (view == null || viewModel == null) return;

            view.CommandBindings.Add(
                new CommandBinding(
                    IncrementCounter,
                    (λ1, λ2) => viewModel.IncrementCounter(),
                    (λ1, λ2) =>
                    {
                        λ2.CanExecute = true;
                        λ2.Handled = true;
                    }));
            view.CommandBindings.Add(
                new CommandBinding(
                    DecrementCounter,
                    (λ1, λ2) => viewModel.DecrementCounter(),
                    (λ1, λ2) =>
                    {
                        λ2.CanExecute = true;
                        λ2.Handled = true;
                    }));
        }
    }
}