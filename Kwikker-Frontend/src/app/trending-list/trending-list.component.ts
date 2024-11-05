import { Component } from '@angular/core';
import { ActivatedRoute, Route } from '@angular/router';
import { TrendService } from '../../Services/Trend.service';
import { CreatedTweet } from '../../Models/Tweet.model';
import { CommonModule } from '@angular/common';
import { TweetComponent } from "../tweet/tweet.component";
@Component({
  selector: 'app-trending-list',
  standalone: true,
  imports: [CommonModule, TweetComponent],
  templateUrl: './trending-list.component.html',
  styleUrl: './trending-list.component.css'
})
export class TrendingListComponent {
//route params
hashtag:string='';
userId:number=0;

isLoading:boolean=false;
relatedTweets:CreatedTweet[]=[];

constructor(private route:ActivatedRoute,private trendService :TrendService){ console.log('trending list reached')}
  ngOnInit():void{
    this.route.paramMap.subscribe(paramMap => {
      this.hashtag = paramMap.get('hashtag')!; // Extract userId from route
      this.getRelatedTweets(); 
    });
    this.route.paramMap.subscribe(paramMap => {
      this.userId = +paramMap.get('id')!; // Extract userId from route
        
    });
    
  }

 
 getRelatedTweets()
 {
  console.log('trending list',this.userId,this.hashtag);
   this.trendService.getTweetsByTrend(this.hashtag).subscribe((data)=>{
      this.relatedTweets=data;
   })
 }


}