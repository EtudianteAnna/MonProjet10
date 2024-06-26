#!/bin/bash
 
# Change to the directory of each project and start it
echo "Starting ApiGateway..."
(cd ApiGateway && dotnet run &)
 
echo "Starting DiabetesRiskAssessmentService..."
(cd DiabetesRiskAssessmentService && dotnet run &)
 
echo "Starting MedicalNotes-Service..."
(cd MediacalNotes-Service && dotnet run &)
 
echo "Starting Patient-Services..."
(cd Patient-Services && dotnet run &)
 
echo "Starting PatientFrontEnd2..."
(cd MicroFrontEnd && dotnet run &)
 
# Wait for all background processes to complete
wait

