using DataBaseFunctional;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Xml.Linq;

namespace TestDB
{
    public class Tests
    {
        DatabaseRepository db;

        [SetUp]
        public void Init() 
        {
            db = new DatabaseRepository();
        }

        [Test]
        public void GetByIdExceptionID()
        {
            var except = "ID cannot be zero";
            Assert.That(db.GetByID(-12), Is.EqualTo(except));
        }

        [Test]
        public void GetByIdExceptionNotExist()
        {
            var except = "This Information isn't exist";
            Assert.That(db.GetByID(12), Is.EqualTo(except));
        }

        [Test]
        public void GetById()
        {
            var except = "ID: 1\nName: name\nMessage: message";
            Assert.That(db.GetByID(1), Is.EqualTo(except));
        }

        [TestCase(null)]
        [TestCase("")]
        public void GetByNameIsEmpty(string name)
        {
            var except = "Name is empty";
            Assert.That(db.GetByName(name), Is.EqualTo(except));
        }

        [Test]
        public void GetByNameNotExist()
        {
            var except = "Information isn't exist";
            Assert.That(db.GetByName("nnnnnn"), Is.EqualTo(except));
        }

        [Test]
        public void GetByName()
        {
            var except = "ID: 1\nName: name\nMessage: message";
            Assert.That(db.GetByName("name"), Is.EqualTo(except));
        }

        [Test]
        public void AddExceptionID()
        {
            var except = "ID should be more zero";
            Assert.That(db.Add(-1, "name", "message"), Is.EqualTo(except));
        }

        [Test]
        public void AddNameIsEmpty()
        {
            var except = "Name is empty";
            Assert.That(db.Add(12, "", "message"), Is.EqualTo(except));
        }

        [Test]
        public void AddMessageIsEmpty()
        {
            var except = "Message is empty";
            Assert.That(db.Add(17, "name", ""), Is.EqualTo(except));
        }

        [Test]
        public void AddIsExist()
        {
            var except = "This Information is exist";
            Assert.That(db.Add(1, "name", "message"), Is.EqualTo(except));
        }

        [Test]
        public void AddInformation()
        {
            var except = "Information is added";
            Assert.That(db.Add(4, "name", "message"), Is.EqualTo(except));
        }

        [Test]
        public void DeleteExceptionID()
        {
            var except = "ID should be more zero";
            Assert.That(db.Delete(0), Is.EqualTo(except));
        }

        [Test]
        public void DeleteNotExist()
        {
            var except = "Information isn't exist";
            Assert.That(db.Delete(10), Is.EqualTo(except));
        }

        [Test]
        public void DeleteInformation()
        {
            var except = "Information is deleted";
            Assert.That(db.Delete(2), Is.EqualTo(except));
        }

        [Test]
        public void UpdateExceptionID()
        {
            var except = "ID should be more zero";
            Assert.That(db.Update(0, "message"), Is.EqualTo(except));
        }

        [Test]
        public void UpdateMessageIsEmpty()
        {
            var except = "Message is empty";
            Assert.That(db.Update(1, ""), Is.EqualTo(except));
        }

        [Test]
        public void UpdateIsNotExist()
        {
            var except = "This Information isn't exist";
            Assert.That(db.Update(100, "message"), Is.EqualTo(except));
        }

        [Test]
        public void UpdateInformation()
        {
            var except = "Information is updated";
            Assert.That(db.Update(2, "new message"), Is.EqualTo(except));
        }

        [TearDown]
        public void CleanUp()
        {
            db = null;
        }
    }
}