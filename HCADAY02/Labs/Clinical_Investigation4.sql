WITH AdmissionCounts AS (

    SELECT

        PatientId,

        COUNT(*) AS TotalAdmissions

    FROM Encounter

    WHERE EncounterType = 'Inpatient'

    GROUP BY PatientId

)

SELECT

    pt.MRN,

    pt.FullName,

    ac.TotalAdmissions

FROM AdmissionCounts ac

JOIN Patient pt
    ON pt.PatientId = ac.PatientId

WHERE ac.TotalAdmissions >= 3

ORDER BY ac.TotalAdmissions DESC;
