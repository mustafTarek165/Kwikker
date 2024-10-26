import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CreatedTweet, TweetForCreation } from "../Models/Tweet.model";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root' // Add this to make the service a singleton across the app
})
export class TweetService{

    private Url='https://localhost:7246/api/Tweets';
    constructor(private http:HttpClient)
    {
                  console.log("hello from Tweet Service")
    }

    getTweetsByUserId(userId: number): Observable<CreatedTweet[]> {
        return this.http.get<CreatedTweet[]>(`${this.Url}/User/${userId}`);
      }

    createTweet(userId:number,tweetToCreate:TweetForCreation):Observable<any>{
         
      return this.http.post(`${this.Url}/${userId}`, tweetToCreate);
    }

}