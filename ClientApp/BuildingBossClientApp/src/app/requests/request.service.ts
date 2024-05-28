import { Injectable } from '@angular/core';
import { Request } from './request.model';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  private requests: Request[] = [ // FIXME: leave empty and make a request for real data
    new Request('1', 'Plumbing job', '3', '2', '1', 'Pending'),
    new Request('2', 'Electrical job', '4', '3', '2', 'In progress'),
    new Request('3', 'Roofing job', '5', '4', '3', 'Completed')
  ];

  constructor() { }

  getRequests(): Request[] {
    return this.requests.slice();
  }
}
