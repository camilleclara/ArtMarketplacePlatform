export enum DeliveryStatus {
    NEW = "NEW", //La commande est créée par le customer
    PROCESSING = "PROCESSING", // La commande est reçue par l'artisan, ou en train d'être picked up par le partner
    SHIPPED = "SHIPPED", //La commande est dans les mains du partner
    INTRANSIT = "IN TRANSIT", //En cours de livraison
    CANCELLED = "CANCELLED", // Annulation
    DELIVERED = "DELIVERED", // Livrée
    RECEIVED = "RECEIVED" // Bien reçue par le consommateur
}