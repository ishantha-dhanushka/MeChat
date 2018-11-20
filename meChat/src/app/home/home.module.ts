import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';
import { MyFriendsComponent } from './my-friends/my-friends.component';
import { FindFriendsComponent } from './find-friends/find-friends.component';

@NgModule({
  declarations: [HomeComponent, MyFriendsComponent, FindFriendsComponent],
  imports: [
    CommonModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
