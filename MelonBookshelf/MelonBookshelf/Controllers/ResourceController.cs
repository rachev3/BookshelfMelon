using AutoMapper;
using MelonBookshelf.Data;
using MelonBookshelf.Data.Services;
using MelonBookshelf.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using MelonBookshelf.Data.DTO;
using MelonBookshelf.Models.ExcelReport;
using OfficeOpenXml;
using static System.Net.WebRequestMethods;
using OfficeOpenXml.Style;
using System.Drawing;

namespace MelonBookshelf.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceService resourceService;
        private readonly ICategoryService categoryService;
        private readonly IResourceCommentService commentService;
        private readonly IMapper mapper;
        private readonly IConfiguration _config;
        private readonly IUserService userService;

        public ResourceController(IResourceService resourceService, ICategoryService categoryService, IConfiguration config, IUserService userService, IResourceCommentService commentService)
        {
            this.resourceService = resourceService;
            this.categoryService = categoryService;
            this._config = config;
            this.userService = userService;
            this.commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string name = User.Identity.Name;
            string userId = userService.GetByUserName(name).Result.Id;

            var resources = await resourceService.GetAll();
            var viewListResource = new List<ResourceViewModel>();

            foreach (var resource in resources)
            {
                var viewModel = new ResourceViewModel(resource, "ResourcesTable");

                var want = resource.WantedResources.FirstOrDefault(w => w.UserId == userId);

                if (want != null)
                {
                    viewModel.Want = true;
                }
                else
                {
                    viewModel.Want = false;
                }

                viewListResource.Add(viewModel);
            }

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var viewPageModel = new ResourcePageViewModel(viewListResource, viewListCategory);

            return View("Resource", viewPageModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string? title, ResourceType? resourceType, int? categoryId)
        {
            var data = await resourceService.Search(title, resourceType, categoryId);

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var resources = data.Select(x => new ResourceViewModel(x)).ToList();
            var viewModel = new ResourcePageViewModel(resources, viewListCategory);

            return View("Resource", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            ResourceEditViewModel resource = new(viewListCategory);

            return View(resource);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResourceEditViewModel resource)
        {
            Resource dto = new Resource(
                resource.Type,
                resource.Author,
                resource.Title,
                resource.Description,
                resource.Location,
                resource.Price,
                resource.Invoice,
                resource.Status,
                resource.DateAdded,
                resource.DateTaken,
                resource.DateReturn,
                null,
                null,
                null,
                null,
                resource.CategoryId,
                null);

            await resourceService.Add(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var data = await resourceService.GetById(id);

            ResourceViewModel resource = new(data, "Details");

            return View("Details", resource);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, string commingViewName)
        {
            var resource = await resourceService.GetById(id);

            if (resource == null)
            {
                return View("NotFound");
            }

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var viewModel = new ResourceEditViewModel(resource, viewListCategory);

            if (commingViewName == "Details")
            {
                return View("Edit", viewModel);
            }

            return PartialView("_Edit", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(ResourceEditViewModel resource)
        {
            if (!ModelState.IsValid)
            {
                return View(resource);
            }

            Resource dto = new Resource(

               resource.Type,
               resource.Author,
               resource.Title,
               resource.Description,
               resource.Location,
               resource.Price,
               resource.Invoice,
               resource.Status,
               resource.DateAdded,
               resource.DateTaken,
               resource.DateReturn,
               null,
               null,
               null,
               null,
               resource.CategoryId, null);
            dto.ResourceId = resource.ResourceId;

            await resourceService.Update(resource.ResourceId, dto);

            var categories = await categoryService.GetAll();
            var viewListCategory = categories.Select(c => new CategoryViewModel(c)).ToList();

            var resources = await resourceService.GetAll();
            var viewListResource = resources.Select(r => new ResourceViewModel(r)).ToList();

            var viewModel = new ResourcePageViewModel(viewListResource, viewListCategory);
            return PartialView("_ResourceTable", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Want(int resourceId)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            await resourceService.Want(userId, resourceId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Unwant(int resourceId)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            await resourceService.Unwant(userId, resourceId);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(ResourceCommentViewModel resourceCommentViewModel, int resourceId)
        {
            string name = User.Identity.Name;
            User user = userService.GetByUserName(name).Result;

            ResourceComment resourceComment = new();
            resourceComment.Comment = resourceCommentViewModel.Comment;
            resourceComment.User = user;
            resourceComment.ResourceId = resourceId;

            await commentService.Add(resourceComment);

            var data = await resourceService.GetById(resourceId);

            ResourceViewModel resource = new(data, "Details");
            return View("Details", resource);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId, int resourceId)
        {
            var comment = await commentService.GetById(commentId);

            if (comment == null)
            {
                return View("NotFound");
            }

            await commentService.Delete(commentId);

            var data = await resourceService.GetById(resourceId);
            ResourceViewModel resource = new(data, "Details");

            return View("Details", resource);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upload(IFormFile file, int resourceId)
        {
            Resource resource = null;
            resource = resourceService.GetById(resourceId).Result;
            Guid guid = Guid.NewGuid();
            string ext = Path.GetExtension(file.FileName);
            string shortLocation = _config.GetValue<string>("FileStorage:Path").ToString() + guid + ext;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), shortLocation);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            resource.Location = shortLocation;
            resource.FileName = file.FileName;
            resourceService.Update(resource.ResourceId, resource);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Download(int resourceId)
        {
            Resource resource = await resourceService.GetById(resourceId);

            if (resource == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), resource.Location);
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

            ResourceDownloadHistory download = new ResourceDownloadHistory();
            download.ResourceId = resourceId;
            download.ResourceName = resource.Title + " " + resource.Author;
            download.Username = User.Identity.Name;
            download.DownloadDate = DateTime.Now;

            await resourceService.AddDownload(download);

            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, resource.FileName);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DownloadsReport()
        {
            return View("DownloadsReport");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> GenerateReport(ReportViewModel reportViewModel)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<ResourceDownloadHistory> history = await resourceService.ReportData(reportViewModel.Date);
            List<ExcelReportModel> reportData = new List<ExcelReportModel>();

            var groupedHistory = history
                 .GroupBy(item => item.ResourceName)
                 .Select(group => new
                 {
                     DownloadDate = group.First().DownloadDate.Date.ToString(), 
                     ResourceName = group.Key,
                     DownloadCount = group.Count()
                 }).ToList();

            var file = new FileInfo(@"C:\Users\User\Desktop\MelonBook\ExcelReport\Reports.xlsx");

            if (file.Exists)
            {
                file.Delete();
            }

            using var package = new ExcelPackage(file);

            var ws = package.Workbook.Worksheets.Add("Report");

            var range = ws.Cells["A2"].LoadFromCollection(groupedHistory, true);
            range.AutoFitColumns();

            // Formats the header
            ws.Cells["A1"].Value = "Daily Download Report";
            ws.Cells["A1:C1"].Merge = true;
            ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Row(1).Style.Font.Size = 24;
            ws.Row(1).Style.Font.Color.SetColor(Color.Blue);

            ws.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Row(2).Style.Font.Bold = true;
            ws.Column(3).Width = 20;

            await package.SaveAsync();

            var generatedFilePath = @"C:\Users\User\Desktop\MelonBook\ExcelReport\Reports.xlsx";
            var generatedFileName = "Reports.xlsx";
            var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return PhysicalFile(generatedFilePath, mimeType, generatedFileName);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var resource = await resourceService.GetById(id);
            ResourceViewModel resourceViewModel = new(resource);

            if (resource == null)
            {
                return View("NotFound");
            }

            return View(resourceViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var resource = await resourceService.GetById(id);

            if (resource == null)
            {
                return View("NotFound");
            }

            await resourceService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
