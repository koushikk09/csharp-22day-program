EXEC usp_ReadmissionAnalytics
    @WithinDays = 30;
The application, reporting team, and analytics team all receive the same answer.

CREATE OR ALTER PROCEDURE usp_ReadmissionAnalytics
    @WithinDays INT = 30
AS
BEGIN

    SET NOCOUNT ON;

    -- Build a patient timeline

    WITH OrderedEncounters AS (

        SELECT

            PatientId,
            EncounterId,
            AdmitDate,

            LAG(DischargeDate)
                OVER (
                    PARTITION BY PatientId
                    ORDER BY AdmitDate
                ) AS PreviousDischarge

        FROM Encounter

        WHERE EncounterType = 'Inpatient'

    )

    -- Find readmissions

    SELECT

        PatientId,
        EncounterId,
        AdmitDate,

        DATEDIFF(
            DAY,
            PreviousDischarge,
            AdmitDate
        ) AS DaysSincePreviousVisit

    FROM OrderedEncounters

    WHERE PreviousDischarge IS NOT NULL

    AND DATEDIFF(
            DAY,
            PreviousDischarge,
            AdmitDate
        ) <= @WithinDays;

END;

Execute
EXEC usp_ReadmissionAnalytics
    @WithinDays = 30;
