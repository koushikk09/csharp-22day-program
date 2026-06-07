SELECT

    e.EncounterId,

    d.Name AS Department,

    -- Length of stay for this encounter

    DATEDIFF(
        DAY,
        e.AdmitDate,
        e.DischargeDate
    ) AS LengthOfStay,

    -- Average LOS for all encounters
    -- in the same department

    AVG(
        DATEDIFF(
            DAY,
            e.AdmitDate,
            e.DischargeDate
        )
    )
    OVER (
        PARTITION BY e.DepartmentId
    ) AS DepartmentAverageLOS

FROM Encounter e

JOIN Department d
    ON d.DepartmentId = e.DepartmentId

WHERE e.DischargeDate IS NOT NULL;
