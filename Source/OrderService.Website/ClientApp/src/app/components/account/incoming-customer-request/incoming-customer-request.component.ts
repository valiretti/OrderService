import { RequestService } from './../../../services/request.service';
import { RequestPage } from './../../../models/requestPage';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-incoming-customer-request',
  templateUrl: './incoming-customer-request.component.html',
  styleUrls: ['./incoming-customer-request.component.css']
})
export class IncomingCustomerRequestComponent implements OnInit {
  page = 1;
  pageSize = 12;
  totalCount: number;
  requests: RequestPage = new RequestPage();

  constructor(
    private requestService: RequestService,
    private router: Router
  ) { }

  ngOnInit() {
    this.loadRequests(this.page);
  }

  loadRequests(page: number) {
    this.requestService
      .getNewCustomerRequests(page - 1, this.pageSize)
      .subscribe(
        result => {
          this.requests = result;
          this.totalCount = result.totalCount;
          this.page = page;
        },
        err => {
          console.error(err);
        }
      );
  }

  accept(id: number) {
    this.requestService.acceptCustomerRequest(id)
      .subscribe(
        data => {
          this.router.navigate(['/account/requests']);
        },
        error => {
          console.error(error);
        });
  }

  reject(id: number) {
    this.requestService.rejectCustomerRequest(id)
      .subscribe(
        data => {
          this.router.navigate(['/account/requests']);
        },
        error => {
          console.error(error);
        });
  }
}
