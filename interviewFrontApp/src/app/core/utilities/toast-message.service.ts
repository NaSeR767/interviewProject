import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToastMessageService {

  constructor(
    private toastr: ToastrService
  ) { }

  showMessages(messagesObj: any): void {
    messagesObj?.informationMessages?.forEach((element: any) => {
      this.toastr.info(element);
    });
    messagesObj?.errorMessages?.forEach((element: any) => {
      this.toastr.error(element);
    });
  }

}
