using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using DataBaseFunctional;
using System.Data.SqlClient;

string connectionString = $"Data Source = LAPTOP-1BQG2FKL\\SQLEXPRESS; Initial Catalog = InfoMessageDB; Integrated Security=true";

string link = "https://www.banki.ru/forum/?PAGE_NAME=read&FID=61&TID=203136";
using (IWebDriver driver = new ChromeDriver())
{
    driver.Navigate().GoToUrl(link);

    var posts = driver.FindElements(By.TagName("table"));

    DatabaseRepository db = new DatabaseRepository();

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        foreach (var post in posts)
        {
            int id = int.Parse(post.GetAttribute("id"));
            string name = post.FindElement(By.ClassName("forum-user-name")).Text;
            string message = post.FindElement(By.CssSelector(".hor-not-fit-element__content.display-block")).Text;

            db.Add(id, name, message);
        }

    }
}
