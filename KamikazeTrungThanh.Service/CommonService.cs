using KamikazeTrungThanh.Common;
using KamikazeTrungThanh.Data.Infrastructure;
using KamikazeTrungThanh.Data.Repositories;
using KamikazeTrungThanh.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace KamikazeTrungThanh.Service
{
    public interface ICommonService
    {
        Footer GetFooter();

        List<ProductCategory> GetBreadcrumb(int childId);

        IEnumerable<Slide> GetSlide();

        IEnumerable<ProductCategory> GetNavigationMenu();

        IEnumerable<ProductCategory> GetNavigationMenuChild();
    }

    public class CommonService : ICommonService
    {
        private IFooterRepository _footerRepository;
        private ISlideRepository _slideRepository;
        private IProductCategoryRepository _productCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public CommonService(IFooterRepository footerRepository, ISlideRepository slideRepository, IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork)
        {
            _footerRepository = footerRepository;
            _slideRepository = slideRepository;
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public Footer GetFooter()
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == CommonConstant.DefaultFooter);
        }

        public IEnumerable<Slide> GetSlide()
        {
            return _slideRepository.GetMulti(x => x.Status);
        }

        public IEnumerable<ProductCategory> GetNavigationMenu()
        {
            return _productCategoryRepository.GetMulti(x => x.Status && x.ParentID == null).OrderBy(x => x.DisplayOrder);
        }

        public IEnumerable<ProductCategory> GetNavigationMenuChild()
        {
            return _productCategoryRepository.GetMulti(x => x.Status && x.ParentID != null).OrderBy(x => x.DisplayOrder);
        }

        public List<ProductCategory> GetBreadcrumb(int childId)
        {
            List<int> familyTree = new List<int>();
            List<ProductCategory> responseBreadcrumb = new List<ProductCategory>();
            int id = childId;
            familyTree.Add(id);
            while (id != 0)
            {
                var tampFather = _productCategoryRepository.GetSingleById(id);
                id = tampFather.ParentID.HasValue ? tampFather.ParentID.Value : 0;
                familyTree.Add(id);
            }
            for (int i = familyTree.Count() - 2; i >= 0; i--)
            {
                int idTamp = familyTree[i];
                var item = _productCategoryRepository.GetSingleById(familyTree[i]);
                responseBreadcrumb.Add(new ProductCategory() { ID = item.ID, Name = item.Name, Alias = item.Alias, MetaKeyword = item.MetaKeyword, Status = item.Status });
            }
            return responseBreadcrumb;
        }
    }
}