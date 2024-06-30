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
import { LessonComponent } from './lesson/lesson.component';
import { HomeComponent } from './home/home.component';
import { WishlistComponent } from './wishlist/wishlist.component';
import { BoughtCoursesListComponent } from './bought-courses-list/bought-courses-list.component';
import { studentGuard } from './_guards/student.guard';
import { SuccessPaymentComponent } from './payments/success-payment/success-payment.component';
import { StudentCourseComponent } from './student-course/student-course.component';
import { UploadedCoursesListComponent } from './uploaded-courses-list/uploaded-courses-list.component';

export const routes: Routes = [
    { path: "", component: HomeComponent },
    { path: "course", component: CourseComponent },
    { path: "module", component: ModuleComponent },
    { path: "lesson", component: LessonComponent },
    { path: 'course-list', component: CourseListComponent },
    { path: 'courses-uploaded', component: UploadedCoursesListComponent },
    { path: 'course-main-page/:id', component: CourseMainPageComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
    { path: 'not-found', component: NotFoundComponent },
    { path: 'server-error', component: ServerErrorComponent },
    { path: 'checkout', component: MembershipCheckoutComponent },
    { path: 'membership-options', component: MembershipOptionsComponent },
    { path: 'course-payment/:id', component: CourseCheckoutComponent },
    { path: 'student-course/:id', component: StudentCourseComponent },
    { path: 'success-payment', component: SuccessPaymentComponent },
    { path: 'wishlist', component: WishlistComponent },
    { path: 'bought-courses-list', component: BoughtCoursesListComponent, canActivate: [studentGuard] },
    {path: '**', component: HomeComponent, pathMatch: 'full'}

];
