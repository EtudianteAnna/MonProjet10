MonProjet10 est une application composée de plusieurs microservices développés avec ASP.NET Core. Ces microservices sont accessibles via une passerelle API (API Gateway) utilisant Ocelot. L’application est conçue pour aider les médecins à identifier les patients présentant des risques de diabète en utilisant des notes et des données démographiques.

L’architecture du projet comprend les éléments suivants :

        •       API Gateway : Utilise Ocelot pour acheminer les requêtes vers les microservices appropriés.
        •       Microservices :
        •       Patient-Services : Gère les données des patients.
        •       DiabetesRiskAssessmentService : Évalue les risques de diabète des patients.
        •       MedicalNotes-Service : Gère les notes médicales des patients.
        •       MicroFrontEnd : Interface utilisateur pour l’interaction avec les services.
        •       Bases de données : Utilise MySQL pour les données relationnelles et MongoDB pour les notes médicales.

Configuration et Déploiement

Prérequis

        •       Docker et Docker Compose
        •       .NET 6.0 SDK

Installation

        1.      Clonez le dépôt :

git clone https://urldefense.com/v3/__https://github.com/votre-repo/MonProjet10.git__;!!BnkV9pdh5V0!APsakMj8O5tz3BF9GO7YHBc0ZytG0sFe_eiaZ-RMrr4fmZnUO-T0eJC_OtnGXKffVHxNI1AKl2oncycvE-qA$
cd MonProjet10


        2.      Configurez les fichiers de configuration nécessaires :
        •       appsettings.json
        •       ocelot.json

Démarrage des Services

Utilisez Docker Compose pour démarrer tous les services :

docker-compose up --build

Services et Endpoints

API Gateway

        •       URL : http://localhost:5106__;!!BnkV9pdh5V0!APsakMj8O5tz3BF9GO7YHBc0ZytG0sFe_eiaZ-RMrr4fmZnUO-T0eJC_OtnGXKffVHxNI1AKl2onc1wtLWUv$

Patient Services

        •       URL : http://localhost:5000__;!!BnkV9pdh5V0!APsakMj8O5tz3BF9GO7YHBc0ZytG0sFe_eiaZ-RMrr4fmZnUO-T0eJC_OtnGXKffVHxNI1AKl2onc_pfnCFV$
        •       Endpoints :
        •       GET /api/patients
        •       POST /api/patients

Diabetes Risk Assessment Service

        •       URL : http://localhost:5002__;!!BnkV9pdh5V0!APsakMj8O5tz3BF9GO7YHBc0ZytG0sFe_eiaZ-RMrr4fmZnUO-T0eJC_OtnGXKffVHxNI1AKl2onc568NQCI$
        •       Endpoints :
        •       GET /api/riskassessment/{patientId}

Medical Notes Service

        •       URL : http://localhost:5001__;!!BnkV9pdh5V0!APsakMj8O5tz3BF9GO7YHBc0ZytG0sFe_eiaZ-RMrr4fmZnUO-T0eJC_OtnGXKffVHxNI1AKl2oncxM7IdTa$
        •       Endpoints :
        •       GET /api/notes/patient/{patientId}
        •       POST /api/notes/patient/{patientId}

Exigences du Client

Normes ISO et Sécurité

        •       Les bases de données doivent être normalisées (3NF) pour garantir la qualité des données.
        •       L’accès aux données des patients doit être sécurisé avec un système d’authentification via Identity.
        •       Politique de protection de l’environnement pour se conformer au Green Code.

Fonctionnalités Clés

        •       Identification des patients à risque en fonction des données démographiques et des notes.
        •       Ajout et gestion des notes médicales par les médecins.
        •       Génération de rapports sur le risque de diabète.

Spécifications Techniques

        •       Bases de données :
        •       MySQL pour les données relationnelles (Patients).
        •       MongoDB pour les notes médicales.

Green Code

Respect des principes de Green Code dans le développement et le déploiement du projet.

Documentation

        •       Implémentation de API Gateways avec Ocelot
        •       Micro frontends avec ASP.NET Core MVC
        •       Création de web API avec ASP.NET Core et MongoDB
        •       Containerization avec Docker

Contributions

Les contributions sont les bienvenues. Veuillez soumettre une pull request pour toute modification.

Schéma UML 
+-----------------+
|  Frontend       |
|  Application    |
|  (MicroFrontEnd)|
+--------+--------+
         |
         v
+--------+--------+
|  API Gateway    |
|  (Ocelot)       |
+---+----+----+---+
    |    |    |
    |    |    |
    |    |    |
    |    |    |
    |    |    |
    v    v    v
+---+--+ +--+-+--+ +--+-+--+
|Patient| |Diabetes| |Medical|
|Service| |Risk    | |Notes  |
|       | |Assessment| |Service|
+---+--+ +--+-+--+ +--+-+--+
    |          |           |
    v          v           v
+---+---+   +--+-+--+  +--+-+--+
| MySQL |   |          | | MongoDB |
|       |   |          | |        |
+-------+   +----------+ +--------+
```
