/* Assignment 1: Provider Workload Ranking */

SELECT
    p.ProviderName,
    d.DepartmentName,
    COUNT(e.EncounterId) AS TotalEncountersHandled,
    RANK() OVER (ORDER BY COUNT(e.EncounterId) DESC) AS ProviderRank
FROM Encounters e
JOIN Providers p
    ON e.ProviderId = p.ProviderId
JOIN Departments d
    ON p.DepartmentId = d.DepartmentId
GROUP BY
    p.ProviderName,
    d.DepartmentName
ORDER BY
    TotalEncountersHandled DESC;