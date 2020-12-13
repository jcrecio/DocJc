namespace DocJc.ViewModels
{
    using CommonServiceLocator;

    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            AutoFacContainer.Initialize();
        }

        public DiagnosisSearchViewModel DiagnosisSearchViewModel 
            => ServiceLocator.Current.GetInstance<DiagnosisSearchViewModel>();

        public LoginViewModel LoginViewModel
            => ServiceLocator.Current.GetInstance<LoginViewModel>();

        public DiagnosticListViewModel DiagnosticListViewModel
            => ServiceLocator.Current.GetInstance<DiagnosticListViewModel>();
    }
}
