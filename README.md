Goodreads .NET API Client Library
=============

A Goodreads .NET API Client Library.

### Usage Examples

```csharp
// Create an API client
var client = new GoodreadsClient("apiKey", "apiSecret");

// Create an authenticated API client
var authClient = new GoodreadsClient("apiKey", "apiSecret", "oAuthToken", "oAuthTokenSecret");

// Retrieve an author
var author = await client.Authors.GetByAuthorId(38550);

// Search for books
var results = await client.Books.Search("The Lord of the Rings", page: 1, searchField: BookSearchField.Title);

// Retrieve a book by ISBN
var book = await client.Books.GetByIsbn("0441172717");

// Retrieve a book by Goodreads book id
var book = await client.Books.GetByBookId(7235533);

// Retrieve a book by title
var book = await client.Books.GetByTitle("The Lord of the Rings");

// Retrieve a paginated list of an author's books
var books = await client.Books.GetListByAuthorId(38550, page: 2);

// Retrieve a paginated list of a user's shelves
var shelves = await client.Shelves.GetListOfUserShelves(userId);

// Retrieve a paginated list of a users's Goodreads friends
var friends = await authClient.Users.GetListOfFriends(userId, page: 1, sort: SortFriendsList.LastOnline);
```

### Goodreads API Coverage

All the available [Goodreads API](https://www.goodreads.com/api/index) methods are listed below. 
This list is kept updated so feel free to request or contribute if you have need of a specific method.

#### Status

- Auth **100%** (1 of 1)
- Authors **100%** (2 of 2)
- Author Following **0%** (0 of 3)
- Books **100%** (6 of 6)
- Comments **0%** (0 of 2)
- Events **0%** (0 of 1)
- Fanship **Deprecated**
- Followers **0%** (0 of 2)
- Friends **0%** (0 of 4)
- Groups **0%** (0 of 5)
- Lists **0%** (0 of 1)
- Notifications **0%** (0 of 1)
- Owned Books **0%** (0 of 5)
- Quotes **0%** (0 of 1)
- Ratings **0%** (0 of 2)
- Read Statuses **0%** (0 of 1)
- Recommendations **0%** (0 of 1)
- Reviews **0%** (0 of 7)
- Search **50%** (1 of 2)
- Series **0%** (0 of 3)
- Shelves **33%** (1 of 3)
- Topics **0%** (0 of 4)
- Updates **0%** (0 of 1)
- User Shelves **0%** (0 of 2)
- Users **20%** (1 of 5)
- User Status **0%** (0 of 4)
- Works **0%** (0 of 1)

**Overall**: **17%** (12 of 70)

#### Complete

The list of API methods this client library supports.

- auth.user — Get id of user who authorized OAuth.
- author.books — Paginate an author's books.
- author.show — Get info about an author by id.
- book.isbn_to_id — Get Goodreads book IDs given ISBNs.
- book.id_to_work_id — Get Goodreads work IDs given Goodreads book IDs.
- book.review_counts — Get review statistics given a list of ISBNs.
- book.show_by_isbn — Get the reviews for a book given an ISBN.
- book.show — Get the reviews for a book given a Goodreads book id.
- book.title — Get the reviews for a book given a title string.
- search.books — Find books by title, author, or ISBN.
- shelves.list — Get a user's shelves.
- user.friends — Get a user's friends.

#### Todo

The list of API methods that will (hopefully) be supported soon.

- author_following.create — Follow an author.
- author_following.destroy — Unfollow an author.
- author_following.show — Show author following information.
- comment.create — Create a comment.
- comment.list — List comments on a subject.
- events.list — Events in your area.
- followers.create — Follow a user.
- followers.destroy — Unfollow a user.
- friend.confirm_recommendation — Confirm or decline a friend recommendation.
- friend.confirm_request — Confirm or decline a friend request.
- friend.requests — Get friend requests.
- friends.create — Add a friend.
- group.join — Join a group.
- group.list — List groups for a given user.
- group.members — Return members of a particular group.
- group.search — Find a group.
- group.show — Get info about a group by id.
- list.book — Get the listopia lists for a given book.
- notifications — See the current user's notifications.
- owned_books.create — Add to books owned.
- owned_books.list — List books owned by a user.
- owned_books.show — Show an owned book.
- owned_books.update — Update an owned book.
- owned_books.destroy — Delete an owned book.
- quotes.create — Add a quote.
- rating.create — Like a resource.
- rating.destroy — Unlike a resource.
- read_statuses.show — Get a user's read status.
- recommendations.show — Get a recommendation from a user to another user.
- review.create — Add review.
- review.edit — Edit a review.
- review.destroy — Delete a book review.
- reviews.list — Get the books on a members shelf.
- review.recent_reviews — Recent reviews from all members..
- review.show — Get a review.
- review.show_by_user_and_book — Get a user's review for a given book.
- series.show — See a series.
- series.list — See all series by an author.
- series.work — See all series a work is in.
- shelves.add_to_shelf — Add a book to a shelf.
- shelves.add_books_to_shelves — Add books to many shelves.
- topic.create — Create a new topic via OAuth.
- topic.group_folder — Get list of topics in a group's folder.
- topic.show — Get info about a topic by id.
- topic.unread_group — Get a list of topics with unread comments.
- updates.friends — Get your friend updates.
- user_shelves.create — Add book shelf.
- user_shelves.update — Edit book shelf.
- user.show — Get info about a member by id or username.
- user.compare — Compare books with another member.
- user.followers — Get a user's followers.
- user.following — Get people a user is following.
- user_status.create — Update user status.
- user_status.destroy — Delete user status.
- user_status.show — Get a user status.
- user_status.index — View user statuses.
- work.editions — See all editions by work.

#### Not Implementing

API methods that don't seem useful or are not worth the effort.

- search.authors — Find an author by name.
  - This doesn't really seem that useful...

#### Deprecated

API methods that Goodreads has deprecated.

- fanship.create — Become fan of an author. DEPRECATED.
- fanship.destroy — Stop being fan of an author. DEPRECATED.
- fanship.show — Show fanship information. DEPRECATED.

### License

The MIT License (MIT)

Copyright (c) 2016 Adam Krogh

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
