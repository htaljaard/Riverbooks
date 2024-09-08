using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Xunit.Abstractions;

namespace RiverBooks.Books.Test;
public class BookList(Fixture fixture, ITestOutputHelper outputHelper) :
  TestClass<Fixture>(fixture, outputHelper)
{
  [Fact]
  public async Task ReturnsThreeBooksAsync()
  {
    var testResult = await Fixture.Client.GETAsync<List, GetBooksResponse>();

    testResult.Response.EnsureSuccessStatusCode();
    testResult.Result.Books.Count.Should().Be(3);
  }
}

public class BookGetById(Fixture fixture, ITestOutputHelper outputHelper) :
  TestClass<Fixture>(fixture, outputHelper)
{
  [Theory]
  [InlineData("00000000-0000-0000-0000-000000000001", "12 Rules for Life")]
  [InlineData("00000000-0000-0000-0000-000000000002", "Beyond Order")]
  [InlineData("00000000-0000-0000-0000-000000000003", "Maps of Meaning")]
  public async Task ReturnExpectedBookGivenIdAsync(string validId, string expectedTitle)
  {
    var id = Guid.Parse(validId);
    var request = new GetBookByIdRequest { Id = id };
    var testResult = await
      Fixture.Client.GETAsync<GetById, GetBookByIdRequest, BookDto>(request);

    testResult.Response.EnsureSuccessStatusCode();
    testResult.Result.Title.Should().Be(expectedTitle);
  }
}