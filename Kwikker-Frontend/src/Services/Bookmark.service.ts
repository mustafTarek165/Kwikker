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

    getBookmarks(userId: number): Observable< number[]> {
        return this.http.get<number[]>(`${this.BookmarksUrl}/${userId}`);

    }
    createBookmark(userId:number,tweetId:number):Observable<any>{
        return this.http.post(`${this.BookmarksUrl}/${userId}/${tweetId}`,[]);
      }
      removeBookmark(userId:number,tweetId:number):Observable<any>{
        return this.http.delete(`${this.BookmarksUrl}/${userId}/${tweetId}`);
      }
  
    
}
