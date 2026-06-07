WITH OrderedEncounters AS (

    SELECT

        PatientId,
        EncounterId,
        ProviderId,
        DepartmentId,
        AdmitDate,

        LAG(DischargeDate)
            OVER (
                PARTITION BY PatientId
                ORDER BY AdmitDate
            ) AS PreviousDischarge

    FROM Encounter

    WHERE EncounterType = 'Inpatient'
),

Readmissions AS (

    SELECT *

    FROM OrderedEncounters

    WHERE PreviousDischarge IS NOT NULL

    AND DATEDIFF(
            DAY,
            PreviousDischarge,
            AdmitDate
        ) <= 30
)

Step 2 - Attribute Accountability

SELECT

    d.Name AS Department,

    p.FullName AS Provider,

    COUNT(*) AS ReadmissionCount

FROM Readmissions r

JOIN Department d
    ON d.DepartmentId = r.DepartmentId

JOIN Provider p
    ON p.ProviderId = r.ProviderId

GROUP BY

    d.Name,
    p.FullName

ORDER BY ReadmissionCount DESC;
