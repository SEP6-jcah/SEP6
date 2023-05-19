using System;
using System.Collections.Generic;
using System.Linq;

namespace Sep6Client.Data.DataHelper
{
    public class QueryHelper
    {
        private string baseBrowseQuery = "/discover/movie?include_adult=false&include_video=false";
        private string baseSearchQuery = "/search/movie?include_adult=false&include_video=false";
        private string page = "&page=";
        private string text = "&query=";
        private string sort = "&sort_by=";
        private readonly string[] sortOptions = {"popularity", "vote_average", "primary_release_date"};
        private readonly string[] sortOrderOptions = { ".desc", ".asc"};

        public static string[] GetSortByOptions()
        {
            return new[] {"Popularity", "Rating"};
        }
        
        public string GetSearchQuery(Dictionary<SearchFilterOptions, string> criteria)
        {
            return baseSearchQuery + GetQueryParameters(criteria);
        }

        public string GetBrowseQuery(Dictionary<SearchFilterOptions, string> criteria)
        {
            return baseBrowseQuery + GetQueryParameters(criteria);
        }

        private string GetQueryParameters(Dictionary<SearchFilterOptions, string> criteria)
        {
            var result = "";
            var searchText = "";
            var pageNr = "";
            var sortBy = "";

            try
            {
                searchText = criteria[SearchFilterOptions.Text];
                pageNr = criteria[SearchFilterOptions.PageNr];
                sortBy = criteria[SearchFilterOptions.SortBy];
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
            }
            
            if (!string.IsNullOrEmpty(searchText))
            {
                result += text + searchText.Replace(' ', '+');
            }

            if (!string.IsNullOrEmpty(pageNr))
            {
                result += page + pageNr;
            }
            if (!string.IsNullOrEmpty(sortBy))
            {
                var opt = "";
                switch (sortBy)
                {
                    case "Popularity":
                        opt = sortOptions[0];
                        break;
                    case "Rating":
                        opt = sortOptions[1];
                        break;
                    default:
                        opt = sortOptions[0];
                        break;
                }
                result += sort + opt+ sortOrderOptions[0];
            }
            else
            {
                result += sort + sortOptions.First() + sortOrderOptions.First();
            }

            return result;
        }
    }
}