using System;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using SQLite;

namespace LiveHTS.Core.Model.Subject
{
    public class ClientState : Entity<Guid>
    {
        public LiveState Status { get; set; }
        public DateTime StatusDate { get; set; }
        [Indexed]
        public Guid ClientId { get; set; }
        public Guid? EncounterId { get; set; }
        public Guid? IndexClientId { get; set; }
        public ClientState()
        {
            Id = LiveGuid.NewGuid();
        }

        public ClientState(Guid clientId, LiveState state)
        {
            Id = LiveGuid.NewGuid();
            ClientId = clientId;
            StatusDate = DateTime.Now;
            Status = state;
        }
        public ClientState(Guid clientId, LiveState state,Guid indexClientId)
        {
            Id = LiveGuid.NewGuid();
            ClientId = clientId;
            IndexClientId = indexClientId;
            StatusDate = DateTime.Now;
            Status = state;
        }

        public ClientState(Guid clientId, Guid encounterId, LiveState state) : this(clientId, state)
        {
            EncounterId = encounterId;
        }

        public ClientState(Guid clientId, Guid encounterId, LiveState state, Guid indexClientId) : this(clientId, state, indexClientId)
        {
            EncounterId = encounterId;
        }

        public static LiveState GetState(Guid id,string mode="")
        {
            if (id == new Guid("b25efd8a-852f-11e7-bb31-be2e44b06b34"))
                return LiveState.HtsTestedPos;
            if (id == new Guid("b25efb78-852f-11e7-bb31-be2e44b06b34"))
                return LiveState.HtsTestedNeg;
            if (id == new Guid("b25f017c-852f-11e7-bb31-be2e44b06b34"))
                return LiveState.HtsTestedInc;

            /*
             b25f0a50-852f-11e7-bb31-be2e44b06b34|C|Contacted|0
            b25f0a51-852f-11e7-bb31-be2e44b06b34|C|Contacted and Linked|0
            b25f102c-852f-11e7-bb31-be2e44b06b34|NC|Not Contacted|0
             */
            if (mode == "fam")
            {
                if (id == new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34"))
                    return LiveState.FamilyTracedContacted;
                if (id == new Guid("b25f102c-852f-11e7-bb31-be2e44b06b34"))
                    return LiveState.FamilyTracedNotcontacted;
            }

            if (mode == "pat")
            {
                if (id == new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34"))
                    return LiveState.PartnerTracedContacted;
                if (id == new Guid("b25f102c-852f-11e7-bb31-be2e44b06b34"))
                    return LiveState.PartnerTracedNotcontacted;
            }

            if (string.IsNullOrWhiteSpace(mode))
            {
                if (id == new Guid("b25f0a50-852f-11e7-bb31-be2e44b06b34"))
                    return LiveState.HtsTracedContacted;
                if (id == new Guid(" b25f0a51-852f-11e7-bb31-be2e44b06b34"))
                    return LiveState.HtsTracedContactedLinked;
                if (id == new Guid("b25f102c-852f-11e7-bb31-be2e44b06b34"))
                    return LiveState.HtsTracedNotContacted;
            }

            return LiveState.Unkown;
        }
        public override string ToString()
        {
            return $"{ClientId}|{Status}|{StatusDate:F}|{(null!=IndexClientId&&!IndexClientId.Value.IsNullOrEmpty()?IndexClientId.Value.ToString():"")}";
        }
    }
}