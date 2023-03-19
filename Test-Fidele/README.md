- dotnet ef migrations add create-table-societes --startup-project ../Test-Fidele/Test-Fidele.csproj
- dotnet ef Database update --startup-project ../Test-Fidele/Test-Fidele.csproj 

# Test Technique: Développeur .NET HaiRun Technology

## Sujet : Créer une application (Distributeur) d'enregistrement de produit.

### Indication
-   Implémenter une structure ***CRUD*** de produit d'un distributeur;
-	Les données d'un produit sont constituées de: 
    - **Name** (obligatoire) (affiché en mode insertion ou édition)
    - **Matricule** (obligatoire) (affiché en mode insertion ou édition)
    - **Slug**  (obligatoire) (Lecture seule) (chaine alphanumérique autogénéré au moment de la création du formulaire) (affiché en mode insertion ou édition)
    - **Date_Create** (obligatoire) (lecture seule) (auto-créé en backend controlers et non en forme) (affiché en format d'édition)
    - **Date_Edit** (obligatoire) (lecture seule) (auto-créé en backend controlers et non en forme) (affiché en format d'édition)
-	L'interface de l'application sera diviser en page avec:
    -  La **page principale** qui affiche un tableau de liste de produits
    -  La **page du formulaire de création** de produit
    -  La **page du formulaire d'édition** d'un produit enregistré
    -  D'un **popup modal d'avertissement de suppréssion** d'un produit

### Fonctionnement
-	Le **tableau** dans le **menu principal** contient les **produits par ordre d'ajout**; (le dernier Insert sera placé en haut de la liste)
-	Un **système de pagination** pour permettre de **réduire la liste d'éléments affichés** sur le tableau (10 produits affichés par défaut)
-   Un bouton **Add** est à créer en haut à gauche du tableau pour **accéder à la page de création de produits**
-   Le **formulaire** dans la **page de création et d'édition** doit avoir 2 boutons:
    - Un boutton **Save** qui enregistre les modification du formulaire
    - Un boutton **Cancel** qui annule la saisie du formulaire et retourne vers la page principale
-   Sur chaque ligne du tableau correspondant à un produit, il faut créer un bouton **Delete** qui:
    - Enclenche l'affichage d'un modal qui demande si oui ou non vous voulez supprimer le produit
    - **Oui** supprime le produit, cache le modal et enclenche la mise à jour de l'affichage du tableau de liste de produits
    - **Non** Annule le modal

### Technologie
- ASP.Net Core MVC
- Angular ou Bootstrap front
- Entity Framework Core
- Framework .NET 6 ou version supérieure
- C# version 6 ou version supérieure

### Condition requis 
- L'ensemble du programme doit être Dockeriser en deux parties ( Partie 1 : Programme ASP, Partie 2 Base de données)
- Inclure SONARQUBE Test
- Test Unitaire 

### BDD
- SQL Server ou PostgreSQL

## Recommandation

Vous disposez de 72 heures pour effectuer le test.

Ne vous inquiétez pas si vous ne pouvez pas tout faire, faites de votre mieux :). Nous préférons de petits changements très propres et utiles plutôt que beaucoup de changements inachevés.
