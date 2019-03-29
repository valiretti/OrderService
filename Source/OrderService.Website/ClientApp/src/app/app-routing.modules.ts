import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { CreateAccountComponent } from './components/create-account/create-account.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent, pathMatch: "full"
  }, {
    path: 'counter',
    component: CounterComponent
  }, {
    path: 'fetch-data',
    component: FetchDataComponent
  }, {
    path: 'sign-in',
    component: SignInComponent, pathMatch: "full"
  }, {
    path: 'create-account',
    component: CreateAccountComponent, pathMatch: "full"
  }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }