Goodreads .NET API Client Library
=============

[![AppVeyor](https://img.shields.io/appveyor/ci/adamkrogh/goodreads-dotnet.svg)](https://ci.appveyor.com/project/adamkrogh/goodreads-dotnet) [![NuGet](https://img.shields.io/nuget/v/Goodreads.svg)](https://www.nuget.org/packages/Goodreads)

A Goodreads .NET API Client Library.

## Usage Examples

### Create a Client

```csharp
// Create an API client
var client = new GoodreadsClient("apiKey", "apiSecret");

// Create an authenticated API client
var authClient = new GoodreadsClient("apiKey", "apiSecret", "oauthToken", "oauthTokenSecret");
```

### User Authorization (OAuth)

```csharp
// Get the Goodreads URL to redirect to for authorization
var authorizeUrl = client.Connection.Credentials.GetRequestTokenAndBuildAuthorizeUrl(callbackUrl);

// Get a user's OAuth access token and secret after they have granted access
var credentials = client.Connection.Credentials.GetAccessToken(apiKey, apiSecret, requestToken, requestSecret);
```

### Authors

```csharp
// Retrieve an author
var author = await client.Authors.GetByAuthorId(38550);

// Retrieve an author id
var authorId = await client.Authors.GetAuthorIdByName("Brandon Sanderson");
```

### Books

```csharp
// Search for books
var results = await client.Books.Search("Dune", page: 1, searchField: BookSearchField.Title);

// Retrieve a book by ISBN
var book = await client.Books.GetByIsbn("0441172717");

// Retrieve a book by Goodreads book id
var book = await client.Books.GetByBookId(7235533);

// Retrieve a book by title
var book = await client.Books.GetByTitle("Dune");

// Retrieve a paginated list of an author's books
var books = await client.Books.GetListByAuthorId(38550, page: 2);
```

### Series

```csharp
// Get detailed information about a series, including all the works that appear in it
var seriesInfo = await client.Series.GetById(seriesId);

// Get all the series an author has written
var series = await client.Series.GetListByAuthorId(authorId);

// Get all the series that a work appears in
var series = await client.Series.GetListByWorkId(workId);
```

### Reviews

```csharp
// Get a review
var review = await client.Reviews.GetById(reviewId);

// Get a review that a user made on a book
var review = await client.Reviews.GetByUserIdAndBookId(userId, bookId);

// Get a list of a user's highest rated book reviews
var list = await client.Reviews.GetListByUser(id, sort: SortReviewsList.AverageRating, order: Order.Descending);

// Create a review
var reviewId = await client.Reviews.Create(bookId, reviewText: "Awesome book!", rating: 5);

// Edit a review
var success = await client.Reviews.Edit(reviewId, rating: 4);
```

### Users

```csharp
// Retrieve user information
var user = await client.Users.GetByUserId(7284465);

// Retrieve a paginated list of a users's Goodreads friends
var friends = await authClient.Users.GetListOfFriends(userId, page: 1, sort: SortFriendsList.LastOnline);

// Retrieve a paginated list of a user's shelves
var shelves = await client.Shelves.GetListOfUserShelves(userId);
```

## Goodreads API Coverage

### Status

- Auth **100%** (1 of 1)
- Authors **100%** (2 of 2)
- Author Following **100%** (3 of 3)
- Books **100%** (6 of 6)
- Comments **50%** (1 of 2)
- Events **100%** (1 of 1)
- Fanship **Deprecated**
- Followers **100%** (2 of 2)
- Friends **100%** (4 of 4)
- Groups **100%** (5 of 5)
- Lists **0%** (0 of 1)
- Notifications **100%** (1 of 1)
- Owned Books **100%** (5 of 5)
- Quotes **100%** (1 of 1)
- Ratings **0%** (0 of 2)
- Read Statuses **100%** (1 of 1)
- Recommendations **100%** (1 of 1)
- Reviews **100%** (7 of 7)
- Search **100%** (2 of 2)
- Series **100%** (3 of 3)
- Shelves **100%** (3 of 3)
- Topics **0%** (0 of 4)
- Updates **100%** (1 of 1)
- User Shelves **100%** (2 of 2)
- Users **100%** (5 of 5)
- User Status **100%** (4 of 4)
- Works **0%** (0 of 1)

**Overall**: **87%** (61 of 70)


### Todo

The list of API methods that will (hopefully) be supported soon.

- comment.list — List comments on a subject.
- list.book — Get the listopia lists for a given book.
- rating.create — Like a resource.
- rating.destroy — Unlike a resource.
- topic.create — Create a new topic via OAuth.
- topic.group_folder — Get list of topics in a group's folder.
- topic.show — Get info about a topic by id.
- topic.unread_group — Get a list of topics with unread comments.
- work.editions — See all editions by work.

### Deprecated

API methods that Goodreads has deprecated.

- fanship.create — Become fan of an author. DEPRECATED.
- fanship.destroy — Stop being fan of an author. DEPRECATED.
- fanship.show — Show fanship information. DEPRECATED.

### Bugs

API methods that Goodreads has bugs.

- friend.confirm_recommendation — Confirm or decline a friend recommendation. THERE IS A BUG IN THE GOODREADS API.
- owned_books.update — Update an owned book. THERE IS A BUG IN THE GOODREADS API.
- review.destroy — Delete a book review. THERE IS A BUG IN THE GOODREADS API.

## License

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
