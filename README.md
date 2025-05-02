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
            [X]View one product details
                [X]If I have already purchased it, I can leave a review
            [X]View artisan's profile
            [X] View & edit my own profile
        Create a basket of products
            [X]Add, remove from the basket
            [X]View basket
            [X]Checkout + select shipping option + entering payment details
            [X]order confirmation & payment sumaary
        View my orders + delivery status
            [X] Get all my orders + delivery status and dipslay
            [ ] Make sure as a customer I can only update to "RECEIVED"
    delivery-partner
        [ ]View orders assigned for delivery (+ filter on status or other attribute)
        [ ]Coordinate Pickup with Artisans
        [ ]Update delivery Status
        [ ]View the status of the deliveries
    Admin
        [ ]Manage users (approve, deactivate accounts)
        [ ]Manage products (approve or delete)
        [ ]View statistics, products trends and user activities 
    Artisan
        [ ]Financial report


   ```bash
   Scaffold-DbContext "Server=DESKTOP-84DA7B5\SQLEXPRESS;Database=MarketPlace;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir ../Domain -ContextDir ../DAL -Namespace Domain -ContextNamespace DAL -Force
   ```