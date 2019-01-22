namespace Sales.Backend.Models
{
    using Sales.Common.Models;
    using System;
    using System.Web;

    public class ProductView:Product
    {
        public HttpPostedFileBase ImageFile { get; set; }

        internal Product ConvertToProduct(string imagePath)
        {
            Product product = new Product()
            {
                Description = this.Description,
                IsAvailable = this.IsAvailable,
                ImagePath = imagePath,
                Price = this.Price,
                PublishOn = this.PublishOn,
                Remarks = this.Remarks
            };

            return product;
        }
    }
}