import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SideBarComponent } from "./side-bar/side-bar.component";
import { LogInComponent } from "./log-in/log-in.component";
import { TrendsComponent } from "./trends/trends.component";
import { SuggestedToFollowComponent } from "./suggested-to-follow/suggested-to-follow.component";
import { TweetComponent } from "./tweet/tweet.component";
import { HomeTimelineComponent } from "./home-timeline/home-timeline.component";
import { CreatedUser } from '../Models/User.model';
import { FollowService } from '../Services/Follow.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SideBarComponent, LogInComponent, TrendsComponent, SuggestedToFollowComponent, TweetComponent, HomeTimelineComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Kwikker-Frontend';
  userId=2;
  users:CreatedUser[]=[];

  constructor (private followService:FollowService)
  {
     console.log("Hello from app component");
  }

  ngOnInit():void{
    console.log("test from app");
    this.followService.getSuggestedUsersToFollow(this.userId).subscribe((data:CreatedUser[])=>{
      this.users=data;
      console.log(data);
    },(error)=>{
      console.error('Error fetching suggested users',error);
    })

  }
    
  }
