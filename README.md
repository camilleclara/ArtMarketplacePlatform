# ArtMarketplacePlatform

ArtMarketPlacePlatform est une application pour un MarketPlace en ligne, où des artisans peuvent mettre en vente des produits qui seront rendus disponibles aux utilisateurs.

Le frontend est une application angular, utilisant bootstrap.
Le backend est une webapi .NET core, utilisant EntityFramework pour les interactions avec la base de données

## Comment démarrer l'application ?

1. Cloner le dépôt et ouvrir le projet

2. Dans le projet backend-app, ouvrir la solution à l'aide de VisualStudio

3. Configurer la chaîne de connexion de la base de données dans le fichier appsettings.json
    {
        "ConnectionStrings": {
            "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MarketPlace;Trusted_Connection=True;MultipleActiveResultSets=true"

        }
    }
4. Créer la base de données et appliquer les migrations
```dotnet ef database update --project DAL --startup-project backend-app```

5. Lancer l'application backend
```dotnet run --project backend-app```

6. Ouvrir le projet frontend-app dans VS Code

7. Assurez-vous que l'url et le port ciblant le backend sont corrects (baseUrl dans le fichier environment.ts)

7. Installer les dépendances nécessaires
```npm install```

9. Lancer le projet 
```npm run ng serve```

10. Identifiez-vous à l'aide d'un de ces profils:
 - Login: 'camcam'; Password 'password' (rôle admin)
 - Login: 'artisan'; Password 'password' (rôle artisan)
 - Login: 'customer'; Password 'password' (rôle customer)
 - Login: 'deliverypartner'; Password 'password' (rôle deliverypartner)

## Les fonctionnalités:
Toutes les fonctionnalités demandées ont été implémentées, mais la gestion des livraisons reste très basique.

- Possibilité de Login/Logout/Register;
- Gestion des rôles avec JWT Token;
- Utilisateur, y compris non-connecté (onglet "Products");
    - Possibilité de browser les produits disponibles, avec filtres de recherche;
    - Possibilité de remplir un panier avec des articles;
    - Possibilité de visualiser et gérer son panier;
- Utilisateur inscrit/Customer:
    - Possibilité de passer commande;
    - Possibilité de visualiser son propre profil, y compris l'historique des commandes;
    - Possibilite de modifier le statut d'une commande à 'RECEIVED';
    - Possibilité de voir le profil des autres utilisateurs;
    - Possibilité d'envoyer un message aux autres utilisateurs;
    - Possibilité de voir l'historique des messages;
    - Possibilité de laisser un commentaire sur un produit déjà acheté;
- Dashboard admin:
    - Visualisation des statistiques de produits, commandes, utilisateurs
    - Gestion des utilisateurs (Visualisation, modifications, déletions, approbation), avec filtres de recherches
    - Gestion des produits (Visualisation, modifications, déletions, approbation), avec filtres de recherches
- Dashboard artisan:
    - Gestion des produits de l'artisan (création, suppression, modification) avec gestion des images
    - Gestion des commandes: possibilité de modifier le statut (PROCESSING, SHIPPED, CANCELLED) et de choisir le partenaire de livraison, possibilité de voir les détails de la commande
    - Avis clients: possibilité de voir les avis des produits de l'artisan, et de produire une réponse
    - Statistiques dashboard;
- Dashboard delivery partner:
    - Gestion des livraisons, y compris update des statuts 
