import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn:'root'
})
export class NotificationService{


    private NotificationUrl='https://localhost:7246/api/Notifications';
   
    constructor(private http:HttpClient)
    {
                 
    }
    

}