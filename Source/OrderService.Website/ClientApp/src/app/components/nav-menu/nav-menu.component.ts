import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Subscription,  } from 'rxjs';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit, OnDestroy {
  isExpanded = false;
  isLogIn: boolean = false;
  sub: Subscription;
  userRole: string;

  constructor(private auth: AuthService) {}

  ngOnInit() {
    this.sub = this.auth.isLoggedInObservable()
      .subscribe(res => {
        this.isLogIn = res;
      });
    this.sub.add(this.auth.userRoleObservable()
     .subscribe(r => {
       this.userRole = r;
     }));
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  signOut() {
    this.auth.signOut();
  }
}
