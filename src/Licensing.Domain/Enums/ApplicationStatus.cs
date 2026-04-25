namespace Licensing.Domain.Enums;

public enum ApplicationStatus
{
    ApplicationReceived = 1,
    UnderReview = 2,
    PendingPreSiteResubmission = 3,
    PreSiteResubmitted = 4,
    SiteVisitScheduled = 5,
    SiteVisitDone = 6,
    AwaitingPostSiteClarification = 7,
    PendingPostSiteResubmission = 8,
    PostSiteClarificationResubmitted = 9,
    PendingApproval = 10,
    Approved = 11,
    Rejected = 12
}
