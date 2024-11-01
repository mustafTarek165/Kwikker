import { Component, EventEmitter, Input, Output, SimpleChanges,  } from '@angular/core';
import { TweetService } from '../../Services/Tweet.service';
import { CreatedTweet } from '../../Models/Tweet.model';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { BookmarkService } from '../../Services/Bookmark.service';

@Component({
  selector: 'app-tweet',
  standalone: true,
  imports: [CommonModule,HttpClientModule],
  templateUrl: './tweet.component.html',
  styleUrl: './tweet.component.css'
})
export class TweetComponent {
   
 @Input() tweet!:CreatedTweet

 @Input() userId=0;
 
 @Input() isRetweeted:boolean=false;
 @Input() isLiked:boolean=false;
 @Input() isBookmarked:boolean=false;


 
 @Output() tweetDeleted = new EventEmitter<CreatedTweet>();

@Output() updateTweet=new EventEmitter<CreatedTweet>();
  constructor(private tweetService: TweetService,private bookmarkService:BookmarkService) { }

  updateLike():void
  {
         if(this.isLiked &&this.tweet.likesNumber>0)
          {
                
              this.tweetService.removeLike(this.userId,this.tweet.id).subscribe();
              this.tweet.likesNumber--;
          } 
          else
          {
            
              this.tweetService.createLike(this.userId,this.tweet.id).subscribe();     
              this.tweet.likesNumber++; 
          } 
          this.isLiked=!this.isLiked;
  }
  updateBookmark():void{
     if(this.isBookmarked&&this.tweet.bookmarksNumber>0)
     {
      this.bookmarkService.removeBookmark(this.userId,this.tweet.id).subscribe();
      this.tweet.bookmarksNumber--;
     }
     else 
     {
      this.bookmarkService.createBookmark(this.userId,this.tweet.id).subscribe();
      this.tweet.bookmarksNumber++;
     }
     this.isBookmarked=!this.isBookmarked;
  }
  
  updateRetweet():void{
    if(this.isRetweeted&&this.tweet.retweetsNumber>0)
    {
      this.tweetService.removeRetweet(this.userId,this.tweet.id).subscribe();
      this.tweet.retweetsNumber--;
    }
    else 
    {
      this.tweetService.createRetweet(this.userId,this.tweet.id).subscribe();
      this.tweet.retweetsNumber++;
    }
    this.isRetweeted=!this.isRetweeted;
 }
 isDropdownOpen = false;

toggleDropdown() {
  this.isDropdownOpen = !this.isDropdownOpen;
}

editTweet() {
  // Logic for editing the tweet

  this.updateTweet.emit(this.tweet);
  console.log("Edit tweet clicked");
}

deleteTweet() {
  // Logic for deleting the tweet
  console.log("Delete tweet clicked");
  this.tweetService.removeTweet(this.tweet.id).subscribe();
  this.tweetDeleted.emit(this.tweet);
}

}
