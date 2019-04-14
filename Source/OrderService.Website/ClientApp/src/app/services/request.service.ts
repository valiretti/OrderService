import { NewRequest } from './../models/newRequest';
import { RequestPage } from './../models/requestPage';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RequestService {
  private baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getNewExecutorRequests(page: number, count: number) {
    return this.http.get<RequestPage>(
      `${this.baseUrl}/api/requests/executor/new?page=${page}&count=${count}`
    );
  }

  getNewCustomerRequests(page: number, count: number) {
    return this.http.get<RequestPage>(
      `${this.baseUrl}/api/requests/customer/new?page=${page}&count=${count}`
    );
  }

  createExecutorRequest(request: NewRequest) {
    return this.http.post(`${this.baseUrl}/api/requests/executor`, request);
  }

  acceptExecutorRequest(id: number) {
    return this.http.post(`${this.baseUrl}/api/requests/executor/${id}/accept`, {});
  }

  rejectExecutorRequest(id: number) {
    return this.http.post(`${this.baseUrl}/api/requests/executor/${id}/reject`, {});
  }

  createCustomerRequest(request: NewRequest) {
    return this.http.post(`${this.baseUrl}/api/requests/customer`, request);
  }

  acceptCustomerRequest(id: number) {
    return this.http.post(`${this.baseUrl}/api/requests/customer/${id}/accept`, {});
  }

  rejectCustomerRequest(id: number) {
    return this.http.post(`${this.baseUrl}/api/requests/customer/${id}/reject`, {});
  }
}
