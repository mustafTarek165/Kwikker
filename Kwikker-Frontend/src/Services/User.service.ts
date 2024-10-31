import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CreatedTweet, TweetForCreation } from "../Models/Tweet.model";
import { Observable } from "rxjs";
import { CreatedUser } from "../Models/User.model";

@Injectable({
  providedIn: 'root' // Add this to make the service a singleton across the app
})
export class UserService{

    private Url='https://localhost:7246/api/Users';
    constructor(private http:HttpClient)
    {
                  
    }

    getUser(userId: number): Observable<CreatedUser> {
        return this.http.get<CreatedUser>(`${this.Url}/${userId}`);
      }

   

}