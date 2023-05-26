using System;
using System.Collections.Generic;

namespace Sep6Client.Data.DataHelper.Search
{
    public class PersonQueryHelper
    {
        private const string BaseBrowseQuery = "person/popular?page=";
        private const string BaseSearchQuery = "search/person?include_adult=false";
        private const string Page = "&page=";
        private const string Text = "&query=";

        public string GetSearchQuery(Dictionary<SearchFilterOptions, string> criteria)
        {
            return BaseSearchQuery + GetSearchQueryParameters(criteria);
        }

        public string GetBrowseQuery(Dictionary<SearchFilterOptions, string> criteria)
        {
            return BaseBrowseQuery + criteria[SearchFilterOptions.PageNr];
        }

        private string GetSearchQueryParameters(Dictionary<SearchFilterOptions, string> criteria)
        {
            var result = "";
            var searchText = "";
            var pageNr = "1";
            
            try
            {
                searchText = criteria[SearchFilterOptions.Text];
                pageNr = criteria[SearchFilterOptions.PageNr];
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
            }
            
            if (!string.IsNullOrEmpty(searchText))
            {
                result += Text + searchText.Replace(' ', '+');
            }

            if (!string.IsNullOrEmpty(pageNr))
            {
                result += Page + pageNr;
            }
            
            return result;
        }
    }
}