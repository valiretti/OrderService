import { Router } from '@angular/router';
import { RequestService } from './../../../services/request.service';
import { RequestPage } from './../../../models/requestPage';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-incoming-executor-request',
  templateUrl: './incoming-executor-request.component.html',
  styleUrls: ['./incoming-executor-request.component.css']
})
export class IncomingExecutorRequestComponent implements OnInit {
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
      .getNewExecutorRequests(page - 1, this.pageSize)
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
    this.requestService.acceptExecutorRequest(id)
      .subscribe(
        data => {
          this.router.navigate(['/account/requests']);
        },
        error => {
          console.error(error);
        });
  }

  reject(id: number) {
    this.requestService.rejectExecutorRequest(id)
      .subscribe(
        data => {
          this.router.navigate(['/account/requests']);
        },
        error => {
          console.error(error);
        });
  }
}
