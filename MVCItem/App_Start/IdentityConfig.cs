using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MVCItem.Models;
using System.Net;

namespace MVCItem
{

    public class ApplicationRoleManger : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManger(IRoleStore<ApplicationRole, string> store) : base(store)
        {

            

        }
        public static ApplicationRoleManger Create(IdentityFactoryOptions<ApplicationRoleManger> options ,IOwinContext context )
        {
            return new ApplicationRoleManger( new RoleStore<ApplicationRole>(context.Get<ApplicationDbContext>() ));


        }
    }
    public class EmailService : IIdentityMessageService
    {
        
      

        public Task SendAsync(IdentityMessage message)
        {

            SmtpClient smtpClient = new SmtpClient("smtp.139.com");
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential()
            {
                UserName = "18721600247@139.com",
                Password = "ycr111450"
            };

            MailMessage mailMessage = new MailMessage();
            mailMessage.Body = message.Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = message.Subject;
            mailMessage.Sender = new MailAddress("18721600247@139.com", "yuchengren");
            //  mailMessage.ReplyTo = new MailAddress("18721600247@139.com");
            mailMessage.ReplyToList.Add(new MailAddress("18721600247@139.com"));
            mailMessage.ReplyToList.Add(new MailAddress("928184371@139.com"));
            mailMessage.To.Add(new MailAddress("539928505@QQ.COM"));
            mailMessage.From = new MailAddress("18721600247@139.com");
            smtpClient.SendCompleted += SmtpClient_SendCompleted;
        
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
               
            }
            // 在此处插入电子邮件服务可发送电子邮件。
            
          
            return Task.FromResult(0);
        }

        private void SmtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            HttpResponse hr = e.UserState as HttpResponse;
            if (e.Error != null)
            {
                hr.Redirect("http://www.baidu.com");
            }
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入 SMS 服务可发送短信。
            return Task.FromResult(0);
        }
    }

    // 配置此应用程序中使用的应用程序用户管理器。UserManager 在 ASP.NET Identity 中定义，并由此应用程序使用。
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // 配置用户名的验证逻辑
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // 配置密码的验证逻辑
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
              //  RequireNonLetterOrDigit = true,
              //  RequireDigit = true,
              //  RequireLowercase = true,
              //  RequireUppercase = true,
            };

            // 配置用户锁定默认值
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // 注册双重身份验证提供程序。此应用程序使用手机和电子邮件作为接收用于验证用户的代码的一个步骤
            // 你可以编写自己的提供程序并将其插入到此处。
            manager.RegisterTwoFactorProvider("电话代码", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "你的安全代码是 {0}"
            });
            manager.RegisterTwoFactorProvider("电子邮件代码", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "安全代码",
                BodyFormat = "你的安全代码是 {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // 配置要在此应用程序中使用的应用程序登录管理器。
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            return base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
        }
        public async Task<SignInStatus> SignInAsync( HttpResponseBase httpResponseBase, ApplicationUser user, bool isPersistent, bool rememberBrowser)
        {
            if (user.EmailConfirmed ==false)
            {
                httpResponseBase.Write("<script>alert('请验证邮箱')</script>");

               return   await Task.FromResult( SignInStatus.Failure); 
            }
            else
            {
               
               await base.SignInAsync(user, isPersistent, rememberBrowser);
               return  await Task.FromResult(SignInStatus.Success);

            }
           
        }
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
           
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {

            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
