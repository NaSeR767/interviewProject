import { Component, OnInit } from '@angular/core';
import { fromEvent, Subscription } from 'rxjs';
import { IUser } from '../core/interfaces/iuser';
import { UserService } from '../core/services/user.service';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.less']
})
export class InfoComponent implements OnInit {

  eventBusSubscription: Subscription = new Subscription();
  user: IUser | undefined;
  showFiller = false;

  constructor(
    private userService: UserService,
  ) {
    this.eventBusSubscription = fromEvent<CustomEvent>(window, 'app-event-bus').subscribe((e) => this.onEventHandler(e));
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.eventBusSubscription.unsubscribe();
  }

  onEventHandler(e: CustomEvent) {
    if (e.detail.eventType === 'getUserId') {
      let userId: string = e.detail.customData;
      this.get(userId);
    }
  }

  get(userId: string): void {

    this.userService.get(userId).subscribe({
      next: (observer) => {
        console.log(observer);

        this.user = observer.data.user;

      },
      error: (err) => {
      },
      complete: () => {
        console.info('complete');
      }
    });
  }

  closeSideNav() {
    const busEvent = new CustomEvent('app-event-bus', {
      bubbles: true,
      detail: {
        eventType: 'closeSideNav',
      }
    });
    dispatchEvent(busEvent);

  }

}
