using System.Windows.Input;
using Humanizer;
using Prism.Commands;

namespace CommitGraph.ViewModels
{
    public abstract class FlyoutViewModelBase : ViewModelBase
    {
        protected FlyoutViewModelBase()
        {
            var viewModelName = GetType().Name;
            const string viewModel = "ViewModel";

            if (viewModelName.EndsWith(viewModel))
                viewModelName = viewModelName.Substring(0, viewModelName.Length - viewModel.Length);

            Header = viewModelName.Humanize(LetterCasing.Title);

            CloseCommand = new DelegateCommand(Close);
            ShowCommand = new DelegateCommand(Show);
        }

        public ICommand CloseCommand { get; }

        public string Header { get; }

        public bool IsOpen { get; private set; }

        public ICommand ShowCommand { get; }

        private void Close()
        {
            IsOpen = false;
        }

        private void Show()
        {
            IsOpen = true;
        }
    }
}