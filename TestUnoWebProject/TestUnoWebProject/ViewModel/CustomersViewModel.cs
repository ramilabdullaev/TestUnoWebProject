using System.Collections.ObjectModel;
using TestUnoWebProject.Data;
using TestUnoWebProject.Model;

namespace TestUnoWebProject.ViewModel
{
    public class CustomersViewModel : ViewModelBase
    {
        private readonly ICustomerDataProvider _customerDataProvider;
        private CustomerItemViewModel? selectedCustomer;

        public CustomersViewModel(ICustomerDataProvider customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;
        }

        public ObservableCollection<CustomerItemViewModel> Customers { get; set; } = new();

        public CustomerItemViewModel? SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                selectedCustomer = value;
                RaisePropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            if (Customers.Any()) return;

            var customer = await _customerDataProvider.GetCustomersAsync();

            if (customer is not null)
            {
                foreach (var customerItem in customer)
                {
                    Customers.Add(new CustomerItemViewModel(customerItem));
                }
            }
        }

        public async Task Add()
        {
            var customer = new Customer { FirstName = "NEW", LastName = "NEW" };
            var viewModel = new CustomerItemViewModel(customer);
            Customers.Add(viewModel);
            SelectedCustomer = viewModel;
        }

        public async Task Delete()
        {
            if (SelectedCustomer != null)
            {
                Customers.Remove(SelectedCustomer);
                SelectedCustomer = null;
            }
        }
    }
}