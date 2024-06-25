import { Routes } from '@angular/router';

import { ModuleComponent } from './module/module.component';
import { CourseComponent } from './course/course.component';
import { CourseListComponent } from './course-list/course-list.component';
import { CourseMainPageComponent } from './course-main-page/course-main-page.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MembershipCheckoutComponent } from './payments/membership-checkout/membership-checkout.component';
import { MembershipOptionsComponent } from './payments/membership-options/membership-options.component';
import { CourseCheckoutComponent } from './payments/course-checkout/course-checkout.component';
import { SuccessPaymentComponent } from './payments/success-payment/success-payment.component';
import { HomeComponent } from './home/home.component';


export const routes: Routes = [
    { path: "", component: HomeComponent },
    { path: "course", component: CourseComponent },
    { path: "module", component: ModuleComponent },
    { path: 'course-list', component: CourseListComponent },
    { path: 'course-main-page/:id', component: CourseMainPageComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
    { path: 'not-found', component: NotFoundComponent },
    { path: 'server-error', component: ServerErrorComponent },
    { path: 'checkout', component: MembershipCheckoutComponent },
    { path: 'membership-options', component: MembershipOptionsComponent },
    { path: 'course-payment/:id', component: CourseCheckoutComponent },
    { path: 'success-payment', component: SuccessPaymentComponent },
    {path: '**', component: HomeComponent, pathMatch: 'full'}
];
