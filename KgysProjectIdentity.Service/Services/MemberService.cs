using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;

namespace KgysProjectIdentity.Service.Services
{
    public class MemberService : IMemberService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signInManager;
        private readonly IFileProvider _fileProvider;
        private readonly ILogDifferenceDetectService _detect;
        public MemberService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context, IMapper mapper, IEmailService emailService, IFileProvider fileProvider, ILogDifferenceDetectService detect)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
            _fileProvider = fileProvider;
            _detect = detect;
        }

        async Task<UseViewModel> IMemberService.GetUseViewModelByUserNameAsync(string userName)
        {
            var currenUser = (await _userManager.FindByNameAsync(userName))!;
            return new UseViewModel
            {
                UsName = currenUser!.UserName!,
                PictureUrl = currenUser.Picture!
            };
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> CheckPasswordAsync(string userName, string oldPassword)
        {
            var currentUser = await _userManager.FindByNameAsync(userName);

            return await _userManager.CheckPasswordAsync(currentUser!, oldPassword);
        }

        public async Task<(bool, IEnumerable<IdentityError>?)> ChangePasswordAsync(string userName, string oldPassword, string newPassword)
        {

            var currentUser = (await _userManager.FindByNameAsync(userName))!;
            var resultChangePassword = await _userManager.ChangePasswordAsync(currentUser, oldPassword, newPassword);
            if (!resultChangePassword.Succeeded)
            {
                return (false, resultChangePassword.Errors);
            }
            await _userManager.UpdateSecurityStampAsync(currentUser!);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser!, newPassword, true, false);// yeni password cookiede geçerli olsun mu?, false yanlış bilgi gelirse hesap kilitlesin mi cevapları

            return (false, null);


        }

        public async Task<UserEditViewModel> GetUserEditViewModelAsync(string id)
        {

            var currentUser = await _userManager.FindByIdAsync(id);

            return new UserEditViewModel()
            {
                UserName = currentUser!.UserName!,
                FullName = currentUser.FullName!,
                Email = currentUser.Email!,
            };
        }

        public Task<string> SecurityCodeCreate()
        {
            string newCode = "";
            const string alphabet = "ABCDEFGHKLMNPRSTUVWYZabcdefghikmnoprstuvwyz0";
            const string num = "1234567890";
            Random random = new();
            for (int i = 0; i < 6; i++)
            {
                if (i < 4)
                {
                    char letterBig = alphabet[random.Next(0, alphabet.Length)];
                    newCode += Convert.ToString(letterBig);
                }
                if (i >= 4)
                {
                    char letterBig = alphabet[random.Next(0, alphabet.Length)];
                    char nums = num[random.Next(0, num.Length)];
                    newCode += Convert.ToString(letterBig) + Convert.ToString(nums);
                }
            }
            return Task.FromResult(newCode);
        }

        public Task<bool> EmailCheck(string email)
        {

            return Task.FromResult(_context.Users.Any(x => x.Email == email));
        }

        public Task SecurityCodeUsageCheck(string email)
        {
            var securityCode = _context.SecurityCode.Where(x => x.Email == email && x.Use == "Hayır");
            foreach (var code in securityCode)
            {
                var notUse = _context.SecurityCode.Find(code.Id);
                notUse!.Use = "Evet";
                _context.SecurityCode.Update(_mapper.Map<SecurityCodeModel>(notUse));


            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<bool> SecuritCodeSendAndSave(string email, string newCode)
        {
            try
            {
                var userName = _context.Users.Where(x => x.Email == email).FirstOrDefault()!.UserName;

                if (userName != null)
                {
                    await _emailService!.SendEmail(newCode, email!);
                    _context.SecurityCode.Add(new SecurityCodeModel { Email = email, SecurityCode = newCode, Use = "Hayır" });
                    _context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public Task<string> NewPasswordCreate()
        {
            string newPassword = "";
            const string alphabet = "ABCDEFGHKLMNPRSTUVWYZabcdefghikmnoprstuvwyz0";
            const string num = "1234567890";
            Random random = new();
            for (int i = 0; i < 6; i++)
            {
                if (i < 2)
                {
                    char letterBig = alphabet[random.Next(0, alphabet.Length)];
                    newPassword += Convert.ToString(letterBig);
                }
                if (i >= 4)
                {
                    char letterBig = alphabet[random.Next(0, alphabet.Length)];
                    char nums = num[random.Next(0, num.Length)];
                    newPassword += Convert.ToString(letterBig) + Convert.ToString(nums);
                }
            }
            return Task.FromResult(newPassword);
        }

        public Task<bool> SecurityCodeCheck(string code)
        {
            return Task.FromResult(_context.SecurityCode.Any(x => x.SecurityCode == code));
        }

        public Task<string> FindUser(string code)
        {
            var id = _context.SecurityCode.Where(x => x.SecurityCode == code).FirstOrDefault()!.Id;
            var security = _context.SecurityCode.Find(id);
            var email = security!.Email;

            var userName = _context.Users.Where(x => x.Email == email).FirstOrDefault()!.UserName;
            return Task.FromResult(userName!);
        }

        public Task SecurityCodeUsage(string code)
        {

            var id = _context.SecurityCode.Where(x => x.SecurityCode == code).FirstOrDefault()!.Id;
            var security = _context.SecurityCode.Find(id);
            security!.Use = "Evet";
            _context.SecurityCode.Update(_mapper.Map<SecurityCodeModel>(security));
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<bool> SecurityCodeConfirm(string code)
        {
            var check = _context.SecurityCode.Where(x => x.SecurityCode == code);
            bool codeCheck = true;
            if (check.FirstOrDefault()!.Use != "Hayır")
            {
                codeCheck = false;
            }
            return Task.FromResult(codeCheck);
        }

        public async Task<(bool, IEnumerable<IdentityError>?,string)> EditUser(UserEditViewModel request, string user)
        {
            var currentUser = await _userManager.FindByIdAsync(request.Id!);
            string log = user + " kullanıcısı " + DateTime.Now + " tarihinde " + currentUser!.UserName;
            log += _detect.UserEdit(request);

            currentUser!.UserName = request.UserName;
            currentUser.FullName = request.FullName;
            currentUser.Email = request.Email;

            if (request.Picture != null && request.Picture.Length > 0)
            {
                var wwroot = _fileProvider.GetDirectoryContents("wwwroot");

                var randomFName = $"{Guid.NewGuid()/*.ToString()*/}{Path.GetExtension(request.Picture.FileName)}";

                var newpicturePath = Path.Combine(wwroot!.First(x => x.Name == "userpictures").PhysicalPath!, randomFName);
                using var stream = new FileStream(newpicturePath, FileMode.Create);

                await request.Picture.CopyToAsync(stream);

                currentUser.Picture = randomFName;

                log += " fotoğrafını " + randomFName + ", ";
            }

            var updatedUser = await _userManager.UpdateAsync(currentUser);
            if (currentUser.Picture != null && currentUser.Picture.Length > 0)
            {
                log += "olacak şekilde güncelledi.";
            }
            await _userManager.UpdateSecurityStampAsync(currentUser);
            return (true, null,log);
        }

        public Task UserEditLogSave(string log)
        {
            log += " olarak güncelledi.";
            _context.LogModel.Add(new LogModel { Log = log });
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
