using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;

namespace LiveHTS.Core.Interfaces.Services.Access
{
    public interface IAuthService
    {
        Provider GetDefaultProvider();
        Practice GetDefaultPractice();
        Device GetDefaultDevice();
        User SignIn(string username,string password);
        void SaveDownloaded(Guid userId,List<UserSummary> userSummaries);
        List<UserSummary> Get(Guid userId);
    }
}