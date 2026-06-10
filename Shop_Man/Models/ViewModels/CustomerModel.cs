namespace Shop_Man.Models.ViewModels
{
    public class CustomerModel
    {

        public int CustomerID { get; set; }

        public string? CustomerNo { get; set; }

        public string? Name { get; set; }

        public string? Address1 { get; set; }
        public string? ShopName  { get; set; }


        public int SubCategoryID { get; set; }

        public string? SUbCategoryName { get; set; }
        public int CategoryID { get; set; }

        public string? CategoryName { get; set; }


    }
}
