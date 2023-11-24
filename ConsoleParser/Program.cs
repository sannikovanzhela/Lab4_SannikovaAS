using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using DataBaseFunctional;
using System.Data.SqlClient;

string connection = "Data Source = LAPTOP-1BQG2FKL\\SQLEXPRESS; Initial Catalog = InfoMessageDB; Integrated Security=true";

//IWebDriver driver = new ChromeDriver();
//driver.Url = @"https://www.banki.ru/forum/?PAGE_NAME=read&FID=61&TID=203136";

//var id = driver.FindElements(By.ClassName("forum-post-number"));
//var names = driver.FindElements(By.ClassName("forum-user-nam"));
//var messages = driver.FindElements(By.ClassName("hor-not-fit-element__content display-block"));

//List<string> listId = new List<string>();
//List<string> listNames = new List<string>();
//List<string> listMessages = new List<string>();

//foreach (var item in id)
//    listId.Add(item.Text);
//foreach (var item in names)
//    listNames.Add(item.Text);
//foreach (var item in messages)
//    listMessages.Add(item.Text);


//DatabaseRepository db = new DatabaseRepository(connection);

//for (int i = 0; i < listId.Count; i++)
//{
//    listId[i].Split('#');
//    db.Add(Convert.ToInt32(listId[i].Substring(1)), listNames[i], listMessages[i]);
//}

//Thread.Sleep(2000);
//driver.Quit();

string link = "https://www.banki.ru/forum/?PAGE_NAME=read&FID=61&TID=203136";
using (IWebDriver driver = new ChromeDriver())
{
    driver.Navigate().GoToUrl(link);

    var list = driver.FindElements(By.ClassName("hor-not-fit-element"));

    foreach (var item in list)
    {
        string number = item.FindElement(By.ClassName("forum-post-number")).Text;
        int id = int.Parse(item.GetAttribute("id"));
        string name = item.FindElement(By.ClassName("forum-user-name")).Text;
        string message = item.FindElement(By.ClassName("hor-not-fit-element")).Text;

        DatabaseRepository database = new DatabaseRepository(connection);
        database.Add(id, name, message);
    }
}

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
