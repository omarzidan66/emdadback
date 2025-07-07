using Emdad.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Emdad.Controllers
{
    public class SubmissionDataController : Controller
    {
        private readonly EmdadContext _context;

        public SubmissionDataController(EmdadContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Save(int id, IFormCollection form, List<IFormFile> files)
        {

            var userName = User.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized(); // Not logged in
            }
            var service = _context.SectorsServices
    .Include(s => s.Sectors)
    .FirstOrDefault(s => s.SectorsServicesId == id);

            if (service == null)
                return NotFound();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var citizen = _context.Citizen.FirstOrDefault(c => c.CreateId == userId);

            var submission = new Submission
            {
                SectorsServicesId = service.SectorsServicesId,
                CitizenNationalId = citizen.CitizenNationalId, 
                SectorsId = service.SectorsId,
                SectorsServicesName = service.SectorsServicesName,
                SectorsName = service.Sectors?.SectorsName,
                SectorsServicesType = service.SectorsServicesType, 
                SubmissionName = userName,
                SubmissionStatus = "مقبول",
                IsActive = true,
                CreateId = userName,
                CreateDate = DateTime.Now,
            };

            _context.Submission.Add(submission);
            _context.SaveChanges();

            // 2. Get form fields for this service
            var fields = _context.FormFields
                .Where(f => f.SectorsServicesId == id)
                .ToList();

            foreach (var field in fields)
            {
                bool isFileField = field.FormFieldName.ToLower().Contains("image") || field.FormFieldType == "file"; 

                if (isFileField)
                {
                    var file = Request.Form.Files[field.FormFieldName];

                    if (file != null && file.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                        if (!Directory.Exists(uploadsFolder))
                            Directory.CreateDirectory(uploadsFolder);

                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        var submissionData = new SubmissionData
                        {
                            SubmissionId = submission.SubmissionId,
                            FormFieldId = field.FormFieldId,
                            SubmissionDataValue = "/Images/" + uniqueFileName, 
                            IsActive = true,
                            CreateId = userName,
                            CreateDate = DateTime.Now,
                        };

                        _context.SubmissionData.Add(submissionData);
                    }

                    continue; 
                }

                var value = form[field.FormFieldName];

                if (string.IsNullOrWhiteSpace(value))
                    continue;

                var textData = new SubmissionData
                {
                    SubmissionId = submission.SubmissionId,
                    FormFieldId = field.FormFieldId,
                    SubmissionDataValue = value,
                    IsActive = true,
                    CreateId = userName,
                    CreateDate = DateTime.Now,
                };

                _context.SubmissionData.Add(textData);
            }



            _context.SaveChanges();

            // 4. Redirect to home page
            return RedirectToAction("Index", "Home");
        }

    }
}
