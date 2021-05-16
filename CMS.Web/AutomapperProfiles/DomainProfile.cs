using AutoMapper;
using CMS.Core.Entity;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Models;
using CMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.AutomapperProfiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<GalleryImage, GalleryImageDetail>();
            CreateMap<GalleryImageDetail, GalleryImage>();
            CreateMap<Menu, MenuDetailModel>();
            CreateMap<MenuDetailModel, Menu>();
            CreateMap<MenuModel, Menu>();
            CreateMap<Menu, MenuModel>();

        }
    }
}
