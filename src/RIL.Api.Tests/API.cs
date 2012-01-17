using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RIL.Api.Tests
{
    [TestClass]
    public class API
    {
        readonly RIL Api = new RIL(Config.ApiKey);

        public API()
        {
            Api.Credentials = new Credentials(Config.Username, Config.Password);
        }

        [TestMethod]
        public void Auth()
        {
            Assert.AreEqual(Api.Auth(Config.Username, "WRONG PASSWORD").Status, Status.IncorrectCredentials);
            Assert.AreEqual(Api.Auth(Config.Username, Config.Password).Status, Status.Success);
        }

        [TestMethod]
        public void ApiStatus()
        {
            Assert.AreEqual(Api.ApiStatus().isOk, true);
            Assert.AreEqual(new RIL("INVALID API KEY", new Credentials(Config.Username, Config.Password)).ApiStatus().isOk, false);
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
            RILResponse responseExistingUser = Api.RegisterNewUser(new Credentials(Config.Username, Config.Password));
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
            RILResponse<UserStats> response = Api.Stats();
            Assert.AreEqual(response.Status, Status.Success);
            Assert.IsNotNull(response.Data);
        }

        [TestMethod]
        public void RetreiveUserList()
        {
            RILResponse<UserList> response = Api.RetreiveUserList();
            Assert.AreEqual(response.Status, Status.Success);
            Assert.IsNotNull(response.Data);

            UserListOptions options = new UserListOptions
                                          {
                                              Count = 3,
                                              Format = Format.Json,
                                              Page = 1,
                                              RetreiveType = RetreiveType.MyApp,
                                              RetriveTags = RetriveTags.Yes,
                                              State = State.All,
                                              Since = DateTime.Now.AddDays(-5)
                                          };
            response = Api.RetreiveUserList(options);
            Assert.AreEqual(response.Status, Status.Success);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(response.Data.List.Count, 3);
        }

        [TestMethod]
        public void RetreiveText()
        {
            PageContent content = Api.GetText("http://google.com", false, true);
            Assert.IsNotNull(content);
        }
    }
}
