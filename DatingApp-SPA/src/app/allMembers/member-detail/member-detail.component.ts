import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';
import { TabsetComponent } from 'ngx-bootstrap/tabs';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  user: User;

  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  @ViewChild('memberTabs') memberTabs: TabsetComponent;
  constructor(private userService: UserService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
  this.route.data.subscribe(data => {
    this.user = data['users'];
  });

  this.route.queryParams.subscribe(params => {
    const selectTab = params['tab'];
    this.memberTabs.tabs[selectTab > 0 ? selectTab : 0].active = true;
  });

  this.galleryOptions = [{

    width: '500px',
    height: '500px',
    imagePercent: 100,
    thumbnailsColumns: 4,
    imageAnimation: NgxGalleryAnimation.Slide,
    preview: false
  }];
this.galleryImages = this.getImg();
  }
getImg() {
  const imagerUrls = [];
for (let i = 0; i < this.user.photos.length; i++) {
  imagerUrls.push({
    small: this.user.photos[i].url,
    medium: this.user.photos[i].url,
    big: this.user.photos[i].url,
    description: this.user.photos[i].description
  });
}
return imagerUrls;
}

selectTab(tabId: number) {
  this.memberTabs.tabs[tabId].active = true;
}
}
