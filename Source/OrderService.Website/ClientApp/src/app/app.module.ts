import { WorkService } from './services/work.service';
import { OrderService } from './services/order.service';
import { AuthGuard } from './guards/auth.guard';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.modules';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { OAuthModule } from 'angular-oauth2-oidc';
import { CreateAccountComponent } from './components/create-account/create-account.component';
import { AuthService } from './services/auth.service';
import { OrdersComponent } from './components/order/orders/orders.component';
import { CreateOrderComponent } from './components/order/create-order/create-order.component';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    SignInComponent,
    CreateAccountComponent,
    OrdersComponent,
    CreateOrderComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule,
    OAuthModule.forRoot({
      resourceServer: {
        allowedUrls: ['https://localhost:55340'],
        sendAccessToken: true
      }
    }),
    FormsModule,
    CollapseModule.forRoot(),
    PaginationModule.forRoot()
  ],
  providers: [
    AuthService,
    OrderService,
    WorkService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
