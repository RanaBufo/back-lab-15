using HandCrafter.DataBase;

namespace HandCrafter.Model
{
    public class ProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImgName { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public List<InfoModel> ProductColor { get; set; }

        public List<InfoModel> ProductCategory { get; set; }

        public List<InfoModel> ProductComposition { get; set; }

    }
}
