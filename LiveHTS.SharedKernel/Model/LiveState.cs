namespace LiveHTS.SharedKernel.Model
{
    public enum LiveState
    {
        Unkown,

        HtsEnrolled,
        HtsTestedPos,
        HtsTestedNeg,
        HtsTestedInc,
        HtsReferred,
        HtsTracedContacted,
        HtsTracedNotcontacted,
        HtsTracedContactedlinked,
        HtsLinkedCare,
        HtsLinkedEnrolled,
        HtsPnsAcceptedYes,
        HtsPnsAcceptedNo,
        HtsFamAcceptedYes,
        HtsPatlisted,
        HtsFamlisted,
        HtsRetestedPos,
        HtsRetestedNeg,
        HtsRetestedInc,

        FamilyListed,
        FamilyScreened,
        FamilyEligibileYes,
        FamilyEligibileNo,
        FamilyTracedContacted,
        FamilyTracedNotcontacted,

        PartnerListed,
        PartnerScreened,
        PartnerEligibileYes,
        PartnerEligibileNo,
        PartnerTracedContacted,
        PartnerTracedNotcontacted,
    }
}