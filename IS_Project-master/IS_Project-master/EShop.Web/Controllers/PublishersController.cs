using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.Domain;
using EShop.Repository;
using EShop.Service.Interface;
using EShop.Service.Implementation;

namespace EShop.Web.Controllers
{
    public class PublishersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPublishService publishService;

        public PublishersController(ApplicationDbContext context,IPublishService publishService)
        {
            _context = context;
            this.publishService = publishService;
        }

        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Publishers.Include(p => p.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publishers = await _context.Publishers
                .Include(p => p.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publishers == null)
            {
                return NotFound();
            }

            return View(publishers);
        }

        // GET: Publishers/Create
        public IActionResult Create(Guid? Id)
        {
            if (Id == null)
            {
                return BadRequest("BookId is required.");
            }

            var book = _context.Books.Find(Id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", Id);
            ViewData["AuthorsId"] = new SelectList(_context.Authors, "Id", "Name", book.AuthorId);

          
            var model = new Publishers
            {
                BookId = Id.Value,
                AuthorsId = book.AuthorId.Value
            };

            return View(model);
        }


        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,AuthorsId,PublishDate,OrderDate,RentAmount,Id")] Publishers publishers)
        {
            if (ModelState.IsValid)
            {
                publishers.Id = Guid.NewGuid();
                publishService.CreateNewPublish(publishers);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Title", publishers.BookId);
            ViewData["AuthorsId"] = new SelectList(_context.Authors, "Id", "Name", publishers.AuthorsId);
            return View(publishers);
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publishers = await _context.Publishers.FindAsync(id);
            if (publishers == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "AuthorId", publishers.BookId);
            return View(publishers);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("BookId,AuthorsId,PublishDate,OrderDate,RentAmount,Id")] Publishers publishers)
        {
            if (id != publishers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publishers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublishersExists(publishers.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "AuthorId", publishers.BookId);
            return View(publishers);
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = publishService.GetPublisherById((Guid)id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            publishService.DeletePublish(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PublishersExists(Guid id)
        {
            return _context.Publishers.Any(e => e.Id == id);
        }
    }
}
