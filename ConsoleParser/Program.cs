using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using DataBaseFunctional;
using System.Data.SqlClient;

string connectionString = $"Data Source = LAPTOP-1BQG2FKL\\SQLEXPRESS; Initial Catalog = InfoMessageDB; Integrated Security=true";

string link = "https://bkrs.info/taolun/thread-316489.html";
using (IWebDriver driver = new ChromeDriver())
{
    driver.Navigate().GoToUrl(link);

    var posts = driver.FindElements(By.ClassName("post"));

    DatabaseRepository db = new DatabaseRepository();

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        foreach (var post in posts)
        {
            int id = int.Parse(post.FindElement(By.CssSelector(".number_checkbox_inner > a")).Text);
            string name = post.FindElement(By.CssSelector(".largetext > a")).Text;
            string message = post.FindElement(By.ClassName("post_body")).Text;

            db.Add(id, name, message);
        }

    }
}
