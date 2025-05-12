using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    login = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    hashed_password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    salt = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    first_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    last_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    user_type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    full_address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3213E83FC8CF1179", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryArtisanPartnerships",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    delivery_partner_id = table.Column<int>(type: "int", nullable: true),
                    artisan_id = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Delivery__3213E83F3E048A52", x => x.id);
                    table.ForeignKey(
                        name: "FK_DeliveryArtisanPartnerships_Artisans",
                        column: x => x.artisan_id,
                        principalTable: "Users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_DeliveryArtisanPartnerships_Partner",
                        column: x => x.delivery_partner_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    artisan_id = table.Column<int>(type: "int", nullable: true),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__3213E83FF2C651DD", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_Artisans",
                        column: x => x.artisan_id,
                        principalTable: "Users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Orders_Customers",
                        column: x => x.customer_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    artisan_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    price = table.Column<double>(type: "float", nullable: true),
                    category = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    is_available = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Products__3213E83F30898AB0", x => x.id);
                    table.ForeignKey(
                        name: "FK_Products_Artisans",
                        column: x => x.artisan_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    deli_status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    estimated_date = table.Column<DateOnly>(type: "date", nullable: true),
                    delivery_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    partner_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Deliveri__3213E83FAB447223", x => x.id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Orders",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Deliverie__partn__6E01572D",
                        column: x => x.partner_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Item_Orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Item_Ord__3213E83FC12496B8", x => x.id);
                    table.ForeignKey(
                        name: "FK_ItemOrders_Orders",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ItemOrders_Products",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    msg_from_id = table.Column<int>(type: "int", nullable: true),
                    msg_to_id = table.Column<int>(type: "int", nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    content = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Messages__3213E83FFA6CD293", x => x.id);
                    table.ForeignKey(
                        name: "FK_Message_From",
                        column: x => x.msg_from_id,
                        principalTable: "Users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Message_Product",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Message_To",
                        column: x => x.msg_to_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    mime_type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductI__3213E83FA06CF177", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: true),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    content = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    fromArtisan = table.Column<bool>(type: "bit", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    score = table.Column<int>(type: "int", nullable: true),
                    last_updated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__3213E83FB1F51E75", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers",
                        column: x => x.customer_id,
                        principalTable: "Users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Reviews_Products",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_order_id",
                table: "Deliveries",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_partner_id",
                table: "Deliveries",
                column: "partner_id");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryArtisanPartnerships_artisan_id",
                table: "DeliveryArtisanPartnerships",
                column: "artisan_id");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryArtisanPartnerships_delivery_partner_id",
                table: "DeliveryArtisanPartnerships",
                column: "delivery_partner_id");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Orders_order_id",
                table: "Item_Orders",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Orders_product_id",
                table: "Item_Orders",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_msg_from_id",
                table: "Messages",
                column: "msg_from_id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_msg_to_id",
                table: "Messages",
                column: "msg_to_id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_product_id",
                table: "Messages",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_artisan_id",
                table: "Orders",
                column: "artisan_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_customer_id",
                table: "Orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_product_id",
                table: "ProductImages",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_artisan_id",
                table: "Products",
                column: "artisan_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_customer_id",
                table: "Reviews",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_product_id",
                table: "Reviews",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Users_logins",
                table: "Users",
                column: "login",
                unique: true,
                filter: "[login] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "DeliveryArtisanPartnerships");

            migrationBuilder.DropTable(
                name: "Item_Orders");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
