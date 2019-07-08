import { Component, OnInit } from '@angular/core';
import { UserService } from '../../_services/user.service';
import { User } from '../../_models/user';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  users: User[];
  paginations: Pagination;
  user: User = JSON.parse(localStorage.getItem('user'));
genderList =  [{value: 'male', display: 'Males'}, {value: 'female', display: 'Females'}];
userparams: any = {};
  constructor(private userservice: UserService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'].results;
      this.paginations = data['users'].pagination;
    });

    this.userparams.gender = this.user.gender ===  'female' ? 'male' : 'female';
    this.userparams.minAge = 18;
    this.userparams.maxAge = 99;
    this.userparams.orderBy = 'lastActive';
    }

    pageChanged(event: any): void {
      this.paginations.currentPage = event.page;
      this.loadUsers();
    }

resetFilters() {
  this.userparams.gender = this.user.gender === 'female' ? 'male' : 'female';
    this.userparams.minAge = 18;
    this.userparams.maxAge = 99;
    this.loadUsers();
}
  loadUsers() {
    this.userservice.getUsers(this.paginations.currentPage, this.paginations.itemsPerPage, this.userparams)
    .subscribe((res: PaginatedResult<User[]>) => {
        this.users = res.results;
        this.paginations = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

}
