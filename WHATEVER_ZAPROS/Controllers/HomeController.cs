using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using WHATEVER_ZAPROS.Models;

namespace WHATEVER_ZAPROS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            if (HttpContext.Session.Keys.Contains("AuthJson"))
            {
                string json = HttpContext.Session.GetString("AuthJson");
                User authUser = new User();
                authUser = JsonConvert.DeserializeObject<User>(json);


                if (authUser.RoleId == 1)
                {

                    return RedirectToAction("UserMenu");
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(User user)
        {

            User authUser = new User();

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://192.168.117.253:7046/api/Users/auth", content))
                    {
                        try
                        {
                            if(response.IsSuccessStatusCode)
                            {
                                string apiResp = await response.Content.ReadAsStringAsync();
                                authUser = JsonConvert.DeserializeObject<User>(apiResp);

                                HttpContext.Session.SetString("AuthJson", apiResp);
                                await Cookie(apiResp);

                                if (authUser.RoleId == 1)
                                {
                                    
                                    return RedirectToAction("UserMenu");
                                }
                                else
                                {
                                    return RedirectToAction("Index", "Admin");
                                }

                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }
            }
        }

        private async Task Cookie(string response)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, response)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims,"ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("AuthJson");
            return RedirectToAction("Index");
        }

        public ViewResult SignUp() => View();

        [HttpPost]
        public async Task<IActionResult> SignUp(string loginUser, string passwordUser, string email)
        {
            User user = new User()
            {
                LoginUser = loginUser,
                PasswordUser = passwordUser,
                Email = email,
                RoleId = 1,
                StatusUserId = 1
                
            };
            User authUser = new User();

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://192.168.117.253:7046/api/Users", content))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResp = await response.Content.ReadAsStringAsync();
                                authUser = JsonConvert.DeserializeObject<User>(apiResp);

                                return RedirectToAction("Index");

                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }
            }
        }
        public async Task<IActionResult> UserMenu()
        {
            List<Article> articles = new List<Article>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync("https://192.168.117.253:7046/api/Articles"))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                articles = JsonConvert.DeserializeObject<List<Article>>(apiResponse);
                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                      
                    }
                }
            }

            return View(articles);
        }

        public async Task<IActionResult> MoreArticle(int id)
        {
            Article articles = new Article();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync("https://192.168.117.253:7046/api/Articles/" + id))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                articles = JsonConvert.DeserializeObject<Article>(apiResponse);
                                
                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }
            }

            return View(articles);
        }

        [HttpPost]
        public async Task<IActionResult> Add_to_Favorites(int idArticle)
        {
            string json = HttpContext.Session.GetString("AuthJson");
            User authUser = new User();
            authUser = JsonConvert.DeserializeObject<User>(json);
            Favorite favorite = new Favorite()
            {
                ArticleId = idArticle,
                UserId = authUser.IdUser
            };
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(favorite), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://192.168.117.253:7046/api/Favorites", content))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResp = await response.Content.ReadAsStringAsync();
                                favorite = JsonConvert.DeserializeObject<Favorite>(apiResp);
                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }
            }
            return RedirectToAction("FavoriteUser");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFavorites(int idArticles)
        {
            string json = HttpContext.Session.GetString("AuthJson");
            User authUser = new User();
            authUser = JsonConvert.DeserializeObject<User>(json);
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.DeleteAsync("https://192.168.117.253:7046/api/Favorites/UART?userId=" +authUser.IdUser + "&artId=" + idArticles))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResp = await response.Content.ReadAsStringAsync();
                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }
            }
            return RedirectToAction("FavoriteUser");
        }

        public async Task<IActionResult> FavoriteUser()
        {
            List<Favorite> favorites = new List<Favorite>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    string json = HttpContext.Session.GetString("AuthJson");
                    User authUser = new User();
                    authUser = JsonConvert.DeserializeObject<User>(json);
                    using (var response = await httpClient.GetAsync("https://192.168.117.253:7046/api/Favorites/user/" + authUser.IdUser))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                favorites = JsonConvert.DeserializeObject<List<Favorite>>(apiResponse);
                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }

                    }
                }
            }
            return View(favorites);
        }

        public ViewResult Search() => View();
        [HttpPost]
        public async Task<IActionResult> Search(string header)
        {
            List<Article> articles = new List<Article>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync("https://192.168.117.253:7046/api/Articles?header=" + header))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                articles = JsonConvert.DeserializeObject<List<Article>>(apiResponse);

                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }
            }

            return View(articles);
        }

        public async Task<IActionResult> Articlelanguage(int id)
        {
            List<Article> articles = new List<Article>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync("https://192.168.117.253:7046/api/Articles/language?languageId=" + id))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                articles = JsonConvert.DeserializeObject<List<Article>>(apiResponse);

                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }
            }

            return View(articles);
        }

        public async Task<IActionResult> ArticleUser()
        {
            List<Article> articles = new List<Article>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    string json = HttpContext.Session.GetString("AuthJson");
                    User authUser = new User();
                    authUser = JsonConvert.DeserializeObject<User>(json);
                    using (var response = await httpClient.GetAsync("https://192.168.117.253:7046/api/Articles/userID?uid=" + authUser.IdUser))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                articles = JsonConvert.DeserializeObject<List<Article>>(apiResponse);

                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }
            }

            return View(articles);
        }
        public async Task<IActionResult> AddArticle()
        {
            List<LanguageProgramming> languages = new List<LanguageProgramming>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync("https://192.168.117.253:7046/api/LanguageProgrammings"))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                languages = JsonConvert.DeserializeObject<List<LanguageProgramming>>(apiResponse);

                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }
            }

            return View(languages);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(string header, string text, int languageID)
        {
            string json = HttpContext.Session.GetString("AuthJson");
            User authUser = new User();
            authUser = JsonConvert.DeserializeObject<User>(json);
            Article article = new Article()
            {
                Header = header,
                Text = text,
                DateTimeArticle = DateTime.Now,
                LanguageProgrammingId = languageID,
                StatusArticleId = 1,
                UserId = authUser.IdUser

            };

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                { 
                    StringContent content = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://192.168.117.253:7046/api/Articles", content))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResp = await response.Content.ReadAsStringAsync();
                                article = JsonConvert.DeserializeObject<Article>(apiResp);
                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }
            }
            return RedirectToAction("ArticleUser");
        }

        public async Task<IActionResult> UpdateArticle(int id)
        {
            Article articles = new Article();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync("https://192.168.117.253:7046/api/Articles/" + id))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                articles = JsonConvert.DeserializeObject<Article>(apiResponse);

                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }
            }

            List<LanguageProgramming> languages = new List<LanguageProgramming>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync("https://192.168.117.253:7046/api/LanguageProgrammings"))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                languages = JsonConvert.DeserializeObject<List<LanguageProgramming>>(apiResponse);

                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }
            }

            ViewModel model = new ViewModel()
            {
                IdArticle = articles.IdArticle,
                Header = articles.Header,
                Text = articles.Text,
                DateTimeArticle = articles.DateTimeArticle,
                LanguageProgrammingId = articles.LanguageProgrammingId,
                StatusArticleId = articles.StatusArticleId,
                UserId = articles.UserId,
                language = languages
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateArticle(string header, string text, int languageProgrammingId, int id)
        {
            string json = HttpContext.Session.GetString("AuthJson");
            User authUser = new User();
            authUser = JsonConvert.DeserializeObject<User>(json);
            Article article = new Article()
            {
                IdArticle = id,
                Header = header,
                Text = text,
                LanguageProgrammingId = languageProgrammingId,
                DateTimeArticle = DateTime.Now,
                StatusArticleId = 1,
                UserId = authUser.IdUser,
            };
            Article receivedArticle = new Article();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync("https://192.168.117.253:7046/api/Articles/" + id, content))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                receivedArticle = JsonConvert.DeserializeObject<Article>(apiResponse);

                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }
            
            }
            return RedirectToAction("ArticleUser");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteArticle(string header, string text, int languageProgrammingId, int idArticle)
        {
            string json = HttpContext.Session.GetString("AuthJson");
            User authUser = new User();
            authUser = JsonConvert.DeserializeObject<User>(json);
            Article article = new Article()
            {
                IdArticle = idArticle,
                Header = header,
                Text = text,
                LanguageProgrammingId = languageProgrammingId,
                DateTimeArticle = DateTime.Now,
                StatusArticleId = 3,
                UserId = authUser.IdUser,
            };
            Article receivedArticle = new Article();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync("https://192.168.117.253:7046/api/Articles/" + idArticle, content))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                receivedArticle = JsonConvert.DeserializeObject<Article>(apiResponse);

                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }

            }
            return RedirectToAction("ArticleUser");
        }

        public async Task<IActionResult> DeleteMyself()
        {
            string json = HttpContext.Session.GetString("AuthJson");
            User authUser = new User();
            authUser = JsonConvert.DeserializeObject<User>(json);

            User user = new User() 
            { 
                IdUser = authUser.IdUser,
                LoginUser = authUser.LoginUser,
                PasswordUser = authUser.PasswordUser,
                Email = authUser.Email,
                RoleId = authUser.RoleId,
                StatusUserId = 2
            };

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync("https://192.168.117.253:7046/api/Users/" + user.IdUser, content))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }

            }
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("AuthJson");
            return RedirectToAction("Index");
        }

        public IActionResult RestoreUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RestoreUser(string login)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync("https://192.168.117.253:7046/api/Users/restore?Login=" + login))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                User user = new User();
                                user = JsonConvert.DeserializeObject<User>(apiResponse);
                                TempData["RestoreUser"] = apiResponse;
                               
                                int Code;
                                Random ran = new Random();
                                MailAddress from = new MailAddress("jallios@mail.ru", "WHATEVER");
                                MailAddress to = new MailAddress(user.Email);
                                MailMessage m = new MailMessage(from, to);
                                m.Subject = "Служба поддержки WHATEVER";
                                Code = ran.Next(100000, 999999);
                                m.Body = "Код: " + Code;
                                m.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                                smtp.Credentials = new NetworkCredential("jallios@mail.ru", "L0ysg6shst6yKwByg0L0");
                                smtp.EnableSsl = true;
                                smtp.Send(m);
                                TempData["Code"] = Code;
                            }
                            else return NotFound();
                        }
                        catch (Exception ex)
                        {
                            return NotFound();
                        }
                    }
                }
            }
            return RedirectToAction("RestorePassword");
        }

        public IActionResult RestorePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RestorePassword(int Ucode, string password, string newpassword)
        {
            try
            {
                string UserByEmail;
                int Code;

                UserByEmail = TempData["RestoreUser"].ToString();
                Code = Int32.Parse(TempData["Code"].ToString());


                if (Code == Ucode)
                {
                    if(password == newpassword)
                    {
                        User user = new User();
                        user = JsonConvert.DeserializeObject<User>(UserByEmail);

                        User userRestore = new User()
                        {
                            IdUser = user.IdUser,
                            LoginUser = user.LoginUser,
                            PasswordUser = newpassword,
                            Email = user.Email,
                            RoleId = user.RoleId,
                            StatusUserId = 1
                        };


                        using (var httpClientHandler = new HttpClientHandler())
                        {
                            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                            using (var httpClient = new HttpClient(httpClientHandler))
                            {
                                StringContent content = new StringContent(JsonConvert.SerializeObject(userRestore), Encoding.UTF8, "application/json");

                                using (var response = await httpClient.PutAsync("https://192.168.117.253:7046/api/Users/" + user.IdUser, content))
                                {
                                    try
                                    {
                                        if (response.IsSuccessStatusCode)
                                        {
                                            string apiResponse = await response.Content.ReadAsStringAsync();
                                        }
                                        else return NotFound();
                                    }
                                    catch (Exception ex)
                                    {
                                        return NotFound();
                                    }
                                }
                            }

                        }

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}