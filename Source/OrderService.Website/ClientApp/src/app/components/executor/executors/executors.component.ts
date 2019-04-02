import { ExecutorService } from './../../../services/executor.service';
import { ExecutorPage } from './../../../models/executorPage';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-executors',
  templateUrl: './executors.component.html',
  styleUrls: ['./executors.component.css']
})
export class ExecutorsComponent implements OnInit {
  page = 1;
  pageSize = 12;
  isLoading: boolean;
  totalCount: number;
  executors: ExecutorPage = new ExecutorPage();

  constructor(
    private executorService: ExecutorService
  ) { }

  ngOnInit() {
    this.loadExecutors(this.page);
  }

  loadExecutors(page: number) {
    this.isLoading = true;
    this.executorService
      .getExecutors(page - 1, this.pageSize)
      .subscribe(
        result => {
          this.executors = result;
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
