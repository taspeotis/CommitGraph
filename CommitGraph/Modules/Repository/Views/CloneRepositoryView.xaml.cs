using System.ComponentModel.Composition;
using CommitGraph.Modules.Repository.ViewModels;

namespace CommitGraph.Modules.Repository.Views
{
    [Export]
    public partial class CloneRepositoryView
    {
        public CloneRepositoryView()
        {
            InitializeComponent();
        }

        [Import]
        public CloneRepositoryViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}