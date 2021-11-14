namespace MVCDotNet5.Models.ViewModels
{
    public class DetailsViewModal
    {
        public DetailsViewModal()
        {
            Product = new Product();
        }

        public Product Product { get; set; }

        public bool ExistsInCart { get; set; }
    }
}
