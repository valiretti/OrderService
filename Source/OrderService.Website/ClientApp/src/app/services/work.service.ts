import { Work } from './../models/work';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class WorkService {
  private baseUrl: string = environment.baseUrl;

  constructor(
    private http: HttpClient
  ) { }

  getWorks() {
    return this.http.get<Work[]>(`${this.baseUrl}/api/works`);
  }

  createWork(work: Work) {
    return this.http.post(`${this.baseUrl}/api/works`, work);
  }

  deleteWork(id: number) {
    return this.http.delete(`${this.baseUrl}/api/works/${id}`);
  }

  getWorkById(id: number) {
    return this.http.get<Work>(`${this.baseUrl}/api/works/${id}`);
  }

  updateWork(work: Work) {
    return this.http.put(`${this.baseUrl}/api/works`, work);
  }
}
