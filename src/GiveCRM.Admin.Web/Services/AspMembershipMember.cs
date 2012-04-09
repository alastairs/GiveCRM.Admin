namespace GiveCRM.Admin.Web.Services
{
    using System;
    using System.Web.Security;

    public class AspMembershipMember : IMember
    {
        private readonly MembershipUser aspMembershipUser;

        public AspMembershipMember(MembershipUser aspMembershipUser)
        {
            if (aspMembershipUser == null)
            {
                throw new ArgumentNullException("aspMembershipUser");
            }

            this.aspMembershipUser = aspMembershipUser;
        }

        public string UserName
        {
            get { return aspMembershipUser.UserName; }
        }

        public string Email
        {
            get { return aspMembershipUser.Email; }
        }

        public Guid Id
        {
            get
            {
                if (aspMembershipUser.ProviderUserKey == null)
                {
                    return Guid.Empty;
                }
                
                return (Guid) this.aspMembershipUser.ProviderUserKey;
            }
        }
    }
}