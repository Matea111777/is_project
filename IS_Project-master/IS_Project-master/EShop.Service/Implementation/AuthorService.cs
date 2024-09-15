using EShop.Domain.Domain;
using EShop.Domain.DTO;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Authors> authorRepository;

        public AuthorService(IRepository<Authors> authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        public List<Authors> GetAuthors()
        {
            return authorRepository.GetAll().ToList();
        }
       

    }
}
