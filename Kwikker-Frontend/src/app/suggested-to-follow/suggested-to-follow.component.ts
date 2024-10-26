import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreatedUser } from '../../Models/User.model';
@Component({
  selector: 'app-suggested-to-follow',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './suggested-to-follow.component.html',
  styleUrl: './suggested-to-follow.component.css'
})
export class SuggestedToFollowComponent {
  
  @Input()
  suggestedUsers :CreatedUser[]=[];





}
