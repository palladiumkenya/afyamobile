using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Engine;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Engine
{
    public static class HtsFunnel
    {

        public static bool CanBeMutliple(this LiveState current)
        {
            return current == LiveState.FamilyTracedContacted;
        }

        public static bool HasNext(this LiveState current)
        {
            return current == LiveState.HtsTestedPos;
        }

        public static List<LiveState> GetNext(this LiveState current)
        {
            var list=new List<LiveState>();

            //HTS

            if (current == LiveState.HtsTestedPos || current == LiveState.HtsTestedInc)
            {
                list.Add(LiveState.HtsReferred);
                list.Add(LiveState.HtsFamAcceptedYes);
            }

            if (current == LiveState.HtsTestedNeg)
            {
                list.Add(LiveState.HtsFamAcceptedYes);
            }

            return list;
        }
    }
}