import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-photoeditor',
  templateUrl: './photoeditor.component.html',
  styleUrls: ['./photoeditor.component.css']
})
export class PhotoeditorComponent implements OnInit {
@Input() photos: Photo[];
@Output() getMemPhotoChange = new EventEmitter<string>();
 uploader: FileUploader;
 hasBaseDropZoneOver: false;
 basUrl = environment.apiUrl;
 currentMain: Photo;
  constructor(private authService: AuthService, private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.initializeuplouder();
  }
  fileOverBase(e: any ): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeuplouder() {
    this.uploader = new FileUploader({
      url: this.basUrl + 'users/' + this.authService.decodeToken.nameid + '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });
    this.uploader.onAfterAddingFile = (file) => {file.withCredentials = false; };

    this.uploader.onSuccessItem = (item, resonse, status, headers) => {
      const res: Photo = JSON.parse(resonse);
      const photo = {
        id: res.id,
        url: res.url,
        dateAdded: res.dateAdded,
        description: res.description,
        isMain: res.isMain
      };
      this.photos.push(photo);

      if (photo.isMain) {
        this.authService.changeMemberPhoto(photo.url);
        this.authService.currUser.photoUrl = photo.url;
        localStorage.setItem('user', JSON.stringify(this.authService.currUser));
      }
    };
  }

  setMainPhoto(photo: Photo) {
    this.userService.setMainPhoto(this.authService.decodeToken.nameid, photo.id).subscribe(() => {
      this.currentMain = this.photos.filter(p => p.isMain === true)[0];
      this.currentMain.isMain = false;
      photo.isMain = true;
      this.authService.changeMemberPhoto(photo.url);
      this.authService.currUser.photoUrl = photo.url;
      localStorage.setItem('user', JSON.stringify(this.authService.currUser));
    }, error => {
      this.alertify.error(error);
    });
  }

  deletePhoto(id: number) {
    this.alertify.confirm('Are you Sure yo want to delete', () => {
      this.userService.deletePhoto(this.authService.decodeToken.nameid, id).subscribe(() => {
        this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
        this.alertify.success('Photo Deleted');
      }, error => {
        this.alertify.error('Delete Failed');
      });
    });
  }
}

