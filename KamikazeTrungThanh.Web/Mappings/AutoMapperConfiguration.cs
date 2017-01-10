using AutoMapper;
using KamikazeTrungThanh.Model.Models;
using KamikazeTrungThanh.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KamikazeTrungThanh.Web.Mappings
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Slide, SlideViewModel>();
            Mapper.CreateMap<Product, ProductViewModel>();
            Mapper.CreateMap<ProductCategory, ProductCategoryViewModel>();
     
            Mapper.CreateMap<ContactDetail, ContactDetailViewModel>();
        }
    }
}