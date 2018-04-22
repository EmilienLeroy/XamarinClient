# Xamarin Client

This is a Xamarin client. I made this client for my futur xamarin test.

## Documentation

### Package 

In first we need to install two package:
- MvvmLightLibs version 5.3.0 
- NewtonSoft.Json

### Architecture

Create four folder:
- Services 
- ViewsModels
- Views
- Models

Move the MainPage.xaml into the view folder and update this:

```xaml
xmlns:local="clr-namespace:XamarinClient.Views"
x:Class="XamarinClient.Views.MainPage">
```

into the MainPage.xaml.cs update this:

```csharp
namespace XamarinClient.Views
```

### Models

You need to create the model like the api model.
For example i have create the devise model into a models folder :

```csharp
 class Devise
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public double Taux { get; set; }
    }
```

### Web Service

Into Service create the class WebService and add this:

```csharp
class WebService
    {
        private HttpClient _httpclient;

        public WebService()
        {
            this._httpclient = new HttpClient();
            this._httpclient.BaseAddress = new Uri("http://localhost:50088/api/");
        }

        public async Task<List<Devise>> getAllDevisesAsync()
        {
            List<Devise> devises = null;
            HttpResponseMessage response = await _httpclient.GetAsync("Devise");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                devises = JsonConvert.DeserializeObject<List<Devise>>(content);
            }
            return devises;
        }
    }
```

### ViewsModels

Create two class MainViewModel and ViewModelLocator.
Into ViewModelLocator write this :

```csharp
class ViewModelLocator
    {
        static ViewModelLocator()
        {
            //Injecteur de dependance
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels          
        }
    }
```

Into the MainViewModel copy this :

```csharp
class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Devise> listDevises;
        private WebService api = new WebService();
        private string montantEuros;

        public string MontantEuros
        {
            get { return montantEuros; }
            set {
                montantEuros = value;
                RaisePropertyChanged();
            }
        }

        private Devise deviseSelect;

        public Devise DeviseSelect
        {
            get { return deviseSelect; }
            set {
                deviseSelect = value;
                RaisePropertyChanged();
            }
        }


        public ICommand BtnSetConversion { get; private set; }

        public ObservableCollection<Devise> ListeDevises
        {
            get { return listDevises; }
            set
            {
                listDevises = value;
                RaisePropertyChanged();// Pour notifier de la modification de ses données
            }
        }

        public MainViewModel()
        {
            ActionGetData();
            BtnSetConversion = new RelayCommand(ActionSetConversion);
        }

        private void ActionSetConversion()
        {
            var result = double.Parse(MontantEuros) * DeviseSelect.Taux;
            MontantEuros = result.ToString();
        }

        private async void ActionGetData()
        {
            var result = await api.getAllDevisesAsync();
            this.ListeDevises = new ObservableCollection<Devise>(result);
        }
    }
```

into the MainPage.xaml.cs add this :
```csharp
BindingContext = ((ViewModelLocator)Application.Current.Resources["Locator"]).Main;
```

and update the App.xaml like this : 
```xaml
<?xml version="1.0" encoding="utf-8" ?>
<Application xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:vm="clr-namespace:XamarinClient.ViewModels"
             x:Class="XamarinClient.App">
	<Application.Resources>

        <!-- Application resource dictionary -->
        <ResourceDictionary>
            <vm:ViewModelLocator x:Key="Locator" />
        </ResourceDictionary>

    </Application.Resources>
</Application>
```

### Views

Update the MainPage.xaml like this : 

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:XamarinClient.Views"
             x:Class="XamarinClient.Views.MainPage">

    <StackLayout>
        <Entry x:Name="MontantEuros" Text="{Binding MontantEuros, Mode=TwoWay}" ></Entry>
        <Button x:Name="convertir" Text="Convertir" Command="{Binding BtnSetConversion}" IsEnabled="True"></Button>
    
    
        <ListView x:Name="listeItineraires" SelectedItem="{Binding DeviseSelect, Mode=TwoWay}" ItemsSource="{Binding ListeDevises, Mode=TwoWay}" SeparatorVisibility="None">
            <ListView.ItemTemplate>
                <DataTemplate>
                    <TextCell Text="{Binding Nom}" />
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>

    </StackLayout>
    

</ContentPage>

```


Finally you can run your xamarin client !