CREATE OR ALTER PROCEDURE usp_ProviderWorkload
AS
BEGIN
    SELECT
        p.ProviderName,
        COUNT(e.EncounterId) AS TotalEncounters
    FROM Encounters e
    JOIN Providers p
        ON e.ProviderId = p.ProviderId
    GROUP BY p.ProviderName
    ORDER BY TotalEncounters DESC;
END;
