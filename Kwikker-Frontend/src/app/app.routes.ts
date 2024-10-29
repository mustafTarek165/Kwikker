import { Routes } from '@angular/router';
import { HomeTimelineComponent } from './home-timeline/home-timeline.component';
import { ProfileComponent } from './profile/profile.component';
import { NotificationsComponent } from './notifications/notifications.component';
import { FollowersComponent } from './followers/followers.component';
import { FolloweesComponent } from './followees/followees.component';

export const routes: Routes = 
[   {path:'home',component:HomeTimelineComponent} , 
    {path:'profile',component:ProfileComponent},
    {path:'notifications',component:NotificationsComponent},
    {path:'followers',component:FollowersComponent},
    {path:'followees',component:FolloweesComponent},
    { path: '**', redirectTo: '/home' }

];
