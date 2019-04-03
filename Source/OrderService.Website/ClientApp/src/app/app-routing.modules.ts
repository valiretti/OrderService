import { ExecutorComponent } from './components/executor/executor/executor.component';
import { OrderComponent } from './components/order/order/order.component';
import { ExecutorsComponent } from './components/executor/executors/executors.component';
import { CreateExecutorComponent } from './components/executor/create-executor/create-executor.component';
import { CreateOrderComponent } from './components/order/create-order/create-order.component';
import { OrdersComponent } from './components/order/orders/orders.component';
import { AuthGuard } from './guards/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { CreateAccountComponent } from './components/create-account/create-account.component';

const routes: Routes = [
  {
    path: '', component: HomeComponent
  }, {
    path: 'counter', component: CounterComponent
  }, {
    path: 'orders', component: OrdersComponent
  },
  {
    path: 'orders/create', component: CreateOrderComponent, canActivate: [AuthGuard]
  },
  {
    path: 'orders/:id', component: OrderComponent
  },
  {
    path: 'executors', component: ExecutorsComponent, canActivate: [AuthGuard]
  },
  {
    path: 'executors/create', component: CreateExecutorComponent, canActivate: [AuthGuard]
  },
  {
    path: 'executors/:id', component: ExecutorComponent, canActivate: [AuthGuard]
  },
  {
    path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthGuard]
  }, {
    path: 'sign-in', component: SignInComponent
  }, {
    path: 'create-account', component: CreateAccountComponent
  }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
