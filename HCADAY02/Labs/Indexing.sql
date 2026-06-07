SELECT
    PatientId,
    FullName,
    City
FROM Patient
WHERE City = 'Hyderabad'
AND IsActive = 1;



Create An Index That Matches The Search
-- City is filtered
-- IsActive is filtered
-- FullName is returned

CREATE NONCLUSTERED INDEX IX_Patient_City_Active

ON Patient
(
    City,
    IsActive
)

INCLUDE
(
    FullName
);




Investigation #2
Recent Encounter Lookup
The ED screen runs:
DECLARE @PatientId INT = 1;

SELECT TOP 10

    EncounterId,
    AdmitDate,
    DischargeDate,
    EncounterType

FROM Encounter

WHERE PatientId = @PatientId

ORDER BY AdmitDate DESC;


Table 'Encounter'. Scan count 1, logical reads 19, physical reads 1, page server reads 0, read-ahead reads 10, page server read-ahead reads 0, lob logical reads 0, lob physical reads 0, lob page server reads 0, lob read-ahead reads 0, lob page server read-ahead reads 0.

Create The Right Index
-- Match filter first
-- Match sort second

CREATE NONCLUSTERED INDEX IX_Encounter_Patient_Admit

ON Encounter
(
    PatientId,
    AdmitDate DESC
)

INCLUDE
(
    DischargeDate,
    EncounterType
);
