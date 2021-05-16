﻿using CMS.User.Dto;

namespace CMS.User.Service.Interface
{
    public interface RoleService
    {
        void save(RoleDto role_dto);
        void delete(long role_id);
        void update(RoleDto role_dto);
        void enable(long role_id);
        void disable(long role_id);
    }
}
