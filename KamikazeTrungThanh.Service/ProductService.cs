using KamikazeTrungThanh.Data.Infrastructure;
using KamikazeTrungThanh.Data.Repositories;
using KamikazeTrungThanh.Model.Models;
using System;
using System.Collections.Generic;

namespace KamikazeTrungThanh.Service
{
    public interface IProductService
    {
        Product Add(Product T);

        void Update(Product T);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string keyword);

        IEnumerable<Product> GetAllByCategoryId(int id);

        IEnumerable<Product> Search(string keyword, int page, int pageSize, out int totalRow, string sort);

        Product GetById(int id);

        void Save();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public Product Add(Product T)
        {
            return _productRepository.Add(T);
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAllByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, out int totalRow, string sort)
        {
            throw new NotImplementedException();
        }

        public void Update(Product T)
        {
            _productRepository.Update(T); ;
        }
    }
}