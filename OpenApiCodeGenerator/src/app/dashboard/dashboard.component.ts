import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ContainerService } from '../service/container.service';
import { StoreService } from '../service/store.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  fileToUpload: File = null;
  fileUploadSuccess: boolean;
  apiInfoList: any = [];

  constructor(private router: Router, private cs: ContainerService, private ss: StoreService) { }

  ngOnInit(): void {
  }

  handleFileInput($event: any) {
    this.fileToUpload = $event.target.files[0];
  }

  uploadFile() {
    var myReader: FileReader = new FileReader();
    myReader.onloadend = (e) => {
      this.cs.getApiInfo(myReader.result as string).subscribe(
        res => {
          console.log(res);
          this.fileUploadSuccess = true;
          this.apiInfoList = res['tags'];
        }, err => {
          console.log(err);
        });
    }
    myReader.readAsText(this.fileToUpload);
  }

  navigateToPropertyViewer(apiDetails: Object) {
    this.ss.setApiDetail(apiDetails);
    this.router.navigate(['/property-viewer']);
  }

}
