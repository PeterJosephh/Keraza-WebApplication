using KerazaWebApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KerazaWebApplication.Controllers
{
    public class FileController : Controller
    {
        private readonly GoogleDriveService _googleDriveService;
        private readonly ApplicationDbContext _context;


        public FileController(GoogleDriveService googleDriveService, ApplicationDbContext context)
        {
            _googleDriveService = googleDriveService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Upload(int id,string FileType)
        {
                ViewBag.SermonId = id;
                ViewBag.FileType = FileType;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, int SermonId, string FileType)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        string fileId = await _googleDriveService.UploadFileAsync(stream, file.FileName, file.ContentType, "16x2piFuVrZC1EWNhUvC9IDsFZe8y6BCL");
                        if (FileType == "DocFile")
                        {
                            _context.Sermons.FirstOrDefault(s => s.Id == SermonId).PdfDriveId = fileId;
                            _context.SaveChanges();
                            return RedirectToAction("index", "sermons", SermonId);
                        }
                        if (FileType == "AudioFile")
                        {
                            _context.Sermons.FirstOrDefault(s => s.Id == SermonId).AudioDriveId = fileId;
                            _context.SaveChanges();
                            return RedirectToAction("index", "sermons", SermonId);
                        }
                    }

                }
                ModelState.AddModelError("", "Please upload a file.");
                return View();

            }
            else {

                ModelState.AddModelError("", "Please upload a file.");

                return View();
            }
        }



        public async Task<IActionResult> Delete()
        {
            await _googleDriveService.DeleteFileAsync("15vlTDoiDRxHwIqaS4rIio3Hb2Uu2I8AG");
            return Ok();
        }



        // Action to handle file upload and replacement
        [HttpPost]
        public async Task<IActionResult> UploadAndReplaceImage(IFormFile newFile)
        {
            string oldFileId = "";

            if (newFile == null || string.IsNullOrEmpty(oldFileId))
            {
                return BadRequest("Invalid file or file ID.");
            }

            try
            {
                // Step 1: Delete the old file
                await _googleDriveService.DeleteFileAsync(oldFileId);

                // Step 2: Upload the new file
                string mimeType = newFile.ContentType;  // Get MIME type from the file
                using (var stream = newFile.OpenReadStream())
                {
                    string newFileUrl = await _googleDriveService.UploadFileAsync(stream, newFile.FileName, mimeType);
                    return Ok(new { newFileUrl });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
