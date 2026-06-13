
CREATE OR ALTER PROCEDURE usp_RevenueLeakageSummary
AS
BEGIN
    SELECT
        ClaimStatus,
        COUNT(*) AS TotalClaims,
        SUM(BilledAmount) AS TotalBilledAmount,
        SUM(ReimbursedAmount) AS TotalReimbursedAmount,
        SUM(BilledAmount - ReimbursedAmount) AS OutstandingAmount,
        RANK() OVER (
            ORDER BY SUM(BilledAmount - ReimbursedAmount) DESC
        ) AS LossRank
    FROM vw_BillingClaims
    GROUP BY ClaimStatus
    ORDER BY OutstandingAmount DESC;
END;