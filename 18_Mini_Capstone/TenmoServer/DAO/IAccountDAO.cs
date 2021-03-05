﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        public Account GetAccount(int userId);

        bool UpdateBalance(int userID, decimal amount);
    }
}
