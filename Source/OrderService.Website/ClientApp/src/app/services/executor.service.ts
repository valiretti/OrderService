import { Executor } from './../models/executor';
import { NewExecutor } from './../models/newExecutor';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ExecutorPage } from '../models/executorPage';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ExecutorService {
  private baseUrl: string = environment.baseUrl;

  constructor(
    private http: HttpClient
  ) { }

  getExecutors(page: number, count: number) {
    return this.http.get<ExecutorPage>(`${this.baseUrl}/api/executors?page=${page}&count=${count}`);
  }

  createExecutor(executor: NewExecutor) {
    return this.http.post(`${this.baseUrl}/api/executors`, executor);
  }

  deleteExecutor(id: number) {
    return this.http.delete(`${this.baseUrl}/api/executors/${id}`);
  }

  getExecutorById(id: number) {
    return this.http.get<Executor>(`${this.baseUrl}/api/executors/${id}`);
  }

  updateExecutor(executor: NewExecutor) {
    return this.http.put(`${this.baseUrl}/api/executors`, executor);
  }
}
