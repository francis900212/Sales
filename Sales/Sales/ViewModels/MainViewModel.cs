using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.ViewModels
{

    public class MainViewModel {
        public ProductsViewModel Product { get; set; }

        public MainViewModel()
        {
            Product = new ProductsViewModel();
        }
    }
}
 