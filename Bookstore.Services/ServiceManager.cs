using Bookstore.Repositories;
using Bookstore.Services.Conctrats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Services
{
    public class ServiceManager : IserviceManager
    {
        Lazy<IBookService> _bookService;

        public ServiceManager(IRepositoryManager manager, ILoggingService logging)
        {
            _bookService = new Lazy<IBookService>(() => new BookService(manager, logging));
        }

        public IBookService Book => _bookService.Value;
    }
}
