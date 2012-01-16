using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RIL.Api.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class API
    {
        private const string RILToken = "";
        readonly RIL Api = new RIL(RILToken);

        public API()
        {
            Api.Credentials = new Credentials("name", "123");
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Auth()
        {
            Assert.AreEqual(Api.Auth("name", "111").Status, Status.IncorrectCredentials);
            Assert.AreEqual(Api.Auth("name", "123").Status, Status.Success);
        }

        [TestMethod]
        public void ApiStatus()
        {
            Assert.AreEqual(Api.ApiStatus().isOk, true);
            Assert.AreEqual(new RIL("invalidtoken", new Credentials("name", "123")).ApiStatus().isOk, false);
        }

        [TestMethod]
        public void AddNewPage()
        {
            RILResponse response = Api.AddNewPage("http://google.com", "test");
            Assert.AreEqual(response.isOk, true);
        }

        [TestMethod]
        public void RegisterNewUser()
        {
            RILResponse responseExistingUser = Api.RegisterNewUser(new Credentials("name", "name"));
            Assert.AreEqual(responseExistingUser.Status, Status.IncorrectCredentials, "User with such name shoudl exists");

            //RILResponse responseNewUser = Api.RegisterNewUser(new Credentials("rilapitest8705", "rilapitest8705"));
            //Assert.AreEqual(responseNewUser.Status, Status.Success, "User with such name shoudl is a new.");
            
        }

        [TestMethod]
        public void SendChangesNewItems()
        {
            RILResponse responseExistingUser = Api.SendChanges(
                new List<Item>
                    {
                        new Item("http://microsoft.com", "MS"),
                        new Item("http://google.com/2", "2"),
                        new Item("http://google.com/3", "3")
                    }, null, null, null
                );


            Assert.AreEqual(responseExistingUser.Status, Status.Success);
        }

        [TestMethod]
        public void AddNewPages()
        {
            RILResponse response = Api.AddNewPages(new List<Item>
                                                       {
                                                           new Item("http://google.com/1", "1"),
                                                           new Item("http://google.com/2", "2"),
                                                           new Item("http://google.com/3", "3")
                                                       });
            Assert.AreEqual(response.Status, Status.Success);
        
            response = Api.AddNewPages("http://google.com/4", "4", "http://google.com/5", "5", "http://google.com/6", "6");
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void UpdateTitles()
        {
            RILResponse response = Api.UpdatePageTitles(new List<Item>
                                                       {
                                                           new Item("http://google.com/1", "1 - updated"),
                                                           new Item("http://google.com/2", "2 - updated"),
                                                           new Item("http://google.com/3", "3 - updated")
                                                       });
            Assert.AreEqual(response.Status, Status.Success);

            response = Api.UpdatePageTitles("http://google.com/4", "4 - updated", "http://google.com/5", "5 - updated", "http://google.com/6", "6 - updated");
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void UpdateTags()
        {
            RILResponse response = Api.UpdateTags(new List<Item>
                                                       {
                                                           new Item("http://google.com/1", "1 - updated") {Tags = "tag1, tag2, tag3"},
                                                           new Item("http://google.com/2", "2 - updated") {Tags = "tag1, tag2, tag3"},
                                                           new Item("http://google.com/3", "3 - updated") {Tags = "tag1, tag2, tag3"}
                                                       });
            Assert.AreEqual(response.Status, Status.Success);

            response = Api.UpdateTags("http://google.com/4", "4", "http://google.com/5", "5,any", "http://google.com/6", "tag1");
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void MarkAsReadPages()
        {
            RILResponse response = Api.MarkPagesAsRead(new List<Item>
                                                       {
                                                           new Item("http://google.com/1", "1"),
                                                           new Item("http://google.com/2", "2"),
                                                           new Item("http://google.com/3", "3")
                                                       });
            Assert.AreEqual(response.Status, Status.Success);

            response = Api.MarkPagesAsRead("http://google.com/4", "http://google.com/5", "http://google.com/6");
            Assert.AreEqual(response.Status, Status.Success);
        }

        [TestMethod]
        public void UserSats()
        {
            UserStats stats = Api.Stats();
            
            Assert.IsNotNull(stats);
        }
    }
}
