import { Routes } from '@angular/router';
import { ModuleComponent } from './module/module.component';
import { CourseComponent } from './course/course.component';

export const routes: Routes = 
[
    {path:"",component:CourseComponent},
    {path:"course", component:CourseComponent },
    {path:"module",component:ModuleComponent},
    { path: '**', redirectTo: 'course' }
    
];
