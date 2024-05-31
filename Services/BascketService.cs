using HandCrafter.DataBase;
using HandCrafter.Model;
using System.Data.Entity;

namespace HandCrafter.Services
{
    public class BascketService
    {
        private readonly ApplicationContext _db;
        private readonly ProductService _productService;
        public BascketService(ApplicationContext db, ProductService productService) => (_db, _productService) = (db, productService);

        public bool addNeItemBascketService(BasketRequest newItem)
        {
            var product = _productService.getProductByIdService(newItem.IdProduct);
            if (product != null)
            {
                if(newItem.Quantity > product.Quantity)
                {
                    newItem.Quantity = product.Quantity;
                }
                else if(newItem.Quantity == 0)
                {
                    newItem.Quantity = 1;
                }
                var item = new BasketDB
                {
                    IdUser = newItem.IdUser,
                    IdProduct = newItem.IdProduct,
                    Price = product.Price * newItem.Quantity,
                    Quantity = newItem.Quantity,
                    Discount = newItem.Discount
                };
                    var bascket = getItemBasketService(newItem.IdProduct, newItem.IdUser);
                if(bascket == null)
                {
                    return false;
                }
                _db.Baskets.Add(item);
                _db.SaveChanges();
                return true;
            }

            return false;
        }

        public List<BasketDB> getAllItemsBasketService(int id)
        {
            var allItems = _db.Baskets
        .Include(b => b.Product)
        .Where(b => b.IdUser == id)
        .GroupBy(b => b.IdProduct)
        .Select(g => new BasketDB
        {
            Id = g.First().Id,
            IdUser = g.First().IdUser,
            IdProduct = g.Key,
            Quantity = g.Sum(b => b.Quantity),
            Price = g.Sum(b => b.Price),
            Product = g.First().Product
        })
        .ToList();

            return allItems;
        }
        public bool updateQuentityBasketService(BasketQuantityRequest basketQuantity)
        {
            var item = _db.Baskets
    .Include(b => b.Product)
    .Where(b => b.Id == basketQuantity.Id)
    .GroupBy(b => b.IdProduct)
    .Select(group => new BasketDB
    {
        Id = group.Select(b => b.Id).FirstOrDefault(),
        IdUser = group.Select(b => b.IdUser).FirstOrDefault(),
        IdProduct = group.Key, // Теперь это ключ группировки (IdProduct)
        Quantity = group.Select(b => b.Quantity).FirstOrDefault(),
        Price = group.Select(b => b.Price).FirstOrDefault(),
        Product = group.Select(b => b.Product).FirstOrDefault()
    })
    .FirstOrDefault();


            if (item != null && item.Product.Quantity > basketQuantity.Quentity)
            {
                if (basketQuantity.Quentity == 0)
                {
                    deleteBasketItemService(basketQuantity.Id);
                    return true;
                }
                item.Quantity = basketQuantity.Quentity;
                item.Price = basketQuantity.Quentity * item.Product.Price;
                _db.Baskets.Update(item);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool deleteBasketItemService(int id)
        {
            var basket = _db.Baskets.FirstOrDefault(b => b.Id == id);

            if (basket == null)
            {
                return false;
            }

            // Удаление элемента
            _db.Baskets.Remove(basket);
            _db.SaveChanges();

            return true;
        }
        public bool deleteBasketIdUserService(int id)
        {
            var basket = _db.Baskets.Where(b => b.IdUser == id)
                .ToList();

            if (basket == null)
            {
                return false;
            }

            foreach (var item in basket)
            {
                _db.Baskets.Remove(item);
            }

            _db.SaveChanges();

            return true;
        }

        public BasketDB getItemBasketService(int idProduct, int idUser)
        {
            var basket = _db.Baskets
               .FirstOrDefault(b => b.IdProduct == idProduct && b.IdUser == idUser);
            return basket;
        }
    }
}
