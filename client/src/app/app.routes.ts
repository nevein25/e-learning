import { Routes } from '@angular/router';
import { CourseListComponent } from './course-list/course-list.component';
import { CourseMainPageComponent } from './course-main-page/course-main-page.component';


export const routes: Routes = [
    {path: 'course-list', component: CourseListComponent},
    {path: 'course-main-page/:id', component: CourseMainPageComponent},
];

