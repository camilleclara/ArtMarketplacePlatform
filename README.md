# ArtMarketplacePlatform
dotnet ef dbcontext scaffold "Server=DESKTOP-84DA7B5\SQLEXPRESS;Database=MarketPlace;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context MarketPlaceContext --force

dotnet ef dbcontext scaffold Name=MarketPlaceDB Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context MarketPlaceContext --force

dashboard 
    artisan
        display all my products
            Display
            Edition
            Creation
            -> Images:
                [X] Aligner sur la droite
                [X] Bouton pour ajouter des images
                [X] Bouton pour supprimer
                [X] Bouton apparait au hover

        display all my orders
            [X] Display orders for my products
                [X] GetOrdersByArtisanId 
                    [X] include products via item_orders
                    [X] include the total amount
            [X] Update the status "in production", "shipped", ...
        display all my customers reviews
            [X] Display all my reviews
            [X] Sort by product
            [X] Answer to review
        View my message
             [x] View messages
             [x] Respond to messages
    customer
        View all Products
            [X]Filter by category
            [X]Filter by price
            [X]Filter by artisan
        View one product details
            [ ]If I have already purchased it, I can leave a review
            [ ]View artisan's profile
        Create a basket of products
            [ ]Add, remove from the basket
            [ ]View basket
            [ ]Checkout + select shipping option + entering payment details
            [ ]order cnfirmation & payment sumaary
        View my orders + delivery status

   ```bash
   Scaffold-DbContext "Server=DESKTOP-84DA7B5\SQLEXPRESS;Database=MarketPlace;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir ../Domain -ContextDir ../DAL -Namespace Domain -ContextNamespace DAL -Force
   ```