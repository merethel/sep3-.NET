using System.Text.Json;
using Shared;

namespace FileData;

public class FileContext
{
    private const string FilePath = "data.json";

    private DataContainer? dataContainer;

    public ICollection<Event> Events
    {
        get
        {
            LazyLoadData();
            return dataContainer!.Events;
        }
    }

    public ICollection<User> Companies
    {
        get
        {
            LazyLoadData();
            return dataContainer!.Companies;
        }
    }

    private void LazyLoadData()
    {
        if (dataContainer == null)
        {
            LoadData();
        }
    }

    private void LoadData()
    {
        string content = File.ReadAllText(FilePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(FilePath, serialized);
        dataContainer = null;
    }
}