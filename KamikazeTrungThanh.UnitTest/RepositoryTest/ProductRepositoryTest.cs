using KamikazeTrungThanh.Data.Infrastructure;
using KamikazeTrungThanh.Data.Repositories;
using KamikazeTrungThanh.Model.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamikazeTrungThanh.UnitTest.RepositoryTest
{
    [TestClass]
    public class ProductRepositoryTest
    {
        private IDbFactory dbFactory;
        private IUnitOfWork unitOfWork;
        private IProductRepository productRepository;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            unitOfWork = new UnitOfWork(dbFactory);
            productRepository = new ProductRepository(dbFactory);

        }
        [TestMethod]
        public void Product_Repository_Create()
        {
            Product product = new Product();
            product.Name = "Công trình C";
            product.Alias = "Cong-Trinh-C";
            product.Price = 100000;
            product.Quantity = 3;
            product.CategoryID = 1;
            product.Status = true;
            product.CreatedDate = DateTime.Now;
            var result = productRepository.Add(product);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(1,result.ID);
        }
    }
}
