import { Component, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { TimelineService } from '../../Services/Timeline.service';
import { CreatedTweet } from '../../Models/Tweet.model';
import { CommonModule } from '@angular/common';
import { TweetComponent } from '../tweet/tweet.component';
import { BookmarkService } from '../../Services/Bookmark.service';
import { FollowService } from '../../Services/Follow.service';
import { CreatedUser } from '../../Models/User.model';
import { TweetWithMetaData } from '../../Models/TweetWithMetaData.model';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, TweetComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements AfterViewInit {
  userId = 2;
  profileTweets: CreatedTweet[] = [];
  followers: CreatedUser[] = [];
  followees: CreatedUser[] = [];
  count: number = 0;

  @ViewChild('posts') posts!: ElementRef<HTMLButtonElement>;
  @ViewChild('likes') likes!: ElementRef<HTMLButtonElement>;
  @ViewChild('bookmarks') bookmarks!: ElementRef<HTMLButtonElement>;

  activeButton!: ElementRef<HTMLButtonElement>;

  constructor(
    private timelineService: TimelineService,
    private bookmarksService: BookmarkService,
    private followService: FollowService
  ) {}

  ngAfterViewInit(): void {
    this.activeButton = this.posts;
    this.getUserProfile();
    this.getFollowers();
    this.getFollowees();
  }

  getUserProfile(): void {
    this.changeTapStatus(this.posts);
    this.timelineService.getProfile(this.userId).subscribe(
      (data) => {
        this.profileTweets = data.tweets || [];
        this.count = data.totalCount;
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
            this.count = data.totalCount; // Set the count to the TotalCount from headers
            
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
}
