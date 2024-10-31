import { Component, ElementRef, ViewChild, AfterViewInit, Input } from '@angular/core';
import { TimelineService } from '../../Services/Timeline.service';
import { CreatedTweet } from '../../Models/Tweet.model';
import { CommonModule } from '@angular/common';
import { TweetComponent } from '../tweet/tweet.component';
import { BookmarkService } from '../../Services/Bookmark.service';
import { FollowService } from '../../Services/Follow.service';
import { CreatedUser } from '../../Models/User.model';
import { TweetWithMetaData } from '../../Models/TweetWithMetaData.model';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../Services/User.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, TweetComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements AfterViewInit {
  
  userId: number = 0;
  profileTweets: CreatedTweet[] = [];
  followers: CreatedUser[] = [];
  followees: CreatedUser[] = [];
  user!: CreatedUser;
  count: number = 0;

  @ViewChild('posts') posts!: ElementRef<HTMLButtonElement>;
  @ViewChild('likes') likes!: ElementRef<HTMLButtonElement>;
  @ViewChild('bookmarks') bookmarks!: ElementRef<HTMLButtonElement>;

  activeButton!: ElementRef<HTMLButtonElement>;

  constructor(
    private timelineService: TimelineService,
    private bookmarksService: BookmarkService,
    private followService: FollowService,
    private userService: UserService,
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
    this.getUserProfile();
    this.getFollowers();
    this.getFollowees();
  }

  ngAfterViewInit(): void {
    this.activeButton = this.posts;
    this.changeTapStatus(this.posts); // Set initial active button to 'posts'
  }

  getUser(): void {
    this.userService.getUser(this.userId).subscribe((data) => {
      this.user = data;
    });
  }

  getUserProfile(): void {
    
    this.timelineService.getProfile(this.userId).subscribe(
      (data) => {
        this.profileTweets = data.tweets || [];
        this.count = data.totalCount;
        console.log('test data',data);

      },
      (error) => {
        console.log('Error fetching profile data', error);
      }
    );
  }

  getUserBookmarks(): void {
    this.changeTapStatus(this.bookmarks);
    this.bookmarksService.getBookmarks(this.userId).subscribe(
      (data) => {
        this.profileTweets = data.tweets;
        this.count = data.totalCount;
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
        this.profileTweets = data.tweets || [];
        this.count = data.totalCount;
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

}
