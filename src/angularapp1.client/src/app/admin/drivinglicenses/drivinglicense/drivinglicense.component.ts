import { Component, Input } from '@angular/core';
import { DrivingLicense } from './drivinglicense';

@Component({
  selector: 'app-drivinglicense',
  templateUrl: './drivinglicense.component.html',
  styleUrl: './drivinglicense.component.scss'
})
export class DrivinglicenseComponent {
  @Input() license!: DrivingLicense;

  constructor() { }
}
