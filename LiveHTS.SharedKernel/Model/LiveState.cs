namespace LiveHTS.SharedKernel.Model
{
    public enum LiveState
    {
        Unkown,

        HtsEnrolled,
        HtsConsented,
        HtsTestedPos,
        HtsTestedNeg,
        HtsTestedInc,
        HtsReferred,
        HtsTracedContacted,
        HtsTracedNotContacted,
        HtsTracedContactedLinked,
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

        HtsSmartCardEnrolled,
        HtsTested,
        IsPead,
        HtsCanBeReferred,
        HtsCanBeLinked
    }
}