using KamikazeTrungThanh.Model.Models;
using System.Collections.Generic;
using System;
using KamikazeTrungThanh.Data.Repositories;
using KamikazeTrungThanh.Data.Infrastructure;

namespace KamikazeTrungThanh.Service
{
    public interface ISlideService
    {
        Slide Add(Slide slide);

        void Update(Slide slide);

        Slide Delete(int id);

        IEnumerable<Slide> GetAll();

        IEnumerable<Slide> GetAll(string keyword);

        Slide GetById(int id);

        void Save();
    }

    public class SlideService : ISlideService
    {
        private ISlideRepository _slideRepository;
        private IUnitOfWork _unitOfWork;

        public SlideService(ISlideRepository slideRepository,IUnitOfWork unitOfWork)
        {
            _slideRepository = slideRepository;
            _unitOfWork = unitOfWork;
        }
        public Slide Add(Slide slide)
        {
            var responseSlide = _slideRepository.Add(slide);
            _unitOfWork.Commit();
            return responseSlide;
        }

        public Slide Delete(int id)
        {
            return _slideRepository.Delete(id);
        }

        public IEnumerable<Slide> GetAll()
        {
            return _slideRepository.GetAll();
        }

        public IEnumerable<Slide> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _slideRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            else
            {
                return _slideRepository.GetAll();
            }
        }

        public Slide GetById(int id)
        {
            return _slideRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide slide)
        {
            _slideRepository.Update(slide);
        }
    }
}