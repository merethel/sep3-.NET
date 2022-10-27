using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared;
using Shared.Dtos;

namespace HttpClients.Implementations;

public class CompanyHttpClient : ICompanyService
{
    private readonly HttpClient Client;

    public CompanyHttpClient(HttpClient client)
    {
        Client = client;
    }

    public async Task<Company> Create(CompanyCreationDto dto)
    {
        HttpResponseMessage response = await Client.PostAsJsonAsync("/company", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Company company = JsonSerializer.Deserialize<Company>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return company;
    }
}