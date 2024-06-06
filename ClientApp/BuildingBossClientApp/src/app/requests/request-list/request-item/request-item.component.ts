import { Component, Input, OnInit } from '@angular/core';
import { ManagerRequest } from '../../request.model';
import { Flat } from '../../../shared/flat.model';
import { Building } from '../../../shared/building.model';
import { User } from '../../../shared/user.model';

@Component({
  selector: 'app-request-item',
  templateUrl: './request-item.component.html',
  styleUrl: './request-item.component.css'
})
export class RequestItemComponent implements OnInit {
  @Input() request: ManagerRequest = new ManagerRequest('', '', new Flat('', '', 0, 0, '', '', '', 0, 0, false), new Building('', '', 0, '', 0, '', '', '', 0, 0), '', new User('', '', '', ''), '');
  @Input() userRole: string = '';
  status: string = '';

  ngOnInit(): void {
    this.status = this.request.status;
  }

  updateStatus(status: string): void {}
}
