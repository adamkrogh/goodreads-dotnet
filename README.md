# Goodreads .NET API Client Library

A Goodreads .NET API Client Library.

# Usage Examples

```csharp
// Create an API client
var client = new GoodreadsClient("apiKey", "apiSecret");

// Create an authenticated API client
var authClient = new GoodreadsClient("apiKey", "apiSecret", "oAuthToken", "oAuthTokenSecret");

// Retrieve an author
var author = await client.Authors.GetByAuthorId(38550);

// Search for books
var results = await client.Books.Search("The Lord of the Rings", page: 1, searchField: BookSearchField.Title);

// Retrieve a book
var book = await client.Books.GetByIsbn("0441172717");

// Retrieve a paginated list of an author's books
var books = await client.Books.GetListByAuthorId(38550, page: 2);

// Retrieve a paginated list of a user's shelves
var shelves = await client.Shelves.GetListOfUserShelves(userId, page: 2);

// Retrieve a paginated list of a users's Goodreads friends
var friends = await authClient.Users.GetListOfFriends(userId, page: 1, SortFriendsList.LastOnline);
```

# Goodreads API Coverage

All the available Goodreads API methods are listed below. Check checked methods are supported through this client library. This list is kept updated. Feel free to request or contribute if you have need of a specific method.

- [ ] auth.user — Get id of user who authorized OAuth.
- [x] author.books — Paginate an author's books.
- [x] author.show — Get info about an author by id.
- [ ] author_following.create — Follow an author.
- [ ] author_following.destroy — Unfollow an author.
- [ ] author_following.show — Show author following information.
- [ ] book.isbn_to_id — Get Goodreads book IDs given ISBNs.
- [ ] book.id_to_work_id — Get Goodreads work IDs given Goodreads book IDs.
- [ ] book.review_counts — Get review statistics given a list of ISBNs.
- [ ] book.show — Get the reviews for a book given a Goodreads book id.
- [x] book.show_by_isbn — Get the reviews for a book given an ISBN.
- [ ] book.title — Get the reviews for a book given a title string.
- [ ] comment.create — Create a comment.
- [ ] comment.list — List comments on a subject.
- [ ] events.list — Events in your area.
- [ ] fanship.create — Become fan of an author. DEPRECATED.
- [ ] fanship.destroy — Stop being fan of an author. DEPRECATED.
- [ ] fanship.show — Show fanship information. DEPRECATED.
- [ ] followers.create — Follow a user.
- [ ] followers.destroy — Unfollow a user.
- [ ] friend.confirm_recommendation — Confirm or decline a friend recommendation.
- [ ] friend.confirm_request — Confirm or decline a friend request.
- [ ] friend.requests — Get friend requests.
- [ ] friends.create — Add a friend.
- [ ] group.join — Join a group.
- [ ] group.list — List groups for a given user.
- [ ] group.members — Return members of a particular group.
- [ ] group.search — Find a group.
- [ ] group.show — Get info about a group by id.
- [ ] list.book — Get the listopia lists for a given book.
- [ ] notifications — See the current user's notifications.
- [ ] owned_books.create — Add to books owned.
- [ ] owned_books.list — List books owned by a user.
- [ ] owned_books.show — Show an owned book.
- [ ] owned_books.update — Update an owned book.
- [ ] owned_books.destroy — Delete an owned book.
- [ ] quotes.create — Add a quote.
- [ ] rating.create — Like a resource.
- [ ] rating.destroy — Unlike a resource.
- [ ] read_statuses.show — Get a user's read status.
- [ ] recommendations.show — Get a recommendation from a user to another user.
- [ ] review.create — Add review.
- [ ] review.edit — Edit a review.
- [ ] review.destroy — Delete a book review.
- [ ] reviews.list — Get the books on a members shelf.
- [ ] review.recent_reviews — Recent reviews from all members..
- [ ] review.show — Get a review.
- [ ] review.show_by_user_and_book — Get a user's review for a given book.
- [ ] search.authors — Find an author by name.
- [x] search.books — Find books by title, author, or ISBN.
- [ ] series.show — See a series.
- [ ] series.list — See all series by an author.
- [ ] series.work — See all series a work is in.
- [ ] shelves.add_to_shelf — Add a book to a shelf.
- [ ] shelves.add_books_to_shelves — Add books to many shelves.
- [x] shelves.list — Get a user's shelves.
- [ ] topic.create — Create a new topic via OAuth.
- [ ] topic.group_folder — Get list of topics in a group's folder.
- [ ] topic.show — Get info about a topic by id.
- [ ] topic.unread_group — Get a list of topics with unread comments.
- [ ] updates.friends — Get your friend updates.
- [ ] user_shelves.create — Add book shelf.
- [ ] user_shelves.update — Edit book shelf.
- [ ] user.show — Get info about a member by id or username.
- [ ] user.compare — Compare books with another member.
- [ ] user.followers — Get a user's followers.
- [ ] user.following — Get people a user is following.
- [x] user.friends — Get a user's friends.
- [ ] user_status.create — Update user status.
- [ ] user_status.destroy — Delete user status.
- [ ] user_status.show — Get a user status.
- [ ] user_status.index — View user statuses.
- [ ] work.editions — See all editions by work.

# License

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
