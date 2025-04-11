using System;
using System.Text.Json;

namespace Aralytiks.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    
    private string _settingsJson = "{}";
    public string SettingsJson
    {
        get => _settingsJson;
        set
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    JsonDocument.Parse(value);
                    _settingsJson = value;
                }
                else
                {
                    _settingsJson = "{}";
                }
            }
            catch (JsonException)
            {                
                _settingsJson = "{}";
            }
        }
    }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
} 