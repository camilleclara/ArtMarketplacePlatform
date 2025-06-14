import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ProductsComponent } from './products/products.component';
import { authGuard } from './login/auth.guard';
import { admGuard } from './login/adm.guard';
import { artisanGuard } from './login/artisan.guard';
import { dashboardGuard } from './login/dashboard.guard';
import { DashboardArtisanComponent } from './dashboard/dashboard-artisan/dashboard-artisan.component';
import { DashboardComponent } from './dashboard/dashboard/dashboard.component';
import { DashboardAdminComponent } from './dashboard/dashboard-admin/dashboard-admin.component';
import { DashboardCustomerComponent } from './dashboard/dashboard-customer/dashboard-customer.component';
import { DashboardDeliveryComponent } from './dashboard/dashboard-delivery/dashboard-delivery.component';
import { MessagesComponent } from './messages/messages.component';
import { RegisterComponent } from './register/register.component';
import { CustomerProductDetailComponent } from './products/customer-product-detail/customer-product-detail.component';
import { BasketComponent } from './basket/basket/basket.component';
import { ProfileComponent } from './profile/profile.component';
import { deliveryGuard } from './login/delivery.guard';

export const routes: Routes = [
    { path: 'basket', component: BasketComponent },
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'product/:id', component: CustomerProductDetailComponent},
    {path: 'profile/:id', component: ProfileComponent},
    {path: 'dashboard', component: DashboardComponent,canActivate: [authGuard, dashboardGuard]},
    {path: 'dashboard-artisan', component: DashboardArtisanComponent, canActivate: [authGuard, artisanGuard]},
    {path: 'dashboard-customer', component: DashboardCustomerComponent},
    {path: 'dashboard-delivery', component: DashboardDeliveryComponent, canActivate: [authGuard, deliveryGuard]},
    {path: 'dashboard-admin', component: DashboardAdminComponent, canActivate: [authGuard, admGuard]},
    {path: 'messages', component: MessagesComponent, canActivate: [authGuard]},
    // {path: 'dashboard-delivery', component: DashboardDeliveryComponent, canActivate: [authGuard, artisanGuard]}, -- TODO delivery guard
    // {path: 'dashboard-customer', component: DashboardCustomerComponent, canActivate: [authGuard, artisanGuard]},  --TODO customer guard
    {path: '**', component: DashboardComponent,canActivate: [authGuard, dashboardGuard]},
    //TODO error page
];
