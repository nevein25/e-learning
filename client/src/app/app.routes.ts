import { Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MembershipCheckoutComponent } from './payments/membership-checkout/membership-checkout.component';
import { MembershipOptionsComponent } from './payments/membership-options/membership-options.component';
import { CourseCheckoutComponent } from './payments/course-checkout/course-checkout.component';
import { SuccessPaymentComponent } from './payment/success-payment/success-payment.component';
import { studentGuard } from './_guards/student.guard';
import { authGuard } from './_guards/auth.guard';

export const routes: Routes = [
    { path: 'register', component: RegisterComponent},
    { path: 'login', component: LoginComponent },
    { path: 'not-found', component: NotFoundComponent },
    { path: 'server-error', component: ServerErrorComponent },
    {path: 'checkout',  component: MembershipCheckoutComponent},
    {path: 'membership-options',  component: MembershipOptionsComponent},
    {path: 'course-payment/:id',  component: CourseCheckoutComponent},
    {path: 'success-payment',  component: SuccessPaymentComponent},



    // {path: '**', component: LoginComponent, pathMatch: 'full'}, // the default if no match, maybe home component
];
