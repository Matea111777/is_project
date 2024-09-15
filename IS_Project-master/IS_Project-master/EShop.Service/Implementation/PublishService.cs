using EShop.Domain.Domain;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class PublishService : IPublishService
    {
        private readonly IRepository<Publishers> publishRepository;
        private readonly IPublishRepository _publishRepository;
        private readonly IBookService bookService;
        public PublishService(IRepository<Publishers> publishRepository, IBookService bookService, IPublishRepository _publishRepository)
        {
            this.publishRepository = publishRepository;
            this.bookService = bookService;
            this._publishRepository = _publishRepository;
        }
        public void CreateNewPublish(Publishers r)
        {
            if (bookService.AvailableCheck(r.BookId))
            {
                bookService.SetBookAsUnavailable(r.BookId);
                publishRepository.Insert(r);
            }
            //else return exception
        }

   
        public Publishers DeletePublish(Guid? id)
        {
            Publishers publish = _publishRepository.GetDetailsForRent(id);
            try
            {
                publishRepository.Delete(publish);
                bookService.SetBookAsAvailable(publish.BookId);
            }
            catch
            {
                //No rent exception
            }
            return publish;
        }

        public Publishers GetPublisherById(Guid id)
        {
            return _publishRepository.GetDetailsForRent(id);
        }

        public List<Publishers> GetPublish()
        {
            return _publishRepository.GetAllPublishers();
        }

        public Publishers UpdatePublish(Publishers publish)
        {
            return publishRepository.Update(publish);
        }

        
    }
}
