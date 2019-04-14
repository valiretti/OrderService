import { NewRequest } from './../../../models/newRequest';
import { RequestService } from './../../../services/request.service';
import { AuthService } from './../../../services/auth.service';
import { OrderService } from './../../../services/order.service';
import { Order } from './../../../models/order';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit, OnDestroy {
  orderId: number;
  order: Order = new Order();
  isLoading: boolean;
  interval: number = 2500;
  activeSlideIndex = 0;
  userRole: string;
  sub: Subscription;
  displayForm = false;
  requestForm: FormGroup;
  request: NewRequest = new NewRequest();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private auth: AuthService,
    private orderService: OrderService,
    private requestService: RequestService,
    private formBuilder: FormBuilder
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
    this.sub = (this.auth.userRoleObservable()
      .subscribe(r => {
        this.userRole = r;
      }));

    this.requestForm = this.formBuilder.group({
      message: ['']
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  displayform() {
    this.displayForm = true;
  }

  onSubmit() {
    this.request = this.requestForm.value;
    this.request.orderId = this.orderId;
    this.requestService.createExecutorRequest(this.request)
    .subscribe(
      data => {
        this.router.navigate(['/orders']);
      },
      error => {
        console.error(error);
      });
  }
}
