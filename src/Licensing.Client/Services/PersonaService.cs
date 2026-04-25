using System;

namespace Licensing.Client.Services;

public enum Persona
{
    Operator,
    Officer
}

public class PersonaService
{
    public Persona CurrentPersona { get; private set; } = Persona.Operator;
    public event Action? OnPersonaChanged;

    public void SwitchTo(Persona persona)
    {
        CurrentPersona = persona;
        OnPersonaChanged?.Invoke();
    }
}
