import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreatedTweet } from "../Models/Tweet.model";

@Injectable({
    providedIn: 'root' // Add this to make the service a singleton across the app
  })
export class TimelineService
{
    private TimelinesUrl='https://localhost:7246/api/Timelines';
   
    constructor(private http:HttpClient)
    {
                 
    }


    getFollowersNews(userId:number):Observable<CreatedTweet[]>
    {

         return this.http.get<CreatedTweet[]>(`${this.TimelinesUrl}/followed/${userId}`);
    }
    getProfile(userId:number):Observable<CreatedTweet[]>{
        return this.http.get<CreatedTweet[]>(`${this.TimelinesUrl}/profile/${userId}`);
    }
   
    getRandomTimeline(userId:number):Observable<CreatedTweet[]>{
    return this.http.get<CreatedTweet[]>(`${this.TimelinesUrl}/random/${userId}`);
  }
   getUserLikedTweets(userId:number): Observable<CreatedTweet[]>{
    return this.http.get<CreatedTweet[]>(`${this.TimelinesUrl}/LikedTweets/${userId}`);
   }
   
    


}