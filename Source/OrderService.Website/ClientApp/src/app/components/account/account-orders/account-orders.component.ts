import { OrderService } from './../../../services/order.service';
import { OrderPage } from './../../../models/orderPage';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-account-orders',
  templateUrl: './account-orders.component.html',
  styleUrls: ['./account-orders.component.css']
})
export class AccountOrdersComponent implements OnInit {
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
      .getOrdersByCustomerId(page - 1, this.pageSize)
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
