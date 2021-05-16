using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.AutomapperProfile
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Career, CareerDto>();
            CreateMap<FileUpload, FileUploadDto>();
            CreateMap<Gallery, GalleryDto>();
            CreateMap<Notice, NoticeDto>();
            CreateMap<PageCategory, PageCategoryDto>();
            CreateMap<PageCategoryDto, PageCategory>();
            CreateMap<Page, PageDto>();
            CreateMap<Courses, CoursesDto>();
            CreateMap<Testimonial, TestimonialDto>();
            CreateMap<ReceivedEmail, ReceivedEmailDto>();
            CreateMap<Designation, DesignationDto>();
            CreateMap<FiscalYear, FiscalYearDto>();
            CreateMap<Member, MemberDto>();
            CreateMap<Event, EventDto>();
            CreateMap<Details, DetailsDto>();
            CreateMap<Blog, BlogDto>();
            CreateMap<BlogDto, Blog>();
            CreateMap<Video, VideoDto>();
            CreateMap<VideoDto, Video>();
            CreateMap<BlogComment, BlogCommentDto>();
            CreateMap<BlogCommentDto, BlogComment>();
            CreateMap<DoctorsDto, Doctors>();
            CreateMap<Doctors, DoctorsDto>();
            CreateMap<News, NewsDto>();
            CreateMap<NewsDto, News>();
            CreateMap<Services, ServicesDto>();
            CreateMap<ServicesDto, Services>();
            CreateMap<Appointment, AppointmentDto>();
            CreateMap<AppointmentDto, Appointment>();
            CreateMap<Faq, FaqDto>();
            CreateMap<FaqDto, Faq>();
            CreateMap<MenuCategory, MenuCategoryDto>();
            CreateMap<MenuCategoryDto, MenuCategory>();
            CreateMap<MenuType, MenuTypeDto>();
            CreateMap<MenuTypeDto, MenuType>();
            CreateMap<Menu, MenuDto>();
            CreateMap<MenuDto, Menu>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
             CreateMap<Partners, PartnersDto>();
            CreateMap<PartnersDto, Partners>();

            CreateMap<OrderDetail, OrderDetailDto>();
            CreateMap<OrderDetailDto, OrderDetail>();

        }
    }
}
