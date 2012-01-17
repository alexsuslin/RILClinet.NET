using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using RIL.Constants;
using RIL.Objects;
using RestSharp;
using RestSharp.Deserializers;

namespace RIL
{
    public class RIL
    {
        #region Fields & Consts

        public const string BaseUrl = "https://readitlaterlist.com/v2/";

        private readonly string key;
        private readonly string ApiUrl;

        #endregion

        #region Properties

        public Credentials Credentials { get; set; }

        #endregion

        #region Constructors

        public RIL(string token, Credentials credentials = null, string baseUrl = BaseUrl)
        {
            key = token;
            ApiUrl = baseUrl;
            Credentials = credentials;

        }

        #endregion

        #region Methods

        #region Add

        /// <summary>
        /// The add method is provided as a quick way to save one link to a users list. 
        /// If you have more than one link to save do not use this method. 
        /// Instead, use the send method which allows you to send multiple changes in one request.
        /// </summary>
        /// <param name="url">The url of a page to add.</param>
        /// <param name="title">
        /// For the best user experience, if you do not have a title, supply some unique context if possible. 
        /// For example, a twitter client might use the tweet that went along with the link.
        /// </param>
        /// <param name="ref_id">
        /// If you are adding RIL support to a Twitter client, please send along a reference 
        /// to the tweet status id in the ref_id parameter. This allows Read It Later to show the 
        /// original message alongside the article. See this post for an example.
        /// </param>
        /// <returns></returns>
        public RILResponse AddNewPage(string url, string title = "", string ref_id = "")
        {
            RestRequest request = new RestRequest(Methods.AddNewPage);
            request.AddParameter(Methods.Params.Url, url);
            if (!string.IsNullOrEmpty(title))
                request.AddParameter(Methods.Params.Title, title);
            if (!string.IsNullOrEmpty(ref_id))
                request.AddParameter(Methods.Params.RefID, ref_id);
            return Execute(request);
        }

        public RILResponse AddNewPage(Item item)
        {
            return AddNewPage(item.Url, item.Title, item.RefID);
        }

        #endregion

        #region Send

        /// <summary>
        /// This method allows you to make changes to a user's reading list. It supports adding new pages, 
        /// marking pages as read, changing titles, or updating tags. Multiple changes to items can be made in one request.
        /// </summary>
        /// <param name="newitems">new page(s) to add to list</param>
        /// <param name="read">page(s) to be marked as read</param>
        /// <param name="update_title">changed titles for items</param>
        /// <param name="update_tags">
        ///     new set of tags for item
        ///     NOTE: This replaces all of the tags for the item with the new ones you provide.
        /// </param>
        /// <returns></returns>
        public RILResponse SendChanges(IList<Item> newitems, IList<Item> read, IList<Item> update_title, IList<Item> update_tags)
        {
            RestRequest request = new RestRequest(Methods.SendChangesToList);
            if (newitems != null && newitems.Count > 0)
                request.AddParameter(Methods.Params.New, request.JsonSerializer.Serialize(Helper.ConvertToDictionary(newitems)));
            if (read != null && read.Count > 0)
                request.AddParameter(Methods.Params.Read, request.JsonSerializer.Serialize(Helper.ConvertToDictionary(read)));
            if (update_title != null && update_title.Count > 0)
                request.AddParameter(Methods.Params.UpdateTitles, request.JsonSerializer.Serialize(Helper.ConvertToDictionary(update_title)));
            if (update_tags != null && update_tags.Count > 0)
                request.AddParameter(Methods.Params.UpdateTags, request.JsonSerializer.Serialize(Helper.ConvertToDictionary(update_tags)));
            return Execute(request);
        }

        public RILResponse AddNewPages(IList<Item> newitems)
        {
            return SendChanges(newitems, null, null, null);
        }

        public RILResponse AddNewPages(params string[] items)
        {
            return AddNewPages(ConvertToList(false, items));
        }

        public RILResponse MarkPagesAsRead(IList<Item> readpages)
        {
            return SendChanges(null, readpages, null, null);
        }

        public RILResponse MarkPagesAsRead(params string[] readpages)
        {
            return MarkPagesAsRead(readpages.Select(url => new Item(url)).ToList());
        }

        public RILResponse UpdatePageTitles(IList<Item> titles)
        {
            return SendChanges(null, null, titles, null);
        }

        /// <summary>
        /// every item is url, the next item should be title
        /// </summary>
        /// <param name="items"> </param>
        /// <returns></returns>
        public RILResponse UpdatePageTitles(params string[] items)
        {
            return UpdatePageTitles(ConvertToList(false, items));
        }

        public RILResponse UpdateTags(IList<Item> tags)
        {
            return SendChanges(null, null, null, tags);
        }

        public RILResponse UpdateTags(params string[] items)
        {
            return UpdateTags(ConvertToList(true, items));
        }

        #endregion

        #region Get

        public RILResponse<UserList> RetreiveUserList(UserListOptions options = null)
        {
            RestRequest request = new RestRequest(Methods.RetreiveUserList);
            if (options != null)
            {
                request.AddDesrelializedObject(options);
            }
            return Execute<UserList>(request);
        }

        #endregion

        #region Stats
        
        public RILResponse<UserStats> Stats(string format = "json")
        {
            RestRequest request = new RestRequest(Methods.RetreiveUserStats);
            if (format != Methods.Params.JsonFormat && format != Methods.Params.XmlFormat)
                throw new Exception("Format should be either json or xml.");
            request.AddParameter(Methods.Params.Format, format);
            return Execute<UserStats>(request);
        }

        #endregion

        #region Auth

        /// <summary>
        /// The authentication method is used to verify a supplied username and password is correct 
        /// (for example when prompting a user for their credentials for the first time). 
        /// It does not need to be called before using any methods below.
        /// </summary>
        /// <returns></returns>
        public RILResponse Auth()
        {
            return Execute(new RestRequest(Methods.Authentication));
        }

        public RILResponse Auth(string username, string password)
        {
            return Execute(new RestRequest(Methods.Authentication), new Credentials(username, password));
        }

        #endregion

        #region SignUp

        /// <summary>
        /// This method allows you to create a new user account.
        /// Special Case: The 401 status code will be returned when the supplied username is already taken. 
        /// The X-Error header will also say this in english.
        /// </summary>
        /// <param name="credentials">Username and Password of the user to create.</param>
        /// <returns></returns>
        public RILResponse RegisterNewUser(Credentials credentials)
        {
            RestRequest request = new RestRequest(Methods.RegisterUser);
            return Execute(request, credentials);
        }

        #endregion

        #region Text

        public RILResponse<PageContent> GetText(string url, bool more = false, bool images = false)
        {
            RestRequest request = new RestRequest(Methods.GetTextOnlyVersion);
            request.AddParameter(Methods.Params.Url, url);
            request.AddParameter(Methods.Params.Mode, more ? Methods.Params.More : Methods.Params.Less);
            request.AddParameter(Methods.Params.Images, images ? 1 : 0);
            IRestResponse response = Execute(request).Response;
            RILResponse<PageContent> rilResponse = new RILResponse<PageContent>(response);
            rilResponse.Data = new PageContent(rilResponse);
            return rilResponse;
        }

        #endregion

        #region Api

        /// <summary>
        /// This method allows you to check your current rate limit status. 
        /// Calls to this method do not count against the rate limit.
        /// </summary>
        /// <returns></returns>
        public RILResponse ApiStatus()
        {
            return Execute(new RestRequest(Methods.ApiStatus));
        }

        #endregion

        #endregion

        #region Helper Methods

        private RILResponse<T> Execute<T>(RestRequest request, Credentials cred = null) where T : new()
        {
            RestClient client = new RestClient(ApiUrl);
            request = RequestSetup(request, cred);
            return new RILResponse<T>(client.Execute<T>(request));
        }

        private RILResponse Execute(RestRequest request, Credentials cred = null)
        {
            RestClient client = new RestClient(ApiUrl);
            request = RequestSetup(request, cred);
            return new RILResponse(client.Execute(request));
        }

        private IList<Item> ConvertToList(bool isSecondParameterTags, params string[] item)
        {
            if (item.Length % 2 != 0)
                throw new Exception("The number of parameters should be even.");
            IList<Item> items = new List<Item>();
            for (int i = 0; i < item.Length; i += 2)
            {
                Item listItems = new Item(item[i]);
                if (isSecondParameterTags)
                    listItems.Tags = item[i + 1];
                else
                    listItems.Title = item[i + 1];
                items.Add(listItems);
            }
            return items;
        }

        private RestRequest RequestSetup(RestRequest request, Credentials cred = null)
        {
            request.Resource = !request.Resource.EndsWith(".php") ? string.Format("{0}.php", request.Resource) : request.Resource;
            request.Method = Method.POST;
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(Methods.Params.ApiKey, key, ParameterType.GetOrPost);

            cred = cred ?? Credentials;

            if (cred != null)
            {
                request.AddParameter(Methods.Params.Username, cred.Username, ParameterType.GetOrPost);
                request.AddParameter(Methods.Params.Password, cred.Password, ParameterType.GetOrPost);
            }
            return request;
        }

        #endregion
    }
}
