Goodreads .NET API Client Library
=============

[![AppVeyor](https://img.shields.io/appveyor/ci/adamkrogh/goodreads-dotnet.svg)](https://ci.appveyor.com/project/adamkrogh/goodreads-dotnet) [![NuGet](https://img.shields.io/nuget/v/Goodreads.svg)](https://www.nuget.org/packages/Goodreads)

A Goodreads .NET API Client Library.

## Getting started
#### Prerequisites
A Goodreads developer key. 
This can be obtained from https://www.goodreads.com/api/keys.
You must register your application in Goodreads as well.
Also you could find more information how obtain your key and register app [here](https://www.goodreads.com/api/documentation).

#### Installation
*Package manager*
```
Install-Package Goodreads
```
*.NET CLI*
```
dotnet add package Goodreads
```

#### Docs
There is a full list of supported methods with examples in our [wiki page](https://github.com/adamkrogh/goodreads-dotnet/wiki/API-methods).

## Usage Examples

### Use an unauthorized Goodreads client
```csharp
// Define your Goodreads key and secret.
const string apiKey = "<Your API Key>";
const string apiSecret = "<Your API Secret>"; 

// Create an unauthorized Goodreads client.
var client = GoodreadsClient.Create(apiKey, apiSecret);

// Now you are able to call some Goodreads endpoints that don't need the OAuth credentials. For example:
// Get a book by specified id.
var book = await client.Books.GetByBookId(bookId: 15979976); 

// Get a list of groups by search keyword.
var groups = await client.Groups.GetGroups(search: "Arts"); 
```

### User Authorization (OAuth)

```csharp
// Define your Goodreads key and secret.
const string apiKey = "<Your API Key>";
const string apiSecret = "<Your API Secret>"; 

// Create an unauthorized Goodreads client.
var client = GoodreadsClient.Create(apiKey, apiSecret);

// Ask a Goodreads request token and build an authorization url.
const string callbackUrl = "<callback_url>";
var requestToken = await client.AskCredentials(callbackUrl);

// Then app has to redirect a user to 'requestToken.AuthorizeUrl' and user must grant access to your application.

// Get a user's OAuth access token and secret after they have granted access.
var accessToken = await client.GetAccessToken(requestToken);

// Create an authorized Goodreads client.
var authClient = GoodreadsClient.CreateAuth(apiKey, apiSecret, accessToken.Token, accessToken.Secret);

// Now you are able to call all of the Goodreads endpoints. For example:
// Add a user to friends list
var book = await client.Friends.AddFriend(userId: 1); 

// Add a book to a 'must-read' shelf.
await client.Shelves.AddBookToShelf(shelf: "must-read", bookId: 15979976); 
```

## Goodreads API Coverage

Library covers all API Goodreads methods except below:

### Need some additional credentials

- list.book — Get the listopia lists for a given book.
- work.editions — See all editions by work.

### Bugs

Unfortunately, some Goodreads API methods have bugs.

- friend.confirm_recommendation — Confirm or decline a friend recommendation.
- owned_books.update — Update an owned book.
- review.destroy — Delete a book review.
- rating.create — Like a resource.
- rating.destroy — Unlike a resource.
