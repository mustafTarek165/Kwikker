import { Routes } from '@angular/router';
import { HomeTimelineComponent } from './home-timeline/home-timeline.component';
import { ProfileComponent } from './profile/profile.component';
import { FollowersComponent } from './followers/followers.component';
import { FolloweesComponent } from './followees/followees.component';

export const routes: Routes = 
[   {path:'home',component:HomeTimelineComponent} , 
    {path:'profile/:id',component:ProfileComponent},
    {path:'followers/:id',component:FollowersComponent},
    {path:'followees/:id',component:FolloweesComponent},
    { path: '**', redirectTo: '/home' }

];
