namespace DocJc.Model.ViewModel
{
    public class SelectableItem: ObservableViewModel
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
