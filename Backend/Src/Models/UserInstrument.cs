using System.Text.Json.Serialization;

namespace Backend.Src.Models;

public class UserInstrument
{    
    public User User { get; set; } = null!;    
    public Guid UserId { get; set; }    
    public Instrument Instrument { get; set; } = null!;
    public Guid InstrumentId { get; set; }
    public bool LookingToPlay { get; set; }
    public bool IsMain { get; set; }
    public SkillLevel Skill { get; set; }
    public override string ToString() => $"{Instrument.Name}";
    public enum SkillLevel
    {
        // 0 < x < 5
        Beginner,
        // 5 < x < 15
        Intermediate,
        // x > 15
        Experienced
    }
}
