import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {
  users: User[];
  paginations: Pagination;
  likesParam: string;
  constructor(private authService: AuthService, private userService: UserService,
     private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'].result;
      this.paginations = data['users'].pagination;
    });
    this.likesParam = 'Likers';
  }
  loadUsers() {
    this.userService.getUsers(this.paginations.currentPage, this.paginations.itemsPerPage, null, this.likesParam)
    .subscribe((res: PaginatedResult<User[]>) => {
        this.users = res.results;
        this.paginations = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }

  pageChanged(event: any): void {
    this.paginations.currentPage = event.page;
    this.loadUsers();
  }
}
