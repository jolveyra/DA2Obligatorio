import { Component, Input, OnInit } from '@angular/core';
import { Request } from '../../request.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-request-item',
  templateUrl: './request-item.component.html',
  styleUrl: './request-item.component.css'
})
export class RequestItemComponent implements OnInit {
  @Input() request: Request = new Request('', '', '', '', '', '', );
  @Input() userRole: string = '';
  status: string = '';

  ngOnInit(): void {
    this.status = this.request.status;
  }

  updateStatus(status: string): void {}
}
