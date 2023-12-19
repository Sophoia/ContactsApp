using ContactsApp.Models;

namespace ContactsApp
{
    public partial class MainPage : ContentPage
    {

        public Contactltem selectedItem;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            contactsListView.ItemsSource = ContactsRepository.contactltems;
        }

        private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = e.CurrentSelection.FirstOrDefault() as Contactltem;
            if (selectedItem == null) return;
            nameEntry.Text = selectedItem.Name;
            numberEntry.Text = selectedItem.Number;
            emailEntry.Text = selectedItem.Email;
            emailEntry.IsEnabled = false;
        }

        public void RefreshCollectionView()
        {
            contactsListView.ItemsSource = null;
            contactsListView.ItemsSource = ContactsRepository.contactltems;
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            Contactltem newContact = new Contactltem()
            {
                Name = nameEntry.Text,
                Number = numberEntry.Text,
                Email = emailEntry.Text
            };
            ContactsRepository.AddContact(newContact);
            RefreshCollectionView();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            clearFields();
        }

        private void clearFields()
        {
            nameEntry.Text = "";
            numberEntry.Text = "";
            emailEntry.Text = "";
            contactsListView.SelectedItem = null;
            emailEntry.IsEnabled = true;
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            //DisplayAlert("Success", $"Idex:{selectedItem.Id}", "OK");
            selectedItem.Name = nameEntry.Text;
            selectedItem.Number = numberEntry.Text;
            selectedItem.Email = emailEntry.Text;
            ContactsRepository.UpdateContact(selectedItem);
            RefreshCollectionView();
            clearFields();
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            ContactsRepository.DeleteContact(selectedItem.Id);
            RefreshCollectionView();
            clearFields();
        }
    }
}