namespace IntegrationTests;

using Backend.Src.Converter;
using Backend.Src.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net.Http.Headers;

public class IntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    public IntegrationTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    static int count = 0;
    static AuthSignUpDTO ValidSignUpDTO() 
    {
        return new AuthSignUpDTO()
        {
            Name = "ValidUserForLogin" + count++,
            LastName = "TestLastName",
            Email = "jeremias@mail.com" + count++,
            Password = "p@55w�rd12AA",
            City = "Tampere",
            Latitude = 60,
            Longitude = 60
        };
    }
    // key is item to try, value is expected result 
    static public IEnumerable<object[]> AuthSignUpDTOs() => new List<object[]>()
    {
        new object[] { new
        {
           Name = "InvalidUser",
           LastName = "TestLastName",
           Email = "jeremias@mail.com",
           Password = "p@55w�rd",
           City = "Tampere",
           Latitude = 60,
           Longitude = 60
        }, false },
        new object[] { new
        {
           Name = "InvalidUser",
           LastName = "TestLastName",
           Email = "jeremias@mail.com",
           Password = "qwerty",
           City = "Tampere",
           Latitude = 60,
           Longitude = 60
        }, false },
        new object[] { new
        {
           Name = "ValidUser",
           LastName = "TestLastName",
           Email = "jeremias@mail.com",
           Password = "p@55w�rd12AA",
           City = "Tampere",
           Latitude = 60,
           Longitude = 60
        }, true },
        new object[] { new
        {
           Name = "ValidUser",
           LastName = "TestLastName",
           Email = "jeremias@mail.com",
           Password = "p@55w�rd12AA",
           City = "Tampere",
           Latitude = 60,
           Longitude = 60
        }, false },

    };
        
    [Theory, MemberData(nameof(AuthSignUpDTOs))]
    public async void SignUp(object input, bool expected)
    {
        var client = _factory.CreateClient();

        var result = await client.PostAsync("/api/v1/Auth/signup", ConvertObjToContent(input));
        Assert.Equal(expected, result.IsSuccessStatusCode);
        if (expected)
        {
            Converter converter = new ();
            converter.CreateModel(input, out AuthSignUpDTO authSignUpDTO);
            result = await client.PostAsync("/api/v1/Auth/login", ConvertObjToContent(new { authSignUpDTO.Email, authSignUpDTO.Password }));
            Assert.Equal(!expected, result.IsSuccessStatusCode);
        }
    }
    [Fact]
    public async void LoginLogout()
    {
        var client = _factory.CreateClient();
        var validSignUp = ValidSignUpDTO();
        var response = await client.PostAsync("/api/v1/Auth/signup", ConvertObjToContent(validSignUp));
        var tokenResponse = await response.Content.ReadAsStringAsync();
        AuthReadDTO token = new();
        JsonConvert.PopulateObject(tokenResponse, token);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
        var result = await client.PostAsync("/api/v1/Auth/logout", null);
        Assert.True(result.IsSuccessStatusCode);
        response = await client.PostAsync("/api/v1/Auth/login", ConvertObjToContent(new { validSignUp.Email, validSignUp.Password }));
        Assert.True(response.IsSuccessStatusCode);
        response = await client.PostAsync("/api/v1/Auth/login", ConvertObjToContent(new { Email = "�iasudf�aisrt", Password = "asdhjbsdfgooo" }));
        Assert.True(!response.IsSuccessStatusCode);
    }
    [Fact]
    public async void GetAlls()
    {
        var client = _factory.CreateClient();
        
        
    }

    static ByteArrayContent ConvertObjToContent(object obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        var buffer = System.Text.Encoding.UTF8.GetBytes(json);
        var content = new ByteArrayContent(buffer);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return content;
    }
}