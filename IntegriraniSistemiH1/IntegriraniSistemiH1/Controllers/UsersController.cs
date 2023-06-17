using CsvHelper;
using CsvHelper.Configuration;
using IntegriraniSistemiH1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;

namespace IntegriraniSistemiH1.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: UsersController
        public ActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public FileResult DownloadCSV()
        {
            // Generate the CSV content
            var csvContent = "UserName,Password,Role\n";
            csvContent += "test@gmail.com,TestPassword,(User or Admin)\n";

            // Convert the CSV content to bytes
            var csvBytes = Encoding.UTF8.GetBytes(csvContent);

            // Return the file as a FileResult
            return File(csvBytes, "text/csv", "users.csv");
        }

        public async Task<IActionResult> ProcessCSV(IFormFile csvFile)
        {
            if (csvFile != null && csvFile.Length > 0)
            {
                using (var reader = new StreamReader(csvFile.OpenReadStream()))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                }))
                {
                    var records = csv.GetRecords<UserImport>().ToList();
                    foreach(var record in records)
                    {
                        PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();

                        var user = new ApplicationUser()
                        {
                            Id = Guid.NewGuid().ToString("D"),
                            Email = record.UserName,
                            NormalizedEmail = record.UserName.ToUpper(),
                            EmailConfirmed = true,
                            UserName = record.UserName,
                            NormalizedUserName = record.UserName.ToUpper(),
                            SecurityStamp = Guid.NewGuid().ToString("D"),
                        };
                        user.PasswordHash = hasher.HashPassword(user, record.Password);

                        await _userManager.CreateAsync(user);

                        if(record.Role.ToLower() == "admin")
                            await _userManager.AddToRoleAsync(user, "Admin");
                        else
                            await _userManager.AddToRoleAsync(user, "User");

                        await _userManager.UpdateAsync(user);

                    }


                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
