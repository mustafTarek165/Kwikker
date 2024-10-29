import { Component, Input,  } from '@angular/core';
import { TweetService } from '../../Services/Tweet.service';
import { CreatedTweet } from '../../Models/Tweet.model';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-tweet',
  standalone: true,
  imports: [CommonModule,HttpClientModule],
  templateUrl: './tweet.component.html',
  styleUrl: './tweet.component.css'
})
export class TweetComponent {
   
 @Input() tweet:CreatedTweet|undefined

  constructor(private tweetService: TweetService) { }


}
