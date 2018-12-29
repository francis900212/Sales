namespace Sales.Infrastructure
{
    using Sales.ViewModels;

    class InstanceLocator {

        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
