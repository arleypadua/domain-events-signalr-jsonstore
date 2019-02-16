﻿using System.Threading.Tasks;
using Ordering.Service.Model;

namespace Ordering.Service.Repositories
{
    public interface IOrderRepository
    {
        Task Add(Order order);
    }
}