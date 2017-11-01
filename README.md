Goodreads .NET API Client Library
=============

[![AppVeyor](https://img.shields.io/appveyor/ci/adamkrogh/goodreads-dotnet.svg)](https://ci.appveyor.com/project/adamkrogh/goodreads-dotnet) [![NuGet](https://img.shields.io/nuget/v/Goodreads.svg)](https://www.nuget.org/packages/Goodreads)

A Goodreads .NET API Client Library.

## Getting started
#### Prerequisites
A Goodreads developer key. 
This can be obtained from https://www.goodreads.com/api/keys
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
There is a full list of supported methods with examples in our [wiki page](https://github.com/adamkrogh/goodreads-dotnet/wiki/API-methods-documentation).

## Usage Examples

### Unauthorized Goodreads client
```csharp
// Define your Goodreads key and secret.
const string apiKey = "<Your API Key>";
const string apiSecret = "<Your API Secret>"; 

// Create an unauthorized goodreads client.
var client = GoodreadsClient.Create(apiKey, apiSecret);

// Now you are able to call some Goodreads endpoints that don't require OAuth credentials. For example:
var book = await client.Books.GetByBookId(15979976); // get a book by specified id.
var groups = await client.Groups.GetGroups("Arts"); // get a list of groups by search keyword
```

### User Authorization (OAuth)

```csharp
// Define your Goodreads key and secret.
const string apiKey = "<Your API Key>";
const string apiSecret = "<Your API Secret>"; 

// Create an unauthorized goodreads client.
var client = GoodreadsClient.Create(apiKey, apiSecret);

// Ask a goodreads request token and build an authorization url.
const string callbackUrl = "<callback_url>";
var requestToken = client.AskCredentials(callbackUrl);

// Get a user's OAuth access token and secret after they have granted access.
var accessToken = client.GetAccessToken(requestToken);

// Create authorized goodreads client.
var authClient = GoodreadsClient.CreateAuth(apiKey, apiSecret, accessToken.Token, accessToken.Secret);

// Now you are able to call all of Goodreads endpoints. For example:
var book = await client.Friends.AddFriend(1); // add user to friends list
await client.Shelves.AddBookToShelf("must-read", 15979976); // add book to 'must-read' shelf
```

## Goodreads API Coverage

Library covers all API Goodreads methods except below:

### Todo

The list of API methods that will be supported soon.

- topic.create — Create a new topic via OAuth.
- topic.group_folder — Get list of topics in a group's folder.
- topic.unread_group — Get a list of topics with unread comments.

There are methods which can't be called without additional credentials.

- list.book — Get the listopia lists for a given book.
- work.editions — See all editions by work.

### Bugs

Unfortunately, some Goodreads API methods have bugs.

- friend.confirm_recommendation — Confirm or decline a friend recommendation.
- owned_books.update — Update an owned book.
- review.destroy — Delete a book review.
- rating.create — Like a resource.
- rating.destroy — Unlike a resource.

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
