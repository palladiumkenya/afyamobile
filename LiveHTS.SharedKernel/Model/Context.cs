using System;

namespace LiveHTS.SharedKernel.Model
{
    public class Context
    {
        public  Guid UserId { get; }
        public  Guid? DeviceId { get; }
        public  Guid? PracticeId { get;  }
        public  Guid? ProviderId { get;  }

        public Context(Guid userId, Guid? deviceId, Guid? practiceId, Guid? providerId)
        {
            UserId = userId;
            DeviceId = deviceId;
            PracticeId = practiceId;
            ProviderId = providerId;
        }


        public static void Create(Guid userId, Guid? deviceId, Guid? practiceId, Guid? providerId)
        {
        }
    }
}