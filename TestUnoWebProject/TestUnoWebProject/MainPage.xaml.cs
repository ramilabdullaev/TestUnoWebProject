using TestUnoWebProject.Data;
using TestUnoWebProject.ViewModel;

namespace TestUnoWebProject
{
    public sealed partial class MainPage : Page
    {
        private readonly CustomersViewModel _viewModel;

        public MainPage()
        {
            this.InitializeComponent();
            _viewModel = new CustomersViewModel(new CustomerDataProvider());
            DataContext = _viewModel;
            this.Loaded += CustomersView_Loaded;
        }

        private async void CustomersView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        private async void ButtonAddUstomer_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.Add();
        }

        private async void ButtonDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.Delete();
        }
    }
}