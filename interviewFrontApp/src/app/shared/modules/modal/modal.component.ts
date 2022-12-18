import { Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalService } from './modal.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.less']
})
export class ModalComponent implements OnInit {

  @Input() id: string = '';
  @Input() modalCssClass: string = '';
  private element: any;
  @Output() closeModalState: EventEmitter<boolean> = new EventEmitter<boolean>(false);

  constructor(
    private modalService: ModalService,
    private el: ElementRef,
  ) {
    this.element = el.nativeElement;
  }

  ngOnInit(): void {
    // ensure id attribute exists
    if (!this.id) {
      console.error('modal must have an id');
      return;
    }

    // move element to bottom of page (just before </body>) so it can be displayed above everything else
    document.body.appendChild(this.element);

    // close modal on background click
    this.element.addEventListener('click', (el: any) => {
      if (el.target.className.includes('jw-modal')) {
        this.closeModalState.emit(true);
        this.close();
      }

      // commented by naser so we can add more class to jw-modal

      // if (el.target.className === 'jw-modal') {
      //   this.close();
      // }

    });

    // add self (this modal instance) to the modal service so it's accessible from controllers
    this.modalService.add(this);
  }

  // remove self from modal service when component is destroyed
  ngOnDestroy(): void {
    this.modalService.remove(this.id);
    this.element.remove();
  }

  // open modal
  open(): void {
    this.element.style.display = 'block';
    document.body.classList.add('jw-modal-open');
  }

  // close modal
  close(): void {
    this.element.style.display = 'none';
    document.body.classList.remove('jw-modal-open');
  }

}
