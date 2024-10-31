import { Component, Input,  } from '@angular/core';
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
  isRetweeted:boolean=false;
  isLiked:boolean=false;
  isBookmarked:boolean=false;
  constructor(private tweetService: TweetService,private bookmarkService:BookmarkService) { }

  updateLike():void
  {
         if(this.isLiked)
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
     if(this.isBookmarked)
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
    if(this.isRetweeted)
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
 

}
