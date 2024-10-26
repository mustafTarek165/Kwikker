import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreatedTweet } from "../Models/Tweet.model";
import { CreatedUser } from "../Models/User.model";

@Injectable({
    providedIn: 'root' // Add this to make the service a singleton across the app
  })
export class TimelineService
{
    private TimelinesUrl='https://localhost:7246/api/Timelines';
    private FollowingsUrl='https://localhost:7246/api/Followings';
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

    getSuggestedUsersToFollow(userId:number): Observable<CreatedUser[]>{
        return this.http.get<CreatedUser[]>(`${this.FollowingsUrl}/random/${userId}`);
    }
    


}