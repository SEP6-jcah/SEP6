using System;
using System.Collections.Generic;
using System.Linq;

namespace Sep6Client.Data.DataHelper.Search
{
    public class MovieQueryHelper
    {
        private const string BaseBrowseQuery = "discover/movie?include_adult=false&include_video=false";
        private const string BaseSearchQuery = "search/movie?include_adult=false&include_video=false";
        private const string BasePopularQuery = "movie/popular";
        private const string BaseCurrentQuery = "movie/now_playing";
        private const string BaseTopRatedQuery = "movie/top_rated";
        private const string BaseUpcomingQuery = "movie/upcoming";
        private const string Page = "&page=";
        private const string Text = "&query=";
        private const string Sort = "&sort_by=";
        private readonly string[] sortOptions = {"popularity", "vote_average"};
        private readonly string[] sortOrderOptions = { ".desc", ".asc"};

        public static string[] GetSortByOptions()
        {
            return new[] {"Popularity", "Rating"};
        }
        
        public string GetSearchQuery(Dictionary<SearchFilterOptions, string> criteria)
        {
            return BaseSearchQuery + GetSearchQueryParameters(criteria);
        }

        public string GetBrowseQuery(Dictionary<SearchFilterOptions, string> criteria)
        {
            return BaseBrowseQuery + GetBrowseQueryParameters(criteria);
        }

        public string GetPopularQuery()
        {
            return BasePopularQuery;
        }

        public string GetCurrentMovieQuery()
        {
            return BaseCurrentQuery;
        }

        public string GetTopRatedQuery()
        {
            return BaseTopRatedQuery;
        }

        public string GetUpcomingQuery()
        {
            return BaseUpcomingQuery;
        }

        private string GetSearchQueryParameters(Dictionary<SearchFilterOptions, string> criteria)
        {
            var result = "";
            var searchText = "";
            var pageNr = "";
            
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

        private string GetBrowseQueryParameters(Dictionary<SearchFilterOptions, string> criteria)
        {
            var result = "";
            var pageNr = "";
            var sortBy = "";

            try
            {
                pageNr = criteria[SearchFilterOptions.PageNr];
                sortBy = criteria[SearchFilterOptions.SortBy];
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
            }

            if (!string.IsNullOrEmpty(pageNr))
            {
                result += Page + pageNr;
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
                result += Sort + opt+ sortOrderOptions[0];
            }
            else
            {
                result += Sort + sortOptions.First() + sortOrderOptions.First();
            }

            return result;
        }
    }
}