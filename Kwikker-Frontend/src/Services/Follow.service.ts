import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CreatedUser } from "../Models/User.model";
import { Observable } from "rxjs";

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
}