using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Movie> Movies { get; protected set; }

        public void OnGet(string SearchTerms, string[] MPAARatings, string[] Genre, double? IMDBMin, double? IMDBMax)
        {
            /*
            this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRottenTomatoRating(Movies, IMDBMin, IMDBMax);
            */
            Movies = MovieDatabase.All;
            // Search movie titles for SearchTerms
            if(SearchTerms != null)
            {
                Movies = Movies.Where(movie => movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.CurrentCultureIgnoreCase));
                //Movies = from movie in Movies where movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.OrdinalIgnoreCase) select movie;
            }
            // Filter by MPAA Rating
            if(MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MPAARating != null && MPAARatings.Contains(movie.MPAARating));
            }
            // Filter by Genre
            if(Genre != null && Genre.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MajorGenre != null && Genres.Contains(movie.MajorGenre));
            }
            // Filter by IMDB Rating
            if(IMDBMin != null && IMDBMax != null)
            {
                Movies = Movies.Where(movie => movie.IMDBRating <= IMDBMax);
                Movies = Movies.Where(movie => movie.IMDBRating >= IMDBMin);
                Movies = Movies.Where(movie => movie.IMDBRating >= IMDBMin && movie.IMDBRating <= IMDBMax);
            }
            // Filter by Rotten Tomato Rating
            if(IMDBMin != null && IMDBMax != null)
            {
                Movies = Movies.Where(movie => movie.IMDBRating <= IMDBMax);
                Movies = Movies.Where(movie => movie.IMDBRating >= IMDBMin);
                Movies = Movies.Where(movie => movie.IMDBRating >= IMDBMin && movie.IMDBRating <= IMDBMax);
            }
        }
        /// <summary>
        /// The current search terms 
        /// </summary>
        [BindProperty]
        public string SearchTerms { get; set; } = "";

        /// <summary>
        /// The filtered MPAA Ratings
        /// </summary>
        [BindProperty]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genres
        /// </summary>
        [BindProperty]
        public string[] Genres { get; set; }

        /// <summary>
        /// The minimum IMDB Rating
        /// </summary>
        [BindProperty]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// The maximum IMDB Rating
        /// </summary>
        [BindProperty]
        public double? IMDBMax { get; set; }
    }
}
