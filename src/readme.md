# EF Core Migrations
```dotnet ef migrations add initial -c BooksDBContext -p ..\RiverBooks.Books\ -s .\riverbooks.web.csproj -o data/migrations```

```dotnet ef database update -c BooksDBContext -p ..\RiverBooks.Books\ -s .\riverbooks.web.csproj```

```
dotnet ef migrations add CartItems -c UsersDBContext -p '..\Users Module\Riverbooks.Users\RiverBooks.Users.csproj' -s .\riverbooks.web.csproj -o Data/Migrations
```
