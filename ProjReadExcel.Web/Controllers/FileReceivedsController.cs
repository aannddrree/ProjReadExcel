using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjReadExcel.Web.Data;
using ProjReadExcel.Web.Services;

namespace ProjReadExcel.Web.Controllers
{
    public class FileReceivedsController : Controller
    {
        private readonly ProjReadExcelWebContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FileReceivedsController(ProjReadExcelWebContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: FileReceiveds
        public async Task<IActionResult> Index()
        {
            return View(await _context.FileReceived.ToListAsync());
        }

        // GET: FileReceiveds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FileReceiveds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,File")] FileReceived fileReceived)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(fileReceived.File.FileName);
                string extension = Path.GetExtension(fileReceived.File.FileName);
                fileReceived.FileDescription = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/files", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await fileReceived.File.CopyToAsync(fileStream);
                }

                _context.Add(fileReceived);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fileReceived);
        }

        // GET: FileReceiveds/Import/5
        public async Task<IActionResult> Import(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.FileReceived.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            //Import File
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (!ReadFileService.Import(wwwRootPath + "/files/" + file.FileDescription))
                return StatusCode(500,"Problema para Importar o Arquivo");

            return View(file);
        }
    }
}
