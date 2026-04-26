using Blazored.LocalStorage;
using System;
using System.Threading.Tasks;

namespace Licensing.Client.Services;

public enum Persona
{
    Operator,
    Officer
}

public class PersonaService
{
    private readonly ILocalStorageService _localStorage;
    private const string PersonaKey = "active_persona";

    public Persona CurrentPersona { get; private set; } = Persona.Operator;
    public event Action? OnPersonaChanged;

    public PersonaService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        var saved = await _localStorage.GetItemAsync<string>(PersonaKey);
        if (Enum.TryParse<Persona>(saved, out var persona))
        {
            CurrentPersona = persona;
        }
        OnPersonaChanged?.Invoke();
    }

    public async Task SwitchTo(Persona persona)
    {
        CurrentPersona = persona;
        await _localStorage.SetItemAsync(PersonaKey, persona.ToString());
        OnPersonaChanged?.Invoke();
    }
}
