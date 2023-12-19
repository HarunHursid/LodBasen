using Elfie.Serialization;
using LodBasen.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Xunit;
using Xunit.Sdk;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LodBasen.Pages.Auth
{
    [RequireNoAuth]
    [BindProperties]
    public class RegisterModel : PageModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Navn er nødvendigt")]
        public string Navn { get; set; } = "";

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Email er nødvendigt"), EmailAddress]
        public string Email { get; set; } = "";

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Password er nødvendigt")]
        [StringLength(50, ErrorMessage = "Password skal have en længde mellem 5 og 50", MinimumLength = 5)]
        public string Password { get; set; } = "";

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Gentag password er nødvendigt")]
        [Compare("Password", ErrorMessage = "Password og Gentag password matcher ikke")]
        public string ConfirmPassword { get; set; } = "";


        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            if (!ModelState.IsValid) 
            {
                errorMessage = "Data verificering fejlede";
                return;
            }

            //tilføj user til databasen
            string cs = "Data Source = mssql5.unoeuro.com; Initial Catalog = lodbasen_dk_db_lodbasen; Persist Security Info = True; User ID = lodbasen_dk; Password = ne2GgdbHy3DEmrkAFwtx; TrustServerCertificate = True";

            try 
            {
                using (SqlConnection connection = new SqlConnection(cs)) 
                {
                    connection.Open();
                    string sql = "INSERT INTO Users " +
                                 "(navn, email, password, role) VALUES " +
                                 "(@navn, @email, @password, 'Barn');";

					var passwordHasher = new PasswordHasher<IdentityUser>();
					string hashedPassword = passwordHasher.HashPassword(new IdentityUser(), Password);

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@navn", Navn);
						command.Parameters.AddWithValue("@email", Email);
						command.Parameters.AddWithValue("@password", hashedPassword);

						command.ExecuteNonQuery();
					}
				}

            }
			catch (Exception ex)
			{
				if (ex.Message.Contains(Email))
				{
					errorMessage = "Email er allerede brugt";
				}
				else
				{
					errorMessage = ex.Message;
				}

				return;
			}

			try
			{
				using (SqlConnection connection = new SqlConnection(cs))
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
								string role = reader.GetString(3);
								string created_at = reader.GetDateTime(4).ToString("DD/MM/YYYY");


								HttpContext.Session.SetInt32("id", id);
								HttpContext.Session.SetString("navn", navn);
								HttpContext.Session.SetString("email", email);
								HttpContext.Session.SetString("role", role);
								HttpContext.Session.SetString("created_at", created_at);
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

			successMessage = "Din bruger er nu oprettet";

			Response.Redirect("/");
        }
    }
}
