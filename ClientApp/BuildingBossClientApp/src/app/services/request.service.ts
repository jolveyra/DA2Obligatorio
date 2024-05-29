import { Injectable } from '@angular/core';
import { Request } from '../requests/request.model';

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

  getRequest(id: string): Request {
    const request: Request | undefined = this.requests.find(request => request.id === id);

    if (request) {
      return request;
    } else {
      return new Request('', '', '', '', '', '');
    }
  }
}
