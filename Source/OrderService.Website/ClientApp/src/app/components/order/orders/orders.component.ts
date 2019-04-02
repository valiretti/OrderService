import { Component, OnInit } from '@angular/core';
import { OrderPage } from './../../../models/orderPage';
import { OrderService } from './../../../services/order.service';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  page = 1;
  pageSize = 12;
  isLoading: boolean;
  totalCount: number;
  orders: OrderPage = new OrderPage();

  constructor(
    private orderService: OrderService,
  ) { }

  ngOnInit() {
    this.loadOrders(this.page);
  }

  loadOrders(page: number) {
    this.isLoading = true;
    this.orderService
      .getOrders(page - 1, this.pageSize)
      .subscribe(
        result => {
          this.orders = result;
          this.totalCount = result.totalCount;
          this.page = page;
          this.isLoading = false;
        },
        err => {
          console.error(err);
          this.isLoading = false;
        }
      );
  }
}
