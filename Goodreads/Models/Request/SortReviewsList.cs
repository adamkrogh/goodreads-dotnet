using Goodreads.Http;

namespace Goodreads.Models.Request
{
    /// <summary>
    /// The options for sorting the review list returned for a user.
    /// </summary>
    [QueryParameterKey("sort")]
    public enum SortReviewsList
    {
        /// <summary>
        /// Sort by the title of the book.
        /// </summary>
        [QueryParameterValue("title")]
        Title,

        /// <summary>
        /// Sort by the author's name.
        /// </summary>
        [QueryParameterValue("author")]
        Author,

        /// <summary>
        /// Sort by the cover of the book.
        /// </summary>
        [QueryParameterValue("cover")]
        Cover,

        /// <summary>
        /// Sort by the rating the user gave the book.
        /// </summary>
        [QueryParameterValue("rating")]
        UserRating,

        /// <summary>
        /// Sort by the year the the first edition of the book was published.
        /// </summary>
        [QueryParameterValue("year_pub")]
        YearPublished,

        /// <summary>
        /// Sort by the date the first edition of the book was published.
        /// </summary>
        [QueryParameterValue("date_pub")]
        DatePublished,

        /// <summary>
        /// Sort by the date this exact edition of the book was published.
        /// </summary>
        [QueryParameterValue("date_pub_edition")]
        DatePublishedEdition,

        /// <summary>
        /// Sort by when the user started reading the book.
        /// </summary>
        [QueryParameterValue("date_started")]
        DateStarted,

        /// <summary>
        /// Sort by when the user finished reading the book.
        /// </summary>
        [QueryParameterValue("date_read")]
        DateRead,

        /// <summary>
        /// Sort by when the user last updated this book on their shelf.
        /// </summary>
        [QueryParameterValue("date_updated")]
        DateUpdated,

        /// <summary>
        /// Sort by when this book was added to the user's shelf.
        /// </summary>
        [QueryParameterValue("date_added")]
        DateAdded,

        /// <summary>
        /// Sort by who recommended the book.
        /// </summary>
        [QueryParameterValue("recommender")]
        Recommender,

        /// <summary>
        /// Sort by the average rating for the book.
        /// </summary>
        [QueryParameterValue("avg_rating")]
        AverageRating,

        /// <summary>
        /// Sort by the number of ratings for the book.
        /// </summary>
        [QueryParameterValue("num_ratings")]
        NumberOfRatings,

        /// <summary>
        /// Sort by the review of the book.
        /// </summary>
        [QueryParameterValue("review")]
        Review,

        /// <summary>
        /// Sort by how many users have read the review.
        /// </summary>
        [QueryParameterValue("read_count")]
        ReadCount,

        /// <summary>
        /// Sort by the number of votes the review has received from other users.
        /// </summary>
        [QueryParameterValue("votes")]
        Votes,

        /// <summary>
        /// Sort the list of reviews randomly.
        /// </summary>
        [QueryParameterValue("random")]
        Random,

        /// <summary>
        /// Sort by the comments left on the review.
        /// </summary>
        [QueryParameterValue("comments")]
        Comments,

        /// <summary>
        /// Sort by the notes on the review.
        /// </summary>
        [QueryParameterValue("notes")]
        Notes,

        /// <summary>
        /// Sort by the ISBN of the book.
        /// </summary>
        [QueryParameterValue("isbn")]
        Isbn,

        /// <summary>
        /// Sort by the ISBN13 of the book.
        /// </summary>
        [QueryParameterValue("isbn13")]
        Isbn13,

        /// <summary>
        /// Sort by the ASIN of the book.
        /// </summary>
        [QueryParameterValue("asin")]
        Asin,

        /// <summary>
        /// Sort by the number of pages the book has.
        /// </summary>
        [QueryParameterValue("num_pages")]
        NumberOfPages,

        /// <summary>
        /// Sort by the format of the book.
        /// </summary>
        [QueryParameterValue("format")]
        Format,

        /// <summary>
        /// Sort by the position the book has on the user's shelf.
        /// </summary>
        [QueryParameterValue("position")]
        Position,

        /// <summary>
        /// Sort by the shelves the book appears on.
        /// </summary>
        [QueryParameterValue("shelves")]
        Shelves,

        /// <summary>
        /// Sort by the book's owned status.
        /// </summary>
        [QueryParameterValue("owned")]
        Owned,

        /// <summary>
        /// Sort by when the book was purchased by the user.
        /// </summary>
        [QueryParameterValue("date_purchased")]
        DatePurchased,

        /// <summary>
        /// Sort by where the book was purchased.
        /// </summary>
        [QueryParameterValue("purchase_location")]
        PurchaseLocation,

        /// <summary>
        /// Sort by the condition of the book.
        /// </summary>
        [QueryParameterValue("condition")]
        Condition
    }
}
