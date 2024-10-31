import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreatedUser } from '../../Models/User.model';
import { FollowService } from '../../Services/Follow.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-suggested-to-follow',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './suggested-to-follow.component.html',
  styleUrl: './suggested-to-follow.component.css'
})
export class SuggestedToFollowComponent {
  
  @Input()
  suggestedUsers :CreatedUser[]=[];
 @Input()
  followerId:number=0;
  followStates: { [userId: number]: boolean } = {};
 


constructor (private followService:FollowService,private router:Router){}

checkFollow(followeeId: number): void {

  if(this.followStates[followeeId])
    {

      this.followService.removeFollow(this.followerId,followeeId).subscribe((response)=>{
        console.log(response);
       })
    }
    else{
      this.followService.createFollow(this.followerId,followeeId).subscribe((response)=>{
        console.log(response);
      });
    }
  this.followStates[followeeId] = !this.followStates[followeeId]; // Toggle the follow state for the specific user
}

isFollowed(followeeId: number): boolean {
     
  return this.followStates[followeeId] || false; // Return follow state or false if not defined
}

goToProfile(userId:number):void{

this.router.navigate(['profile',userId]);
}

}
