import { loadRemoteModule } from '@angular-architects/module-federation';
import { Component, ComponentRef, ElementRef, ViewChild, ViewContainerRef, ViewRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { fromEvent } from 'rxjs';
import { IUser } from './core/interfaces/iuser';
import { UserService } from './core/services/user.service';
import { Helper } from './core/utilities/helper';
import { ToastMessageService } from './core/utilities/toast-message.service';
import { ModalService } from './shared/modules/modal/modal.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {

  title = 'interviewFrontApp';

  userForm: FormGroup = this.createUserForm();
  users!: Array<IUser>;
  user: IUser | undefined;
  pageNumber: number = 1;
  take: number = 10;
  pagination: any;
  totalPage: number = 1;
  pages: number = 1;
  displayedColumns: string[] = ['id', 'name', 'action'];
  @ViewChild(MatTable) table!: MatTable<ElementRef>;
  dataSource = new MatTableDataSource(this.users);
  selectedIndexForUpdate: number = 0;
  selectedDeleteUser: IUser | undefined;
  selectedUserIndex: number = 0;

  @ViewChild('sidenav', { read: ViewContainerRef })
  viewContainer!: ViewContainerRef;
  ref!: ComponentRef<ViewRef>;

  constructor(
    private userService: UserService,
    private toastr: ToastrService,
    private toastMessageService: ToastMessageService,
    private modalService: ModalService
  ) {
    fromEvent<CustomEvent>(window, 'app-event-bus').subscribe((e) => {
      if (e.detail.eventType === 'closeSideNav') {
        this.onDeletesideNavInstance();
      }
    });
  }

  ngOnInit(): void {
    this.getPagination();
  }

  createUserForm(): FormGroup {
    return new FormGroup({
      'id': new FormControl(null),
      'name': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
      'email': new FormControl(null, [Validators.required, Validators.email]),
    })
  }

  getPagination(): void {

    this.userService.getPagination(this.pageNumber, this.take).subscribe({
      next: (observer) => {
        console.log(observer);
        this.users = observer.data.users;
        this.dataSource = new MatTableDataSource(this.users);
        this.pagination = Helper.paginate(observer.data.count, this.pageNumber, this.take);
        this.setPaginateValue(this.pagination);
        this.toastMessageService.showMessages(observer);

      },
      error: (err) => {
        this.toastMessageService.showMessages(err.error);
      },
      complete: () => {
        console.info('complete');
      }
    });
  }

  setPaginateValue(pagination: any): void {
    this.totalPage = pagination.totalPages;
    this.pages = pagination.pages;
    if (pagination.pageNumber <= 0) {
      pagination.pageNumber = 1;
    }
    this.pageNumber = pagination.pageNumber;
  }

  onNextPage(): void {
    if (this.pageNumber < this.pagination.totalPages) {
      this.pageNumber++;
      this.getPagination();
    }
  }

  onPreviousPage(): void {
    if (this.pageNumber <= this.pagination.endPage && this.pageNumber > this.pagination.startPage) {
      this.pageNumber--;
      this.getPagination();
    }
  }

  onGoToPage(pageNumber: number): void {
    if (this.pageNumber !== pageNumber) {
      this.pageNumber = pageNumber;
      this.getPagination();
    }
  }

  onToggleDelete(user: IUser, selectedUserIndex: number): void {
    this.selectedDeleteUser = user;
    this.selectedUserIndex = selectedUserIndex;
    this.openModal('confirmDeleteModal');
  }

  deleteUser(): void {

    if (!this.selectedDeleteUser?.id) {
      return;
    }

    this.userService.delete(this.selectedDeleteUser.id).subscribe({
      next: (observer) => {
        console.log(observer);

        this.users.splice(this.selectedUserIndex, 1);
        this.table.renderRows();
        this.closeModal('confirmDeleteModal');
        this.toastr.success('User successfully deleted');
        this.toastMessageService.showMessages(observer);

      },
      error: (err) => {
        this.toastMessageService.showMessages(err.error);
      },
      complete: () => {
        console.info('complete');
      }
    });
  }

  get(userId: string): void {

    this.userService.get(userId).subscribe({
      next: (observer) => {
        console.log(observer);

        this.userForm.patchValue(observer.data.user);
        this.toastMessageService.showMessages(observer);
      },
      error: (err) => {
        this.toastMessageService.showMessages(err.error);
      },
      complete: () => {
        console.info('complete');
      }
    });
  }

  openModal(modalId: string): void {
    this.modalService.open(modalId);
  }

  closeModal(modalId: string): void {
    this.modalService.close(modalId);
  }

  getErrorMessage(controlName: string) {
    if (this.userForm.get(controlName)?.hasError('required')) {
      return 'You must enter a value';
    }
    return this.userForm.get(controlName)?.hasError(controlName) ? `Not a valid ${controlName}` : '';
  }

  create(): void {

    if (this.userForm.invalid) {
      this.userForm.markAllAsTouched();
      this.toastr.warning('please complete the form');
      return;
    }

    this.userService.create(this.userForm.value).subscribe({
      next: (observer) => {
        console.log(observer);

        this.users.unshift(observer.data.user);
        this.table.renderRows();
        this.toastr.success('User successfully created');
        this.closeModal('userCreateUpdateModal');
        this.userForm = this.createUserForm();
        this.toastMessageService.showMessages(observer);

      },
      error: (err) => {
        this.toastMessageService.showMessages(err.error);
      },
      complete: () => {
        console.info('complete');
      }
    });
  }

  onToggleUpdate(userId: string, index: number): void {
    this.selectedIndexForUpdate = index;
    this.get(userId);
    this.openModal('userCreateUpdateModal');
  }

  onCloseUpdateModal(event: any): void {
    this.userForm = this.createUserForm();
  }

  update(): void {

    if (this.userForm.invalid) {
      this.userForm.markAllAsTouched();
      this.toastr.warning('please complete the form');
      return;
    }

    this.userService.update(this.userForm.value).subscribe({
      next: (observer) => {
        console.log(observer);

        this.users.splice(this.selectedIndexForUpdate, 1, observer.data.user);
        this.table.renderRows();
        this.toastr.success('User successfully updated');
        this.closeModal('userCreateUpdateModal');
        this.userForm = this.createUserForm();
        this.toastMessageService.showMessages(observer);

      },
      error: (err) => {
        this.toastMessageService.showMessages(err.error);
      },
      complete: () => {
        console.info('complete');
      }
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }


  async onSendIdToSideNavApp(userId: string): Promise<void> {
    await this.loadSidenav();
    this.onSendId(userId);
  }

  showBackDrop: boolean = false;

  async loadSidenav(): Promise<void> {

    const m = await loadRemoteModule({
      type: 'module',
      remoteEntry: 'http://localhost:4300/remoteEntry.js',
      exposedModule: './Component'
    });

    this.ref = this.viewContainer.createComponent(m.InfoComponent);

    this.showBackDrop = true;

  }

  onSendId(userId: string) {

    const busEvent = new CustomEvent('app-event-bus', {
      bubbles: true,
      detail: {
        eventType: 'getUserId',
        customData: userId
      }
    });
    dispatchEvent(busEvent);

  }

  onDeletesideNavInstance(): void {
    this.viewContainer.remove();
    this.showBackDrop = false;
    // this.viewContainer.detach();
  }

}
