import { environment } from './../../../../environments/environment';
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
  images: number[];
  works: Work[];
  orderForm: FormGroup;
  loading = false;
  order: NewOrder = new NewOrder();
  private baseUrl: string = environment.baseUrl;

  afuConfig = {
    multiple: true,
    formatsAllowed: '.jpg,.png,.jpeg',
    maxSize: '5',
    uploadAPI: {
      url: `${this.baseUrl}/api/photos`,
    },
    theme: 'dragNDrop',
    hideProgressBar: false,
    hideResetBtn: false,
    hideSelectBtn: false,
    replaceTexts: {
      selectFileBtn: 'Select',
      resetBtn: 'Reset',
      uploadBtn: 'Upload',
      dragNDropBox: 'Drag photos for your order here or select files. They will help better evaluate the task.',
      attachPinBtn: 'Attach Files...',
      afterUploadMsg_success: 'Successfully Uploaded !',
      afterUploadMsg_error: 'Upload Failed !'
    }
  };

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private orderService: OrderService,
    private workService: WorkService,
  ) { }

  ngOnInit() {
    this.orderForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      location: ['', Validators.required],
      customerPhoneNumber: ['', Validators.required],
      finishDate: [''],
      price: [''],
      workTypeId: ['']
    });

    this.workService.getWorks()
      .subscribe(
        data => {
          this.works = data;
        },
        error => {
        });
  }

  docUpload($event) {
    this.images = JSON.parse($event.responseText);
  }

  onSubmit() {
    this.loading = true;
    if (this.orderForm.valid) {
      this.order = this.orderForm.value;
      this.order.photos = this.images;
      this.orderService.createOrder(this.order)
        .subscribe(
          data => {
            this.router.navigate(['/orders']);
          },
          error => {
            console.error(error);
            this.loading = false;
          });
    } else {
      return;
    }
  }
}
