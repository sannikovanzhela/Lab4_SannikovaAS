using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using DataBaseFunctional;
using System.Data.SqlClient;

string connection = "Data Source = LAPTOP-1BQG2FKL\\SQLEXPRESS; Initial Catalog = InfoMessageDB; Integrated Security=true";

IWebDriver driver = new ChromeDriver();
driver.Url = @"https://bkrs.info/taolun/thread-327516.html";

var id = driver.FindElements(By.ClassName("forum-post-number"));
var names = driver.FindElements(By.ClassName("forum-user-name"));
var messages = driver.FindElements(By.ClassName("hor-not-fit-element"));

List<string> listId = new List<string>();
List<string> listNames = new List<string>();
List<string> listMessages = new List<string>();

foreach (var item in id)
    listId.Add(item.Text);
foreach (var item in names)
    listNames.Add(item.Text);
foreach (var item in messages)
    listMessages.Add(item.Text);


DatabaseRepository db = new DatabaseRepository(connection);

for (int i = 0; i < listId.Count; i++)
{
    listId[i].Split('#');
    db.Add(Convert.ToInt32(listId[i].Substring(1)), listNames[i], listMessages[i]);
}

Thread.Sleep(2000);
driver.Quit();



//string link = "https://bkrs.info/taolun/thread-327516.html";
//using (IWebDriver driver = new ChromeDriver())
//{
//    driver.Navigate().GoToUrl(link);

//    var list = driver.FindElements(By.CssSelector(".post.classic"));

//    foreach (var item in list)
//    {
//        int id = Convert.ToInt32(item.FindElement(By.ClassName("number_checkbox_inner")));
//        string name = item.FindElement(By.ClassName("largetext")).Text;
//        string message = item.FindElement(By.ClassName("post_body")).Text;

//        DatabaseRepository database = new DatabaseRepository(connection);
//        database.Add(id, name, message);
//    }
//}
