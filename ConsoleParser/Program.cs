using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using DataBaseFunctional;
using System.Data.SqlClient;

string connectionString = $"Data Source = LAPTOP-1BQG2FKL\\SQLEXPRESS; Initial Catalog = InfoMessageDB; Integrated Security=true";

string link = "https://habr.com/ru/articles/";
using (IWebDriver driver = new ChromeDriver())
{
    driver.Navigate().GoToUrl(link);

    var posts = driver.FindElements(By.TagName("article"));

    DatabaseRepository db = new DatabaseRepository();

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        foreach (var post in posts)
        {
            int id = int.Parse(post.GetAttribute("id"));
            string name = post.FindElement(By.ClassName("tm-user-info__username")).Text;
            string message = post.FindElement(By.CssSelector(".article-formatted-body.article-formatted-body")).Text;

            db.Add(id, name, message);
        }

    }
}
