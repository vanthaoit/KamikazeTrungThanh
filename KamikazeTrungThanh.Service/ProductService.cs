using KamikazeTrungThanh.Data.Infrastructure;
using KamikazeTrungThanh.Data.Repositories;
using KamikazeTrungThanh.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        IEnumerable<Product> GetAllByAlias(string alias);

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
            if (!string.IsNullOrEmpty(keyword))
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productRepository.GetAll();

        }

        public IEnumerable<Product> GetAllByAlias(string alias)
        {
            if (!string.IsNullOrEmpty(alias))
            {
                return _productRepository.GetMulti(x => x.Alias.Contains(alias)).OrderBy(x=>x.UpdatedDate);
            }
            else
            {
                return _productRepository.GetAll().OrderBy(x => x.UpdatedDate);
            }
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