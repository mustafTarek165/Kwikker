import { HttpClient, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";
import { CreatedTweet } from "../Models/Tweet.model";
import { RequestParameters } from "../Models/RequestParameters.model";

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

         return this.http.get<CreatedTweet[]>
         (`${this.TimelinesUrl}/followed/${userId}`);
    }
    
    getRandomTimeline(userId:number):Observable<CreatedTweet[]>{
    return this.http.get<CreatedTweet[]>
    
    (`${this.TimelinesUrl}/random/${userId}`);
  }

  getUserLikedTweets(userId: number): Observable<{ tweets: CreatedTweet[], totalCount: number }> {

    console.log('hello from timeline service');
    return this.http.get<CreatedTweet[]>(`${this.TimelinesUrl}/LikedTweets/${userId}`, {
        observe: 'response'
    }).pipe(
        map((response: HttpResponse<CreatedTweet[]>) => {
            // Extract metadata from X-Pagination header
            const paginationHeader = response.headers.get('X-Pagination');
            let totalCount = 0;
            console.log('hello from timeline service');
            if (paginationHeader) {
                const metaData = JSON.parse(paginationHeader);
                totalCount = metaData.TotalCount || 0;
                console.log(metaData);
                console.log(metaData.totalCount);
            }

            return {
                tweets: response.body || [],
                totalCount
            };
        })
    );
}
getProfile(userId: number, requestParameters: RequestParameters): Observable<{ tweets: CreatedTweet[], totalCount: number }> {
  return this.http.get<CreatedTweet[]>
  (`${this.TimelinesUrl}/profile/${userId}?PageNumber=${requestParameters.PageNumber}&PageSize=${requestParameters.PageSize} `, {
      observe: 'response'
  }).pipe(
      map((response: HttpResponse<CreatedTweet[]>) => {
          // Extract metadata from X-Pagination header
          const paginationHeader = response.headers.get('X-Pagination');
          let totalCount = 0;

          if (paginationHeader) {
              const metaData = JSON.parse(paginationHeader);
              totalCount = metaData.TotalCount || 0;
              console.log(metaData);
          }

          return {
              tweets: response.body || [],
              totalCount
          };
      })
  );
}


}