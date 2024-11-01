import { Component, ElementRef, HostListener, ViewChild } from '@angular/core';
import { TweetComponent } from "../tweet/tweet.component";
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CreatedTweet, TweetForCreation } from '../../Models/Tweet.model';
import { TweetService } from '../../Services/Tweet.service';
import { TimelineService } from '../../Services/Timeline.service';
import { TweetPostComponent } from "../tweet-post/tweet-post.component";
import { debounceTime, Subject } from 'rxjs';

@Component({
  selector: 'app-home-timeline',
  standalone: true,
  imports: [TweetComponent, FormsModule, CommonModule, TweetPostComponent],
  templateUrl: './home-timeline.component.html',
  styleUrl: './home-timeline.component.css'
})
export class HomeTimelineComponent {
  private scrollSubject = new Subject<void>();
  private readonly SCROLL_DEBOUNCE_TIME = 300; // Adjust as needed

  constructor(private tweetService:TweetService,private timelinService:TimelineService)
  {
    this.scrollSubject.pipe(debounceTime(this.SCROLL_DEBOUNCE_TIME)).subscribe(() => {
     // this.onScrollToEnd();
    });
  }
  tweetToCreate: TweetForCreation = {
    content: '',
    mediaUrl: null
  };
   showTweetPost:boolean=false;
  isTweetValid: boolean = false;
  tweets=new Set<CreatedTweet>();

  tweetForUpdate!:CreatedTweet;
  activeButton!:ElementRef<HTMLButtonElement>;
  userId=2;
  // Check the tweet content whenever the input changes
  checkTweetContent(): void {
    this.isTweetValid = this.tweetToCreate.content.trim().length > 0;
  }

  @ViewChild('fileInput') fileInput!: ElementRef;
  @ViewChild('Foryou') ForYou!:ElementRef<HTMLButtonElement>; 
 @ViewChild('Following') Following!:ElementRef<HTMLButtonElement>;

  ngAfterViewInit():void{
    this.activeButton=this.ForYou;

    this.GetRandomTimeline();
    console.log(this.ForYou);
  }

 
 
 GetFollowersNews():void{
    console.log("test id",this.userId);
    this.changeTapStatus(this.Following);
    this.tweets.clear();
    this.timelinService.getFollowersNews(this.userId).subscribe((data:CreatedTweet[])=>{
    
       data.forEach(tweet=>{
        if(!this.tweets.has(tweet))
        this.tweets.add(tweet);
       })

    },  (error) => {
      console.error('Error fetching home timeline', error);  // Handle error
    })
      }

  GetRandomTimeline():void{

   this.changeTapStatus(this.ForYou);
   this.tweets.clear();
    this.timelinService.getRandomTimeline(this.userId).subscribe((data:CreatedTweet[])=>{
      
      data.forEach(tweet=>{
        this.tweets.add(tweet);
       })
    },(error)=>{
      console.error('Error fetching random timeline',error);
    })
  }


  submitTweet(): void {
    this.tweetService.createTweet(this.userId,this.tweetToCreate).subscribe({
      next: (response) => {
        console.log('Response from server:', response,response.id);
        // Here you can access and handle the response from the backend
           if(response)
           {
           
           let newTweet:CreatedTweet={
             id: response.id,
             mediaUrl: response.mediaUrl,
             content: response.content,
             userId: response.userId,
             createdAt: response.createdAt,
             profilePicture: response.profilePicture,
             userName: response.userName,
             likesNumber:response.likesNumber,
             retweetsNumber:response.retweetsNumber,
             bookmarksNumber:response.bookmarksNumber
           }    
           this.tweets.add(newTweet);          

            this.tweetToCreate={
              content:'',
              mediaUrl:null
            }
              
           }
      },
      error: (error) => {
        console.error('Error occurred:', error);
        // Handle the error response
      }
    });
  }



    // Trigger the hidden file input
    triggerFileInput(): void {
      this.fileInput.nativeElement.click();
    }
  
    // Handle file selection
    onFileSelected(event: any): void {
       const file = event.target.files[0];
       if (file) {
        const reader = new FileReader();
  
        // Read the file content as a Data URL
        reader.onload = (e: any) => {
          
          this.tweetToCreate.mediaUrl = e.target.result;
        };
  
        reader.readAsDataURL(file);  // Read the file as Data URL (for image preview)
  
      }
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
    handleDeletion(tweet:CreatedTweet)
 {
  this.tweets.delete(tweet);
 }
 receiveForUpdate(tweet:CreatedTweet)
 {
       this.showTweetPost=true;
    this.tweetForUpdate=tweet;
 }
    handleUpdate(tweet:CreatedTweet){
      this.tweetForUpdate=tweet;
      this.showTweetPost=false;

     this.tweetService.updateTweet(this.tweetForUpdate).subscribe();

    }
   closePopUp():void{
          this.showTweetPost=false;
   }

    // HostListener to listen to the scroll event on the window
  /*@HostListener('window:scroll', [])
  onWindowScroll(): void {
    // Calculate if the user has scrolled to the bottom
    const scrollTop = window.scrollY || document.documentElement.scrollTop;
    const windowHeight = window.innerHeight;
    const documentHeight = document.documentElement.scrollHeight;

    if (scrollTop + windowHeight >= documentHeight - 40) {
      // Trigger your event or action when the user reaches the bottom
      this.onScrollToEnd();
    }
  }
  isLoading:boolean=false;
  onScrollToEnd(): void {
    if (this.isLoading) {
      return; // If already loading, exit the function to prevent multiple requests
    }
    
    this.isLoading = true; // Show loading indicator
  
    // Check which button is active and make the corresponding request
    if (this.activeButton === this.Following) {
      this.timelinService.getFollowersNews(this.userId).subscribe(
        (data: CreatedTweet[]) => {
          // Add tweets to your collection
          data.forEach(tweet => {
            this.tweets.add(tweet);
          });
        },
        (error) => {
          console.error('Error fetching home timeline', error); // Handle error
        },
        () => {
          // This callback runs when the observable completes, regardless of success or failure
          this.isLoading = false; // Hide loading indicator
        }
      );
  
    } else if (this.activeButton === this.ForYou) {
      this.timelinService.getRandomTimeline(this.userId).subscribe(
        (data: CreatedTweet[]) => {
          // Add tweets to your collection
          data.forEach(tweet => {
            this.tweets.add(tweet);
          });
        },
        (error) => {
          console.error('Error fetching random timeline', error); // Handle error
        },
        () => {
          // This callback runs when the observable completes, regardless of success or failure
          this.isLoading = false; // Hide loading indicator
        }
      );
    }
  }*/
  
  
  }

