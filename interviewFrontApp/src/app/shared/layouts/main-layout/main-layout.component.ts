import { Component, OnInit } from '@angular/core';
import { IUser } from 'src/app/core/interfaces/iuser';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.less']
})
export class MainLayoutComponent implements OnInit {

  user: IUser | undefined;

  constructor(
    private userService: UserService
  ) {
    this.userService.getUserInfo().subscribe(response => {
      // console.log('obs res', response);
      this.user = response;
    })
  }

  ngOnInit(): void {
  }

}
