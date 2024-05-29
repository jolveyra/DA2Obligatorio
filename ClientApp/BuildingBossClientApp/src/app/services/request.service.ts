import { Injectable } from '@angular/core';
import { Request } from '../requests/request.model';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

export interface RequestResponseData {
  id: string;
  description: string;
  flatId: string;
  categoryName: string;
  assignedEmployeeId: string;
  status: string;
}

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  private requests: Request[] = [ // FIXME: leave empty and make a request for real data
    new Request('1', 'Plumbing job', '3', '2', '1', 'Pending'),
    new Request('2', 'Electrical job', '4', '3', '2', 'In progress'),
    new Request('3', 'Roofing job', '5', '4', '3', 'Completed')
  ];

  constructor(
    private httpClient: HttpClient
  ) { }

  fetchRequests(): Observable<RequestResponseData[]> {
    return this.httpClient.get<RequestResponseData[]>('https://localhost:7122/api/v1/requests');
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
