﻿using CMS.Core.Dto;
using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Interface
{
    public interface MenuMaker
    {
        void copy(Menu menu, MenuDto dto);
    }
}
