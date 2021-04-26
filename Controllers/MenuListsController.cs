using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvcEntities.Entities;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderingApp.Controllers
{
    [Authorize(Roles = "1")]
    public class MenuListsController : Controller
    {
        private readonly FoodOrderedDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MenuListsController(FoodOrderedDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;

        }

        // GET: MenuLists
        public async Task<IActionResult> Index()
        {
            var foodOrderedDbContext = _context.MenuList.Include(m => m.Category);
            return View(await foodOrderedDbContext.ToListAsync());
        }

        // GET: MenuLists/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuList = await _context.MenuList
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menuList == null)
            {
                return NotFound();
            }

            return View(menuList);
        }

        // GET: MenuLists/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuId,MenuName,MenuPrice,MenuDescription,ImageUploader,CategoryId")] MenuList menu)
        {
            if (ModelState.IsValid)
            {
                //save image to wwwroot/Image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(menu.ImageUploader.FileName);
                string extension = Path.GetExtension(menu.ImageUploader.FileName);
                menu.MenuImage = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/MenuImages/" + fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await menu.ImageUploader.CopyToAsync(filestream);
                }
                //insert record
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", menu.CategoryId);
            return View(menu);
        }

        // GET: MenuLists/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuList = await _context.MenuList.FindAsync(id);
            if (menuList == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", menuList.CategoryId);
            return View(menuList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MenuId,MenuName,MenuPrice,MenuDescription,MenuImage,CategoryId")] MenuList menuList)
        {
            if (id != menuList.MenuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuListExists(menuList.MenuId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", menuList.CategoryId);
            return View(menuList);
        }

        // GET: MenuLists/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuList = await _context.MenuList
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (menuList == null)
            {
                return NotFound();
            }

            return View(menuList);
        }

        // POST: MenuLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var menuList = await _context.MenuList.FindAsync(id);
            _context.MenuList.Remove(menuList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuListExists(long id)
        {
            return _context.MenuList.Any(e => e.MenuId == id);
        }
    }
}
