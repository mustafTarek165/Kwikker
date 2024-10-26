import { Component, ElementRef, ViewChild } from '@angular/core';
import { TweetComponent } from "../tweet/tweet.component";
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CreatedTweet, TweetForCreation } from '../../Models/Tweet.model';
import { TweetService } from '../../Services/Tweet.service';
import { TimelineService } from '../../Services/Timeline.service';
import { CreatedUser, UserForCreation } from '../../Models/User.model';
@Component({
  selector: 'app-home-timeline',
  standalone: true,
  imports: [TweetComponent,FormsModule,CommonModule],
  templateUrl: './home-timeline.component.html',
  styleUrl: './home-timeline.component.css'
})
export class HomeTimelineComponent {


  constructor(private tweetService:TweetService,private timelinService:TimelineService)
  {

  }
  tweetToCreate: TweetForCreation = {
    content: '',
    mediaUrl: null
  };

  isTweetValid: boolean = false;
  tweets: CreatedTweet[] = [];

  userId=2;
  // Check the tweet content whenever the input changes
  checkTweetContent(): void {
    this.isTweetValid = this.tweetToCreate.content.trim().length > 0;
  }

  @ViewChild('fileInput') fileInput!: ElementRef;
  @ViewChild('Foryou') ForYou!:ElementRef<HTMLButtonElement>; 
 @ViewChild('Following') Following!:ElementRef<HTMLButtonElement>;

  ngAfterViewInit():void{
    this.changeTapStatus(this.ForYou,this.Following);
    console.log(this.ForYou);
  }
  ngOnInit(): void {

   this.GetRandomTimeline();


  }
 

  GetFollowersNews():void{
    this.changeTapStatus(this.Following,this.ForYou);

    this.timelinService.getFollowersNews(this.userId).subscribe((data:CreatedTweet[])=>{
      this.tweets=data;
    },  (error) => {
      console.error('Error fetching home timeline', error);  // Handle error
    })
      }

  GetRandomTimeline():void{

   this.changeTapStatus(this.ForYou,this.Following);

    this.timelinService.getRandomTimeline(this.userId).subscribe((data:CreatedTweet[])=>{
      this.tweets=data;
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
             createdAt: response.createdAt
           }    
           this.tweets.unshift(newTweet);          

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

    changeTapStatus(ele1 :ElementRef<HTMLButtonElement>,ele2 :ElementRef<HTMLButtonElement>):void{
      ele1.nativeElement.style.backgroundColor='rgb(38, 39, 40)';
      ele1.nativeElement.style.color='white';

      ele2.nativeElement.style.backgroundColor='inherit';
      ele2.nativeElement.style.color='rgb(119, 111, 111)';
    }
    
    

}
