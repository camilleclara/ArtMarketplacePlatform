using Microsoft.EntityFrameworkCore.Migrations;
using static System.Net.Mime.MediaTypeNames;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "id", "first_name", "hashed_password", "is_active", "last_name", "login", "salt", "user_type", "full_address" },
            values: new object[,]
            {
                { 1,  "Camille", "EE1D043DE283E12CD10A", true, "Berrier-Plater Syberg", "camcam", "Sunday", "ADMIN", "4 rue Camille Claudel 1000 Bruxelles" },
                { 2,  "Henri", "EE1D043DE283E12CD10A", true, "Dupont", "customer", "Sunday", "CUSTOMER", "121 rue Manet 1040 Etterbeek" },
                { 3,  "Gérard", "EE1D043DE283E12CD10A", true, "Agediss", "deliverypartner", "Sunday", "DELIVERYPARTNER","6 rue du Fossé aux Loups, 1000 Bruxelles" },
                { 4,  "Charles", "EE1D043DE283E12CD10A", true, "Dupont", "artisan", "Sunday", "ARTISAN", "78 avenue de la Couronne, 1050 Ixelles" },
                { 5,  "Vincent", "EE1D043DE283E12CD10A", true, "Van Gogh", "artisan2", "Sunday", "ARTISAN", "Rue des étangs 67, 1090" },
                { 6,  "Claude", "EE1D043DE283E12CD10A", true, "Monet", "artisan3", "Sunday", "ARTISAN", "45 rue de l'adresse postale, 75006 Paris" }
            });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "id", "artisan_id", "customer_id" },
                values: new object[,]
                {
                    { 1, 4, 1 },
                    { 2, 4, 2 }
                }
            );

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "artisan_id", "category", "description", "name", "price" },
                values: new object[,]
                {
                    { 1, 4, "PAINTING", "Acrylic painting of a blue whale under water, by Scott Highlander", "Whale painting", 400.5 },
                    { 2, 4, "POTTERY", "The perfect getting started kit for pottery enthousiasts", "Pottery Kit", 20.5 },
                    { 3, 4, "JEWELS", "A beautiful topaze chocker necklace", "Topaze necklace", 50.5 },
                    { 4, 5, "PAINTING", "Sunflowers, la peinture connue", "Tournesols", 5000000.0 },
                    { 5, 6, "PAINTING", "Peinture impressioniste de Nénuphars", "Nénuphars", 560.5 }
                }
            );

            migrationBuilder.InsertData(
                table: "Deliveries",
                columns: new[] { "id", "deli_status", "delivery_date", "estimated_date", "order_id", "partner_id" },
                values: new object[] { 1, "PROCESSING", null, null, 1, null });

            migrationBuilder.InsertData(
                table: "Deliveries",
                columns: new[] { "id", "deli_status", "delivery_date", "estimated_date", "is_active", "order_id", "partner_id" },
                values: new object[] { 2, "PROCESSING", null, null, true, 1, null });

            migrationBuilder.InsertData(
                table: "Deliveries",
                columns: new[] { "id", "deli_status", "delivery_date", "estimated_date", "order_id", "partner_id" },
                values: new object[] { 3, "SHIPPED", null, null, 2, 3 });

            migrationBuilder.InsertData(
                table: "Item_Orders",
                columns: new[] { "id", "order_id", "product_id", "quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 3 },
                    { 2, 1, 2, 1 },
                    { 3, 2, 3, 6 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "id", "content", "customer_id", "fromArtisan", "is_active", "product_id", "score" },
                values: new object[,]
                {
                    { 1, "Beautiful", 2, false, true, 3, 5 },
                    { 2, "Thank you :)", 2, true, true, 3, 5 },
                    { 3, "Whimsical!", 1, false, true, 1, 4 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "id", "msg_from_id", "msg_to_id", "content", "created" },
                values: new object[,]
                {
                    { 1, 2, 4, "Bonjour, je voudrais avoir plus d'informations sur votre artisanat, avez-vous un site web ?", new DateTime(2025, 5, 19, 19, 56, 4, 707) },
                    { 2, 4, 2, "Bonjour, merci pour votre message :) Je n'ai pas de site pour l'instant, mais je me ferai un plaisir de répondre à vos questions.", new DateTime(2025, 5, 19, 19, 57, 4, 707) },
                    { 3, 1, 4, "Hello, ça va ?", new DateTime(2025, 5, 19, 19, 58, 4, 707) },
                    { 4, 4, 1, "Hey !, Bien et toi?", new DateTime(2025, 5, 19, 19, 59, 4, 707) },
                    { 5, 4, 1, "ça fait un bail dis donc...", new DateTime(2025, 5, 19, 20, 56, 4, 707) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
