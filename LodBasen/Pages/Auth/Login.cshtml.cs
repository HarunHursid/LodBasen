using LodBasen.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace LodBasen.Pages.Auth
{
    [RequireNoAuth]
    [BindProperties]
    public class LoginModel : PageModel
    {
        [Required(ErrorMessage = "Email er nødvendigt"), EmailAddress]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password er nødvendigt")]
        public string Password { get; set; } = "";


        public string errorMessage = "";
        public string successMessage = "";

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);

            if (HttpContext.Session.GetString("role") != null) 
            {
                context.Result = new RedirectResult("/");
            }

        }
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            if (!ModelState.IsValid) 
            {
                errorMessage = "Verificering af data fejlede";
                return;
            }
            try
            {
                string connectionString = "Data Source = mssql5.unoeuro.com; Initial Catalog = lodbasen_dk_db_lodbasen; Persist Security Info = True; User ID = lodbasen_dk; Password = ne2GgdbHy3DEmrkAFwtx; TrustServerCertificate = True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM users WHERE email=@email";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", Email);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
								int id = reader.GetInt32(0);
								string navn = reader.GetString(1);
								string email = reader.GetString(2);
                                string hashedPassword = reader.GetString(3);
								string role = reader.GetString(4);
								string created_at = reader.GetDateTime(5).ToString("DD/MM/YYYY");

                                // verify the password
                                var passwordHasher = new PasswordHasher<IdentityUser>();
                                var result = passwordHasher.VerifyHashedPassword(new IdentityUser(),
                                    hashedPassword, Password);

                                if (result == PasswordVerificationResult.Success
                                    || result == PasswordVerificationResult.SuccessRehashNeeded)
                                {
                                    // successful password verification => initialize the session
                                    HttpContext.Session.SetInt32("id", id);
                                    HttpContext.Session.SetString("navn", navn);
                                    HttpContext.Session.SetString("email", email);
                                    HttpContext.Session.SetString("role", role);
                                    HttpContext.Session.SetString("created_at", created_at);

                                    // the user is authenticated successfully => redirect to the home page
                                    Response.Redirect("/Barn/GetBarn");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Wrong Email or Password
            errorMessage = "Forkert Email eller Password";
        }
    }
}
