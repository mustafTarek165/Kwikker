import { Component, ElementRef, ViewChild, AfterViewInit, Input, Output, EventEmitter } from '@angular/core';
import { TimelineService } from '../../Services/Timeline.service';
import { CreatedTweet } from '../../Models/Tweet.model';
import { CommonModule } from '@angular/common';
import { TweetComponent } from '../tweet/tweet.component';
import { BookmarkService } from '../../Services/Bookmark.service';
import { FollowService } from '../../Services/Follow.service';
import { CreatedUser } from '../../Models/User.model';

import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../Services/User.service';
import { TweetService } from '../../Services/Tweet.service';
import { TweetPostComponent } from "../tweet-post/tweet-post.component";
import { RequestParameters } from '../../Models/RequestParameters.model';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, TweetComponent, TweetPostComponent,InfiniteScrollModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements AfterViewInit {
  requestParameters:RequestParameters={
    PageNumber: 1,
    PageSize: 6,
    OrderBy: '',
    Fields: ''
  }
  userId: number = 0;
  profileTweets= new Set<CreatedTweet>();
  followers: CreatedUser[] = [];
  followees: CreatedUser[] = [];
  user!: CreatedUser;
  count: number = 0;
  showTweetPost:boolean=false;
  tweetForUpdate!:CreatedTweet;
  allTweets=new Set<number>();
  bookmarkes=new Set<number>();
  likedTweets=new Set<number>();
  userRetweets=new Set<number>();

  @ViewChild('posts') posts!: ElementRef<HTMLButtonElement>;
  @ViewChild('likes') likes!: ElementRef<HTMLButtonElement>;
  @ViewChild('bookmarks') bookmarks!: ElementRef<HTMLButtonElement>;
@ViewChild('retweets') retweets!:ElementRef<HTMLButtonElement>;

  activeButton!: ElementRef<HTMLButtonElement>;

  constructor(
    private timelineService: TimelineService,
    private bookmarksService: BookmarkService,
    private followService: FollowService,
    private userService: UserService,
    private tweetService: TweetService,
    private router:Router,
    private route: ActivatedRoute
  ) {}


  
  ngOnInit(): void {
    // Subscribe to changes in the route parameters
    this.route.paramMap.subscribe(paramMap => {
      this.userId = +paramMap.get('id')!; // Extract userId from route
      console.log('test profile id',this.userId);
      this.fetchProfileData(); // Fetch data for the new userId
    
    });
  }
  fetchProfileData(): void {
    
    this.getUser();
    
    this.getFollowers();
    this.getFollowees();
  }

  ngAfterViewInit(): void {
    this.activeButton = this.posts;
     this.getUserBookmarks();
     this.getUserLikedTweets();
     this.getUserRetweets();
   
    this.getUserProfile();
    
     // Set initial active button to 'posts'
  }



//implement infinite scroll
isLoading:boolean=false;
toggleLoading = ()=>this.isLoading=!this.isLoading;


onScroll= ()=>{

if(this.activeButton==this.posts)
  this.getUserProfile();

}

  getUser(): void {
    this.userService.getUser(this.userId).subscribe((data) => {
      this.user = data;
    });
  }

  getUserProfile(): void {
    this.changeTapStatus(this.posts);
    this.toggleLoading();
    this.timelineService.getProfile(this.userId,this.requestParameters).subscribe(
      (data) => {
      
        data.tweets.forEach(tweet=>{
         
          this.profileTweets.add(tweet);
          this.allTweets.add(tweet.id);
        })
        this.count = data.totalCount;
       this.requestParameters.PageNumber++;
     
      },
      (error) => {
        console.log('Error fetching profile data', error);
      },
      ()=>{
        this.toggleLoading()
      }
        
        
    );
  }

  getUserBookmarks(): void {
   
    this.changeTapStatus(this.bookmarks);

    this.bookmarksService.getBookmarks(this.userId).subscribe(
        (data) => {
         
          data.forEach(tweet=>{
           
            this.bookmarkes.add(tweet);
          })
          this.count = this.bookmarkes.size;
        },
        (error) => {
          console.log('Error fetching user bookmarks', error);
        }
      );
    
  }

  getUserLikedTweets(): void {
    
    this.changeTapStatus(this.likes);

    this.timelineService.getUserLikedTweets(this.userId).subscribe(
      (data) => {
    
        data.forEach(tweet=>{
         
          this.likedTweets.add(tweet);
        })
      
        this.count = this.likedTweets.size;
      },
      (error) => {
        console.log('Error fetching user liked tweets', error);
      }
    );  
    
  }

  getUserRetweets(): void {
    
    this.changeTapStatus(this.retweets);

    this.timelineService.getUserRetweets(this.userId).subscribe(
      (data) => {
    
        data.forEach(tweet=>{
         
          this.userRetweets.add(tweet);
        })
      
        this.count = this.userRetweets.size;
        console.log(this.userRetweets);
      },
      (error) => {
        console.log('Error fetching user liked tweets', error);
      }
    );  
    
  }

  getFollowers(): void {
    this.followService.getFollowers(this.userId).subscribe(
      (data: CreatedUser[]) => {
        this.followers = data || [];
      },
      (error) => {
        console.log('Error fetching followers data', error);
      }
    );
  }

  getFollowees(): void {
    this.followService.getFollowees(this.userId).subscribe(
      (data: CreatedUser[]) => {
        this.followees = data || [];
      },
      (error) => {
        console.log('Error fetching followees data', error);
      }
    );
  }

  changeTapStatus(selectedButton: ElementRef<HTMLButtonElement>): void {
    if (this.activeButton) {
      this.activeButton.nativeElement.style.backgroundColor = 'inherit';
      this.activeButton.nativeElement.style.color = 'rgb(119, 111, 111)';
    }

    selectedButton.nativeElement.style.backgroundColor = 'rgb(38, 39, 40)';
    selectedButton.nativeElement.style.color = 'white';
    this.activeButton = selectedButton;
  }
  goToFollowees(userId:number):void{

    this.router.navigate(['/followees',userId]);
    }
    goToFollowers(userId:number):void{

      this.router.navigate(['/followers',userId]);
      }
 handleDeletion(tweet:CreatedTweet)
 {
  this.profileTweets.delete(tweet);
 }
 handleUpdate(tweet:CreatedTweet){
  this.tweetForUpdate=tweet;
  this.showTweetPost=false;

 this.tweetService.updateTweet(this.tweetForUpdate).subscribe();

}
 receiveForUpdate(tweet:CreatedTweet)
 {
  this.showTweetPost=true;
  this.tweetForUpdate=tweet;
 }
 closePopUp():void{
  this.showTweetPost=false;
}



}
