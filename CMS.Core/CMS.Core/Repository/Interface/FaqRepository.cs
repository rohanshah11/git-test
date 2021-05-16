using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface FaqRepository
    {
        void insert(Faq faq);
        void update(Faq faq);
        void delete(Faq faq);
        Faq getById(long faq_id);
        List<Faq> getAll();
        IQueryable<Faq> getQueryable();

    }
}
