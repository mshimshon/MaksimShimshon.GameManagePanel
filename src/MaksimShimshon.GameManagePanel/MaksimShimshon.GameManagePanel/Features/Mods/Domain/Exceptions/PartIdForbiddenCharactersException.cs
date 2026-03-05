namespace MaksimShimshon.GameManagePanel.Features.Mods.Domain.Exceptions;

public class PartIdForbiddenCharactersException : DomainException
{
    public PartIdForbiddenCharactersException(string detectedChar) : base($"Part Id contains forbiden characters ({detectedChar})")
    {
    }
}
