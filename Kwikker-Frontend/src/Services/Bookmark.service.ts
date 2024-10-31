import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CreatedTweet } from '../Models/Tweet.model';


@Injectable({
    providedIn: 'root'
})
export class BookmarkService {

    private BookmarksUrl = 'https://localhost:7246/api/Bookmarks';

    constructor(private http: HttpClient) { }

    getBookmarks(userId: number): Observable<{ tweets: CreatedTweet[], totalCount: number }> {
        return this.http.get<CreatedTweet[]>(`${this.BookmarksUrl}/${userId}`, {
            observe: 'response'
        }).pipe(
            map((response: HttpResponse<CreatedTweet[]>) => {
                // Extract metadata from X-Pagination header
                const paginationHeader = response.headers.get('X-Pagination');
                let totalCount = 0;


                if (paginationHeader) {
                    const metaData = JSON.parse(paginationHeader);
                    console.log(metaData);
                    totalCount = metaData.TotalCount || 0;
                }

                return {
                    tweets: response.body || [],
                    totalCount
                };
            })
        );
    }
    createBookmark(userId:number,tweetId:number):Observable<any>{
        return this.http.post(`${this.BookmarksUrl}/${userId}/${tweetId}`,[]);
      }
      removeBookmark(userId:number,tweetId:number):Observable<any>{
        return this.http.delete(`${this.BookmarksUrl}/${userId}/${tweetId}`);
      }
  
    
}
