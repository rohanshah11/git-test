﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.User.Repository.Interface
{
    public interface BaseRepository<T>
    {
        void delete(T entity);
        void insert(T entity);
        void update(T entity);
        List<T> getAll();
        T getById(long id);
        IQueryable<T> getQueryable();
    }
}
