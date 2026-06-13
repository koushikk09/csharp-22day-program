

CREATE OR ALTER VIEW vw_BillingClaims
AS
SELECT
    ClaimId,
    ClaimStatus,
    BilledAmount,
    ReimbursedAmount,
    (BilledAmount - ReimbursedAmount) AS OutstandingAmount
FROM Claims;