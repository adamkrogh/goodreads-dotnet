using Goodreads.Http;

namespace Goodreads.Models.Request
{
    /// <summary>
    /// Goodreads resource type.
    /// </summary>
    public enum ResourceType
    {
        [QueryParameterValue("author_blog_post")]
        AuthorBlogPost,

        [QueryParameterValue("author_following")]
        AuthorFollowing,

        [QueryParameterValue("blog")]
        Blog,

        [QueryParameterValue("book_news_post")]
        BooksNewsPost,

        [QueryParameterValue("chapter")]
        Chapter,

        [QueryParameterValue("comment")]
        Comment,

        [QueryParameterValue("community_answer")]
        CommunityAnswer,

        [QueryParameterValue("event_response")]
        EventResponse,

        [QueryParameterValue("friend")]
        Friend,

        [QueryParameterValue("giveaway")]
        Giveaway,

        [QueryParameterValue("group_user")]
        GroupUser,

        [QueryParameterValue("interview")]
        Interview,

        [QueryParameterValue("librarian_note")]
        LibrarianNotes,

        [QueryParameterValue("link_collection")]
        LinkCollection,

        [QueryParameterValue("list")]
        List,

        [QueryParameterValue("owned_book")]
        OwnedBook,

        [QueryParameterValue("photo")]
        Photo,

        [QueryParameterValue("poll")]
        Poll,

        [QueryParameterValue("poll_vote")]
        PollVote,

        [QueryParameterValue("queued_item")]
        QueuedItem,

        [QueryParameterValue("question")]
        Question,

        [QueryParameterValue("question_user_stat")]
        QuestionUserStat,

        [QueryParameterValue("quiz")]
        Quiz,

        [QueryParameterValue("quiz_score")]
        QuizScore,

        [QueryParameterValue("rating")]
        Rating,

        [QueryParameterValue("read_status")]
        ReadStatus,

        [QueryParameterValue("recommendation")]
        Recommendation,

        [QueryParameterValue("recommendation_request")]
        RecommendationRequest,

        [QueryParameterValue("review")]
        Review,

        [QueryParameterValue("review_proxy")]
        ReviewProxy,

        [QueryParameterValue("services/models/reading_note")]
        ReadingNote,

        [QueryParameterValue("sharing")]
        Sharing,

        [QueryParameterValue("topic")]
        Topic,

        [QueryParameterValue("user")]
        User,

        [QueryParameterValue("user_challenge")]
        UserChallenge,

        [QueryParameterValue("user_following")]
        UserFollowing,

        [QueryParameterValue("user_list_challenge")]
        UserListChallenge,

        [QueryParameterValue("user_list_vote")]
        UserListVote,

        [QueryParameterValue("user_quote")]
        UserQuote,

        [QueryParameterValue("user_status")]
        UserStatus,

        [QueryParameterValue("video")]
        Video
    }
}
