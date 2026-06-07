SELECT

    p.FullName,

    COUNT(e.EncounterId) AS EncounterCount,

    RANK()
    OVER (
        ORDER BY COUNT(e.EncounterId) DESC
    ) AS VolumeRank

FROM Provider p

LEFT JOIN Encounter e
    ON e.ProviderId = p.ProviderId

GROUP BY
    p.ProviderId,
    p.FullName;
