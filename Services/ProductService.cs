using HandCrafter.DataBase;
using HandCrafter.Migrations;
using HandCrafter.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text.RegularExpressions;



namespace HandCrafter.Services
{
    public class ProductService
    {
        private readonly ApplicationContext _db;
        public ProductService(ApplicationContext db) => (_db) = (db);

        public void addProductService(ProductRequestModel newProduct)
        {
            var addProduct = new ProductDB
            {
                Name = newProduct?.Name ?? "Новый продукт",
                Description = newProduct.Description,
                ImgName = newProduct.ImgName,
                Price = newProduct?.Price ?? 5000,
                Discount = newProduct?.Discount ?? 0,
                Quantity = newProduct?.Quantity ?? 0
            };
            _db.Products.Add(addProduct);


            _db.SaveChanges();
            if (newProduct.ProductColor != null)
            {
                var colors = _db.Colors;
                foreach (var colorId in newProduct.ProductColor)
                {
                    if (colors.Any(c => c.Id == colorId.Id))
                    {
                        _db.ProductsColors.Add(new ProductColorDB
                        {
                            IdProduct = addProduct.Id,
                            IdColor = colorId.Id
                        });


                        _db.SaveChanges();
                    }

                }
            }
            if (newProduct.ProductCategory != null)
            {

                var categories = _db.Categories;
                foreach (var categoryId in newProduct.ProductCategory)
                {

                    if (categories.Any(c => c.Id == categoryId.Id))
                    {
                        _db.ProductsCategories.Add(new ProductCategoryDB
                        {
                            IdProduct = addProduct.Id,
                            IdCategory = categoryId.Id
                        });


                        _db.SaveChanges();
                    }

                }
            }
            if (newProduct.ProductComposition != null)
            {
                var composition = _db.Compositions;
                foreach (var compositionId in newProduct.ProductComposition)
                {
                    if (composition.Any(c => c.Id == compositionId.Id))
                    {
                        _db.ProductsCompositions.Add(new ProductCompositionDB
                        {
                            IdProduct = addProduct.Id,
                            IdComposition = compositionId.Id
                        });

                        _db.SaveChanges();
                    }
                }
            }

        }
        public List<ProductRequest> getProductsService()
        {
            var products = _db.Products
               .Include(pc => pc.ProductColor)
               .ThenInclude(pc => pc.Color)
               .Include(pc => pc.ProductCategory)
               .ThenInclude(pc => pc.Category)
               .Include(pc => pc.ProductComposition)
               .ThenInclude(pc => pc.Composition)
               .GroupBy(pc => pc.Id)
               .Select(group => new ProductRequest
               {
                   Id = group.Key,
                   Name = group.Select(pc => pc.Name).FirstOrDefault(),
                   Description = group.Select(pc => pc.Description).FirstOrDefault(),
                   ImgName = group.Select(pc => pc.ImgName).FirstOrDefault(),
                   Price = group.Select(pc => pc.Price).FirstOrDefault(),
                   Discount = group.Select(pc => pc.Discount).FirstOrDefault(),
                   Quantity = group.Select(pc => pc.Quantity).FirstOrDefault(),
                   ProductColor = group.SelectMany(pc => pc.ProductColor)
                   .Select(pc => new InfoModel
                   {
                       Id = pc.Color.Id,
                       Name = pc.Color.Name

                   }).ToList(),
                   ProductCategory = group.SelectMany(pc => pc.ProductCategory)
                   .Select(pc => new InfoModel
                   {
                       Id = pc.IdCategory,
                       Name = pc.Category.Name
                   }).ToList(),
                   ProductComposition = group.SelectMany(pc => pc.ProductComposition)
                   .Select(pc => new InfoModel
                   {
                       Id = pc.IdComposition,
                       Name = pc.Composition.Name
                   }).ToList()
               })
               .ToList();
            
            return products;

        }

        public ProductRequest getProductByIdService(int id)
        {
            var products = _db.Products
               .Include(pc => pc.ProductColor)
               .ThenInclude(pc => pc.Color)
               .Include(pc => pc.ProductCategory)
               .ThenInclude(pc => pc.Category)
               .Include(pc => pc.ProductComposition)
               .ThenInclude(pc => pc.Composition)
               .GroupBy(pc => pc.Id)
               .Select(group => new ProductRequest
               {
                   Id = group.Key,
                   Name = group.Select(pc => pc.Name).FirstOrDefault(),
                   Description = group.Select(pc => pc.Description).FirstOrDefault(),
                   Price = group.Select(pc => pc.Price).FirstOrDefault(),
                   ImgName = group.Select(pc => pc.ImgName).FirstOrDefault(),
                   Discount = group.Select(pc => pc.Discount).FirstOrDefault(),
                   Quantity = group.Select(pc => pc.Quantity).FirstOrDefault(),
                   ProductColor = group.SelectMany(pc => pc.ProductColor)
                   .Select(pc => new InfoModel
                   {
                       Id = pc.Color.Id,
                       Name = pc.Color.Name

                   }).ToList(),
                   ProductCategory = group.SelectMany(pc => pc.ProductCategory)
                   .Select(pc => new InfoModel
                   {
                       Id = pc.IdCategory,
                       Name = pc.Category.Name
                   }).ToList(),
                   ProductComposition = group.SelectMany(pc => pc.ProductComposition)
                   .Select(pc => new InfoModel
                   {
                       Id = pc.IdComposition,
                       Name = pc.Composition.Name
                   }).ToList()
               }).Where(pc => pc.Id == id).FirstOrDefault();

            return products;
        }
        public List<ProductRequest> getProductsByCategoriesService(int id)
        {
            var products = _db.Products
               .Include(pc => pc.ProductColor)
               .ThenInclude(pc => pc.Color)
               .Include(pc => pc.ProductCategory)
               .ThenInclude(pc => pc.Category)
               .Include(pc => pc.ProductComposition)
               .ThenInclude(pc => pc.Composition)
               .Where(pc => pc.ProductCategory.Any(pc => pc.IdCategory == id))
               .GroupBy(pc => pc.Id)
               .Select(group => new ProductRequest
               {
                   Id = group.Key,
                   Name = group.Select(pc => pc.Name).FirstOrDefault(),
                   Description = group.Select(pc => pc.Description).FirstOrDefault(),
                   Price = group.Select(pc => pc.Price).FirstOrDefault(),
                   ImgName = group.Select(pc => pc.ImgName).FirstOrDefault(),
                   Discount = group.Select(pc => pc.Discount).FirstOrDefault(),
                   Quantity = group.Select(pc => pc.Quantity).FirstOrDefault(),
                   ProductColor = group.SelectMany(pc => pc.ProductColor)
                   .Select(pc => new InfoModel
                   {
                       Id = pc.Color.Id,
                       Name = pc.Color.Name

                   }).ToList(),
                   ProductCategory = group.SelectMany(pc => pc.ProductCategory)
                   .Select(pc => new InfoModel
                   {
                       Id = pc.IdCategory,
                       Name = pc.Category.Name
                   }).ToList(),
                   ProductComposition = group.SelectMany(pc => pc.ProductComposition)
                   .Select(pc => new InfoModel
                   {
                       Id = pc.IdComposition,
                       Name = pc.Composition.Name
                   }).ToList()
               }).ToList();

            return products;
        }
    }
}
