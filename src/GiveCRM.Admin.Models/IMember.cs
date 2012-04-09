namespace GiveCRM.Admin.Models
{
    using System;

    public interface IMember
    {
        string UserName { get; }
        string Email { get; }
        Guid Id { get; }
    }
}