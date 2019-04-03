import { Executor } from './../../../models/executor';
import { ExecutorService } from './../../../services/executor.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-executor',
  templateUrl: './executor.component.html',
  styleUrls: ['./executor.component.css']
})
export class ExecutorComponent implements OnInit {
  executorId: number;
  executor: Executor = new Executor();
  isLoading: boolean;
  interval: number = 2500;
  activeSlideIndex = 0;

  constructor(
    private route: ActivatedRoute,
    private executorServise: ExecutorService
  ) {
    route.params.subscribe(p => this.executorId = p.id);
  }

  ngOnInit() {
    this.isLoading = true;
    this.executorServise.getExecutorById(this.executorId)
      .subscribe(
        result => {
          this.isLoading = false;
          this.executor = result;
        },
        err => {
          this.isLoading = false;
          console.error(err);
        }
      );
  }

}
