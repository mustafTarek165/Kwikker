import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CreatedBookmark } from "../Models/Bookmark.model";

@Injectable({
    providedIn:'root'
})
export class BookmarkService{

    private BookmarksUrl='https://localhost:7246/api/Bookmarks';

    constructor(private http:HttpClient){

    }

    getBookmarks(userId:number):Observable<CreatedBookmark[]>{
        return this.http.get<CreatedBookmark[]>(`${this.BookmarksUrl}/${userId}`);
    }


}