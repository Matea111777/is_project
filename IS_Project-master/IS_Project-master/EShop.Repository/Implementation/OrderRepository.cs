using EShop.Domain.Domain;
using EShop.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EShop.Repository.Implementation.OrderRepository;

namespace EShop.Repository.Implementation
{

        public class OrderRepository : IOrderRepository
        {
            private readonly ApplicationDbContext _context;
            private DbSet<Orders> _orders;
            public OrderRepository(ApplicationDbContext context)
            {
                _context = context;
                _orders = context.Set<Orders>();
            }
            public List<Orders> GetAllOrders()
            {
                return _orders
                    .Include(z => z.BooksInOrders)
                    .Include(z => z.Owner)
                    .Include("BooksInOrders.OrderedProduct")
                    .ToList();
            }

            public Orders GetDetailsForOrder(BaseEntity model)
            {
                return _orders
                    .Include(z => z.BooksInOrders)
                    .Include(z => z.Owner)
                    .Include("BooksInOrders.OrderedProduct")
                    .Include("BooksInOrders.OrderedProduct.Author")
                    .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
            }

        }
    }
