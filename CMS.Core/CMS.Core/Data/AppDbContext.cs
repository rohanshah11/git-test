using CMS.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CMS.Core.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Video> videos { get; set; }
        public DbSet<Career> careers { get; set; }
        public DbSet<FileUpload> file_uploads { get; set; }
        public DbSet<GalleryImage> gallery_image { get; set; }
        public DbSet<Notice> notices { get; set; }
        public DbSet<Page> pages { get; set; }
        public DbSet<PageCategory> page_categories { get; set; }
        public DbSet<Courses> courses { get; set; }
        public DbSet<Setup> setup { get; set; }
        public DbSet<Testimonial> testimonials { get; set; }
        public DbSet<ReceivedEmail> received_emails { get; set; }
        public DbSet<Designation> designations { get; set; }
        public DbSet<FiscalYear> fiscalYears { get; set; }
        public DbSet<Member> members { get; set; }

        public DbSet<Event> events { get; set; }
        public DbSet<Details> details { get; set; }

        public DbSet<Blog> blog { get; set; }
        public DbSet<BlogComment> blog_comment { get; set; }
        public DbSet<Faculty> faculty { get; set; }
        public DbSet<Gallery> gallery { get; set; }
        public DbSet<Classes> classes { get; set; }
        public DbSet<ExamTerm> exam_terms { get; set; }
        public DbSet<GalleryImage> gallery_images { get; set; }
        public DbSet<Routine> routine { get; set; }
        public DbSet<Doctors> doctors { get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<Services> services { get; set; }
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<Faq> faqs { get; set; }
        public DbSet<MenuCategory> menu_category { get; set; }
        public DbSet<MenuType> menu_type { get; set; }
        public DbSet<Menu> menu { get; set; }
        public DbSet<Order> order { get; set; }
        public DbSet<OrderDetail> order_detail { get; set; }
        public DbSet<Partners> partners { get; set; }
    }
}
