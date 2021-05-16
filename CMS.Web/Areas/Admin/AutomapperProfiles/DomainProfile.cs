using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Web.Areas.Core.Models;
using CMS.Web.Areas.Admin.ViewModels;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.ViewModels;

namespace CMS.Web.Areas.Core.AutomapperProfiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CareerDto, Career>();

            CreateMap<FileUploadModel, FileUploadDto>();
            CreateMap<FileUploadDto, FileUpload>();
            CreateMap<FileUpload, FileUploadModel>();

            CreateMap<ClassesModel, ClassesDto>();
            CreateMap<ClassesDto, Classes>();
            CreateMap<Classes, ClassesModel>();


            CreateMap<GalleryModel, GalleryDto>();
            CreateMap<GalleryDto, Gallery>();
            CreateMap<Gallery, GalleryModel>();

            CreateMap<NoticeDto, Notice>();

            CreateMap<PageCategoryModel, PageCategoryDto>();
            CreateMap<PageCategoryDto, PageCategoryModel>();
            CreateMap<PageCategoryDetailModel, PageCategory>();
            CreateMap<PageCategory, PageCategoryDetailModel>();

            CreateMap<PageDto, PageModel>();
            CreateMap<PageDto, Page>();
            CreateMap<PageModel, Page>();
            CreateMap<PageDetailModel, Page>();
            CreateMap<Page, PageDetailModel>();

            CreateMap<ViewModels.CourseDetail, Courses>();
            CreateMap<Courses, ViewModels.CourseDetail>();
            CreateMap<CoursesDto, Courses>();

            CreateMap<Web.ViewModels.CoursesDetail, Courses>();
            CreateMap<Courses, Web.ViewModels.CoursesDetail>();

            CreateMap<Web.ViewModels.PageDetail, Page>();
            CreateMap<Page, Web.ViewModels.PageDetail>();

            CreateMap<TestimonialDto, Testimonial>();

            CreateMap<ReceivedEmailDto, ReceivedEmail>();

            CreateMap<EventDto, Event>();
            CreateMap<Event, EventDto>();

            CreateMap<PartnersDto, Partners>();
            CreateMap<Partners, PartnersDto>();

            CreateMap<Member, MemberDto>();
            CreateMap<MemberDto, Member>();

            CreateMap<DetailsDto, Details>();
            CreateMap<Details, DetailsDto>();


            CreateMap<Details, DetailsModel>();
            CreateMap<DetailsModel, Details>();

            CreateMap<DetailsDetailModel, Details>();
            CreateMap<Details, DetailsDetailModel>();

            CreateMap<Event, EventModel>();
            CreateMap<EventModel, Event>();

            CreateMap<EventDetailModel, Event>();
            CreateMap<Event, EventDetailModel>();

            CreateMap<OrderDetailModel, Order>();
            CreateMap<Order, OrderDetailModel>();

            CreateMap<Blog, BlogDto>();
            CreateMap<BlogDto, Blog>();

            CreateMap<ViewModels.BlogDetailModel, Blog>();
            CreateMap<Blog, ViewModels.BlogDetailModel>();

            CreateMap<BlogCommentDetailModel, BlogComment>();
            CreateMap<BlogComment, BlogCommentDetailModel>();

            CreateMap<GalleryImageDetailModel, GalleryImage>();
            CreateMap<GalleryImage, GalleryImageDetailModel>();


            CreateMap<GalleryImage, Gallery>();
            CreateMap<Gallery, GalleryImage>();

            CreateMap<GalleryDetailModel, Gallery>();
            CreateMap<Gallery, GalleryDetailModel>();


            CreateMap<RoutineModel, Routine>();
            CreateMap<Routine, RoutineModel>();

            CreateMap<BlogCommentDetailModel, BlogComment>();
            CreateMap<BlogComment, BlogCommentDetailModel>();

            CreateMap<VideoDetails, Video>();
            CreateMap<Video, VideoDetails>();
            CreateMap<ServicesDetails, Services>();
            CreateMap<Services, ServicesDetails>();

            CreateMap<MenuCategoriesDetail, MenuCategory>();
            CreateMap<MenuCategory, MenuCategoriesDetail>();
            CreateMap<MenuCategoryDto, MenuCategoryModel>();
            CreateMap<MenuCategoryModel, MenuCategoryDto>();
            CreateMap<MenuCategoryModel, MenuCategory>();
            CreateMap<MenuCategory, MenuCategoryModel>();

            CreateMap<MenuTypeModel, MenuTypeDto>();
            CreateMap<MenuTypeDto, MenuTypeModel>();
            CreateMap<MenuTypeModel, MenuType>();
            CreateMap<MenuType, MenuTypeModel>();
            CreateMap<MenuTypeDetails, MenuType>();
            CreateMap<MenuType, MenuTypeDetails>();

            CreateMap<MenuModel, MenuDto>();
            CreateMap<MenuDto, MenuModel>();
            CreateMap<MenuModel, Menu>();
            CreateMap<Menu, MenuModel>();
            CreateMap<MenuDetails, Menu>();
            CreateMap<Menu, MenuDetails>();



        }
    }
}
