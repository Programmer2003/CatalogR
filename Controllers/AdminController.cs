using System.Net;
using Newtonsoft.Json;
using CatalogR.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CatalogR.Controllers
{
    [Authorize]
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : Controller
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService) => _adminService = adminService;

        public async Task<IActionResult> Index() => View(await _adminService.GetUsersAsync());

        [HttpPost]
        public async Task<JsonResult> UsersChangeBlockStatus(string data, bool block)
        {
            var userIds = UsersFromJson(data);
            if (userIds == null) return Json(HttpStatusCode.OK);

            await _adminService.ChangeBlockStatus(userIds, block);
            return Json(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<JsonResult> UsersChangeRole(string data, bool admin)
        {
            var userIds = UsersFromJson(data);
            if (userIds == null) return Json(HttpStatusCode.OK);

            await _adminService.ChangeRole(userIds, admin);
            return Json(HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteUsers(string data)
        {
            var userIds = UsersFromJson(data);
            if (userIds == null) return Json(HttpStatusCode.OK);

            await _adminService.RemoveUsers(userIds);
            return Json(HttpStatusCode.OK);
        }

        private static List<string>? UsersFromJson(string data) => JsonConvert.DeserializeObject<List<string>>(data);
    }
}
