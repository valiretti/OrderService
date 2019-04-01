import { NewOrder } from './../../../models/newOrder';
import { Work } from './../../../models/work';
import { WorkService } from './../../../services/work.service';
import { OrderService } from './../../../services/order.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.css']
})
export class CreateOrderComponent implements OnInit {
  images = [];
  works: Work[];
  orderForm: FormGroup;
  loading = false;
  order: NewOrder = new NewOrder();

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private orderService: OrderService,
    private workService: WorkService
  ) { }

  ngOnInit() {
    this.orderForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      location: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      photos: null,
      finishDate: null,
      price: null
    });

    this.workService.getWorks()
    .subscribe(
      data => {
        this.works = data;
      },
      error => {
      });
  }

  uploaded($event) {
    this.images.push($event.target.files);
  }

  onSubmit() {
    this.loading = true;
    this.order = this.orderForm.value;
    this.order.photos = this.images;
    this.orderService.createOrder(this.order)
      .subscribe(
        data => {
          this.router.navigate(['/orders']);
        },
        error => {
          this.loading = false;
        });
  }
}
