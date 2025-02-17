import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ProductsComponent } from './products/products.component';
import { authGuard } from './login/auth.guard';
import { admGuard } from './login/adm.guard';
import { userGuard } from './login/user.guard';
import { AdminComponent } from './admin/admin.component';

export const routes: Routes = [
    {path: 'login', component: LoginComponent},
    {path: 'weather', component: ProductsComponent, canActivate: [authGuard, userGuard]},
    {path: 'admin', component: AdminComponent, canActivate: [authGuard, admGuard]}
];
