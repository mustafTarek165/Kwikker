import { Routes } from '@angular/router';
import { HomeTimelineComponent } from './home-timeline/home-timeline.component';

export const routes: Routes = 
[   {path:'home',component:HomeTimelineComponent} , 
    { path: '**', redirectTo: '/home' }   

];
