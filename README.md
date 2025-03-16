# ArtMarketplacePlatform
dotnet ef dbcontext scaffold "Server=DESKTOP-84DA7B5\SQLEXPRESS;Database=MarketPlace;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context MarketPlaceContext --force

dotnet ef dbcontext scaffold Name=MarketPlaceDB Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context MarketPlaceContext --force

dashboard 
    artisan
        display all my products
            Display
            Edition
            Creation
            ->Images
        display all my orders
        display all my customers reviews
