import { Routes } from '@angular/router';
import { HomeTimelineComponent } from './home-timeline/home-timeline.component';
import { ProfileComponent } from './profile/profile.component';
import { NotificationsComponent } from './notifications/notifications.component';

export const routes: Routes = 
[   {path:'home',component:HomeTimelineComponent} , 
    {path:'profile',component:ProfileComponent},
    {path:'notifications',component:NotificationsComponent},
    { path: '**', redirectTo: '/home' }

];
