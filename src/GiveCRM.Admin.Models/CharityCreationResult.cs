namespace GiveCRM.Admin.Models
{
    public enum CharityCreationResult
    {
        Success,
        InvalidCharityNumber,
        InvalidName,
        InvalidSubdomain,
        DuplicateCharityNumber,
        DuplicateSubdomain,
        UnexpectedFailure
    }
}
