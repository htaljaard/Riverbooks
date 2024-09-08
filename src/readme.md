# EF Core Migrations
```dotnet ef migrations add initial -c BooksDBContext -p ..\RiverBooks.Books\ -s .\riverbooks.web.csproj -o data/migrations```

```dotnet ef database update -c BooksDBContext -p ..\RiverBooks.Books\ -s .\riverbooks.web.csproj```
