import { AuthService } from './../../../services/auth.service';
import { Subscription } from 'rxjs';
import { User } from './../../../models/user';
import { Executor } from './../../../models/executor';
import { ExecutorService } from './../../../services/executor.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-executor',
  templateUrl: './executor.component.html',
  styleUrls: ['./executor.component.css']
})
export class ExecutorComponent implements OnInit, OnDestroy {
  executorId: number;
  executor: Executor = new Executor();
  isLoading: boolean;
  interval: number = 2500;
  activeSlideIndex = 0;
  user: any;
  sub: Subscription;

  constructor(
    private route: ActivatedRoute,
    private executorServise: ExecutorService,
    private authService: AuthService
  ) {
    route.params.subscribe(p => this.executorId = p.id);
  }

  ngOnInit() {
    this.isLoading = true;
    this.sub = this.authService
      .currentUserObservable()
      .subscribe(u => (this.user = u));

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

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

}
