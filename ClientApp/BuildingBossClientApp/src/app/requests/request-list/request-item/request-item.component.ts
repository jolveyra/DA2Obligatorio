import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Request } from '../../request.model';
import { Flat } from '../../../shared/flat.model';
import { Building } from '../../../shared/building.model';
import { User } from '../../../shared/user.model';
import { RequestService } from '../../../services/request.service';

@Component({
  selector: 'app-request-item',
  templateUrl: './request-item.component.html',
  styleUrl: './request-item.component.css'
})
export class RequestItemComponent implements OnInit {
  @Input() request: Request = new Request('', '', new Flat('', '', 0, 0, '', '', '', 0, 0, false), new Building('', '', 0, '', 0, '', '', '', 0, 0), '', new User('', '', '', ''), '');
  @Input() userRole: string = '';
  @Output() requestUpdated = new EventEmitter<{id: string, status: string}>();
  statusList: string[] = ['Pending', 'InProgress', 'Completed'];
  selectedStatus: string = '';

  ngOnInit(): void {
    this.selectedStatus = this.request.status;
  }

  getStatusColor(): string {
    switch (this.selectedStatus) {
      case 'InProgress':
        return 'btn btn-warning dropdown-toggle border-dark-subtle';
      case 'Completed':
        return 'btn btn-success dropdown-toggle border-dark-subtle';
      default:
        return 'btn btn-primary dropdown-toggle border-dark-subtle';
    }
  }

  updateStatus(status: string): void {
    this.selectedStatus = this.request.status = status;
    this.requestUpdated.emit({ id: this.request.id, status: this.request.status });
  }
}
