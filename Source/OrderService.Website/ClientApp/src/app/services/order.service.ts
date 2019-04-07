import { NewOrder } from './../models/newOrder';
import { HttpClient } from '@angular/common/http';
import { Order } from './../models/order';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { OrderPage } from '../models/orderPage';

@Injectable()
export class OrderService {
 private baseUrl: string = environment.baseUrl;

  constructor(
    private http: HttpClient
  ) { }

  getOrders(page: number, count: number) {
    return this.http.get<OrderPage>(`${this.baseUrl}/api/orders?page=${page}&count=${count}`);
  }

  getOrdersByCustomerId(page: number, count: number) {
    return this.http.get<OrderPage>(`${this.baseUrl}/api/orders/user?page=${page}&count=${count}`);
  }

  createOrder(order: NewOrder) {
    return this.http.post(`${this.baseUrl}/api/orders`, order);
  }

  deleteOrder(id: number) {
    return this.http.delete(`${this.baseUrl}/api/orders/${id}`);
  }

  getOrderById(id: number) {
    return this.http.get<Order>(`${this.baseUrl}/api/orders/${id}`);
  }

  updateOrder(order: NewOrder) {
    return this.http.put(`${this.baseUrl}/api/orders`, order);
  }
}
