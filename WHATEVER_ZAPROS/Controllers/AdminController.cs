using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WHATEVER_ZAPROS.Models;

namespace WHATEVER_ZAPROS.Controllers
{
	public class AdminController : Controller
	{
		public async Task<IActionResult> Index()
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

        public async Task<IActionResult> FavoriteAdmin()
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
        public ViewResult SearchAdmin() => View();
        [HttpPost]
        public async Task<IActionResult> SearchAdmin(string header)
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

        public async Task<IActionResult> ArticlelanguageAdmin(int id)
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

        public async Task<IActionResult> ArticleAdmin()
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

        [HttpPost]
        public async Task<IActionResult> AddFavorite(int idArticle)
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
            return RedirectToAction("FavoriteAdmin");
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
                    using (var response = await httpClient.DeleteAsync("https://192.168.117.253:7046/api/Favorites/UART?userId=" + authUser.IdUser + "&artId=" + idArticles))
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
            return RedirectToAction("FavoriteAdmin");
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
            return RedirectToAction("ArticleAdmin");
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
            return RedirectToAction("ArticleAdmin");
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
            return RedirectToAction("ArticleAdmin");
        }

        public async Task<IActionResult> StatusArticle()
        {
            List<Article> articles = new List<Article>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync("https://192.168.117.253:7046/api/Articles/status"))
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
        public async Task<IActionResult> AcceptArticle(int id)
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
        public async Task<IActionResult> AcceptArticle(string header, string text, int languageProgrammingId,int UserId, int id)
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
                StatusArticleId = 2,
                UserId = UserId,
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
            return RedirectToAction("StatusArticle");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteArticleAccept(string header, string text, int languageProgrammingId, int idArticle)
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
            return RedirectToAction("StatusArticle");
        }

    }
}
