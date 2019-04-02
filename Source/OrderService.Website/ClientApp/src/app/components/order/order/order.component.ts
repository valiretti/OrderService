import { OrderService } from './../../../services/order.service';
import { Order } from './../../../models/order';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  orderId: number;
  order: Order = new Order();
  isLoading: boolean;

  constructor(
    private route: ActivatedRoute,
    private orderService: OrderService
  ) {
    route.params.subscribe(p => this.orderId = p.id);
  }

  ngOnInit() {
    this.isLoading = true;
    this.orderService.getOrderById(this.orderId)
      .subscribe(
        result => {
          this.isLoading = false;
          this.order = result;
        },
        err => {
          this.isLoading = false;
          console.error(err);
        }
      );
  }
}
