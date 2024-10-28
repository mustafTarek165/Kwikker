import { Component, ElementRef, ViewChild } from '@angular/core';
import { TimelineService } from '../../Services/Timeline.service';
import { CreatedTweet } from '../../Models/Tweet.model';
import { CommonModule } from '@angular/common';
import { TweetComponent } from "../tweet/tweet.component";
import { BookmarkService } from '../../Services/Bookmark.service';
import { CreatedBookmark } from '../../Models/Bookmark.model';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, TweetComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
 userId=1;
 profileTweets:any=[];
 count:number=0;

 @ViewChild('posts') posts!:ElementRef<HTMLButtonElement>
@ViewChild('likes') likes!:ElementRef<HTMLButtonElement>
@ViewChild('bookmarks') bookmarks!:ElementRef<HTMLButtonElement>
 

activeButton!:ElementRef<HTMLButtonElement>;

constructor(private timelineService:TimelineService,private bookmarksService:BookmarkService)
{
 
}
ngAfterViewInit(): void {
  this.activeButton = this.posts; // Initialize activeButton here


  this.getUserProfile();
}


ngOnInit():void{


    
}
ngAfterView():void{
  this.activeButton=this.posts;
  this.changeTapStatus(this.posts);
}

getUserProfile():void{
  this.changeTapStatus(this.posts);
this.timelineService.getProfile(this.userId).subscribe((data:CreatedTweet[])=>{
    this.profileTweets=data;
    this.count=this.profileTweets.length;
  },(error)=>{
   console.log('error at fetching profile data',(error));
  })
}

getUserBookmarks():void{
  this.changeTapStatus(this.bookmarks);
this.bookmarksService.getBookmarks(this.userId).subscribe((data:CreatedBookmark[])=>{
  this.profileTweets=data;
  this.count=this.profileTweets.length;
},(error)=>{
  console.log('error fetching user bookmarks',error);
})
}

getUserLikedTweets():void{
  
  this.changeTapStatus(this.likes);
  this.timelineService.getUserLikedTweets(this.userId).subscribe((data:CreatedTweet[])=>{
    this.profileTweets=data;
    this.count=this.profileTweets.length;
  }, (error)=>{
    console.log('error fetching user liked tweets',error);
  })
}

changeTapStatus(selectedButton: ElementRef<HTMLButtonElement>): void {
  if (this.activeButton) {
    // Reset previous active button
    this.activeButton.nativeElement.style.backgroundColor = 'inherit';
    this.activeButton.nativeElement.style.color = 'rgb(119, 111, 111)';
  }
 
  // Set the new active button styles
  selectedButton.nativeElement.style.backgroundColor = 'rgb(38, 39, 40)';
  selectedButton.nativeElement.style.color = 'white';

  // Update the active button reference
  this.activeButton = selectedButton;
}

}
