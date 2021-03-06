﻿using System;
using System.Web.Security;

namespace GiveCRM.Admin.Web.Services
{
    public class AspMembershipService : IMembershipService
    {
        private readonly MembershipProvider membershipProvider;

        public AspMembershipService(MembershipProvider membershipProvider)
        {
            if (membershipProvider == null)
            {
                throw new ArgumentNullException("membershipProvider");
            }

            this.membershipProvider = membershipProvider;
        }

        public IMember GetUser(string username)
        {
            var aspMembershipUser = membershipProvider.GetUser(username, false);
            return aspMembershipUser == null ? null : new AspMembershipMember(aspMembershipUser);
        }

        public UserCreationResult CreateUser(string username, string password, string email)
        {
            MembershipCreateStatus membershipCreateStatus;
            membershipProvider.CreateUser(username, password, email, null, null, true, Guid.NewGuid(),
                                          out membershipCreateStatus);

            return MapProviderCreateStatus(membershipCreateStatus);
        }

        private UserCreationResult MapProviderCreateStatus(MembershipCreateStatus providerCreateStatus)
        {
            switch (providerCreateStatus)
            {
                case MembershipCreateStatus.Success:
                    return UserCreationResult.Success;
                case MembershipCreateStatus.DuplicateUserName:
                    return UserCreationResult.DuplicateUsername;
                case MembershipCreateStatus.DuplicateEmail:
                    return UserCreationResult.DuplicateEmail;
                case MembershipCreateStatus.InvalidUserName:
                    return UserCreationResult.InvalidUsername;
                case MembershipCreateStatus.InvalidPassword:
                    return UserCreationResult.InvalidPassword;
                case MembershipCreateStatus.InvalidEmail:
                    return UserCreationResult.InvalidEmail;
                case MembershipCreateStatus.InvalidQuestion:
                case MembershipCreateStatus.InvalidAnswer:
                case MembershipCreateStatus.UserRejected:
                case MembershipCreateStatus.ProviderError:
                case MembershipCreateStatus.InvalidProviderUserKey:
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    return UserCreationResult.UnexpectedFailure;
                default:
                    throw new ArgumentException(
                        string.Format("Unsupported value of System.Web.Security.MembershipCreateStatus {0}",
                                      providerCreateStatus));
            }
        }
    }
}