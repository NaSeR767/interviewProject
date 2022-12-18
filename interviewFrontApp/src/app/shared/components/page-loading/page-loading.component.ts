import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { LoadingService } from 'src/app/core/utilities/loading.service';

@Component({
  selector: 'app-page-loading',
  templateUrl: './page-loading.component.html',
  styleUrls: ['./page-loading.component.less']
})
export class PageLoadingComponent implements OnInit {

  loading: Subject<boolean> = this.loadingService.isLoading;

  constructor(private loadingService: LoadingService) { }

  ngOnInit(): void {
  }

}
