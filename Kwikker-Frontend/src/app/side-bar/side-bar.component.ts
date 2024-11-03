import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NotificationService } from '../../Services/Notification.service';

@Component({
  selector: 'app-side-bar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.css'
})
export class SideBarComponent {
@Input()
userId:number=0;

notificationCount!: number;
constructor(private router:Router,private notificationService: NotificationService){}
ngOnInit(): void {

  this.notificationService.notificationCount$.subscribe(
    (count) => {
      this.notificationCount = count;
    }
  );
}
 // Reset notification count when viewing notifications
 viewNotifications() {
  this.notificationService.resetNotificationCount();
  // Make a GET request to fetch all notifications if needed
}


goToProfile(userId:number):void{

  this.router.navigate(['profile',userId]);
  }
}
