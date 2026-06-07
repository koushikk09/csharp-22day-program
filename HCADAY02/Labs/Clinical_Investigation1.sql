Step 1: Arrange Patient Encounters In Order
-- LAG() allows us to look at the previous encounter
-- for the same patient without writing a self-join

WITH OrderedEncounters AS (

    SELECT

        PatientId,

        EncounterId,

        AdmitDate,

        DischargeDate,

        -- Previous discharge for this patient

        LAG(DischargeDate)
            OVER (
                PARTITION BY PatientId
                ORDER BY AdmitDate
            ) AS PreviousDischarge

    FROM Encounter

    WHERE EncounterType = 'Inpatient'

)

PARTITION BY PatientId tells SQL Server to analyse each patient's encounter history separately. Within each patient's history, ORDER BY AdmitDate arranges encounters in the order they occurred, creating a timeline of care. LAG() then looks at the previous encounter in that timeline, allowing us to compare the current admission with the patient's prior discharge without writing a complex self-join

Step 2: Identify Readmissions
WITH OrderedEncounters AS (

    SELECT

        PatientId,
        EncounterId,
        AdmitDate,
        DischargeDate,

        LAG(DischargeDate)
            OVER (
                PARTITION BY PatientId
                ORDER BY AdmitDate
            ) AS PreviousDischarge

    FROM Encounter

    WHERE EncounterType = 'Inpatient'
)

SELECT

    PatientId,
    EncounterId,
    AdmitDate,
    PreviousDischarge,

    -- Number of days between visits

    DATEDIFF(
        DAY,
        PreviousDischarge,
        AdmitDate
    ) AS DaysBetweenVisits

FROM OrderedEncounters

WHERE PreviousDischarge IS NOT NULL

AND DATEDIFF(
        DAY,
        PreviousDischarge,
        AdmitDate
    ) <= 30;
