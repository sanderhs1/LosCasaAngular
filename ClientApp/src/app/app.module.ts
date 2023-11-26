import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ListingsComponent } from './listings/listings.component';
import { ListingDetailComponent } from './listings/listingdetail.component';
import { ConvertToCurrency } from './shared/convert-to-currency.pipe';
import { ListingformComponent } from './listings/listingform.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ListingsComponent,
    ListingDetailComponent,
    ConvertToCurrency,
    ListingformComponent
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'listings', component: ListingsComponent },
      { path: 'listing-detail/:id', component: ListingDetailComponent},
      { path: 'listingform', component: ListingformComponent },
      { path: 'listingform/:mode/:id', component: ListingformComponent },
      { path: '**', redirectTo: '', pathMatch: 'full' },
      
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
