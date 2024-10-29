import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CreatedUser } from "../Models/User.model";
import { Observable } from "rxjs";
import { CreatedTweet } from "../Models/Tweet.model";

@Injectable({
    providedIn:'root'
})
export class FollowService{

    private FollowingsUrl='https://localhost:7246/api/Followings';

    constructor(private http:HttpClient){

    }
 getSuggestedUsersToFollow(userId:number): Observable<CreatedUser[]>{
        return this.http.get<CreatedUser[]>(`${this.FollowingsUrl}/random/${userId}`);
    }

    getFollowers(userId:number):Observable<CreatedUser[]>{
        return this.http.get<CreatedUser[]>(`${this.FollowingsUrl}/${userId}/followers`);
    }
    getFollowees(userId:number):Observable<CreatedUser[]>{
        return this.http.get<CreatedUser[]>(`${this.FollowingsUrl}/${userId}/followees`);
    }

    createFollow(followerId:number,followeeId:number):Observable<any>{
         
        return this.http.post(`${this.FollowingsUrl}/${followerId}/follows/${followeeId}`,[]);
      }

   removeFollow(followerId:number,followeeId:number):Observable<any>{
    return this.http.delete(`${this.FollowingsUrl}/${followerId}/unfollows/${followeeId}`);
   }   
}