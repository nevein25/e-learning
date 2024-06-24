import { Routes } from '@angular/router';
import { ModuleComponent } from './module/module.component';
import { CourseComponent } from './course/course.component';
import { CourseListComponent } from './course-list/course-list.component';
import { CourseMainPageComponent } from './course-main-page/course-main-page.component';

export const routes: Routes = 
[
    {path:"",component:CourseComponent},
    {path:"course", component:CourseComponent },
    {path:"module",component:ModuleComponent},
    {path: 'course-list', component: CourseListComponent},
    {path: 'course-main-page/:id', component: CourseMainPageComponent},
    { path: '**', redirectTo: 'course' }
    
];
