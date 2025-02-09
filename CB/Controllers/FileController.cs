﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CB.Data;
using CB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CB.Controllers
{

    public class FileController : Controller
    {
        private readonly ApplicationDbContext context;

        public FileController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Message = TempData["Message"];
            
            var viewModel = new FileUploadViewModel();
            viewModel.FilesOnDatabase = await context.FilesOnDatabase.ToListAsync();

            return View(viewModel);


        }
        private async Task<FileUploadViewModel> LoadAllFiles()
        {
            var viewModel = new FileUploadViewModel();
            viewModel.FilesOnDatabase = await context.FilesOnDatabase.ToListAsync();
            return viewModel;
        }

        [HttpPost]
        public async Task<IActionResult> UploadToDatabase(List<IFormFile> files, string description)
        {
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var fileModel = new FileOnDatabaseModel
                {
                    CreatedOn = DateTime.UtcNow,
                    FileType = file.ContentType,
                    Extension = extension,
                    Name = fileName,
                    Description = description
                };
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    fileModel.Data = dataStream.ToArray();
                }
                context.FilesOnDatabase.Add(fileModel);
                context.SaveChanges();
            }
            TempData["Message"] = "File successfully uploaded to Database";
            return RedirectToAction("Index");
        }

       

        public async Task<IActionResult> DownloadFileFromDatabase(int id)
        {

            var file = await context.FilesOnDatabase.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (file == null) return null;
            return File(file.Data, file.FileType, file.Name + file.Extension);
        }
       
        public async Task<IActionResult> DeleteFileFromDatabase(int id)
        {

            var file = await context.FilesOnDatabase.Where(x => x.Id == id).FirstOrDefaultAsync();
            context.FilesOnDatabase.Remove(file);
            context.SaveChanges();
            TempData["Message"] = $"Removed {file.Name + file.Extension} successfully from Database.";
            return RedirectToAction("Index");
        }
    }
}