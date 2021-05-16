﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.User.Library
{
    public interface PasswordHash
    {
        string CreateHash(string password);
        bool ValidatePassword(string password, string correct_hash);
    }
}
