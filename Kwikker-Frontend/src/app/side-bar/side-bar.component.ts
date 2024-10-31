import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

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

constructor(private router:Router){}
ngOnInit(): void {

}

viewNotifications(): void {
this.router.navigate(['notifications']);
  

 
}

goToProfile(userId:number):void{

  this.router.navigate(['profile',userId]);
  }
}
