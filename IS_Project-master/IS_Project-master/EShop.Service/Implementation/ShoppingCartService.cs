using EShop.Domain.Domain;
using EShop.Domain.DTO;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> shoppingCartRepository;
        private readonly IRepository<BookInShoppingCart> bookInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Orders> _orderRepository;
        private readonly IRepository<BooksInOrder> _booksInOrderRepository;
        public ShoppingCartService(IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<BookInShoppingCart> bookInShoppingCartRepository,IRepository<Orders> ordersRepository,IRepository<BooksInOrder> booksInOrder)
        {
            
            _userRepository = userRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.bookInShoppingCartRepository = bookInShoppingCartRepository;
            _booksInOrderRepository = booksInOrder;
            _orderRepository = ordersRepository;

        }
        public bool AddToShoppingConfirmed(BookInShoppingCart model, string userId)
        {
            var loggedInUser = _userRepository.Get(userId);
            var userShoppingCart = loggedInUser.ShoppingCart;
            if (userShoppingCart.BookInShoppingCarts == null)
            {
                userShoppingCart.BookInShoppingCarts = new List<BookInShoppingCart>();
            }
            userShoppingCart.BookInShoppingCarts.Add(model);
            shoppingCartRepository.Update(userShoppingCart);
            return true;
         }
        public bool deleteProductFromShoppingCart(string userId, Guid productId)
        {
            if (productId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.ShoppingCart;
                var product = userShoppingCart.BookInShoppingCarts.Where(x => x.BookId == productId).FirstOrDefault();

                userShoppingCart.BookInShoppingCarts.Remove(product);

                shoppingCartRepository.Update(userShoppingCart);
                return true;
            }
            return false;

        }
        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            var userShoppingCart = loggedInUser?.ShoppingCart;
            var allBooks = userShoppingCart?.BookInShoppingCarts?.ToList();

            var totalPrice = allBooks.Select(x => (x.Book.Price * x.Quantity)).Sum();

            ShoppingCartDto dto = new ShoppingCartDto
            {
                Books = allBooks,
                TotalPrice = totalPrice
            };
            return dto;
        }
        public bool orderProducts(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.ShoppingCart;

                Orders order = new Orders
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId,
                    Owner = loggedInUser
                };

                _orderRepository.Insert(order);

                List<BooksInOrder> productInOrder = new List<BooksInOrder>();

                var lista = userShoppingCart.BookInShoppingCarts.Select(
                    x => new BooksInOrder
                    {
                        Id = Guid.NewGuid(),
                        BookId = x.Book.Id,
                        OrderedProduct = x.Book,
                        OrderId = order.Id,
                        Order = order,
                        Quantity = x.Quantity
                    }
                    ).ToList();

                productInOrder.AddRange(lista);

                foreach (var product in productInOrder)
                {
                    _booksInOrderRepository.Insert(product);
                }

                loggedInUser.ShoppingCart.BookInShoppingCarts.Clear();
                _userRepository.Update(loggedInUser);
                return true;
            }
            return false;
        }

    }
}
